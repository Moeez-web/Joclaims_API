using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class DemandProfile
    {
        public Request demand { get; set; }
        public List<RequestedPart> DemandedParts { get; set; }
        public List<Image> DemandedPartsImages { get; set; }
    }
}
