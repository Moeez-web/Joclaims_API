using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekVehicle
    {
        public int? VehicleID { get; set; }
        public string TchekVehicleID { get; set; }
        public string TchekID { get; set; }
        public string Immat { get; set; }
        public DateTime? TchekCreatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
