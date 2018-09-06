using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Mass_Filter_Generator
{
    public partial class MGFForm : Form
    {
        private readonly Stopwatch stopwatchMFG = new Stopwatch();
        public int startingLineMFG = 0;
        public string outputPathMFG;
        public string outputFolderMFG;
        public string outputFileNameMFG;
        public string openedFileMFG = "";

        public MGFForm()
        {
            InitializeComponent();
            elapsedTimeLabelMFG.Text = "";
            completedPercentageLabelMFG.Text = "";
            openFileResultMFG.Enabled = false;            
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerMFG.WorkerReportsProgress = true;

            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerMFG.WorkerSupportsCancellation = true;
        }

        private void openFileMFG_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogMFG.ShowDialog(); //Open File dialog 
            if (result == DialogResult.OK)
            {
                openedFileLabelMFG.Text = openFileDialogMFG.SafeFileName; //Write on label which file is opened
                openedFileMFG = openFileDialogMFG.FileName;
                openFileResultMFG.Enabled = false;
                if (currentOutputFilePathMFG.Text == "" && currentOutputFilePathMFG.Text == "")
                {
                    outputFileNameMFG = Path.GetFileNameWithoutExtension(openFileDialogMFG.FileName);
                    outputFileTextBoxMFG.Text = outputFileNameMFG;
                    outputFolderMFG = Path.GetDirectoryName(openFileDialogMFG.FileName);
                }
                outputPathMFG = (outputFolderMFG + @"\" + outputFileNameMFG + ".mir");
                currentOutputFilePathMFG.Text = outputPathMFG;
            }
        }

        private void backgroundWorkerMFG_DoWork(object sender, DoWorkEventArgs e)
        {
            string firstString = "<ImagingResults flexImagingVersion=\"2.1.15.0\" last_modified=\"" + DateTime.Now.ToString("s") + "\"" + ">";
            List<string> arrayToAppend = new List<string>();
            arrayToAppend.Add(firstString);

            File.AppendAllLines(outputPathMFG, arrayToAppend);
            arrayToAppend.Clear();
            int lineCount = File.ReadLines(openedFileMFG).Count();
            var reader = new StreamReader(openedFileMFG);
            double minimumMassFilter = Convert.ToDouble(minimumMassFilterInputMFG.Value);
            double maximumMassFilter = Convert.ToDouble(maximumMassFilterInputMFG.Value);            
            var randomColor = new Random();
            stopwatchMFG.Restart();
            for (int i = 1; i < (lineCount+1); i++)
            {                                  
                var line = reader.ReadLine();
                string lineToAppend = "<Result Type=\"PkFilter\" ";
                if (backgroundWorkerMFG.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                double currentMass;
                bool isDouble = Double.TryParse(line, out currentMass);
                
                if (Double.TryParse(line, out currentMass)) //check if the line is a umber of type double
                {
                    if (currentMass >= minimumMassFilter && currentMass <= maximumMassFilter) // check if the number is within the specified MW limits
                    {
                        string entryName = "Name=" + "\"" + currentMass.ToString() + "\" ";                        
                        string color = "Color=" + "\"" + String.Format("#{0:X6}", randomColor.Next(0x1000000)) + "\" " + "Show=" + "\"" + "1" + "\" ";
                        string minIntensity = "MinIntensity=" + "\"" + minimumIntensityMFG.Value.ToString() + "\" ";
                        string maxIntensity = "IntensityThreshold=" + "\"" + maximumIntensityMFG.Value.ToString() + "\" ";                       
                        string absoluteIntensity = "AbsIntens=" + "\"" + (absoluteIntensityMFG.Checked ? "1" : "0") + "\" ";                        
                        string logScale = "LogScale=" + "\"" + (logScaleMFG.Checked ? "1" : "0") + "\" ";
                        string minusWindow = "";
                        string pluswindow = "";

                        if (percentWindowMFG.Checked)
                        {
                            double minusWindowPercent = (currentMass - (currentMass / 100 * (double)mzWindowMFG.Value));
                            double plusWindowPercent = (currentMass + (currentMass / 100 * (double)mzWindowMFG.Value));
                            if (minusWindowPercent > 0)
                            {
                                minusWindow = minusWindowPercent.ToString();
                                pluswindow = plusWindowPercent.ToString();
                            }
                            else
                            {
                                MessageBox.Show("% The selected filter will result in negative values for the value at line #:" + i.ToString());
                                break;
                            }
                            

                        }
                        else
                        {
                            double minusWindowAbsolute = (currentMass - (double)mzWindowMFG.Value);
                            double plusWindowAbsolute = (currentMass + (double)mzWindowMFG.Value);
                            if (minusWindowAbsolute > 0)
                            {
                                minusWindow = minusWindowAbsolute.ToString();
                                pluswindow = plusWindowAbsolute.ToString();
                            }
                            else
                            {
                                MessageBox.Show("Abs The selected filter will result in negative values for the mass at line #" + i.ToString());
                                break;
                            }
                        }
                        string minMass = "MinMass=" + "\"" + minusWindow + "\" ";
                        string maxMass = "MaxMass=" + "\"" + pluswindow + "\" ";
                        string integrate = "Integrate=" + (integrationPeakIntensityMFG.Checked ? "0" : "1") + "\" FindMass =" + "\"" + 0 + "\" ";
                        string relMass = "RelMass=" + (percentWindowMFG.Checked ? "1" : "0") + "\"/>";

                        lineToAppend += entryName + color + minIntensity + maxIntensity + absoluteIntensity + logScale + minMass + maxMass + integrate + relMass;
                        arrayToAppend.Add(lineToAppend);
                        File.AppendAllLines(outputPathMFG, arrayToAppend);
                        arrayToAppend.Clear();

                    }
                }

                else
                {
                    MessageBox.Show("The mass value on line #" + i.ToString() + "is not a valid mass", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                int percentage = (startingLineMFG++) * 100 / lineCount;
                Console.WriteLine(startingLineMFG);
                backgroundWorkerMFG.ReportProgress(percentage);
            }
            string lastString = "</ImagingResults>";
            arrayToAppend.Add(lastString);
            File.AppendAllLines(outputPathMFG, arrayToAppend);
            arrayToAppend.Clear();
            stopwatchMFG.Stop();
        }

        private void backgroundWorkerMFG_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMFG.Value = e.ProgressPercentage;
            if (statusLabelMFG.Text != "Work in progress...")
            {
                statusLabelMFG.Text = "Work in progress...";
            }
        }

        private void backgroundWorkerMFG_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            timerMFG.Stop();
            if (e.Cancelled)
            {
                statusLabelMFG.Text = ("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                statusLabelMFG.Text = ("Error. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                progressBarMFG.Value = 100;
                completedPercentageLabelMFG.Text = ("100%");
                elapsedTimeLabelMFG.Text = stopwatchMFG.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelMFG.Text = "Work completed.";
                openFileResultMFG.Enabled = true;
                processButtonMFG.Enabled = true;
                selectFolderMFG.Enabled = true;
                resetButtonMFG.Enabled = true;
                minimumMassFilterInputMFG.Enabled = true;
                openFileMFG.Enabled = true;
            }
        }

        private void processButtonMFG_Click(object sender, EventArgs e)
        {

            if (String.IsNullOrEmpty(openedFileMFG))
            {
                MessageBox.Show("You must open a file first.");
            }
            else
            {
                if (File.Exists(outputPathMFG))
                {
                    if (MessageBox.Show("The output file exists" + "Are you sure you want to overwrite it?", "Alert", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        progressBarMFG.Value = 0;
                        elapsedTimeLabelMFG.Text = "";
                        completedPercentageLabelMFG.Text = "";
                        startingLineMFG = 0;
                        processButtonMFG.Enabled = false;
                        openFileResultMFG.Enabled = false;
                        selectFolderMFG.Enabled = false;
                        resetButtonMFG.Enabled = false;
                        minimumMassFilterInputMFG.Enabled = false;
                        openFileMFG.Enabled = false;
                        timerMFG.Start();
                        backgroundWorkerMFG.RunWorkerAsync();
                    }
                }
                else
                {
                    progressBarMFG.Value = 0;
                    elapsedTimeLabelMFG.Text = "";
                    completedPercentageLabelMFG.Text = "";
                    startingLineMFG = 0;
                    processButtonMFG.Enabled = false;
                    openFileResultMFG.Enabled = false;
                    selectFolderMFG.Enabled = false;
                    resetButtonMFG.Enabled = false;
                    minimumMassFilterInputMFG.Enabled = false;
                    openFileMFG.Enabled = false;
                    timerMFG.Start();
                    backgroundWorkerMFG.RunWorkerAsync();
                }
            }

        }

        private void cancelButtonMFG_Click(object sender, EventArgs e)
        {
            backgroundWorkerMFG.CancelAsync();
            openFileResultMFG.Enabled = false;
            processButtonMFG.Enabled = true;
            selectFolderMFG.Enabled = true;
            resetButtonMFG.Enabled = true;
            minimumMassFilterInputMFG.Enabled = true;
            openFileMFG.Enabled = true;
        }

        private void massFilterDefaultMFG_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["DefaultMassFilterMFG"] = minimumMassFilterInputMFG.Value;
            Properties.Settings.Default.Save();
        }

        private void openFileResultMFG_Click(object sender, EventArgs e)
        {
            if (File.Exists(outputPathMFG))
            {
                Process.Start(outputPathMFG);
            }
            else
            {
                MessageBox.Show("The file does not exists", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetButtonMFG_Click(object sender, EventArgs e)
        {
            minimumMassFilterInputMFG.Value = Properties.Settings.Default.DefaultMassFilterMFG;
            elapsedTimeLabelMFG.Text = "";
            completedPercentageLabelMFG.Text = "";
            openedFileMFG = "";
            openedFileLabelMFG.Text = "";
            statusLabelMFG.Text = "";
            outputFileTextBoxMFG.Text = "";
            currentOutputFilePathMFG.Text = "";
            openFileResultMFG.Enabled = false;
            processButtonMFG.Enabled = true;
            selectFolderMFG.Enabled = true;
            resetButtonMFG.Enabled = true;
            minimumMassFilterInputMFG.Enabled = true;
            openFileMFG.Enabled = true;
            progressBarMFG.Value = 0;
        }

        private void selectFolderMFG_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (outputFolderMFG != null)
            {
                dialog.InitialDirectory = outputFolderMFG;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFolderMFG = dialog.FileName;
                outputPathMFG = (outputFolderMFG + @"\" + outputFileNameMFG + "_filtered.fasta");
                currentOutputFilePathMFG.Text = outputPathMFG;
            }
        }

        private void outputFileTextBoxMFG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                outputFileNameMFG = outputFileTextBoxMFG.Text;
                outputPathMFG = (outputFolderMFG + @"\" + outputFileNameMFG + "_filtered.fasta");
                currentOutputFilePathMFG.Text = outputPathMFG;
            }
        }

        private void timerMFG_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabelMFG.Text = stopwatchMFG.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabelMFG.Text = (progressBarMFG.Value.ToString() + "%");
        }

        private void percentWindowMFG_CheckedChanged(object sender, EventArgs e)
        {
            if (percentWindowMFG.Checked)
            {
                mzWindowMFG.DecimalPlaces = 2;
                mzWindowMFG.Increment = 0.01M;
                mzWindowMFG.Maximum = 100;
                mzWindowMFG.Minimum = 0.01M;
                mzWindowMFG.Value = 0.01M;
                mzWindowLabelMFG.Text = "%";
            }
            else
            {
                mzWindowMFG.DecimalPlaces = 3;
                mzWindowMFG.Increment = 0.001M;
                mzWindowMFG.Maximum = 10000000000;
                mzWindowMFG.Minimum = 0.001M;
                mzWindowMFG.Value = 0.001M;
                mzWindowLabelMFG.Text = "Da";
            }
        }

        private void absoluteIntensityMFG_CheckedChanged(object sender, EventArgs e)
        {
            if (absoluteIntensityMFG.Checked)
            {
                minIntensityLabelMFG.Text = "a.u.";
                maxIntensityLabelMFG.Text = "a.u.";
                maximumIntensityMFG.Maximum = 1000000;
                minimumIntensityMFG.Value = 0;
                maximumIntensityMFG.Value = 100;
            }
            else
            {
                minIntensityLabelMFG.Text = "%";
                maxIntensityLabelMFG.Text = "%";
                maximumIntensityMFG.Maximum = 100;
                minimumIntensityMFG.Value = 0;
                maximumIntensityMFG.Value = 100;
            }
                
        }

        private void minimumIntensityMFG_ValueChanged(object sender, EventArgs e)
        {
            if (minimumIntensityMFG.Value > maximumIntensityMFG.Value)
            {
                minimumIntensityMFG.Value = 0;
            }

        }

        private void maximumIntensityMFG_ValueChanged(object sender, EventArgs e)
        {
            if (minimumIntensityMFG.Value > maximumIntensityMFG.Value)
            {
                maximumIntensityMFG.Value = minimumIntensityMFG.Value + 1;
            }
        }
    }
}
