using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FileHelpers;
using System.Diagnostics;
using System.Threading;

namespace FileParser
{
    
    public partial class Form1 : Form
    {
        public string masterFilePath;
        public string compareFilePath;
        public int startingLine;
        private readonly Stopwatch stopwatch = new Stopwatch();



        public Form1()
        {
            InitializeComponent();
            elapsedTimeLabel.Text = "";
            completedPercentageLabel.Text = "";
            openFileResult.Visible = false;
            //mandatory. Otherwise will throw an exception when calling ReportProgress method  
            compareBackgroundWorker.WorkerReportsProgress = true;

            //mandatory. Otherwise we would get an InvalidOperationException when trying to cancel the operation  
            compareBackgroundWorker.WorkerSupportsCancellation = true;
        }

        public void openMasterFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); //Open File dialog 
            if (result == DialogResult.OK) 
            {
                openedMasterFileLabel.Text = openFileDialog1.FileName; //Write on label which file is opened
                masterFilePath = openFileDialog1.FileName;
            }        
        }

        public void openCompareFile_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); //Open File dialog 
            if (result == DialogResult.OK)
            {
                openedCompareFileLabel.Text = openFileDialog1.FileName; //Write on label which file is opened  
                compareFilePath = openFileDialog1.FileName;
            }    
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            timer1.Start();
            compareBackgroundWorker.RunWorkerAsync();
            compareButton.Enabled = false;
        }

        private void compareBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {   
            startingLine = 0;
            var engineMaster = new FileHelperEngine<masterFileToRead>(); // Launch FileHelper engine
            var engineCompare = new FileHelperEngine<compareFileToRead>();
            var masterFileResults = engineMaster.ReadFile(masterFilePath); //    
            var compareFileResults = engineCompare.ReadFile(compareFilePath);            
            string mainPath = Path.GetDirectoryName(masterFilePath);
            List<string> masterFileColumn1 = new List<string>();
            var lineCount = File.ReadLines(compareFilePath).Count();


            foreach (var mrl in masterFileResults)
            {
                masterFileColumn1.Add(mrl.entry);
            }

            stopwatch.Restart();
            foreach (var crl in compareFileResults)
            {
                
                if (compareBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                else if (masterFileColumn1.IndexOf(crl.compareColumn1) != -1) // this checks if the entry has a match
                {                    
                    int indexOfMatch = masterFileColumn1.IndexOf(crl.compareColumn1); //store the index of the match
                    var match = new List<masterFileToRead>(); //create a list using the class of the file containing the match
                    match.Add(masterFileResults[indexOfMatch]); // add the line from the file to the list using the index constant
                    engineMaster.AppendToFile(mainPath + @"/output.txt", match);  // write the list to file                    
                }
                Thread.Sleep(1);
                int percentage = (startingLine++) * 100 / lineCount;
                compareBackgroundWorker.ReportProgress(percentage);
                              
            }
            stopwatch.Stop();  
        }

        private void compareBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;

        }

        private void compareBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            compareButton.Enabled = true;
            timer1.Stop();
            if (e.Cancelled)
            {
                MessageBox.Show("The task has been cancelled");
            }
            else if (e.Error != null)
            {
                MessageBox.Show("Error. Details: " + (e.Error as Exception).ToString());
            }
            else
            {
                progressBar1.Value = 100;
                MessageBox.Show("Done");
                openFileResult.Visible = true;
            }
            
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            compareBackgroundWorker.CancelAsync();
        }

        private void openFileResult_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            elapsedTimeLabel.Text = stopwatch.Elapsed.ToString(@"hh\:mm\:ss\.f");
            completedPercentageLabel.Text = (progressBar1.Value.ToString() + "%");
        }
    }
}
