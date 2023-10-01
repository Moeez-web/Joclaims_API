using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekDamageCost
    {
        public int? DamageCostID { get; set; }
        public string TchekID { get; set; }
        public string CostType { get; set; }
        public double? Cost { get; set; }
        public string Severity { get; set; }
        public string PointName { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
