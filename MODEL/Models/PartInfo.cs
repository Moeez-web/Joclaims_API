using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PartInfo
    {
        public int PartInfoID { get; set; }
        public int? SupplierID { get; set; }
        public string SupplierName { get; set; } 
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }

        public string GroupName { get; set; }
        public string ArabicModelName { get; set; }
        public Int16 IsApproved { get; set; }
        public Int16 ProductionYearFrom { get; set; }
        public Int16 ProductionYearTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int MinYearCode { get; set; }
        public int MaxYearCode { get; set; }
        public int ConditionTypeID { get; set; }
        public string ConditionTypeName { get; set; }
        public bool? IsConditionNew { get; set; }
        public bool? IsConditionUsed { get; set; } 
        public bool? IsConditionOriginal { get; set; }
        public bool? IsConditionAfterMarket { get; set; }

        public bool? IsModified { get; set; }
      public bool? IsCheck { get; set; }
  }
}
