using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class TRApproval
    {
        public int TRApprovalID { get; set; }
        public int? UserID { get; set; }
        public string AccidentNo { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int ModifiedBy { get; set; }
        public int? ObjectTypeID { get; set; }
        public bool? IsDeleted { get; set; }
        public int EmployeeID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public int? MaxPrice { get; set; }
        public string Note { get; set; }
        public int AccidentID { get; set; }
        public int TRObjectType { get; set; }
        public bool? IsReturn { get; set; }
        public string ReturnNote { get; set; }
        public bool? IsExist { get; set; }

    }
}
