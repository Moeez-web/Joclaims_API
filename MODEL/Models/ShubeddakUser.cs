using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ShubeddakUser: User
    {
        //public int UserID { get; set; }
        public int ShubeddakUserID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ProfileImageURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<Permission> UserPermissions { get; set; }
        public string ErrorMessage { get; set; }

    }
}
