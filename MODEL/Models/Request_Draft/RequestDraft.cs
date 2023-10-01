using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Request_Draft
{
    public class RequestDraft:Request
    {
        public int DraftID { get; set; }
        public Int16 StatusID { get; set; }
        public bool? IsAccidentExist { get; set; }

        public bool? IsAIReport { get; set; }

        public bool? IsAIDraft { get; set; }

        public DateTime? LossDate { get; set; }

        public string RejectDraftReason { get; set; }

        public string PoliceReportNumber { get; set; }
        public int? Milage { get; set; }

        public int? MilageUnit { get; set; }


        public string RestoreDraftReason { get; set; }
        public int? IsAgencyDraft { get; set; }


        public string PolicyNumber { get; set; }




    }
}
