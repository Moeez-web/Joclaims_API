using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Request_Draft
{
    public class DraftData
    {
        public RequestDraft RequestDraft { get; set; }
        public List<AccidentMarker> DraftMarkers { get; set; }
        public List<RequestDraftTask> RequestDraftTask { get; set; }
        public List<RequestDraftImage> RequestDraftImage { get; set; }
        public List<RequestDraftParts> RequestDraftParts { get; set; }
        public List<RequestDraftImage> RequestDraftPartImage { get; set; }
    }
}
