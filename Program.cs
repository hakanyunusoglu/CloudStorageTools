using CloudStorageTools.CloudMediaValidator.Components;
using CloudStorageTools.VideoSizeFinder.Components;
using OfficeOpenXml;
using System.Diagnostics;
using System.Security.Principal;

namespace CloudStorageTools;

internal static class Program
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, args) => ShowError(args.Exception);
        AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            ShowError(args.ExceptionObject as Exception);

        bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent())
            .IsInRole(WindowsBuiltInRole.Administrator);

        if (!isAdmin)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = true;
                startInfo.WorkingDirectory = Environment.CurrentDirectory;
                startInfo.FileName = Environment.ProcessPath ?? Application.ExecutablePath;
                startInfo.Verb = "runas";

                Process.Start(startInfo);
                Application.Exit();
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Uygulama yönetici haklarıyla başlatılamadı: " + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        var mainMenuForm = new MainMenuForm();
        Application.Run(mainMenuForm);
    }

    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnVideoSizeFinder = new Button();
            this.btnCloudMediaValidator = new Button();
            this.btnCloudFileCleaner = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.Location = new Point(80, 30);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(250, 31);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cloud Storage Tools";
            
            // btnVideoSizeFinder
            this.btnVideoSizeFinder.BackColor = Color.LightBlue;
            this.btnVideoSizeFinder.FlatStyle = FlatStyle.Flat;
            this.btnVideoSizeFinder.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnVideoSizeFinder.Location = new Point(50, 100);
            this.btnVideoSizeFinder.Name = "btnVideoSizeFinder";
            this.btnVideoSizeFinder.Size = new Size(350, 130);
            this.btnVideoSizeFinder.TabIndex = 1;
            this.btnVideoSizeFinder.Text = "Video Size Finder & Analyzer\r\nExport detailed reports to Excel";
            this.btnVideoSizeFinder.UseVisualStyleBackColor = false;
            this.btnVideoSizeFinder.Click += btnVideoSizeFinder_Click;
            
            // btnCloudMediaValidator
            this.btnCloudMediaValidator.BackColor = Color.LightGreen;
            this.btnCloudMediaValidator.FlatStyle = FlatStyle.Flat;
            this.btnCloudMediaValidator.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnCloudMediaValidator.Location = new Point(50, 250);
            this.btnCloudMediaValidator.Name = "btnCloudMediaValidator";
            this.btnCloudMediaValidator.Size = new Size(350, 130);
            this.btnCloudMediaValidator.TabIndex = 2;
            this.btnCloudMediaValidator.Text = "Cloud Media Validator\r\nValidate media existence from CSV";
            this.btnCloudMediaValidator.UseVisualStyleBackColor = false;
            this.btnCloudMediaValidator.Click += btnCloudMediaValidator_Click;
            
            // btnCloudFileCleaner
            this.btnCloudFileCleaner.BackColor = Color.LightCoral;
            this.btnCloudFileCleaner.FlatStyle = FlatStyle.Flat;
            this.btnCloudFileCleaner.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold);
            this.btnCloudFileCleaner.Location = new Point(50, 400);
            this.btnCloudFileCleaner.Name = "btnCloudFileCleaner";
            this.btnCloudFileCleaner.Size = new Size(350, 130);
            this.btnCloudFileCleaner.TabIndex = 3;
            this.btnCloudFileCleaner.Text = "Cloud File Cleaner\r\nClean and organize cloud storage";
            this.btnCloudFileCleaner.UseVisualStyleBackColor = false;
            this.btnCloudFileCleaner.Enabled = false;
            
            // MainMenuForm
            this.AutoScaleDimensions = new SizeF(8F, 16F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(450, 570);
            this.Controls.Add(this.btnCloudFileCleaner);
            this.Controls.Add(this.btnCloudMediaValidator);
            this.Controls.Add(this.btnVideoSizeFinder);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainMenuForm";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cloud Storage Tools - Main Menu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Label lblTitle = new();
        private Button btnVideoSizeFinder = new();
        private Button btnCloudMediaValidator = new();
        private Button btnCloudFileCleaner = new();

        private void btnVideoSizeFinder_Click(object sender, EventArgs e)
        {
            try
            {
                var videoSizeFinderForm = new VideoSizeFinderForm();
                videoSizeFinderForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Video Size Finder: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCloudMediaValidator_Click(object sender, EventArgs e)
        {
            try
            {
                var cloudMediaValidatorForm = new CloudMediaValidatorForm();
                cloudMediaValidatorForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening Cloud Media Validator: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private static void ShowError(Exception? ex)
    {
        MessageBox.Show($"Kritik hata: {ex?.ToString()}", "Çökme", MessageBoxButtons.OK, MessageBoxIcon.Error);
        Environment.Exit(1);
    }
}