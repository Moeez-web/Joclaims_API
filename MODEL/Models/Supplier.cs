using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Supplier : User
    {
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        //public Int16? CityID { get; set; }
        public Int16? CountryID { get; set; }
        //public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string GoogleMapLink { get; set; }
        public string CPFirstName { get; set; }
        public string CPLastName { get; set; }
        public string CPPositionName { get; set; }
        public int OffersCount { get; set; }
        public string CPPositionArabicName { get; set; }
        public Int16? CPPositionID { get; set; }
        public string CPPhone { get; set; }
        public string CPEmail { get; set; }
        public Int16? StatusID { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? IsSellNew { get; set; }
        public bool? IsSellUsed { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string LogoURL { get; set; }
        //public string RoleName { get; set; }
        public string RegistrationNumber { get; set; }
        public string CityName { get; set; }

        public string CityNameArabic { get; set; }

        public string CountryName { get; set; }

        public string CountryNameArabic { get; set; }

        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public string ArabicStatusName { get; set; }

        public int TotalOrderedParts { get; set; }
        public int DemandID { get; set; }
        public int QuotationID { get; set; }
        public string RejectNote { get; set; }
        public int AlreadyApplied { get; set; }
        public byte Rating { get; set; }
        public int RequestID { get; set; }
        public int TotalDemands { get; set; }
        public int? ApplyRatio { get; set; }
        public Int16? PaymentTypeID { get; set; }
        public string PaymentTypeName { get; set; }
        public string PaymentTypeArabicName { get; set; }
        public int? TotalPO { get; set; }
        public double? TotalPOAmount { get; set; }
        public string IBAN { get; set; }
        public string ProfessionLicenseImg { get; set; }
        public string CommercialRegisterImg { get; set; }
        public Int16? SupplierType { get; set; }
        public string ArabicTypeName { get; set; }
        public string ContactPerson { get; set; }
        public int? TotalRequestsReceived { get; set; }
        public int? TotalRequestsApplied { get; set; }
        public double? SuccessRate { get; set; }
    public int? AccountID { get; set; }
    public string AccountNumber { get; set; }
    public string UserEncryptedID { get; set; }

        public string CurrencyEnglish { get; set; }

        public string CurrencyArabic { get; set; }
    
        public int? WorkshopID { get; set; }
    }
}
