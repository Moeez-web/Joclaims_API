using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class TabInfo
    {
        //Accident screen tabs count
        public int OpenedAccidents { get; set; }
        public int ClosedAccidents { get; set; }
        public int DeletedAccidents { get; set; }

        //IC Request list screen tabs count
        public int PendingRequests { get; set; }
        public int InprogressRequests { get; set; }
        public int ReferredRequests { get; set; }
        public int PendingApprovalRequests { get; set; }
        public int OrderPlacedRequests { get; set; }
        public int DeliveredRequests { get; set; }
        public int PaidRequests { get; set; }
        public int CancelledRequests { get; set; }
        public int ClosedRequests { get; set; }
        public int DeletedRequests { get; set; }

        // Jolcaims admin request list screen tabs count
        public int DemandedRequests { get; set; }
        public int? PendingParts { get; set; }
        public int? ApprovedParts { get; set; }
        public int? RejectedParts { get; set; }
        public int? RejectedQuotation { get; set; }
        public int? DraftCount { get; set; }
        public int? AutomotivePartCount { get; set; }
        public int? AccidentDraftCount { get; set; }

        public int EstimationPriceRequests { get; set; }
    }
}
