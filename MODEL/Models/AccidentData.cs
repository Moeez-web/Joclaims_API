using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MODEL.InspectAI;
namespace MODEL.Models
{
    public class AccidentData
    {
        public Accident Accident { get; set; }
        public List<Note> Notes { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public List<AccidentPart> AccidentParts { get; set; }
        public List<Image> AccidentPartsImages { get; set; }
        public List<ObjectType> ObjectTypes { get; set; }
        public TechnicalNotes TechnicalNotes { get; set; }

        public List<AIInspectionRequest> customerRequests { get; set; }
    }
}
