using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
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
    public class AwsMediaService : ICloudMediaService
    {
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _region;
        private readonly string _bucketName;
        private readonly AmazonS3Client _s3Client;

        public AwsMediaService(string accessKey, string secretKey, string region, string bucketName)
        {
            _accessKey = accessKey;
            _secretKey = secretKey;
            _region = region;
            _bucketName = bucketName;

            _s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));
        }

        public async Task<bool> ValidateConnectionAsync(string url)
        {
            try
            {
                var response = await _s3Client.ListBucketsAsync();
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK &&
                       response.Buckets.Any(b => b.BucketName == _bucketName);
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<CloudMediaItemDto>> SearchMediaAsync(CloudMediaSearchDto searchCriteria)
        {
            var results = new List<CloudMediaItemDto>();

            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = string.IsNullOrEmpty(searchCriteria.FolderPath) ? "" : searchCriteria.FolderPath.TrimEnd('/') + "/",
                    MaxKeys = searchCriteria.MaxResults
                };

                ListObjectsV2Response response;
                do
                {
                    response = await _s3Client.ListObjectsV2Async(request);

                    foreach (var obj in response.S3Objects)
                    {
                        if (obj.Key.EndsWith("/")) continue; // Skip folder markers

                        var mediaItem = CreateMediaItemFromS3Object(obj, searchCriteria);

                        if (ShouldIncludeMediaItem(mediaItem, searchCriteria))
                        {
                            results.Add(mediaItem);
                        }
                    }

                    request.ContinuationToken = response.NextContinuationToken;

                } while ((bool)response.IsTruncated && results.Count < searchCriteria.MaxResults);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error searching media in AWS S3: {ex.Message}", ex);
            }

            return results.Take(searchCriteria.MaxResults).ToList();
        }

        public async Task<Stream> DownloadMediaAsync(string fullPath)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = fullPath
                };

                var response = await _s3Client.GetObjectAsync(request);
                return response.ResponseStream;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error downloading media from AWS S3: {ex.Message}", ex);
            }
        }

        public async Task<byte[]> DownloadMediaBytesAsync(string fullPath)
        {
            using (var stream = await DownloadMediaAsync(fullPath))
            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public string GetPublicUrl(string fullPath)
        {
            return $"https://{_bucketName}.s3.{_region}.amazonaws.com/{fullPath}";
        }

        public async Task<bool> MediaExistsAsync(string fullPath)
        {
            try
            {
                var request = new GetObjectMetadataRequest
                {
                    BucketName = _bucketName,
                    Key = fullPath
                };

                await _s3Client.GetObjectMetadataAsync(request);
                return true;
            }
            catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return false;
            }
        }

        public async Task<CloudMediaItemDto> GetMediaDetailsAsync(string fullPath)
        {
            try
            {
                var request = new GetObjectMetadataRequest
                {
                    BucketName = _bucketName,
                    Key = fullPath
                };

                var response = await _s3Client.GetObjectMetadataAsync(request);

                return new CloudMediaItemDto
                {
                    Name = Path.GetFileName(fullPath),
                    Extension = Path.GetExtension(fullPath),
                    FullPath = fullPath,
                    FolderPath = Path.GetDirectoryName(fullPath)?.Replace("\\", "/"),
                    Size = response.ContentLength,
                    SizeFormatted = FormatFileSize(response.ContentLength),
                    LastModified = response.LastModified,
                    ContentType = response.Headers.ContentType,
                    IsFolder = false,
                    IsM3u8Content = fullPath.Contains("/") && Path.GetExtension(fullPath) == ".m3u8",
                    PublicUrl = GetPublicUrl(fullPath),
                    MediaType = GetMediaTypeFromExtension(Path.GetExtension(fullPath))
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting media details from AWS S3: {ex.Message}", ex);
            }
        }

        public string GetCloudProviderName()
        {
            return "AWS S3";
        }

        private CloudMediaItemDto CreateMediaItemFromS3Object(S3Object obj, CloudMediaSearchDto searchCriteria)
        {
            var fileName = Path.GetFileName(obj.Key);
            var extension = Path.GetExtension(obj.Key);

            return new CloudMediaItemDto
            {
                Name = fileName,
                Extension = extension,
                FullPath = obj.Key,
                FolderPath = Path.GetDirectoryName(obj.Key)?.Replace("\\", "/"),
                Size = obj.Size,
                SizeFormatted = FormatFileSize(obj.Size),
                LastModified = obj.LastModified,
                ContentType = GetContentTypeFromExtension(extension),
                IsFolder = false,
                IsM3u8Content = obj.Key.Contains("/") && extension == ".m3u8",
                PublicUrl = GetPublicUrl(obj.Key),
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

        private string FormatFileSize(long? bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes ?? 0;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        public async Task SearchMediaRealTimeAsync(CloudMediaSearchDto searchCriteria,
            Action<List<CloudMediaItemDto>, int, int> onProgressCallback,
            CancellationToken cancellationToken = default)
        {
            var allResults = new List<CloudMediaItemDto>();
            int processedCount = 0;

            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = string.IsNullOrEmpty(searchCriteria.FolderPath) ? "" : searchCriteria.FolderPath.TrimEnd('/') + "/",
                    MaxKeys = 1000 // Küçük batch size
                };

                ListObjectsV2Response response;
                do
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    response = await _s3Client.ListObjectsV2Async(request, cancellationToken);
                    var batchResults = new List<CloudMediaItemDto>();

                    foreach (var obj in response.S3Objects)
                    {
                        cancellationToken.ThrowIfCancellationRequested();

                        processedCount++;

                        if (obj.Key.EndsWith("/")) continue; // Skip folder markers
                        if (allResults.Count >= searchCriteria.MaxResults) break;

                        var mediaItem = CreateMediaItemFromS3Object(obj, searchCriteria);

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

                    request.ContinuationToken = response.NextContinuationToken;

                } while ((bool)response.IsTruncated && allResults.Count < searchCriteria.MaxResults);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error in AWS S3 real-time search: {ex.Message}", ex);
            }
        }

        public void Dispose()
        {
            _s3Client?.Dispose();
        }
    }
}
