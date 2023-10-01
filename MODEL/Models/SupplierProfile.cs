using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class SupplierProfile
    {
        public Supplier Supplier { get; set; }
        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }
        public List<ObjectType> Positions { get; set; }
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public List<PartInfo> PartsInfo { get; set; }

        public List<AutomotivePart> AutomotiveParts { get; set; }
        public List<AutomotivePart> UniversalPart { get; set; }
        public List<Model> MakeGroups { get; set; }
    }
}
