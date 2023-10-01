using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class UpdateAutomotivePart
    {
        public AutomotivePart oldAutomotivePart { get; set; }
        public AutomotivePart mergeAutomotivePart { get; set; }
        public List<AutomotivePart> ReplaceWithPart { get; set; }
        public int UserID { get; set; }
        public int StatusID { get; set; }
    }
}
