using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ICDashboard
    {
        public List<GraphInfo> Accidents { get; set; }
        public List<GraphInfo> Requests { get; set; }
        public DashboardStats DashboardStatsInfo { get; set; }
        public List<DashboardAdditionalStats> DashboardAdditionalStatsData { get; set; }
        public List<Accident> FinalAccidentCosts { get; set; }
        public List<Accident> AccidentClearance { get; set; }
        public List<Supplier> SupplierStats { get; set; }
        public List<ICWorkshop> WorkshopStats { get; set; }
    }
}
