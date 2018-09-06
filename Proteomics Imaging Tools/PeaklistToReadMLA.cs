using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord(",")] 
    [IgnoreFirst(3)]
    public class PeaklistToReadMLA
    {
        public decimal mass;
        public decimal time;
        public decimal intensity;
        public decimal sn;
        [FieldNullValue(typeof(int), "0")]
        public decimal qualityFactor;
        public decimal resolution;
        public decimal area;
        public decimal relativeIntensity;
        public decimal fwhm;
        [FieldNullValue(typeof(int), "0")]
        public decimal chi2;
        public decimal backgroundPeak;
    }

}