using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestMetaData
    {
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public List<AutomotivePart> AutomotivePart { get; set; }
        public List<Accident> Accidents { get; set; }
        public List<PartManufacturer> PartManufacturers { get; set; }
        public List<Country> Countries { get; set; }
        public List<AccidentPart> AccidentParts { get; set; }
        public List<Image> AccidentPartsImages { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public List<JCSeriesCase> jCSeriesCases { get; set; }
    }
}
