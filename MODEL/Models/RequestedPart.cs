using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestedPart : AutomotivePart
    {
        public int RequestedPartID { get; set; }
        public Int16 ConditionTypeID { get; set; }
        public string ConditionTypeName { get; set; }
        public int isSpliced { get; set; }
        public string ConditionTypeNameArabic { get; set; }
        public Int16? NewPartConditionTypeID { get; set; }
        public string NewPartConditionTypeName { get; set; }
        public string NewPartConditionTypeArabicName { get; set; }
        public double? DesiredPrice { get; set; }
        public int? Quantity { get; set; }
        public Int16? DesiredManufacturerID { get; set; }
        public string DesiredManufacturerName { get; set; }
        public Int16? DesiredManufacturerRegionID { get; set; }
        public string DesiredManufacturerRegionName { get; set; }
        public int RequestID { get; set; }
        public int? DemandID { get; set; }
        public int? DemandedQuantity { get; set; }
        public string PartImgURL { get; set; }
        public string EncryptedName { get; set; }
        public string AutomotivePartName { get; set; }
        public int ImageRef { get; set; }
        public string NoteInfo { get; set; }
        public double? Price { get; set; }
        public byte DeliveryTime { get; set; }
        public Int16? IsAvailableNow { get; set; }
        public bool? IsIncluded { get; set; }
        public int? QuotationPartID { get; set; }
        public int PartBranchID { get; set; }
        public byte? Rating { get; set; }
        public string RejectReason { get; set; }
        public string PartRejectReason { get; set; }
        public bool? IsAccepted { get; set; }
        public bool? IsPartApproved { get; set; }
        public bool? IsRecycled { get; set; }
        public int? AccidentPartID { get; set; }
        public int? AccidentID { get; set; }
        public string CreatedByName { get; set; }
        public string RPConditionTypeName { get; set; }
        public string RPConditionTypeArabicName { get; set; }
        public string RPNewPartConditionTypeName { get; set; }
        public string RPNewPartConditionTypeArabicName { get; set; }
        public string ESignatureURL { get; set; }
        public int? IsPartAvailable { get; set; }
        public int? RecommendedSupplierID { get; set; }
        public string RecommendedSupplierName { get; set; }
        public int? MinPriceQuotationPartID { get; set; }
        public double? MinPrice { get; set; }
        public bool? IsBringOldPart { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public Int16? PartStatus { get; set; }
        public string AccidentNo { get; set; }
        public string TypeName { get; set; }
        public bool? isExistInAccident { get; set; }
        public string Name1 { get; set; }
        public int? AutomotivePartID { get; set; }
        public double MaxRequestedPartPrice  { get; set; }
        public double MinRequestedPartPrice { get; set; }
        public double AvgRequestedPartPrice { get; set; }
        //public List<RequestedPart> RequestedParts { get; set; }
        //public RequestedPart requestedPart { get; set; }
        //public int? IsImgExist { get; set; }

        public int IsCheck { get; set; }
        public int IsUniversal { get; set; }
        public double? UsedPriceByUser { get; set; }
        public double? NewOriginalPriceByUser { get; set; }
        public double? NewAfterMarketPriceByUser { get; set; }
        public double? UniversalPartPriceByUser { get; set; }
        public double? expctedPrice { get; set; }
        public double? DraftRequestedPartPrice { get; set; }

        public double? Depriciationvalue { get; set; }


    }
}
