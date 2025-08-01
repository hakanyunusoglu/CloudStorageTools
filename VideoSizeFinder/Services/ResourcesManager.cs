using System.IO;
using System.Text;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public static class ResourcesManager
    {
        public static string GetAwsKeysTemplate()
        {
            return "access_key,secret_access_key,bucket_name,region\n";
        }

        public static string GetAzureKeysTemplate()
        {
            return "blob_url,sas_token,container_name,folder_path\n";
        }

        public static void SaveTemplate(string content, string filePath)
        {
            byte[] fileContent = Encoding.UTF8.GetBytes(content);
            File.WriteAllBytes(filePath, fileContent);
        }
    }
}
