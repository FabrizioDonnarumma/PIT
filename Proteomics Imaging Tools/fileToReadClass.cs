using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord("\t")] 
    public class fileToRead
    {
        public string entry;
        public string entryName;
        public string proteinNames;
        public string mass;
        public string chain;
        public string sequence;
        public string initiatorMethionine;
        public string peptide;
        public string propeptide;
        public string signalPeptide;
        public string transitPeptide;
        public string reviewStatus;
    }

}