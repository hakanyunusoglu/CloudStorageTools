using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Interfaces
{
    public interface IExcelExportService
    {
        Task<byte[]> ExportMediaAnalysisAsync(List<VideoInfoDto> mediaAnalysisList, string sheetName = "Media Analysis");
        Task SaveMediaAnalysisAsync(List<VideoInfoDto> mediaAnalysisList, string filePath, string sheetName = "Media Analysis");
    }
}
