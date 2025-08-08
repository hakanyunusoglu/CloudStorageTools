using CloudStorageTools.VideoSizeFinder.Dtos;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class ImageResizeService
    {
        private readonly HttpClient _httpClient;

        public ImageResizeService()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromMinutes(5); // 5 dakika timeout
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
                using (var stream = new MemoryStream(imageData))
                using (var image = Image.FromStream(stream))
                {
                    return (image.Width, image.Height);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting image dimensions: {ex.Message}", ex);
            }
        }

        public bool ShouldResize(MediaResizerDto media, MediaResizeCriteriaDto criteria)
        {
            // Dosya boyutu kontrolü - MB'yi byte'a çevir
            decimal maxFileSizeBytes = (decimal)(criteria.MaxFileSizeMB * 1024 * 1024);
            bool exceedsFileSize = media.MediaFileSize > maxFileSizeBytes;

            // Boyut kontrolü
            bool exceedsDimensions = false;
            if (media.Width.HasValue && media.Height.HasValue)
            {
                exceedsDimensions = media.Width.Value > criteria.MaxWidth || media.Height.Value > criteria.MaxHeight;
            }

            return exceedsFileSize || exceedsDimensions;
        }


        public async Task<byte[]> ResizeImageAsync(byte[] imageData, MediaResizeCriteriaDto criteria)
        {
            try
            {
                using (var inputStream = new MemoryStream(imageData))
                using (var originalImage = Image.FromStream(inputStream))
                {
                    // Yeni boyutları hesapla
                    var newSize = CalculateNewSize(
                        originalImage.Width,
                        originalImage.Height,
                        criteria.TargetWidth,
                        criteria.TargetHeight,
                        criteria.MaintainAspectRatio,
                        criteria.ResizeMode);

                    // Eğer orijinal boyutlarla aynıysa, orijinali döndür
                    if (newSize.width == originalImage.Width && newSize.height == originalImage.Height)
                    {
                        return imageData;
                    }

                    // Yeniden boyutlandır - PixelFormat'ı koruyarak
                    PixelFormat pixelFormat = originalImage.PixelFormat;

                    // Eğer orijinal format uyumlu değilse, 24bppRgb kullan
                    if (!IsCompatiblePixelFormat(pixelFormat))
                    {
                        pixelFormat = PixelFormat.Format24bppRgb;
                    }

                    using (var resizedImage = new Bitmap(newSize.width, newSize.height, pixelFormat))
                    {
                        // DPI'yi orijinalden kopyala
                        resizedImage.SetResolution(originalImage.HorizontalResolution, originalImage.VerticalResolution);

                        using (var graphics = Graphics.FromImage(resizedImage))
                        {
                            // Kalite ayarları - optimize edilmiş
                            graphics.Clear(Color.Transparent); // Şeffaflığı koru
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Çizim alanını tanımla
                            var destRect = new Rectangle(0, 0, newSize.width, newSize.height);
                            var srcRect = new Rectangle(0, 0, originalImage.Width, originalImage.Height);

                            // Resmi çiz
                            graphics.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
                        }

                        // Formatı belirle - orijinal formatı korumaya çalış
                        ImageFormat outputFormat = GetOutputFormat(originalImage);

                        using (var outputStream = new MemoryStream())
                        {
                            if (outputFormat.Equals(ImageFormat.Jpeg))
                            {
                                SaveAsJpeg(resizedImage, outputStream, criteria.Quality);
                            }
                            else if (outputFormat.Equals(ImageFormat.Png))
                            {
                                SaveAsPng(resizedImage, outputStream);
                            }
                            else
                            {
                                // Diğer formatlar için varsayılan olarak JPEG kullan
                                SaveAsJpeg(resizedImage, outputStream, criteria.Quality);
                            }

                            return outputStream.ToArray();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error resizing image: {ex.Message}", ex);
            }
        }

        private bool IsCompatiblePixelFormat(PixelFormat pixelFormat)
        {
            // GDI+ ile uyumlu pixel formatları
            return pixelFormat == PixelFormat.Format24bppRgb ||
                   pixelFormat == PixelFormat.Format32bppArgb ||
                   pixelFormat == PixelFormat.Format32bppRgb ||
                   pixelFormat == PixelFormat.Format32bppPArgb;
        }

        private ImageFormat GetOutputFormat(Image originalImage)
        {
            // Orijinal formatı koru, desteklenmeyenler için JPEG kullan
            if (originalImage.RawFormat.Equals(ImageFormat.Png))
                return ImageFormat.Png;
            else if (originalImage.RawFormat.Equals(ImageFormat.Gif))
                return ImageFormat.Png; // GIF şeffaflığını korumak için PNG kullan
            else
                return ImageFormat.Jpeg; // Varsayılan JPEG
        }

        private void SaveAsJpeg(Image image, Stream stream, int quality)
        {
            var encoder = GetEncoder(ImageFormat.Jpeg);
            if (encoder != null)
            {
                var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                using (var encoderParams = new EncoderParameters(1))
                {
                    encoderParams.Param[0] = new EncoderParameter(qualityEncoder, quality);
                    image.Save(stream, encoder, encoderParams);
                }
            }
            else
            {
                image.Save(stream, ImageFormat.Jpeg);
            }
        }

        private void SaveAsPng(Image image, Stream stream)
        {
            var encoder = GetEncoder(ImageFormat.Png);
            if (encoder != null)
            {
                image.Save(stream, encoder, null);
            }
            else
            {
                image.Save(stream, ImageFormat.Png);
            }
        }

        private (int width, int height) CalculateNewSize(int originalWidth, int originalHeight,
            int targetWidth, int targetHeight, bool maintainAspectRatio, string resizeMode)
        {
            if (!maintainAspectRatio || resizeMode == "Stretch")
            {
                return (targetWidth, targetHeight);
            }

            double widthRatio = (double)targetWidth / originalWidth;
            double heightRatio = (double)targetHeight / originalHeight;

            if (resizeMode == "Fit")
            {
                // En küçük oranı kullan (tüm resim hedef boyutlara sığar)
                double ratio = Math.Min(widthRatio, heightRatio);
                int newWidth = (int)Math.Round(originalWidth * ratio);
                int newHeight = (int)Math.Round(originalHeight * ratio);

                // Minimum 1 pixel olsun
                newWidth = Math.Max(1, newWidth);
                newHeight = Math.Max(1, newHeight);

                return (newWidth, newHeight);
            }
            else if (resizeMode == "Crop")
            {
                // En büyük oranı kullan (hedef boyutları tamamen doldurur)
                double ratio = Math.Max(widthRatio, heightRatio);
                int newWidth = (int)Math.Round(originalWidth * ratio);
                int newHeight = (int)Math.Round(originalHeight * ratio);

                // Minimum 1 pixel olsun
                newWidth = Math.Max(1, newWidth);
                newHeight = Math.Max(1, newHeight);

                return (newWidth, newHeight);
            }

            // Varsayılan: Fit
            double defaultRatio = Math.Min(widthRatio, heightRatio);
            int defaultWidth = (int)Math.Round(originalWidth * defaultRatio);
            int defaultHeight = (int)Math.Round(originalHeight * defaultRatio);

            // Minimum 1 pixel olsun
            defaultWidth = Math.Max(1, defaultWidth);
            defaultHeight = Math.Max(1, defaultHeight);

            return (defaultWidth, defaultHeight);
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageEncoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

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
        }
    }
}
