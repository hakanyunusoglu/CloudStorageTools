using System;

namespace CloudStorageTools.CloudMediaValidator.Dtos
{
    public class MediaValidationDto
    {
        public string MediaName { get; set; }
        public bool IsExist { get; set; }
        public string ErrorMessage { get; set; }
        public string FoundWithExtension { get; set; }
        public long? FileSize { get; set; }
        public string FileSizeFormatted { get; set; }
        public string ContentType { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
