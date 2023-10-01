using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Inspekt
{
    public class AdditionalFeature
    {
        public int FeatureID { get; set; }
        public int TypeID { get; set; }
        public string ImageId { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public SizeOfDamage sizeOfDamage { get; set; }
        public string[] highProbabilityInternalDamages { get; set; }
        public string fastTrackFlag { get; set; }

        public  string Depth { get; set; }

        public int DamagedPartID { get; set; }
        public string DamageSizeName   { get; set; }

        public int PreInspectionID  { get; set; }
    }
}
