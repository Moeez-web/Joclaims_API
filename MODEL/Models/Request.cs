using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Request: Accident
    {
        public int RequestID { get; set; }
        public int? SerialNo { get; set; }
        new public string UserName { get; set; }
        public bool? IsNotAvailable { get; set; }

        //public int? CityID { get; set; }
        //public Int16? StatusID { get; set; }
        public Int16? AdminStatusID { get; set; }
        //public string StatusName { get; set; }
        //public string ArabicStatusName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PublishedOn { get; set; }
        //public int CreatedBy { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        //public bool IsDeleted { get; set; }
        //public string CreatedSince { get; set; }
        //public string CreatedSinceArabic { get; set; }
        public new int? AccidentID { get; set; }
        
        public string OrderByName { get; set; }
        //public int MakeID { get; set; }
        //public int ModelID { get; set; }
        //public int ProductionYear { get; set; }
        //public int CompanyID { get; set; }
        //public string VIN { get; set; }
        //public string MakeName { get; set; }
        //public string ArabicMakeName { get; set; }
        //public string ModelCode { get; set; }
        //public string ArabicModelName { get; set; }
        //public int YearCode { get; set; }
        //public string AccidentNo { get; set; }
        public double? POTotalAmount { get; set; }
        public bool? WillDeliver { get; set; }
        public decimal? DeliveryCost { get; set; }


        public string CPPhone { get; set; }
        public string ICName { get; set; }
        public string CPName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }
        public int? QuotationID { get; set; }
        public int? DemandID { get; set; }
        public int? SupplierID { get; set; }
        public int TotalQuotations { get; set; }
        public int? TotalNotAvailableQuotations { get; set; }
        public int AdminTotalQuotations { get; set; }


        ////some comapy arrtibutes for data maping
        public string LogoURL { get; set; }
        public string CPEmail { get; set; }
        public string SupplierName { get; set; }
        public double? BiddingHours { get; set; }
        public int? RequestNumber { get; set; }
        public DateTime BiddingDateTime { get; set; }
        public int IsBiddingTimeExpired { get; set; }
        public bool? IsPublished { get; set; }
        public int? RejectionCount { get; set; }
        public bool? IsReady { get; set; }
        public bool? IsCarRepairStarted { get; set; }

        public string CompanyAddress1 { get; set; }
        public string CompanyAddress2 { get; set; }
        public string CompanyCountryName { get; set; }
        public string InvoiceImage { get; set; }
        public string SpAddressLine1 { get; set; }
        public string SpAddressLine2 { get; set; }
        public string SpCountryName { get; set; }
        public string SpCityname { get; set; }
        public string IcCancelOrderNote { get; set; }
        public string JoCancelOrderNote { get; set; }
        public string Comment { get; set; }
        public bool? IsDiscountAvailable { get; set; }
        public Double? DiscountValue { get; set; }
        public string ESignatureURL { get; set; }
        public int? TotalOffersCount { get; set; }
        public double? BestOfferPrice { get; set; }
        public double? BestMinOfferPrice { get; set; }
        public int? BestOfferQuotationID { get; set; }
        public int? BestOfferSupplierID { get; set; }
        public bool? IsOldPartsRequired { get; set; }
        public int? RejectedOffersCount { get; set; }
        public int? TotalRequestedParts { get; set; }
        public DateTime? DemandCreatedOn { get; set; }
        public string DemandCreatedSince { get; set; }
        public string DemandCreatedSinceArabic { get; set; }
        public int? QuotationsCount { get; set; }
        public double? OriginalPrice { get; set; }
        public double? RecommendedTotalPrice { get; set; }
        public double? LowestMatchingOffer { get; set; }
        public double? LowestNotMatchingOffer { get; set; }
        public double? SavingByRecommended { get; set; }
        public string PhoneNumber { get; set; }
        public int UserID { get; set; }
        public string WebsiteAddress { get; set; }
        public string FaxNumber { get; set; }
        public string CompanyName { get; set; }
        public string QuotationSummaryPdfUrl { get; set; }
        public int? RequestRowNumber { get; set; }
        public string AllOffersPdfUrl { get; set; }
        //public string ClearanceSummaryPdfUrl { get; set; }
        public double? BestMinOfferMatchingPrice { get; set; }
        public int? BestOfferMatchingQuotationID { get; set; }
        public int? BestOfferMatchingSupplierID { get; set; }
        //public DateTime? EventDateTime { get; set; }
        //public string ModifiedByName { get; set; }
        //public string CreatedByName { get; set; }
        //public string CreatedByEmail { get; set; }
        //public string ModifiedByEmail { get; set; }
        public double? LowestMatchingOfferPrice { get; set; }
        public string CustomerESignatureURL { get; set; }
        public string POPdfUrl { get; set; }
        public DateTime? OrderOn { get; set; }
        public double? TotalRepairOrderAmount { get; set; }
        public string ReturnReason { get; set; }
        public string PONote { get; set; }
        public string SupplierPhone { get; set; }
        public bool? IsLowestMatching { get; set; }
        public bool? IsPartialSellings { get; set; }
        public double? TotalTaskAmount { get; set; }
        public string ROPdfURL { get; set; }
        public int? AccidentCount { get; set; }
        public double? CarCost { get; set; }
        public double? LabourPrice { get; set; }
        public string JCSeriesCode { get; set; }
        public bool? IsMakeChange { get; set; }
        public bool? IsRequestWorkshopIC { get; set; }
        public string JoReviewNote { get; set; }
        public int? DraftID { get; set; }
        public short? DeletedStatusID { get; set; }
        public int? NumberRequestedPart { get; set; }
        public DateTime? POApprovalOn { get; set; }
        public double? FirstRequestLabourPrice { get; set; }
        public double? TotalPartLabourPrice { get; set; }
        
        public string NotPurchasingLMOReason { get; set; }


        public bool? IsTaskApproved { get; set; }

        public bool? IsPartApproved  { get; set; }

        public string RequestPdfUrl { get; set; }
        public double? EstimatedPrice { get; set; }
        public bool? IsInstantPrice  { get; set; }
        public int? OurResponsibility { get; set; }
        public string AIToken { get; set; }
        public string AICaseID { get; set; }
        public double? Depreciation { get; set; }
        public int? IsAgencyRequest { get; set; }
        public double? TotalCost { get; set; }
        public int? TotalLabour { get; set; }
        public string LPO { get; set; }
        public string ReferenceNo { get; set; }
        public string PolicyNumber { get; set; }
        public string section1 { get; set; }

        public string ClearanceSummaryFreeText  { get; set; }

        public string ClearanceSummaryImage { get; set; }
        public int? IsEnterLabourPartPriceChecked { get; set; }
        public string RepairOrderApprovedDate { get; set; }
        public string SectionTwo { get; set; }
        public string SectionThree { get; set; }
        public string SectionFour { get; set; }
        public int Discount { get; set; }
        public Double? VAT { get; set; }
        public Double? VATValue { get; set; }
        public Double? TOTAL { get; set; }
    }
}
