using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.InspectAI
{
    public class AIInspectionRequest
    {
        public int? CustomerRequestID { get; set; }
        public string VehicleOwnerName { get; set; }
        public string OwnerPhoneNo { get; set; }
        public string VIN { get; set; }
        public string PlateNo { get; set; }
        public string AICaseID { get; set; }
        public string AIToken { get; set; }
        public string AILink { get; set; }
        public string AccidentNo { get; set; }
        public int? UserID { get; set; }
        public string SMSbody { get; set; }
        public int CompanyID { get; set; }
        public string AIResponseStatus { get; set; }
        public string AIResponseMessage { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public int? ServiceID  { get; set; }
    }
}
