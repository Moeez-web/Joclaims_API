using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Inspekt
{
    public  class InspektObj
    {
        //public List<AdditionalFeature> AdditionalFeature { get; set; }
        //public BoundingBoxInformation BoundingBoxInformation { get; set; }
        //public CaseDamageReport CaseDamageReport { get; set; }

        //public List<DamagedPart> DamagedPart { get; set; }

        //public List<RelevantImage> RelevantImage { get; set; }

        //public PreInspection PreInspection { get; set; }

        public int AppID { get; set; }
        public int ResposeResult { get; set; }
        public CaseDamageReport CaseDamageReport { get; set; }
        public PreInspection PreInspection { get; set; }
        public List<DamagedPart> DamagedPart { get; set; }
        public List<RelevantImage> RelevantImage { get; set; }

        public List<DamagePoint> DamagePoint { get; set; }

        public InpektlabResponse inpektlabResponse { get; set; }






    }
}
