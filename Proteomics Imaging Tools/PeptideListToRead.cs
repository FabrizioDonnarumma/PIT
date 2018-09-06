using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Proteomics_Imaging_Tools
{
    [DelimitedRecord(",")]
    [IgnoreFirst(1)]
    class PeptideListToRead
    {
        public int protein_key;
        public string protein_Entry;
        public string protein_Accession;
        public string protein_Description;
        public string protein_dataBaseType;
        public double protein_score;
        public double protein_falsePositiveRate;
        public double protein_avgMass;
        public int protein_MatchedProducts;
        public int protein_matchedPeptides;
        public int protein_digestPeps;
        public double protein_seqCover_pct; //was originally  protein_seqCover(%)
        public int protein_MatchedPeptideIntenSum;
        public double protein_top3MatchedPeptideIntenSum;
        public int protein_MatchedProductIntenSum;
        [FieldNullValue(typeof(int), "0")]
        public int protein_fmolOnColumn;
        [FieldNullValue(typeof(int), "0")]
        public int protein_ngramOnColumn;
        public string protein_AutoCurate;
        [FieldNullValue(typeof(string), "")]
        //[FieldHidden] //hiding the field for now, because the field can contain multiple types 
        public string protein_Key_ForHomologs;
        public int protein_SumForTotalProteins;
        public int peptide_Rank;
        public string peptide_Pass;
        public string peptide_matchType;
        public string peptide_modification;
        public double peptide_mhp;
        public string peptide_seq;
        public string peptide_OriginatingSeq;
        public int peptide_seqStart;
        public int peptide_seqLength;
        public double peptide_pI;
        public int peptide_componentID;
        public int peptide_MatchedProducts;
        public int peptide_UniqueProducts;
        public int peptide_ConsectiveMatchedProducts;
        public int peptide_ComplementaryMatchedProducts;
        public double peptide_rawScore;
        public double peptide_score;
        public string peptideXPBond; //was originally peptide.(X)-P Bond
        public int peptide_MatchedProductsSumInten;
        public double peptide_MatchedProductsTheoretical;
        public string peptide_MatchedProductsString;
        public double peptide_ModelRT;
        public int peptide_Volume;
        public double peptide_CSA;
        public double peptide_ModelDrift;
        public double peptide_RelIntensity;
        public string peptide_AutoCurate;
        public int precursor_leID;
        public double precursor_mhp;
        public double precursor_mhpCal;
        public double precursor_retT;
        public double precursor_inten;
        public double precursor_calcInten;
        public double precursor_charge;
        public int precursor_z;
        public double precursor_mz;
        public double precursor_Mobility;
        public int precursor_MobilitySD;
        public double precursor_fwhm;
        public double precursor_liftOffRT;
        public double precursor_infUpRT;
        public double precursor_infDownRT;
        public double precursor_touchDownRT;
        public double prec_rmsFWHMDelta;
        public double peptidePrecursor_deltaMhpPPM;
    }
}
