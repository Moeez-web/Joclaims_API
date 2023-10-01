using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ShopQuotations : QuotationMetaData
    {
        public List<Quotation> Quotations { get; set; }
        public Quotation Quotation { get; set; }
        public List<QuotationPart> QuotationParts { get; set; }
        public List<Image> QuotationPartsImages { get; set; }
        public TabInfo TabInfoData { get; set; }
        public List<Company> Companies { get; set; }
    }
}
