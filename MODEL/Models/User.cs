using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class User
    {
        public string Id { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime LockoutEndDateUtc { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public byte RoleID { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public string EmailVerifyToken { get; set; }
        public string Reset_token { get; set; }
        public string ConfirmPassword { get; set; }
        public Int16? CityID { get; set; }
        public string AddressLine1 { get; set; }

        public int? ICWorkshopID { get; set; }
        public string RoleName { get; set; }
        public string RoleNameArabic { get; set; }
        public string RoleIcon { get; set; }
        public string CommercialRegisterImg { get; set; }
        public string ProfessionLicenseImg { get; set; }
        public string MobileNotificationToken { get; set; }
        public string access_token { get; set; }
        public Int16? MobileTypeID { get; set; }
        public string AppVersion { get; set; }
        public string WorkshopGoogleMapLink { get; set; }
        public string WorkshopAreaName { get; set; }
        public bool isPriceEstimate { get; set; }
        public bool IsCompanyExist { get; set; }
        public int? CompanyTypeID { get; set; }
        public int? CountryID { get; set; }
        public string CurrencyEn { get; set; }

        public string CurrencyAr { get; set; }

        public string AppID { get; set; }



    }
}
