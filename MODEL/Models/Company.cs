using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Company : User
    {
      
        //public int CompanyID { get; set; }
        public int EmployeeID { get; set; }
        public string ProfileImageURL { get; set; }
        public string Name { get; set; }
        public string LogoURL { get; set; }
        //public Int16? CityID { get; set; }
        public Int16? CountryID { get; set; }
        //public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CPFirstName { get; set; }
        public string CPLastName { get; set; }
        public Int16? CPPositionID { get; set; }
        public string CPPositionName { get; set; }
        public string CPPositionArabicName { get; set; }
        public string CPPhone { get; set; }
        public string CPEmail { get; set; }
        public Int16? StatusID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        //public string RoleName { get; set; }
        public string CityName { get; set; }

        public string CityNameArabic { get; set; }

        public string CountryNameArabic { get; set; }
        public string CountryName { get; set; }
        public string TypeName { get; set; }
        public string StatusName { get; set; }
        public string CompanyCode { get; set; }
        public string ESignatureURL { get; set; }
        public string FaxNumber { get; set; }
        public string WebsiteAddress { get; set; }
        public int TotalQuotations { get; set; }
        public Double? DiscountValue { get; set; }
        public int? AccidentLimit { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }

        public int? SuggestionOfferTime { get; set; }

        public string CurrencyEnglish  { get; set; }

        public string CurrencyArabic   { get; set; }

    }
}
