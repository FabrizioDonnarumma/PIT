using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using FileHelpers;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using LinqStatistics;
using System.Net;
using System.Xml;
using System.Windows.Threading;

namespace Proteomics_Imaging_Tools
{
    public partial class ProteomicsImagingTools : Form
    {
        //General variables and constants
        string contactEmail = "fabrizio@lsu.edu";
        public string tempFolderPath = Path.GetTempPath();
        string applicationPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);

        //PTM Parser variables and constants        
        private readonly Stopwatch stopwatchPTMParser = new Stopwatch();
        public int startingLinePTMParser = 0;
        public string openedFilePTMParser = "";
        public int startingPoint = 0;
        public int ptmlength = 0;
        public string sequenceToWrite;
        List<string> arrayToWrite = new List<string>();
        public string modifiedEntry;
        public string tempOutputPathPTMParser;
        public string outputPathPTMParser;
        public string outputFolderPTMParser;
        public string outputFileNamePTMParser;
        public string review = "";

        //MW calculator variables and constants
        private readonly Stopwatch stopwatchMWCalculator = new Stopwatch();
        public int startingLineMWCalculator = 0;
        public string openedFileMWCalculator = "";
        List<string> aaLetter = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "V", "W", "Y", "X", "Z", "U", "O" };
        List<double> aaMasses = new List<double> { 71.0788, 114.5948, 103.1388, 115.0886, 129.1155, 147.1766, 57.0519, 137.1411, 113.1594, 128.1741, 113.1594, 131.1926, 114.1038, 97.1167, 128.1367, 156.1875, 87.0782, 101.1051, 99.1326, 186.2132, 163.1760, 110.1448, 128.6248, 150.04, 237.30 };
        List<string> sequenceArray = new List<string>();
        public int massFilter;
        public string tempOutputPathMWCalculator;
        public string outputPathMWCalculator;
        public string outputFolderMWCalculator;
        public string outputFileNameMWCalculator;

        //MFG variables and constants
        private readonly Stopwatch stopwatchMFG = new Stopwatch();
        public int startingLineMFG = 0;
        public string tempOutputPathMFG;
        public string outputPathMFG;
        public string outputFolderMFG;
        public string outputFileNameMFG;
        public string openedFileMFG = "";

        //MLA variables and constants
        private readonly Stopwatch stopwatchMLA = new Stopwatch();
        public string inputFolderMLA = "";
        public string outputFolderMLA = "";
        public string outputPathMLA;
        public string rawOutputPathMLA;
        public string outputFileNameMLA = "Results";
        public int startingFileMLA = 0;

        //PF variables
        private readonly Stopwatch stopwatchPF = new Stopwatch();
        public string inputFolderPF = "";
        public string outputPathPF = "";
        public string databaseOutputPathPF;
        public string outputFileNamePF = "Results";
        public int startingFilePF = 0;
        public int selectedDBIndex = -1;
        public string selectedDBText = "";

        //PA variables and constants
        public string openedFile;
        public string outputFile;
        public string tempFilePath = Path.GetTempPath();



        public ProteomicsImagingTools()
        {
            InitializeComponent();
            elapsedTimeLabelPTMParser.Text = "";
            completedPercentageLabelPTMParser.Text = "";
            openFileResultPTMParser.Enabled = false;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerPTMParser.WorkerReportsProgress = true;
            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerPTMParser.WorkerSupportsCancellation = true;

            elapsedTimeLabelMWCalculator.Text = "";
            completedPercentageLabelMWCalculator.Text = "";
            openFileResultMWCalculator.Enabled = false;
            massFilterInputMWCalculator.Value = 10000000;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerMWCalculator.WorkerReportsProgress = true;
            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerMWCalculator.WorkerSupportsCancellation = true;

            elapsedTimeLabelMFG.Text = "";
            completedPercentageLabelMFG.Text = "";
            openFileResultMFG.Enabled = false;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerMFG.WorkerReportsProgress = true;
            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerMFG.WorkerSupportsCancellation = true;

            elapsedTimeLabelMLA.Text = "";
            completedPercentageLabelMLA.Text = "";
            openFileResultsMLA.Enabled = false;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerMLA.WorkerReportsProgress = true;
            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerMLA.WorkerSupportsCancellation = true;

            elapsedTimeLabelPF.Text = "";
            completedPercentageLabelPF.Text = "";
            ResultsButtonPF.Enabled = false;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            backgroundWorkerPF.WorkerReportsProgress = true;
            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            backgroundWorkerPF.WorkerSupportsCancellation = true;

        }

        //******************************************************************************************************
        //********************************************** PTM Parser ********************************************
        //******************************************************************************************************

        public static IEnumerable<string> SplitByLength(string str, int maxLength) // This will split a string in chunks
        {
            for (int index = 0; index < str.Length; index += maxLength)
            {
                yield return str.Substring(index, Math.Min(maxLength, str.Length - index));
            }
        }

        private void backgroundWorkerPTMParser_DoWork(object sender, DoWorkEventArgs e)
        {
            startingLinePTMParser = 0;
            var engine = new FileHelperEngine<fileToRead>();
            var fileArray = engine.ReadFile(openedFilePTMParser);
            var lineCount = File.ReadLines(openedFilePTMParser).Count();
            stopwatchPTMParser.Restart();
            foreach (var line in fileArray)
            {
                if (line.entry == "Entry")
                {
                    continue;
                }

                if (backgroundWorkerPTMParser.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                else if (line.chain != null)
                {
                    string[] chainArray = Regex.Split(line.chain, @"(?=CHAIN)");

                    foreach (string element in chainArray)
                    {
                        if (element != "")
                        {
                            arrayToWrite.Clear();
                            Match questionMarkCheck = Regex.Match(element, @"\?");
                            if (questionMarkCheck.Value == "?") // this checks that at least 2 numbers have been found 
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = Regex.Matches(element, @"\d+");
                                startingPoint = Int32.Parse(matches[0].ToString());
                                ptmlength = Int32.Parse(matches[1].ToString()) - startingPoint;
                                sequenceToWrite = line.sequence.Substring(startingPoint - 1, ptmlength + 1);
                                modifiedEntry = (line.entry + "_CHAIN_" + matches[0].ToString() + "_" + matches[1].ToString());
                                if (line.reviewStatus == "reviewed")
                                {
                                    review = "sp";
                                }
                                else
                                {
                                    review = "tr";
                                }
                                arrayToWrite.Add(">" + review + "|" + modifiedEntry + "|" + line.entryName + " " + line.proteinNames);
                                if (sequenceToWrite.Length >= 60)
                                {
                                    foreach (string s in SplitByLength(sequenceToWrite, 60))
                                    {
                                        arrayToWrite.Add(s);
                                    }
                                }
                                else
                                {
                                    arrayToWrite.Add(sequenceToWrite);
                                }
                                File.AppendAllLines(tempOutputPathPTMParser, arrayToWrite);
                            }
                        }
                    }


                }

                if (line.peptide != null)
                {
                    string[] chainArray = Regex.Split(line.peptide, @"(?=PEPTIDE)");

                    foreach (string element in chainArray)
                    {
                        if (element != "")
                        {
                            arrayToWrite.Clear();
                            Match questionMarkCheck = Regex.Match(element, @"\?");
                            if (questionMarkCheck.Value == "?") // this checks that at least 2 numbers have been found 
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = Regex.Matches(element, @"\d+");
                                startingPoint = Int32.Parse(matches[0].ToString());
                                ptmlength = Int32.Parse(matches[1].ToString()) - startingPoint;
                                sequenceToWrite = line.sequence.Substring(startingPoint - 1, ptmlength + 1);
                                modifiedEntry = (line.entry + "_PEPTIDE_" + matches[0].ToString() + "_" + matches[1].ToString());
                                if (line.reviewStatus == "reviewed")
                                {
                                    review = "sp";
                                }
                                else
                                {
                                    review = "tr";
                                }
                                arrayToWrite.Add(">" + review + "|" + modifiedEntry + "|" + line.entryName + " " + line.proteinNames);
                                if (sequenceToWrite.Length >= 60)
                                {
                                    foreach (string s in SplitByLength(sequenceToWrite, 60))
                                    {
                                        arrayToWrite.Add(s);
                                    }
                                }
                                else
                                {
                                    arrayToWrite.Add(sequenceToWrite);
                                }
                                File.AppendAllLines(tempOutputPathPTMParser, arrayToWrite);
                            }
                        }
                    }
                }

                if (line.propeptide != null)
                {
                    string[] chainArray = Regex.Split(line.propeptide, @"(?=PROPEP)");

                    foreach (string element in chainArray)
                    {
                        if (element != "")
                        {
                            arrayToWrite.Clear();
                            Match questionMarkCheck = Regex.Match(element, @"\?");
                            if (questionMarkCheck.Value == "?") // this checks that at least 2 numbers have been found 
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = Regex.Matches(element, @"\d+");
                                startingPoint = Int32.Parse(matches[0].ToString());
                                ptmlength = Int32.Parse(matches[1].ToString()) - startingPoint;
                                sequenceToWrite = line.sequence.Substring(startingPoint - 1, ptmlength + 1);
                                modifiedEntry = (line.entry + "_PROPEP_" + matches[0].ToString() + "_" + matches[1].ToString());
                                if (line.reviewStatus == "reviewed")
                                {
                                    review = "sp";
                                }
                                else
                                {
                                    review = "tr";
                                }
                                arrayToWrite.Add(">" + review + "|" + modifiedEntry + "|" + line.entryName + " " + line.proteinNames);
                                if (sequenceToWrite.Length >= 60)
                                {
                                    foreach (string s in SplitByLength(sequenceToWrite, 60))
                                    {
                                        arrayToWrite.Add(s);
                                    }
                                }
                                else
                                {
                                    arrayToWrite.Add(sequenceToWrite);
                                }
                                File.AppendAllLines(tempOutputPathPTMParser, arrayToWrite);
                            }
                        }
                    }
                }

                if (line.signalPeptide != null)
                {
                    string[] chainArray = Regex.Split(line.signalPeptide, @"(?=SIGNAL)");

                    foreach (string element in chainArray)
                    {
                        if (element != "")
                        {
                            arrayToWrite.Clear();
                            Match questionMarkCheck = Regex.Match(element, @"\?");
                            if (questionMarkCheck.Value == "?") // this checks that no ? has been found
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = Regex.Matches(element, @"\d+");
                                startingPoint = Int32.Parse(matches[0].ToString());
                                ptmlength = Int32.Parse(matches[1].ToString()) - startingPoint;
                                sequenceToWrite = line.sequence.Substring(startingPoint - 1, ptmlength + 1);
                                modifiedEntry = (line.entry + "_SIGNAL_" + matches[0].ToString() + "_" + matches[1].ToString());
                                if (line.reviewStatus == "reviewed")
                                {
                                    review = "sp";
                                }
                                else
                                {
                                    review = "tr";
                                }
                                arrayToWrite.Add(">" + review + "|" + modifiedEntry + "|" + line.entryName + " " + line.proteinNames);
                                if (sequenceToWrite.Length >= 60)
                                {
                                    foreach (string s in SplitByLength(sequenceToWrite, 60))
                                    {
                                        arrayToWrite.Add(s);
                                    }
                                }
                                else
                                {
                                    arrayToWrite.Add(sequenceToWrite);
                                }
                                File.AppendAllLines(tempOutputPathPTMParser, arrayToWrite);
                            }
                        }
                    }
                }

                if (line.transitPeptide != null)
                {
                    string[] chainArray = Regex.Split(line.transitPeptide, @"(?=TRANSIT)");

                    foreach (string element in chainArray)
                    {
                        if (element != "")
                        {
                            arrayToWrite.Clear();
                            Match questionMarkCheck = Regex.Match(element, @"\?");
                            if (questionMarkCheck.Value == "?") // this checks that at least 2 numbers have been found 
                            {
                                continue;
                            }
                            else
                            {
                                MatchCollection matches = Regex.Matches(element, @"\d+");
                                startingPoint = Int32.Parse(matches[0].ToString());
                                ptmlength = Int32.Parse(matches[1].ToString()) - startingPoint;
                                sequenceToWrite = line.sequence.Substring(startingPoint - 1, ptmlength + 1);
                                modifiedEntry = (line.entry + "_TRANSIT_" + matches[0].ToString() + "_" + matches[1].ToString());
                                if (line.reviewStatus == "reviewed")
                                {
                                    review = "sp";
                                }
                                else
                                {
                                    review = "tr";
                                }
                                arrayToWrite.Add(">" + review + "|" + modifiedEntry + "|" + line.entryName + " " + line.proteinNames);
                                if (sequenceToWrite.Length >= 60)
                                {
                                    foreach (string s in SplitByLength(sequenceToWrite, 60))
                                    {
                                        arrayToWrite.Add(s);
                                    }
                                }
                                else
                                {
                                    arrayToWrite.Add(sequenceToWrite);
                                }
                                File.AppendAllLines(tempOutputPathPTMParser, arrayToWrite);
                            }
                        }
                    }
                }


                int percentage = (startingLinePTMParser++) * 100 / lineCount;
                backgroundWorkerPTMParser.ReportProgress(percentage);
            }
            stopwatchPTMParser.Stop();
        }

        private void backgroundWorkerPTMParser_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarPTMParser.Value = e.ProgressPercentage;
            if (statusLabelPTMParser.Text != "Work in progress...")
            {
                statusLabelPTMParser.Text = "Work in progress...";
            }

        }

        private void BackgroundWorkerPTMParser_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            processButtonPTMParser.Enabled = true;
            timerPTMParser.Stop();
            if (e.Cancelled)
            {
                statusLabelPTMParser.Text = ("The task has been cancelled");
                File.Delete(tempOutputPathPTMParser);
            }
            else if (e.Error != null)
            {
                statusLabelPTMParser.Text = ("Error. Details: " + (e.Error as Exception).ToString());
                File.Delete(tempOutputPathPTMParser);
            }
            else
            {
                progressBarPTMParser.Value = 100;
                completedPercentageLabelPTMParser.Text = ("100%");
                elapsedTimeLabelPTMParser.Text = stopwatchPTMParser.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelPTMParser.Text = "Work completed.";
                openFileResultPTMParser.Enabled = true;
                selectFolderPTMParser.Enabled = true;
                processButtonPTMParser.Enabled = true;
                resetButtonPTMParser.Enabled = true;
                openFilePTMParser.Enabled = true;
                //This block implement the merging capability
                if (MergeDBPTMParser.Checked)
                {
                    if (selectDBToMerge.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        using (Stream input = File.OpenRead(selectDBToMerge.FileName))

                        using (Stream output = new FileStream(tempOutputPathPTMParser, FileMode.Append, FileAccess.Write, FileShare.None))
                        {
                            input.CopyTo(output);
                        }

                        File.Move(tempOutputPathPTMParser, outputPathPTMParser);
                    }

                }
                else
                {
                    File.Move(tempOutputPathPTMParser, outputPathPTMParser);        
                }
               
            }
        }

        private void OpenFilePTMParser_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogPTMParser.ShowDialog(); //Open File dialog 
            if (result == DialogResult.OK)
            {
                openedFileLabelPTMParser.Text = openFileDialogPTMParser.SafeFileName; //Write on label which file is opened
                openedFilePTMParser = openFileDialogPTMParser.FileName;
                openFileResultPTMParser.Enabled = false;
                if (currentOutputFileNamePTMParser.Text == "" && outputFileTextBoxPTMParser.Text == "")
                {
                    outputFileNamePTMParser = Path.GetFileNameWithoutExtension(openFileDialogPTMParser.FileName);
                    currentOutputFileNamePTMParser.Text = outputFileNamePTMParser;
                    outputFolderPTMParser = Path.GetDirectoryName(openFileDialogPTMParser.FileName);
                }
                outputPathPTMParser = (outputFolderPTMParser + @"\" + outputFileNamePTMParser + ".fasta");
                outputFileTextBoxPTMParser.Text = outputPathPTMParser;
            }
        }

        private void CancelButtonPTMParser_Click(object sender, EventArgs e)
        {
            backgroundWorkerPTMParser.CancelAsync();
            openFileResultPTMParser.Enabled = false;
            openFilePTMParser.Enabled = true;
            processButtonPTMParser.Enabled = true;
            selectFolderMWCalculator.Enabled = true;
            resetButtonPTMParser.Enabled = true;
        }

        private void processButtonPTMParser_Click(object sender, EventArgs e) // add checks for existing file
        {
            if (String.IsNullOrEmpty(openedFilePTMParser))
            {
                MessageBox.Show("You must open a file first.");
            }
            else
            {
                if (File.Exists(outputPathPTMParser))
                {
                    MessageBox.Show("The output file exists" + Environment.NewLine + "Please select a different file name or a different folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    processButtonPTMParser.Enabled = false;
                    openFileResultPTMParser.Enabled = false;
                    openFilePTMParser.Enabled = false;
                    selectFolderPTMParser.Enabled = false;
                    resetButtonPTMParser.Enabled = false;
                    startingLinePTMParser = 0;
                    tempOutputPathPTMParser = tempFolderPath + @"\" + outputFileNamePTMParser + ".temp";
                    timerPTMParser.Start();
                    backgroundWorkerPTMParser.RunWorkerAsync();
                }
            }

        }

        private void timerPTMParser_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabelPTMParser.Text = stopwatchPTMParser.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabelPTMParser.Text = (progressBarPTMParser.Value.ToString() + "%");
        }

        private void openFileResultPTMParser_Click(object sender, EventArgs e)
        {
            if (File.Exists(outputPathPTMParser))
            {
                Process.Start(outputPathPTMParser);
            }
            else
            {
                MessageBox.Show("The file does not exists", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void selectFolderPTMParser_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (outputFolderPTMParser != null)
            {
                dialog.InitialDirectory = outputFolderPTMParser;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFolderPTMParser = dialog.FileName;
                outputPathPTMParser = (outputFolderPTMParser + @"\" + outputFileNamePTMParser + ".fasta");
                outputFileTextBoxPTMParser.Text = outputPathPTMParser;
            }
        }

        private void outputFileTextBoxPTMParser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                outputFileNamePTMParser = currentOutputFileNamePTMParser.Text;
                outputPathPTMParser = (outputFolderPTMParser + @"\" + outputFileNamePTMParser + ".fasta");
                outputFileTextBoxPTMParser.Text = outputPathPTMParser;
            }
        }

        private void resetButtonPTMParser_Click(object sender, EventArgs e)
            {
                elapsedTimeLabelPTMParser.Text = "";
                completedPercentageLabelPTMParser.Text = "";
                openFileResultPTMParser.Enabled = false;
                statusLabelPTMParser.Text = "";
                openedFileLabelPTMParser.Text = "";
                currentOutputFileNamePTMParser.Text = "";
                outputFileTextBoxPTMParser.Text = "";
                openedFilePTMParser = "";
                openFilePTMParser.Enabled = true;
                processButtonPTMParser.Enabled = true;
                selectFolderPTMParser.Enabled = true;
                progressBarPTMParser.Value = 0;
            }

        //******************************************************************************************************
        //******************************************** PIT General *********************************************
        //******************************************************************************************************

        private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Proteomic and Imaging Tools" + Environment.NewLine + "Version 1.0.1" + Environment.NewLine + "© 2017 Murray Group, LSU" + Environment.NewLine + "Contact: fabrizio@lsu.edu", "About Proteomic Database Parser", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void userManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string userGuideFolder = applicationPath + @"\Documents\PIT User Guide.pdf";
            System.Diagnostics.Process.Start(userGuideFolder);
        }

        private void sendFeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + contactEmail + "?Subject=Proteomics%20and%20Imaging%20Tools%20-%20General%20question");
        }

        private void reportABugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + contactEmail + "?Subject=Proteomics%20and%20Imaging%20Tools%20-%20Bug%20report");
        }

        private void requestAFeatureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("mailto:" + contactEmail + "?Subject=Proteomics%20and%20Imaging%20Tools%20-%20Feature%20request");
        }

        //******************************************************************************************************
        //******************************************* MW Calculator ********************************************
        //******************************************************************************************************

        private void OpenFileMWCalculator_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialogMWCalculator.ShowDialog(); //Open File dialog 
            if (result == DialogResult.OK)
            {
                openedFileLabelMWCalculator.Text = openFileDialogMWCalculator.SafeFileName; //Write on label which file is opened
                openedFileMWCalculator = openFileDialogMWCalculator.FileName;
                openFileResultMWCalculator.Enabled = false;
                if (currentOutputFilePathMWCalculator.Text == "" && outputFileTextBoxMWCalculator.Text == "")
                {
                    outputFileNameMWCalculator = Path.GetFileNameWithoutExtension(openFileDialogMWCalculator.FileName);
                    outputFileTextBoxMWCalculator.Text = outputFileNameMWCalculator;
                    outputFolderMWCalculator = Path.GetDirectoryName(openFileDialogMWCalculator.FileName);
                }
                outputPathMWCalculator = (outputFolderMWCalculator + @"\" + outputFileNameMWCalculator + "_filtered.fasta");
                currentOutputFilePathMWCalculator.Text = outputPathMWCalculator;
            }
        }

        private void backgroundWorkerMWCalculator_DoWork(object sender, DoWorkEventArgs e)
        {
            if (File.ReadLines(openedFileMWCalculator).Last() != "")
            {
                List<string> emptyLines = new List<string>()
                {
                    "",
                };
                File.AppendAllLines(openedFileMWCalculator, emptyLines);
            }
            int lineCount = File.ReadLines(openedFileMWCalculator).Count() + 1;
            var reader = new StreamReader(openedFileMWCalculator);
            string currentProtein = "";
            string currentSequence = "";
            stopwatchMWCalculator.Restart();
            for (int i = 1; i < lineCount; i++)
            {
                var line = reader.ReadLine();
                if (backgroundWorkerMWCalculator.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                else if (line.StartsWith(">") || String.IsNullOrEmpty(line))
                {
                    if (i != 1)
                    {
                        double MW = 0;
                        foreach (char c in currentSequence)
                        {
                            MW += aaMasses[aaLetter.IndexOf(c.ToString())];
                        }
                        MW += 18.01524;

                        if ((int)MW < massFilter)
                        {
                            if (currentProtein != "")
                            {
                                currentProtein = currentProtein.Insert(currentProtein.IndexOf("_RAT") + 4, "|" + MW.ToString("F1") + "|");
                                sequenceArray.Insert(0, currentProtein);
                                File.AppendAllLines(tempOutputPathMWCalculator, sequenceArray);
                                currentSequence = "";
                                currentProtein = "";
                                sequenceArray.Clear();
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            currentSequence = "";
                            currentProtein = "";
                            sequenceArray.Clear();
                        }
                        //int percentage = (startingLineMWCalculator++) * 100 / lineCount;
                        //backgroundWorkerMWCalculator.ReportProgress(percentage);
                    }
                    currentProtein = line;
                }

                else
                {
                    currentSequence += line;
                    sequenceArray.Add(line);
                }
                int percentage = (startingLineMWCalculator++) * 100 / lineCount;
                backgroundWorkerMWCalculator.ReportProgress(percentage);
            }
            stopwatchMWCalculator.Stop();
        }

        private void backgroundWorkerMWCalculator_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMWCalculator.Value = e.ProgressPercentage;
            if (statusLabelMWCalculator.Text != "Work in progress...")
            {
                statusLabelMWCalculator.Text = "Work in progress...";
            }
        }

        private void backgroundWorkerMWCalculator_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerMWCalculator.Stop();
            if (e.Cancelled)
            {
                statusLabelMWCalculator.Text = ("The task has been cancelled");
                File.Delete(tempOutputPathMWCalculator);
            }
            else if (e.Error != null)
            {
                statusLabelMWCalculator.Text = ("Error. Details: " + (e.Error as Exception).ToString());
                File.Delete(tempOutputPathMWCalculator);
            }
            else
            {
                progressBarMWCalculator.Value = 100;
                completedPercentageLabelMWCalculator.Text = ("100%");
                elapsedTimeLabelMWCalculator.Text = stopwatchMWCalculator.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelMWCalculator.Text = "Work completed.";
                openFileResultMWCalculator.Enabled = true;
                processButtonMWCalculator.Enabled = true;
                selectFolderMWCalculator.Enabled = true;
                resetButtonMWCalculator.Enabled = true;
                massFilterInputMWCalculator.Enabled = true;
                openFileMWCalculator.Enabled = true;
                File.Move(tempOutputPathMWCalculator, outputPathMWCalculator);
            }
        }

        private void processButtonMWCalculator_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(openedFileMWCalculator))
            {
                MessageBox.Show("You must open a file first.");
            }
            else
            {
                if (File.Exists(outputPathMWCalculator))
                {
                    MessageBox.Show("The output file exists" + Environment.NewLine + "Please select a different file name or a different folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    massFilter = (int)massFilterInputMWCalculator.Value;
                    progressBarMWCalculator.Value = 0;
                    elapsedTimeLabelMWCalculator.Text = "";
                    completedPercentageLabelMWCalculator.Text = "";
                    startingLineMWCalculator = 0;
                    processButtonMWCalculator.Enabled = false;
                    openFileResultMWCalculator.Enabled = false;
                    selectFolderMWCalculator.Enabled = false;
                    resetButtonMWCalculator.Enabled = false;
                    massFilterInputMWCalculator.Enabled = false;
                    openFileMWCalculator.Enabled = false;
                    tempOutputPathMWCalculator = tempFolderPath + @"\" + outputFileNameMWCalculator + ".temp";
                    timerMWCalculator.Start();
                    backgroundWorkerMWCalculator.RunWorkerAsync();
                }
            }

        }

        private void cancelButtonMWCalculator_Click(object sender, EventArgs e)
        {
            backgroundWorkerMWCalculator.CancelAsync();
            openFileResultMWCalculator.Enabled = false;
            processButtonMWCalculator.Enabled = true;
            selectFolderMWCalculator.Enabled = true;
            resetButtonMWCalculator.Enabled = true;
            massFilterInputMWCalculator.Enabled = true;
            openFileMWCalculator.Enabled = true;
        }

        private void openFileResultMWCalculator_Click(object sender, EventArgs e)
        {
            if (File.Exists(outputPathMWCalculator))
            {
                Process.Start(outputPathMWCalculator);
            }
            else
            {
                MessageBox.Show("The file does not exists", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void outputFileTextBoxMWCalculator_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                outputFileNameMWCalculator = outputFileTextBoxMWCalculator.Text;
                outputPathMWCalculator = (outputFolderMWCalculator + @"\" + outputFileNameMWCalculator + "_filtered.fasta");
                currentOutputFilePathMWCalculator.Text = outputPathMWCalculator;
            }
        }

        private void SelectFolderMWCalculator_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (outputFolderMWCalculator != null)
            {
                dialog.InitialDirectory = outputFolderMWCalculator;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFolderMWCalculator = dialog.FileName;
                outputPathMWCalculator = (outputFolderMWCalculator + @"\" + outputFileNameMWCalculator + "_filtered.fasta");
                currentOutputFilePathMWCalculator.Text = outputPathMWCalculator;
            }
        }

        private void timerMWCalculator_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabelMWCalculator.Text = stopwatchMWCalculator.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabelMWCalculator.Text = (progressBarMWCalculator.Value.ToString() + "%");
        }

        private void defaultButtonMWCalculator_Click(object sender, EventArgs e)
        {
            massFilterInputMWCalculator.Value = Properties.Settings.Default.DefaultMassFilterMWCalculator;
            elapsedTimeLabelMWCalculator.Text = "";
            completedPercentageLabelMWCalculator.Text = "";
            openedFileMWCalculator = "";
            openedFileLabelMWCalculator.Text = "";
            statusLabelMWCalculator.Text = "";
            outputFileTextBoxMWCalculator.Text = "";
            currentOutputFilePathMWCalculator.Text = "";
            openFileResultMWCalculator.Enabled = false;
            processButtonMWCalculator.Enabled = true;
            selectFolderMWCalculator.Enabled = true;
            resetButtonMWCalculator.Enabled = true;
            massFilterInputMWCalculator.Enabled = true;
            openFileMWCalculator.Enabled = true;
            progressBarMWCalculator.Value = 0;
        }

        private void setDefaultMassFilterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["DefaultMassFilterMWCalculator"] = massFilterInputMWCalculator.Value;
            Properties.Settings.Default.Save();
        }

        //******************************************************************************************************
        //**************************************** Mass Filter Generator ***************************************
        //******************************************************************************************************

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
            File.AppendAllText(tempOutputPathMFG, firstString + Environment.NewLine);
            int lineCount = File.ReadLines(openedFileMFG).Count();
            var reader = new StreamReader(openedFileMFG);
            double minimumMassFilter = Convert.ToDouble(minimumMassFilterInputMFG.Value);
            double maximumMassFilter = Convert.ToDouble(maximumMassFilterInputMFG.Value);
            var randomColor = new Random();
            stopwatchMFG.Restart();
            for (int i = 1; i < (lineCount + 1); i++)
            {
                var line = reader.ReadLine();
                string lineToAppend = "<Result Type=\"PkFilter\" ";
                if (backgroundWorkerMFG.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                double currentMass;
                if (Double.TryParse(line, out currentMass)) //check if the line is a umber of type double
                {
                    if (currentMass >= minimumMassFilter && currentMass <= maximumMassFilter) // check if the number is within the specified MW limits
                    {
                        string entryName = "Name=" + "\"" + currentMass.ToString() + "\" ";
                        string color = "Color=" + "\"" + String.Format("#{0:X6}", randomColor.Next(0x1000000)) + "\" " + "Show=" + "\"" + "0" + "\" ";
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
                                MessageBox.Show("The selected filter will result in negative values for the value at line #:" + i.ToString() + Environment.NewLine + "Please check the input file and the filter parameters");
                                backgroundWorkerMFG.CancelAsync();
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
                                MessageBox.Show("The selected filter will result in negative values for the value at line #:" + i.ToString() + Environment.NewLine + "Please check the input file and the filter parameters");
                                backgroundWorkerMFG.CancelAsync();
                            }
                        }
                        string minMass = "MinMass=" + "\"" + minusWindow + "\" ";
                        string maxMass = "MaxMass=" + "\"" + pluswindow + "\" ";
                        string integrate = "Integrate=" + "\"" + (integrationPeakIntensityMFG.Checked ? "0" : "1") + "\" FindMass =" + "\"" + 0 + "\" ";
                        string relMass = "RelMass=" + "\"" + (percentWindowMFG.Checked ? "1" : "0") + "\"/>";

                        lineToAppend += entryName + color + minIntensity + maxIntensity + absoluteIntensity + logScale + minMass + maxMass + integrate + relMass;
                        File.AppendAllText(tempOutputPathMFG, lineToAppend + Environment.NewLine);

                    }
                }

                else
                {
                    MessageBox.Show("The mass value on line #" + i.ToString() + "is not a valid mass and has not been included in the result file." + Environment.NewLine + "Please check the input file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                int percentage = (startingLineMFG++) * 100 / lineCount;
                backgroundWorkerMFG.ReportProgress(percentage);
            }
            string lastString = "</ImagingResults>";
            File.AppendAllText(tempOutputPathMFG, lastString);
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
                File.Delete(tempOutputPathMFG);
            }
            else if (e.Error != null)
            {
                statusLabelMFG.Text = ("Error. Details: " + (e.Error as Exception).ToString());
                File.Delete(tempOutputPathMFG);
            }
            else
            {
                progressBarMFG.Value = 100;
                completedPercentageLabelMFG.Text = ("100%");
                elapsedTimeLabelMFG.Text = stopwatchMFG.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelMFG.Text = "Work completed.";
                openFileResultMFG.Enabled = true;
                File.Move(tempOutputPathMFG, outputPathMFG);
            }
            processButtonMFG.Enabled = true;
            selectFolderMFG.Enabled = true;
            resetButtonMFG.Enabled = true;
            minimumMassFilterInputMFG.Enabled = true;
            openFileMFG.Enabled = true;
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
                    MessageBox.Show("The output file exists" + Environment.NewLine + "Please select a different file name or a different folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    tempOutputPathMFG = tempFolderPath + @"\" + outputFileNameMFG + ".temp";
                    timerMFG.Start();
                    backgroundWorkerMFG.RunWorkerAsync();
                }
            }

        }

        private void cancelButtonMFG_Click(object sender, EventArgs e)
        {
            backgroundWorkerMFG.CancelAsync();
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
            minimumIntensityMFG.Value = Properties.Settings.Default.DefaultMinimumIntensityMFG;
            maximumIntensityMFG.Value = Properties.Settings.Default.DefaultMaximumIntensityMFG;
            mzWindowMFG.Value = Properties.Settings.Default.DefaultMassRangeMFG;
            minimumMassFilterInputMFG.Value = Properties.Settings.Default.DefaultMinimumMWMFG;
            maximumMassFilterInputMFG.Value = Properties.Settings.Default.DefaultMaximumMWMFG;
            absoluteIntensityMFG.Checked = Properties.Settings.Default.DefaultAbsoluteIntensityCheckStateMFG;
            logScaleMFG.Checked = Properties.Settings.Default.DefaultLogScaleCheckStateMFG;
            percentWindowMFG.Checked = Properties.Settings.Default.DefaultPercentWindowCheckStateMFG;
            integrationPeakIntensityMFG.Checked= Properties.Settings.Default.DefaultIntegrationPeakIntensityCheckStateMFG;
            absoluteWindowMFG.Checked = Properties.Settings.Default.DefaultAbsoluteWindowMFG;
            integrationIntegrateSignalMFG.Checked = Properties.Settings.Default.DefaultIntegrationIntegrateSignalMFG;

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
                inputFolderMLA = dialog.FileName;
                outputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + ".mir");
                currentOutputFilePathMFG.Text = outputPathMFG;
            }
        }

        private void outputFileTextBoxMFG_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                outputFileNameMFG = outputFileTextBoxMFG.Text;
                outputPathMFG = (outputFolderMFG + @"\" + outputFileNameMFG + ".mir");
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
                mzWindowMFG.Value = Properties.Settings.Default.DefaultMassRangeMFG;
                mzWindowLabelMFG.Text = "%";
            }
            else
            {
                mzWindowMFG.DecimalPlaces = 3;
                mzWindowMFG.Increment = 0.001M;
                mzWindowMFG.Maximum = 10000000000;
                mzWindowMFG.Minimum = 0.001M;
                mzWindowMFG.Value = Properties.Settings.Default.DefaultMassRangeMFG;
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
                minimumIntensityMFG.Value = Properties.Settings.Default.DefaultMinimumIntensityMFG;
                maximumIntensityMFG.Value = Properties.Settings.Default.DefaultMaximumIntensityMFG;
            }
            else
            {
                minIntensityLabelMFG.Text = "%";
                maxIntensityLabelMFG.Text = "%";
                maximumIntensityMFG.Maximum = 100;
                minimumIntensityMFG.Value = Properties.Settings.Default.DefaultMinimumIntensityPercentMFG;
                maximumIntensityMFG.Value = Properties.Settings.Default.DefaultMaximumIntensityPercentMFG;
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

        private void setDefaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (absoluteIntensityMFG.Checked)
            {
                Properties.Settings.Default["DefaultMinimumIntensityMFG"] = minimumIntensityMFG.Value;
                Properties.Settings.Default["DefaultMaximumIntensityMFG"] = maximumIntensityMFG.Value;
            }
            else
            {
                Properties.Settings.Default["DefaultMinimumIntensityPercentMFG"] = minimumIntensityMFG.Value;
                Properties.Settings.Default["DefaultMaximumIntensityPercentMFG"] = maximumIntensityMFG.Value;
            }

            Properties.Settings.Default["DefaultMassRangeMFG"] = mzWindowMFG.Value;
            Properties.Settings.Default["DefaultMinimumMWMFG"] = minimumMassFilterInputMFG.Value;
            Properties.Settings.Default["DefaultMaximumMWMFG"] = maximumMassFilterInputMFG.Value;
            Properties.Settings.Default["DefaultAbsoluteIntensityCheckStateMFG"] = absoluteIntensityMFG.Checked;
            Properties.Settings.Default["DefaultLogScaleCheckStateMFG"] = logScaleMFG.Checked;
            Properties.Settings.Default["DefaultPercentWindowCheckStateMFG"] = percentWindowMFG.Checked;
            Properties.Settings.Default["DefaultAbsoluteWindowMFG"] = absoluteWindowMFG.Checked;
            Properties.Settings.Default["DefaultIntegrationIntegrateSignalMFG"] = integrationIntegrateSignalMFG.Checked;
            Properties.Settings.Default.Save();
        }

        //******************************************************************************************************
        //***************************************** Mass List Analizer *****************************************
        //******************************************************************************************************
        
        private void backgroundWorkerMLA_DoWork(object sender, DoWorkEventArgs e)
        {
            List<TemporaryPeaklistMLA> tempArray = new List<TemporaryPeaklistMLA>(); // create an empty array to store temporary values in string form
            var readEngine = new FileHelperEngine<ProteinResultsToRead>();
            var tempEngine = new FileHelperEngine<TemporaryPeaklistMLA>();
            var writeEngine = new FileHelperEngine<PeaklistToWriteMLA>();
            string[] fileEntries = Directory.GetFiles(inputFolderMLA, "*.csv");
            List<decimal> massesList = new List<decimal>();
            stopwatchMLA.Restart();
            foreach (string s in fileEntries)
            {
                var currentFile = readEngine.ReadFile(s);

                if (backgroundWorkerMFG.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                
                else if (!tempArray.Any()) // check if the output array is empty, basically determining if this is the first file
                {
                    foreach (var line in currentFile)
                    {
                        TemporaryPeaklistMLA newLine = new TemporaryPeaklistMLA();
                        newLine.mass = line.mass.ToString();
                        newLine.time = line.time.ToString();
                        newLine.intensity = line.intensity.ToString();
                        newLine.sn = line.sn.ToString();
                        newLine.qualityFactor = line.qualityFactor.ToString();
                        newLine.resolution = line.resolution.ToString();
                        newLine.area = line.area.ToString();
                        newLine.relativeIntensity = line.relativeIntensity.ToString();
                        newLine.fwhm = line.fwhm.ToString();
                        newLine.chi2 = line.chi2.ToString();
                        newLine.backgroundPeak = line.backgroundPeak.ToString();
                        tempArray.Add(newLine);
                        massesList.Add(line.mass); //Adds all masses to the mass list.
                    }
                }
                else
                {
                    foreach (var line in currentFile) // parse each line of the opened file
                    {
                        decimal minusRangeMass;
                        decimal plusRangeMass;

                        if (percentWindowMLA.Checked) //determine the range from the percentage value
                        {
                            minusRangeMass = line.mass - ((line.mass / 100) * mzWindowMLA.Value);
                            plusRangeMass = line.mass + ((line.mass / 100) * mzWindowMLA.Value);
                        }
                        else
                        {
                           minusRangeMass = line.mass - mzWindowMLA.Value;
                           plusRangeMass = line.mass + mzWindowMLA.Value;
                        }

                        if (massesList.Any(n => n >= minusRangeMass && n <= plusRangeMass)) // Determines if there is at least one value in the current mass list close to the one under examination
                        {
                            int matchLine = massesList.FindIndex(n => n >= minusRangeMass && n <= plusRangeMass);  // find the index of the match and add the value of the opened file to the current value for the entry 
                            tempArray[matchLine].mass += "/" + line.mass.ToString();
                            tempArray[matchLine].time += "/" + line.time.ToString();
                            tempArray[matchLine].intensity += "/" + line.intensity.ToString();
                            tempArray[matchLine].sn += "/" + line.sn.ToString();
                            tempArray[matchLine].qualityFactor += "/" + line.qualityFactor.ToString();
                            tempArray[matchLine].resolution += "/" + line.resolution.ToString();
                            tempArray[matchLine].area += "/" + line.area.ToString();
                            tempArray[matchLine].relativeIntensity += "/" + line.relativeIntensity.ToString();
                            tempArray[matchLine].fwhm += "/" + line.fwhm.ToString();
                            tempArray[matchLine].chi2 += "/" + line.chi2.ToString();
                            tempArray[matchLine].backgroundPeak += "/" + line.backgroundPeak.ToString();
                        }
                        else
                        {
                            TemporaryPeaklistMLA newLine = new TemporaryPeaklistMLA(); // create a new empty entry and fills it with the values of the opened file columns
                            newLine.mass = line.mass.ToString();
                            newLine.time = line.time.ToString();
                            newLine.intensity = line.intensity.ToString();
                            newLine.sn = line.sn.ToString();
                            newLine.qualityFactor = line.qualityFactor.ToString();
                            newLine.resolution = line.resolution.ToString();
                            newLine.area = line.area.ToString();
                            newLine.relativeIntensity = line.relativeIntensity.ToString();
                            newLine.fwhm = line.fwhm.ToString();
                            newLine.chi2 = line.chi2.ToString();
                            newLine.backgroundPeak = line.backgroundPeak.ToString();
                            tempArray.Add(newLine); // add the entry to the result array
                            massesList.Add(line.mass);
                        }
                    }
                }
                int percentage = (startingFileMLA++) * 100 / fileEntries.Count();
                backgroundWorkerMFG.ReportProgress(percentage);
            }

            List<PeaklistToWriteMLA> finalFileToWrite = new List<PeaklistToWriteMLA>();

            foreach (var line in tempArray)
            {
                PeaklistToWriteMLA newLine = new PeaklistToWriteMLA();
                if (Regex.Matches(line.mass, "/").Count >= 1)
                {
                    char[] charSeparators = new char[] { '/' };

                    // retrieve all masses and calculate mean and standard deviation
                    string[] masses = line.mass.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalMasses = new List<double>();
                    foreach (string mass in masses)
                    {
                        totalMasses.Add(System.Convert.ToDouble(mass));
                    }

                    newLine.mass = totalMasses.Average();

                    newLine.massStDev = totalMasses.StandardDeviation();

                    // retrieve all times and calculate mean and standard deviation
                    string[] times = line.time.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalTimes = new List<double>();
                    foreach (string time in times)
                    {
                        totalTimes.Add(System.Convert.ToDouble(time));
                    }
                    newLine.time = totalTimes.Average();
                    newLine.timeStDev = totalTimes.StandardDeviation();

                    // retrieve all intensities and calculate mean and standard deviation
                    string[] intensities = line.intensity.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalIntensities = new List<double>();
                    foreach (string intensity in intensities)
                    {
                        totalIntensities.Add(System.Convert.ToDouble(intensity));
                    }
                    newLine.intensity = totalIntensities.Average();
                    newLine.intensityStDev = totalIntensities.StandardDeviation();

                    // retrieve all sn and calculate mean and standard deviation
                    string[] sns = line.sn.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalSns = new List<double>();
                    foreach (string sn in sns)
                    {
                        totalSns.Add(System.Convert.ToDouble(sn));
                    }
                    newLine.sn = totalSns.Average();
                    newLine.snStDev = totalSns.StandardDeviation();

                    // retrieve all qualityFactor and calculate mean and standard deviation
                    string[] qualityFactors = line.qualityFactor.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalQualityFactors = new List<double>();
                    foreach (string qualityFactor in qualityFactors)
                    {
                        totalQualityFactors.Add(System.Convert.ToDouble(qualityFactor));
                    }
                    newLine.qualityFactor = totalQualityFactors.Average();
                    newLine.qualityFactorStDev = totalQualityFactors.StandardDeviation();

                    // retrieve all resolutions and calculate mean and standard deviation
                    string[] resolutions = line.resolution.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalResolutions = new List<double>();
                    foreach (string resolution in resolutions)
                    {
                        totalResolutions.Add(System.Convert.ToDouble(resolution));
                    }
                    newLine.resolution = totalResolutions.Average();
                    newLine.resolutionStDev = totalResolutions.StandardDeviation();

                    // retrieve all areas and calculate mean and standard deviation
                    string[] areas = line.area.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalAreas = new List<double>();
                    foreach (string area in areas)
                    {
                        totalAreas.Add(System.Convert.ToDouble(area));
                    }
                    newLine.area = totalAreas.Average();
                    newLine.areaStDev = totalAreas.StandardDeviation();

                    // retrieve all relative intensities and calculate mean and standard deviation
                    string[] relativeIntensities = line.relativeIntensity.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalRelativeIntensities = new List<double>();
                    foreach (string relativeIntensity in relativeIntensities)
                    {
                        totalRelativeIntensities.Add(System.Convert.ToDouble(relativeIntensity));
                    }
                    newLine.relativeIntensity = totalRelativeIntensities.Average();
                    newLine.relativeIntensityStDev = totalRelativeIntensities.StandardDeviation();

                    // retrieve all fwhm and calculate mean and standard deviation
                    string[] fwhms = line.fwhm.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalFwhms = new List<double>();
                    foreach (string fwhm in fwhms)
                    {
                        totalFwhms.Add(System.Convert.ToDouble(fwhm));
                    }
                    newLine.fwhm = totalFwhms.Average();
                    newLine.fwhmStDev = totalFwhms.StandardDeviation();

                    // retrieve all chi2 and calculate mean and standard deviation
                    string[] chi2s = line.chi2.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalChi2 = new List<double>();
                    foreach (string chi2 in chi2s)
                    {
                        totalChi2.Add(System.Convert.ToDouble(chi2));
                    }
                    newLine.chi2 = totalChi2.Average();
                    newLine.chi2StDev = totalChi2.StandardDeviation();

                    // retrieve all backgroundPeaks and calculate mean and standard deviation
                    string[] backgroundPeaks = line.backgroundPeak.Split(charSeparators, StringSplitOptions.RemoveEmptyEntries);
                    List<double> totalBackgroundPeaks = new List<double>();
                    foreach (string backgroundPeak in backgroundPeaks)
                    {
                        totalBackgroundPeaks.Add(System.Convert.ToDouble(backgroundPeak));
                    }
                    newLine.backgroundPeak = totalBackgroundPeaks.Average();
                    newLine.backgroundPeakStDev = totalBackgroundPeaks.StandardDeviation();

                    // add the total occurencuies in the last column
                    newLine.occurrences = totalMasses.Count();

                    finalFileToWrite.Add(newLine); // add the entry to the result array
                }
                else
                {
                    // writes all columns of the entry and set occurency to 1
                    newLine.mass = System.Convert.ToDouble(line.mass);
                    newLine.massStDev = 0;
                    newLine.intensity = System.Convert.ToDouble(line.intensity);
                    newLine.intensityStDev = 0;
                    newLine.time = System.Convert.ToDouble(line.time);
                    newLine.timeStDev = 0;
                    newLine.sn = System.Convert.ToDouble(line.sn);
                    newLine.snStDev = 0;
                    newLine.qualityFactor = System.Convert.ToDouble(line.qualityFactor);
                    newLine.qualityFactorStDev = 0;
                    newLine.resolution = System.Convert.ToDouble(line.resolution);
                    newLine.resolutionStDev = 0;
                    newLine.area = System.Convert.ToDouble(line.area);
                    newLine.areaStDev = 0;
                    newLine.relativeIntensity = System.Convert.ToDouble(line.relativeIntensity);
                    newLine.relativeIntensityStDev = 0;
                    newLine.fwhm = System.Convert.ToDouble(line.fwhm);
                    newLine.fwhmStDev = 0;
                    newLine.chi2 = System.Convert.ToDouble(line.chi2);
                    newLine.chi2StDev = 0;
                    newLine.backgroundPeak = System.Convert.ToDouble(line.backgroundPeak);
                    newLine.backgroundPeakStDev = 0;
                    newLine.occurrences = 1;
                    finalFileToWrite.Add(newLine);
                }

            }
            File.WriteAllText(rawOutputPathMLA, "Mass,Time,Intensity,S/N,Quality Factor,Resolution,Area,Relative Intensity,FWHM,Shi^2,Background Peak"); //headers for the raw output file
            tempEngine.AppendToFile(rawOutputPathMLA, tempArray); //write the files
            File.WriteAllText(outputPathMLA, "Occurences,Mass,SD,Time,SD,Intensity,SD,S/N,SD,Quality Factor,SD,Resolution,SD,Area,SD,Relative Intensity,SD,FWHM,SD,Shi^2,SD,Background Peak"); //headers for the output file
            writeEngine.AppendToFile(outputPathMLA, finalFileToWrite); //write the files
            stopwatchMLA.Stop();
        }
        
        private void backgroundWorkerMLA_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarMLA.Value = e.ProgressPercentage;
            if (statusLabelMLA.Text != "Work in progress...")
            {
                statusLabelMLA.Text = "Work in progress...";
            }
        }
        
        private void backgroundWorkerMLA_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerMLA.Stop();
            if (e.Cancelled)
            {
                statusLabelMLA.Text = ("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                statusLabelMLA.Text = ("Error. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                progressBarMLA.Value = 100;
                completedPercentageLabelMLA.Text = ("100%");
                elapsedTimeLabelMLA.Text = stopwatchMLA.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelMLA.Text = "Work completed.";
                openFileResultsMLA.Enabled = true;
            }
            processButtonMLA.Enabled = true;
            selectFolderMLA.Enabled = true;
            resetButtonMLA.Enabled = true;
            mzWindowMLA.Enabled = true;
        }
        
        private void selectFolderMLA_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (inputFolderMLA != null)
            {
                dialog.InitialDirectory = inputFolderMLA;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                inputFolderMLA = dialog.FileName;
                outputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + ".csv");
                rawOutputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + "_raw.csv");
                currentOutputFilePathMLA.Text = outputPathMLA;
                selectedFolderLabelMLA.Text = inputFolderMLA;
                outputFileTextBoxMLA.Text = outputFileNameMLA;
            }
        }
        
        private void processMLA_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputFolderMLA))
            {
                MessageBox.Show("You must open a folder first.");
            }
            else
            {
                if (File.Exists(outputPathMLA))
                {
                    MessageBox.Show("The output files exist" + Environment.NewLine + "Please select a different file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    progressBarMLA.Value = 0;
                    elapsedTimeLabelMLA.Text = "";
                    completedPercentageLabelMFG.Text = "";
                    startingFileMLA = 0;
                    processButtonMLA.Enabled = false;
                    openFileResultsMLA.Enabled = false;
                    selectFolderMLA.Enabled = false;
                    resetButtonMLA.Enabled = false;
                    mzWindowMLA.Enabled = false;
                    timerMLA.Start();
                    backgroundWorkerMLA.RunWorkerAsync();
                }

            }
        }
        
        private void cancelMLA_Click(object sender, EventArgs e)
        {
            backgroundWorkerMLA.CancelAsync();
        }
        
        private void resetMLA_Click(object sender, EventArgs e)
        {
            percentWindowMLA.Checked = Properties.Settings.Default.DefaultPercentWindowMLA;
            absoluteWindowMLA.Checked = Properties.Settings.Default.DefaultAbsoluteWindowMLA;
            mzWindowMLA.Value = Properties.Settings.Default.DefaultMassRangeMLA;
            processButtonMLA.Enabled = true;
            selectFolderMLA.Enabled = true;
            resetButtonMLA.Enabled = true;
            mzWindowMLA.Enabled = true;
            progressBarMLA.Value = 0;
            completedPercentageLabelMLA.Text = "";
            elapsedTimeLabelMLA.Text = "";
            statusLabelMLA.Text = "";
            openFileResultsMLA.Enabled = false;
            selectedFolderLabelMLA.Text = "";
            currentOutputFilePathMLA.Text = "";
            inputFolderMLA = "";
            outputFileTextBoxMLA.Text = "";
        }
        
        private void openFileResultsMLA_Click(object sender, EventArgs e)
        {
            Point screenPoint = openFileResultsMLA.PointToScreen(new Point(openFileResultsMLA.Left, openFileResultsMLA.Bottom));
            if (screenPoint.Y + openResultContextMenuMLA.Size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                openResultContextMenuMLA.Show(openFileResultsMLA, new Point(0, -openResultContextMenuMLA.Size.Height));
            }
            else
            {
                openResultContextMenuMLA.Show(openFileResultsMLA, new Point(0, openFileResultsMLA.Height));
            }
        }
        
        private void openRawDataResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(rawOutputPathMLA))
            {
                Process.Start(rawOutputPathMLA);
            }
            else
            {
                MessageBox.Show("The file does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(outputPathMLA))
            {
                Process.Start(outputPathMLA);
            }
            else
            {
                MessageBox.Show("The file does not exists", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void outputFileTextBoxMLA_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                outputFileNameMLA = outputFileTextBoxMLA.Text;
                outputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + ".csv");
                rawOutputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + "_raw.csv");
                currentOutputFilePathMLA.Text = outputPathMLA;
            }
        }
        
        private void percentWindowMLA_CheckedChanged(object sender, EventArgs e)
        {
            if (percentWindowMLA.Checked)
            {
                mzWindowMLA.DecimalPlaces = 2;
                mzWindowMLA.Increment = 0.01M;
                mzWindowMLA.Maximum = 100;
                mzWindowMLA.Minimum = 0.01M;
                mzWindowMLA.Value = Properties.Settings.Default.DefaultMassRangeMLA;
                mzWindowLabelMLA.Text = "%";
            }
            else
            {
                mzWindowMLA.DecimalPlaces = 3;
                mzWindowMLA.Increment = 0.001M;
                mzWindowMLA.Maximum = 10000000000;
                mzWindowMLA.Minimum = 0.001M;
                mzWindowMLA.Value = Properties.Settings.Default.DefaultMassRangeMLA;
                mzWindowLabelMLA.Text = "Da";
            }
        }

        private void timerMLA_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabelMLA.Text = stopwatchMLA.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabelMLA.Text = (progressBarMLA.Value.ToString() + "%");
        }

        private void setDefaultMassWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["DefaultMassRangeMLA"] = mzWindowMLA.Value;
            Properties.Settings.Default["DefaultPercentWindowMLA"] = percentWindowMLA.Checked;
            Properties.Settings.Default["DefaultAbsoluteWindowMLA"] = absoluteWindowMLA.Checked;
            Properties.Settings.Default.Save();
        }

        private void outputFolderButtonMLA_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (inputFolderMLA != null)
            {
                dialog.InitialDirectory = inputFolderMLA;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                outputFolderMLA = dialog.FileName;
                outputPathMLA = (outputFolderMLA + @"\" + outputFileNameMLA + ".csv");
                rawOutputPathMLA = (outputFolderMLA + @"\" + outputFileNameMLA + "_raw.csv");
                currentOutputFilePathMLA.Text = outputPathMLA;
                outputFileTextBoxMLA.Text = outputFileNameMLA;
            }
        }

        //******************************************************************************************************
        //***************************************** Peptide Filtering ******************************************
        //******************************************************************************************************

        private void backgroundWorkerPF_DoWork(object sender, DoWorkEventArgs e)
        {
            string finalHeader = "Protein ID" + "," + "# of matching peptides" + "," + "Protein mass" + "," + "Protein name" + "," + "Matching peptides";
            stopwatchPF.Restart();

            // check if DB exists. If not, it creates it.
            string DatabaseName = Path.GetFileNameWithoutExtension(inputFolderPF + @"\" + selectedDBText);
            string DatabaseFolderPath = inputFolderPF + @"\" + DatabaseName + "_index";
            if (Directory.Exists(DatabaseFolderPath))
            {
                statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nDatabase already exists."); });
            }
            else
            {
                if (backgroundWorkerPF.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    //launch command for creating the database
                    Process CreateDB = new Process();
                    ProcessStartInfo CreateDBstartInfo = new ProcessStartInfo();
                    CreateDBstartInfo.FileName = "java.exe";
                    string CreateDBstartInfoArguments = "-jar " + "\"" + applicationPath + @"\" + "Java apps" + @"\" + "PeptideMatchCMD_1.0.jar" + "\"" + " -a index -d " + "\"" + inputFolderPF + @"\" + selectedDBText + "\"" + " -i " + "\"" + DatabaseFolderPath;
                    CreateDBstartInfo.Arguments = CreateDBstartInfoArguments;
                    //launch command for creating the database;
                    CreateDB.StartInfo = CreateDBstartInfo;
                    CreateDB.Start();
                    CreateDB.WaitForExit();
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nDatabase created."); });
                }
            }
            string fileExtension;
            if (PLGSFilesRadioButton.Checked)
            {
                fileExtension = "*.csv";
            }
            else
            {
                fileExtension = "*.list";
            }
            string[] fileEntries = Directory.GetFiles(inputFolderPF, fileExtension); // generate a list of files
            int totalFiles = fileEntries.Count();
            int fileProcessed = 1;
            // Loop to sample one file at time
            foreach (string s in fileEntries)
            {
                if (backgroundWorkerPF.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    List<string> peptideList = new List<string>();
                    int sequencesSkipped = 0;
                    int totalsequences;
                    // Choose PLGS or list files
                    if (PLGSFilesRadioButton.Checked)
                    {
                        //PLGS file processing
                        string firstLine = File.ReadLines(s).First();
                        if (firstLine.Substring(0, 7) != "protein")
                        {
                            statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nFile #" + fileProcessed.ToString() + " of " + totalFiles.ToString() + " skipped because it is not a PLGS result file."); });
                            continue;
                        }
                        else
                        {
                            var readEngine = new FileHelperEngine<PeptideListToRead>();
                            var currentFile = readEngine.ReadFile(s);                             
                            totalsequences = currentFile.Count();
                            foreach (var line in currentFile) // parse each line of the opened file
                            {
                                string currentSequence = line.peptide_seq;
                                   
                                if (currentSequence.Length < MinimumAAPF.Value) //check if the sequence has > AA than the user defined limit
                                {
                                    sequencesSkipped++;
                                }
                                else
                                {
                                    peptideList.Add(currentSequence);
                                }
                            }                             
                        }
                    }
                    else
                    {
                        // list file processing
                        StreamReader currentFile = File.OpenText(s);
                        string line;
                        totalsequences = File.ReadLines(s).Count();
                        while ((line = currentFile.ReadLine()) != null)
                        {
                            if (line.Length < MinimumAAPF.Value) //check if the sequence has > AA than the user defined limit
                            {
                                sequencesSkipped++;
                            }
                            else
                            {
                                peptideList.Add(line);
                            }

                        }
                    }
                    // Eliminate duplicate peptides.
                    List<string> finalPeptideList = peptideList.Distinct().ToList();
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\n" + sequencesSkipped.ToString() + " out of " + totalsequences.ToString() + " skipped because too short"); });

                    //Creates the temporary peptide list
                    string tempResultFile = tempFolderPath + @"\peptides.list";
                    File.AppendAllLines(tempResultFile, finalPeptideList);
                    //Launch DB search command
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nAssigning protein to peptides..."); });
                    Process SearchDB = new Process();
                    ProcessStartInfo SearchDBstartInfo = new ProcessStartInfo();
                    SearchDBstartInfo.FileName = "java.exe";
                    string SearchDBstartInfoArguments = "-jar " + "\"" + applicationPath + @"\" + "Java apps" + @"\" + "PeptideMatchCMD_1.0.jar" + "\"" + " -a query -i " + "\"" + DatabaseFolderPath + "\"" + " -Q " + "\"" + tempResultFile + "\"" + " -l -e -o " + "\"" + tempFolderPath + @"\peptides.txt" + "\"";
                    SearchDBstartInfo.Arguments = SearchDBstartInfoArguments;
                    SearchDB.StartInfo = SearchDBstartInfo;
                    SearchDB.Start();
                    SearchDB.WaitForExit();
                    //Delete the temporary peptide list
                    File.Delete(tempResultFile);
                    //Changes the "|" delimitar to a tab
                    string filetoread = tempFolderPath + @"\peptides.txt";
                    string text = File.ReadAllText(filetoread);
                    text = text.Replace("|", "\t");
                    File.WriteAllText(filetoread, text);
                    // check for peptides without mathes
                    var rawFileToRead = File.ReadAllLines(filetoread);
                    foreach (string line in rawFileToRead)
                    {
                        if (line.Contains("No match"))
                        {
                            statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\n" + line); });
                        }
                    }
                    var newFileToRead = rawFileToRead.Where(line => !line.Contains("No match"));
                    File.WriteAllLines(filetoread, newFileToRead);
                    // read the temporary result files to filter proteins
                    var readTempEngine = new FileHelperEngine<DatabaseResultsToRead>(); //open the temp file from the search
                    var tempFile = readTempEngine.ReadFile(filetoread);
                    //Creates lists for total ID and result ID
                    List<string> AllAccessions = new List<string>();
                    List<string> currentResults = new List<string>();
                    List<string> currentCount = new List<string>();
                    List<string> currentMasses = new List<string>();
                    //Add all accession to the corresponding list
                    foreach (var entry in tempFile)
                    {
                        AllAccessions.Add(entry.AccessionNumber);
                    }

                    //Check if an ID has multiple occurrencies. If it does and is not already in the result list, it adds it to it and it records the number of occurrencies as well as the MW
                    foreach (string id in AllAccessions)
                    {
                        int indexofID = AllAccessions.IndexOf(id);
                        int count = AllAccessions.Where(a => a == id).Count();
                        int count2 = currentResults.Where(a => a == id).Count();
                        if (count >= minimumPeptidePF.Value && count2 < 1)
                        {
                            currentResults.Add(id);
                            currentCount.Add(count.ToString());
                            currentMasses.Add(tempFile[indexofID].ProteinMW);
                        }
                    }
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nProteins assigned."); });
                    // count the total number of hits
                    string hitCount = currentResults.Count().ToString();

                    // joins the protein result list and the count list
                    List<string> tempJoinedLists = new List<string>(currentResults.Zip(currentCount, (first, second) => first + "," + second));                   
                    List<string> joinedLists = new List<string>(tempJoinedLists.Zip(currentMasses, (first, second) => first + "," + second));                    

                    // add protein masses if the peptide search result has a field for it
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nAdding protein masses."); });

                    // Add protein names from the DB used for the search
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nAssigning protein names"); });
                    List<string> idProteinResults = new List<string>();
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Maximum = joinedLists.Count()));
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Value = 0));
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Step = 1));
                    File.WriteAllLines(tempFilePath + "/tempDBHeaders.txt", File.ReadLines(inputFolderPF + @"\" + selectedDBText).Where(line => line.StartsWith(">")));
                    foreach (string entry in joinedLists)
                    {
                        if (backgroundWorkerPF.CancellationPending)
                        {
                            e.Cancel = true;
                            return;
                        }
                        else
                        {
                            string hit = entry.Split(new[] { ',' }, StringSplitOptions.None)[0];
                            string proteinName = "";
                            string modifiedEntry = "|" + hit + "|";
                            StreamReader DBReader = new StreamReader(tempFilePath + "/tempDBHeaders.txt");                            
                            string line;
                            while ((line = DBReader.ReadLine()) != null)
                            {
                                if (line.Contains(modifiedEntry))
                                {                                    
                                    int start = Regex.Match(line, @"_*?\s").Index;
                                    int stop = line.IndexOf("OS=");
                                    if (stop < 0)
                                    {
                                        proteinName = line.Substring(start + 1, line.Length - start - 1); 
                                    }
                                    else
                                    {         
                                        proteinName = line.Substring(start + 1, stop - start - 2);
                                        
                                    }
                                    idProteinResults.Add(entry + "," + Regex.Replace(proteinName, "[,|]", " -"));
                                }
                            };
                            DBReader.Close();                                                     
                            partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.PerformStep()));
                        }
                    }
                    File.Delete(tempFilePath + "/tempDBHeaders.txt");
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Value = joinedLists.Count()));

                    // associate peptides to each protein hit
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nAssigning peptide hits sequences to proteins"); });
                    List<string> currentResultsCopy = new List<string>(currentResults);
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Maximum = tempFile.Count()));
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Value = 0));
                    foreach (var entry in tempFile)
                    {
                        int index = currentResultsCopy.FindIndex(x => x.Equals(entry.AccessionNumber));
                        if (index > -1)
                        {
                            if (idProteinResults[index].Contains("|"))
                            {
                                idProteinResults[index] += entry.PeptideSeq + " | ";
                            }
                            else
                            {
                                idProteinResults[index] += "," + entry.PeptideSeq + " | ";
                            }
                            partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.PerformStep()));
                        }
                    }
                    partialProgressBarPF.Invoke(new Action(() => partialProgressBarPF.Value = tempFile.Count()));

                    //Write the result file
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nWriting the result file."); });
                    string resultFilePath = Path.GetDirectoryName(s) + @"\" + Path.GetFileNameWithoutExtension(s) + "_DBSearch.csv";

                    while (File.Exists(resultFilePath))
                    {
                        string fileNameOnly = Path.GetFileNameWithoutExtension(resultFilePath);
                        string extension = Path.GetExtension(resultFilePath);
                        string path = Path.GetDirectoryName(resultFilePath);
                        string promptValue = FileExistsPrompt.ShowDialog("Results file " + fileNameOnly + " already exists", "Please chose a different name for this file");
                        resultFilePath = Path.Combine(path, promptValue + extension);
                    }
                    string inputFileName = Path.GetFileName(s);
                    File.AppendAllText(resultFilePath, "Search parameters" + "," + Environment.NewLine);
                    File.AppendAllText(resultFilePath, "Input file: " + "," + inputFileName + "," + Environment.NewLine);
                    File.AppendAllText(resultFilePath, "Database: " + "," + selectedDBText + "," + Environment.NewLine);
                    File.AppendAllText(resultFilePath, "Minimum AA: " + "," + MinimumAAPF.Value.ToString() + "," + Environment.NewLine);
                    File.AppendAllText(resultFilePath, "Minimum peptides: " + "," + minimumPeptidePF.Value.ToString() + "," + Environment.NewLine);
                    File.AppendAllText(resultFilePath, "Total hits" + "," + hitCount + Environment.NewLine);
                    File.AppendAllText(resultFilePath, finalHeader + Environment.NewLine);
                    File.AppendAllLines(resultFilePath, idProteinResults);

                    File.Delete(filetoread);
                    statusLabelPF.Invoke((MethodInvoker)delegate { statusLabelPF.AppendText("\r\nFile #" + fileProcessed.ToString() + " of " + totalFiles.ToString() + " processed."); });
                        
                        
                }
                int percentage = (fileProcessed++) * 100 / totalFiles;
                backgroundWorkerPF.ReportProgress(percentage);
            }    
            // Write the log file
            File.WriteAllText(inputFolderPF + "/search_log_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", statusLabelPF.Text);
            stopwatchPF.Stop();
        }

        public static class FileExistsPrompt
        {
            public static string ShowDialog(string text, string caption)
            {
                Form prompt = new Form()
                {
                    Width = 500,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    Text = caption,
                    StartPosition = FormStartPosition.CenterScreen
                };
                Label textLabel = new Label() { Left = 50, Top = 20, Width = 400, Text = text };
                TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
                Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
                confirmation.Click += (sender, e) => { prompt.Close(); };
                prompt.Controls.Add(textBox);
                prompt.Controls.Add(confirmation);
                prompt.Controls.Add(textLabel);
                prompt.AcceptButton = confirmation;
                return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
            }
        }

        private void backgroundWorkerPF_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBarPF.Value = e.ProgressPercentage;

        }

        private void backgroundWorkerPF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            timerPF.Stop();
            if (e.Cancelled)
            {
                statusLabelPF.AppendText("\r\nThe task has been cancelled");
            }
            else if (e.Error != null)
            {
                statusLabelPF.AppendText("\r\nError. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                progressBarPF.Value = 100;
                completedPercentageLabelPF.Text = ("100%");
                elapsedTimeLabelPF.Text = stopwatchPF.Elapsed.ToString(@"hh\:mm\:ss\.f");
                statusLabelPF.AppendText("\r\nWork completed.");
                ResultsButtonPF.Enabled = true;
            }
            ProcessButtonPF.Enabled = true;
            selectFolderPF.Enabled = true;
            ResetButtonPF.Enabled = true;
            MinimumAAPF.Enabled = true;
        }

        private void selectFolderPF_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (inputFolderPF != null)
            {
                dialog.InitialDirectory = inputFolderPF;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                inputFolderPF = dialog.FileName;
                outputPathPF = inputFolderPF;
                currentOutputFilePathPF.Text = outputPathPF;
                selectedFolderLabelPF.Text = inputFolderPF;
                outputFileTextBoxPF.Text = outputFileNamePF;
                string[] DatabaseFileEntries = Directory.GetFiles(inputFolderPF, "*.fasta").Select(x =>Path.GetFileName(x)).ToArray();
                if (DatabaseFileEntries == null || DatabaseFileEntries.Length == 0)
                {
                    MessageBox.Show("The folder you selected does not contain any database (.fasta) file", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    selectDatabasePF.Items.Clear();
                    foreach (string s in DatabaseFileEntries)
                    {
                        selectDatabasePF.Items.Add(s);
                    }
                    selectDatabasePF.SelectedIndex = 0;
                }
            }
        }

        private void outputFolderButtonPF_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            if (inputFolderMLA != null)
            {
                dialog.InitialDirectory = inputFolderMLA;
            }
            else
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
            }
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                //outputFolderMLA = dialog.FileName;
                //outputPathMLA = (outputFolderMLA + @"\" + outputFileNameMLA + ".csv");
                //rawOutputPathMLA = (outputFolderMLA + @"\" + outputFileNameMLA + "_raw.csv");
                //currentOutputFilePathMLA.Text = outputPathMLA;
                //outputFileTextBoxMLA.Text = outputFileNameMLA;
            }
        }

        private void outputFileTextBoxPF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                //outputFileNameMLA = outputFileTextBoxMLA.Text;
                //outputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + ".csv");
                //rawOutputPathMLA = (inputFolderMLA + @"\" + outputFileNameMLA + "_raw.csv");
                //currentOutputFilePathMLA.Text = outputPathMLA;
            }
        }

        private void ProcessButtonPF_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(inputFolderPF))
            {
                MessageBox.Show("You must select a folder first.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (selectDatabasePF.SelectedIndex == -1)
            {
                MessageBox.Show("You did not select any database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (File.Exists(outputPathPF))
                {
                    MessageBox.Show("The output files exist" + Environment.NewLine + "Please select a different file name.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    selectedDBIndex = selectDatabasePF.SelectedIndex;
                    selectedDBText = selectDatabasePF.Items[selectedDBIndex].ToString();
                    progressBarPF.Value = 0;
                    elapsedTimeLabelPF.Text = "";
                    completedPercentageLabelMFG.Text = "";
                    startingFilePF = 0;
                    ProcessButtonPF.Enabled = false;
                    ResultsButtonPF.Enabled = false;
                    selectFolderPF.Enabled = false;
                    ResetButtonPF.Enabled = false;
                    MinimumAAPF.Enabled = false;
                    timerPF.Start();
                    backgroundWorkerPF.RunWorkerAsync();
                }

            }
        }

        private void CancelButtonPF_Click(object sender, EventArgs e)
        {
            backgroundWorkerPF.CancelAsync();
            File.Delete(tempFolderPath + @"\peptides.list");
        }

        private void ResetButtonPF_Click(object sender, EventArgs e)
        {
            MinimumAAPF.Value = Properties.Settings.Default.DefaultMinimumAAPF;
            minimumPeptidePF.Value = Properties.Settings.Default.DefaultMinimumPeptides;
            ProcessButtonPF.Enabled = true;
            selectFolderPF.Enabled = true;
            ResetButtonPF.Enabled = true;
            MinimumAAPF.Enabled = true;
            progressBarPF.Value = 0;
            completedPercentageLabelPF.Text = "";
            elapsedTimeLabelPF.Text = "";
            statusLabelPF.Text = "";
            ResultsButtonPF.Enabled = false;
            selectedFolderLabelPF.Text = "";
            currentOutputFilePathPF.Text = "";
            inputFolderPF = "";
            outputFileTextBoxPF.Text = "";
            selectDatabasePF.Items.Clear();
            File.Delete(tempFolderPath + @"\peptides.list");
        }

        private void timerPF_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabelPF.Text = stopwatchPF.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabelPF.Text = (progressBarPF.Value.ToString() + "%");
        }

        private void ResultsButtonPF_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(outputPathPF))
            {
                Process.Start(outputPathPF);
            }
            else
            {
                MessageBox.Show("You did not select an output folder", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void setDefaultValuesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["DefaultMinimumAAPF"] = MinimumAAPF.Value;
            Properties.Settings.Default["DefaultMinimumPeptides"] = minimumPeptidePF.Value;
            Properties.Settings.Default.Save();
        }


    }
}
