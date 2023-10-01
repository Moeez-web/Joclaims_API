using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class DraftRequestReport
    {
        public string AccidentNo { get; set; }
        public double? DraftRequestPartCount { get; set; }
        public double? DrafTotalLabour { get; set; }
        public double? RequestPartcount { get; set; }
        public double? RequestLabor { get; set; }
        public string WorkshopName { get; set; }
        public DateTime? createdOn { get; set; }
        public string LabourDescription { get; set; }
    }
}
