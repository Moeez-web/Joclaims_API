using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekDevice
    {
        public int? DeviceID { get; set; }
        public string TchekDeviceID { get; set; }
        public string TchekID { get; set; }
        public string Address { get; set; }
        public string WebObjectId { get; set; }
        public string GpsLocation { get; set; }
        public DateTime? TchekCreatedOn { get; set; }
        public string IpMachine { get; set; }
        public string SerialNumber { get; set; }
        public bool? Status { get; set; }
        public int? Mode { get; set; }
        public bool? FromDistant { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }

    }
}
