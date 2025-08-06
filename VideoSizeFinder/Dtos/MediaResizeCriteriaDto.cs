namespace CloudStorageTools.VideoSizeFinder.Dtos
{
    public class MediaResizeCriteriaDto
    {
        public long MaxFileSizeMB { get; set; }
        public int MaxWidth { get; set; }
        public int MaxHeight { get; set; }
        public int TargetWidth { get; set; }
        public int TargetHeight { get; set; }
        public int Quality { get; set; } = 85; // 1-100 arası, varsayılan 85
        public bool MaintainAspectRatio { get; set; } = true;
        public string ResizeMode { get; set; } = "Fit"; // Fit, Stretch, Crop
    }
}
