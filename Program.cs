using CloudStorageTools.VideoSizeFinder.Components;
using System;
using System.Windows.Forms;

namespace CloudStorageTools
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

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
                this.btnVideoSizeFinder = new System.Windows.Forms.Button();
                this.btnCloudFileCleaner = new System.Windows.Forms.Button();
                this.lblTitle = new System.Windows.Forms.Label();
                this.SuspendLayout();
                // 
                // lblTitle
                // 
                this.lblTitle.AutoSize = true;
                this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
                this.lblTitle.Location = new System.Drawing.Point(80, 30);
                this.lblTitle.Name = "lblTitle";
                this.lblTitle.Size = new System.Drawing.Size(250, 31);
                this.lblTitle.TabIndex = 0;
                this.lblTitle.Text = "Cloud Storage Tools";
                // 
                // btnVideoSizeFinder
                // 
                this.btnVideoSizeFinder.BackColor = System.Drawing.Color.LightBlue;
                this.btnVideoSizeFinder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnVideoSizeFinder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                this.btnVideoSizeFinder.Location = new System.Drawing.Point(50, 100);
                this.btnVideoSizeFinder.Name = "btnVideoSizeFinder";
                this.btnVideoSizeFinder.Size = new System.Drawing.Size(350, 130);
                this.btnVideoSizeFinder.TabIndex = 1;
                this.btnVideoSizeFinder.Text = "Video Size Finder & Analyzer\r\n\r\nAnalyze cloud media files with FFMPEG\r\nExport detailed reports to Excel";
                this.btnVideoSizeFinder.UseVisualStyleBackColor = false;
                this.btnVideoSizeFinder.Click += new System.EventHandler(this.btnVideoSizeFinder_Click);
                // 
                // btnCloudFileCleaner
                // 
                this.btnCloudFileCleaner.BackColor = System.Drawing.Color.LightCoral;
                this.btnCloudFileCleaner.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnCloudFileCleaner.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
                this.btnCloudFileCleaner.Location = new System.Drawing.Point(50, 250);
                this.btnCloudFileCleaner.Name = "btnCloudFileCleaner";
                this.btnCloudFileCleaner.Size = new System.Drawing.Size(350, 130);
                this.btnCloudFileCleaner.TabIndex = 2;
                this.btnCloudFileCleaner.Text = "Cloud File Cleaner\r\n\r\n(Coming Soon)\r\nClean and organize cloud storage";
                this.btnCloudFileCleaner.UseVisualStyleBackColor = false;
                this.btnCloudFileCleaner.Enabled = false;
                // 
                // MainMenuForm
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(450, 420);
                this.Controls.Add(this.btnCloudFileCleaner);
                this.Controls.Add(this.btnVideoSizeFinder);
                this.Controls.Add(this.lblTitle);
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
                this.MaximizeBox = false;
                this.Name = "MainMenuForm";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Cloud Storage Tools - Main Menu";
                this.ResumeLayout(false);
                this.PerformLayout();
            }

            private System.Windows.Forms.Label lblTitle;
            private System.Windows.Forms.Button btnVideoSizeFinder;
            private System.Windows.Forms.Button btnCloudFileCleaner;

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
        }
    }
}
