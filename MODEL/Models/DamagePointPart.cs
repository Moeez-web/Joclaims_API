using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class DamagePointPart
    {
        public int DamagePointPartID { get; set; }
        public int DamagePointID { get; set; }
        public int AutomotivePartID { get; set; }
        public string PartName { get; set; }
        public string PartNameEnglish { get; set; }
    }
}