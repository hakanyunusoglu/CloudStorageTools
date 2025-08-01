using CloudStorageTools.VideoSizeFinder.Dtos;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class KeysLoaderService
    {
        public static AwsKeysModel LoadAwsKeysFromCsv(string csvPath)
        {
            try
            {
                using (var reader = new StreamReader(csvPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    if (csv.Read())
                    {
                        var headers = csv.Context.Reader.HeaderRecord;
                        var keys = new Dictionary<string, string>();

                        foreach (var header in headers)
                        {
                            string value = csv.GetField(header)?.Replace(" ", "").Trim() ?? "";
                            keys[header.ToLower()] = value;
                        }

                        if (keys.ContainsKey("access_key") && keys.ContainsKey("secret_access_key") &&
                            keys.ContainsKey("bucket_name") && keys.ContainsKey("region"))
                        {
                            return new AwsKeysModel
                            {
                                access_key = keys["access_key"],
                                secret_access_key = keys["secret_access_key"],
                                bucket_name = keys["bucket_name"],
                                region = keys["region"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"AWS keys loading error: {ex.Message}", ex);
            }

            return null;
        }

        public static AzureKeysModel LoadAzureKeysFromCsv(string csvPath)
        {
            try
            {
                using (var reader = new StreamReader(csvPath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read();
                    csv.ReadHeader();

                    if (csv.Read())
                    {
                        var headers = csv.Context.Reader.HeaderRecord;
                        var keys = new Dictionary<string, string>();

                        foreach (var header in headers)
                        {
                            string value = csv.GetField(header)?.Replace(" ", "").Trim() ?? "";
                            keys[header.ToLower()] = value;
                        }

                        if (keys.ContainsKey("blob_url") && keys.ContainsKey("sas_token") &&
                            keys.ContainsKey("container_name"))
                        {
                            return new AzureKeysModel
                            {
                                blob_url = keys["blob_url"],
                                sas_token = keys["sas_token"],
                                container_name = keys["container_name"],
                                folder_path = keys.ContainsKey("folder_path") ? keys["folder_path"] : ""
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Azure keys loading error: {ex.Message}", ex);
            }

            return null;
        }
    }
}
