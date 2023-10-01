using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestTask
    {
        public int RequestTaskID { get; set; }
        public string TaskName { get; set; }
        public string TaskDescription { get; set; }
        public Int16 TaskTypeID { get; set; }
        public int? LabourTime { get; set; }
        public double? LabourPrice { get; set; }
        public int? RequestID { get; set; }
        public int? DraftTaskID { get; set; }
        public bool? IsTaskApproved { get; set; }
        public string TaskRejectReason { get; set; }
        public string TaskTypeName { get; set; }
        public string TaskArabicTypeName { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsModified { get; set; }
        public double? LabourPriceWithoutDiscount { get; set; }
        public string AccidentNo { get; set; }
        public string EditTaskReason { get; set; }
        public double? OldLabourPrice { get; set; }
        public double? OldLabourPriceWithoutDiscount { get; set; }

        public string ImageURL { get; set; } 
        public double? SurveyorPrice { get; set; } 
    }
}
