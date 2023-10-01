using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestAllOffersData
    {
        public Request Request { get; set; }
        public List<Quotation> SupplierQuotations { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<PartBranch> Branches { get; set; }
        public List<Note> AccidentNotes { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<QuotationPart> QuotationParts { get; set; }
        public List<Image> QuotationPartsImages { get; set; }
    }
}
