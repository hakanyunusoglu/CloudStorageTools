using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CloudStorageTools.VideoSizeFinder.Dtos;
using CloudStorageTools.VideoSizeFinder.Enums;
using CloudStorageTools.VideoSizeFinder.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class AzureMediaService : ICloudMediaService
    {
        private readonly string _connectionString;
        private readonly string _containerName;
        private readonly string _folderPath;
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;

        public AzureMediaService(string connectionString, string containerName, string folderPath = "")
        {
            _connectionString = connectionString;
            _containerName = containerName;
            _folderPath = folderPath?.TrimEnd('/') ?? "";

            _blobServiceClient = new BlobServiceClient(_connectionString);
            _containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        }

        public AzureMediaService(string blobUrl, string sasToken, string containerName, string folderPath = "")
        {
            _containerName = containerName?.TrimEnd('/').TrimStart('/') ?? "";
            _folderPath = folderPath?.TrimEnd('/').TrimStart('/') ?? "";
            sasToken = sasToken.TrimStart('?') ?? "";
            blobUrl = blobUrl.TrimEnd('/') ?? "";

            string fullUri;
            if (sasToken.StartsWith("?"))
            {
                fullUri = $"{blobUrl}/{containerName}?{sasToken}";
            }
            else
            {
                fullUri = $"{blobUrl}/{containerName}?{sasToken}";
            }

            Uri uri = new Uri(fullUri);
            _containerClient = new BlobContainerClient(uri);
        }

        public async Task<bool> ValidateConnectionAsync(string url)
        {
            try
            {
                BlobClient blobClient = _containerClient.GetBlobClient(url);
                return await blobClient.ExistsAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<CloudMediaItemDto>> SearchMediaAsync(CloudMediaSearchDto searchCriteria)
        {
            var results = new List<CloudMediaItemDto>();

            try
            {
                string prefix = string.IsNullOrEmpty(searchCriteria.FolderPath) ? _folderPath : searchCriteria.FolderPath;

                if (!string.IsNullOrEmpty(prefix) && !prefix.EndsWith("/"))
                {
                    prefix += "/";
                }

                var blobs = _containerClient.GetBlobsAsync(prefix: prefix);
                var asyncEnumerator = blobs.GetAsyncEnumerator();

                try
                {
                    while (await asyncEnumerator.MoveNextAsync())
                    {
                        if (results.Count >= searchCriteria.MaxResults) break;

                        var blobItem = asyncEnumerator.Current;
                        var mediaItem = CreateMediaItemFromBlobItem(blobItem, searchCriteria);

                        if (ShouldIncludeMediaItem(mediaItem, searchCriteria))
                        {
                            results.Add(mediaItem);
                        }
                    }
                }
                finally
                {
                    await asyncEnumerator.DisposeAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching media in Azure Blob: {ex.Message}", ex);
            }

            return results.Take(searchCriteria.MaxResults).ToList();
        }

        public async Task<Stream> DownloadMediaAsync(string fullPath)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(GetFullPath(fullPath));
                var response = await blobClient.DownloadStreamingAsync();
                return response.Value.Content;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading media from Azure Blob: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> DownloadMediaBytesAsync(string fullPath)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(GetFullPath(fullPath));
                var response = await blobClient.DownloadContentAsync();
                return response.Value.Content.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading media bytes from Azure Blob: {ex.Message}", ex);
            }
        }

        public string GetPublicUrl(string fullPath)
        {
            var blobClient = _containerClient.GetBlobClient(GetFullPath(fullPath));
            return blobClient.Uri.ToString();
        }

        public async Task<bool> MediaExistsAsync(string fullPath)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(GetFullPath(fullPath));
                var response = await blobClient.ExistsAsync();
                return response.Value;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CloudMediaItemDto> GetMediaDetailsAsync(string fullPath)
        {
            try
            {
                var blobClient = _containerClient.GetBlobClient(GetFullPath(fullPath));
                var properties = await blobClient.GetPropertiesAsync();

                return new CloudMediaItemDto
                {
                    Name = Path.GetFileName(fullPath),
                    Extension = Path.GetExtension(fullPath),
                    FullPath = fullPath,
                    FolderPath = Path.GetDirectoryName(fullPath)?.Replace("\\", "/"),
                    Size = properties.Value.ContentLength,
                    SizeFormatted = FormatFileSize(properties.Value.ContentLength),
                    LastModified = properties.Value.LastModified.DateTime,
                    ContentType = properties.Value.ContentType,
                    IsFolder = false,
                    IsM3u8Content = fullPath.Contains("/") && Path.GetExtension(fullPath) == ".m3u8",
                    PublicUrl = GetPublicUrl(fullPath),
                    MediaType = GetMediaTypeFromExtension(Path.GetExtension(fullPath))
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting media details from Azure Blob: {ex.Message}", ex);
            }
        }

        public string GetCloudProviderName()
        {
            return "Azure Blob Storage";
        }

        private CloudMediaItemDto CreateMediaItemFromBlobItem(BlobItem blobItem, CloudMediaSearchDto searchCriteria)
        {
            var fileName = Path.GetFileName(blobItem.Name);
            var extension = Path.GetExtension(blobItem.Name);

            return new CloudMediaItemDto
            {
                Name = fileName,
                Extension = extension,
                FullPath = blobItem.Name.Replace(_folderPath + "/", "").TrimStart('/'),
                FolderPath = Path.GetDirectoryName(blobItem.Name.Replace(_folderPath + "/", ""))?.Replace("\\", "/"),
                Size = blobItem.Properties.ContentLength ?? 0,
                SizeFormatted = FormatFileSize(blobItem.Properties.ContentLength ?? 0),
                LastModified = blobItem.Properties.LastModified?.DateTime ?? DateTime.MinValue,
                ContentType = blobItem.Properties.ContentType ?? GetContentTypeFromExtension(extension),
                IsFolder = false,
                IsM3u8Content = blobItem.Name.Contains("/") && extension == ".m3u8",
                PublicUrl = GetPublicUrl(blobItem.Name.Replace(_folderPath + "/", "").TrimStart('/')),
                MediaType = GetMediaTypeFromExtension(extension),
                IsSelected = false
            };
        }

        private bool ShouldIncludeMediaItem(CloudMediaItemDto mediaItem, CloudMediaSearchDto searchCriteria)
        {
            switch (searchCriteria.SearchType)
            {
                case MediaSearchType.ByMediaName:
                    return mediaItem.Name.IndexOf(searchCriteria.SearchTerm, StringComparison.OrdinalIgnoreCase) >= 0;

                case MediaSearchType.ByExtension:
                    return mediaItem.Extension.Equals(searchCriteria.Extension, StringComparison.OrdinalIgnoreCase);

                case MediaSearchType.AllMedia:
                    return true;

                default:
                    return false;
            }
        }

        private CloudMediaType GetMediaTypeFromExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return CloudMediaType.All;

            extension = extension.ToLower();

            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm", ".mkv" };
            var audioExtensions = new[] { ".mp3", ".wav", ".flac", ".aac", ".ogg" };

            if (extension == ".m3u8") return CloudMediaType.M3u8Folder;
            if (imageExtensions.Contains(extension)) return CloudMediaType.Image;
            if (videoExtensions.Contains(extension)) return CloudMediaType.Video;
            if (audioExtensions.Contains(extension)) return CloudMediaType.Audio;

            return CloudMediaType.All;
        }

        private string GetContentTypeFromExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return "application/octet-stream";

            extension = extension.ToLower();

            var contentTypes = new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".bmp", "image/bmp" },
                { ".webp", "image/webp" },
                { ".mp4", "video/mp4" },
                { ".avi", "video/x-msvideo" },
                { ".mov", "video/quicktime" },
                { ".wmv", "video/x-ms-wmv" },
                { ".flv", "video/x-flv" },
                { ".webm", "video/webm" },
                { ".mkv", "video/x-matroska" },
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".flac", "audio/flac" },
                { ".aac", "audio/aac" },
                { ".ogg", "audio/ogg" },
                { ".m3u8", "application/vnd.apple.mpegurl" },
                { ".ts", "video/mp2t" }
            };

            return contentTypes.ContainsKey(extension) ? contentTypes[extension] : "application/octet-stream";
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

        private string GetFullPath(string fileName)
        {
            return string.IsNullOrEmpty(_folderPath)
                ? fileName
                : Path.Combine(_folderPath, fileName).Replace("\\", "/");
        }

        public void Dispose()
        {
            // Azure SDK handles disposal automatically
        }

        public async Task SearchMediaRealTimeAsync(CloudMediaSearchDto searchCriteria,
            Action<List<CloudMediaItemDto>, int, int> onProgressCallback,
            CancellationToken cancellationToken = default)
        {
            var allResults = new List<CloudMediaItemDto>();
            int processedCount = 0;

            try
            {
                string prefix = string.IsNullOrEmpty(searchCriteria.FolderPath) ? _folderPath : searchCriteria.FolderPath;

                if (!string.IsNullOrEmpty(prefix) && !prefix.EndsWith("/"))
                {
                    prefix += "/";
                }

                int pageSize = Math.Min(1000, searchCriteria.MaxResults); // Küçük batch size

                var blobPages = _containerClient.GetBlobsAsync(prefix: prefix, cancellationToken: cancellationToken)
                    .AsPages(pageSizeHint: pageSize);
                var pageEnumerator = blobPages.GetAsyncEnumerator();

                try
                {
                    while (await pageEnumerator.MoveNextAsync() && allResults.Count < searchCriteria.MaxResults)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        var page = pageEnumerator.Current;
                        var batchResults = new List<CloudMediaItemDto>();

                        foreach (var blobItem in page.Values)
                        {
                            cancellationToken.ThrowIfCancellationRequested();

                            processedCount++;

                            if (allResults.Count >= searchCriteria.MaxResults) break;

                            var mediaItem = CreateMediaItemFromBlobItem(blobItem, searchCriteria);

                            if (ShouldIncludeMediaItem(mediaItem, searchCriteria))
                            {
                                batchResults.Add(mediaItem);
                                allResults.Add(mediaItem);
                            }

                            // Her 100 kayıtta bir callback çağır
                            if (processedCount % 100 == 0 && batchResults.Count > 0)
                            {
                                onProgressCallback?.Invoke(new List<CloudMediaItemDto>(batchResults), processedCount, allResults.Count);
                                batchResults.Clear();
                            }
                        }

                        // Kalan batch'i de gönder
                        if (batchResults.Count > 0)
                        {
                            onProgressCallback?.Invoke(batchResults, processedCount, allResults.Count);
                        }

                        if (allResults.Count >= searchCriteria.MaxResults)
                            break;
                    }
                }
                finally
                {
                    if (pageEnumerator != null)
                    {
                        await pageEnumerator.DisposeAsync();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in real-time search: {ex.Message}", ex);
            }
        }
    }
}
