using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class SupplierReport
    {
    public List<MakeOffer> MakeOffers { get; set; }
    public List<SupplierOffer> SupplierOffers { get; set; }
    public int? TotalRequestsReceived { get; set; }
    public int? TotalRequestsApplied { get; set; }
    public int? TotalRequestsAppliedWithNotAvailable { get; set; }
    public int? TotalPricingAndProvidingRequestsReceived { get; set; }
    public int? TotalPricingAndProvidingRequestsApplied { get; set; }
    public int? TotalPricingAndProvidingRequestsAppliedWithNotAvailable { get; set; }
    public int? TotalPricingOnlyRequestsReceived { get; set; }
    public int? TotalPricingOnlyRequestsApplied { get; set; }
    public int? TotalPricingOnlyRequestsAppliedWithNotAvailable { get; set; }
    public int? TotalSupplierOffers { get; set; }

  }
}
