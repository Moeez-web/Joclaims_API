using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class VehicleSeriesMeta
    {
        public List<Make> Makes { get; set; }
        public List<Model> VehicleGroups { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public List<BodyTypes> BodyType { get; set; }
        public List<FuelTypes> FuelType { get; set; }
        //public List<ModelVariant> ModelVariants { get; set; }
        //public List<EngineCapacity> EngineCapacity { get; set; }
        public List<joClaimsSeriesData> joClaimsSeries { get; set; }
        public int? MinYear { get; set; }
        public int? MaxYear { get; set; }
        public int? PendingCount { get; set; }
        public int? ApprovedCount { get; set; }
        public int? RejectedCount { get; set; }
    }
}
