namespace CloudStorageTools.VideoSizeFinder.Dtos
{
    public class AzureKeysModel
    {
        public string blob_url { get; set; }
        public string sas_token { get; set; }
        public string container_name { get; set; }
        public string folder_path { get; set; }
    }
}
