using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PSDashboard
    {
        public List<GraphInfo> Quotations { get; set; }
        public List<GraphInfo> Requests { get; set; }
    }
}
