using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ManufacturerRegion
    {
        public int ManufacturerRegionID { get; set; }
        public string ManufacturerRegionName { get; set; }
        public int TotalQuotations { get; set; }
        public bool? IsSelected { get; set; }
    }
}
