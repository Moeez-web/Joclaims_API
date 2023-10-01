using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationSummary
    {
        public Request Request { get; set; }
        public List<Quotation> Quotations { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<QuotationPartRef> QuotationParts { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<Image> QuotationPartsImages { get; set; }
    }
}
