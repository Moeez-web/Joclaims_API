using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class SignInResponse
    {
        public User UserObj { get; set; }
        public Company Company { get; set; }
        public Supplier Supplier { get; set; }
        public Employee Employee { get; set; }
        public ShubeddakUser ShubeddakUser { get; set; }
        public List<Permission> UserPermissions { get; set; }
        public Workshop Workshop { get; set; }
        public List<ICWorkshop> ICWorkshops { get; set; }
        public List<FeaturePermission> featurePermissions { get; set; }

        public List<JoclaimsSetting> joclaimsSettings { get; set; }
    }
}
