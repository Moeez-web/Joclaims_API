using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public  class TechnicalNotesData
    {
        public TechnicalNotes TechnicalNotes { get; set; }
        public List<Accident> accidents { get; set; }
        public List<AccidentCost> accidentCosts { get; set; }
        public List<AccidentMarker> accidentMarker { get; set; }
        public List<TRApproval> TRApprovalEmployees { get; set; }
        public List<TRApproval> TRApprovalLogEmployees { get; set; }
        public List<DamagePoint> CommonDamagePoints { get; set; }
        public List<AccidentCost> accidentInfo { get; set; }
        public List<Image> AccidentImages { get; set; }
    }
}
