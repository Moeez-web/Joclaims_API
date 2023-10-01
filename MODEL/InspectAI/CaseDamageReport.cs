using MODEL.Models.Inspekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class CaseDamageReport
    {
        public int CaseDamageReportID { get; set; }
        public string caseId { get; set; }
        public string inspectionId { get; set; }
        public string vendor { get; set; }
        public string version { get; set; }
        public VehicleReading vehicleReadings { get; set; }
        public PreInspection preInspection { get; set; }
        public Object claim { get; set; }
        public AdditionalFeature additionalFeatures { get; set; }
        public List<RelevantImage> relevantImages { get; set; }

        public string EnglishMakeName       { get; set; }
        public string ArabicMakeName { get; set; }
        public string ImgURL { get; set; }
        public string EnglishModelName { get; set; }
        public string ArabicModelName { get; set; }
    }
}
