using MODEL.Models.Inspekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class BoundingBoxInformation
    {
        public int BoxInformationID { get; set; }
        public string confidenceScore { get; set; }
        public Coordinates coordinates { get; set; }
        public float damageSeverityScore { get; set; }
        public string damageType { get; set; }
        public string partComponent { get; set; }

        public int RelevantImageID { get; set; }

    }
}
