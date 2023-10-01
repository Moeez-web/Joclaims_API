using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PublishRequest
    {
        public int RequestID { get; set; }
        public int ModifiedBy { get; set; }
        public int SerialNo { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<RequestTask> RequestedTask { get; set; }
    }
}
