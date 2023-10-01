using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Accident: ICWorkshop
    {
//        
public int AccidentID { get; set; }
        public Int16? AccidentTypeID { get; set; }
        public string AccidentTypeName { get; set; }
        public string ArabicAccidentTypeName { get; set; }
        public int ResponsibilityTypeID { get; set; }
        public string ResponsibilityTypeName { get; set; }
        public string ArabicResponsibilityTypeName { get; set; }
        public string StatusName { get; set; }
        public string ArabicStatusName { get; set; }

        //public int CompanyID { get; set; }
        public string AccidentNo { get; set; }
        public string VIN { get; set; }
        public string VehicleOwnerName { get; set; }
        public string PlateNo { get; set; }
        public int? MakeID { get; set; }
        public int? ModelID { get; set; } 
        
        public string GroupName { get; set; }
        public string MakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public string ModelCode { get; set; }
        public string ArabicModelName { get; set; }
        public string CarLicenseFront { get; set; }
        public string CarLicenseBack { get; set; }
        public string OwnerIDFront { get; set; }
        public string OwnerIDBack { get; set; }
        public string PoliceReport { get; set; }
        public int YearCode { get; set; }
        public int ProductionYear { get; set; }
        public new int? ICWorkshopID { get; set; }
        public string CreatedSince { get; set; }
        public DateTime? CreatedOnAccident { get; set; }
        public Int16? StatusID { get; set; }
        public string CreatedSinceArabic { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public int CreatedBy { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedOn { get; set; }
        public string ImgURL { get; set; }
        public string UserName { get; set; }
        public int? InprogressRequestCount { get; set; }

        public string PDFReport { get; set; }
        public string CarLicenseFrontDescription { get; set; }
        public string CarLicenseBackDescription { get; set; }
        public string OwnerIDFrontDescription { get; set; }
        public string OwnerIDBackDescription { get; set; }
        public string PoliceReportDescription { get; set; }
        public string PDFReportDescription { get; set; }
        public int? CarsInvolved { get; set; }
        public DateTime? AccidentHappendOn { get; set; }
        public Int16? EngineTypeID { get; set; }
        public Int16? BodyTypeID { get; set; }
        public string ImportantNote { get; set; }
        public string EngineTypeName { get; set; }
        public string EngineTypeArabicName { get; set; }
        public string BodyTypeName { get; set; }
        public string BodyTypeArabicName { get; set; }
        public string BodyTypeIcon { get; set; }
        public string FaultyCompanyName { get; set; }
        public string AccidentCreatedBy { get; set; }
        public DateTime? AccidentCreatedOn { get; set; }
        public int? TotalRequestCount { get; set; }
        public int? DemandCount { get; set; }
        public string ArabicBodyTypeName { get; set; }
        public string OwnerPhoneNo { get; set; }

        public double? VernishPrice { get; set; }
        public double? MechanicLabourPrice { get; set; }
        public double? DamagePrice { get; set; }
        public double? RentPrice { get; set; }
        public double? TotalLabourCost { get; set; }
        public int? TotalLabourTime { get; set; }
        public double? FinalPartsAmount { get; set; }
        public double? FinalLabourCost { get; set; }
        public double? TotalPartsAmount { get; set; }
        public double? GasPrice { get; set; }
        public double? TotalAccidentCost { get; set; }
        public double? FinalAccidentCost { get; set; }
        public int? FinalLabourTime { get; set; }
        public double? TotalOldPartsAmount { get; set; }
        public double? TotalMechanicLabourPrice { get; set; }
        public int? ClearanceModifiedBy { get; set; }
        public string ClearanceModifiedByName { get; set; }
        public DateTime? ClearanceModifiedOn { get; set; }
        public bool? IsPurchasing { get; set; }
        public string ClearanceSummaryPdfUrl { get; set; }
        public int? ClearanceCreatedBy { get; set; }
        public DateTime? ClearanceCreatedOn { get; set; }
        public string ClearanceCreatedByName { get; set; }
        public string ClearanceRoute { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public double? Saving { get; set; }
        public string WorkshopDetails { get; set; }
        public int? IsClearance { get; set; }
        public string Name { get; set; }
        public string CPPhone { get; set; }
        public string CPName { get; set; }
        public string ICName { get; set; }
        public int PendingRequestID { get; set; }
        public string CloseReason { get; set; }
        public string CompanyName { get; set; }
        public string JCSeriesID { get; set; }
        public string EnglishMakeName { get; set; }
        public string EnglishModelName { get; set; }
        public string ImageCaseEncryptedName { get; set; }
        public string ImageCaseURL { get; set; }
        public bool? AskForPartCondition { get; set; }
        public int? IndividualReturnID { get; set; }
        public string IndividualReturnText { get; set; }
        public Int16? TechnicalNotesStatus { get; set; }
        public string faultyPlateNo { get; set; }
        public string AccidentLocation { get; set; }
        public string FaultyPartyName { get; set; }
        public string AccidentConsequences { get; set; }
        public string TotalAmountInWritten { get; set; }
        public string InsuranceDocumentNo { get; set; }
        public string RequestStatusName { get; set; }
        public string RequestArabicStatusName { get; set; }
        public string PricingType { get; set; }
        public double? POAmount { get; set; }
        public string ChequeFreeText { get; set; }
        public DateTime? ChequeDate { get; set; }
        public DateTime? ChequeCreatedOn { get; set; }
        public string ChecqueApprovalSignature { get; set; }
        public string ChequePdfUrl { get; set; }
        public int? SignaturedBy { get; set; }
        public string SignaturedByName { get; set; }
        public string OtherPlusText { get; set; }
        public string OtherMinText { get; set; }
        public string ClosedByName { get; set; }
        public string BrokerName { get; set; }
        public int? ClaimentID { get; set; }

        public int? OurResponsibility { get; set; }

        public int AccidentCount { get; set; }

        public int? DraftID { get; set; }

        public int? RequestCount  { get; set; }

        public int? RequestPartCount  { get; set; }
        public int? PublishedRequestCount { get; set; }
        public int? UnPublishedRequestCount { get; set; }
        public int? QuotationCount { get; set; }
        public int? MatchingOffer { get; set; }
        public int? NotMatchingOffers { get; set; }


        public string IndividualReturnEnglishText { get; set; }


        public bool? isHighPriority { get; set; }
        public bool? isLearningSelected { get; set; }

        public int? SurveyorID { get; set; }

        public string SurveyorName { get; set; }

        public DateTime? SurveyorAppointmentDate { get; set; }

        public string PolicyNumber { get;set; }

        public bool? isTotalLossSelected { get; set; }
        //public bool? isSalvageSelected { get; set; }

        public int? MarketInsureValue { get; set; } 

        public int? SalvageAmount  { get; set; }

        public int? TotalLossOfferAmount { get; set; }
        public int? AgencyID { get; set; }
        public bool? IsAgencyAccident { get; set; }
        public int? IsAgencyRepair { get; set; }

        public string FaultyPolicyNumber { get; set; }

        public int? FaultyCompanyID { get; set; }

        public string FaultyCompanyNameDropDown { get; set; }
        public string FaultyVehiclePlateNo { get; set; }
        public string FaultyVehicleMakeName { get; set; }
        public string FaultyVehicleModelName { get; set; }
        public string VehicleCountryName { get; set; }
        public int? FaultyVehicleYearCode { get; set; }
        public int? FaultyVehicleMakeID { get; set; }
        public int? FaultyVehicleModelID { get; set; }
        public int? FaultyVehicleYearID { get; set; }
        public int? VehicleCountryID { get; set; }
        public bool? IsReplacementCar { get; set; }
        public int? ReplacementCarID { get; set; }
        public TimeSpan? AccidentHappendOnTime { get; set; }
        public string HappendOnTime { get; set; }
        public string ReplacementCarName { get; set; }
        public bool? IsDeductible { get; set; }
        public int? DeductibleAmount { get; set; }
        public int? DeductibleStatus { get; set; }
        public bool? IsSumInsured { get; set; }
        public int? SumInsuredAmount { get; set; }
        public string GeographicalExtension { get; set; }
        public bool? AOGCover { get; set; }
        public bool? SRCCCover { get; set; }
        public bool? IsWindshieldCover { get; set; }
        public int? WindshieldCoverAmount { get; set; }
    }
}
