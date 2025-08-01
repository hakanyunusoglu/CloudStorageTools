using CloudStorageTools.VideoSizeFinder.Dtos;
using CloudStorageTools.VideoSizeFinder.Entities;
using CloudStorageTools.VideoSizeFinder.Enums;
using CloudStorageTools.VideoSizeFinder.Factory;
using CloudStorageTools.VideoSizeFinder.Interfaces;
using CloudStorageTools.VideoSizeFinder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CloudStorageTools.VideoSizeFinder.Components
{
    public partial class VideoSizeFinderForm : Form
    {
        private ICloudMediaService _cloudMediaService;
        private IFFmpegAnalyzerService _ffmpegAnalyzer;
        private IExcelExportService _excelExportService;
        private CancellationTokenSource _cancellationTokenSource;

        private List<CloudMediaItemDto> _searchResults = new List<CloudMediaItemDto>();
        private List<VideoInfoDto> _analysisResults = new List<VideoInfoDto>();

        private CloudProviderType _currentProvider;
        private CloudConnectionConfig _connectionConfig;

        // Key visibility flags
        private bool isAwsKeysVisible = false;
        private bool isAzureKeysVisible = false;

        // Cancel operation flag for partial save
        private bool _operationCancelled = false;
        public VideoSizeFinderForm()
        {
            InitializeComponent();
            InitializeServices();
            InitializeUI();
        }

        private void InitializeServices()
        {
            _ffmpegAnalyzer = new FFmpegAnalyzerService();
            _excelExportService = new ExcelExportService();
        }

        private void InitializeUI()
        {
            this.Text = "Cloud Media Size Finder & Analyzer";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Provider seçimi için radioButton'ları ayarla
            rbAwsS3.CheckedChanged += OnProviderChanged;
            rbAzureBlob.CheckedChanged += OnProviderChanged;

            // Arama tipi için radioButton'ları ayarla
            rbSearchByName.Checked = true;
            rbSearchByName.CheckedChanged += OnSearchTypeChanged;
            rbSearchByExtension.CheckedChanged += OnSearchTypeChanged;
            rbSearchAllMedia.CheckedChanged += OnSearchTypeChanged;

            // DataGridView ayarları
            ConfigureDataGridView();

            // İlk durumda kontrolleri deaktif et
            EnableSearchControls(false);
            EnableAnalysisControls(false);

            // FFmpeg kontrolü
            if (!_ffmpegAnalyzer.IsFFmpegAvailable())
            {
                lblFFmpegStatus.Text = "⚠️ FFmpeg not found in Assets/FFMPEG directory";
                lblFFmpegStatus.ForeColor = Color.Red;
                btnAnalyzeSelected.Enabled = false;
            }
            else
            {
                lblFFmpegStatus.Text = "✅ FFmpeg is available";
                lblFFmpegStatus.ForeColor = Color.Green;
            }
        }
        private void ConfigureDataGridView()
        {
            dgvSearchResults.AutoGenerateColumns = false;
            dgvSearchResults.AllowUserToAddRows = false;
            dgvSearchResults.AllowUserToDeleteRows = false;
            dgvSearchResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSearchResults.MultiSelect = true;
            dgvSearchResults.ReadOnly = false;

            // Checkbox column for selection
            var checkBoxColumn = new DataGridViewCheckBoxColumn
            {
                Name = "Selected",
                HeaderText = "Select",
                DataPropertyName = "IsSelected",
                Width = 60,
                ReadOnly = false
            };
            dgvSearchResults.Columns.Add(checkBoxColumn);

            // Media name column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MediaName",
                HeaderText = "Media Name",
                DataPropertyName = "Name",
                Width = 200,
                ReadOnly = true
            });

            // Extension column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Extension",
                HeaderText = "Extension",
                DataPropertyName = "Extension",
                Width = 80,
                ReadOnly = true
            });

            // Size column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Size",
                HeaderText = "Size",
                DataPropertyName = "SizeFormatted",
                Width = 100,
                ReadOnly = true
            });

            // Media type column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MediaType",
                HeaderText = "Type",
                DataPropertyName = "MediaType",
                Width = 80,
                ReadOnly = true
            });

            // Last modified column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LastModified",
                HeaderText = "Last Modified",
                DataPropertyName = "LastModified",
                Width = 150,
                ReadOnly = true
            });

            // Full path column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FullPath",
                HeaderText = "Full Path",
                DataPropertyName = "FullPath",
                Width = 300,
                ReadOnly = true
            });

            // Public URL column
            dgvSearchResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PublicUrl",
                HeaderText = "Public URL",
                DataPropertyName = "PublicUrl",
                Width = 300,
                ReadOnly = true
            });
        }

        private void OnProviderChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                if (rb == rbAwsS3)
                {
                    _currentProvider = CloudProviderType.AWSS3;
                    ShowAwsConfiguration();
                }
                else if (rb == rbAzureBlob)
                {
                    _currentProvider = CloudProviderType.AzureBlob;
                    ShowAzureConfiguration();
                }

                EnableSearchControls(false);
                _cloudMediaService = null;
            }
        }

        private void OnSearchTypeChanged(object sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                txtSearchTerm.Enabled = rb == rbSearchByName;
                cmbExtension.Enabled = rb == rbSearchByExtension;

                if (rb == rbSearchByName)
                {
                    lblSearchHint.Text = "Enter media name to search (partial names supported)";
                }
                else if (rb == rbSearchByExtension)
                {
                    lblSearchHint.Text = "Select file extension to filter";
                }
                else
                {
                    lblSearchHint.Text = "All media files will be listed";
                }
            }
        }

        private void btnVisibleKey_Click(object sender, EventArgs e)
        {
            isAwsKeysVisible = !isAwsKeysVisible;

            txtAwsAccessKey.PasswordChar = isAwsKeysVisible ? '\0' : '*';
            txtAwsSecretKey.PasswordChar = isAwsKeysVisible ? '\0' : '*';

            btnVisibleKey.Text = isAwsKeysVisible ? "🔒" : "👁️";
        }

        private void btnAzureVisibleKey_Click(object sender, EventArgs e)
        {
            isAzureKeysVisible = !isAzureKeysVisible;

            txtAzureBlobUrl.PasswordChar = isAzureKeysVisible ? '\0' : '*';
            txtAzureSasToken.PasswordChar = isAzureKeysVisible ? '\0' : '*';

            btnAzureVisibleKey.Text = isAzureKeysVisible ? "🔒" : "👁️";
        }

        private void btnFillKeysFromCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    openFileDialog.Title = "Choose AWS Keys File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string csvPath = openFileDialog.FileName;

                        if (Path.GetExtension(csvPath).ToLower() != ".csv")
                        {
                            MessageBox.Show("Only files with .CSV extension can be uploaded!", "Incorrect File Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var awsKeys = KeysLoaderService.LoadAwsKeysFromCsv(csvPath);

                        if (awsKeys != null)
                        {
                            txtAwsAccessKey.Text = awsKeys.access_key;
                            txtAwsSecretKey.Text = awsKeys.secret_access_key;
                            txtAwsBucketName.Text = awsKeys.bucket_name;
                            txtAwsRegion.Text = awsKeys.region;

                            MessageBox.Show("AWS Keys information uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The CSV format is wrong. Use the headings in the template in the same way!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading AWS keys: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFillAzureKeysFromCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    openFileDialog.Title = "Choose Azure Keys File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string csvPath = openFileDialog.FileName;

                        if (Path.GetExtension(csvPath).ToLower() != ".csv")
                        {
                            MessageBox.Show("Only files with .CSV extension can be uploaded!", "Incorrect File Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        var azureKeys = KeysLoaderService.LoadAzureKeysFromCsv(csvPath);

                        if (azureKeys != null)
                        {
                            txtAzureBlobUrl.Text = azureKeys.blob_url;
                            txtAzureSasToken.Text = azureKeys.sas_token;
                            txtAzureContainerName.Text = azureKeys.container_name;
                            txtAzureFolderPath.Text = azureKeys.folder_path;

                            MessageBox.Show("Azure Keys information uploaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("The CSV format is wrong. Use the headings in the template in the same way!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Azure keys: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKeysCsvTemplateDownload_Click(object sender, EventArgs e)
        {
            DownloadKeysTemplate("aws_keys_template.csv", ResourcesManager.GetAwsKeysTemplate(), "AWS S3");
        }

        private void btnAzureKeysCsvTemplateDownload_Click(object sender, EventArgs e)
        {
            DownloadKeysTemplate("azure_keys_template.csv", ResourcesManager.GetAzureKeysTemplate(), "Azure Blob");
        }

        private void DownloadKeysTemplate(string fileName, string template, string providerName)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                saveFileDialog.Title = $"Save {providerName} Keys Template File";
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ResourcesManager.SaveTemplate(template, saveFileDialog.FileName);
                        MessageBox.Show($"{providerName} Keys Template file saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void ShowAwsConfiguration()
        {
            pnlAzureConfig.Visible = false;
            pnlAwsConfig.Visible = true;
        }

        private void ShowAzureConfiguration()
        {
            pnlAwsConfig.Visible = false;
            pnlAzureConfig.Visible = true;
        }

        private async void btnTestConnection_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestConnection.Enabled = false;
                lblConnectionStatus.Text = "Testing connection...";
                lblConnectionStatus.ForeColor = Color.Orange;

                _connectionConfig = CreateConnectionConfig();
                _cloudMediaService = CloudMediaServiceFactory.CreateService(_currentProvider, _connectionConfig);

                string folderCheck = null;

                if (_currentProvider == CloudProviderType.AzureBlob)
                {
                    folderCheck = _connectionConfig.AzureConfig.FolderPath;
                }
                else
                {
                    folderCheck = _connectionConfig.AwsConfig.BucketName;
                }

                bool isConnected = await _cloudMediaService.ValidateConnectionAsync("products/11544473000_066_100.mp4");

                if (isConnected)
                {
                    lblConnectionStatus.Text = "✅ Connection successful";
                    lblConnectionStatus.ForeColor = Color.Green;
                    EnableSearchControls(true);
                }
                else
                {
                    lblConnectionStatus.Text = "❌ Connection failed";
                    lblConnectionStatus.ForeColor = Color.Red;
                    EnableSearchControls(false);
                }
            }
            catch (Exception ex)
            {
                lblConnectionStatus.Text = $"❌ Error: {ex.Message}";
                lblConnectionStatus.ForeColor = Color.Red;
                EnableSearchControls(false);
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }

        private CloudConnectionConfig CreateConnectionConfig()
        {
            var config = new CloudConnectionConfig();

            if (_currentProvider == CloudProviderType.AWSS3)
            {
                config.AwsConfig = new AwsS3Config
                {
                    AccessKey = txtAwsAccessKey.Text.Trim(),
                    SecretKey = txtAwsSecretKey.Text.Trim(),
                    Region = txtAwsRegion.Text.Trim(),
                    BucketName = txtAwsBucketName.Text.Trim()
                };
            }
            else if (_currentProvider == CloudProviderType.AzureBlob)
            {
                config.AzureConfig = new AzureBlobConfig
                {
                    BlobUrl = txtAzureBlobUrl.Text.Trim(),
                    SasToken = txtAzureSasToken.Text.Trim(),
                    ContainerName = txtAzureContainerName.Text.Trim(),
                    FolderPath = txtAzureFolderPath.Text.Trim()
                };
            }

            return config;
        }

        private void EnableSearchControls(bool enabled)
        {
            grpSearchOptions.Enabled = enabled;
            btnSearch.Enabled = enabled;
        }

        private void EnableAnalysisControls(bool enabled)
        {
            btnDownloadSelected.Enabled = enabled;
            btnAnalyzeSelected.Enabled = enabled && _ffmpegAnalyzer.IsFFmpegAvailable();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (_cloudMediaService == null)
            {
                MessageBox.Show("Please test connection first!", "Connection Required",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnSearch.Enabled = false;
                btnCancelOperation.Enabled = true;
                lblSearchStatus.Text = "Searching...";
                lblSearchStatus.ForeColor = Color.Orange;
                progressBarSearch.Visible = true;
                progressBarSearch.Style = ProgressBarStyle.Marquee;

                // Real-time sonuç gösterimi için liste temizle
                _searchResults.Clear();

                // BindingList kullanarak otomatik UI güncellemesi sağla
                var bindingList = new BindingList<CloudMediaItemDto>(_searchResults);
                dgvSearchResults.DataSource = bindingList;

                var searchCriteria = CreateSearchCriteria();
                _cancellationTokenSource = new CancellationTokenSource();

                // Real-time search başlat
                if (_cloudMediaService is AzureMediaService azureService)
                {
                    await azureService.SearchMediaRealTimeAsync(searchCriteria,
                        (batchResults, processed, found) =>
                        {
                            // UI thread'de çalıştır
                            if (InvokeRequired)
                            {
                                Invoke(new Action(() => AddResultsBatch(batchResults, processed, found, bindingList)));
                            }
                            else
                            {
                                AddResultsBatch(batchResults, processed, found, bindingList);
                            }
                        },
                        _cancellationTokenSource.Token);
                }
                else if (_cloudMediaService is AwsMediaService awsService)
                {
                    await awsService.SearchMediaRealTimeAsync(searchCriteria,
                        (batchResults, processed, found) =>
                        {
                            // UI thread'de çalıştır
                            if (InvokeRequired)
                            {
                                Invoke(new Action(() => AddResultsBatch(batchResults, processed, found, bindingList)));
                            }
                            else
                            {
                                AddResultsBatch(batchResults, processed, found, bindingList);
                            }
                        },
                        _cancellationTokenSource.Token);
                }
                else
                {
                    // Fallback: Geleneksel yöntem
                    var results = await _cloudMediaService.SearchMediaAsync(searchCriteria);
                    foreach (var item in results)
                    {
                        bindingList.Add(item);
                    }
                }

                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    lblSearchStatus.Text = $"✅ Found {_searchResults.Count} media files";
                    lblSearchStatus.ForeColor = Color.Green;
                }

                EnableAnalysisControls(_searchResults.Count > 0);
                chkSelectAll.Enabled = _searchResults.Count > 0;
            }
            catch (OperationCanceledException)
            {
                lblSearchStatus.Text = "❌ Search cancelled";
                lblSearchStatus.ForeColor = Color.Orange;
            }
            catch (Exception ex)
            {
                lblSearchStatus.Text = $"❌ Search failed: {ex.Message}";
                lblSearchStatus.ForeColor = Color.Red;
                MessageBox.Show($"Search error: {ex.Message}", "Search Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnSearch.Enabled = true;
                btnCancelOperation.Enabled = false;
                progressBarSearch.Visible = false;
            }
        }

        private void AddResultsBatch(List<CloudMediaItemDto> batchResults, int processed, int found, BindingList<CloudMediaItemDto> bindingList)
        {
            try
            {
                // Batch sonuçları BindingList'e ekle (otomatik UI güncellemesi)
                foreach (var item in batchResults)
                {
                    bindingList.Add(item);
                }

                // Status güncelle
                lblSearchStatus.Text = $"🔍 Processed: {processed} | Found: {found} media files";

                // Son eklenen kayıtları görünür yap
                if (dgvSearchResults.Rows.Count > 0)
                {
                    try
                    {
                        int lastRowIndex = dgvSearchResults.Rows.Count - 1;
                        dgvSearchResults.FirstDisplayedScrollingRowIndex = Math.Max(0, lastRowIndex - 5);
                        dgvSearchResults.Rows[lastRowIndex].Selected = true;
                    }
                    catch
                    {
                        // Scroll hatası varsa sessizce devam et
                    }
                }

                // UI'yi zorla yenile
                dgvSearchResults.Refresh();
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                // UI update hatalarını logla ama devam et
                System.Diagnostics.Debug.WriteLine($"UI Update Error: {ex.Message}");
            }
        }

        private async Task HandleCancelledAnalysis(int completedCount)
        {
            lblOperationStatus.Text = $"⚠️ Analysis cancelled - {completedCount} files analyzed";
            lblOperationStatus.ForeColor = Color.Orange;

            if (_analysisResults.Count > 0)
            {
                var result = MessageBox.Show(
                    $"Analysis was cancelled, but {_analysisResults.Count} files were successfully analyzed.\n\nWould you like to save the partial results to Excel?",
                    "Save Partial Results?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await ExportAnalysisToExcelAsync();
                }
            }
            else
            {
                MessageBox.Show("Analysis was cancelled and no files were processed.",
                    "Analysis Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private CloudMediaSearchDto CreateSearchCriteria()
        {
            var criteria = new CloudMediaSearchDto
            {
                FolderPath = GetCurrentFolderPath(),
                MaxResults = (int)numMaxResults.Value
            };

            if (rbSearchByName.Checked)
            {
                criteria.SearchType = MediaSearchType.ByMediaName;
                criteria.SearchTerm = txtSearchTerm.Text.Trim();
            }
            else if (rbSearchByExtension.Checked)
            {
                criteria.SearchType = MediaSearchType.ByExtension;
                criteria.Extension = cmbExtension.SelectedItem?.ToString();
            }
            else
            {
                criteria.SearchType = MediaSearchType.AllMedia;
            }

            return criteria;
        }

        private string GetCurrentFolderPath()
        {
            if (_currentProvider == CloudProviderType.AzureBlob)
            {
                return txtAzureFolderPath.Text.Trim();
            }
            // AWS S3 için folder path ayrı bir textbox olabilir veya arama kriterlerinde belirtilebilir
            return string.Empty;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in _searchResults)
            {
                item.IsSelected = chkSelectAll.Checked;
            }
            dgvSearchResults.Refresh();
        }

        private async void btnDownloadSelected_Click(object sender, EventArgs e)
        {
            var selectedItems = _searchResults.Where(x => (bool)x.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one media file to download.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select folder to save downloaded files";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    await DownloadSelectedFilesAsync(selectedItems, folderDialog.SelectedPath);
                }
            }
        }

        private async Task DownloadSelectedFilesAsync(List<CloudMediaItemDto> selectedItems, string downloadPath)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _operationCancelled = false;

            try
            {
                btnDownloadSelected.Enabled = false;
                btnCancelOperation.Enabled = true;
                progressBarOperation.Visible = true;
                progressBarOperation.Style = ProgressBarStyle.Blocks;
                progressBarOperation.Maximum = selectedItems.Count;
                progressBarOperation.Value = 0;

                int completed = 0;

                foreach (var item in selectedItems)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        _operationCancelled = true;
                        break;
                    }

                    try
                    {
                        lblOperationStatus.Text = $"Downloading: {item.Name}";

                        var mediaData = await _cloudMediaService.DownloadMediaBytesAsync(item.FullPath);
                        string filePath = System.IO.Path.Combine(downloadPath, item.Name);

                        File.WriteAllBytes(filePath, mediaData);

                        completed++;
                        progressBarOperation.Value = completed;
                        lblOperationStatus.Text = $"Downloaded {completed}/{selectedItems.Count} files";
                    }
                    catch (Exception ex)
                    {
                        LogError($"Failed to download {item.Name}: {ex.Message}");
                    }
                }

                if (!_operationCancelled)
                {
                    lblOperationStatus.Text = $"✅ Downloaded {completed} files successfully";
                    lblOperationStatus.ForeColor = Color.Green;
                    MessageBox.Show($"Download completed! Downloaded {completed} files to:\n{downloadPath}",
                        "Download Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lblOperationStatus.Text = $"⚠️ Download cancelled - {completed} files completed";
                    lblOperationStatus.ForeColor = Color.Orange;
                }
            }
            catch (OperationCanceledException)
            {
                lblOperationStatus.Text = "Download cancelled";
                lblOperationStatus.ForeColor = Color.Orange;
            }
            finally
            {
                btnDownloadSelected.Enabled = true;
                btnCancelOperation.Enabled = false;
                progressBarOperation.Visible = false;
            }
        }

        private async void btnAnalyzeSelected_Click(object sender, EventArgs e)
        {
            var selectedItems = _searchResults.Where(x => (bool)x.IsSelected).ToList();

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Please select at least one media file to analyze.",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            await AnalyzeSelectedFilesAsync(selectedItems);
        }

        private async Task AnalyzeSelectedFilesAsync(List<CloudMediaItemDto> selectedItems)
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _operationCancelled = false;

            try
            {
                btnAnalyzeSelected.Enabled = false;
                btnCancelOperation.Enabled = true;
                progressBarOperation.Visible = true;
                progressBarOperation.Style = ProgressBarStyle.Blocks;
                progressBarOperation.Maximum = selectedItems.Count;
                progressBarOperation.Value = 0;

                _analysisResults.Clear();
                int completed = 0;

                foreach (var item in selectedItems)
                {
                    if (_cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        _operationCancelled = true;
                        break;
                    }

                    try
                    {
                        lblOperationStatus.Text = $"Analyzing: {item.Name}";

                        var mediaData = await _cloudMediaService.DownloadMediaBytesAsync(item.FullPath);
                        var analysisResult = await _ffmpegAnalyzer.AnalyzeMediaAsync(mediaData, item.Name);

                        // Cloud bilgilerini ekle
                        analysisResult.CloudProvider = _cloudMediaService.GetCloudProviderName();
                        analysisResult.ContainerBucket = GetContainerBucketName();
                        analysisResult.FolderPath = item.FolderPath;
                        analysisResult.FullPath = item.FullPath;
                        analysisResult.PublicUrl = item.PublicUrl;
                        analysisResult.IsM3u8Folder = item.IsM3u8Content;

                        _analysisResults.Add(analysisResult);

                        completed++;
                        progressBarOperation.Value = completed;
                        lblOperationStatus.Text = $"Analyzed {completed}/{selectedItems.Count} files";
                    }
                    catch (Exception ex)
                    {
                        LogError($"Failed to analyze {item.Name}: {ex.Message}");
                    }
                }

                if (!_operationCancelled)
                {
                    lblOperationStatus.Text = $"✅ Analyzed {completed} files successfully";
                    lblOperationStatus.ForeColor = Color.Green;

                    // Excel export seçeneği sun
                    if (_analysisResults.Count > 0)
                    {
                        var result = MessageBox.Show(
                            $"Analysis completed for {_analysisResults.Count} files.\n\nWould you like to export the results to Excel?",
                            "Analysis Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            await ExportAnalysisToExcelAsync();
                        }
                    }
                }
                else
                {
                    // Cancel durumunda kaydetmek isteyip istemediğini sor
                    await HandleCancelledAnalysis(completed);
                }
            }
            catch (OperationCanceledException)
            {
                await HandleCancelledAnalysis(_analysisResults.Count);
            }
            finally
            {
                btnAnalyzeSelected.Enabled = true;
                btnCancelOperation.Enabled = false;
                progressBarOperation.Visible = false;
            }
        }

        private async Task ExportAnalysisToExcelAsync()
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                saveDialog.Title = "Save Media Analysis Report";
                saveDialog.FileName = $"MediaAnalysis_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        lblOperationStatus.Text = "Exporting to Excel...";

                        await _excelExportService.SaveMediaAnalysisAsync(_analysisResults, saveDialog.FileName);

                        lblOperationStatus.Text = "✅ Excel export completed";
                        lblOperationStatus.ForeColor = Color.Green;

                        var result = MessageBox.Show(
                            $"Excel report saved successfully to:\n{saveDialog.FileName}\n\nWould you like to open the file?",
                            "Export Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveDialog.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to export to Excel: {ex.Message}",
                            "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private string GetContainerBucketName()
        {
            if (_currentProvider == CloudProviderType.AWSS3)
            {
                return _connectionConfig.AwsConfig?.BucketName;
            }
            else if (_currentProvider == CloudProviderType.AzureBlob)
            {
                return _connectionConfig.AzureConfig?.ContainerName;
            }
            return string.Empty;
        }

        private void btnCancelOperation_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
        }

        private void LogError(string message)
        {
            // Log hata mesajlarını bir listeye kaydet veya dosyaya yaz
            System.Diagnostics.Debug.WriteLine($"[ERROR] {DateTime.Now}: {message}");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            base.OnFormClosing(e);
        }
    }
}
