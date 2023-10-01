using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationData : RequestData
    {
        public Quotation Quotation { get; set; }
        //public List<QuotationPart> QuotationParts { get; set; }
        public List<PartBranch> Branches { get; set; }
        public int ModifiedBy { get; set; }
        public int RequestID { get; set; }
        public int QuotationID { get; set; }
        public List<Note> AccidentNotes { get; set; }
        public int? WorkShopID { get; set; }
        public List<int> QuotationIDUnderTenPercent { get; set; }
        //public List<Image> AccidentImages { get; set; }

        public string ReasonNotSelectLMO { get; set; }

        public int LMO_QuotationID { get; set; }

        public List<PartsBargain> partsBargains { get; set; }
    }
}
