using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class RelevantImage
    {
        public int RelevantImageID { get; set; }

        public int CaseDamageID { get; set; }
        public string ImageID { get; set; }
        public string OriginalImageID { get; set; }
        public string EncryptedName { get; set; }
        public string ImageURL { get; set; }
        public string OriginalImageURL { get; set; }
        public string RequestImageURL { get; set; }
        public int QualityScore { get; set; }
        public string DetectedAngle { get; set; }
        public string CaseID { get; set; }
        public int CaseDamageReportID { get; set; }
        public List<BoundingBoxInformation> boundingBoxInformation { get; set; }

        public bool? isDeleted { get; set; }

        public int DraftID   { get; set; }



    }
}
