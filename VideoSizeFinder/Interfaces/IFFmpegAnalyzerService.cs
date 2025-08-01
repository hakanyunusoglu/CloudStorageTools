using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Interfaces
{
    public interface IFFmpegAnalyzerService
    {
        Task<VideoInfoDto> AnalyzeMediaAsync(byte[] mediaData, string fileName);
        Task<VideoInfoDto> AnalyzeMediaAsync(string filePath);
        bool IsFFmpegAvailable();
    }
}
