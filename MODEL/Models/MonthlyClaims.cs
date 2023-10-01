using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class MonthlyClaims
    {
        public int AccidentID { get; set; }
        public string AccidentNo { get; set; }
        public string VIN { get; set; }
        public string ArabicMakeName { get; set; }
        public string ArabicModelName { get; set; }
        public string VehicleOwnerName { get; set; }
        public string AccidentType { get; set; }
        public string CreatedOn { get; set; }
        public int? RequestCount { get; set; }
        public int? PublishedRequestCount { get; set; }
        public int? UnPublishedRequestCount { get; set; }
        public int? QuotationCount { get; set; }
        public int? AvailableOffers { get; set; }
        public int? NOTAvailableOffers { get; set; }
        public int? MatchingOffers { get; set; }
        public int? NotMatchingOffers { get; set; }
        public string BrokerName { get; set; }

        public int? IsInstantPriceCount { get; set; }

  }
}
