namespace FileParser
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.openMasterFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openedMasterFileLabel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.openCompareFile = new System.Windows.Forms.Button();
            this.openedCompareFileLabel = new System.Windows.Forms.TextBox();
            this.compareButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.totalTime = new System.Windows.Forms.Label();
            this.elapsedTimeLabel = new System.Windows.Forms.Label();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.compareBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.cancelButton = new System.Windows.Forms.Button();
            this.openFileResult = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.completedPercentageLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // openMasterFile
            // 
            this.openMasterFile.Location = new System.Drawing.Point(12, 12);
            this.openMasterFile.Name = "openMasterFile";
            this.openMasterFile.Size = new System.Drawing.Size(99, 23);
            this.openMasterFile.TabIndex = 0;
            this.openMasterFile.Text = "Open Master File";
            this.openMasterFile.UseVisualStyleBackColor = true;
            this.openMasterFile.Click += new System.EventHandler(this.openMasterFile_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Tab Delimited Files (*.tab)|*.tab|CSV Files (*.csv)|*.csv|All files (*.*)|*.*";
            this.openFileDialog1.InitialDirectory = "D:\\Fabrizio\\VB project\\FileParser";
            // 
            // openedMasterFileLabel
            // 
            this.openedMasterFileLabel.Location = new System.Drawing.Point(12, 57);
            this.openedMasterFileLabel.Name = "openedMasterFileLabel";
            this.openedMasterFileLabel.Size = new System.Drawing.Size(359, 20);
            this.openedMasterFileLabel.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Opened file";
            // 
            // openCompareFile
            // 
            this.openCompareFile.Location = new System.Drawing.Point(377, 12);
            this.openCompareFile.Name = "openCompareFile";
            this.openCompareFile.Size = new System.Drawing.Size(141, 23);
            this.openCompareFile.TabIndex = 5;
            this.openCompareFile.Text = "Open File to compare";
            this.openCompareFile.UseVisualStyleBackColor = true;
            this.openCompareFile.Click += new System.EventHandler(this.openCompareFile_Click);
            // 
            // openedCompareFileLabel
            // 
            this.openedCompareFileLabel.Location = new System.Drawing.Point(377, 57);
            this.openedCompareFileLabel.Name = "openedCompareFileLabel";
            this.openedCompareFileLabel.Size = new System.Drawing.Size(359, 20);
            this.openedCompareFileLabel.TabIndex = 6;
            // 
            // compareButton
            // 
            this.compareButton.Location = new System.Drawing.Point(569, 233);
            this.compareButton.Name = "compareButton";
            this.compareButton.Size = new System.Drawing.Size(75, 23);
            this.compareButton.TabIndex = 7;
            this.compareButton.Text = "Compare";
            this.compareButton.UseVisualStyleBackColor = true;
            this.compareButton.Click += new System.EventHandler(this.compareButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 200);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(724, 23);
            this.progressBar1.TabIndex = 8;
            // 
            // totalTime
            // 
            this.totalTime.AutoSize = true;
            this.totalTime.Location = new System.Drawing.Point(12, 235);
            this.totalTime.Name = "totalTime";
            this.totalTime.Size = new System.Drawing.Size(70, 13);
            this.totalTime.TabIndex = 9;
            this.totalTime.Text = "Elapsed time:";
            // 
            // elapsedTimeLabel
            // 
            this.elapsedTimeLabel.AutoSize = true;
            this.elapsedTimeLabel.Location = new System.Drawing.Point(86, 235);
            this.elapsedTimeLabel.Name = "elapsedTimeLabel";
            this.elapsedTimeLabel.Size = new System.Drawing.Size(0, 13);
            this.elapsedTimeLabel.TabIndex = 10;
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // compareBackgroundWorker
            // 
            this.compareBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.compareBackgroundWorker_DoWork);
            this.compareBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.compareBackgroundWorker_ProgressChanged);
            this.compareBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.compareBackgroundWorker_RunWorkerCompleted);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(661, 233);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 11;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // openFileResult
            // 
            this.openFileResult.Location = new System.Drawing.Point(472, 233);
            this.openFileResult.Name = "openFileResult";
            this.openFileResult.Size = new System.Drawing.Size(91, 23);
            this.openFileResult.TabIndex = 0;
            this.openFileResult.Text = "Open result file";
            this.openFileResult.UseVisualStyleBackColor = true;
            this.openFileResult.Visible = false;
            this.openFileResult.Click += new System.EventHandler(this.openFileResult_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Completed:";
            // 
            // completedPercentageLabel
            // 
            this.completedPercentageLabel.AutoSize = true;
            this.completedPercentageLabel.Location = new System.Drawing.Point(86, 251);
            this.completedPercentageLabel.Name = "completedPercentageLabel";
            this.completedPercentageLabel.Size = new System.Drawing.Size(0, 13);
            this.completedPercentageLabel.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(377, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Opened file";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 268);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.completedPercentageLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.openFileResult);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.elapsedTimeLabel);
            this.Controls.Add(this.totalTime);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.compareButton);
            this.Controls.Add(this.openedCompareFileLabel);
            this.Controls.Add(this.openCompareFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.openedMasterFileLabel);
            this.Controls.Add(this.openMasterFile);
            this.Name = "Form1";
            this.Text = "Standard program";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openMasterFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox openedMasterFileLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openCompareFile;
        private System.Windows.Forms.TextBox openedCompareFileLabel;
        private System.Windows.Forms.Button compareButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label elapsedTimeLabel;
        private System.Windows.Forms.Label totalTime;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.ComponentModel.BackgroundWorker compareBackgroundWorker;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button openFileResult;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label completedPercentageLabel;
        private System.Windows.Forms.Label label3;
    }
}

