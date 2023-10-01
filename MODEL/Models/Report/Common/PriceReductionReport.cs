using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class PriceReductionReport
    {
        public string AccidentNo { get; set; }
        public int AccidentID { get; set; }
        public int RequestNumber { get; set; }
        public DateTime RequestCreatedDate { get; set; }
        public DateTime PurchaseOrderApprovedDate { get; set; }
        public DateTime PurchaseOrderCreatedOn { get; set; }
        public double? OldLowestOfferMatchingPrice { get; set; }
        public double? SuggestedPrice { get; set; }
        public double? Savings { get; set; }
        public double? LowestOfferMatchingPrice { get; set; }
        public bool? IsSuggestionAccepted { get; set; }
        public double? CounterOfferPrice { get; set; }
        public bool? ISCounterOfferAccepted { get; set; }
        public string BrokerName { get; set; }

        public string ICUserName { get; set; }

  }
}
