using CloudStorageTools.VideoSizeFinder.Entities;
using CloudStorageTools.VideoSizeFinder.Enums;
using CloudStorageTools.VideoSizeFinder.Interfaces;
using CloudStorageTools.VideoSizeFinder.Services;
using System;

namespace CloudStorageTools.VideoSizeFinder.Factory
{
    public class CloudMediaServiceFactory
    {
        public static ICloudMediaService CreateService(CloudProviderType providerType, CloudConnectionConfig config)
        {
            switch (providerType)
            {
                case CloudProviderType.AWSS3:
                    if (config.AwsConfig == null)
                        throw new ArgumentException("AWS configuration is required for AWS S3 service");

                    return new AwsMediaService(
                        config.AwsConfig.AccessKey,
                        config.AwsConfig.SecretKey,
                        config.AwsConfig.Region,
                        config.AwsConfig.BucketName
                    );

                case CloudProviderType.AzureBlob:
                    if (config.AzureConfig == null)
                        throw new ArgumentException("Azure configuration is required for Azure Blob service");

                    if (!string.IsNullOrEmpty(config.AzureConfig.ConnectionString))
                    {
                        return new AzureMediaService(
                            config.AzureConfig.ConnectionString,
                            config.AzureConfig.ContainerName,
                            config.AzureConfig.FolderPath
                        );
                    }
                    else
                    {
                        return new AzureMediaService(
                            config.AzureConfig.BlobUrl,
                            config.AzureConfig.SasToken,
                            config.AzureConfig.ContainerName,
                            config.AzureConfig.FolderPath
                        );
                    }

                default:
                    throw new NotSupportedException($"Cloud provider type '{providerType}' is not supported");
            }
        }
    }
}
