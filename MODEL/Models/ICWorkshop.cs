using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ICWorkshop : Workshop
    {

        public int ICWorkshopID { get; set; }
        public int? CompanyID { get; set; }
        public int? EmployeeID { get; set; }
        public string CompanyCode { get; set; }
        public bool? IsApproved { get; set; }

        public string CompanyName { get; set; }
        public string WorkshopOwnerName { get; set; }
        public double? TotalRepairOrderAmount { get; set; }
        public int? RepairOrderCount { get; set; }
        public Int16? StatusID { get; set; }
    public int? AccountID { get; set; }
    public string AccountNumber { get; set; } 
    public bool? IsAIDraft { get; set; }
    public bool? IsLossDate { get; set; }
    public bool? IsPoliceReportNumber { get; set; }
    public bool? IsMilage { get; set; }
    public bool? IsMilageImage { get; set; }
    public bool? IsVINImage { get; set; }
    public bool? IsVIN { get; set; }
    public bool? AskForPartCondition { get; set; }
    }
}
