using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public  class ICStaffKPI
    {
        public string AccidentNo { get; set; }
        public string VIN { get; set; }
        public string PlateNo { get; set; }
        public string AccidentCreatedOn { get; set; }
        public string AccidentCreatedBy { get; set; }
        public string DelayInFirstRequestCreation { get; set; }
        public string RequestNumber { get; set; }
        public string RequestCreatedOn { get; set; }
        public string RequestCreatedBy { get; set; }
        public string PublishedOn { get; set; }
        public string PublishedBy { get; set; }
        public string DelayInPublishRequest { get; set; }
        public string CurrentStatus { get; set; }
        public string ReferredOn { get; set; }
        public string DelayInPOCreation { get; set; }
        public string OrderOn { get; set; }
        public string OrderByName { get; set; }
        public string DelayInPOApproval { get; set; }
        public string Approval { get; set; }
        public int? DelayInPublishRequestInMin { get; set; }
        public int? DelayInFirstRequestCreationInMin { get; set; }
        public int? DelayInPOApprovalInMin { get; set; }
    }
}
