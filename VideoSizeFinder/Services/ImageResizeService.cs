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
            // Dosya boyutu kontrolü
            long maxFileSizeBytes = criteria.MaxFileSizeMB * 1024 * 1024;
            if (media.MediaFileSize <= maxFileSizeBytes)
            {
                return false;
            }

            // Boyut kontrolü
            if (!media.Width.HasValue || !media.Height.HasValue)
            {
                return false;
            }

            return media.Width.Value > criteria.MaxWidth || media.Height.Value > criteria.MaxHeight;
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

                    // Yeniden boyutlandır
                    using (var resizedImage = new Bitmap(newSize.width, newSize.height))
                    {
                        using (var graphics = Graphics.FromImage(resizedImage))
                        {
                            // Kalite ayarları
                            graphics.CompositingMode = CompositingMode.SourceCopy;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                            // Çiz
                            graphics.DrawImage(originalImage, 0, 0, newSize.width, newSize.height);
                        }

                        // JPEG formatında kaydet
                        using (var outputStream = new MemoryStream())
                        {
                            var encoder = GetEncoder(ImageFormat.Jpeg);
                            var qualityEncoder = System.Drawing.Imaging.Encoder.Quality;
                            var encoderParams = new EncoderParameters(1);
                            encoderParams.Param[0] = new EncoderParameter(qualityEncoder, criteria.Quality);

                            resizedImage.Save(outputStream, encoder, encoderParams);
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
                return ((int)(originalWidth * ratio), (int)(originalHeight * ratio));
            }
            else if (resizeMode == "Crop")
            {
                // En büyük oranı kullan (hedef boyutları tamamen doldurur)
                double ratio = Math.Max(widthRatio, heightRatio);
                return ((int)(originalWidth * ratio), (int)(originalHeight * ratio));
            }

            // Varsayılan: Fit
            double defaultRatio = Math.Min(widthRatio, heightRatio);
            return ((int)(originalWidth * defaultRatio), (int)(originalHeight * defaultRatio));
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        public string FormatFileSize(long bytes)
        {
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
