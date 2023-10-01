using MODEL.InspectAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekResponse
    {
        public List<TchekDamages> damages { get; set; }
        public List<TchekDamageImageList> damageImageList { get; set; }
        public List<TchekDamageImageList> markers { get; set; }
        public List<TchekDamageCost> damageCost { get; set; }
        public List<DamagePoint> damagePoint { get; set; }
        public List<TchekImages> images { get; set; }
        public TchekInspection tchek { get; set; }
        public AIInspectionRequest CustomerData { get; set; }
        public TchekVehicle vehicle { get; set; }
        public TchekDevice device { get; set; }
    }
}
