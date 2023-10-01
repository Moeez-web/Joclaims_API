using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AppSetting
    {
        public bool? IsForceUpdated { get; set; }
        public string AppVersion { get; set; }
        public bool? IsLogout { get; set; }
    }
}
