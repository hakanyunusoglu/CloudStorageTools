using CloudStorageTools.VideoSizeFinder.Dtos;
using CsvHelper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
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

                        // Base64'e çevir
                        media.ResizedMediaData = Convert.ToBase64String(resizedData);
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

        public async Task ExportResultsToExcelAsync(List<MediaResizerDto> results, string filePath)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Media Resize Results");

                // Başlık satırı
                var headers = new[]
                {
                    "Media Name", "Media URL", "Original File Size", "Original Resolution",
                    "Width", "Height", "Needs Resize", "Processing Status",
                    "Resized File Size", "Resized Resolution", "Resized Width", "Resized Height",
                    "Resized Media Data (Base64)", "Error Message", "Processed Date"
                };

                // Başlıkları yaz
                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cells[1, col].Value = headers[col - 1];
                }

                // Başlık stilini ayarla
                using (var headerRange = worksheet.Cells[1, 1, 1, headers.Length])
                {
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                    headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Veri satırları
                for (int i = 0; i < results.Count; i++)
                {
                    int row = i + 2;
                    var result = results[i];

                    worksheet.Cells[row, 1].Value = result.MediaName;
                    worksheet.Cells[row, 2].Value = result.MediaUrl;
                    worksheet.Cells[row, 3].Value = result.MediaFileSizeFormatted;
                    worksheet.Cells[row, 4].Value = result.Resolution;
                    worksheet.Cells[row, 5].Value = result.Width;
                    worksheet.Cells[row, 6].Value = result.Height;
                    worksheet.Cells[row, 7].Value = result.NeedsResize ? "YES" : "NO";
                    worksheet.Cells[row, 8].Value = result.ProcessingStatus;
                    worksheet.Cells[row, 9].Value = result.ResizedFileSizeFormatted;
                    worksheet.Cells[row, 10].Value = result.ResizedResolution;
                    worksheet.Cells[row, 11].Value = result.ResizedWidth;
                    worksheet.Cells[row, 12].Value = result.ResizedHeight;
                    worksheet.Cells[row, 13].Value = result.ResizedMediaData;
                    worksheet.Cells[row, 14].Value = result.ErrorMessage;
                    worksheet.Cells[row, 15].Value = result.ProcessedDate?.ToString("dd/MM/yyyy HH:mm:ss");

                    // Satır renklandirme
                    Color rowColor = Color.White;
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        rowColor = Color.FromArgb(255, 220, 220); // Hata - kırmızı
                    }
                    else if (result.NeedsResize && result.IsProcessed)
                    {
                        rowColor = Color.FromArgb(220, 255, 220); // Resize edildi - yeşil
                    }
                    else if (!result.NeedsResize && result.IsProcessed)
                    {
                        rowColor = Color.FromArgb(255, 255, 220); // Resize gerekmedi - sarı
                    }

                    using (var rowRange = worksheet.Cells[row, 1, row, headers.Length])
                    {
                        rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rowRange.Style.Fill.BackgroundColor.SetColor(rowColor);
                        rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    }
                }

                // Sütun genişlikleri
                worksheet.Column(1).Width = 30; // Media Name
                worksheet.Column(2).Width = 50; // Media URL
                worksheet.Column(3).Width = 15; // Original File Size
                worksheet.Column(4).Width = 15; // Original Resolution
                worksheet.Column(8).Width = 20; // Processing Status
                worksheet.Column(13).Width = 20; // Base64 (kısıtlı genişlik)
                worksheet.Column(14).Width = 30; // Error Message

                // Filtreler
                worksheet.Cells[1, 1, results.Count + 1, headers.Length].AutoFilter = true;

                // Özet istatistikler
                int summaryStartRow = results.Count + 4;
                worksheet.Cells[summaryStartRow, 1].Value = "ÖZET İSTATİSTİKLER";
                worksheet.Cells[summaryStartRow, 1].Style.Font.Bold = true;

                worksheet.Cells[summaryStartRow + 2, 1].Value = "Toplam Medya:";
                worksheet.Cells[summaryStartRow + 2, 2].Value = results.Count;

                worksheet.Cells[summaryStartRow + 3, 1].Value = "Resize Edilenler:";
                worksheet.Cells[summaryStartRow + 3, 2].Value = results.Count(r => r.NeedsResize && r.IsProcessed);

                worksheet.Cells[summaryStartRow + 4, 1].Value = "Hatalı İşlemler:";
                worksheet.Cells[summaryStartRow + 4, 2].Value = results.Count(r => !string.IsNullOrEmpty(r.ErrorMessage));

                var fileInfo = new FileInfo(filePath);
                await package.SaveAsAsync(fileInfo);
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
    }
}