using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Export_Documents
{
    public class ExportDocuments
    {
        public List<Image> images { get; set; }
        public string CompanyName { get; set; }
        public string timeStamp { get; set; }
        public string AccidentNumber { get; set; }
    }
}
