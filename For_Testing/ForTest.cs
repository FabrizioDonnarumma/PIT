using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileHelpers;
using System.Reflection;
using LinqStatistics;
using System.Windows;
using System.Net;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;
using Proteomics_Imaging_Tools;
using System.Xml;

namespace For_Testing
{
    public partial class Test : Form
    {
        static readonly XNamespace uniprot = "http://uniprot.org/uniprot";
        public string openedFile;
        public string outputFile;
        public string tempFilePath = Path.GetTempPath();
        public string inputFolder;


        public Test()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string startTime = DateTime.Now.ToString("MM-dd-yyyy-HH:mm:ss");
            Console.WriteLine(startTime);
            Process startNotepad = new Process();
            startNotepad.StartInfo.FileName = @"C:/Program Files/Notepad++/notepad++.exe";
            startNotepad.Start();
            Console.WriteLine("Program started");
            startNotepad.WaitForExit();
            string endTime = DateTime.Now.ToString("MM-dd-yyyy-HH:mm:ss");
            Console.WriteLine(endTime);
            TimeSpan duration = DateTime.ParseExact(endTime, "MM-dd-yyyy-HH:mm:ss", null).Subtract(DateTime.ParseExact(startTime, "MM-dd-yyyy-HH:mm:ss", null));
            Console.WriteLine("Duration: "+duration);
        }

    }
}
