using CloudStorageTools.VideoSizeFinder.Enums;
using System;

namespace CloudStorageTools.VideoSizeFinder.Dtos
{
    public class CloudMediaItemDto
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string FullPath { get; set; }
        public string FolderPath { get; set; }
        public long? Size { get; set; }
        public string SizeFormatted { get; set; }
        public DateTime? LastModified { get; set; }
        public string ContentType { get; set; }
        public bool IsFolder { get; set; }
        public bool IsM3u8Content { get; set; }
        public string PublicUrl { get; set; }
        public CloudMediaType? MediaType { get; set; }
        public bool IsSelected { get; set; }
    }
}
