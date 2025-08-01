using CloudStorageTools.VideoSizeFinder.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Interfaces
{
    public interface ICloudMediaService
    {
        Task<bool> ValidateConnectionAsync(string url);
        Task<List<CloudMediaItemDto>> SearchMediaAsync(CloudMediaSearchDto searchCriteria);
        Task<Stream> DownloadMediaAsync(string fullPath);
        Task<byte[]> DownloadMediaBytesAsync(string fullPath);
        string GetPublicUrl(string fullPath);
        Task<bool> MediaExistsAsync(string fullPath);
        Task<CloudMediaItemDto> GetMediaDetailsAsync(string fullPath);
        string GetCloudProviderName();
        Task SearchMediaRealTimeAsync(CloudMediaSearchDto searchCriteria,
            Action<List<CloudMediaItemDto>, int, int> onProgressCallback,
            CancellationToken cancellationToken = default);
    }
}
