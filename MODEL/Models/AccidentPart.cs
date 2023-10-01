using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AccidentPart
    {
        public int AccidentPartID { get; set; }
        public int AutomotivePartID { get; set; }
        public Int16 ConditionTypeID { get; set; }
        public string ConditionTypeName { get; set; }
        public string ConditionTypeNameArabic { get; set; }
        public Int16? NewPartConditionTypeID { get; set; }
        public string NewPartConditionTypeName { get; set; }
        public string NewPartConditionTypeArabicName { get; set; }
        public double? DesiredPrice { get; set; }
        public int? Quantity { get; set; }
        public int AccidentID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string AutomotivePartName { get; set; }
        public int ImageRef { get; set; }
        public string NoteInfo { get; set; }
        public double Price { get; set; }
        public bool? IsRequestCreated { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public string DamagePointName { get; set; }
    }
}
