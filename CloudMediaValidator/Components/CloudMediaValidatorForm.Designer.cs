namespace CloudStorageTools.CloudMediaValidator.Components
{
    partial class CloudMediaValidatorForm
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
            this.tabPageValidation = new System.Windows.Forms.TabPage();
            this.grpValidationResults = new System.Windows.Forms.GroupBox();
            this.dgvValidationResults = new System.Windows.Forms.DataGridView();
            this.grpValidationControl = new System.Windows.Forms.GroupBox();
            this.btnExportResults = new System.Windows.Forms.Button();
            this.btnCancelValidation = new System.Windows.Forms.Button();
            this.btnStartValidation = new System.Windows.Forms.Button();
            this.progressBarValidation = new System.Windows.Forms.ProgressBar();
            this.lblValidationStatus = new System.Windows.Forms.Label();
            this.grpCsvUpload = new System.Windows.Forms.GroupBox();
            this.lblMediaPreview = new System.Windows.Forms.Label();
            this.lblCsvStatus = new System.Windows.Forms.Label();
            this.btnDownloadCsvTemplate = new System.Windows.Forms.Button();
            this.btnUploadCsv = new System.Windows.Forms.Button();
            this.lblCsvInstructions = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPageConnection.SuspendLayout();
            this.pnlAzureConfig.SuspendLayout();
            this.pnlAwsConfig.SuspendLayout();
            this.grpCloudProvider.SuspendLayout();
            this.tabPageValidation.SuspendLayout();
            this.grpValidationResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvValidationResults)).BeginInit();
            this.grpValidationControl.SuspendLayout();
            this.grpCsvUpload.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageConnection);
            this.tabControl.Controls.Add(this.tabPageValidation);
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
            this.btnKeysCsvTemplateDownload.Location = new System.Drawing.Point(210, 175);
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
            this.btnFillKeysFromCsv.Location = new System.Drawing.Point(20, 175);
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
            // tabPageValidation
            // 
            this.tabPageValidation.Controls.Add(this.grpValidationResults);
            this.tabPageValidation.Controls.Add(this.grpValidationControl);
            this.tabPageValidation.Controls.Add(this.grpCsvUpload);
            this.tabPageValidation.Location = new System.Drawing.Point(4, 25);
            this.tabPageValidation.Name = "tabPageValidation";
            this.tabPageValidation.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageValidation.Size = new System.Drawing.Size(1385, 878);
            this.tabPageValidation.TabIndex = 1;
            this.tabPageValidation.Text = "Media Validation";
            this.tabPageValidation.UseVisualStyleBackColor = true;
            // 
            // grpValidationResults
            // 
            this.grpValidationResults.Controls.Add(this.dgvValidationResults);
            this.grpValidationResults.Location = new System.Drawing.Point(20, 350);
            this.grpValidationResults.Name = "grpValidationResults";
            this.grpValidationResults.Size = new System.Drawing.Size(1344, 520);
            this.grpValidationResults.TabIndex = 2;
            this.grpValidationResults.TabStop = false;
            this.grpValidationResults.Text = "Validation Results";
            // 
            // dgvValidationResults
            // 
            this.dgvValidationResults.AllowUserToAddRows = false;
            this.dgvValidationResults.AllowUserToDeleteRows = false;
            this.dgvValidationResults.AllowUserToOrderColumns = true;
            this.dgvValidationResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvValidationResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvValidationResults.Location = new System.Drawing.Point(20, 25);
            this.dgvValidationResults.Name = "dgvValidationResults";
            this.dgvValidationResults.RowHeadersWidth = 51;
            this.dgvValidationResults.RowTemplate.Height = 24;
            this.dgvValidationResults.Size = new System.Drawing.Size(1318, 489);
            this.dgvValidationResults.TabIndex = 0;
            // 
            // grpValidationControl
            // 
            this.grpValidationControl.Controls.Add(this.btnExportResults);
            this.grpValidationControl.Controls.Add(this.btnCancelValidation);
            this.grpValidationControl.Controls.Add(this.btnStartValidation);
            this.grpValidationControl.Controls.Add(this.progressBarValidation);
            this.grpValidationControl.Controls.Add(this.lblValidationStatus);
            this.grpValidationControl.Location = new System.Drawing.Point(20, 200);
            this.grpValidationControl.Name = "grpValidationControl";
            this.grpValidationControl.Size = new System.Drawing.Size(800, 130);
            this.grpValidationControl.TabIndex = 1;
            this.grpValidationControl.TabStop = false;
            this.grpValidationControl.Text = "Validation Control";
            // 
            // btnExportResults
            // 
            this.btnExportResults.BackColor = System.Drawing.Color.LightGreen;
            this.btnExportResults.Enabled = false;
            this.btnExportResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportResults.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportResults.Location = new System.Drawing.Point(380, 30);
            this.btnExportResults.Name = "btnExportResults";
            this.btnExportResults.Size = new System.Drawing.Size(195, 45);
            this.btnExportResults.TabIndex = 4;
            this.btnExportResults.Text = "Export to Excel";
            this.btnExportResults.UseVisualStyleBackColor = false;
            this.btnExportResults.Click += new System.EventHandler(this.btnExportResults_Click);
            // 
            // btnCancelValidation
            // 
            this.btnCancelValidation.BackColor = System.Drawing.Color.Red;
            this.btnCancelValidation.Enabled = false;
            this.btnCancelValidation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnCancelValidation.ForeColor = System.Drawing.Color.White;
            this.btnCancelValidation.Location = new System.Drawing.Point(250, 30);
            this.btnCancelValidation.Name = "btnCancelValidation";
            this.btnCancelValidation.Size = new System.Drawing.Size(100, 45);
            this.btnCancelValidation.TabIndex = 3;
            this.btnCancelValidation.Text = "Cancel";
            this.btnCancelValidation.UseVisualStyleBackColor = false;
            this.btnCancelValidation.Click += new System.EventHandler(this.btnCancelValidation_Click);
            // 
            // btnStartValidation
            // 
            this.btnStartValidation.BackColor = System.Drawing.Color.Orange;
            this.btnStartValidation.Enabled = false;
            this.btnStartValidation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartValidation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnStartValidation.Location = new System.Drawing.Point(20, 30);
            this.btnStartValidation.Name = "btnStartValidation";
            this.btnStartValidation.Size = new System.Drawing.Size(200, 45);
            this.btnStartValidation.TabIndex = 2;
            this.btnStartValidation.Text = "Start Validation";
            this.btnStartValidation.UseVisualStyleBackColor = false;
            this.btnStartValidation.Click += new System.EventHandler(this.btnStartValidation_Click);
            // 
            // progressBarValidation
            // 
            this.progressBarValidation.Location = new System.Drawing.Point(20, 90);
            this.progressBarValidation.Name = "progressBarValidation";
            this.progressBarValidation.Size = new System.Drawing.Size(600, 25);
            this.progressBarValidation.TabIndex = 1;
            this.progressBarValidation.Visible = false;
            // 
            // lblValidationStatus
            // 
            this.lblValidationStatus.AutoSize = true;
            this.lblValidationStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblValidationStatus.Location = new System.Drawing.Point(650, 45);
            this.lblValidationStatus.Name = "lblValidationStatus";
            this.lblValidationStatus.Size = new System.Drawing.Size(149, 18);
            this.lblValidationStatus.TabIndex = 0;
            this.lblValidationStatus.Text = "Ready for validation...";
            this.lblValidationStatus.Click += new System.EventHandler(this.lblValidationStatus_Click);
            // 
            // grpCsvUpload
            // 
            this.grpCsvUpload.Controls.Add(this.lblMediaPreview);
            this.grpCsvUpload.Controls.Add(this.lblCsvStatus);
            this.grpCsvUpload.Controls.Add(this.btnDownloadCsvTemplate);
            this.grpCsvUpload.Controls.Add(this.btnUploadCsv);
            this.grpCsvUpload.Controls.Add(this.lblCsvInstructions);
            this.grpCsvUpload.Enabled = false;
            this.grpCsvUpload.Location = new System.Drawing.Point(20, 20);
            this.grpCsvUpload.Name = "grpCsvUpload";
            this.grpCsvUpload.Size = new System.Drawing.Size(800, 160);
            this.grpCsvUpload.TabIndex = 0;
            this.grpCsvUpload.TabStop = false;
            this.grpCsvUpload.Text = "CSV Upload";
            // 
            // lblMediaPreview
            // 
            this.lblMediaPreview.AutoSize = true;
            this.lblMediaPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Italic);
            this.lblMediaPreview.ForeColor = System.Drawing.Color.Gray;
            this.lblMediaPreview.Location = new System.Drawing.Point(20, 130);
            this.lblMediaPreview.Name = "lblMediaPreview";
            this.lblMediaPreview.Size = new System.Drawing.Size(0, 17);
            this.lblMediaPreview.TabIndex = 4;
            // 
            // lblCsvStatus
            // 
            this.lblCsvStatus.AutoSize = true;
            this.lblCsvStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCsvStatus.Location = new System.Drawing.Point(458, 75);
            this.lblCsvStatus.Name = "lblCsvStatus";
            this.lblCsvStatus.Size = new System.Drawing.Size(132, 18);
            this.lblCsvStatus.TabIndex = 3;
            this.lblCsvStatus.Text = "No CSV file loaded";
            // 
            // btnDownloadCsvTemplate
            // 
            this.btnDownloadCsvTemplate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDownloadCsvTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownloadCsvTemplate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnDownloadCsvTemplate.Location = new System.Drawing.Point(200, 60);
            this.btnDownloadCsvTemplate.Name = "btnDownloadCsvTemplate";
            this.btnDownloadCsvTemplate.Size = new System.Drawing.Size(213, 40);
            this.btnDownloadCsvTemplate.TabIndex = 2;
            this.btnDownloadCsvTemplate.Text = "Download Template";
            this.btnDownloadCsvTemplate.UseVisualStyleBackColor = false;
            this.btnDownloadCsvTemplate.Click += new System.EventHandler(this.btnDownloadCsvTemplate_Click);
            // 
            // btnUploadCsv
            // 
            this.btnUploadCsv.BackColor = System.Drawing.Color.LightBlue;
            this.btnUploadCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUploadCsv.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnUploadCsv.Location = new System.Drawing.Point(20, 60);
            this.btnUploadCsv.Name = "btnUploadCsv";
            this.btnUploadCsv.Size = new System.Drawing.Size(160, 40);
            this.btnUploadCsv.TabIndex = 1;
            this.btnUploadCsv.Text = "Upload CSV";
            this.btnUploadCsv.UseVisualStyleBackColor = false;
            this.btnUploadCsv.Click += new System.EventHandler(this.btnUploadCsv_Click);
            // 
            // lblCsvInstructions
            // 
            this.lblCsvInstructions.AutoSize = true;
            this.lblCsvInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblCsvInstructions.Location = new System.Drawing.Point(20, 25);
            this.lblCsvInstructions.Name = "lblCsvInstructions";
            this.lblCsvInstructions.Size = new System.Drawing.Size(568, 18);
            this.lblCsvInstructions.TabIndex = 0;
            this.lblCsvInstructions.Text = "Upload a CSV file containing media names to validate their existence in cloud sto" +
    "rage.";
            // 
            // CloudMediaValidatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1393, 907);
            this.Controls.Add(this.tabControl);
            this.MaximizeBox = false;
            this.Name = "CloudMediaValidatorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cloud Media Validator";
            this.tabControl.ResumeLayout(false);
            this.tabPageConnection.ResumeLayout(false);
            this.tabPageConnection.PerformLayout();
            this.pnlAzureConfig.ResumeLayout(false);
            this.pnlAzureConfig.PerformLayout();
            this.pnlAwsConfig.ResumeLayout(false);
            this.pnlAwsConfig.PerformLayout();
            this.grpCloudProvider.ResumeLayout(false);
            this.grpCloudProvider.PerformLayout();
            this.tabPageValidation.ResumeLayout(false);
            this.grpValidationResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvValidationResults)).EndInit();
            this.grpValidationControl.ResumeLayout(false);
            this.grpValidationControl.PerformLayout();
            this.grpCsvUpload.ResumeLayout(false);
            this.grpCsvUpload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageConnection;
        private System.Windows.Forms.TabPage tabPageValidation;

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
        private System.Windows.Forms.Button btnVisibleKey;
        private System.Windows.Forms.Button btnFillKeysFromCsv;
        private System.Windows.Forms.Button btnKeysCsvTemplateDownload;
        private System.Windows.Forms.Button btnAzureVisibleKey;
        private System.Windows.Forms.Button btnFillAzureKeysFromCsv;
        private System.Windows.Forms.Button btnAzureKeysCsvTemplateDownload;
        private System.Windows.Forms.Label lblConnectionStatus;
        private System.Windows.Forms.Button btnTestConnection;

        // Validation Tab Controls
        private System.Windows.Forms.GroupBox grpCsvUpload;
        private System.Windows.Forms.Label lblCsvInstructions;
        private System.Windows.Forms.Button btnUploadCsv;
        private System.Windows.Forms.Button btnDownloadCsvTemplate;
        private System.Windows.Forms.Label lblCsvStatus;
        private System.Windows.Forms.Label lblMediaPreview;

        private System.Windows.Forms.GroupBox grpValidationControl;
        private System.Windows.Forms.Label lblValidationStatus;
        private System.Windows.Forms.ProgressBar progressBarValidation;
        private System.Windows.Forms.Button btnStartValidation;
        private System.Windows.Forms.Button btnCancelValidation;
        private System.Windows.Forms.Button btnExportResults;

        private System.Windows.Forms.GroupBox grpValidationResults;
        private System.Windows.Forms.DataGridView dgvValidationResults;
    }
}