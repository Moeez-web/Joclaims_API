using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationPartRef : QuotationPart
    {
        public new int? QuotationPartID { get; set; }
        new public int? AutomotivePartID { get; set; }
        public new int? QuotationID { get; set; }
        public new int? RequestedPartID { get; set; }
        public new int? Quantity { get; set; }
        public int ItemRank { get; set; }
        public double? NetPrice { get; set; }
        public bool? IsRecommended { get; set; }
        public bool? IsPartialSellings { get; set; }
        public bool? IsAccepted { get; set; }
        public double? PreviousPrice { get; set; }

    }
}
