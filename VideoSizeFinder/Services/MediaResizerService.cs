using CloudStorageTools.VideoSizeFinder.Dtos;
using CsvHelper;
using CsvHelper.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class MediaResizerService
    {
        private readonly ImageResizeService _imageResizeService;

        public MediaResizerService()
        {
            _imageResizeService = new ImageResizeService();
        }

        // Dosya boyutunu parse eden yardımcı method
        private long ParseFileSize(string fileSizeStr)
        {
            if (string.IsNullOrWhiteSpace(fileSizeStr))
                return 0;

            fileSizeStr = fileSizeStr.Trim().ToUpper();

            // Eğer sadece sayı ise (bytes olarak kabul et)
            if (long.TryParse(fileSizeStr, out long bytesValue))
            {
                return bytesValue;
            }

            // Regex ile sayı ve birim ayır
            var match = Regex.Match(fileSizeStr, @"^(\d+(?:\.\d+)?)\s*([KMGT]?B?)$");
            if (!match.Success)
            {
                // Fallback: sadece sayısal kısmı al
                var numberMatch = Regex.Match(fileSizeStr, @"(\d+(?:\.\d+)?)");
                if (numberMatch.Success && double.TryParse(numberMatch.Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double fallbackValue))
                {
                    return (long)fallbackValue; // Bytes olarak kabul et
                }
                return 0;
            }

            if (!double.TryParse(match.Groups[1].Value, NumberStyles.Float, CultureInfo.InvariantCulture, out double size))
                return 0;

            string unit = match.Groups[2].Value;

            long result;
            switch (unit)
            {
                case "KB":
                case "K":
                    result = (long)(size * 1024);
                    break;

                case "MB":
                case "M":
                    result = (long)(size * 1024 * 1024);
                    break;

                case "GB":
                case "G":
                    result = (long)(size * 1024 * 1024 * 1024);
                    break;

                case "TB":
                case "T":
                    result = (long)(size * 1024L * 1024 * 1024 * 1024);
                    break;

                case "B":
                case "":
                    result = (long)size;
                    break;

                default:
                    result = (long)size; // bilinmeyen birim
                    break;
            }

            return result;
        }

        public List<MediaResizerDto> LoadMediaListFromCsv(string csvFilePath, bool createUrlMode = false, string baseUrl = "", string token = "")
        {
            var mediaList = new List<MediaResizerDto>();

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    var headers = csv.Context.Reader.HeaderRecord;

                    // Gerekli header'ları bul (case insensitive)
                    string mediaNameHeader = headers?.FirstOrDefault(h =>
                        string.Equals(h, "MediaName", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "Media Name", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "FileName", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "File Name", StringComparison.OrdinalIgnoreCase));

                    string mediaUrlHeader = headers?.FirstOrDefault(h =>
                        string.Equals(h, "MediaUrl", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "Media Url", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "URL", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "Url", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "Path", StringComparison.OrdinalIgnoreCase));

                    string fileSizeHeader = headers?.FirstOrDefault(h =>
                        string.Equals(h, "MediaFileSize", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "Media File Size", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "FileSize", StringComparison.OrdinalIgnoreCase) ||
                        string.Equals(h, "File Size", StringComparison.OrdinalIgnoreCase));

                    if (string.IsNullOrEmpty(mediaNameHeader) || string.IsNullOrEmpty(mediaUrlHeader) || string.IsNullOrEmpty(fileSizeHeader))
                    {
                        string requiredColumns = createUrlMode
                            ? "MediaName, MediaUrl/Path, MediaFileSize"
                            : "MediaName, MediaUrl, MediaFileSize";
                        throw new Exception($"CSV dosyasında gerekli sütunlar bulunamadı. Gerekli sütunlar: {requiredColumns}");
                    }

                    while (csv.Read())
                    {
                        string mediaName = csv.GetField(mediaNameHeader)?.Trim();
                        string mediaUrlOrPath = csv.GetField(mediaUrlHeader)?.Trim();
                        string fileSizeStr = csv.GetField(fileSizeHeader)?.Trim();

                        if (!string.IsNullOrWhiteSpace(mediaName) && !string.IsNullOrWhiteSpace(mediaUrlOrPath))
                        {
                            // URL'yi işle
                            string processedUrl = ProcessMediaUrl(mediaUrlOrPath, createUrlMode, baseUrl, token);

                            // Dosya boyutunu parse et (düzeltilmiş kısım)
                            long fileSize = ParseFileSize(fileSizeStr);

                            var mediaItem = new MediaResizerDto
                            {
                                MediaName = mediaName,
                                MediaUrl = processedUrl,
                                MediaFileSize = fileSize,
                                MediaFileSizeFormatted = _imageResizeService.FormatFileSize(fileSize),
                                ProcessingStatus = "Pending"
                            };

                            mediaList.Add(mediaItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"CSV dosyası okuma hatası: {ex.Message}", ex);
            }

            return mediaList;
        }

        public List<MediaResizerDto> LoadMediaListFromExcel(string excelFilePath, bool createUrlMode = false, string baseUrl = "", string token = "")
        {
            var mediaList = new List<MediaResizerDto>();

            try
            {
                using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
                {
                    var worksheet = package.Workbook.Worksheets.First();
                    int rowCount = worksheet.Dimension.Rows;

                    // Header satırını oku
                    var headers = new Dictionary<string, int>();
                    for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                    {
                        string header = worksheet.Cells[1, col].Value?.ToString()?.Trim();
                        if (!string.IsNullOrEmpty(header))
                        {
                            headers[header.ToLower()] = col;
                        }
                    }

                    // Gerekli sütun indekslerini bul
                    int mediaNameCol = GetColumnIndex(headers, new[] { "medianame", "media name", "filename", "file name" });
                    int mediaUrlCol = GetColumnIndex(headers, new[] { "mediaurl", "media url", "url", "path" });
                    int fileSizeCol = GetColumnIndex(headers, new[] { "mediafilesize", "media file size", "filesize", "file size" });

                    if (mediaNameCol == -1 || mediaUrlCol == -1 || fileSizeCol == -1)
                    {
                        string requiredColumns = createUrlMode
                            ? "MediaName, MediaUrl/Path, MediaFileSize"
                            : "MediaName, MediaUrl, MediaFileSize";
                        throw new Exception($"Excel dosyasında gerekli sütunlar bulunamadı. Gerekli sütunlar: {requiredColumns}");
                    }

                    // Veri satırlarını oku
                    for (int row = 2; row <= rowCount; row++)
                    {
                        string mediaName = worksheet.Cells[row, mediaNameCol].Value?.ToString()?.Trim();
                        string mediaUrlOrPath = worksheet.Cells[row, mediaUrlCol].Value?.ToString()?.Trim();
                        string fileSizeStr = worksheet.Cells[row, fileSizeCol].Value?.ToString()?.Trim();

                        if (!string.IsNullOrWhiteSpace(mediaName) && !string.IsNullOrWhiteSpace(mediaUrlOrPath))
                        {
                            // URL'yi işle
                            string processedUrl = ProcessMediaUrl(mediaUrlOrPath, createUrlMode, baseUrl, token);

                            // Dosya boyutunu parse et (düzeltilmiş kısım)
                            long fileSize = ParseFileSize(fileSizeStr);

                            var mediaItem = new MediaResizerDto
                            {
                                MediaName = mediaName,
                                MediaUrl = processedUrl,
                                MediaFileSize = fileSize,
                                MediaFileSizeFormatted = _imageResizeService.FormatFileSize(fileSize),
                                ProcessingStatus = "Pending"
                            };

                            mediaList.Add(mediaItem);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Excel dosyası okuma hatası: {ex.Message}", ex);
            }

            return mediaList;
        }

        private int GetColumnIndex(Dictionary<string, int> headers, string[] possibleNames)
        {
            foreach (string name in possibleNames)
            {
                if (headers.ContainsKey(name))
                {
                    return headers[name];
                }
            }
            return -1;
        }

        public async Task ProcessMediaListAsync(
            List<MediaResizerDto> mediaList,
            MediaResizeCriteriaDto criteria,
            BindingList<MediaResizerDto> bindingList,
            Action<int, int, string> progressCallback = null,
            CancellationToken cancellationToken = default)
        {
            int processed = 0;
            int total = mediaList.Count;

            progressCallback?.Invoke(processed, total, "Medya işleme başlatılıyor...");

            foreach (var media in mediaList)
            {
                cancellationToken.ThrowIfCancellationRequested();

                try
                {
                    progressCallback?.Invoke(processed, total, $"İşleniyor: {media.MediaName}");

                    // Medya dosyasını indir
                    media.ProcessingStatus = "Downloading...";
                    var imageData = await _imageResizeService.DownloadImageAsync(media.MediaUrl);

                    // Boyutları al
                    media.ProcessingStatus = "Analyzing...";
                    var dimensions = _imageResizeService.GetImageDimensions(imageData);
                    media.Width = dimensions.width;
                    media.Height = dimensions.height;
                    media.Resolution = $"{dimensions.width}x{dimensions.height}";

                    // Resize gerekip gerekmediğini kontrol et
                    media.NeedsResize = _imageResizeService.ShouldResize(media, criteria);

                    if (media.NeedsResize)
                    {
                        // Resize et
                        media.ProcessingStatus = "Resizing...";
                        var resizedData = await _imageResizeService.ResizeImageAsync(imageData, criteria);

                        // Resize edilmiş boyutları al
                        var resizedDimensions = _imageResizeService.GetImageDimensions(resizedData);
                        media.ResizedWidth = resizedDimensions.width;
                        media.ResizedHeight = resizedDimensions.height;
                        media.ResizedResolution = $"{resizedDimensions.width}x{resizedDimensions.height}";
                        media.ResizedFileSize = resizedData.Length;
                        media.ResizedFileSizeFormatted = _imageResizeService.FormatFileSize(resizedData.Length);

                        // Data URI formatında sakla
                        string fileExtension = GetFileExtensionFromMediaName(media.MediaName);
                        string mimeType = GetMimeTypeFromExtension(fileExtension);
                        string base64String = Convert.ToBase64String(resizedData);
                        media.ResizedMediaData = $"data:{mimeType};base64,{base64String}";

                        media.ProcessingStatus = "Completed - Resized";
                    }
                    else
                    {
                        media.ProcessingStatus = "Completed - No Resize Needed";
                    }

                    media.IsProcessed = true;
                    media.ProcessedDate = DateTime.Now;
                }
                catch (Exception ex)
                {
                    media.ProcessingStatus = "Error";
                    media.ErrorMessage = ex.Message;
                    media.IsProcessed = false;
                }

                // BindingList'e ekle (UI otomatik güncellenecek)
                bindingList.Add(media);

                processed++;
                progressCallback?.Invoke(processed, total, $"İşlenen: {processed}/{total} - {media.MediaName}");

                // Rate limiting
                await Task.Delay(100, cancellationToken);
            }

            progressCallback?.Invoke(total, total, $"İşleme tamamlandı. {mediaList.Count(m => m.NeedsResize)} medya resize edildi.");
        }

        private string ProcessMediaUrl(string mediaUrl, bool createUrlMode, string baseUrl, string token)
        {
            if (!createUrlMode)
            {
                // URL zaten var, doğrudan döndür
                return mediaUrl;
            }

            // URL oluştur
            return CreateFullUrl(mediaUrl, baseUrl, token);
        }

        private string CreateFullUrl(string mediaPath, string baseUrl, string token)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException("Base URL cannot be empty when Create URL mode is selected.");
            }

            // Base URL'nin sonuna '/' ekle (yoksa)
            string processedBaseUrl = baseUrl.TrimEnd('/');
            if (!string.IsNullOrEmpty(processedBaseUrl))
            {
                processedBaseUrl += "/";
            }

            // Media URL'nin başında ve sonundaki '/' karakterlerini kaldır
            string processedMediaPath = mediaPath?.Trim('/', ' ') ?? "";

            // Token işleme
            string processedToken = "";
            if (!string.IsNullOrWhiteSpace(token))
            {
                processedToken = token.Trim();
                if (!processedToken.StartsWith("?"))
                {
                    processedToken = "?" + processedToken;
                }
            }

            // Final URL'yi oluştur
            string finalUrl = processedBaseUrl + processedMediaPath + processedToken;

            return finalUrl;
        }

        public async Task ExportResultsToCsvAsync(List<MediaResizerDto> results, string csvFilePath)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    LineBreakInQuotedFieldIsBadData = false, // Tüm alanları quote'la
                    Delimiter = ",",
                    TrimOptions = TrimOptions.None // Base64 verilerini trim etme
                };

                using (var writer = new StreamWriter(csvFilePath, false, System.Text.Encoding.UTF8))
                using (var csv = new CsvWriter(writer, config))
                {
                    // Header
                    csv.WriteField("MediaName");
                    csv.WriteField("MediaUrl");
                    csv.WriteField("OriginalFileSize");
                    csv.WriteField("OriginalResolution");
                    csv.WriteField("Width");
                    csv.WriteField("Height");
                    csv.WriteField("NeedsResize");
                    csv.WriteField("ProcessingStatus");
                    csv.WriteField("ResizedFileSize");
                    csv.WriteField("ResizedResolution");
                    csv.WriteField("ResizedWidth");
                    csv.WriteField("ResizedHeight");
                    csv.WriteField("MimeType");
                    csv.WriteField("CompleteBase64DataUri");
                    csv.WriteField("Base64Only");
                    csv.WriteField("HasResizedData");
                    csv.WriteField("ErrorMessage");
                    csv.WriteField("ProcessedDate");
                    csv.NextRecord();

                    // Data rows
                    foreach (var result in results)
                    {
                        csv.WriteField(result.MediaName ?? "");
                        csv.WriteField(result.MediaUrl ?? "");
                        csv.WriteField(result.MediaFileSizeFormatted ?? "");
                        csv.WriteField(result.Resolution ?? "");
                        csv.WriteField(result.Width?.ToString() ?? "");
                        csv.WriteField(result.Height?.ToString() ?? "");
                        csv.WriteField(result.NeedsResize ? "YES" : "NO");
                        csv.WriteField(result.ProcessingStatus ?? "");
                        csv.WriteField(result.ResizedFileSizeFormatted ?? "");
                        csv.WriteField(result.ResizedResolution ?? "");
                        csv.WriteField(result.ResizedWidth?.ToString() ?? "");
                        csv.WriteField(result.ResizedHeight?.ToString() ?? "");

                        if (!string.IsNullOrEmpty(result.ResizedMediaData))
                        {
                            string fileExtension = GetFileExtensionFromMediaName(result.MediaName);
                            string mimeType = GetMimeTypeFromExtension(fileExtension);

                            string completeDataUri = result.ResizedMediaData.StartsWith("data:")
                                ? result.ResizedMediaData
                                : $"data:{mimeType};base64,{result.ResizedMediaData}";

                            string base64Only = result.ResizedMediaData.Contains(",")
                                ? result.ResizedMediaData.Substring(result.ResizedMediaData.IndexOf(",") + 1)
                                : result.ResizedMediaData;

                            csv.WriteField(mimeType);
                            csv.WriteField(completeDataUri);
                            csv.WriteField(base64Only);
                            csv.WriteField("YES");
                        }
                        else
                        {
                            csv.WriteField("");
                            csv.WriteField("");
                            csv.WriteField("");
                            csv.WriteField("NO");
                        }

                        csv.WriteField(result.ErrorMessage ?? "");
                        csv.WriteField(result.ProcessedDate?.ToString("yyyy-MM-dd HH:mm:ss") ?? "");
                        csv.NextRecord();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"CSV export error: {ex.Message}", ex);
            }
        }
        public static string GetMediaListTemplate()
        {
            return "MediaName,MediaUrl,MediaFileSize\n";
        }

        public static void SaveMediaListTemplate(string filePath)
        {
            var template = GetMediaListTemplate();
            File.WriteAllText(filePath, template);
        }

        public void Dispose()
        {
            _imageResizeService?.Dispose();
        }

        private string GetFileExtensionFromMediaName(string mediaName)
        {
            if (string.IsNullOrEmpty(mediaName))
                return ".jpg"; // Default extension

            string extension = Path.GetExtension(mediaName).ToLower();

            // Eğer uzantı yoksa, varsayılan olarak .jpg kullan (resized images için)
            if (string.IsNullOrEmpty(extension))
                return ".jpg";

            return extension;
        }

        private string GetMimeTypeFromExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension))
                return "image/jpeg"; // Default MIME type

            extension = extension.ToLower().TrimStart('.');

            var mimeTypes = new Dictionary<string, string>
    {
        { "jpg", "image/jpeg" },
        { "jpeg", "image/jpeg" },
        { "png", "image/png" },
        { "gif", "image/gif" },
        { "bmp", "image/bmp" },
        { "webp", "image/webp" },
        { "tiff", "image/tiff" },
        { "tif", "image/tiff" },
        { "svg", "image/svg+xml" },
        { "ico", "image/x-icon" }
    };

            if (mimeTypes.TryGetValue(extension, out string mimeType))
            {
                return mimeType;
            }

            // Bilinmeyen uzantı için varsayılan
            return "image/jpeg";
        }

        public async Task ProcessMediaDownloadAsync(
            List<MediaResizerDto> mediaList,
            string downloadPath,
            BindingList<MediaResizerDto> bindingList,
            Action<int, int, string> progressCallback = null,
            CancellationToken cancellationToken = default)
        {
            int processed = 0;
            int total = mediaList.Count;

            progressCallback?.Invoke(processed, total, "Starting download process...");

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMinutes(5); // 5 dakika timeout

                foreach (var media in mediaList)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    var downloadResult = new MediaResizerDto
                    {
                        MediaName = media.MediaName,
                        MediaUrl = media.MediaUrl,
                        MediaFileSize = media.MediaFileSize,
                        MediaFileSizeFormatted = media.MediaFileSizeFormatted,
                        ProcessingStatus = "Downloading...",
                        ProcessedDate = DateTime.Now
                    };

                    try
                    {
                        progressCallback?.Invoke(processed, total, $"Downloading: {media.MediaName}");

                        // Dosyayı indir
                        var response = await httpClient.GetAsync(media.MediaUrl, cancellationToken);
                        response.EnsureSuccessStatusCode();

                        var fileBytes = await response.Content.ReadAsByteArrayAsync();

                        // Gerçek dosya boyutunu güncelle
                        downloadResult.MediaFileSize = fileBytes.Length;
                        downloadResult.MediaFileSizeFormatted = _imageResizeService.FormatFileSize(fileBytes.Length);

                        // Dosya adını temizle (invalid karakterleri kaldır)
                        string safeFileName = GetSafeFileName(media.MediaName);
                        string filePath = Path.Combine(downloadPath, safeFileName);

                        // Eğer aynı isimde dosya varsa, numara ekle
                        filePath = GetUniqueFilePath(filePath);

                        // Dosyayı kaydet
                        File.WriteAllBytes(filePath, fileBytes);

                        downloadResult.ProcessingStatus = $"Downloaded - {Path.GetFileName(filePath)}";
                        downloadResult.IsProcessed = true;

                        // İsteğe bağlı: Boyutları al (sadece resimler için)
                        if (IsImageFile(safeFileName))
                        {
                            try
                            {
                                var dimensions = _imageResizeService.GetImageDimensions(fileBytes);
                                downloadResult.Width = dimensions.width;
                                downloadResult.Height = dimensions.height;
                                downloadResult.Resolution = $"{dimensions.width}x{dimensions.height}";
                            }
                            catch
                            {
                                // Boyut alınamazsa devam et
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        downloadResult.ProcessingStatus = "Download Failed";
                        downloadResult.ErrorMessage = ex.Message;
                        downloadResult.IsProcessed = false;
                    }

                    // BindingList'e ekle (UI otomatik güncellenecek)
                    bindingList.Add(downloadResult);

                    processed++;
                    progressCallback?.Invoke(processed, total, $"Processed: {processed}/{total} - {media.MediaName}");

                    // Rate limiting - sunucuyu yormamak için
                    await Task.Delay(200, cancellationToken);
                }
            }

            progressCallback?.Invoke(total, total, $"Download completed! {bindingList.Count(r => r.IsProcessed)} files downloaded.");
        }

        // Yardımcı metodlar
        private string GetSafeFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return "unnamed_file";

            // Invalid karakterleri temizle
            char[] invalidChars = Path.GetInvalidFileNameChars();
            string safeName = fileName;

            foreach (char c in invalidChars)
            {
                safeName = safeName.Replace(c, '_');
            }

            // Çok uzun dosya adlarını kısalt
            if (safeName.Length > 200)
            {
                string extension = Path.GetExtension(safeName);
                string nameWithoutExt = Path.GetFileNameWithoutExtension(safeName);
                safeName = nameWithoutExt.Substring(0, 200 - extension.Length) + extension;
            }

            return safeName;
        }

        private string GetUniqueFilePath(string filePath)
        {
            if (!File.Exists(filePath))
                return filePath;

            string directory = Path.GetDirectoryName(filePath);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            string extension = Path.GetExtension(filePath);

            int counter = 1;
            string newFilePath;

            do
            {
                string newFileName = $"{fileNameWithoutExt}_{counter}{extension}";
                newFilePath = Path.Combine(directory, newFileName);
                counter++;
            }
            while (File.Exists(newFilePath));

            return newFilePath;
        }

        private bool IsImageFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            string extension = Path.GetExtension(fileName).ToLower();
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".tiff", ".tif" };

            return imageExtensions.Contains(extension);
        }
    }
}