namespace Mass_Filter_Generator
{
    partial class MGFForm
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
            this.backgroundWorkerMFG = new System.ComponentModel.BackgroundWorker();
            this.timerMFG = new System.Windows.Forms.Timer(this.components);
            this.openFileDialogMFG = new System.Windows.Forms.OpenFileDialog();
            this.cancelButtonMFG = new System.Windows.Forms.Button();
            this.processButtonMFG = new System.Windows.Forms.Button();
            this.resetButtonMFG = new System.Windows.Forms.Button();
            this.selectFolderMFG = new System.Windows.Forms.Button();
            this.openFileResultMFG = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.completedPercentageLabelMFG = new System.Windows.Forms.Label();
            this.elapsedTimeLabelMFG = new System.Windows.Forms.Label();
            this.progressBarMFG = new System.Windows.Forms.ProgressBar();
            this.openFileMFG = new System.Windows.Forms.Button();
            this.outputFileTextBoxMFG = new System.Windows.Forms.TextBox();
            this.currentOutputFilePathMFG = new System.Windows.Forms.TextBox();
            this.openedFileLabelMFG = new System.Windows.Forms.Label();
            this.statusLabelMFG = new System.Windows.Forms.TextBox();
            this.minimumMassFilterInputMFG = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.integrationPeakIntensityMFG = new System.Windows.Forms.RadioButton();
            this.integrationIntegrateSignalMFG = new System.Windows.Forms.RadioButton();
            this.logScaleMFG = new System.Windows.Forms.CheckBox();
            this.absoluteIntensityMFG = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.absoluteWindowMFG = new System.Windows.Forms.RadioButton();
            this.percentWindowMFG = new System.Windows.Forms.RadioButton();
            this.maximumMassFilterInputMFG = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.mzWindowMFG = new System.Windows.Forms.NumericUpDown();
            this.mzWindowLabelMFG = new System.Windows.Forms.Label();
            this.minimumIntensityMFG = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.maximumIntensityMFG = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.minIntensityLabelMFG = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.maxIntensityLabelMFG = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.minimumMassFilterInputMFG)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumMassFilterInputMFG)).BeginInit();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mzWindowMFG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumIntensityMFG)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumIntensityMFG)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorkerMFG
            // 
            this.backgroundWorkerMFG.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorkerMFG_DoWork);
            this.backgroundWorkerMFG.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorkerMFG_ProgressChanged);
            this.backgroundWorkerMFG.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorkerMFG_RunWorkerCompleted);
            // 
            // openFileDialogMFG
            // 
            this.openFileDialogMFG.FileName = "Open a list file";
            this.openFileDialogMFG.Filter = "List files (*.list)|*.list|All files (*.*)|*.*";
            // 
            // cancelButtonMFG
            // 
            this.cancelButtonMFG.Location = new System.Drawing.Point(333, 61);
            this.cancelButtonMFG.Name = "cancelButtonMFG";
            this.cancelButtonMFG.Size = new System.Drawing.Size(75, 23);
            this.cancelButtonMFG.TabIndex = 21;
            this.cancelButtonMFG.Text = "Cancel";
            this.cancelButtonMFG.UseVisualStyleBackColor = true;
            this.cancelButtonMFG.Click += new System.EventHandler(this.cancelButtonMFG_Click);
            // 
            // processButtonMFG
            // 
            this.processButtonMFG.Location = new System.Drawing.Point(252, 62);
            this.processButtonMFG.Name = "processButtonMFG";
            this.processButtonMFG.Size = new System.Drawing.Size(75, 23);
            this.processButtonMFG.TabIndex = 20;
            this.processButtonMFG.Text = "Process";
            this.processButtonMFG.UseVisualStyleBackColor = true;
            this.processButtonMFG.Click += new System.EventHandler(this.processButtonMFG_Click);
            // 
            // resetButtonMFG
            // 
            this.resetButtonMFG.Location = new System.Drawing.Point(87, 61);
            this.resetButtonMFG.Name = "resetButtonMFG";
            this.resetButtonMFG.Size = new System.Drawing.Size(75, 23);
            this.resetButtonMFG.TabIndex = 26;
            this.resetButtonMFG.Text = "Reset";
            this.resetButtonMFG.UseVisualStyleBackColor = true;
            this.resetButtonMFG.Click += new System.EventHandler(this.resetButtonMFG_Click);
            // 
            // selectFolderMFG
            // 
            this.selectFolderMFG.Location = new System.Drawing.Point(168, 61);
            this.selectFolderMFG.Name = "selectFolderMFG";
            this.selectFolderMFG.Size = new System.Drawing.Size(75, 23);
            this.selectFolderMFG.TabIndex = 25;
            this.selectFolderMFG.Text = "Folder";
            this.selectFolderMFG.UseVisualStyleBackColor = true;
            this.selectFolderMFG.Click += new System.EventHandler(this.selectFolderMFG_Click);
            // 
            // openFileResultMFG
            // 
            this.openFileResultMFG.Location = new System.Drawing.Point(412, 61);
            this.openFileResultMFG.Name = "openFileResultMFG";
            this.openFileResultMFG.Size = new System.Drawing.Size(75, 23);
            this.openFileResultMFG.TabIndex = 24;
            this.openFileResultMFG.Text = "Result File";
            this.openFileResultMFG.UseVisualStyleBackColor = true;
            this.openFileResultMFG.Click += new System.EventHandler(this.openFileResultMFG_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(452, 216);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "Completed:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(434, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 27;
            this.label1.Text = "Elapsed time:";
            // 
            // completedPercentageLabelMFG
            // 
            this.completedPercentageLabelMFG.AutoSize = true;
            this.completedPercentageLabelMFG.Location = new System.Drawing.Point(522, 218);
            this.completedPercentageLabelMFG.Name = "completedPercentageLabelMFG";
            this.completedPercentageLabelMFG.Size = new System.Drawing.Size(0, 13);
            this.completedPercentageLabelMFG.TabIndex = 29;
            // 
            // elapsedTimeLabelMFG
            // 
            this.elapsedTimeLabelMFG.AutoSize = true;
            this.elapsedTimeLabelMFG.Location = new System.Drawing.Point(511, 202);
            this.elapsedTimeLabelMFG.Name = "elapsedTimeLabelMFG";
            this.elapsedTimeLabelMFG.Size = new System.Drawing.Size(0, 13);
            this.elapsedTimeLabelMFG.TabIndex = 30;
            // 
            // progressBarMFG
            // 
            this.progressBarMFG.Location = new System.Drawing.Point(17, 203);
            this.progressBarMFG.Name = "progressBarMFG";
            this.progressBarMFG.Size = new System.Drawing.Size(400, 26);
            this.progressBarMFG.TabIndex = 34;
            // 
            // openFileMFG
            // 
            this.openFileMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.openFileMFG.Location = new System.Drawing.Point(6, 61);
            this.openFileMFG.MinimumSize = new System.Drawing.Size(75, 23);
            this.openFileMFG.Name = "openFileMFG";
            this.openFileMFG.Size = new System.Drawing.Size(75, 23);
            this.openFileMFG.TabIndex = 35;
            this.openFileMFG.Text = "Open File";
            this.openFileMFG.UseVisualStyleBackColor = true;
            this.openFileMFG.Click += new System.EventHandler(this.openFileMFG_Click);
            // 
            // outputFileTextBoxMFG
            // 
            this.outputFileTextBoxMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.outputFileTextBoxMFG.Location = new System.Drawing.Point(217, 95);
            this.outputFileTextBoxMFG.MaximumSize = new System.Drawing.Size(200, 20);
            this.outputFileTextBoxMFG.MinimumSize = new System.Drawing.Size(200, 20);
            this.outputFileTextBoxMFG.Name = "outputFileTextBoxMFG";
            this.outputFileTextBoxMFG.Size = new System.Drawing.Size(200, 20);
            this.outputFileTextBoxMFG.TabIndex = 37;
            // 
            // currentOutputFilePathMFG
            // 
            this.currentOutputFilePathMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.currentOutputFilePathMFG.Location = new System.Drawing.Point(12, 121);
            this.currentOutputFilePathMFG.MaximumSize = new System.Drawing.Size(616, 20);
            this.currentOutputFilePathMFG.MinimumSize = new System.Drawing.Size(416, 20);
            this.currentOutputFilePathMFG.Name = "currentOutputFilePathMFG";
            this.currentOutputFilePathMFG.ReadOnly = true;
            this.currentOutputFilePathMFG.Size = new System.Drawing.Size(493, 20);
            this.currentOutputFilePathMFG.TabIndex = 38;
            // 
            // openedFileLabelMFG
            // 
            this.openedFileLabelMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.openedFileLabelMFG.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.openedFileLabelMFG.Location = new System.Drawing.Point(11, 97);
            this.openedFileLabelMFG.MaximumSize = new System.Drawing.Size(200, 20);
            this.openedFileLabelMFG.MinimumSize = new System.Drawing.Size(200, 20);
            this.openedFileLabelMFG.Name = "openedFileLabelMFG";
            this.openedFileLabelMFG.Size = new System.Drawing.Size(200, 20);
            this.openedFileLabelMFG.TabIndex = 36;
            // 
            // statusLabelMFG
            // 
            this.statusLabelMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.statusLabelMFG.Location = new System.Drawing.Point(513, 146);
            this.statusLabelMFG.Multiline = true;
            this.statusLabelMFG.Name = "statusLabelMFG";
            this.statusLabelMFG.ReadOnly = true;
            this.statusLabelMFG.Size = new System.Drawing.Size(252, 50);
            this.statusLabelMFG.TabIndex = 39;
            // 
            // minimumMassFilterInputMFG
            // 
            this.minimumMassFilterInputMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.minimumMassFilterInputMFG.DecimalPlaces = 1;
            this.minimumMassFilterInputMFG.Location = new System.Drawing.Point(3, 3);
            this.minimumMassFilterInputMFG.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.minimumMassFilterInputMFG.MaximumSize = new System.Drawing.Size(80, 0);
            this.minimumMassFilterInputMFG.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.minimumMassFilterInputMFG.MinimumSize = new System.Drawing.Size(80, 0);
            this.minimumMassFilterInputMFG.Name = "minimumMassFilterInputMFG";
            this.minimumMassFilterInputMFG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minimumMassFilterInputMFG.Size = new System.Drawing.Size(80, 20);
            this.minimumMassFilterInputMFG.TabIndex = 22;
            this.minimumMassFilterInputMFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minimumMassFilterInputMFG.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.integrationPeakIntensityMFG, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.integrationIntegrateSignalMFG, 0, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(18, 150);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(104, 46);
            this.tableLayoutPanel2.TabIndex = 42;
            // 
            // integrationPeakIntensityMFG
            // 
            this.integrationPeakIntensityMFG.AutoSize = true;
            this.integrationPeakIntensityMFG.Checked = true;
            this.integrationPeakIntensityMFG.Location = new System.Drawing.Point(3, 3);
            this.integrationPeakIntensityMFG.Name = "integrationPeakIntensityMFG";
            this.integrationPeakIntensityMFG.Size = new System.Drawing.Size(91, 17);
            this.integrationPeakIntensityMFG.TabIndex = 40;
            this.integrationPeakIntensityMFG.TabStop = true;
            this.integrationPeakIntensityMFG.Text = "Peak intensity";
            this.integrationPeakIntensityMFG.UseVisualStyleBackColor = true;
            // 
            // integrationIntegrateSignalMFG
            // 
            this.integrationIntegrateSignalMFG.AutoSize = true;
            this.integrationIntegrateSignalMFG.Location = new System.Drawing.Point(3, 26);
            this.integrationIntegrateSignalMFG.Name = "integrationIntegrateSignalMFG";
            this.integrationIntegrateSignalMFG.Size = new System.Drawing.Size(97, 17);
            this.integrationIntegrateSignalMFG.TabIndex = 41;
            this.integrationIntegrateSignalMFG.TabStop = true;
            this.integrationIntegrateSignalMFG.Text = "Integrate signal";
            this.integrationIntegrateSignalMFG.UseVisualStyleBackColor = true;
            // 
            // logScaleMFG
            // 
            this.logScaleMFG.AutoSize = true;
            this.logScaleMFG.Location = new System.Drawing.Point(522, 61);
            this.logScaleMFG.Name = "logScaleMFG";
            this.logScaleMFG.Size = new System.Drawing.Size(102, 17);
            this.logScaleMFG.TabIndex = 44;
            this.logScaleMFG.Text = "Logaritmic scale";
            this.logScaleMFG.UseVisualStyleBackColor = true;
            // 
            // absoluteIntensityMFG
            // 
            this.absoluteIntensityMFG.AutoSize = true;
            this.absoluteIntensityMFG.Location = new System.Drawing.Point(657, 61);
            this.absoluteIntensityMFG.Name = "absoluteIntensityMFG";
            this.absoluteIntensityMFG.Size = new System.Drawing.Size(108, 17);
            this.absoluteIntensityMFG.TabIndex = 45;
            this.absoluteIntensityMFG.Text = "Absolute intensity";
            this.absoluteIntensityMFG.UseVisualStyleBackColor = true;
            this.absoluteIntensityMFG.CheckedChanged += new System.EventHandler(this.absoluteIntensityMFG_CheckedChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.absoluteWindowMFG, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.percentWindowMFG, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(128, 149);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(74, 46);
            this.tableLayoutPanel3.TabIndex = 41;
            // 
            // absoluteWindowMFG
            // 
            this.absoluteWindowMFG.AutoSize = true;
            this.absoluteWindowMFG.Location = new System.Drawing.Point(3, 26);
            this.absoluteWindowMFG.Name = "absoluteWindowMFG";
            this.absoluteWindowMFG.Size = new System.Drawing.Size(66, 17);
            this.absoluteWindowMFG.TabIndex = 41;
            this.absoluteWindowMFG.TabStop = true;
            this.absoluteWindowMFG.Text = "Absolute";
            this.absoluteWindowMFG.UseVisualStyleBackColor = true;
            // 
            // percentWindowMFG
            // 
            this.percentWindowMFG.AutoSize = true;
            this.percentWindowMFG.Checked = true;
            this.percentWindowMFG.Location = new System.Drawing.Point(3, 3);
            this.percentWindowMFG.Name = "percentWindowMFG";
            this.percentWindowMFG.Size = new System.Drawing.Size(62, 17);
            this.percentWindowMFG.TabIndex = 40;
            this.percentWindowMFG.TabStop = true;
            this.percentWindowMFG.Text = "Percent";
            this.percentWindowMFG.UseVisualStyleBackColor = true;
            this.percentWindowMFG.CheckedChanged += new System.EventHandler(this.percentWindowMFG_CheckedChanged);
            // 
            // maximumMassFilterInputMFG
            // 
            this.maximumMassFilterInputMFG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.maximumMassFilterInputMFG.DecimalPlaces = 1;
            this.maximumMassFilterInputMFG.Location = new System.Drawing.Point(3, 28);
            this.maximumMassFilterInputMFG.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.maximumMassFilterInputMFG.MaximumSize = new System.Drawing.Size(80, 0);
            this.maximumMassFilterInputMFG.MinimumSize = new System.Drawing.Size(80, 0);
            this.maximumMassFilterInputMFG.Name = "maximumMassFilterInputMFG";
            this.maximumMassFilterInputMFG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maximumMassFilterInputMFG.Size = new System.Drawing.Size(80, 20);
            this.maximumMassFilterInputMFG.TabIndex = 46;
            this.maximumMassFilterInputMFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumMassFilterInputMFG.Value = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(87, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Minimum MW";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Maximum MW";
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel5.ColumnCount = 2;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.label5, 1, 1);
            this.tableLayoutPanel5.Controls.Add(this.maximumMassFilterInputMFG, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.minimumMassFilterInputMFG, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel5.Location = new System.Drawing.Point(336, 147);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(169, 50);
            this.tableLayoutPanel5.TabIndex = 50;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel7.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.mzWindowMFG, 0, 1);
            this.tableLayoutPanel7.Controls.Add(this.mzWindowLabelMFG, 1, 1);
            this.tableLayoutPanel7.Location = new System.Drawing.Point(208, 147);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 2;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(119, 50);
            this.tableLayoutPanel7.TabIndex = 52;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 49;
            this.label6.Text = "Mass range (±)";
            // 
            // mzWindowMFG
            // 
            this.mzWindowMFG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.mzWindowMFG.DecimalPlaces = 2;
            this.mzWindowMFG.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.mzWindowMFG.Location = new System.Drawing.Point(3, 28);
            this.mzWindowMFG.MaximumSize = new System.Drawing.Size(80, 0);
            this.mzWindowMFG.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.mzWindowMFG.Name = "mzWindowMFG";
            this.mzWindowMFG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.mzWindowMFG.Size = new System.Drawing.Size(80, 20);
            this.mzWindowMFG.TabIndex = 46;
            this.mzWindowMFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.mzWindowMFG.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // mzWindowLabelMFG
            // 
            this.mzWindowLabelMFG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mzWindowLabelMFG.AutoSize = true;
            this.mzWindowLabelMFG.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mzWindowLabelMFG.Location = new System.Drawing.Point(89, 31);
            this.mzWindowLabelMFG.Name = "mzWindowLabelMFG";
            this.mzWindowLabelMFG.Size = new System.Drawing.Size(21, 13);
            this.mzWindowLabelMFG.TabIndex = 55;
            this.mzWindowLabelMFG.Text = "Da";
            // 
            // minimumIntensityMFG
            // 
            this.minimumIntensityMFG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.minimumIntensityMFG.Location = new System.Drawing.Point(12, 28);
            this.minimumIntensityMFG.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.minimumIntensityMFG.MaximumSize = new System.Drawing.Size(80, 0);
            this.minimumIntensityMFG.Name = "minimumIntensityMFG";
            this.minimumIntensityMFG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.minimumIntensityMFG.Size = new System.Drawing.Size(80, 20);
            this.minimumIntensityMFG.TabIndex = 46;
            this.minimumIntensityMFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.minimumIntensityMFG.ValueChanged += new System.EventHandler(this.minimumIntensityMFG_ValueChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 49;
            this.label7.Text = "Minimum intensity";
            // 
            // maximumIntensityMFG
            // 
            this.maximumIntensityMFG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.maximumIntensityMFG.Location = new System.Drawing.Point(3, 28);
            this.maximumIntensityMFG.MaximumSize = new System.Drawing.Size(80, 0);
            this.maximumIntensityMFG.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maximumIntensityMFG.Name = "maximumIntensityMFG";
            this.maximumIntensityMFG.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.maximumIntensityMFG.Size = new System.Drawing.Size(80, 20);
            this.maximumIntensityMFG.TabIndex = 46;
            this.maximumIntensityMFG.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.maximumIntensityMFG.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.maximumIntensityMFG.ValueChanged += new System.EventHandler(this.maximumIntensityMFG_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(19, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Full intensity";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.minIntensityLabelMFG, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.minimumIntensityMFG, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(513, 89);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(123, 50);
            this.tableLayoutPanel1.TabIndex = 56;
            // 
            // minIntensityLabelMFG
            // 
            this.minIntensityLabelMFG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.minIntensityLabelMFG.AutoSize = true;
            this.minIntensityLabelMFG.Location = new System.Drawing.Point(98, 31);
            this.minIntensityLabelMFG.Name = "minIntensityLabelMFG";
            this.minIntensityLabelMFG.Size = new System.Drawing.Size(15, 13);
            this.minIntensityLabelMFG.TabIndex = 51;
            this.minIntensityLabelMFG.Text = "%";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel4.Controls.Add(this.maxIntensityLabelMFG, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.label8, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.maximumIntensityMFG, 0, 1);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(642, 89);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(123, 50);
            this.tableLayoutPanel4.TabIndex = 57;
            // 
            // maxIntensityLabelMFG
            // 
            this.maxIntensityLabelMFG.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.maxIntensityLabelMFG.AutoSize = true;
            this.maxIntensityLabelMFG.Location = new System.Drawing.Point(89, 31);
            this.maxIntensityLabelMFG.Name = "maxIntensityLabelMFG";
            this.maxIntensityLabelMFG.Size = new System.Drawing.Size(15, 13);
            this.maxIntensityLabelMFG.TabIndex = 52;
            this.maxIntensityLabelMFG.Text = "%";
            // 
            // MGFForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 323);
            this.Controls.Add(this.tableLayoutPanel4);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Controls.Add(this.tableLayoutPanel5);
            this.Controls.Add(this.absoluteIntensityMFG);
            this.Controls.Add(this.logScaleMFG);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel3);
            this.Controls.Add(this.outputFileTextBoxMFG);
            this.Controls.Add(this.currentOutputFilePathMFG);
            this.Controls.Add(this.openedFileLabelMFG);
            this.Controls.Add(this.statusLabelMFG);
            this.Controls.Add(this.openFileMFG);
            this.Controls.Add(this.progressBarMFG);
            this.Controls.Add(this.elapsedTimeLabelMFG);
            this.Controls.Add(this.completedPercentageLabelMFG);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.resetButtonMFG);
            this.Controls.Add(this.selectFolderMFG);
            this.Controls.Add(this.openFileResultMFG);
            this.Controls.Add(this.cancelButtonMFG);
            this.Controls.Add(this.processButtonMFG);
            this.Name = "MGFForm";
            this.Text = "Mass Filter Generator";
            ((System.ComponentModel.ISupportInitialize)(this.minimumMassFilterInputMFG)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maximumMassFilterInputMFG)).EndInit();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mzWindowMFG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minimumIntensityMFG)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maximumIntensityMFG)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorkerMFG;
        private System.Windows.Forms.Timer timerMFG;
        private System.Windows.Forms.OpenFileDialog openFileDialogMFG;
        private System.Windows.Forms.NumericUpDown minimumMassFilterInputMFG;
        private System.Windows.Forms.Button cancelButtonMFG;
        private System.Windows.Forms.Button processButtonMFG;
        private System.Windows.Forms.Button resetButtonMFG;
        private System.Windows.Forms.Button selectFolderMFG;
        private System.Windows.Forms.Button openFileResultMFG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label completedPercentageLabelMFG;
        private System.Windows.Forms.Label elapsedTimeLabelMFG;
        private System.Windows.Forms.ProgressBar progressBarMFG;
        private System.Windows.Forms.Button openFileMFG;
        private System.Windows.Forms.TextBox outputFileTextBoxMFG;
        private System.Windows.Forms.TextBox currentOutputFilePathMFG;
        private System.Windows.Forms.Label openedFileLabelMFG;
        private System.Windows.Forms.TextBox statusLabelMFG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RadioButton integrationPeakIntensityMFG;
        private System.Windows.Forms.RadioButton integrationIntegrateSignalMFG;
        private System.Windows.Forms.CheckBox logScaleMFG;
        private System.Windows.Forms.CheckBox absoluteIntensityMFG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RadioButton percentWindowMFG;
        private System.Windows.Forms.RadioButton absoluteWindowMFG;
        private System.Windows.Forms.NumericUpDown maximumMassFilterInputMFG;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.NumericUpDown mzWindowMFG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown minimumIntensityMFG;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown maximumIntensityMFG;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label mzWindowLabelMFG;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label minIntensityLabelMFG;
        private System.Windows.Forms.Label maxIntensityLabelMFG;
    }
}

