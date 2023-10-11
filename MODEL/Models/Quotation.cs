using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Quotation
    {
        public bool? WillDeliver { get; set; }
        public decimal? DeliveryCost { get; set; }
        public int QuotationID { get; set; }
        public int RequestID { get; set; }
        public int DemandID { get; set; }
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public Int16? StatusID { get; set; }
        public string StatusName { get; set; }
        public string ArabicStatusName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string ModifiedByName { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeliveryTime { get; set; }
        public string MakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public string ModelCode { get; set; }
        public bool? IsNotAvailable { get; set; }
        public string GroupName { get; set; }
        public string ArabicModelName { get; set; }
        public int YearCode { get; set; }
        public string CreatedSince { get; set; }

        public string CreatedSinceArabic{ get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int ProductionYear { get; set; }
        public string VIN { get; set; }
        public string ImgURL { get; set; }
        public string CPEmail { get; set; }
        public string CPPhone { get; set; }
        public byte Rating { get; set; }
        public int PendingQuotationParts { get; set; }
        public int? RequestNumber { get; set; }
        public string InvoiceImage { get; set; }
        public int? CountDownTime { get; set; }
        public DateTime? CountDownDate { get; set; }
        public string JoReviewNote { get; set; }
        public Int16? JoReviewStatusID { get; set; }
        public int? RequestPartCount { get; set; }
        public int? ProcessedQuotationParts { get; set; }
        public int? ReferredPartsCount { get; set; }
        public string BranchName { get; set; }
        public string BranchAreaName { get; set; }
        public string Comment { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public bool? IsDiscountAvailable { get; set; }
        public Double? DiscountValue { get; set; }
        public int? QuotedPartCount { get; set; }
        public double? BiddingHours { get; set; }
        public DateTime PublishedOn { get; set; }
        public Int16? PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeArabicName { get; set; }
        public int RecommendationType { get; set; }
        public decimal? FillingRate { get; set; }
        public double? BestOfferPrice { get; set; }
        public double? OriginalPrice { get; set; }
        public double? LowestOfferMatchingPrice { get; set; }
        public decimal? MatchingFillingRate { get; set; }
        public double? SuggestedPrice { get; set; }
        public bool? IsSuggestionAccepted { get; set; }
        public double? OldLowestOfferMatchingPrice { get; set; }
        public int MatchingOfferSortNo { get; set; }
        public int OfferSortNo { get; set; }
        public string POPdfUrl { get; set; }
        public string AccidentNo { get; set; }
        public string PONote { get; set; }
        public string CompanyName { get; set; }
        public Double? POTotalAmount { get; set; }
        public DateTime? OrderOn { get; set; }
        public Int16? SupplierType { get; set; }
        public bool? IsPartialSellings { get; set; }
        public bool? IsSuggestedPriceSet { get; set; }
        public double? PreviousBestOfferPrice { get; set; }
        public double? PreviousLowestOfferMatchingPrice { get; set; }
        public double? PreviousOriginalPrice { get; set; }
        public double? TotalAmount { get; set; }
        public bool? IsRequestWorkshopIC { get; set; }
        public double? CounterOfferPrice { get; set; }
        public bool? ISCounterOfferAccepted { get; set; }

        public DateTime? SuggestionCounterTime  { get; set; }
        public bool? IsPrioritySupplier { get; set; }
        public Double? POROAmount { get; set; }
        public Double? LabourDays { get; set; }

    }
}
