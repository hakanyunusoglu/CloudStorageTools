using CloudStorageTools.VideoSizeFinder.Enums;
using System.Collections.Generic;

namespace CloudStorageTools.VideoSizeFinder.Dtos
{
    public class CloudMediaSearchDto
    {
        public MediaSearchType SearchType { get; set; }
        public string SearchTerm { get; set; }
        public string Extension { get; set; }
        public CloudMediaType? MediaType { get; set; }
        public string ContainerBucket { get; set; }
        public string FolderPath { get; set; }
        public int MaxResults { get; set; } = 1000;
        public bool IncludeSubFolders { get; set; } = true;
        public List<string> AllowedExtensions { get; set; }

        public CloudMediaSearchDto()
        {
            AllowedExtensions = new List<string>();
            MediaType = CloudMediaType.All;
        }
    }
}
