namespace CloudStorageTools.VideoSizeFinder.Components
{
    partial class VideoSizeFinderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageConnection = new System.Windows.Forms.TabPage();
            this.lblConnectionStatus = new System.Windows.Forms.Label();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.pnlAzureConfig = new System.Windows.Forms.Panel();
            this.txtAzureFolderPath = new System.Windows.Forms.TextBox();
            this.lblAzureFolderPath = new System.Windows.Forms.Label();
            this.txtAzureContainerName = new System.Windows.Forms.TextBox();
            this.lblAzureContainerName = new System.Windows.Forms.Label();
            this.txtAzureSasToken = new System.Windows.Forms.TextBox();
            this.lblAzureSasToken = new System.Windows.Forms.Label();
            this.txtAzureBlobUrl = new System.Windows.Forms.TextBox();
            this.lblAzureBlobUrl = new System.Windows.Forms.Label();
            this.pnlAwsConfig = new System.Windows.Forms.Panel();
            this.txtAwsBucketName = new System.Windows.Forms.TextBox();
            this.lblAwsBucketName = new System.Windows.Forms.Label();
            this.txtAwsRegion = new System.Windows.Forms.TextBox();
            this.lblAwsRegion = new System.Windows.Forms.Label();
            this.txtAwsSecretKey = new System.Windows.Forms.TextBox();
            this.lblAwsSecretKey = new System.Windows.Forms.Label();
            this.txtAwsAccessKey = new System.Windows.Forms.TextBox();
            this.lblAwsAccessKey = new System.Windows.Forms.Label();
            this.grpCloudProvider = new System.Windows.Forms.GroupBox();
            this.rbAzureBlob = new System.Windows.Forms.RadioButton();
            this.rbAwsS3 = new System.Windows.Forms.RadioButton();
            this.tabPageSearch = new System.Windows.Forms.TabPage();
            this.grpSearchResults = new System.Windows.Forms.GroupBox();
            this.dgvSearchResults = new System.Windows.Forms.DataGridView();
            this.chkSelectAll = new System.Windows.Forms.CheckBox();
            this.progressBarSearch = new System.Windows.Forms.ProgressBar();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.grpSearchOptions = new System.Windows.Forms.GroupBox();
            this.lblSearchHint = new System.Windows.Forms.Label();
            this.numMaxResults = new System.Windows.Forms.NumericUpDown();
            this.lblMaxResults = new System.Windows.Forms.Label();
            this.cmbExtension = new System.Windows.Forms.ComboBox();
            this.lblExtension = new System.Windows.Forms.Label();
            this.txtSearchTerm = new System.Windows.Forms.TextBox();
            this.lblSearchTerm = new System.Windows.Forms.Label();
            this.rbSearchAllMedia = new System.Windows.Forms.RadioButton();
            this.rbSearchByExtension = new System.Windows.Forms.RadioButton();
            this.rbSearchByName = new System.Windows.Forms.RadioButton();
            this.tabPageAnalysis = new System.Windows.Forms.TabPage();
            this.lblOperationStatus = new System.Windows.Forms.Label();
            this.progressBarOperation = new System.Windows.Forms.ProgressBar();
            this.grpAnalysisOptions = new System.Windows.Forms.GroupBox();
            this.lblFFmpegStatus = new System.Windows.Forms.Label();
            this.btnCancelOperation = new System.Windows.Forms.Button();
            this.btnAnalyzeSelected = new System.Windows.Forms.Button();
            this.btnDownloadSelected = new System.Windows.Forms.Button();
            this.btnVisibleKey = new System.Windows.Forms.Button();
            this.btnFillKeysFromCsv = new System.Windows.Forms.Button();
            this.btnKeysCsvTemplateDownload = new System.Windows.Forms.Button();
            this.btnAzureVisibleKey = new System.Windows.Forms.Button();
            this.btnFillAzureKeysFromCsv = new System.Windows.Forms.Button();
            this.btnAzureKeysCsvTemplateDownload = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageConnection.SuspendLayout();
            this.pnlAzureConfig.SuspendLayout();
            this.pnlAwsConfig.SuspendLayout();
            this.grpCloudProvider.SuspendLayout();
            this.tabPageSearch.SuspendLayout();
            this.grpSearchResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).BeginInit();
            this.grpSearchOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResults)).BeginInit();
            this.tabPageAnalysis.SuspendLayout();
            this.grpAnalysisOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConnection);
            this.tabControl.Controls.Add(this.tabPageSearch);
            this.tabControl.Controls.Add(this.tabPageAnalysis);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1393, 907);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageConnection
            // 
            this.tabPageConnection.Controls.Add(this.lblConnectionStatus);
            this.tabPageConnection.Controls.Add(this.btnTestConnection);
            this.tabPageConnection.Controls.Add(this.pnlAzureConfig);
            this.tabPageConnection.Controls.Add(this.pnlAwsConfig);
            this.tabPageConnection.Controls.Add(this.grpCloudProvider);
            this.tabPageConnection.Location = new System.Drawing.Point(4, 25);
            this.tabPageConnection.Name = "tabPageConnection";
            this.tabPageConnection.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConnection.Size = new System.Drawing.Size(1385, 878);
            this.tabPageConnection.TabIndex = 0;
            this.tabPageConnection.Text = "Connection";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConnectionStatus.Location = new System.Drawing.Point(190, 390);
            this.lblConnectionStatus.Name = "lblConnectionStatus";
            this.lblConnectionStatus.Size = new System.Drawing.Size(186, 18);
            this.lblConnectionStatus.TabIndex = 4;
            this.lblConnectionStatus.Text = "Please test connection first";
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.BackColor = System.Drawing.Color.LightBlue;
            this.btnTestConnection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTestConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTestConnection.Location = new System.Drawing.Point(20, 380);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(150, 40);
            this.btnTestConnection.TabIndex = 3;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = false;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // pnlAzureConfig
            // 
            this.pnlAzureConfig.Controls.Add(this.txtAzureFolderPath);
            this.pnlAzureConfig.Controls.Add(this.lblAzureFolderPath);
            this.pnlAzureConfig.Controls.Add(this.txtAzureContainerName);
            this.pnlAzureConfig.Controls.Add(this.lblAzureContainerName);
            this.pnlAzureConfig.Controls.Add(this.txtAzureSasToken);
            this.pnlAzureConfig.Controls.Add(this.lblAzureSasToken);
            this.pnlAzureConfig.Controls.Add(this.txtAzureBlobUrl);
            this.pnlAzureConfig.Controls.Add(this.lblAzureBlobUrl);
            this.pnlAzureConfig.Location = new System.Drawing.Point(20, 138);
            this.pnlAzureConfig.Name = "pnlAzureConfig";
            this.pnlAzureConfig.Size = new System.Drawing.Size(668, 161);
            this.pnlAzureConfig.TabIndex = 2;
            this.pnlAzureConfig.Visible = false;
            // 
            // txtAzureFolderPath
            // 
            this.txtAzureFolderPath.Location = new System.Drawing.Point(150, 122);
            this.txtAzureFolderPath.Name = "txtAzureFolderPath";
            this.txtAzureFolderPath.Size = new System.Drawing.Size(400, 22);
            this.txtAzureFolderPath.TabIndex = 7;
            // 
            // lblAzureFolderPath
            // 
            this.lblAzureFolderPath.AutoSize = true;
            this.lblAzureFolderPath.Location = new System.Drawing.Point(20, 125);
            this.lblAzureFolderPath.Name = "lblAzureFolderPath";
            this.lblAzureFolderPath.Size = new System.Drawing.Size(79, 16);
            this.lblAzureFolderPath.TabIndex = 6;
            this.lblAzureFolderPath.Text = "Folder Path:";
            // 
            // txtAzureContainerName
            // 
            this.txtAzureContainerName.Location = new System.Drawing.Point(150, 87);
            this.txtAzureContainerName.Name = "txtAzureContainerName";
            this.txtAzureContainerName.Size = new System.Drawing.Size(300, 22);
            this.txtAzureContainerName.TabIndex = 5;
            // 
            // lblAzureContainerName
            // 
            this.lblAzureContainerName.AutoSize = true;
            this.lblAzureContainerName.Location = new System.Drawing.Point(20, 90);
            this.lblAzureContainerName.Name = "lblAzureContainerName";
            this.lblAzureContainerName.Size = new System.Drawing.Size(107, 16);
            this.lblAzureContainerName.TabIndex = 4;
            this.lblAzureContainerName.Text = "Container Name:";
            // 
            // txtAzureSasToken
            // 
            this.txtAzureSasToken.Location = new System.Drawing.Point(150, 52);
            this.txtAzureSasToken.Name = "txtAzureSasToken";
            this.txtAzureSasToken.PasswordChar = '*';
            this.txtAzureSasToken.Size = new System.Drawing.Size(500, 22);
            this.txtAzureSasToken.TabIndex = 3;
            // 
            // lblAzureSasToken
            // 
            this.lblAzureSasToken.AutoSize = true;
            this.lblAzureSasToken.Location = new System.Drawing.Point(20, 55);
            this.lblAzureSasToken.Name = "lblAzureSasToken";
            this.lblAzureSasToken.Size = new System.Drawing.Size(79, 16);
            this.lblAzureSasToken.TabIndex = 2;
            this.lblAzureSasToken.Text = "SAS Token:";
            // 
            // txtAzureBlobUrl
            // 
            this.txtAzureBlobUrl.Location = new System.Drawing.Point(150, 17);
            this.txtAzureBlobUrl.Name = "txtAzureBlobUrl";
            this.txtAzureBlobUrl.Size = new System.Drawing.Size(500, 22);
            this.txtAzureBlobUrl.TabIndex = 1;
            // 
            // lblAzureBlobUrl
            // 
            this.lblAzureBlobUrl.AutoSize = true;
            this.lblAzureBlobUrl.Location = new System.Drawing.Point(20, 20);
            this.lblAzureBlobUrl.Name = "lblAzureBlobUrl";
            this.lblAzureBlobUrl.Size = new System.Drawing.Size(68, 16);
            this.lblAzureBlobUrl.TabIndex = 0;
            this.lblAzureBlobUrl.Text = "Blob URL:";
            // 
            // pnlAwsConfig
            // 
            this.pnlAwsConfig.Controls.Add(this.txtAwsBucketName);
            this.pnlAwsConfig.Controls.Add(this.lblAwsBucketName);
            this.pnlAwsConfig.Controls.Add(this.txtAwsRegion);
            this.pnlAwsConfig.Controls.Add(this.lblAwsRegion);
            this.pnlAwsConfig.Controls.Add(this.txtAwsSecretKey);
            this.pnlAwsConfig.Controls.Add(this.lblAwsSecretKey);
            this.pnlAwsConfig.Controls.Add(this.txtAwsAccessKey);
            this.pnlAwsConfig.Controls.Add(this.lblAwsAccessKey);
            this.pnlAwsConfig.Location = new System.Drawing.Point(20, 138);
            this.pnlAwsConfig.Name = "pnlAwsConfig";
            this.pnlAwsConfig.Size = new System.Drawing.Size(683, 223);
            this.pnlAwsConfig.TabIndex = 1;
            this.pnlAwsConfig.Visible = false;
            // 
            // txtAwsBucketName
            // 
            this.txtAwsBucketName.Location = new System.Drawing.Point(150, 122);
            this.txtAwsBucketName.Name = "txtAwsBucketName";
            this.txtAwsBucketName.Size = new System.Drawing.Size(300, 22);
            this.txtAwsBucketName.TabIndex = 7;
            // 
            // lblAwsBucketName
            // 
            this.lblAwsBucketName.AutoSize = true;
            this.lblAwsBucketName.Location = new System.Drawing.Point(20, 125);
            this.lblAwsBucketName.Name = "lblAwsBucketName";
            this.lblAwsBucketName.Size = new System.Drawing.Size(91, 16);
            this.lblAwsBucketName.TabIndex = 6;
            this.lblAwsBucketName.Text = "Bucket Name:";
            // 
            // txtAwsRegion
            // 
            this.txtAwsRegion.Location = new System.Drawing.Point(150, 87);
            this.txtAwsRegion.Name = "txtAwsRegion";
            this.txtAwsRegion.Size = new System.Drawing.Size(200, 22);
            this.txtAwsRegion.TabIndex = 5;
            // 
            // lblAwsRegion
            // 
            this.lblAwsRegion.AutoSize = true;
            this.lblAwsRegion.Location = new System.Drawing.Point(20, 90);
            this.lblAwsRegion.Name = "lblAwsRegion";
            this.lblAwsRegion.Size = new System.Drawing.Size(54, 16);
            this.lblAwsRegion.TabIndex = 4;
            this.lblAwsRegion.Text = "Region:";
            // 
            // txtAwsSecretKey
            // 
            this.txtAwsSecretKey.Location = new System.Drawing.Point(150, 52);
            this.txtAwsSecretKey.Name = "txtAwsSecretKey";
            this.txtAwsSecretKey.PasswordChar = '*';
            this.txtAwsSecretKey.Size = new System.Drawing.Size(400, 22);
            this.txtAwsSecretKey.TabIndex = 3;
            // 
            // lblAwsSecretKey
            // 
            this.lblAwsSecretKey.AutoSize = true;
            this.lblAwsSecretKey.Location = new System.Drawing.Point(20, 55);
            this.lblAwsSecretKey.Name = "lblAwsSecretKey";
            this.lblAwsSecretKey.Size = new System.Drawing.Size(75, 16);
            this.lblAwsSecretKey.TabIndex = 2;
            this.lblAwsSecretKey.Text = "Secret Key:";
            // 
            // txtAwsAccessKey
            // 
            this.txtAwsAccessKey.Location = new System.Drawing.Point(150, 17);
            this.txtAwsAccessKey.Name = "txtAwsAccessKey";
            this.txtAwsAccessKey.PasswordChar = '*';
            this.txtAwsAccessKey.Size = new System.Drawing.Size(400, 22);
            this.txtAwsAccessKey.TabIndex = 1;
            // 
            // lblAwsAccessKey
            // 
            this.lblAwsAccessKey.AutoSize = true;
            this.lblAwsAccessKey.Location = new System.Drawing.Point(20, 20);
            this.lblAwsAccessKey.Name = "lblAwsAccessKey";
            this.lblAwsAccessKey.Size = new System.Drawing.Size(81, 16);
            this.lblAwsAccessKey.TabIndex = 0;
            this.lblAwsAccessKey.Text = "Access Key:";
            // 
            // grpCloudProvider
            // 
            this.grpCloudProvider.Controls.Add(this.rbAzureBlob);
            this.grpCloudProvider.Controls.Add(this.rbAwsS3);
            this.grpCloudProvider.Location = new System.Drawing.Point(20, 20);
            this.grpCloudProvider.Name = "grpCloudProvider";
            this.grpCloudProvider.Size = new System.Drawing.Size(300, 100);
            this.grpCloudProvider.TabIndex = 0;
            this.grpCloudProvider.TabStop = false;
            this.grpCloudProvider.Text = "Cloud Provider";
            // 
            // rbAzureBlob
            // 
            this.rbAzureBlob.AutoSize = true;
            this.rbAzureBlob.Location = new System.Drawing.Point(20, 60);
            this.rbAzureBlob.Name = "rbAzureBlob";
            this.rbAzureBlob.Size = new System.Drawing.Size(144, 20);
            this.rbAzureBlob.TabIndex = 1;
            this.rbAzureBlob.TabStop = true;
            this.rbAzureBlob.Text = "Azure Blob Storage";
            this.rbAzureBlob.UseVisualStyleBackColor = true;
            // 
            // rbAwsS3
            // 
            this.rbAwsS3.AutoSize = true;
            this.rbAwsS3.Location = new System.Drawing.Point(20, 30);
            this.rbAwsS3.Name = "rbAwsS3";
            this.rbAwsS3.Size = new System.Drawing.Size(78, 20);
            this.rbAwsS3.TabIndex = 0;
            this.rbAwsS3.TabStop = true;
            this.rbAwsS3.Text = "AWS S3";
            this.rbAwsS3.UseVisualStyleBackColor = true;
            // 
            // tabPageSearch
            // 
            this.tabPageSearch.Controls.Add(this.grpSearchResults);
            this.tabPageSearch.Controls.Add(this.progressBarSearch);
            this.tabPageSearch.Controls.Add(this.lblSearchStatus);
            this.tabPageSearch.Controls.Add(this.btnSearch);
            this.tabPageSearch.Controls.Add(this.grpSearchOptions);
            this.tabPageSearch.Location = new System.Drawing.Point(4, 25);
            this.tabPageSearch.Name = "tabPageSearch";
            this.tabPageSearch.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSearch.Size = new System.Drawing.Size(1385, 878);
            this.tabPageSearch.TabIndex = 1;
            this.tabPageSearch.Text = "Search Media";
            this.tabPageSearch.UseVisualStyleBackColor = true;
            // 
            // grpSearchResults
            // 
            this.grpSearchResults.Controls.Add(this.dgvSearchResults);
            this.grpSearchResults.Controls.Add(this.chkSelectAll);
            this.grpSearchResults.Location = new System.Drawing.Point(20, 343);
            this.grpSearchResults.Name = "grpSearchResults";
            this.grpSearchResults.Size = new System.Drawing.Size(1344, 527);
            this.grpSearchResults.TabIndex = 4;
            this.grpSearchResults.TabStop = false;
            this.grpSearchResults.Text = "Search Results";
            // 
            // dgvSearchResults
            // 
            this.dgvSearchResults.AllowUserToAddRows = false;
            this.dgvSearchResults.AllowUserToDeleteRows = false;
            this.dgvSearchResults.AllowUserToOrderColumns = true;
            this.dgvSearchResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSearchResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSearchResults.Location = new System.Drawing.Point(20, 55);
            this.dgvSearchResults.Name = "dgvSearchResults";
            this.dgvSearchResults.RowHeadersWidth = 51;
            this.dgvSearchResults.RowTemplate.Height = 24;
            this.dgvSearchResults.Size = new System.Drawing.Size(1318, 466);
            this.dgvSearchResults.TabIndex = 1;
            // 
            // chkSelectAll
            // 
            this.chkSelectAll.AutoSize = true;
            this.chkSelectAll.Enabled = false;
            this.chkSelectAll.Location = new System.Drawing.Point(20, 25);
            this.chkSelectAll.Name = "chkSelectAll";
            this.chkSelectAll.Size = new System.Drawing.Size(85, 20);
            this.chkSelectAll.TabIndex = 0;
            this.chkSelectAll.Text = "Select All";
            this.chkSelectAll.UseVisualStyleBackColor = true;
            this.chkSelectAll.CheckedChanged += new System.EventHandler(this.chkSelectAll_CheckedChanged);
            // 
            // progressBarSearch
            // 
            this.progressBarSearch.Location = new System.Drawing.Point(20, 300);
            this.progressBarSearch.Name = "progressBarSearch";
            this.progressBarSearch.Size = new System.Drawing.Size(400, 23);
            this.progressBarSearch.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBarSearch.TabIndex = 3;
            this.progressBarSearch.Visible = false;
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSearchStatus.Location = new System.Drawing.Point(160, 250);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(172, 18);
            this.lblSearchStatus.TabIndex = 2;
            this.lblSearchStatus.Text = "Ready to search media...";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.LightGreen;
            this.btnSearch.Enabled = false;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.Location = new System.Drawing.Point(20, 240);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(120, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // grpSearchOptions
            // 
            this.grpSearchOptions.Controls.Add(this.lblSearchHint);
            this.grpSearchOptions.Controls.Add(this.numMaxResults);
            this.grpSearchOptions.Controls.Add(this.lblMaxResults);
            this.grpSearchOptions.Controls.Add(this.cmbExtension);
            this.grpSearchOptions.Controls.Add(this.lblExtension);
            this.grpSearchOptions.Controls.Add(this.txtSearchTerm);
            this.grpSearchOptions.Controls.Add(this.lblSearchTerm);
            this.grpSearchOptions.Controls.Add(this.rbSearchAllMedia);
            this.grpSearchOptions.Controls.Add(this.rbSearchByExtension);
            this.grpSearchOptions.Controls.Add(this.rbSearchByName);
            this.grpSearchOptions.Enabled = false;
            this.grpSearchOptions.Location = new System.Drawing.Point(20, 20);
            this.grpSearchOptions.Name = "grpSearchOptions";
            this.grpSearchOptions.Size = new System.Drawing.Size(625, 200);
            this.grpSearchOptions.TabIndex = 0;
            this.grpSearchOptions.TabStop = false;
            this.grpSearchOptions.Text = "Search Options";
            // 
            // lblSearchHint
            // 
            this.lblSearchHint.AutoSize = true;
            this.lblSearchHint.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.lblSearchHint.ForeColor = System.Drawing.Color.Gray;
            this.lblSearchHint.Location = new System.Drawing.Point(20, 160);
            this.lblSearchHint.Name = "lblSearchHint";
            this.lblSearchHint.Size = new System.Drawing.Size(353, 17);
            this.lblSearchHint.TabIndex = 9;
            this.lblSearchHint.Text = "Enter media name to search (partial names supported)";
            // 
            // numMaxResults
            // 
            this.numMaxResults.Location = new System.Drawing.Point(300, 123);
            this.numMaxResults.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numMaxResults.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxResults.Name = "numMaxResults";
            this.numMaxResults.Size = new System.Drawing.Size(120, 22);
            this.numMaxResults.TabIndex = 8;
            this.numMaxResults.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // lblMaxResults
            // 
            this.lblMaxResults.AutoSize = true;
            this.lblMaxResults.Location = new System.Drawing.Point(200, 125);
            this.lblMaxResults.Name = "lblMaxResults";
            this.lblMaxResults.Size = new System.Drawing.Size(83, 16);
            this.lblMaxResults.TabIndex = 7;
            this.lblMaxResults.Text = "Max Results:";
            // 
            // cmbExtension
            // 
            this.cmbExtension.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbExtension.Enabled = false;
            this.cmbExtension.FormattingEnabled = true;
            this.cmbExtension.Items.AddRange(new object[] {
            ".jpg",
            ".jpeg",
            ".png",
            ".gif",
            ".bmp",
            ".webp",
            ".mp4",
            ".avi",
            ".mov",
            ".wmv",
            ".flv",
            ".webm",
            ".mkv",
            ".mp3",
            ".wav",
            ".flac",
            ".aac",
            ".ogg",
            ".m3u8",
            ".ts"});
            this.cmbExtension.Location = new System.Drawing.Point(300, 59);
            this.cmbExtension.Name = "cmbExtension";
            this.cmbExtension.Size = new System.Drawing.Size(150, 24);
            this.cmbExtension.TabIndex = 6;
            // 
            // lblExtension
            // 
            this.lblExtension.AutoSize = true;
            this.lblExtension.Location = new System.Drawing.Point(200, 62);
            this.lblExtension.Name = "lblExtension";
            this.lblExtension.Size = new System.Drawing.Size(68, 16);
            this.lblExtension.TabIndex = 5;
            this.lblExtension.Text = "Extension:";
            // 
            // txtSearchTerm
            // 
            this.txtSearchTerm.Location = new System.Drawing.Point(300, 29);
            this.txtSearchTerm.Name = "txtSearchTerm";
            this.txtSearchTerm.Size = new System.Drawing.Size(300, 22);
            this.txtSearchTerm.TabIndex = 4;
            // 
            // lblSearchTerm
            // 
            this.lblSearchTerm.AutoSize = true;
            this.lblSearchTerm.Location = new System.Drawing.Point(200, 32);
            this.lblSearchTerm.Name = "lblSearchTerm";
            this.lblSearchTerm.Size = new System.Drawing.Size(88, 16);
            this.lblSearchTerm.TabIndex = 3;
            this.lblSearchTerm.Text = "Search Term:";
            // 
            // rbSearchAllMedia
            // 
            this.rbSearchAllMedia.AutoSize = true;
            this.rbSearchAllMedia.Location = new System.Drawing.Point(20, 90);
            this.rbSearchAllMedia.Name = "rbSearchAllMedia";
            this.rbSearchAllMedia.Size = new System.Drawing.Size(116, 20);
            this.rbSearchAllMedia.TabIndex = 2;
            this.rbSearchAllMedia.Text = "All Media Files";
            this.rbSearchAllMedia.UseVisualStyleBackColor = true;
            // 
            // rbSearchByExtension
            // 
            this.rbSearchByExtension.AutoSize = true;
            this.rbSearchByExtension.Location = new System.Drawing.Point(20, 60);
            this.rbSearchByExtension.Name = "rbSearchByExtension";
            this.rbSearchByExtension.Size = new System.Drawing.Size(150, 20);
            this.rbSearchByExtension.TabIndex = 1;
            this.rbSearchByExtension.Text = "Search by Extension";
            this.rbSearchByExtension.UseVisualStyleBackColor = true;
            // 
            // rbSearchByName
            // 
            this.rbSearchByName.AutoSize = true;
            this.rbSearchByName.Checked = true;
            this.rbSearchByName.Location = new System.Drawing.Point(20, 30);
            this.rbSearchByName.Name = "rbSearchByName";
            this.rbSearchByName.Size = new System.Drawing.Size(129, 20);
            this.rbSearchByName.TabIndex = 0;
            this.rbSearchByName.TabStop = true;
            this.rbSearchByName.Text = "Search by Name";
            this.rbSearchByName.UseVisualStyleBackColor = true;
            // 
            // tabPageAnalysis
            // 
            this.tabPageAnalysis.Controls.Add(this.lblOperationStatus);
            this.tabPageAnalysis.Controls.Add(this.progressBarOperation);
            this.tabPageAnalysis.Controls.Add(this.grpAnalysisOptions);
            this.tabPageAnalysis.Location = new System.Drawing.Point(4, 25);
            this.tabPageAnalysis.Name = "tabPageAnalysis";
            this.tabPageAnalysis.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAnalysis.Size = new System.Drawing.Size(1385, 878);
            this.tabPageAnalysis.TabIndex = 2;
            this.tabPageAnalysis.Text = "Analysis & Download";
            this.tabPageAnalysis.UseVisualStyleBackColor = true;
            // 
            // lblOperationStatus
            // 
            this.lblOperationStatus.AutoSize = true;
            this.lblOperationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblOperationStatus.Location = new System.Drawing.Point(635, 245);
            this.lblOperationStatus.Name = "lblOperationStatus";
            this.lblOperationStatus.Size = new System.Drawing.Size(197, 18);
            this.lblOperationStatus.TabIndex = 2;
            this.lblOperationStatus.Text = "Ready for analysis/download";
            // 
            // progressBarOperation
            // 
            this.progressBarOperation.Location = new System.Drawing.Point(20, 240);
            this.progressBarOperation.Name = "progressBarOperation";
            this.progressBarOperation.Size = new System.Drawing.Size(600, 30);
            this.progressBarOperation.TabIndex = 1;
            this.progressBarOperation.Visible = false;
            // 
            // grpAnalysisOptions
            // 
            this.grpAnalysisOptions.Controls.Add(this.lblFFmpegStatus);
            this.grpAnalysisOptions.Controls.Add(this.btnCancelOperation);
            this.grpAnalysisOptions.Controls.Add(this.btnAnalyzeSelected);
            this.grpAnalysisOptions.Controls.Add(this.btnDownloadSelected);
            this.grpAnalysisOptions.Location = new System.Drawing.Point(20, 20);
            this.grpAnalysisOptions.Name = "grpAnalysisOptions";
            this.grpAnalysisOptions.Size = new System.Drawing.Size(1150, 200);
            this.grpAnalysisOptions.TabIndex = 0;
            this.grpAnalysisOptions.TabStop = false;
            this.grpAnalysisOptions.Text = "Analysis & Download Options";
            // 
            // lblFFmpegStatus
            // 
            this.lblFFmpegStatus.AutoSize = true;
            this.lblFFmpegStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblFFmpegStatus.Location = new System.Drawing.Point(20, 110);
            this.lblFFmpegStatus.Name = "lblFFmpegStatus";
            this.lblFFmpegStatus.Size = new System.Drawing.Size(141, 18);
            this.lblFFmpegStatus.TabIndex = 3;
            this.lblFFmpegStatus.Text = "Checking FFmpeg...";
            // 
            // btnCancelOperation
            // 
            this.btnCancelOperation.BackColor = System.Drawing.Color.Red;
            this.btnCancelOperation.Enabled = false;
            this.btnCancelOperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelOperation.ForeColor = System.Drawing.Color.White;
            this.btnCancelOperation.Location = new System.Drawing.Point(517, 40);
            this.btnCancelOperation.Name = "btnCancelOperation";
            this.btnCancelOperation.Size = new System.Drawing.Size(100, 45);
            this.btnCancelOperation.TabIndex = 2;
            this.btnCancelOperation.Text = "Cancel";
            this.btnCancelOperation.UseVisualStyleBackColor = false;
            this.btnCancelOperation.Click += new System.EventHandler(this.btnCancelOperation_Click);
            // 
            // btnAnalyzeSelected
            // 
            this.btnAnalyzeSelected.BackColor = System.Drawing.Color.Orange;
            this.btnAnalyzeSelected.Enabled = false;
            this.btnAnalyzeSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnalyzeSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnAnalyzeSelected.Location = new System.Drawing.Point(247, 40);
            this.btnAnalyzeSelected.Name = "btnAnalyzeSelected";
            this.btnAnalyzeSelected.Size = new System.Drawing.Size(252, 45);
            this.btnAnalyzeSelected.TabIndex = 1;
            this.btnAnalyzeSelected.Text = "Analyze && Export to Excel";
            this.btnAnalyzeSelected.UseVisualStyleBackColor = false;
            this.btnAnalyzeSelected.Click += new System.EventHandler(this.btnAnalyzeSelected_Click);
            // 
            // btnDownloadSelected
            // 
            this.btnDownloadSelected.BackColor = System.Drawing.Color.LightBlue;
            this.btnDownloadSelected.Enabled = false;
            this.btnDownloadSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadSelected.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadSelected.Location = new System.Drawing.Point(20, 40);
            this.btnDownloadSelected.Name = "btnDownloadSelected";
            this.btnDownloadSelected.Size = new System.Drawing.Size(209, 45);
            this.btnDownloadSelected.TabIndex = 0;
            this.btnDownloadSelected.Text = "Download Selected";
            this.btnDownloadSelected.UseVisualStyleBackColor = false;
            this.btnDownloadSelected.Click += new System.EventHandler(this.btnDownloadSelected_Click);
            // 
            // btnVisibleKey
            // 
            this.btnVisibleKey.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnVisibleKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnVisibleKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnVisibleKey.Location = new System.Drawing.Point(570, 17);
            this.btnVisibleKey.Name = "btnVisibleKey";
            this.btnVisibleKey.Size = new System.Drawing.Size(53, 52);
            this.btnVisibleKey.TabIndex = 14;
            this.btnVisibleKey.Text = "👁️";
            this.btnVisibleKey.UseVisualStyleBackColor = false;
            this.btnVisibleKey.Click += new System.EventHandler(this.btnVisibleKey_Click);
            // 
            // btnFillKeysFromCsv
            // 
            this.btnFillKeysFromCsv.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFillKeysFromCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillKeysFromCsv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnFillKeysFromCsv.Location = new System.Drawing.Point(20, 230);
            this.btnFillKeysFromCsv.Name = "btnFillKeysFromCsv";
            this.btnFillKeysFromCsv.Size = new System.Drawing.Size(170, 37);
            this.btnFillKeysFromCsv.TabIndex = 17;
            this.btnFillKeysFromCsv.Text = "Fill Keys From Csv";
            this.btnFillKeysFromCsv.UseVisualStyleBackColor = false;
            this.btnFillKeysFromCsv.Click += new System.EventHandler(this.btnFillKeysFromCsv_Click);
            // 
            // btnKeysCsvTemplateDownload
            // 
            this.btnKeysCsvTemplateDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnKeysCsvTemplateDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKeysCsvTemplateDownload.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnKeysCsvTemplateDownload.Location = new System.Drawing.Point(210, 230);
            this.btnKeysCsvTemplateDownload.Name = "btnKeysCsvTemplateDownload";
            this.btnKeysCsvTemplateDownload.Size = new System.Drawing.Size(250, 37);
            this.btnKeysCsvTemplateDownload.TabIndex = 18;
            this.btnKeysCsvTemplateDownload.Text = "Download Keys Template";
            this.btnKeysCsvTemplateDownload.UseVisualStyleBackColor = false;
            this.btnKeysCsvTemplateDownload.Click += new System.EventHandler(this.btnKeysCsvTemplateDownload_Click);
            // 
            // btnAzureVisibleKey
            // 
            this.btnAzureVisibleKey.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btnAzureVisibleKey.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnAzureVisibleKey.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.btnAzureVisibleKey.Location = new System.Drawing.Point(670, 17);
            this.btnAzureVisibleKey.Name = "btnAzureVisibleKey";
            this.btnAzureVisibleKey.Size = new System.Drawing.Size(53, 52);
            this.btnAzureVisibleKey.TabIndex = 14;
            this.btnAzureVisibleKey.Text = "👁️";
            this.btnAzureVisibleKey.UseVisualStyleBackColor = false;
            this.btnAzureVisibleKey.Click += new System.EventHandler(this.btnAzureVisibleKey_Click);
            // 
            // btnFillAzureKeysFromCsv
            // 
            this.btnFillAzureKeysFromCsv.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFillAzureKeysFromCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillAzureKeysFromCsv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnFillAzureKeysFromCsv.Location = new System.Drawing.Point(20, 230);
            this.btnFillAzureKeysFromCsv.Name = "btnFillAzureKeysFromCsv";
            this.btnFillAzureKeysFromCsv.Size = new System.Drawing.Size(170, 37);
            this.btnFillAzureKeysFromCsv.TabIndex = 17;
            this.btnFillAzureKeysFromCsv.Text = "Fill Keys From Csv";
            this.btnFillAzureKeysFromCsv.UseVisualStyleBackColor = false;
            this.btnFillAzureKeysFromCsv.Click += new System.EventHandler(this.btnFillAzureKeysFromCsv_Click);
            // 
            // btnAzureKeysCsvTemplateDownload
            // 
            this.btnAzureKeysCsvTemplateDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAzureKeysCsvTemplateDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAzureKeysCsvTemplateDownload.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnAzureKeysCsvTemplateDownload.Location = new System.Drawing.Point(210, 230);
            this.btnAzureKeysCsvTemplateDownload.Name = "btnAzureKeysCsvTemplateDownload";
            this.btnAzureKeysCsvTemplateDownload.Size = new System.Drawing.Size(250, 37);
            this.btnAzureKeysCsvTemplateDownload.TabIndex = 18;
            this.btnAzureKeysCsvTemplateDownload.Text = "Download Keys Template";
            this.btnAzureKeysCsvTemplateDownload.UseVisualStyleBackColor = false;
            this.btnAzureKeysCsvTemplateDownload.Click += new System.EventHandler(this.btnAzureKeysCsvTemplateDownload_Click);
            // 
            // VideoSizeFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 907);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "VideoSizeFinderForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud Media Size Finder & Analyzer";
            this.tabControl.ResumeLayout(false);
            this.tabPageConnection.ResumeLayout(false);
            this.tabPageConnection.PerformLayout();
            this.pnlAzureConfig.ResumeLayout(false);
            this.pnlAzureConfig.PerformLayout();
            this.pnlAwsConfig.ResumeLayout(false);
            this.pnlAwsConfig.PerformLayout();
            this.grpCloudProvider.ResumeLayout(false);
            this.grpCloudProvider.PerformLayout();
            this.tabPageSearch.ResumeLayout(false);
            this.tabPageSearch.PerformLayout();
            this.grpSearchResults.ResumeLayout(false);
            this.grpSearchResults.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSearchResults)).EndInit();
            this.grpSearchOptions.ResumeLayout(false);
            this.grpSearchOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxResults)).EndInit();
            this.tabPageAnalysis.ResumeLayout(false);
            this.tabPageAnalysis.PerformLayout();
            this.grpAnalysisOptions.ResumeLayout(false);
            this.grpAnalysisOptions.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TabPage tabPageAnalysis;

        // Connection Tab Controls
        private System.Windows.Forms.GroupBox grpCloudProvider;
        private System.Windows.Forms.RadioButton rbAwsS3;
        private System.Windows.Forms.RadioButton rbAzureBlob;
        private System.Windows.Forms.Panel pnlAwsConfig;
        private System.Windows.Forms.Label lblAwsAccessKey;
        private System.Windows.Forms.TextBox txtAwsAccessKey;
        private System.Windows.Forms.Label lblAwsSecretKey;
        private System.Windows.Forms.TextBox txtAwsSecretKey;
        private System.Windows.Forms.Label lblAwsRegion;
        private System.Windows.Forms.TextBox txtAwsRegion;
        private System.Windows.Forms.Label lblAwsBucketName;
        private System.Windows.Forms.TextBox txtAwsBucketName;
        private System.Windows.Forms.Panel pnlAzureConfig;
        private System.Windows.Forms.Label lblAzureBlobUrl;
        private System.Windows.Forms.TextBox txtAzureBlobUrl;
        private System.Windows.Forms.Label lblAzureSasToken;
        private System.Windows.Forms.TextBox txtAzureSasToken;
        private System.Windows.Forms.Label lblAzureContainerName;
        private System.Windows.Forms.TextBox txtAzureContainerName;
        private System.Windows.Forms.Label lblAzureFolderPath;
        private System.Windows.Forms.TextBox txtAzureFolderPath;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Label lblConnectionStatus;

        // Search Tab Controls
        private System.Windows.Forms.GroupBox grpSearchOptions;
        private System.Windows.Forms.RadioButton rbSearchByName;
        private System.Windows.Forms.RadioButton rbSearchByExtension;
        private System.Windows.Forms.RadioButton rbSearchAllMedia;
        private System.Windows.Forms.Label lblSearchTerm;
        private System.Windows.Forms.TextBox txtSearchTerm;
        private System.Windows.Forms.Label lblExtension;
        private System.Windows.Forms.ComboBox cmbExtension;
        private System.Windows.Forms.Label lblMaxResults;
        private System.Windows.Forms.NumericUpDown numMaxResults;
        private System.Windows.Forms.Label lblSearchHint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.ProgressBar progressBarSearch;
        private System.Windows.Forms.GroupBox grpSearchResults;
        private System.Windows.Forms.CheckBox chkSelectAll;
        private System.Windows.Forms.DataGridView dgvSearchResults;

        // Analysis Tab Controls
        private System.Windows.Forms.GroupBox grpAnalysisOptions;
        private System.Windows.Forms.Button btnDownloadSelected;
        private System.Windows.Forms.Button btnAnalyzeSelected;
        private System.Windows.Forms.Button btnCancelOperation;
        private System.Windows.Forms.Label lblFFmpegStatus;
        private System.Windows.Forms.ProgressBar progressBarOperation;
        private System.Windows.Forms.Label lblOperationStatus;

        // AWS Panel'e eklenecek yeni kontroller
        private System.Windows.Forms.Button btnVisibleKey;
        private System.Windows.Forms.Button btnFillKeysFromCsv;
        private System.Windows.Forms.Button btnKeysCsvTemplateDownload;

        // Azure Panel'e eklenecek yeni kontroller  
        private System.Windows.Forms.Button btnAzureVisibleKey;
        private System.Windows.Forms.Button btnFillAzureKeysFromCsv;
        private System.Windows.Forms.Button btnAzureKeysCsvTemplateDownload;
    }
}