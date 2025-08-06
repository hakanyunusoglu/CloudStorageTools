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
            this.btnAzureKeysCsvTemplateDownload = new System.Windows.Forms.Button();
            this.btnFillAzureKeysFromCsv = new System.Windows.Forms.Button();
            this.btnAzureVisibleKey = new System.Windows.Forms.Button();
            this.pnlAwsConfig = new System.Windows.Forms.Panel();
            this.txtAwsBucketName = new System.Windows.Forms.TextBox();
            this.lblAwsBucketName = new System.Windows.Forms.Label();
            this.txtAwsRegion = new System.Windows.Forms.TextBox();
            this.lblAwsRegion = new System.Windows.Forms.Label();
            this.txtAwsSecretKey = new System.Windows.Forms.TextBox();
            this.lblAwsSecretKey = new System.Windows.Forms.Label();
            this.txtAwsAccessKey = new System.Windows.Forms.TextBox();
            this.lblAwsAccessKey = new System.Windows.Forms.Label();
            this.btnKeysCsvTemplateDownload = new System.Windows.Forms.Button();
            this.btnFillKeysFromCsv = new System.Windows.Forms.Button();
            this.btnVisibleKey = new System.Windows.Forms.Button();
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
            this.tabPageMediaResizer = new System.Windows.Forms.TabPage();
            this.grpResizerResults = new System.Windows.Forms.GroupBox();
            this.dgvResizerResults = new System.Windows.Forms.DataGridView();
            this.grpResizerControl = new System.Windows.Forms.GroupBox();
            this.lblResizeStatus = new System.Windows.Forms.Label();
            this.progressBarResize = new System.Windows.Forms.ProgressBar();
            this.btnExportResizerResults = new System.Windows.Forms.Button();
            this.btnCancelResize = new System.Windows.Forms.Button();
            this.btnStartResize = new System.Windows.Forms.Button();
            this.grpResizerUpload = new System.Windows.Forms.GroupBox();
            this.lblResizerStatus = new System.Windows.Forms.Label();
            this.btnDownloadResizerTemplate = new System.Windows.Forms.Button();
            this.btnUploadMediaList = new System.Windows.Forms.Button();
            this.lblResizerInstructions = new System.Windows.Forms.Label();
            this.grpUrlCreation = new System.Windows.Forms.GroupBox();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.lblToken = new System.Windows.Forms.Label();
            this.txtBaseUrl = new System.Windows.Forms.TextBox();
            this.lblBaseUrl = new System.Windows.Forms.Label();
            this.grpUrlMode = new System.Windows.Forms.GroupBox();
            this.rbCreateUrl = new System.Windows.Forms.RadioButton();
            this.rbUrlAlreadyExists = new System.Windows.Forms.RadioButton();
            this.grpResizerSettings = new System.Windows.Forms.GroupBox();
            this.cmbResizeMode = new System.Windows.Forms.ComboBox();
            this.lblResizeMode = new System.Windows.Forms.Label();
            this.chkMaintainAspectRatio = new System.Windows.Forms.CheckBox();
            this.numQuality = new System.Windows.Forms.NumericUpDown();
            this.lblQuality = new System.Windows.Forms.Label();
            this.numTargetHeight = new System.Windows.Forms.NumericUpDown();
            this.numTargetWidth = new System.Windows.Forms.NumericUpDown();
            this.lblTargetDimensions = new System.Windows.Forms.Label();
            this.numMaxHeight = new System.Windows.Forms.NumericUpDown();
            this.numMaxWidth = new System.Windows.Forms.NumericUpDown();
            this.lblMaxDimensions = new System.Windows.Forms.Label();
            this.numMaxFileSize = new System.Windows.Forms.NumericUpDown();
            this.lblMaxFileSize = new System.Windows.Forms.Label();
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
            this.tabPageMediaResizer.SuspendLayout();
            this.grpResizerResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResizerResults)).BeginInit();
            this.grpResizerControl.SuspendLayout();
            this.grpResizerUpload.SuspendLayout();
            this.grpUrlCreation.SuspendLayout();
            this.grpUrlMode.SuspendLayout();
            this.grpResizerSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConnection);
            this.tabControl.Controls.Add(this.tabPageSearch);
            this.tabControl.Controls.Add(this.tabPageAnalysis);
            this.tabControl.Controls.Add(this.tabPageMediaResizer);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1383, 883);
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
            this.tabPageConnection.Size = new System.Drawing.Size(1375, 912);
            this.tabPageConnection.TabIndex = 0;
            this.tabPageConnection.Text = "Connection";
            this.tabPageConnection.UseVisualStyleBackColor = true;
            // 
            // lblConnectionStatus
            // 
            this.lblConnectionStatus.AutoSize = true;
            this.lblConnectionStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblConnectionStatus.Location = new System.Drawing.Point(251, 391);
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
            this.btnTestConnection.Location = new System.Drawing.Point(20, 381);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(190, 40);
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
            this.pnlAzureConfig.Controls.Add(this.btnAzureKeysCsvTemplateDownload);
            this.pnlAzureConfig.Controls.Add(this.btnFillAzureKeysFromCsv);
            this.pnlAzureConfig.Controls.Add(this.btnAzureVisibleKey);
            this.pnlAzureConfig.Location = new System.Drawing.Point(20, 138);
            this.pnlAzureConfig.Name = "pnlAzureConfig";
            this.pnlAzureConfig.Size = new System.Drawing.Size(746, 224);
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
            // btnAzureKeysCsvTemplateDownload
            // 
            this.btnAzureKeysCsvTemplateDownload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnAzureKeysCsvTemplateDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAzureKeysCsvTemplateDownload.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnAzureKeysCsvTemplateDownload.Location = new System.Drawing.Point(210, 175);
            this.btnAzureKeysCsvTemplateDownload.Name = "btnAzureKeysCsvTemplateDownload";
            this.btnAzureKeysCsvTemplateDownload.Size = new System.Drawing.Size(250, 37);
            this.btnAzureKeysCsvTemplateDownload.TabIndex = 18;
            this.btnAzureKeysCsvTemplateDownload.Text = "Download Keys Template";
            this.btnAzureKeysCsvTemplateDownload.UseVisualStyleBackColor = false;
            this.btnAzureKeysCsvTemplateDownload.Click += new System.EventHandler(this.btnAzureKeysCsvTemplateDownload_Click);
            // 
            // btnFillAzureKeysFromCsv
            // 
            this.btnFillAzureKeysFromCsv.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btnFillAzureKeysFromCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillAzureKeysFromCsv.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F);
            this.btnFillAzureKeysFromCsv.Location = new System.Drawing.Point(20, 175);
            this.btnFillAzureKeysFromCsv.Name = "btnFillAzureKeysFromCsv";
            this.btnFillAzureKeysFromCsv.Size = new System.Drawing.Size(170, 37);
            this.btnFillAzureKeysFromCsv.TabIndex = 17;
            this.btnFillAzureKeysFromCsv.Text = "Fill Keys From Csv";
            this.btnFillAzureKeysFromCsv.UseVisualStyleBackColor = false;
            this.btnFillAzureKeysFromCsv.Click += new System.EventHandler(this.btnFillAzureKeysFromCsv_Click);
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
            this.pnlAwsConfig.Controls.Add(this.btnKeysCsvTemplateDownload);
            this.pnlAwsConfig.Controls.Add(this.btnFillKeysFromCsv);
            this.pnlAwsConfig.Controls.Add(this.btnVisibleKey);
            this.pnlAwsConfig.Location = new System.Drawing.Point(20, 138);
            this.pnlAwsConfig.Name = "pnlAwsConfig";
            this.pnlAwsConfig.Size = new System.Drawing.Size(746, 223);
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
            this.tabPageSearch.Size = new System.Drawing.Size(1375, 912);
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
            this.tabPageAnalysis.Size = new System.Drawing.Size(1375, 912);
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
            // tabPageMediaResizer
            // 
            this.tabPageMediaResizer.Controls.Add(this.grpResizerResults);
            this.tabPageMediaResizer.Controls.Add(this.grpResizerControl);
            this.tabPageMediaResizer.Controls.Add(this.grpResizerUpload);
            this.tabPageMediaResizer.Controls.Add(this.grpResizerSettings);
            this.tabPageMediaResizer.Location = new System.Drawing.Point(4, 25);
            this.tabPageMediaResizer.Name = "tabPageMediaResizer";
            this.tabPageMediaResizer.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMediaResizer.Size = new System.Drawing.Size(1375, 854);
            this.tabPageMediaResizer.TabIndex = 3;
            this.tabPageMediaResizer.Text = "Media Resizer";
            this.tabPageMediaResizer.UseVisualStyleBackColor = true;
            // 
            // grpResizerResults
            // 
            this.grpResizerResults.Controls.Add(this.dgvResizerResults);
            this.grpResizerResults.Location = new System.Drawing.Point(20, 455);
            this.grpResizerResults.Name = "grpResizerResults";
            this.grpResizerResults.Size = new System.Drawing.Size(1340, 391);
            this.grpResizerResults.TabIndex = 3;
            this.grpResizerResults.TabStop = false;
            this.grpResizerResults.Text = "Resize Results";
            // 
            // dgvResizerResults
            // 
            this.dgvResizerResults.AllowUserToAddRows = false;
            this.dgvResizerResults.AllowUserToDeleteRows = false;
            this.dgvResizerResults.AllowUserToOrderColumns = true;
            this.dgvResizerResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResizerResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResizerResults.Location = new System.Drawing.Point(20, 25);
            this.dgvResizerResults.Name = "dgvResizerResults";
            this.dgvResizerResults.ReadOnly = true;
            this.dgvResizerResults.RowHeadersWidth = 51;
            this.dgvResizerResults.RowTemplate.Height = 24;
            this.dgvResizerResults.Size = new System.Drawing.Size(1314, 353);
            this.dgvResizerResults.TabIndex = 0;
            // 
            // grpResizerControl
            // 
            this.grpResizerControl.Controls.Add(this.lblResizeStatus);
            this.grpResizerControl.Controls.Add(this.progressBarResize);
            this.grpResizerControl.Controls.Add(this.btnExportResizerResults);
            this.grpResizerControl.Controls.Add(this.btnCancelResize);
            this.grpResizerControl.Controls.Add(this.btnStartResize);
            this.grpResizerControl.Location = new System.Drawing.Point(20, 349);
            this.grpResizerControl.Name = "grpResizerControl";
            this.grpResizerControl.Size = new System.Drawing.Size(1340, 100);
            this.grpResizerControl.TabIndex = 2;
            this.grpResizerControl.TabStop = false;
            this.grpResizerControl.Text = "Resize Control";
            // 
            // lblResizeStatus
            // 
            this.lblResizeStatus.AutoSize = true;
            this.lblResizeStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblResizeStatus.Location = new System.Drawing.Point(520, 60);
            this.lblResizeStatus.Name = "lblResizeStatus";
            this.lblResizeStatus.Size = new System.Drawing.Size(149, 18);
            this.lblResizeStatus.TabIndex = 4;
            this.lblResizeStatus.Text = "Ready for processing";
            // 
            // progressBarResize
            // 
            this.progressBarResize.Location = new System.Drawing.Point(520, 30);
            this.progressBarResize.Name = "progressBarResize";
            this.progressBarResize.Size = new System.Drawing.Size(400, 25);
            this.progressBarResize.TabIndex = 3;
            this.progressBarResize.Visible = false;
            // 
            // btnExportResizerResults
            // 
            this.btnExportResizerResults.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportResizerResults.Enabled = false;
            this.btnExportResizerResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportResizerResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportResizerResults.Location = new System.Drawing.Point(310, 30);
            this.btnExportResizerResults.Name = "btnExportResizerResults";
            this.btnExportResizerResults.Size = new System.Drawing.Size(180, 45);
            this.btnExportResizerResults.TabIndex = 2;
            this.btnExportResizerResults.Text = "Export to Excel";
            this.btnExportResizerResults.UseVisualStyleBackColor = false;
            this.btnExportResizerResults.Click += new System.EventHandler(this.btnExportResizerResults_Click);
            // 
            // btnCancelResize
            // 
            this.btnCancelResize.BackColor = System.Drawing.Color.Red;
            this.btnCancelResize.Enabled = false;
            this.btnCancelResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelResize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelResize.ForeColor = System.Drawing.Color.White;
            this.btnCancelResize.Location = new System.Drawing.Point(190, 30);
            this.btnCancelResize.Name = "btnCancelResize";
            this.btnCancelResize.Size = new System.Drawing.Size(100, 45);
            this.btnCancelResize.TabIndex = 1;
            this.btnCancelResize.Text = "Cancel";
            this.btnCancelResize.UseVisualStyleBackColor = false;
            this.btnCancelResize.Click += new System.EventHandler(this.btnCancelResize_Click);
            // 
            // btnStartResize
            // 
            this.btnStartResize.BackColor = System.Drawing.Color.Orange;
            this.btnStartResize.Enabled = false;
            this.btnStartResize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartResize.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartResize.Location = new System.Drawing.Point(20, 30);
            this.btnStartResize.Name = "btnStartResize";
            this.btnStartResize.Size = new System.Drawing.Size(150, 45);
            this.btnStartResize.TabIndex = 0;
            this.btnStartResize.Text = "Start Resize";
            this.btnStartResize.UseVisualStyleBackColor = false;
            this.btnStartResize.Click += new System.EventHandler(this.btnStartResize_Click);
            // 
            // grpResizerUpload
            // 
            this.grpResizerUpload.Controls.Add(this.lblResizerStatus);
            this.grpResizerUpload.Controls.Add(this.btnDownloadResizerTemplate);
            this.grpResizerUpload.Controls.Add(this.btnUploadMediaList);
            this.grpResizerUpload.Controls.Add(this.lblResizerInstructions);
            this.grpResizerUpload.Controls.Add(this.grpUrlCreation);
            this.grpResizerUpload.Controls.Add(this.grpUrlMode);
            this.grpResizerUpload.Location = new System.Drawing.Point(20, 20);
            this.grpResizerUpload.Name = "grpResizerUpload";
            this.grpResizerUpload.Size = new System.Drawing.Size(600, 320);
            this.grpResizerUpload.TabIndex = 0;
            this.grpResizerUpload.TabStop = false;
            this.grpResizerUpload.Text = "Media List Upload";
            // 
            // lblResizerStatus
            // 
            this.lblResizerStatus.AutoSize = true;
            this.lblResizerStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblResizerStatus.Location = new System.Drawing.Point(20, 115);
            this.lblResizerStatus.Name = "lblResizerStatus";
            this.lblResizerStatus.Size = new System.Drawing.Size(114, 18);
            this.lblResizerStatus.TabIndex = 3;
            this.lblResizerStatus.Text = "No file uploaded";
            // 
            // btnDownloadResizerTemplate
            // 
            this.btnDownloadResizerTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDownloadResizerTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadResizerTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadResizerTemplate.Location = new System.Drawing.Point(200, 60);
            this.btnDownloadResizerTemplate.Name = "btnDownloadResizerTemplate";
            this.btnDownloadResizerTemplate.Size = new System.Drawing.Size(180, 40);
            this.btnDownloadResizerTemplate.TabIndex = 2;
            this.btnDownloadResizerTemplate.Text = "Download Template";
            this.btnDownloadResizerTemplate.UseVisualStyleBackColor = false;
            this.btnDownloadResizerTemplate.Click += new System.EventHandler(this.btnDownloadResizerTemplate_Click);
            // 
            // btnUploadMediaList
            // 
            this.btnUploadMediaList.BackColor = System.Drawing.Color.LightBlue;
            this.btnUploadMediaList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadMediaList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadMediaList.Location = new System.Drawing.Point(20, 60);
            this.btnUploadMediaList.Name = "btnUploadMediaList";
            this.btnUploadMediaList.Size = new System.Drawing.Size(160, 40);
            this.btnUploadMediaList.TabIndex = 1;
            this.btnUploadMediaList.Text = "Upload File";
            this.btnUploadMediaList.UseVisualStyleBackColor = false;
            this.btnUploadMediaList.Click += new System.EventHandler(this.btnUploadMediaList_Click);
            // 
            // lblResizerInstructions
            // 
            this.lblResizerInstructions.AutoSize = true;
            this.lblResizerInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblResizerInstructions.Location = new System.Drawing.Point(20, 25);
            this.lblResizerInstructions.Name = "lblResizerInstructions";
            this.lblResizerInstructions.Size = new System.Drawing.Size(515, 18);
            this.lblResizerInstructions.TabIndex = 0;
            this.lblResizerInstructions.Text = "Upload CSV or Excel file with MediaName, MediaUrl, MediaFileSize columns.";
            // 
            // grpUrlCreation
            // 
            this.grpUrlCreation.Controls.Add(this.txtToken);
            this.grpUrlCreation.Controls.Add(this.lblToken);
            this.grpUrlCreation.Controls.Add(this.txtBaseUrl);
            this.grpUrlCreation.Controls.Add(this.lblBaseUrl);
            this.grpUrlCreation.Enabled = false;
            this.grpUrlCreation.Location = new System.Drawing.Point(20, 210);
            this.grpUrlCreation.Name = "grpUrlCreation";
            this.grpUrlCreation.Size = new System.Drawing.Size(560, 100);
            this.grpUrlCreation.TabIndex = 5;
            this.grpUrlCreation.TabStop = false;
            this.grpUrlCreation.Text = "URL Creation Settings";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(100, 62);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(440, 22);
            this.txtToken.TabIndex = 3;
            this.txtToken.Text = "?token=example";
            // 
            // lblToken
            // 
            this.lblToken.AutoSize = true;
            this.lblToken.Location = new System.Drawing.Point(20, 65);
            this.lblToken.Name = "lblToken";
            this.lblToken.Size = new System.Drawing.Size(49, 16);
            this.lblToken.TabIndex = 2;
            this.lblToken.Text = "Token:";
            // 
            // txtBaseUrl
            // 
            this.txtBaseUrl.Location = new System.Drawing.Point(100, 27);
            this.txtBaseUrl.Name = "txtBaseUrl";
            this.txtBaseUrl.Size = new System.Drawing.Size(440, 22);
            this.txtBaseUrl.TabIndex = 1;
            this.txtBaseUrl.Text = "https://example.com";
            // 
            // lblBaseUrl
            // 
            this.lblBaseUrl.AutoSize = true;
            this.lblBaseUrl.Location = new System.Drawing.Point(20, 30);
            this.lblBaseUrl.Name = "lblBaseUrl";
            this.lblBaseUrl.Size = new System.Drawing.Size(72, 16);
            this.lblBaseUrl.TabIndex = 0;
            this.lblBaseUrl.Text = "Base URL:";
            // 
            // grpUrlMode
            // 
            this.grpUrlMode.Controls.Add(this.rbCreateUrl);
            this.grpUrlMode.Controls.Add(this.rbUrlAlreadyExists);
            this.grpUrlMode.Location = new System.Drawing.Point(20, 150);
            this.grpUrlMode.Name = "grpUrlMode";
            this.grpUrlMode.Size = new System.Drawing.Size(560, 50);
            this.grpUrlMode.TabIndex = 4;
            this.grpUrlMode.TabStop = false;
            this.grpUrlMode.Text = "URL Mode";
            // 
            // rbCreateUrl
            // 
            this.rbCreateUrl.AutoSize = true;
            this.rbCreateUrl.Location = new System.Drawing.Point(180, 20);
            this.rbCreateUrl.Name = "rbCreateUrl";
            this.rbCreateUrl.Size = new System.Drawing.Size(98, 20);
            this.rbCreateUrl.TabIndex = 1;
            this.rbCreateUrl.Text = "Create URL";
            this.rbCreateUrl.UseVisualStyleBackColor = true;
            this.rbCreateUrl.CheckedChanged += new System.EventHandler(this.rbCreateUrl_CheckedChanged);
            // 
            // rbUrlAlreadyExists
            // 
            this.rbUrlAlreadyExists.AutoSize = true;
            this.rbUrlAlreadyExists.Checked = true;
            this.rbUrlAlreadyExists.Location = new System.Drawing.Point(20, 20);
            this.rbUrlAlreadyExists.Name = "rbUrlAlreadyExists";
            this.rbUrlAlreadyExists.Size = new System.Drawing.Size(143, 20);
            this.rbUrlAlreadyExists.TabIndex = 0;
            this.rbUrlAlreadyExists.TabStop = true;
            this.rbUrlAlreadyExists.Text = "URL Already Exists";
            this.rbUrlAlreadyExists.UseVisualStyleBackColor = true;
            this.rbUrlAlreadyExists.CheckedChanged += new System.EventHandler(this.rbUrlAlreadyExists_CheckedChanged);
            // 
            // grpResizerSettings
            // 
            this.grpResizerSettings.Controls.Add(this.cmbResizeMode);
            this.grpResizerSettings.Controls.Add(this.lblResizeMode);
            this.grpResizerSettings.Controls.Add(this.chkMaintainAspectRatio);
            this.grpResizerSettings.Controls.Add(this.numQuality);
            this.grpResizerSettings.Controls.Add(this.lblQuality);
            this.grpResizerSettings.Controls.Add(this.numTargetHeight);
            this.grpResizerSettings.Controls.Add(this.numTargetWidth);
            this.grpResizerSettings.Controls.Add(this.lblTargetDimensions);
            this.grpResizerSettings.Controls.Add(this.numMaxHeight);
            this.grpResizerSettings.Controls.Add(this.numMaxWidth);
            this.grpResizerSettings.Controls.Add(this.lblMaxDimensions);
            this.grpResizerSettings.Controls.Add(this.numMaxFileSize);
            this.grpResizerSettings.Controls.Add(this.lblMaxFileSize);
            this.grpResizerSettings.Enabled = false;
            this.grpResizerSettings.Location = new System.Drawing.Point(640, 20);
            this.grpResizerSettings.Name = "grpResizerSettings";
            this.grpResizerSettings.Size = new System.Drawing.Size(720, 150);
            this.grpResizerSettings.TabIndex = 1;
            this.grpResizerSettings.TabStop = false;
            this.grpResizerSettings.Text = "Resize Settings";
            // 
            // cmbResizeMode
            // 
            this.cmbResizeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbResizeMode.FormattingEnabled = true;
            this.cmbResizeMode.Items.AddRange(new object[] {
            "Fit",
            "Stretch",
            "Crop"});
            this.cmbResizeMode.Location = new System.Drawing.Point(450, 58);
            this.cmbResizeMode.Name = "cmbResizeMode";
            this.cmbResizeMode.Size = new System.Drawing.Size(120, 24);
            this.cmbResizeMode.TabIndex = 12;
            // 
            // lblResizeMode
            // 
            this.lblResizeMode.AutoSize = true;
            this.lblResizeMode.Location = new System.Drawing.Point(350, 60);
            this.lblResizeMode.Name = "lblResizeMode";
            this.lblResizeMode.Size = new System.Drawing.Size(90, 16);
            this.lblResizeMode.TabIndex = 11;
            this.lblResizeMode.Text = "Resize Mode:";
            // 
            // chkMaintainAspectRatio
            // 
            this.chkMaintainAspectRatio.AutoSize = true;
            this.chkMaintainAspectRatio.Checked = true;
            this.chkMaintainAspectRatio.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMaintainAspectRatio.Location = new System.Drawing.Point(350, 30);
            this.chkMaintainAspectRatio.Name = "chkMaintainAspectRatio";
            this.chkMaintainAspectRatio.Size = new System.Drawing.Size(159, 20);
            this.chkMaintainAspectRatio.TabIndex = 10;
            this.chkMaintainAspectRatio.Text = "Maintain Aspect Ratio";
            this.chkMaintainAspectRatio.UseVisualStyleBackColor = true;
            // 
            // numQuality
            // 
            this.numQuality.Location = new System.Drawing.Point(150, 118);
            this.numQuality.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuality.Name = "numQuality";
            this.numQuality.Size = new System.Drawing.Size(80, 22);
            this.numQuality.TabIndex = 9;
            this.numQuality.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // lblQuality
            // 
            this.lblQuality.AutoSize = true;
            this.lblQuality.Location = new System.Drawing.Point(20, 120);
            this.lblQuality.Name = "lblQuality";
            this.lblQuality.Size = new System.Drawing.Size(94, 16);
            this.lblQuality.TabIndex = 8;
            this.lblQuality.Text = "Quality (1-100):";
            // 
            // numTargetHeight
            // 
            this.numTargetHeight.Location = new System.Drawing.Point(240, 88);
            this.numTargetHeight.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numTargetHeight.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTargetHeight.Name = "numTargetHeight";
            this.numTargetHeight.Size = new System.Drawing.Size(80, 22);
            this.numTargetHeight.TabIndex = 7;
            this.numTargetHeight.Value = new decimal(new int[] {
            1080,
            0,
            0,
            0});
            // 
            // numTargetWidth
            // 
            this.numTargetWidth.Location = new System.Drawing.Point(150, 88);
            this.numTargetWidth.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.numTargetWidth.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numTargetWidth.Name = "numTargetWidth";
            this.numTargetWidth.Size = new System.Drawing.Size(80, 22);
            this.numTargetWidth.TabIndex = 6;
            this.numTargetWidth.Value = new decimal(new int[] {
            1920,
            0,
            0,
            0});
            // 
            // lblTargetDimensions
            // 
            this.lblTargetDimensions.AutoSize = true;
            this.lblTargetDimensions.Location = new System.Drawing.Point(20, 90);
            this.lblTargetDimensions.Name = "lblTargetDimensions";
            this.lblTargetDimensions.Size = new System.Drawing.Size(124, 16);
            this.lblTargetDimensions.TabIndex = 5;
            this.lblTargetDimensions.Text = "Target Dimensions:";
            // 
            // numMaxHeight
            // 
            this.numMaxHeight.Location = new System.Drawing.Point(240, 58);
            this.numMaxHeight.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxHeight.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxHeight.Name = "numMaxHeight";
            this.numMaxHeight.Size = new System.Drawing.Size(80, 22);
            this.numMaxHeight.TabIndex = 4;
            this.numMaxHeight.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // numMaxWidth
            // 
            this.numMaxWidth.Location = new System.Drawing.Point(150, 58);
            this.numMaxWidth.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numMaxWidth.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numMaxWidth.Name = "numMaxWidth";
            this.numMaxWidth.Size = new System.Drawing.Size(80, 22);
            this.numMaxWidth.TabIndex = 3;
            this.numMaxWidth.Value = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            // 
            // lblMaxDimensions
            // 
            this.lblMaxDimensions.AutoSize = true;
            this.lblMaxDimensions.Location = new System.Drawing.Point(20, 60);
            this.lblMaxDimensions.Name = "lblMaxDimensions";
            this.lblMaxDimensions.Size = new System.Drawing.Size(109, 16);
            this.lblMaxDimensions.TabIndex = 2;
            this.lblMaxDimensions.Text = "Max Dimensions:";
            // 
            // numMaxFileSize
            // 
            this.numMaxFileSize.Location = new System.Drawing.Point(150, 28);
            this.numMaxFileSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numMaxFileSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numMaxFileSize.Name = "numMaxFileSize";
            this.numMaxFileSize.Size = new System.Drawing.Size(100, 22);
            this.numMaxFileSize.TabIndex = 1;
            this.numMaxFileSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblMaxFileSize
            // 
            this.lblMaxFileSize.AutoSize = true;
            this.lblMaxFileSize.Location = new System.Drawing.Point(20, 30);
            this.lblMaxFileSize.Name = "lblMaxFileSize";
            this.lblMaxFileSize.Size = new System.Drawing.Size(120, 16);
            this.lblMaxFileSize.TabIndex = 0;
            this.lblMaxFileSize.Text = "Max File Size (MB):";
            // 
            // VideoSizeFinderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1383, 883);
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
            this.tabPageMediaResizer.ResumeLayout(false);
            this.grpResizerResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResizerResults)).EndInit();
            this.grpResizerControl.ResumeLayout(false);
            this.grpResizerControl.PerformLayout();
            this.grpResizerUpload.ResumeLayout(false);
            this.grpResizerUpload.PerformLayout();
            this.grpUrlCreation.ResumeLayout(false);
            this.grpUrlCreation.PerformLayout();
            this.grpUrlMode.ResumeLayout(false);
            this.grpUrlMode.PerformLayout();
            this.grpResizerSettings.ResumeLayout(false);
            this.grpResizerSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuality)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTargetWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxFileSize)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.TabPage tabPageSearch;
        private System.Windows.Forms.TabPage tabPageAnalysis;
        private System.Windows.Forms.TabPage tabPageMediaResizer;

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
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnTestConnection;

        private System.Windows.Forms.GroupBox grpResizerUpload;
        private System.Windows.Forms.GroupBox grpResizerSettings;
        private System.Windows.Forms.GroupBox grpResizerControl;
        private System.Windows.Forms.GroupBox grpResizerResults;

        // Upload controls
        private System.Windows.Forms.Label lblResizerInstructions;
        private System.Windows.Forms.Button btnUploadMediaList;
        private System.Windows.Forms.Button btnDownloadResizerTemplate;
        private System.Windows.Forms.Label lblResizerStatus;

        // Settings controls
        private System.Windows.Forms.Label lblMaxFileSize;
        private System.Windows.Forms.NumericUpDown numMaxFileSize;
        private System.Windows.Forms.Label lblMaxDimensions;
        private System.Windows.Forms.NumericUpDown numMaxWidth;
        private System.Windows.Forms.NumericUpDown numMaxHeight;
        private System.Windows.Forms.Label lblTargetDimensions;
        private System.Windows.Forms.NumericUpDown numTargetWidth;
        private System.Windows.Forms.NumericUpDown numTargetHeight;
        private System.Windows.Forms.Label lblQuality;
        private System.Windows.Forms.NumericUpDown numQuality;
        private System.Windows.Forms.CheckBox chkMaintainAspectRatio;
        private System.Windows.Forms.ComboBox cmbResizeMode;
        private System.Windows.Forms.Label lblResizeMode;

        // Control buttons
        private System.Windows.Forms.Button btnStartResize;
        private System.Windows.Forms.Button btnCancelResize;
        private System.Windows.Forms.Button btnExportResizerResults;
        private System.Windows.Forms.ProgressBar progressBarResize;
        private System.Windows.Forms.Label lblResizeStatus;

        // Results
        private System.Windows.Forms.DataGridView dgvResizerResults;

        // URL Mode seçimi için GroupBox
        private System.Windows.Forms.GroupBox grpUrlMode;
        private System.Windows.Forms.RadioButton rbUrlAlreadyExists;
        private System.Windows.Forms.RadioButton rbCreateUrl;

        // URL oluşturma için kontroller
        private System.Windows.Forms.GroupBox grpUrlCreation;
        private System.Windows.Forms.Label lblBaseUrl;
        private System.Windows.Forms.TextBox txtBaseUrl;
        private System.Windows.Forms.Label lblToken;
        private System.Windows.Forms.TextBox txtToken;
    }
}