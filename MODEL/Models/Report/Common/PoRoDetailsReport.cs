using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class PoRoDetailsReport
    {
        public string AccidentNo { get; set; }
        public string PlateNo { get; set; }
        public string WorkshopName { get; set; }
        public double? labour { get; set; }
        public string SupplierName { get; set; }
        public double? PartsCost { get; set; }
        public DateTime? RepairOrderDate { get; set; }
    }
}
