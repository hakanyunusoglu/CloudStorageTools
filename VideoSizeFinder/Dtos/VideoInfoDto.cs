using System;

namespace CloudStorageTools.VideoSizeFinder
{
    public class VideoInfoDto
    {
        public string MediaName { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public long FileSizeBytes { get; set; }
        public string FileSizeFormatted { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Resolution { get; set; }
        public double? DurationSeconds { get; set; }
        public string DurationFormatted { get; set; }
        public string VideoCodec { get; set; }
        public string AudioCodec { get; set; }
        public int? VideoBitrate { get; set; }
        public int? AudioBitrate { get; set; }
        public double? FrameRate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string CloudProvider { get; set; }
        public string ContainerBucket { get; set; }
        public string FolderPath { get; set; }
        public string PublicUrl { get; set; }
        public bool IsM3u8Folder { get; set; }
        public int? M3u8SegmentCount { get; set; }
        public string AspectRatio { get; set; }
        public string ColorSpace { get; set; }
        public int? SampleRate { get; set; }
        public int? AudioChannels { get; set; }
        public string MediaType { get; set; } // Video, Audio, Image

        public VideoInfoDto()
        {
            CreatedDate = DateTime.Now;
        }
    }
}
