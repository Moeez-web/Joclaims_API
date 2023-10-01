using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using ShubeddakAPI.Models;
using BAL.IManager;
using BAL.Manager;
using MODEL.Models;

namespace ShubeddakAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private readonly IAccountManager _accountManager;
        private readonly IProfileManager _profileManager;
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _accountManager = new AccountManager();
            _profileManager = new ProfileManager();
            _publicClientId = publicClientId;
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var data = await context.Request.ReadFormAsync();
            var AppID = data.Get("AppID");
            var param = data.Where(x => x.Key == "mobileNotificationToken").Select(x => x.Value).FirstOrDefault(); 
            var MobileTypeID = data.Where(x => x.Key == "MobileTypeID").Select(x => x.Value).FirstOrDefault(); 
            var AppVersion = data.Where(x => x.Key == "AppVersion").Select(x => x.Value).FirstOrDefault();
            var userObj = new User();

            if (MobileTypeID != null && MobileTypeID.Length > 0 && MobileTypeID[0] != "null")
            {
                userObj.MobileTypeID = Convert.ToInt16(MobileTypeID[0]);
            }

            if (AppVersion != null && AppVersion.Length > 0 && AppVersion[0] != "null")
            {
                userObj.AppVersion = Convert.ToString(AppVersion[0]);
            }


            if (param != null && param.Length > 0 && param[0] != "null")
            {
                userObj.MobileNotificationToken = param[0];
            }
            string username = context.UserName.Replace("%2B", "+");
            if (username.Contains("@"))
            {
                userObj.Email = username;
            }
            else
            {
                userObj.PhoneNumber = username;
            }

            userObj.Password = context.Password;
            
             userObj.AppID = AppID;
           
            
            var response = _accountManager.SignIn(userObj);

            if (response == null || response.UserObj == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            //if the company/supplier status is rejected/blocked then we will show it a block msg
            else if (response.Company != null && response.Company.StatusID == 3)
            {
                context.SetError("invalid_grant", "The User is Blocked");
                return;
            }
            //else if (response.Supplier != null && response.Supplier.StatusID == 3 )
            //{
            //    context.SetError("invalid_grant", "The User is Blocked");
            //    return;
            //}

            ApplicationUser user = userManager.FindById(response.UserObj.Id);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            var UserID = response.UserObj.UserID;

            var StatusID = response.Supplier != null && response.Supplier.UserID > 0 ? response.Supplier.StatusID : response.Company != null && response.Company.UserID > 0 ? response.Company.StatusID : response.Workshop != null && response.Workshop.UserID > 0 ? response.Workshop.StatusID : null;

            AuthenticationProperties properties = CreateProperties(UserID.ToString(), user.UserName, user.Id, response.UserObj.RoleID,
                user.Email != null ? user.Email : "",
                user.PhoneNumber != null ? user.PhoneNumber : "",
                response.ShubeddakUser != null ? response.ShubeddakUser.ShubeddakUserID : 0,
                response.ShubeddakUser != null ? response.ShubeddakUser.ProfileImageURL != null ? response.ShubeddakUser.ProfileImageURL : "" : "",
                //response.ShubeddakUser.UserID != null ? response.ShubeddakUser.UserID : 0, 
                response.Company != null ? response.Company.CompanyID : 0,
                response.Company != null ? response.Company.LogoURL != null ? response.Company.LogoURL : "" : "",
                //response.Company.UserID != null ? response.Company.UserID : 0,
                response.Supplier != null ? response.Supplier.SupplierID : (response.Workshop != null ? (response.Workshop.SupplierID) : (0)),
                response.Company != null ? response.Company.EmployeeID : 0,
                response.Company != null ? response.Company.ProfileImageURL != null ? response.Company.ProfileImageURL : "" : "",
                response.Supplier != null ? response.Supplier.LogoURL != null ? response.Supplier.LogoURL : "" : "",
                //response.Company != null ? response.Company.ICWorkshopID != null ? response.Company.ICWorkshopID : 0 : 0,
                response.UserPermissions != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.UserPermissions) : "",
                response.featurePermissions != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.featurePermissions) : "",
                StatusID, response.Company != null ? response.Company.CompanyCode : "",
                response.Company != null ? response.Company.CPFirstName + ' ' + response.Company.CPLastName : "",
                response.Workshop != null ? response.Workshop.ProfileImageURL != null ? response.Workshop.ProfileImageURL : "" : "",
                response.Workshop != null ?  response.Workshop.WorkshopID  : (response.Supplier != null ? (response.Supplier.WorkshopID) : (0)),
                response.Workshop != null ? response.Workshop.WorkshopName != null ? response.Workshop.WorkshopName : "" : "",
                response.Workshop != null ? response.Workshop.WorkshopGoogleMapLink != null ? response.Workshop.WorkshopGoogleMapLink : "" : "",
                response.Workshop != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.Workshop) : "",
                response.ICWorkshops != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.ICWorkshops) : "",
                response.UserObj.isPriceEstimate != null ? response.UserObj.isPriceEstimate : false,
                response.UserObj.IsCompanyExist != null ? response.UserObj.IsCompanyExist : false,
                response.UserObj.CompanyTypeID != null ? response.UserObj.CompanyTypeID : 0,
                response.Company != null ? response.Company.SuggestionOfferTime != null ? response.Company.SuggestionOfferTime:0: 0,
                response.joclaimsSettings != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.joclaimsSettings) : "",
                response.UserObj.CurrencyEn != null? response.UserObj.CurrencyEn: "",
                response.UserObj.CurrencyAr != null? response.UserObj.CurrencyAr : "",
                response.UserObj.CountryID != null? response.UserObj.CountryID : null
                //response.Company != null? response.Company.CurrencyEnglish != null ? response.Company.CurrencyEnglish:"":"",
                //response.Company != null? response.Company.CurrencyArabic != null ? response.Company.CurrencyArabic : "":"",
                //response.Supplier != null ? response.Supplier.CurrencyEnglish != null ? response.Supplier.CurrencyEnglish : "" : "",
                //response.Supplier != null ? response.Supplier.CurrencyArabic != null ? response.Supplier.CurrencyArabic : "" : "",
                //response.Workshop != null ? response.Workshop.CurrencyEnglish != null ? response.Workshop.CurrencyEnglish : "" : "",
                //response.Workshop != null ? response.Workshop.CurrencyArabic != null ? response.Workshop.CurrencyArabic : "" : ""
                //response.Company != null ? response.Company.SuggestionOfferTime != null ? response.Company.SuggestionOfferTime : 0 : 0
                //response.joclaimsSettings != null ? Newtonsoft.Json.JsonConvert.SerializeObject(response.joclaimsSettings) : ""
                ); 
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string UserID, string UserName,string Id,byte RoleID, string Email, string PhoneNumber,int ShubeddakUserID, 
               string ProfileImageURL, int CompanyID, string LogoURL, int? SupplierID,int EmployeeID, string EmployeeProfileImageURL, string SupplierLogoURL, string UserPermissions, string featurePermissions, Int16? StatusID, string CompanyCode, string CPName, string WorkshopProfileImageURL, int? WorkshopID, string WorkshopName, string WorkshopGoogleMapLink, string Workshop,string ICWorkshops, bool isPriceEstimate,bool IsCompanyExist,int? CompanyTypeID, int? SuggestionOfferTime,string joclaimssetting,string CurrencyEn,string CurrencyAr,int? CountryID
               /*,string SupplierCurrencyEnglish,string SupplierCurrencyArabic, string WorkShopCurrencyEnglish, string WorkShopCurrencyArabic*/)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "UserId", UserID},
                { "UserName", UserName },
                { "Id", Id },
                { "RoleID", RoleID.ToString() },
                { "Email", Email },
                { "PhoneNumber", PhoneNumber },
                { "ProfileImageURL", ProfileImageURL },
                { "LogoURL", LogoURL },
                { "EmployeeProfileImageURL", EmployeeProfileImageURL },
                { "ShubeddakUserID", ShubeddakUserID.ToString() },
                { "CompanyID", CompanyID.ToString() },
                { "SupplierID", SupplierID.ToString() },
                { "EmployeeID", EmployeeID.ToString() },
                { "SupplierLogoURL", SupplierLogoURL },
                { "UserPermissions", UserPermissions },
                { "featurePermissions", featurePermissions },
                { "CompanyCode", CompanyCode },
                { "CPName", CPName },
                { "StatusID", StatusID > 0 ? StatusID.ToString() : "0" },
                { "WorkshopProfileImageURL", WorkshopProfileImageURL },
                { "WorkshopID", WorkshopID > 0 ? WorkshopID.ToString() : "0" },
                {"Workshop", Workshop},
                { "ICWorkshops", ICWorkshops},
                { "isPriceEstimate",isPriceEstimate == true?"1":"0"},
                {"IsCompanyExist",IsCompanyExist == true? "1":"0" },
                {"CompanyTypeID",CompanyTypeID != null ? CompanyTypeID.ToString(): "0" },
                {"SuggestionOfferTime",SuggestionOfferTime != null ? SuggestionOfferTime.ToString():""},
                {"joclaimsSetting" , joclaimssetting },
                {"CurrencyEn",CurrencyEn != null? CurrencyEn.ToString():"" },
                {"CurrencyAr",CurrencyAr != null? CurrencyAr.ToString():"" },
                {"CountryID", CountryID != null? CountryID.ToString():""}
                //{"SupplierCurrencyEnglish",SupplierCurrencyEnglish != null? SupplierCurrencyEnglish.ToString():"" },
                //{"SupplierCurrencyArabic",SupplierCurrencyArabic != null? SupplierCurrencyArabic.ToString():"" },
                //{"WorkShopCurrencyEnglish",WorkShopCurrencyEnglish != null? WorkShopCurrencyEnglish.ToString():"" },
                //{"WorkShopCurrencyArabic",WorkShopCurrencyArabic != null? WorkShopCurrencyArabic.ToString():"" }

            };
            return new AuthenticationProperties(data);
        }
    }
}