using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord("\t")]
    [IgnoreFirst(2)]
    public class DatabaseResultsToRead
    {        
        public string PeptideSeq;
        public string DatabaseType;
        public string AccessionNumber;
        public string GeneName;
        [FieldOptional]
        public string ProteinMW;
        [FieldOptional]
        public string emtyString;
        [FieldOptional]
        public string MatchedProteinLength;
        [FieldOptional]
        public string MatchStart;
        [FieldOptional]
        public string MatchEnd;
        [FieldOptional]
        [FieldNullValue(typeof(string), "")]
        public string ResidueIsoAsLeu;
    }
}
