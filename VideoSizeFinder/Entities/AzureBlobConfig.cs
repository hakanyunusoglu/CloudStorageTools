namespace CloudStorageTools.VideoSizeFinder.Entities
{
    public class AzureBlobConfig
    {
        public string ConnectionString { get; set; }
        public string BlobUrl { get; set; }
        public string SasToken { get; set; }
        public string ContainerName { get; set; }
        public string FolderPath { get; set; }
    }
}
