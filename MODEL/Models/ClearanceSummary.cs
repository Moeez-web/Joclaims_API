using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ClearanceSummary
    {
        public Request Request { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<RequestTask> RequestTasks { get; set; }
        public List<Quotation> Quotations { get; set; }

        public List<Employee> Employees { get; set; }
    }
}
