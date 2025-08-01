using CloudStorageTools.VideoSizeFinder.Interfaces;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CloudStorageTools.VideoSizeFinder.Services
{
    public class FFmpegAnalyzerService : IFFmpegAnalyzerService
    {
        private readonly string _ffmpegPath;
        private readonly string _ffprobePath;

        public FFmpegAnalyzerService()
        {
            string exeDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string assetsDirectory = Path.Combine(exeDirectory, "Assets", "FFMPEG");

            _ffmpegPath = Path.Combine(assetsDirectory, "ffmpeg.exe");
            _ffprobePath = Path.Combine(assetsDirectory, "ffprobe.exe");
        }

        public bool IsFFmpegAvailable()
        {
            return File.Exists(_ffmpegPath) && File.Exists(_ffprobePath);
        }

        public async Task<VideoInfoDto> AnalyzeMediaAsync(byte[] mediaData, string fileName)
        {
            if (!IsFFmpegAvailable())
            {
                throw new InvalidOperationException("FFmpeg tools are not available. Please ensure ffmpeg.exe and ffprobe.exe are in the Assets/FFMPEG directory.");
            }

            // Geçici dosya oluştur
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetExtension(fileName));

            try
            {
                // Medya verisini geçici dosyaya yaz
                File.WriteAllBytes(tempFilePath, mediaData);

                // Analiz et
                return await AnalyzeMediaAsync(tempFilePath);
            }
            finally
            {
                // Geçici dosyayı temizle
                if (File.Exists(tempFilePath))
                {
                    try
                    {
                        File.Delete(tempFilePath);
                    }
                    catch
                    {
                        // Geçici dosya silinemezse sessizce devam et
                    }
                }
            }
        }

        public async Task<VideoInfoDto> AnalyzeMediaAsync(string filePath)
        {
            if (!IsFFmpegAvailable())
            {
                throw new InvalidOperationException("FFmpeg tools are not available.");
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Media file not found: {filePath}");
            }

            var videoInfo = new VideoInfoDto
            {
                MediaName = Path.GetFileNameWithoutExtension(filePath),
                Extension = Path.GetExtension(filePath),
                ContentType = GetContentTypeFromExtension(Path.GetExtension(filePath))
            };

            try
            {
                // Dosya boyutunu al
                var fileInfo = new FileInfo(filePath);
                videoInfo.FileSizeBytes = fileInfo.Length;
                videoInfo.FileSizeFormatted = FormatFileSize(fileInfo.Length);
                videoInfo.CreatedDate = fileInfo.CreationTime;
                videoInfo.ModifiedDate = fileInfo.LastWriteTime;

                // FFprobe ile medya bilgilerini al
                string ffprobeOutput = await RunFFprobeAsync(filePath);

                if (!string.IsNullOrEmpty(ffprobeOutput))
                {
                    ParseFFprobeOutput(ffprobeOutput, videoInfo);
                }

                // Medya tipini belirle
                videoInfo.MediaType = DetermineMediaType(videoInfo.Extension);

                return videoInfo;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error analyzing media with FFmpeg: {ex.Message}", ex);
            }
        }

        private async Task<string> RunFFprobeAsync(string filePath)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = _ffprobePath,
                    Arguments = $"-v quiet -print_format json -show_format -show_streams \"{filePath}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        throw new Exception("Failed to start FFprobe process");
                    }

                    string output = await process.StandardOutput.ReadToEndAsync();
                    string error = await process.StandardError.ReadToEndAsync();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        throw new Exception($"FFprobe failed with exit code {process.ExitCode}: {error}");
                    }

                    return output;
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error running FFprobe: {ex.Message}", ex);
            }
        }

        private void ParseFFprobeOutput(string jsonOutput, VideoInfoDto videoInfo)
        {
            try
            {
                using (JsonDocument document = JsonDocument.Parse(jsonOutput))
                {
                    var root = document.RootElement;

                    // Format bilgilerini parse et
                    if (root.TryGetProperty("format", out JsonElement format))
                    {
                        if (format.TryGetProperty("duration", out JsonElement durationElement))
                        {
                            if (double.TryParse(durationElement.GetString(), NumberStyles.Float, CultureInfo.InvariantCulture, out double duration))
                            {
                                videoInfo.DurationSeconds = duration;
                                videoInfo.DurationFormatted = FormatDuration(duration);
                            }
                        }

                        if (format.TryGetProperty("bit_rate", out JsonElement bitrateElement))
                        {
                            if (int.TryParse(bitrateElement.GetString(), out int bitrate))
                            {
                                videoInfo.VideoBitrate = bitrate;
                            }
                        }
                    }

                    // Stream bilgilerini parse et
                    if (root.TryGetProperty("streams", out JsonElement streams))
                    {
                        foreach (JsonElement stream in streams.EnumerateArray())
                        {
                            if (stream.TryGetProperty("codec_type", out JsonElement codecType))
                            {
                                string type = codecType.GetString();

                                if (type == "video")
                                {
                                    ParseVideoStream(stream, videoInfo);
                                }
                                else if (type == "audio")
                                {
                                    ParseAudioStream(stream, videoInfo);
                                }
                            }
                        }
                    }

                    // Aspect ratio hesapla
                    if (videoInfo.Width.HasValue && videoInfo.Height.HasValue && videoInfo.Width > 0 && videoInfo.Height > 0)
                    {
                        double aspectRatio = (double)videoInfo.Width.Value / videoInfo.Height.Value;
                        videoInfo.AspectRatio = $"{videoInfo.Width}:{videoInfo.Height}";

                        // Common aspect ratios
                        if (Math.Abs(aspectRatio - 16.0 / 9.0) < 0.01)
                            videoInfo.AspectRatio = "16:9";
                        else if (Math.Abs(aspectRatio - 4.0 / 3.0) < 0.01)
                            videoInfo.AspectRatio = "4:3";
                        else if (Math.Abs(aspectRatio - 21.0 / 9.0) < 0.01)
                            videoInfo.AspectRatio = "21:9";
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error parsing FFprobe output: {ex.Message}", ex);
            }
        }

        private void ParseVideoStream(JsonElement stream, VideoInfoDto videoInfo)
        {
            if (stream.TryGetProperty("width", out JsonElement widthElement))
            {
                videoInfo.Width = widthElement.GetInt32();
            }

            if (stream.TryGetProperty("height", out JsonElement heightElement))
            {
                videoInfo.Height = heightElement.GetInt32();
            }

            if (videoInfo.Width.HasValue && videoInfo.Height.HasValue)
            {
                videoInfo.Resolution = $"{videoInfo.Width}x{videoInfo.Height}";
            }

            if (stream.TryGetProperty("codec_name", out JsonElement codecElement))
            {
                videoInfo.VideoCodec = codecElement.GetString();
            }

            if (stream.TryGetProperty("r_frame_rate", out JsonElement frameRateElement))
            {
                string frameRateStr = frameRateElement.GetString();
                if (!string.IsNullOrEmpty(frameRateStr) && frameRateStr.Contains("/"))
                {
                    var parts = frameRateStr.Split('/');
                    if (parts.Length == 2 &&
                        double.TryParse(parts[0], out double numerator) &&
                        double.TryParse(parts[1], out double denominator) &&
                        denominator != 0)
                    {
                        videoInfo.FrameRate = numerator / denominator;
                    }
                }
            }

            if (stream.TryGetProperty("color_space", out JsonElement colorSpaceElement))
            {
                videoInfo.ColorSpace = colorSpaceElement.GetString();
            }

            if (stream.TryGetProperty("bit_rate", out JsonElement videoBitrateElement))
            {
                if (int.TryParse(videoBitrateElement.GetString(), out int bitrate))
                {
                    videoInfo.VideoBitrate = bitrate;
                }
            }
        }

        private void ParseAudioStream(JsonElement stream, VideoInfoDto videoInfo)
        {
            if (stream.TryGetProperty("codec_name", out JsonElement codecElement))
            {
                videoInfo.AudioCodec = codecElement.GetString();
            }

            if (stream.TryGetProperty("sample_rate", out JsonElement sampleRateElement))
            {
                if (int.TryParse(sampleRateElement.GetString(), out int sampleRate))
                {
                    videoInfo.SampleRate = sampleRate;
                }
            }

            if (stream.TryGetProperty("channels", out JsonElement channelsElement))
            {
                videoInfo.AudioChannels = channelsElement.GetInt32();
            }

            if (stream.TryGetProperty("bit_rate", out JsonElement audioBitrateElement))
            {
                if (int.TryParse(audioBitrateElement.GetString(), out int bitrate))
                {
                    videoInfo.AudioBitrate = bitrate;
                }
            }
        }

        private string DetermineMediaType(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return "Unknown";

            extension = extension.ToLower();

            var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };
            var videoExtensions = new[] { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".webm", ".mkv" };
            var audioExtensions = new[] { ".mp3", ".wav", ".flac", ".aac", ".ogg" };

            if (Array.Exists(imageExtensions, ext => ext == extension)) return "Image";
            if (Array.Exists(videoExtensions, ext => ext == extension)) return "Video";
            if (Array.Exists(audioExtensions, ext => ext == extension)) return "Audio";

            return "Unknown";
        }

        private string GetContentTypeFromExtension(string extension)
        {
            if (string.IsNullOrEmpty(extension)) return "application/octet-stream";

            extension = extension.ToLower();

            var contentTypes = new System.Collections.Generic.Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" },
                { ".bmp", "image/bmp" },
                { ".webp", "image/webp" },
                { ".mp4", "video/mp4" },
                { ".avi", "video/x-msvideo" },
                { ".mov", "video/quicktime" },
                { ".wmv", "video/x-ms-wmv" },
                { ".flv", "video/x-flv" },
                { ".webm", "video/webm" },
                { ".mkv", "video/x-matroska" },
                { ".mp3", "audio/mpeg" },
                { ".wav", "audio/wav" },
                { ".flac", "audio/flac" },
                { ".aac", "audio/aac" },
                { ".ogg", "audio/ogg" },
                { ".m3u8", "application/vnd.apple.mpegurl" },
                { ".ts", "video/mp2t" }
            };

            return contentTypes.ContainsKey(extension) ? contentTypes[extension] : "application/octet-stream";
        }

        private string FormatFileSize(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            decimal number = bytes;

            while (Math.Round(number / 1024) >= 1)
            {
                number /= 1024;
                counter++;
            }

            return $"{number:n1} {suffixes[counter]}";
        }

        private string FormatDuration(double seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);

            if (timeSpan.TotalHours >= 1)
            {
                return timeSpan.ToString(@"hh\:mm\:ss\.fff");
            }
            else
            {
                return timeSpan.ToString(@"mm\:ss\.fff");
            }
        }
    }
}
