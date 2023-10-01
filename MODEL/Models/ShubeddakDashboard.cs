using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ShubeddakDashboard
    {
        public List<GraphInfo> Suppliers { get; set; }
        public List<GraphInfo> InsuranceCompanies { get; set; }
        public List<GraphInfo> Requests { get; set; }
    }
}
