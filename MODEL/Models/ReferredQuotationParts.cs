using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ReferredQuotationParts
    {
        public int UserID { get; set; }
        public int tabId { get; set; }
        public List<QuotationPart> referredQuotationParts { get; set; }
    }
}
