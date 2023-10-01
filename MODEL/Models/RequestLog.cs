using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestLog
    {
        public Vehicle Vehicle { get; set; }
        public List<Request> Requests { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<RequestTask> RequestTasks { get; set; }
        public List<Accident> Accidents { get; set; }
        public List<AccidentPart> AccidentParts { get; set; }
        public List<Note> AccidentNotes { get; set; }
        public List<POApproval> POApprovals { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<Image> AccidentPartsImages { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public bool IsRequestWorkshopIC { get; set; }
    }
}
