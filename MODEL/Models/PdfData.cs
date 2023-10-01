using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PdfData
    {
        public string elementHtml { get; set; }
        public string AccidentNo { get; set; }
        public string CompanyName { get; set; }
        public int AccidentID { get; set; }
        public int ModifiedBy { get; set; }
        public int RequestID { get; set; }
        public int RequestNumber { get; set; }
        public int StatusID { get; set; }
        public int SupplierID { get; set; }
        public int QuotationID { get; set; }
        public List<PdfData> SupplierPdfs { get; set; }
    }
}
