using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord(",")] // determine which 
    public class TemporaryPeaklistMLA
    {
        public string mass;
        public string time;
        public string intensity;
        public string sn;
        [FieldNullValue(typeof(string), "")]
        public string qualityFactor;
        public string resolution;
        public string area;
        public string relativeIntensity;
        public string fwhm;
        [FieldNullValue(typeof(string), "")]
        public string chi2;
        public string backgroundPeak;
    }

}