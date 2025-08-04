using CloudStorageTools.CloudMediaValidator.Dtos;
using CloudStorageTools.VideoSizeFinder.Entities;
using CloudStorageTools.VideoSizeFinder.Enums;
using CloudStorageTools.VideoSizeFinder.Factory;
using CloudStorageTools.VideoSizeFinder.Interfaces;
using CloudStorageTools.VideoSizeFinder.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace CloudStorageTools.CloudMediaValidator.Components
{
    public partial class CloudMediaValidatorForm : Form
    {
        private ICloudMediaService _cloudMediaService;
        private CloudMediaValidatorService _validatorService;
        private CancellationTokenSource _cancellationTokenSource;

        private List<string> _mediaNames = new List<string>();
        private List<MediaValidationDto> _validationResults = new List<MediaValidationDto>();
        private BindingList<MediaValidationDto> _bindingList;

        private CloudProviderType _currentProvider;
        private CloudConnectionConfig _connectionConfig;

        // Key visibility flags
        private bool isAwsKeysVisible = false;
        private bool isAzureKeysVisible = false;

        private bool _operationCancelled = false;

        public CloudMediaValidatorForm()
        {
            InitializeComponent();
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Cloud Media Validator";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Provider seçimi için radioButton'ları ayarla
            rbAwsS3.CheckedChanged += OnProviderChanged;
            rbAzureBlob.CheckedChanged += OnProviderChanged;

            // DataGridView ayarları
            ConfigureDataGridView();

            // İlk durumda kontrolleri deaktif et
            EnableValidationControls(false);

            AddLogMessage("Validation system ready...", Color.Cyan);
        }

        private void ConfigureDataGridView()
        {
            dgvValidationResults.AutoGenerateColumns = false;
            dgvValidationResults.AllowUserToAddRows = false;
            dgvValidationResults.AllowUserToDeleteRows = false;
            dgvValidationResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvValidationResults.MultiSelect = true;
            dgvValidationResults.ReadOnly = true;

            // Media name column
            dgvValidationResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MediaName",
                HeaderText = "Media Name",
                DataPropertyName = "MediaName",
                Width = 300,
                ReadOnly = true
            });

            // Is exist column
            dgvValidationResults.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsExist",
                HeaderText = "Exists",
                DataPropertyName = "IsExist",
                Width = 80,
                ReadOnly = true
            });

            // Status column
            var statusColumn = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 120,
                ReadOnly = true
            };
            dgvValidationResults.Columns.Add(statusColumn);

            // Found with extension column
            dgvValidationResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FoundWithExtension",
                HeaderText = "Found As",
                DataPropertyName = "FoundWithExtension",
                Width = 200,
                ReadOnly = true
            });

            // File size column
            dgvValidationResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "FileSizeFormatted",
                HeaderText = "File Size",
                DataPropertyName = "FileSizeFormatted",
                Width = 100,
                ReadOnly = true
            });

            // Error message column
            dgvValidationResults.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ErrorMessage",
                HeaderText = "Error Message",
                DataPropertyName = "ErrorMessage",
                Width = 250,
                ReadOnly = true
            });

            // DataBindingComplete event handler for row coloring
            dgvValidationResults.DataBindingComplete += DgvValidationResults_DataBindingComplete;
        }

        private void DgvValidationResults_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in dgvValidationResults.Rows)
            {
                if (row.DataBoundItem is MediaValidationDto result)
                {
                    // Status sütununu güncelle
                    if (!string.IsNullOrEmpty(result.ErrorMessage))
                    {
                        row.Cells["Status"].Value = "ERROR";
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220); // Açık kırmızı
                    }
                    else if (result.IsExist)
                    {
                        row.Cells["Status"].Value = "FOUND";
                        row.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220); // Açık yeşil
                    }
                    else
                    {
                        row.Cells["Status"].Value = "NOT FOUND";
                        row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220); // Açık sarı
                    }
                }
            }
        }

        private void AddLogMessage(string message, Color? color = null)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => AddLogMessage(message, color)));
                return;
            }

            try
            {
                string timestamp = DateTime.Now.ToString("HH:mm:ss");
                string logLine = $"[{timestamp}] {message}\n";

                // Renk ayarla
                rtbValidationLog.SelectionStart = rtbValidationLog.TextLength;
                rtbValidationLog.SelectionLength = 0;
                rtbValidationLog.SelectionColor = color ?? Color.White;
                rtbValidationLog.AppendText(logLine);

                // En alta kaydır
                rtbValidationLog.ScrollToCaret();

                // Çok fazla log satırı varsa temizle
                var lines = rtbValidationLog.Lines;
                if (lines.Length > 1000)
                {
                    var lastLines = lines.Skip(lines.Length - 500).ToArray();
                    rtbValidationLog.Lines = lastLines;
                }
            }
            catch
            {
                // Log ekleme hatası varsa sessizce devam et
            }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            rtbValidationLog.Clear();
            AddLogMessage("Log cleared", Color.Yellow);
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

                EnableValidationControls(false);
                _cloudMediaService = null;
                _validatorService = null;
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
            _connectionConfig = CreateConnectionConfig();
            _cloudMediaService = CloudMediaServiceFactory.CreateService(_currentProvider, _connectionConfig);


            _validatorService = new CloudMediaValidatorService(_cloudMediaService);
            lblConnectionStatus.Text = "✅ Connection successful";
            lblConnectionStatus.ForeColor = Color.Green;
            EnableValidationControls(true);
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

        private void EnableValidationControls(bool enabled)
        {
            grpCsvUpload.Enabled = enabled;
            btnStartValidation.Enabled = enabled && _mediaNames.Count > 0;
            btnExportResults.Enabled = enabled && _validationResults.Count > 0;
        }

        private void btnUploadCsv_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    openFileDialog.Title = "Choose Media Names CSV File";

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string csvPath = openFileDialog.FileName;

                        if (Path.GetExtension(csvPath).ToLower() != ".csv")
                        {
                            MessageBox.Show("Only files with .CSV extension can be uploaded!", "Incorrect File Format", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        AddLogMessage($"Loading CSV file: {Path.GetFileName(csvPath)}", Color.Cyan);

                        _mediaNames = _validatorService.LoadMediaNamesFromCsv(csvPath);

                        if (_mediaNames.Count > 0)
                        {
                            lblCsvStatus.Text = $"✅ {_mediaNames.Count} media names loaded from CSV";
                            lblCsvStatus.ForeColor = Color.Green;

                            // İlk birkaç medya ismini göster
                            string preview = string.Join(", ", _mediaNames.Take(3));
                            if (_mediaNames.Count > 3)
                                preview += $"... (+{_mediaNames.Count - 3} more)";

                            lblMediaPreview.Text = $"Preview: {preview}";

                            EnableValidationControls(true);

                            AddLogMessage($"Successfully loaded {_mediaNames.Count} media names from CSV", Color.Green);
                        }
                        else
                        {
                            lblCsvStatus.Text = "❌ No valid media names found in CSV";
                            lblCsvStatus.ForeColor = Color.Red;
                            AddLogMessage("No valid media names found in CSV file", Color.Red);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblCsvStatus.Text = $"❌ Error loading CSV: {ex.Message}";
                lblCsvStatus.ForeColor = Color.Red;
                AddLogMessage($"Error loading CSV: {ex.Message}", Color.Red);
                MessageBox.Show($"Error loading CSV file: {ex.Message}", "CSV Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDownloadCsvTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
                    saveFileDialog.Title = "Save Media Validation CSV Template";
                    saveFileDialog.FileName = "media_validation_template.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        CloudMediaValidatorService.SaveValidationCsvTemplate(saveFileDialog.FileName);
                        MessageBox.Show("CSV template saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving CSV template: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnStartValidation_Click(object sender, EventArgs e)
        {
            if (_mediaNames.Count == 0)
            {
                MessageBox.Show("Please upload a CSV file with media names first!", "No Media Names", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                btnStartValidation.Enabled = false;
                btnCancelValidation.Enabled = true;
                progressBarValidation.Visible = true;
                progressBarValidation.Style = ProgressBarStyle.Blocks;
                progressBarValidation.Maximum = _mediaNames.Count;
                progressBarValidation.Value = 0;

                _cancellationTokenSource = new CancellationTokenSource();
                _validationResults.Clear();
                _operationCancelled = false;

                // BindingList oluştur ve DataGridView'e bağla
                _bindingList = new BindingList<MediaValidationDto>(_validationResults);
                dgvValidationResults.DataSource = _bindingList;

                lblValidationStatus.Text = "Starting validation...";
                lblValidationStatus.ForeColor = Color.Orange;

                AddLogMessage($"Starting validation of {_mediaNames.Count} media files", Color.Yellow);

                _validationResults = await _validatorService.ValidateMediaExistenceAsync(
                    _mediaNames,
                    (processed, total, currentMedia) =>
                    {
                        // UI thread'de çalıştır
                        if (InvokeRequired)
                        {
                            Invoke(new Action(() => UpdateValidationProgress(processed, total, currentMedia)));
                        }
                        else
                        {
                            UpdateValidationProgress(processed, total, currentMedia);
                        }
                    },
                    _cancellationTokenSource.Token);

                if (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    // Sonuçları DataGridView'e bind et (zaten bağlı ama güncellemek için)
                    var bindingList = new BindingList<MediaValidationDto>(_validationResults);
                    dgvValidationResults.DataSource = bindingList;

                    int foundCount = _validationResults.Count(r => r.IsExist);
                    int totalCount = _validationResults.Count;

                    lblValidationStatus.Text = $"✅ Validation completed: {foundCount}/{totalCount} media found";
                    lblValidationStatus.ForeColor = Color.Green;

                    EnableValidationControls(true);

                    AddLogMessage($"Validation completed! Found: {foundCount}/{totalCount}", Color.Green);

                    MessageBox.Show($"Validation completed!\n\nTotal: {totalCount}\nFound: {foundCount}\nNot Found: {totalCount - foundCount}",
                        "Validation Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    await HandleCancelledValidation(_validationResults.Count);
                }
            }
            catch (OperationCanceledException)
            {
                await HandleCancelledValidation(_validationResults.Count);
            }
            catch (Exception ex)
            {
                lblValidationStatus.Text = $"❌ Validation failed: {ex.Message}";
                lblValidationStatus.ForeColor = Color.Red;
                AddLogMessage($"Validation failed: {ex.Message}", Color.Red);
                MessageBox.Show($"Validation error: {ex.Message}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnStartValidation.Enabled = true;
                btnCancelValidation.Enabled = false;
                progressBarValidation.Visible = false;
            }
        }

        private async System.Threading.Tasks.Task HandleCancelledValidation(int completedCount)
        {
            _operationCancelled = true;
            lblValidationStatus.Text = $"⚠️ Validation cancelled - {completedCount} files processed";
            lblValidationStatus.ForeColor = Color.Orange;

            AddLogMessage($"Validation cancelled - {completedCount} files were processed", Color.Orange);

            if (_validationResults.Count > 0)
            {
                var result = MessageBox.Show(
                    $"Validation was cancelled, but {_validationResults.Count} files were successfully processed.\n\nWould you like to save the partial results to Excel?",
                    "Save Partial Results?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await ExportValidationToExcelAsync();
                }
            }
            else
            {
                MessageBox.Show("Validation was cancelled and no files were processed.",
                    "Validation Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UpdateValidationProgress(int processed, int total, string currentMedia)
        {
            progressBarValidation.Value = processed;
            lblValidationStatus.Text = $"Processing {processed}/{total}: {currentMedia}";

            // Log mesajını ekle
            AddLogMessage($"Processing ({processed}/{total}): {currentMedia}", Color.White);

            // Gerçek zamanlı sonuçları göster (opsiyonel)
            this.Text = $"Cloud Media Validator - {processed}/{total} processed";

            // DataGridView'i güncelle - BindingList otomatik güncellenir
            if (_bindingList != null && dgvValidationResults.Rows.Count > 0)
            {
                try
                {
                    // Son eklenen kayıtları görünür yap
                    int lastRowIndex = dgvValidationResults.Rows.Count - 1;

                    // Auto scroll to last item
                    if (lastRowIndex >= 0)
                    {
                        dgvValidationResults.FirstDisplayedScrollingRowIndex = Math.Max(0, lastRowIndex - 10);
                        dgvValidationResults.ClearSelection();
                        dgvValidationResults.Rows[lastRowIndex].Selected = true;

                        // Son satırın rengini güncelle
                        var lastResult = _bindingList[lastRowIndex];
                        var lastRow = dgvValidationResults.Rows[lastRowIndex];

                        if (!string.IsNullOrEmpty(lastResult.ErrorMessage))
                        {
                            lastRow.Cells["Status"].Value = "ERROR";
                            lastRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
                        }
                        else if (lastResult.IsExist)
                        {
                            lastRow.Cells["Status"].Value = "FOUND";
                            lastRow.DefaultCellStyle.BackColor = Color.FromArgb(220, 255, 220);
                        }
                        else
                        {
                            lastRow.Cells["Status"].Value = "NOT FOUND";
                            lastRow.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 220);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Scroll hatası varsa log'a yaz ama sessizce devam et
                    AddLogMessage($"UI update error: {ex.Message}", Color.Red);
                }
            }

            // UI'yi zorla yenile
            dgvValidationResults.Refresh();
            Application.DoEvents();
        }

        private void btnCancelValidation_Click(object sender, EventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            AddLogMessage("Validation cancellation requested...", Color.Yellow);
        }

        private async void btnExportResults_Click(object sender, EventArgs e)
        {
            if (_validationResults.Count == 0)
            {
                MessageBox.Show("No validation results to export!", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await ExportValidationToExcelAsync();
        }

        private async System.Threading.Tasks.Task ExportValidationToExcelAsync()
        {
            try
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                    saveFileDialog.Title = "Save Validation Results";
                    saveFileDialog.FileName = $"MediaValidation_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        lblValidationStatus.Text = "Exporting to Excel...";
                        lblValidationStatus.ForeColor = Color.Orange;

                        AddLogMessage("Exporting results to Excel...", Color.Cyan);

                        await _validatorService.ExportValidationResultsToExcelAsync(_validationResults, saveFileDialog.FileName);

                        lblValidationStatus.Text = "✅ Excel export completed";
                        lblValidationStatus.ForeColor = Color.Green;

                        AddLogMessage($"Excel export completed: {Path.GetFileName(saveFileDialog.FileName)}", Color.Green);

                        var result = MessageBox.Show(
                            $"Validation results exported successfully to:\n{saveFileDialog.FileName}\n\nWould you like to open the file?",
                            "Export Complete", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(saveFileDialog.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblValidationStatus.Text = $"❌ Export failed: {ex.Message}";
                lblValidationStatus.ForeColor = Color.Red;
                AddLogMessage($"Export failed: {ex.Message}", Color.Red);
                MessageBox.Show($"Failed to export results: {ex.Message}", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Key visibility methods (copy from VideoSizeFinderForm)
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
                        var awsKeys = KeysLoaderService.LoadAwsKeysFromCsv(openFileDialog.FileName);
                        if (awsKeys != null)
                        {
                            txtAwsAccessKey.Text = awsKeys.access_key;
                            txtAwsSecretKey.Text = awsKeys.secret_access_key;
                            txtAwsBucketName.Text = awsKeys.bucket_name;
                            txtAwsRegion.Text = awsKeys.region;
                            MessageBox.Show("AWS Keys loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        var azureKeys = KeysLoaderService.LoadAzureKeysFromCsv(openFileDialog.FileName);
                        if (azureKeys != null)
                        {
                            txtAzureBlobUrl.Text = azureKeys.blob_url;
                            txtAzureSasToken.Text = azureKeys.sas_token;
                            txtAzureContainerName.Text = azureKeys.container_name;
                            txtAzureFolderPath.Text = azureKeys.folder_path;
                            MessageBox.Show("Azure Keys loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            base.OnFormClosing(e);
        }

        private void lblValidationStatus_Click(object sender, EventArgs e)
        {

        }

        private void grpValidationControl_Enter(object sender, EventArgs e)
        {

        }
    }
}