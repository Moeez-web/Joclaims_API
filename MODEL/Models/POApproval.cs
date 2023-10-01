using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class POApproval
    {
        public int POApprovalID { get; set; }
        public int UserID { get; set; }
        public int RequestID { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        public int? EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ESignatureURL { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public string PoApprovalUserName { get; set; }
        public int? MaxPrice { get; set; }
    }
}
