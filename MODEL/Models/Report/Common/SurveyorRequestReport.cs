using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
  public class SurveyorRequestReport
  {
    public int UserID { get; set; }
    public string UserName { get; set; }
    public int? TotalRequests { get; set; }
    public int? TotalParts { get; set; }
    public double? TotalLabourPrice { get; set; }
    public int? ZeroLabour { get; set; }
    public double? AverageLabour { get; set; }
    public double? AveragePartsPerRequest { get; set; }
  }
}
