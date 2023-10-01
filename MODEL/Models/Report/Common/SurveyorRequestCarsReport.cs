using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
 public class SurveyorRequestCarsReport
  {
    public int UserID { get; set; }
    public int MakeID { get; set; }
    public string EnglishMakeName { get; set; }
    public int TotalVehicleCount { get; set; }
    public double? VehicleCost { get; set; }
    public double? AverageCost { get; set; }
  }
}
