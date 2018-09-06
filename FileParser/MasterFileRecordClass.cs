using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace FileParser
{
    [DelimitedRecord("\t")] // determine which 
    public class masterFileToRead
    {
        //public string masterColumn1;
        //public string masterColumn2;
        //public string masterColumn3;
        public string entry;
        public string entryName;
        public string proteinNames;
        public string mass;
        public string sequence;
        public string lipidation;
        public string glycosilation;
        public string modifiedResidue;
        public string ptm;
        public string thisfile;
    }

}
