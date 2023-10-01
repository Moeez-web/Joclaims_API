using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AccidentDamagePartsMeta
    {
        public List<AutomotivePart> DamagePointParts { get; set; }
        public List<DamagePoint> DamagePoints { get; set; }
    }
}
