using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationPart
    {
        public int QuotationPartID { get; set; }
        public int QuotationID { get; set; }
        public int RequestedPartID { get; set; }
        public int AutomotivePartID { get; set; }
        public Int16? IsAvailableNow { get; set; }
        public double? Price { get; set; }
        public double? WithoutDiscountPrice { get; set; }
        public int Quantity { get; set; }
        public int? OrderedQuantity { get; set; }
        public Int16? ConditionTypeID { get; set; }
        public string ConditionTypeName { get; set; }
        public string ConditionTypeNameArabic { get; set; }
        public Int16? NewPartConditionTypeID { get; set; }
        public string NewPartConditionTypeName { get; set; }
        public string NewPartConditionTypeArabicName { get; set; }
        public bool WillDeliver { get; set; }
        public decimal? DeliveryCost { get; set; }
        public bool? IsReferred { get; set; }
        public double? ReferredPrice { get; set; }
        public bool? IsOrdered { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public byte? DeliveryTime { get; set; }
        public string AutomotivePartName { get; set; }
        public string AutomotivePartArabicName { get; set; }
        public int SupplierID { get; set; }

        public string SupplierPhoneNumber { get; set; }
        public Int16? ManufacturerID { get; set; }
        public string ManufacturerName { get; set; }        
        public bool? IsIncluded { get; set; }
        public string SupplierName { get; set; } 
        public string CityName { get; set; }
        public string BranchAreaName { get; set; }
        public Int16 ManufacturerRegionID { get; set; }
        public string ManufacturerRegionName { get; set; }
        public string NoteInfo { get; set; }
        public string BranchCityName { get; set; }
        public string BranchName { get; set; }
        public byte? Rating { get; set; }
        public int? PartBranchID { get; set; }
        public string RejectReason { get; set; }
        public bool? IsAccepted { get; set; } 
        public bool? IsRecycled { get; set; }
        public bool? IsAdminAccepted { get; set; }
        public string AdminRejectNote { get; set; }
        public int RowNumber { get; set; }
        public int? CityID { get; set; }
        public bool? IsReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public int? ReceivedBy { get; set; }
        public int isSpliced { get; set; }
        public DateTime? OrderedOn { get; set; }
        public string AcceptedByName { get; set; }
        public string WorkshopName { get; set; }
        public decimal? FillingRate { get; set; }
        public int OrderRowNumber { get; set; }
        public int? RequestPartCount { get; set; }
        public string RPConditionTypeName { get; set; }
        public string RPConditionTypeNameArabic { get; set; }
        public string RPNewPartConditionTypeName { get; set; }
        public string RPNewPartConditionTypeArabicName { get; set; }
        public List<Image> RejectedPartImage { get; set; }
        public bool? IsReturn { get; set; }
        public bool? IsWsAccepted { get; set; }
        public string WsRejectionNote { get; set; }
        public bool? IsLastPrice { get; set; }
        public bool? IsModified { get; set; }
        public string POPdfURL { get; set; }
        public string POPdfUrl { get; set; }
        public Int16? PartStatus { get; set; }
        public Int16? QuotationStatus { get; set; }
        public double? PreviousPrice { get; set; }
        public double? UsedAVGPrice { get; set; }
        public double? NEWAVGPrice { get; set; }
        public double? AftermarketAVGPrice { get; set; }
        public int TotalOffers { get; set; }
        public double? UsedPrice { get; set; }
        public double? NewPrice { get; set; }
        public double? AftermarketPrice { get; set; }
        public DateTime? UsedCreatedOn { get; set; }
        public DateTime? NewCreatedOn { get; set; }
        public DateTime? AftermarketCreatedOn { get; set; }
        public double? MinimumPrice { get; set; }
        public double? MaximumPrice { get; set; }
        public double? AvgPrice { get; set; }


        public int RejectedWorkshopPartReasonID  { get; set; }

        public int? RequestID { get; set; }
        public int? IsUniversal { get; set; }

        public double? DepriciationPrice { get; set; }

        public double? Depriciationvalue { get; set; }
        

    }
}
