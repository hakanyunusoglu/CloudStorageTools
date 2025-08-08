using CloudStorageTools.VideoSizeFinder.Dtos;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.Processing;
using Image = SixLabors.ImageSharp.Image;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class ImageResizeService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly Configuration _imageSharpConfig;

        public ImageResizeService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(5);

            // ImageSharp konfigürasyonu
            _imageSharpConfig = Configuration.Default.Clone();

            // Memory allocation limits
            _imageSharpConfig.MemoryAllocator = MemoryAllocator.Create(new MemoryAllocatorOptions
            {
                MaximumPoolSizeMegabytes = 512
            });
        }

        public async Task<byte[]> DownloadImageAsync(string imageUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(imageUrl);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading image from URL: {ex.Message}", ex);
            }
        }

        public (int width, int height) GetImageDimensions(byte[] imageData)
        {
            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var image = Image.Load(decoderOptions, imageData);
                return (image.Width, image.Height);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting image dimensions: {ex.Message}", ex);
            }
        }

        public bool ShouldResize(MediaResizerDto media, MediaResizeCriteriaDto criteria)
        {
            decimal maxFileSizeBytes = criteria.MaxFileSizeMB * 1024 * 1024;
            bool exceedsFileSize = media.MediaFileSize > maxFileSizeBytes;

            bool exceedsDimensions = false;
            if (media.Width.HasValue && media.Height.HasValue)
            {
                exceedsDimensions = media.Width.Value > criteria.MaxWidth ||
                                   media.Height.Value > criteria.MaxHeight;
            }

            return exceedsFileSize || exceedsDimensions;
        }

        public async Task<byte[]> ResizeImageAsync(byte[] imageData, MediaResizeCriteriaDto criteria)
        {
            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var image = Image.Load(decoderOptions, imageData);
                var originalFormat = image.Metadata.DecodedImageFormat;

                var (newWidth, newHeight) = CalculateNewSize(
                    image.Width,
                    image.Height,
                    criteria.TargetWidth,
                    criteria.TargetHeight,
                    criteria.MaintainAspectRatio,
                    criteria.ResizeMode);

                if (newWidth == image.Width && newHeight == image.Height)
                {
                    return await OptimizeImageQualityAsync(image, originalFormat, criteria.Quality);
                }

                // DÜZELME 1: ResizeOptions için doğru parametreler
                var resizeOptions = new ResizeOptions
                {
                    Size = new SixLabors.ImageSharp.Size(newWidth, newHeight),
                    Mode = GetResizeMode(criteria.ResizeMode), // Method adı düzeltildi
                    Position = AnchorPositionMode.Center,
                    Sampler = KnownResamplers.Lanczos3,
                    Compand = true
                };

                // DÜZELME 2: Task.Run gereksiz, doğrudan mutate edebiliriz
                image.Mutate(x => x.Resize(resizeOptions));

                return await EncodeImageAsync(image, originalFormat, criteria.Quality);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resizing image: {ex.Message}", ex);
            }
        }

        private async Task<byte[]> OptimizeImageQualityAsync(Image image, IImageFormat? originalFormat, int quality)
        {
            return await EncodeImageAsync(image, originalFormat, quality);
        }

        private (int width, int height) CalculateNewSize(int originalWidth, int originalHeight,
            int targetWidth, int targetHeight, bool maintainAspectRatio, string resizeMode)
        {
            if (!maintainAspectRatio || resizeMode.Equals("Stretch", StringComparison.OrdinalIgnoreCase))
            {
                return (targetWidth, targetHeight);
            }

            double widthRatio = (double)targetWidth / originalWidth;
            double heightRatio = (double)targetHeight / originalHeight;

            double ratio = resizeMode.ToUpper() switch
            {
                "FIT" => Math.Min(widthRatio, heightRatio),
                "CROP" => Math.Max(widthRatio, heightRatio),
                _ => Math.Min(widthRatio, heightRatio)
            };

            int newWidth = Math.Max(1, (int)Math.Round(originalWidth * ratio));
            int newHeight = Math.Max(1, (int)Math.Round(originalHeight * ratio));

            return (newWidth, newHeight);
        }

        // DÜZELME 3: Method signature düzeltildi ve ResizeOptions için gereksiz struct kaldırıldı
        private ResizeMode GetResizeMode(string resizeMode)
        {
            return resizeMode.ToUpper() switch
            {
                "FIT" => ResizeMode.Max,
                "STRETCH" => ResizeMode.Stretch,
                "CROP" => ResizeMode.Crop,
                _ => ResizeMode.Max
            };
        }

        private async Task<byte[]> EncodeImageAsync(Image image, IImageFormat? originalFormat, int quality)
        {
            using var outputStream = new MemoryStream();

            if (originalFormat?.Name.Equals("JPEG", StringComparison.OrdinalIgnoreCase) == true)
            {
                var encoder = new JpegEncoder
                {
                    Quality = quality
                };

                await image.SaveAsJpegAsync(outputStream, encoder);
            }
            else if (originalFormat?.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase) == true)
            {
                var encoder = new PngEncoder
                {
                    CompressionLevel = PngCompressionLevel.BestCompression,
                    ColorType = PngColorType.RgbWithAlpha,
                    BitDepth = PngBitDepth.Bit8
                };
                await image.SaveAsPngAsync(outputStream, encoder);
            }
            else if (originalFormat?.Name.Equals("WEBP", StringComparison.OrdinalIgnoreCase) == true)
            {
                var encoder = new WebpEncoder
                {
                    Quality = quality,
                    Method = WebpEncodingMethod.BestQuality,
                    FileFormat = WebpFileFormatType.Lossy
                };
                await image.SaveAsWebpAsync(outputStream, encoder);
            }
            else
            {
                var encoder = new JpegEncoder
                {
                    Quality = quality
                };

                await image.SaveAsJpegAsync(outputStream, encoder);
            }

            return outputStream.ToArray();
        }

        // DÜZELME 4: GetImageDimensions'da da _imageSharpConfig kullanılması
        public string FormatFileSize(long bytes)
        {
            if (bytes == 0) return "0 B";

            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            GC.SuppressFinalize(this);
        }

        public bool IsValidImage(byte[] imageData)
        {
            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var image = Image.Load(decoderOptions, imageData);
                return image.Width > 0 && image.Height > 0;
            }
            catch
            {
                return false;
            }
        }

        public string GetImageFormat(byte[] imageData)
        {
            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var image = Image.Load(decoderOptions, imageData);
                return image.Metadata.DecodedImageFormat?.Name ?? "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }

        public async Task<byte[]> StripMetadataAsync(byte[] imageData)
        {
            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var image = Image.Load(decoderOptions, imageData);

                image.Metadata.ExifProfile = null;
                image.Metadata.IccProfile = null;
                image.Metadata.XmpProfile = null;

                using var outputStream = new MemoryStream();
                var format = image.Metadata.DecodedImageFormat;

                if (format?.Name.Equals("JPEG", StringComparison.OrdinalIgnoreCase) == true)
                {
                    await image.SaveAsJpegAsync(outputStream);
                }
                else if (format?.Name.Equals("PNG", StringComparison.OrdinalIgnoreCase) == true)
                {
                    await image.SaveAsPngAsync(outputStream);
                }
                else
                {
                    await image.SaveAsJpegAsync(outputStream);
                }

                return outputStream.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error stripping metadata: {ex.Message}", ex);
            }
        }

        public async Task<Dictionary<string, byte[]>> ResizeImageToBatchAsync(
            byte[] imageData,
            Dictionary<string, (int width, int height)> targetSizes,
            int quality = 90)
        {
            var results = new Dictionary<string, byte[]>();

            try
            {
                var decoderOptions = new DecoderOptions()
                {
                    Configuration = _imageSharpConfig
                };

                using var originalImage = Image.Load(decoderOptions, imageData);
                var originalFormat = originalImage.Metadata.DecodedImageFormat;

                foreach (var (sizeName, (width, height)) in targetSizes)
                {
                    using var resizedImage = originalImage.Clone(x => x.Resize(new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(width, height),
                        Mode = ResizeMode.Max,
                        Position = AnchorPositionMode.Center,
                        Sampler = KnownResamplers.Lanczos3,
                        Compand = true
                    }));

                    // DÜZELME 5: Task.Run gereksiz burada da
                    resizedImage.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Size = new SixLabors.ImageSharp.Size(width, height),
                        Mode = ResizeMode.Max,
                        Position = AnchorPositionMode.Center,
                        Sampler = KnownResamplers.Lanczos3,
                        Compand = true
                    }));

                    var resizedData = await EncodeImageAsync(resizedImage, originalFormat, quality);
                    results[sizeName] = resizedData;
                }

                return results;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in batch resize: {ex.Message}", ex);
            }
        }
    }
}