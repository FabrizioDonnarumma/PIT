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
            string m = "";
            string line = ">sp|Q6P7B2_CHAIN_1_408|CNPD1_RAT Protein CNPPD1";
            int start = Regex.Match(line, @"_*?\s").Index;
            int stop = line.IndexOf("OS=");
            if (stop < 0)
            {
                m = line.Substring(start+1, line.Length - start-1); //idProteinResults.Add(entry + "," + "Not found");
            }
            else
            {
                m = line.Substring(start + 1, stop - start - 2);

            }
            Console.WriteLine(m);


        }

    }
}
