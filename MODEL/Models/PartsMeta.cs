using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PartsMeta
    {
        public List<AutomotivePart> Name1 { get; set; }
        public List<AutomotivePart> AutomotivePart { get; set; }
        public TabInfo TabData { get; set; }
        public int? AutomotivePartCount { get; set; }
    }
}
