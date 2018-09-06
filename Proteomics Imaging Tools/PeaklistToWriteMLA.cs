using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord(",")] // determine which 
    [IgnoreFirst(3)]
    public class PeaklistToWriteMLA
    {
        public int occurrences;
        public double mass;
        public double massStDev;
        public double intensity;
        public double intensityStDev;
        public double time;
        public double timeStDev;
        public double sn;
        public double snStDev;
        [FieldNullValue(typeof(double), "0.0")]
        public double qualityFactor;
        public double qualityFactorStDev;
        public double resolution;
        public double resolutionStDev;
        public double area;
        public double areaStDev;
        public double relativeIntensity;
        public double relativeIntensityStDev;
        public double fwhm;
        public double fwhmStDev;
        [FieldNullValue(typeof(double), "0.0")]
        public double chi2;
        public double chi2StDev;
        public double backgroundPeak;
        public double backgroundPeakStDev;        
    }

}