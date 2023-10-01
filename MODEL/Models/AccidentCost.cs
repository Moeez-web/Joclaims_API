using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AccidentCost { 
        public int? AccidentCostID { get; set; }
        public int AccidentID { get; set; }
        public double? Labour { get; set; }
        public double? SpareParts { get; set; }
        public double? ValueLoss { get; set; }
        public double? CommercialDiscount { get; set; }
        public double? Excess { get; set; }
        public double? Depreciation { get; set; }
        public double? NetLoss { get; set; }
        public double? RentPerDay { get; set; }
        public double? NetCost { get; set; }
        public short? AccidentTypeID { get; set; }
        public double? TortCompensation { get; set; }
        public int? FinalPartsAmount { get; set; }
        public double? Total { get; set; }
        public bool? PreviousAccident  { get; set; }
        public string VehicleOwnerName { get; set; }
        public int ResponsibilityTypeID { get; set; }
        public string damagePointText { get; set; }
        public string AccidentNote { get; set; }
        public int? LabourTime { get; set; }
        public double? Percentage { get; set; }

        public string ValueLossNote  { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? EventDateTime { get; set; }
        public DateTime? CreatedOn { get; set; }
        public double? OldSpareParts { get; set; }
        public string SupplierName { get; set; }
        public string WorkshopName { get; set; }
        public int? RequestID { get; set; }
        public double? OtherExpenses { get; set; }
        public string OtherExpensesNote { get; set; }
        public bool? IsApproved { get; set; }
        public double? PreviousSpareParts { get; set; }
        public int? SerialNo { get; set; }
        public int? RequestRowNumber { get; set; }
        public double? Refunds { get; set; }
        public string ChangePriceReason { get; set; }
        public string NetVehicleCostNote { get; set; }
        public double? ResponsibilityPercentage { get; set; }
        public string SurveyorName { get; set; }
        public int? TotalQuotationCount { get; set; }
        public int? TotalNotAvailableCount { get; set; }

        public bool? IsChecked { get; set; }
        public bool? IsCheckedtemp { get; set; }

        public bool? IsPublished { get; set; }

        
    }
}
