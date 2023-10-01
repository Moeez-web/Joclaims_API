using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AccidentMarker : DamagePoint
    {
        public int AccidentMarkerID { get; set; }
        public int AccidentID { get; set; }
        public bool IsDamage { get; set; }
        public bool? IsAddition { get; set; }

        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public bool? IsDeleted { get; set; }
        public string PointNameArabic { get; set; }
        public short AccidentTypeID { get; set; }
        public string AccidentNo { get; set; }
        public int? DraftMarkerID { get; set; }
        public int? DraftID { get; set; }
    }
}
