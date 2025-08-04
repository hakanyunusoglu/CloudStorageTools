
using CloudStorageTools.VideoSizeFinder.Interfaces;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public ExcelExportService()
        {
        }

        public async Task<byte[]> ExportMediaAnalysisAsync(List<VideoInfoDto> mediaAnalysisList, string sheetName = "Media Analysis")
        {
            using (var package = new ExcelPackage())
            {
                await CreateMediaAnalysisWorksheetAsync(package, mediaAnalysisList, sheetName);
                return package.GetAsByteArray();
            }
        }

        public async Task SaveMediaAnalysisAsync(List<VideoInfoDto> mediaAnalysisList, string filePath, string sheetName = "Media Analysis")
        {
            using (var package = new ExcelPackage())
            {
                await CreateMediaAnalysisWorksheetAsync(package, mediaAnalysisList, sheetName);

                var fileInfo = new FileInfo(filePath);
                await package.SaveAsAsync(fileInfo);
            }
        }

        private async Task CreateMediaAnalysisWorksheetAsync(ExcelPackage package, List<VideoInfoDto> mediaAnalysisList, string sheetName)
        {
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // Başlık satırını oluştur
            var headers = new[]
            {
                "Media Name", "Extension", "Content Type", "File Size", "File Size (Bytes)",
                "Width", "Height", "Resolution", "Duration", "Duration (Seconds)",
                "Video Codec", "Audio Codec", "Video Bitrate", "Audio Bitrate",
                "Frame Rate", "Aspect Ratio", "Color Space", "Sample Rate",
                "Audio Channels", "Media Type", "Cloud Provider", "Container/Bucket",
                "Folder Path", "Full Path", "Public URL", "Is M3U8", "M3U8 Segments",
                "Created Date", "Modified Date"
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

            // Veri satırlarını yaz
            for (int row = 0; row < mediaAnalysisList.Count; row++)
            {
                var media = mediaAnalysisList[row];
                int excelRow = row + 2; // Excel 1-indexed ve başlık satırı var

                worksheet.Cells[excelRow, 1].Value = media.MediaName;
                worksheet.Cells[excelRow, 2].Value = media.Extension;
                worksheet.Cells[excelRow, 3].Value = media.ContentType;
                worksheet.Cells[excelRow, 4].Value = media.FileSizeFormatted;
                worksheet.Cells[excelRow, 5].Value = media.FileSizeBytes;
                worksheet.Cells[excelRow, 6].Value = media.Width;
                worksheet.Cells[excelRow, 7].Value = media.Height;
                worksheet.Cells[excelRow, 8].Value = media.Resolution;
                worksheet.Cells[excelRow, 9].Value = media.DurationFormatted;
                worksheet.Cells[excelRow, 10].Value = media.DurationSeconds;
                worksheet.Cells[excelRow, 11].Value = media.VideoCodec;
                worksheet.Cells[excelRow, 12].Value = media.AudioCodec;
                worksheet.Cells[excelRow, 13].Value = media.VideoBitrate;
                worksheet.Cells[excelRow, 14].Value = media.AudioBitrate;
                worksheet.Cells[excelRow, 15].Value = media.FrameRate;
                worksheet.Cells[excelRow, 16].Value = media.AspectRatio;
                worksheet.Cells[excelRow, 17].Value = media.ColorSpace;
                worksheet.Cells[excelRow, 18].Value = media.SampleRate;
                worksheet.Cells[excelRow, 19].Value = media.AudioChannels;
                worksheet.Cells[excelRow, 20].Value = media.MediaType;
                worksheet.Cells[excelRow, 21].Value = media.CloudProvider;
                worksheet.Cells[excelRow, 22].Value = media.ContainerBucket;
                worksheet.Cells[excelRow, 23].Value = media.FolderPath;
                worksheet.Cells[excelRow, 24].Value = media.PublicUrl;
                worksheet.Cells[excelRow, 25].Value = media.IsM3u8Folder;
                worksheet.Cells[excelRow, 26].Value = media.M3u8SegmentCount;
                worksheet.Cells[excelRow, 27].Value = media.CreatedDate;
                worksheet.Cells[excelRow, 28].Value = media.ModifiedDate;

                // Satır renklandirme (alternatif renkler)
                if (row % 2 == 1)
                {
                    using (var rowRange = worksheet.Cells[excelRow, 1, excelRow, headers.Length])
                    {
                        rowRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        rowRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));
                    }
                }
            }

            // Sütun genişliklerini otomatik ayarla
            worksheet.Cells.AutoFitColumns();

            // Bazı sütunlara özel genişlik ver
            worksheet.Column(1).Width = 25; // Media Name
            worksheet.Column(3).Width = 20; // Content Type
            worksheet.Column(8).Width = 15; // Resolution
            worksheet.Column(9).Width = 15; // Duration Formatted
            worksheet.Column(24).Width = 50; // Public URL

            // Tarih formatı ayarla
            worksheet.Column(28).Style.Numberformat.Format = "dd/mm/yyyy hh:mm:ss";
            worksheet.Column(29).Style.Numberformat.Format = "dd/mm/yyyy hh:mm:ss";

            // Sayı formatları ayarla
            worksheet.Column(5).Style.Numberformat.Format = "#,##0"; // File Size Bytes
            worksheet.Column(13).Style.Numberformat.Format = "#,##0"; // Video Bitrate
            worksheet.Column(14).Style.Numberformat.Format = "#,##0"; // Audio Bitrate
            worksheet.Column(15).Style.Numberformat.Format = "0.00"; // Frame Rate
            worksheet.Column(18).Style.Numberformat.Format = "#,##0"; // Sample Rate

            // Filtreler ekle
            worksheet.Cells[1, 1, mediaAnalysisList.Count + 1, headers.Length].AutoFilter = true;

            // Dondurulan paneller (başlık satırı)
            worksheet.View.FreezePanes(2, 1);

            // Sayfa ayarları
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
            worksheet.PrinterSettings.FitToPage = true;
            worksheet.PrinterSettings.FitToWidth = 1;
            worksheet.PrinterSettings.FitToHeight = 0;

            // Özet istatistikler için yeni bir sheet ekle
            await CreateSummaryWorksheetAsync(package, mediaAnalysisList);
        }

        private async Task CreateSummaryWorksheetAsync(ExcelPackage package, List<VideoInfoDto> mediaAnalysisList)
        {
            var summarySheet = package.Workbook.Worksheets.Add("Summary");

            // Genel istatistikler
            summarySheet.Cells[1, 1].Value = "MEDIA ANALYSIS SUMMARY";
            summarySheet.Cells[1, 1].Style.Font.Bold = true;
            summarySheet.Cells[1, 1].Style.Font.Size = 16;

            int row = 3;

            // Toplam medya sayısı
            summarySheet.Cells[row, 1].Value = "Total Media Count:";
            summarySheet.Cells[row, 2].Value = mediaAnalysisList.Count;
            row++;

            // Toplam dosya boyutu
            long totalSize = mediaAnalysisList.Sum(m => m.FileSizeBytes);
            summarySheet.Cells[row, 1].Value = "Total File Size:";
            summarySheet.Cells[row, 2].Value = FormatFileSize(totalSize);
            row++;

            // Medya tipine göre dağılım
            row++;
            summarySheet.Cells[row, 1].Value = "MEDIA TYPE DISTRIBUTION";
            summarySheet.Cells[row, 1].Style.Font.Bold = true;
            row++;

            var mediaTypeGroups = mediaAnalysisList.GroupBy(m => m.MediaType);
            foreach (var group in mediaTypeGroups)
            {
                summarySheet.Cells[row, 1].Value = $"{group.Key}:";
                summarySheet.Cells[row, 2].Value = group.Count();
                summarySheet.Cells[row, 3].Value = $"({(double)group.Count() / mediaAnalysisList.Count * 100:F1}%)";
                row++;
            }

            // Uzantıya göre dağılım
            row++;
            summarySheet.Cells[row, 1].Value = "EXTENSION DISTRIBUTION";
            summarySheet.Cells[row, 1].Style.Font.Bold = true;
            row++;

            var extensionGroups = mediaAnalysisList.GroupBy(m => m.Extension?.ToLower())
                .OrderByDescending(g => g.Count())
                .Take(10); // En çok kullanılan 10 uzantı

            foreach (var group in extensionGroups)
            {
                summarySheet.Cells[row, 1].Value = $"{group.Key}:";
                summarySheet.Cells[row, 2].Value = group.Count();
                summarySheet.Cells[row, 3].Value = FormatFileSize(group.Sum(m => m.FileSizeBytes));
                row++;
            }

            // Video çözünürlükleri (sadece videolar için)
            var videoItems = mediaAnalysisList.Where(m => m.MediaType == "Video" && !string.IsNullOrEmpty(m.Resolution)).ToList();
            if (videoItems.Any())
            {
                row++;
                summarySheet.Cells[row, 1].Value = "VIDEO RESOLUTION DISTRIBUTION";
                summarySheet.Cells[row, 1].Style.Font.Bold = true;
                row++;

                var resolutionGroups = videoItems.GroupBy(m => m.Resolution)
                    .OrderByDescending(g => g.Count());

                foreach (var group in resolutionGroups)
                {
                    summarySheet.Cells[row, 1].Value = $"{group.Key}:";
                    summarySheet.Cells[row, 2].Value = group.Count();
                    row++;
                }
            }

            // Sütun genişliklerini ayarla
            summarySheet.Cells.AutoFitColumns();
            summarySheet.Column(1).Width = 30;
        }

        private string FormatFileSize(long bytes)
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
    }
}
