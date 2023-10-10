using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class CarReadyImagePrice
    {
        public List<Image> image { get; set; }
        public double? TotalPrice { get; set; }
        public int? RequestID { get; set; }
        public int? IsEnterLabourPartPriceChecked { get; set; }
    }
}
