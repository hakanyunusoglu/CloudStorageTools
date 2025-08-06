using System;

namespace CloudStorageTools.VideoSizeFinder.Dtos
{
    public class MediaResizerDto
    {
        public string MediaName { get; set; }
        public string MediaUrl { get; set; }
        public long MediaFileSize { get; set; }
        public string MediaFileSizeFormatted { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Resolution { get; set; }
        public bool NeedsResize { get; set; }
        public bool IsProcessed { get; set; }
        public string ResizedMediaData { get; set; } // Base64
        public string ProcessingStatus { get; set; }
        public string ErrorMessage { get; set; }
        public long? ResizedFileSize { get; set; }
        public string ResizedFileSizeFormatted { get; set; }
        public int? ResizedWidth { get; set; }
        public int? ResizedHeight { get; set; }
        public string ResizedResolution { get; set; }
        public DateTime? ProcessedDate { get; set; }
    }
}
