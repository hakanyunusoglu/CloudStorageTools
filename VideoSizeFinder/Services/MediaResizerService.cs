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

        public async Task ExportResultsToExcelAsync(List<MediaResizerDto> results, string filePath)
        {
            try
            {
                // Dosya yolunu doğrula ve düzelt
                var directoryPath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Eğer dosya mevcutsa, önce sil
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    {
                        filePath = Path.Combine(Path.GetDirectoryName(filePath),
                            $"{Path.GetFileNameWithoutExtension(filePath)}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx");
                    }
                }

                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Media Resize Results");

                    // Başlık satırı - Base64 için multiple columns
                    var headers = new[]
                    {
                "Media Name", "Media URL", "Original File Size", "Original Resolution",
                "Width", "Height", "Needs Resize", "Processing Status",
                "Resized File Size", "Resized Resolution", "Resized Width", "Resized Height",
                "MIME Type", "Base64_Part1", "Base64_Part2", "Base64_Part3", "Base64_Part4", "Base64_Part5",
                "Total_Parts", "Full_DataURI_Available", "Error Message", "Processed Date"
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

                    // Base64 sütunları için özel renk
                    for (int col = 14; col <= 18; col++) // Base64_Part1 to Part5
                    {
                        worksheet.Cells[1, col].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                    }

                    const int maxCharsPerCell = 32000; // Excel güvenli limit

                    // Veri satırları
                    for (int i = 0; i < results.Count; i++)
                    {
                        int row = i + 2;
                        var result = results[i];

                        // Temel bilgiler
                        worksheet.Cells[row, 1].Value = result.MediaName ?? "";
                        worksheet.Cells[row, 2].Value = result.MediaUrl ?? "";
                        worksheet.Cells[row, 3].Value = result.MediaFileSizeFormatted ?? "";
                        worksheet.Cells[row, 4].Value = result.Resolution ?? "";
                        worksheet.Cells[row, 5].Value = result.Width;
                        worksheet.Cells[row, 6].Value = result.Height;
                        worksheet.Cells[row, 7].Value = result.NeedsResize ? "YES" : "NO";
                        worksheet.Cells[row, 8].Value = result.ProcessingStatus ?? "";
                        worksheet.Cells[row, 9].Value = result.ResizedFileSizeFormatted ?? "";
                        worksheet.Cells[row, 10].Value = result.ResizedResolution ?? "";
                        worksheet.Cells[row, 11].Value = result.ResizedWidth;
                        worksheet.Cells[row, 12].Value = result.ResizedHeight;

                        // Base64 data işleme
                        if (!string.IsNullOrEmpty(result.ResizedMediaData))
                        {
                            string fileExtension = GetFileExtensionFromMediaName(result.MediaName);
                            string mimeType = GetMimeTypeFromExtension(fileExtension);
                            worksheet.Cells[row, 13].Value = mimeType;

                            // Complete Data URI oluştur
                            string completeDataUri = result.ResizedMediaData;
                            if (!completeDataUri.StartsWith("data:"))
                            {
                                completeDataUri = $"data:{mimeType};base64,{result.ResizedMediaData}";
                            }

                            // Base64 data'yı parçalara böl (5 sütuna)
                            var parts = SplitBase64IntoParts(completeDataUri, maxCharsPerCell, 5);

                            // Her parçayı ilgili sütuna yaz
                            for (int partIndex = 0; partIndex < parts.Count && partIndex < 5; partIndex++)
                            {
                                worksheet.Cells[row, 14 + partIndex].Value = parts[partIndex];
                            }

                            worksheet.Cells[row, 19].Value = parts.Count; // Total Parts
                            worksheet.Cells[row, 20].Value = "YES"; // Full DataURI Available
                        }
                        else
                        {
                            worksheet.Cells[row, 13].Value = "";
                            worksheet.Cells[row, 19].Value = 0;
                            worksheet.Cells[row, 20].Value = "NO";
                        }

                        worksheet.Cells[row, 21].Value = result.ErrorMessage ?? "";
                        worksheet.Cells[row, 22].Value = result.ProcessedDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";

                        // Satır renklandirme
                        Color rowColor = GetRowColor(result);
                        using (var rowRange = worksheet.Cells[row, 1, row, headers.Length])
                        {
                            rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            rowRange.Style.Fill.BackgroundColor.SetColor(rowColor);
                            rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    // Sütun genişlikleri
                    SetColumnWidthsForAzure(worksheet, headers.Length);

                    // Filtreler
                    if (results.Count > 0)
                    {
                        worksheet.Cells[1, 1, results.Count + 1, headers.Length].AutoFilter = true;
                    }

                    // Instructions sheet ekle
                    CreateInstructionsSheet(package);

                    // Özet istatistikler
                    AddSummaryStatistics(worksheet, results);

                    // Base64 birleştirme helper sheet'i
                    CreateBase64ReconstructionSheet(package, results);

                    // Dosyayı kaydet
                    var fileBytes = package.GetAsByteArray();
                    File.WriteAllBytes(filePath, fileBytes);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Excel export hatası: {ex.Message}", ex);
            }
        }

        private List<string> SplitBase64IntoParts(string dataUri, int maxLength, int maxParts)
        {
            var parts = new List<string>();

            if (string.IsNullOrEmpty(dataUri))
                return parts;

            int index = 0;
            int partCount = 0;

            while (index < dataUri.Length && partCount < maxParts)
            {
                int length = Math.Min(maxLength, dataUri.Length - index);
                parts.Add(dataUri.Substring(index, length));
                index += length;
                partCount++;
            }

            return parts;
        }

        private void CreateBase64ReconstructionSheet(ExcelPackage package, List<MediaResizerDto> results)
        {
            var helperSheet = package.Workbook.Worksheets.Add("Base64 Reconstruction Helper");

            // Başlık
            helperSheet.Cells[1, 1].Value = "Media Name";
            helperSheet.Cells[1, 2].Value = "Complete Data URI (Reconstructed)";
            helperSheet.Cells[1, 3].Value = "Ready for Azure Upload";

            using (var headerRange = helperSheet.Cells[1, 1, 1, 3])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightYellow);
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }

            var resizedResults = results.Where(r => !string.IsNullOrEmpty(r.ResizedMediaData)).ToList();

            for (int i = 0; i < resizedResults.Count; i++)
            {
                int row = i + 2;
                var result = resizedResults[i];

                helperSheet.Cells[row, 1].Value = result.MediaName;

                // Excel formülü ile parçaları birleştir
                string formula = $"=CONCATENATE('Media Resize Results'.N{row + 1},'Media Resize Results'.O{row + 1},'Media Resize Results'.P{row + 1},'Media Resize Results'.Q{row + 1},'Media Resize Results'.R{row + 1})";
                helperSheet.Cells[row, 2].Formula = formula;

                helperSheet.Cells[row, 3].Value = "YES";
            }

            // Sütun genişlikleri
            helperSheet.Column(1).Width = 30;
            helperSheet.Column(2).Width = 100;
            helperSheet.Column(3).Width = 20;

            // Not ekle
            helperSheet.Cells[resizedResults.Count + 4, 1].Value = "NOT: Column B contains the complete Data URI for Azure upload";
            helperSheet.Cells[resizedResults.Count + 4, 1].Style.Font.Bold = true;
            helperSheet.Cells[resizedResults.Count + 4, 1].Style.Font.Color.SetColor(Color.Red);
        }

        private void CreateInstructionsSheet(ExcelPackage package)
        {
            var instructionsSheet = package.Workbook.Worksheets.Add("Azure Upload Instructions");

            instructionsSheet.Cells[1, 1].Value = "AZURE UPLOAD INSTRUCTIONS";
            instructionsSheet.Cells[1, 1].Style.Font.Bold = true;
            instructionsSheet.Cells[1, 1].Style.Font.Size = 16;
            instructionsSheet.Cells[1, 1].Style.Font.Color.SetColor(Color.DarkBlue);

            var instructions = new string[]
            {
        "",
        "1. BASE64 DATA RECONSTRUCTION:",
        "   - Main sheet contains Base64 data split across columns Base64_Part1 to Base64_Part5",
        "   - Use 'Base64 Reconstruction Helper' sheet for complete Data URIs",
        "   - Column B in helper sheet contains ready-to-use Data URIs",
        "",
        "2. AZURE UPLOAD PROCESS:",
        "   - Copy complete Data URI from helper sheet",
        "   - Decode Base64 portion (after 'base64,') to get byte array",
        "   - Upload byte array to Azure Blob Storage",
        "",
        "3. DATA URI FORMAT:",
        "   - Format: data:image/jpeg;base64,/9j/4AAQSkZJRgABAQ...",
        "   - MIME Type: Specified in 'MIME Type' column",
        "   - Base64 Data: Everything after 'base64,'",
        "",
        "4. C# EXAMPLE CODE:",
        "   string dataUri = // from helper sheet column B",
        "   string base64Data = dataUri.Substring(dataUri.IndexOf(',') + 1);",
        "   byte[] imageBytes = Convert.FromBase64String(base64Data);",
        "   // Upload imageBytes to Azure",
        "",
        "5. VERIFICATION:",
        "   - 'Full_DataURI_Available' column shows YES/NO status",
        "   - Only rows with YES can be uploaded to Azure",
        "   - Check 'Total_Parts' column for data completeness"
            };

            for (int i = 0; i < instructions.Length; i++)
            {
                instructionsSheet.Cells[i + 3, 1].Value = instructions[i];
                if (instructions[i].EndsWith(":"))
                {
                    instructionsSheet.Cells[i + 3, 1].Style.Font.Bold = true;
                }
            }

            instructionsSheet.Column(1).Width = 80;
            instructionsSheet.Cells.Style.WrapText = true;
        }

        private void SetColumnWidthsForAzure(ExcelWorksheet worksheet, int headerCount)
        {
            try
            {
                // Temel sütunlar
                worksheet.Column(1).Width = 25; // Media Name
                worksheet.Column(2).Width = 40; // Media URL
                worksheet.Column(13).Width = 15; // MIME Type

                // Base64 sütunları (geniş)
                for (int col = 14; col <= 18; col++)
                {
                    worksheet.Column(col).Width = 50;
                }

                worksheet.Column(19).Width = 12; // Total Parts
                worksheet.Column(20).Width = 18; // Full DataURI Available
                worksheet.Column(21).Width = 30; // Error Message
                worksheet.Column(22).Width = 18; // Processed Date
            }
            catch
            {
                // Hata durumunda standart genişlik
                for (int col = 1; col <= headerCount; col++)
                {
                    worksheet.Column(col).Width = 15;
                }
            }
        }

        private Color GetRowColor(MediaResizerDto result)
        {
            if (!string.IsNullOrEmpty(result.ErrorMessage))
                return Color.FromArgb(255, 220, 220); // Hata - kırmızı
            else if (result.NeedsResize && result.IsProcessed)
                return Color.FromArgb(220, 255, 220); // Resize edildi - yeşil
            else if (!result.NeedsResize && result.IsProcessed)
                return Color.FromArgb(255, 255, 220); // Resize gerekmedi - sarı
            else
                return Color.White;
        }

        private void AddSummaryStatistics(ExcelWorksheet worksheet, List<MediaResizerDto> results)
        {
            int summaryStartRow = results.Count + 4;
            worksheet.Cells[summaryStartRow, 1].Value = "ÖZET İSTATİSTİKLER";
            worksheet.Cells[summaryStartRow, 1].Style.Font.Bold = true;

            worksheet.Cells[summaryStartRow + 2, 1].Value = "Toplam Medya:";
            worksheet.Cells[summaryStartRow + 2, 2].Value = results.Count;

            worksheet.Cells[summaryStartRow + 3, 1].Value = "Resize Edilenler:";
            worksheet.Cells[summaryStartRow + 3, 2].Value = results.Count(r => r.NeedsResize && r.IsProcessed);

            worksheet.Cells[summaryStartRow + 4, 1].Value = "Azure'a Yüklenebilir:";
            worksheet.Cells[summaryStartRow + 4, 2].Value = results.Count(r => !string.IsNullOrEmpty(r.ResizedMediaData));

            worksheet.Cells[summaryStartRow + 5, 1].Value = "Hatalı İşlemler:";
            worksheet.Cells[summaryStartRow + 5, 2].Value = results.Count(r => !string.IsNullOrEmpty(r.ErrorMessage));

            worksheet.Cells[summaryStartRow + 6, 1].Value = "Rapor Tarihi:";
            worksheet.Cells[summaryStartRow + 6, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
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
    }
}