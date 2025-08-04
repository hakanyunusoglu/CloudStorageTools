using CloudStorageTools.CloudMediaValidator.Dtos;
using CloudStorageTools.VideoSizeFinder.Dtos;
using CloudStorageTools.VideoSizeFinder.Interfaces;
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
using System.Threading;
using System.Threading.Tasks;

public class CloudMediaValidatorService
{
    private readonly ICloudMediaService _cloudMediaService;

    public CloudMediaValidatorService(ICloudMediaService cloudMediaService)
    {
        _cloudMediaService = cloudMediaService ?? throw new ArgumentNullException(nameof(cloudMediaService));
    }

    /// <summary>
    /// CSV dosyasından medya isimlerini okur
    /// </summary>
    public List<string> LoadMediaNamesFromCsv(string csvFilePath)
    {
        var mediaNames = new List<string>();

        try
        {
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                var headers = csv.Context.Reader.HeaderRecord;

                // MediaName header'ını bul (case insensitive)
                string mediaNameHeader = headers?.FirstOrDefault(h =>
                    string.Equals(h, "MediaName", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(h, "Media Name", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(h, "FileName", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(h, "File Name", StringComparison.OrdinalIgnoreCase));

                if (string.IsNullOrEmpty(mediaNameHeader))
                {
                    throw new Exception("CSV dosyasında 'MediaName' sütunu bulunamadı. Lütfen CSV dosyasının 'MediaName' sütunu içerdiğinden emin olun.");
                }

                while (csv.Read())
                {
                    string mediaName = csv.GetField(mediaNameHeader)?.Trim();

                    if (!string.IsNullOrWhiteSpace(mediaName))
                    {
                        mediaNames.Add(mediaName);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"CSV dosyası okuma hatası: {ex.Message}", ex);
        }

        return mediaNames;
    }

    /// <summary>
    /// Medya isimlerini cloud'da doğrular - Real-time güncelleme ile
    /// </summary>
    public async Task<List<MediaValidationDto>> ValidateMediaExistenceAsync(
        List<string> mediaNames,
        Action<int, int, string> progressCallback = null,
        CancellationToken cancellationToken = default)
    {
        var results = new List<MediaValidationDto>();
        int processed = 0;
        int total = mediaNames.Count;

        progressCallback?.Invoke(processed, total, "Doğrulama başlatılıyor...");

        foreach (string mediaName in mediaNames)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = new MediaValidationDto
            {
                MediaName = mediaName,
                IsExist = false,
                ErrorMessage = null
            };

            try
            {
                progressCallback?.Invoke(processed, total, $"Kontrol ediliyor: {mediaName}");

                // Cloud'da medya varlığını kontrol et
                bool exists = await _cloudMediaService.MediaExistsAsync(mediaName);

                if (exists)
                {
                    validationResult.IsExist = true;
                    validationResult.FoundWithExtension = mediaName;

                    // Detayları al
                    try
                    {
                        var details = await _cloudMediaService.GetMediaDetailsAsync(mediaName);
                        validationResult.FileSize = details.Size;
                        validationResult.FileSizeFormatted = details.SizeFormatted;
                        validationResult.ContentType = details.ContentType;
                        validationResult.LastModified = details.LastModified;
                    }
                    catch { /* Detay alınamazsa sadece varlığı işaretle */ }
                }
                else
                {
                    // Eğer direkt medya bulunamazsa, farklı uzantılarla da dene
                    var (found, foundExtension, details) = await TryWithDifferentExtensions(mediaName);
                    validationResult.IsExist = found;

                    if (found)
                    {
                        validationResult.FoundWithExtension = foundExtension;
                        if (details != null)
                        {
                            validationResult.FileSize = details.Size;
                            validationResult.FileSizeFormatted = details.SizeFormatted;
                            validationResult.ContentType = details.ContentType;
                            validationResult.LastModified = details.LastModified;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationResult.IsExist = false;
                validationResult.ErrorMessage = ex.Message;
            }

            // Sonucu listeye ekle
            results.Add(validationResult);
            processed++;

            // Her kayıt sonrası callback çağır (real-time güncelleme için)
            string status = validationResult.IsExist ? "BULUNDU" :
                           !string.IsNullOrEmpty(validationResult.ErrorMessage) ? "HATA" : "BULUNAMADI";

            progressCallback?.Invoke(processed, total,
                $"İşlenen: {processed}/{total} - {mediaName} ({status})");

            // Kısa bir bekleme ekle (rate limiting için)
            await Task.Delay(50, cancellationToken);
        }

        progressCallback?.Invoke(total, total, $"Doğrulama tamamlandı. {results.Count(r => r.IsExist)} medya bulundu.");

        return results;
    }

    /// <summary>
    /// Real-time progress ile BindingList güncellemesi için özel metod
    /// </summary>
    public async Task<List<MediaValidationDto>> ValidateMediaExistenceWithBindingAsync(
        List<string> mediaNames,
        BindingList<MediaValidationDto> bindingList,
        Action<int, int, string> progressCallback = null,
        CancellationToken cancellationToken = default)
    {
        var results = new List<MediaValidationDto>();
        int processed = 0;
        int total = mediaNames.Count;

        progressCallback?.Invoke(processed, total, "Doğrulama başlatılıyor...");

        foreach (string mediaName in mediaNames)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = new MediaValidationDto
            {
                MediaName = mediaName,
                IsExist = false,
                ErrorMessage = null
            };

            try
            {
                progressCallback?.Invoke(processed, total, $"Kontrol ediliyor: {mediaName}");

                // Cloud'da medya varlığını kontrol et
                bool exists = await _cloudMediaService.MediaExistsAsync(mediaName);

                if (exists)
                {
                    validationResult.IsExist = true;
                    validationResult.FoundWithExtension = mediaName;

                    // Detayları al
                    try
                    {
                        var details = await _cloudMediaService.GetMediaDetailsAsync(mediaName);
                        validationResult.FileSize = details.Size;
                        validationResult.FileSizeFormatted = details.SizeFormatted;
                        validationResult.ContentType = details.ContentType;
                        validationResult.LastModified = details.LastModified;
                    }
                    catch { /* Detay alınamazsa sadece varlığı işaretle */ }
                }
                else
                {
                    // Eğer direkt medya bulunamazsa, farklı uzantılarla da dene
                    var (found, foundExtension, details) = await TryWithDifferentExtensions(mediaName);
                    validationResult.IsExist = found;

                    if (found)
                    {
                        validationResult.FoundWithExtension = foundExtension;
                        if (details != null)
                        {
                            validationResult.FileSize = details.Size;
                            validationResult.FileSizeFormatted = details.SizeFormatted;
                            validationResult.ContentType = details.ContentType;
                            validationResult.LastModified = details.LastModified;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationResult.IsExist = false;
                validationResult.ErrorMessage = ex.Message;
            }

            // Real-time güncelleme: BindingList'e ekle
            results.Add(validationResult);

            // UI thread'de BindingList'e ekleme yapılmalı
            if (System.Windows.Forms.Application.OpenForms.Count > 0)
            {
                var form = System.Windows.Forms.Application.OpenForms[0];
                if (form.InvokeRequired)
                {
                    form.Invoke(new Action(() => bindingList.Add(validationResult)));
                }
                else
                {
                    bindingList.Add(validationResult);
                }
            }
            else
            {
                bindingList.Add(validationResult);
            }

            processed++;

            // Her kayıt sonrası callback çağır
            string status = validationResult.IsExist ? "BULUNDU" :
                           !string.IsNullOrEmpty(validationResult.ErrorMessage) ? "HATA" : "BULUNAMADI";

            progressCallback?.Invoke(processed, total,
                $"İşlenen: {processed}/{total} - {mediaName} ({status})");

            // Kısa bir bekleme ekle (rate limiting için)
            await Task.Delay(50, cancellationToken);
        }

        progressCallback?.Invoke(total, total, $"Doğrulama tamamlandı. {results.Count(r => r.IsExist)} medya bulundu.");

        return results;
    }

    /// <summary>
    /// Farklı uzantılarla medya arama
    /// </summary>
    private async Task<(bool found, string foundExtension, CloudMediaItemDto details)> TryWithDifferentExtensions(string baseName)
    {
        // Eğer zaten uzantı varsa, uzantısız hali ile de dene
        string nameWithoutExtension = Path.GetFileNameWithoutExtension(baseName);
        string currentExtension = Path.GetExtension(baseName);

        var extensionsToTry = new List<string>();

        // Eğer uzantı yoksa, yaygın uzantılarla dene
        if (string.IsNullOrEmpty(currentExtension))
        {
            extensionsToTry.AddRange(new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm", ".mkv",
                                                ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp",
                                                ".mp3", ".wav", ".flac", ".aac", ".ogg", ".m3u8" });
        }
        else
        {
            // Uzantı varsa, uzantısız hali ile dene
            extensionsToTry.Add("");
        }

        foreach (string extension in extensionsToTry)
        {
            try
            {
                string testName = string.IsNullOrEmpty(extension) ? nameWithoutExtension : nameWithoutExtension + extension;
                bool exists = await _cloudMediaService.MediaExistsAsync(testName);

                if (exists)
                {
                    // Detayları al
                    try
                    {
                        var details = await _cloudMediaService.GetMediaDetailsAsync(testName);
                        return (true, testName, details);
                    }
                    catch
                    {
                        return (true, testName, null);
                    }
                }
            }
            catch
            {
                // Hata durumunda devam et
                continue;
            }
        }

        return (false, null, null);
    }

    /// <summary>
    /// Doğrulama sonuçlarını Excel'e export eder
    /// </summary>
    public async Task ExportValidationResultsToExcelAsync(List<MediaValidationDto> validationResults, string filePath)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Media Validation Results");

            // Başlık satırı
            worksheet.Cells[1, 1].Value = "Media Name";
            worksheet.Cells[1, 2].Value = "Is Exist";
            worksheet.Cells[1, 3].Value = "Status";
            worksheet.Cells[1, 4].Value = "Found As";
            worksheet.Cells[1, 5].Value = "File Size";
            worksheet.Cells[1, 6].Value = "Content Type";
            worksheet.Cells[1, 7].Value = "Last Modified";
            worksheet.Cells[1, 8].Value = "Error Message";

            // Başlık stilini ayarla
            using (var headerRange = worksheet.Cells[1, 1, 1, 8])
            {
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
                headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Veri satırları
            for (int i = 0; i < validationResults.Count; i++)
            {
                int row = i + 2;
                var result = validationResults[i];

                worksheet.Cells[row, 1].Value = result.MediaName;
                worksheet.Cells[row, 2].Value = result.IsExist ? "YES" : "NO";
                worksheet.Cells[row, 3].Value = result.IsExist ? "FOUND" : "NOT FOUND";
                worksheet.Cells[row, 4].Value = result.FoundWithExtension ?? "";
                worksheet.Cells[row, 5].Value = result.FileSizeFormatted ?? "";
                worksheet.Cells[row, 6].Value = result.ContentType ?? "";
                worksheet.Cells[row, 7].Value = result.LastModified?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";
                worksheet.Cells[row, 8].Value = result.ErrorMessage ?? "";

                // Satır renklandirme
                Color rowColor;
                if (result.IsExist)
                {
                    rowColor = Color.FromArgb(220, 255, 220); // Açık yeşil
                }
                else if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    rowColor = Color.FromArgb(255, 220, 220); // Açık kırmızı
                }
                else
                {
                    rowColor = Color.FromArgb(255, 255, 220); // Açık sarı
                }

                using (var rowRange = worksheet.Cells[row, 1, row, 8])
                {
                    rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rowRange.Style.Fill.BackgroundColor.SetColor(rowColor);
                    rowRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);
                }
            }

            // Özet istatistikler
            int totalCount = validationResults.Count;
            int foundCount = validationResults.Count(r => r.IsExist);
            int notFoundCount = totalCount - foundCount;
            int errorCount = validationResults.Count(r => !string.IsNullOrEmpty(r.ErrorMessage));

            int summaryStartRow = totalCount + 4;

            worksheet.Cells[summaryStartRow, 1].Value = "ÖZET İSTATİSTİKLER";
            worksheet.Cells[summaryStartRow, 1].Style.Font.Bold = true;
            worksheet.Cells[summaryStartRow, 1].Style.Font.Size = 14;

            worksheet.Cells[summaryStartRow + 2, 1].Value = "Toplam Medya:";
            worksheet.Cells[summaryStartRow + 2, 2].Value = totalCount;

            worksheet.Cells[summaryStartRow + 3, 1].Value = "Bulunan:";
            worksheet.Cells[summaryStartRow + 3, 2].Value = foundCount;
            worksheet.Cells[summaryStartRow + 3, 3].Value = $"({(double)foundCount / totalCount * 100:F1}%)";

            worksheet.Cells[summaryStartRow + 4, 1].Value = "Bulunamayan:";
            worksheet.Cells[summaryStartRow + 4, 2].Value = notFoundCount;
            worksheet.Cells[summaryStartRow + 4, 3].Value = $"({(double)notFoundCount / totalCount * 100:F1}%)";

            worksheet.Cells[summaryStartRow + 5, 1].Value = "Hatalı:";
            worksheet.Cells[summaryStartRow + 5, 2].Value = errorCount;

            worksheet.Cells[summaryStartRow + 7, 1].Value = "Rapor Tarihi:";
            worksheet.Cells[summaryStartRow + 7, 2].Value = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            worksheet.Cells[summaryStartRow + 8, 1].Value = "Cloud Provider:";
            worksheet.Cells[summaryStartRow + 8, 2].Value = _cloudMediaService.GetCloudProviderName();

            // Sütun genişliklerini ayarla
            worksheet.Column(1).Width = 40; // Media Name
            worksheet.Column(2).Width = 12; // Is Exist
            worksheet.Column(3).Width = 15; // Status
            worksheet.Column(4).Width = 30; // Found As
            worksheet.Column(5).Width = 15; // File Size
            worksheet.Column(6).Width = 20; // Content Type
            worksheet.Column(7).Width = 20; // Last Modified
            worksheet.Column(8).Width = 30; // Error Message

            // Filtreler ekle
            worksheet.Cells[1, 1, totalCount + 1, 8].AutoFilter = true;

            // Dondurulan paneller
            worksheet.View.FreezePanes(2, 1);

            var fileInfo = new FileInfo(filePath);
            await package.SaveAsAsync(fileInfo);
        }
    }

    public async Task ValidateMediaExistenceWithRealtimeBinding(
    List<string> mediaNames,
    BindingList<MediaValidationDto> bindingList,
    Action<int, int, string> progressCallback = null,
    CancellationToken cancellationToken = default)
    {
        int processed = 0;
        int total = mediaNames.Count;

        progressCallback?.Invoke(processed, total, "Doğrulama başlatılıyor...");

        foreach (string mediaName in mediaNames)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var validationResult = new MediaValidationDto
            {
                MediaName = mediaName,
                IsExist = false,
                ErrorMessage = null
            };

            try
            {
                progressCallback?.Invoke(processed, total, $"Kontrol ediliyor: {mediaName}");

                // Cloud'da medya varlığını kontrol et
                bool exists = await _cloudMediaService.MediaExistsAsync(mediaName);

                if (exists)
                {
                    validationResult.IsExist = true;
                    validationResult.FoundWithExtension = mediaName;

                    // Detayları al
                    try
                    {
                        var details = await _cloudMediaService.GetMediaDetailsAsync(mediaName);
                        validationResult.FileSize = details.Size;
                        validationResult.FileSizeFormatted = details.SizeFormatted;
                        validationResult.ContentType = details.ContentType;
                        validationResult.LastModified = details.LastModified;
                    }
                    catch { /* Detay alınamazsa sadece varlığı işaretle */ }
                }
                else
                {
                    // Eğer direkt medya bulunamazsa, farklı uzantılarla da dene
                    var (found, foundExtension, details) = await TryWithDifferentExtensions(mediaName);
                    validationResult.IsExist = found;

                    if (found)
                    {
                        validationResult.FoundWithExtension = foundExtension;
                        if (details != null)
                        {
                            validationResult.FileSize = details.Size;
                            validationResult.FileSizeFormatted = details.SizeFormatted;
                            validationResult.ContentType = details.ContentType;
                            validationResult.LastModified = details.LastModified;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                validationResult.IsExist = false;
                validationResult.ErrorMessage = ex.Message;
            }

            // Real-time güncelleme: BindingList'e ekle (UI otomatik güncellenecek)
            bindingList.Add(validationResult);

            processed++;

            // Her kayıt sonrası callback çağır
            string status = validationResult.IsExist ? "BULUNDU" :
                           !string.IsNullOrEmpty(validationResult.ErrorMessage) ? "HATA" : "BULUNAMADI";

            progressCallback?.Invoke(processed, total,
                $"İşlenen: {processed}/{total} - {mediaName} ({status})");

            // Kısa bir bekleme ekle (rate limiting için)
            await Task.Delay(50, cancellationToken);
        }

        progressCallback?.Invoke(total, total, $"Doğrulama tamamlandı. {bindingList.Count(r => r.IsExist)} medya bulundu.");
    }

    /// <summary>
    /// Medya doğrulama CSV şablonunu oluşturur
    /// </summary>
    public static string GetValidationCsvTemplate()
    {
        return "MediaName\n";
    }

    /// <summary>
    /// CSV şablonunu dosyaya kaydeder
    /// </summary>
    public static void SaveValidationCsvTemplate(string filePath)
    {
        var template = GetValidationCsvTemplate();
        File.WriteAllText(filePath, template);
    }
}