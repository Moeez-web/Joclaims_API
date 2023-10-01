using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class DamagedPart
    {
        public int DamagePartID { get; set; }
        public string partName { get; set; }
        public string partNameArabic { get; set; }
        public string listOfDamages { get; set; }
        public string damageSeverityScore { get; set; }
        public string laborOperation { get; set; }
        public string confidenceScore { get; set; }
        public string paintLaborUnits { get; set; }
        public string removalRefitUnits { get; set; }
        public string laborRepairUnits { get; set; }
        public int PreInspectionID { get; set; }

        public string ListOfDamagesAr { get; set; }

    }
}
