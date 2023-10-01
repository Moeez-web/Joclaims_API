using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class EstimationPrice
    {
        public int RequestID { get; set; }

        public List<RequestedPart> RequestedPart { get; set; }

        public double TotalEsitimatedPartsPrice { get; set; }
    }
}
