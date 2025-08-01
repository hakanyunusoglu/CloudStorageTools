namespace CloudStorageTools.VideoSizeFinder.Entities
{
    public class CloudConnectionConfig
    {
        public AwsS3Config AwsConfig { get; set; }
        public AzureBlobConfig AzureConfig { get; set; }
    }
}
