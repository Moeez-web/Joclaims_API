using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PartBranch
    {
		public int BranchID { get; set; }
		public int SupplierID { get; set; }
		public int BranchCityID { get; set; }
		public string BranchName { get; set; }
		public string BranchPhone { get; set; }
		public string BranchAreaName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string BranchCityName { get; set; }
        public string BranchGoogleMapLink { get; set; }
        public string RegistrationNo { get; set; }
    }
}
