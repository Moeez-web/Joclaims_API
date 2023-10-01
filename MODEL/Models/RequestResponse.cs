using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestResponse
    {
        public List<RequestedPart> RequestedParts { get; set; }
        public string Result { get; set; }

        public int RequestID { get; set; }
        public int DraftID { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string WorkshopName { get; set; }
        public string VIN { get; set; }
        public string AccidentNo { get; set; }
        public string VehicleOwnerName { get; set; }
        public string ArabicMakeName { get; set; }
        public string ArabicModelName { get; set; }
        public int? YearCode { get; set; }
        public int? AccidentID { get; set; }

        public bool? IsAIDraft { get; set; }
        public string PoliceReportNumber { get; set; }

        public DateTime? LossDate { get; set; }

        public string PolicyNumber { get; set; }    
        public string PlateNo { get; set; }    

        public int? RepairDays { get; set; }

        public int? CountryID { get; set; }
        public string ReplacementCarFooter { get; set; }

    }
}
