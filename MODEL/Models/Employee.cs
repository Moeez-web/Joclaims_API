using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Employee : User
    {
        //public int UserID { get; set; }
        public int EmployeeID { get; set; }
        //public int CompanyID { get; set; }
        public Int16 PositionID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Phone { get; set; }
        //public string Email { get; set; }
        public string ProfileImageURL { get; set; }
        public string ImgURL { get; set; }
        public string WorkshopName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public List<Permission> UserPermissions { get; set; }
        public string ESignatureURL { get; set; }
        public string ErrorMessage { get; set; }
        public string EmployeeName { get; set; }

        public bool? IsApproved  { get; set; }

        public int ClearanceSummaryApprovalID { get; set; }

        public string SurveyorName { get; set; }

        public int? SurveyorID { get;set; }
    }
}
