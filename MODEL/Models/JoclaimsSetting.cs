using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class JoclaimsSetting
    {
        public int JoclaimsSettingID  { get; set; }

        public int ServiceID  { get; set; }

        public string ServiceName  { get; set; }

        public int SettingTypeID  { get; set; }

        public string SettingTypeName  { get; set; }

        public int? CountryID   { get; set; }
    }
}
