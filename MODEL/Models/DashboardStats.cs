using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class DashboardStats
    {
        public int? AccidentCount { get; set; }
        public int? RequestCount { get; set; }
        public double? TotalLabourAmount { get; set; }
        public double? AVGLabourAmount { get; set; }
        public double? TotalPOAmount { get; set; }
        public double? AVGClaimCost { get; set; }
        public int? POCount { get; set; }
        public int? RepairOrderCount { get; set; }
        public int? PricingAndProviding { get; set; }
        // public int? PricingOnly { get; set; }
        public int? AccidentCountWithPO { get; set; }
        public int? RequestCountWithPO { get; set; }
        public double? TotalLabourWithRO { get; set; }
        public double? LowestMatchingPrices { get; set; }
    }
}
