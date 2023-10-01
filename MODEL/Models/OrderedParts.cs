using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
  public class OrderedParts
  {
    public int? AutomotivePartID { get; set; }
    public int? RequestID { get; set; }
    public int? RequestedPartID { get; set; }
    public int? QuotationID { get; set; }
    public string PartName { get; set; }
    public double? Price { get; set; }
    public int? Quantity { get; set; }
    public string StatusName { get; set; }
    public string ArabicStatusName { get; set; }

  }
}
