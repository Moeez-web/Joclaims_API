using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class FeaturePermission
    {
        public int? FeaturePermissionsID { get; set; }
        public int? FeatureID { get; set; }
        public bool IsApproved { get; set; }
        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
        public bool? IsDeleted { get; set; }

        public string FeatureNameArabic { get; set; }

        public string FeatureName { get; set; }

        public bool? AICustomerRequestApproval { get; set; }

    }
}
