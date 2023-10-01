using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
  public class SurveyorDetailRequestReport
  {
    public int RequestID { get; set; }
    public int RequestNumber { get; set; }
    public double LabourPrice { get; set; }
    public int RequestedPartCount { get; set; }
    public double POTotalAmount { get; set; }
    public int UserID { get; set; }
    public string CreatedOn { get; set; }
    public string BrokerName { get; set; }
  }
}
