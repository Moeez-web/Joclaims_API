using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Permission : UserPermission
    {
        public Int16 PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string PermissionArabicName { get; set; }
        public int? RoleID { get; set; }
        public string UserObjectName { get; set; }

    }
}
