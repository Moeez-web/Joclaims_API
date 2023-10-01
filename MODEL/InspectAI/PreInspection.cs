using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Inspekt
{
    public class PreInspection
    {
        public int PreInspectionID { get; set; }
        public string recommendationStatus { get; set; }
        public string message { get; set; }
        public List<DamagedPart> damagedParts { get; set; }

        public int CaseDamageID  { get; set; }
    }
}
