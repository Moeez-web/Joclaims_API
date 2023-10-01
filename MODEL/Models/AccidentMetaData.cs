using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AccidentMetaData
    {
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public List<Accident> Accidents { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<ICWorkshop> Workshops { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public List<AutomotivePart> AutomotivePart { get; set; }
        public List<ObjectType> ObjectTypes { get; set; }
        public int TotalPages { get; set; }
        public TabInfo TabInfoData { get; set; }
        public TechnicalNotes TechnicalNotes { get; set; }
        public List<TRApproval> TRApprovalEmployees { get; set; }
        public List<Employee> Users { get; set; }
        public List<ICWorkshop> Agency { get; set; }

        public List<FaultyCompany> faultyCompany { get; set; }
        public List<Country> AllCountries { get; set; }
        public List<ReplacementCar> ReplacementCar { get; set; }

    }
}
