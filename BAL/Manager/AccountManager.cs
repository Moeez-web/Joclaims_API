using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Manager
{
    public class AccountManager : IAccountManager
    {
        #region Register
        public string Register(User userObj)
        {
            try
            {
                //var enc = EncryptionDecryption.EncryptString(userObj.Password);
                //var dec = EncryptionDecryption.DecryptString(enc);
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@Email", Value = userObj.Email},
                    new SqlParameter {ParameterName = "@PhoneNumber", Value = userObj.PhoneNumber},
                    new SqlParameter {ParameterName = "@Password", Value = EncryptionDecryption.EncryptString(userObj.Password)},
                    new SqlParameter {ParameterName = "@UserName", Value = userObj.UserName},
                    new SqlParameter {ParameterName = "@CityID", Value = userObj.CityID},
                    new SqlParameter {ParameterName = "@AddressLine1", Value = userObj.AddressLine1},
                    new SqlParameter {ParameterName = "@CompanyName", Value = userObj.CompanyName},
                    new SqlParameter {ParameterName = "@RoleID", Value = userObj.RoleID},
                    new SqlParameter {ParameterName = "@Id", Value = userObj.Id },
                    new SqlParameter {ParameterName = "@CommercialRegisterImg", Value = userObj.CommercialRegisterImg },
                    new SqlParameter {ParameterName = "@ProfessionLicenseImg", Value = userObj.ProfessionLicenseImg },
                    new SqlParameter {ParameterName = "@WorkshopAreaName", Value = userObj.WorkshopAreaName },
                    new SqlParameter {ParameterName = "@WorkshopGoogleMapLink", Value = userObj.WorkshopGoogleMapLink },
                    new SqlParameter {ParameterName = "@CountryID", Value = userObj.CountryID },
                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[registerUser]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? DataValidation.success :result ==0? DataValidation.emailExist: DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return ex.ToString();
            }
            //return "0";
        }
        #endregion

        #region SaveEmailConfirmationToken
        public string SaveEmailConfirmationToken(string UserId, string token)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@UserId", Value = UserId},
                    new SqlParameter {ParameterName = "@token", Value = token},
                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[SaveEmailConfirmationToken]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? DataValidation.success : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return "0";
        }
        #endregion

        #region SignIn
        public SignInResponse SignIn(User userObj)
        {
            DataSet dt = new DataSet();

            try
            {
                var signInResponse = new SignInResponse();
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@Email", Value = userObj.Email == null ? null : userObj.Email},
                    new SqlParameter {ParameterName = "@MobileNotificationToken", Value = userObj.MobileNotificationToken == null ? null : userObj.MobileNotificationToken},
                    new SqlParameter {ParameterName = "@MobileTypeID", Value = userObj.MobileTypeID == null ? null : userObj.MobileTypeID},
                    new SqlParameter {ParameterName = "@PhoneNumber", Value = userObj.PhoneNumber == null ? null  : userObj.PhoneNumber},
                    new SqlParameter {ParameterName = "@AppVersion", Value = userObj.AppVersion == null ? null  : userObj.AppVersion},
                    new SqlParameter {ParameterName = "@AppID", Value = userObj.AppID == "" ? null  : userObj.AppID}
                };
                using (dt = ADOManager.Instance.DataSet("[signIn]", CommandType.StoredProcedure, sParameter))
                {
                    signInResponse.UserObj = dt.Tables[0].AsEnumerable().Select(employee => new User
                    {
                        UserID = employee.Field<int>("UserID"),
                        Id = employee.Field<string>("Id"),
                        UserName = employee.Field<string>("UserName"),
                        Email = employee.Field<string>("Email"),
                        EmailConfirmed = employee.Field<bool>("EmailConfirmed"),
                        Password = employee.Field<string>("PasswordHash"),
                        RoleID = employee.Field<byte>("RoleID"),
                        PhoneNumber = employee.Field<string>("PhoneNumber"),
                        PhoneNumberConfirmed = employee.Field<bool>("PhoneNumberConfirmed"),
                        isPriceEstimate = employee.Field<bool>("isPriceEstimate"),
                        IsCompanyExist = employee.Field<bool>("IsCompanyExist"),
                        CompanyTypeID = employee.Field<int?>("CompanyTypeID"),
                        CurrencyEn = employee.Field<string>("CurrencyEnglish"),
                        CurrencyAr = employee.Field<string>("CurrencyArabic"),
                        CountryID = employee.Field<int?>("CountryID")
                    }).FirstOrDefault();

                    signInResponse.joclaimsSettings = dt.Tables[1].AsEnumerable().Select(js => new JoclaimsSetting
                    {

                        JoclaimsSettingID = js.Field<int>("JoclaimsSettingID"),
                        ServiceID = js.Field<int>("ServiceID"),
                        ServiceName = js.Field<string>("ServiceName"),
                        SettingTypeID = js.Field<int>("SettingTypeID"),
                        SettingTypeName = js.Field<string>("SettingTypeName")

                    }).ToList();

                    if (signInResponse.UserObj != null)
                    {
                        if (signInResponse.UserObj.RoleID == 1 || signInResponse.UserObj.RoleID == 2 || signInResponse.UserObj.RoleID == 3)
                        {
                            signInResponse.ShubeddakUser = dt.Tables[2].AsEnumerable().Select(sbu => new ShubeddakUser
                            {
                                ShubeddakUserID = sbu.Field<int>("ShubeddakUserID"),
                                UserID = sbu.Field<int>("UserID"),
                                CreatedOn = sbu.Field<DateTime>("CreatedOn"),
                                FirstName = sbu.Field<string>("FirstName"),
                                IsDeleted = sbu.Field<bool?>("IsDeleted"),
                                ProfileImageURL = sbu.Field<string>("ProfileImageURL"),
                                LastName = sbu.Field<string>("LastName"),
                                CreatedBy = sbu.Field<int?>("CreatedBy"),
                                ModifiedBy = sbu.Field<int?>("ModifiedBy"),
                                ModifiedOn = sbu.Field<DateTime?>("ModifiedOn")

                            }).FirstOrDefault();
                            signInResponse.UserPermissions = dt.Tables[3].AsEnumerable().Select(pm => new Permission
                            {
                                PermissionID = pm.Field<Int16>("PermissionID"),
                                RoleID = pm.Field<int?>("RoleID"),
                                PermissionName = pm.Field<string>("PermissionName"),
                                PermissionArabicName = pm.Field<string>("PermissionArabicName"),
                                UserPermissionID = pm.Field<int?>("UserPermissionID"),
                                UserID = pm.Field<int?>("UserID"),
                                IsChecked = pm.Field<bool?>("IsChecked"),
                                MinPrice = pm.Field<int?>("MinPrice"),
                                MaxPrice = pm.Field<int?>("MaxPrice"),
                            }).ToList();
                        }
                        else if (signInResponse.UserObj.RoleID == 4 || signInResponse.UserObj.RoleID == 8 || signInResponse.UserObj.RoleID == 9 || signInResponse.UserObj.RoleID == 10 || signInResponse.UserObj.RoleID == 12 || signInResponse.UserObj.RoleID == 13)
                        {
                            signInResponse.Company = dt.Tables[2].AsEnumerable().Select(cmp => new Company
                            {
                                CompanyID = cmp.Field<int>("CompanyID"),
                                EmployeeID = cmp.Field<int>("EmployeeID"),
                                ProfileImageURL = cmp.Field<string>("ImgURL"),
                                UserID = cmp.Field<int>("UserID"),
                                CreatedOn = cmp.Field<DateTime>("CreatedOn"),
                                CPFirstName = cmp.Field<string>("CPFirstName"),
                                IsDeleted = cmp.Field<bool>("IsDeleted"),
                                LogoURL = cmp.Field<string>("LogoURL"),
                                CPLastName = cmp.Field<string>("CPLastName"),
                                AddressLine1 = cmp.Field<string>("AddressLine1"),
                                AddressLine2 = cmp.Field<string>("AddressLine2"),
                                CityID = cmp.Field<Int16?>("CityID"),
                                CountryID = cmp.Field<Int16?>("CountryID"),
                                CPEmail = cmp.Field<string>("CPEmail"),
                                CPPhone = cmp.Field<string>("CPPhone"),
                                CPPositionID = cmp.Field<Int16?>("CPPositionID"),
                                Name = cmp.Field<string>("Name"),
                                StatusID = cmp.Field<Int16?>("StatusID"),
                                ICWorkshopID = cmp.Field<int?>("WorkshopID"),
                                CompanyCode = cmp.Field<string>("CompanyCode"),
                                SuggestionOfferTime = cmp.Field<int?>("SuggestionOfferTime"),
                                //CurrencyEnglish = cmp.Field<string>("CurrencyEnglish"),
                                //CurrencyArabic = cmp.Field<string>("CurrencyArabic")
                            }).FirstOrDefault();

                            signInResponse.UserPermissions = dt.Tables[3].AsEnumerable().Select(pm => new Permission
                            {
                                PermissionID = pm.Field<Int16>("PermissionID"),
                                RoleID = pm.Field<int?>("RoleID"),
                                PermissionName = pm.Field<string>("PermissionName"),
                                PermissionArabicName = pm.Field<string>("PermissionArabicName"),
                                UserPermissionID = pm.Field<int?>("UserPermissionID"),
                                UserID = pm.Field<int?>("UserID"),
                                IsChecked = pm.Field<bool?>("IsChecked"),
                                MinPrice = pm.Field<int?>("MinPrice"),
                                MaxPrice = pm.Field<int?>("MaxPrice"),
                            }).ToList();
                            signInResponse.featurePermissions = dt.Tables[4].AsEnumerable().Select(fp => new FeaturePermission
                            {
                                FeaturePermissionsID = fp.Field<int?>("FeaturePermissionsID"),
                                FeatureID = fp.Field<int?>("FeatureID"),
                                IsApproved = fp.Field<bool>("IsApproved"),
                                CompanyID = fp.Field<int?>("ObjectID"),
                                IsDeleted = fp.Field<bool?>("IsDeleted"),
                                AICustomerRequestApproval = fp.Field<bool?>("AICustomerRequestApproval")
                            }).ToList();
                        }
                        else if (signInResponse.UserObj.RoleID == 5 || signInResponse.UserObj.RoleID == 7)
                        {
                            signInResponse.Employee = dt.Tables[2].AsEnumerable().Select(emp => new Employee
                            {
                                EmployeeID = emp.Field<int>("EmployeeID"),
                                UserID = emp.Field<int>("UserID"),
                                CreatedOn = emp.Field<DateTime>("CreatedOn"),
                                FirstName = emp.Field<string>("FirstName"),
                                IsDeleted = emp.Field<bool>("IsDeleted"),
                                ProfileImageURL = emp.Field<string>("ProfileImageURL"),
                                LastName = emp.Field<string>("LastName"),
                                CompanyID = emp.Field<int>("CompanyID"),
                                Email = emp.Field<string>("Email"),
                                Phone = emp.Field<string>("Phone"),
                                PositionID = emp.Field<Int16>("PositionID"),
                                CreatedBy = emp.Field<int>("CreatedBy"),

                            }).FirstOrDefault();
                        }
                        else if (signInResponse.UserObj.RoleID == 6)
                        {
                            signInResponse.Supplier = dt.Tables[2].AsEnumerable().Select(sup => new Supplier
                            {
                                SupplierID = sup.Field<int>("SupplierID"),
                                UserID = sup.Field<int>("UserID"),
                                CreatedOn = sup.Field<DateTime>("CreatedOn"),
                                IsDeleted = sup.Field<bool>("IsDeleted"),
                                CreatedBy = sup.Field<int?>("CreatedBy"),
                                AddressLine1 = sup.Field<string>("AddressLine1"),
                                AddressLine2 = sup.Field<string>("AddressLine2"),
                                CPLastName = sup.Field<string>("CPLastName"),
                                CityID = sup.Field<Int16?>("CityID"),
                                CountryID = sup.Field<Int16?>("CountryID"),
                                CPEmail = sup.Field<string>("CPEmail"),
                                CPPhone = sup.Field<string>("CPPhone"),
                                IsSellNew = sup.Field<bool?>("IsSellNew"),
                                IsSellUsed = sup.Field<bool?>("IsSellUsed"),
                                CPFirstName = sup.Field<string>("CPFirstName"),
                                StatusID = sup.Field<Int16?>("StatusID"),
                                CPPositionID = sup.Field<Int16?>("CPPositionID"),
                                SupplierName = sup.Field<string>("SupplierName"),
                                LogoURL = sup.Field<string>("LogoURL"),
                                //CurrencyEnglish = sup.Field<string>("CurrencyEnglish"),
                                //CurrencyArabic = sup.Field<string>("CurrencyArabic"),
                                WorkshopID = sup.Field<int?>("WorkshopID")

                            }).FirstOrDefault();
                        }
                        else if (signInResponse.UserObj.RoleID == 11)
                        {
                            signInResponse.Workshop = dt.Tables[2].AsEnumerable().Select(sup => new Workshop
                            {
                                WorkshopID = sup.Field<int>("WorkshopID"),
                                UserID = sup.Field<int>("UserID"),
                                CreatedOn = sup.Field<DateTime>("CreatedOn"),
                                IsDeleted = sup.Field<bool>("IsDeleted"),
                                WorkshopAreaName = sup.Field<string>("WorkshopAreaName"),
                                WorkshopGoogleMapLink = sup.Field<string>("WorkshopGoogleMapLink"),
                                WorkshopName = sup.Field<string>("WorkshopName"),
                                ProfileImageURL = sup.Field<string>("ProfileImageURL"),
                                WorkshopPhone = sup.Field<string>("WorkshopPhone"),
                                StatusID = sup.Field<Int16?>("StatusID"),
                                WorkshopCityID = sup.Field<int>("WorkshopCityID"),
                                Email = sup.Field<string>("Email"),
                                SupplierID = sup.Field<int?>("SupplierID"),
                                //CurrencyEnglish = sup.Field<string>("CurrencyEnglish"),
                                //CurrencyArabic = sup.Field<string>("CurrencyArabic")


                            }).FirstOrDefault();

                            signInResponse.ICWorkshops = dt.Tables[3].AsEnumerable().Select(pm => new ICWorkshop
                            {
                                CompanyID = pm.Field<int>("CompanyID"),
                                WorkshopID = pm.Field<int>("WorkshopID"),
                                CompanyName = pm.Field<string>("CompanyName"),
                                ICWorkshopID = pm.Field<int>("ICWorkshopID"),
                                CompanyCode = pm.Field<string>("CompanyCode"),

                            }).ToList();
                        }


                    }

                    if (signInResponse.UserObj != null && EncryptionDecryption.DecryptString(signInResponse.UserObj.Password) == userObj.Password)
                    {
                        return signInResponse;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context GetJob(): " + ex.Message);
                //return ex.Message;
            }

            return null;
        }
        #endregion

        #region VerifyEmail
        public string VerifyEmail(User user)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@EmailVerifyToken" , Value = user.EmailVerifyToken} ,
                        new SqlParameter { ParameterName = "@Id" , Value = user.Id} ,
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[verifyEmail]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Email verified" : "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region FindForgotEmail
        public User FindForgotEmail(User UserObj)
        {
            try
            {
                var user = new User();
                DataSet dt = new DataSet();
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@Email", Value = UserObj.Email},
                };
                using (dt = ADOManager.Instance.DataSet("[findForgotEmail]", CommandType.StoredProcedure, sParameter))
                {
                    user = dt.Tables[0].AsEnumerable().Select(employee => new User
                    {
                        Id = employee.Field<string>("Id"),
                        Email = employee.Field<string>("Email"),

                    }).FirstOrDefault();
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region SavePasswordResetToken
        public string SavePasswordResetToken(string UserId, string token)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@UserId", Value = UserId},
                    new SqlParameter {ParameterName = "@token", Value = token},
                };
                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[savePasswordResetToken]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? DataValidation.success : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[dbo].[savePasswordResetToken]", CommandType.StoredProcedure, sParameter).ToString();
                return result.Equals("Success") ? DataValidation.success : result;

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return "0";
        }
        #endregion

        #region ResetPassword
        public string ResetPassword(User user)
        {
            try
            {
                var enc = EncryptionDecryption.EncryptString(user.Password);
                var dec = EncryptionDecryption.DecryptString(enc);
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@Id", Value = user.Id},
                    new SqlParameter {ParameterName = "@Password", Value = EncryptionDecryption.EncryptString(user.Password)},
                    new SqlParameter {ParameterName = "@ConfirmPassword", Value = user.ConfirmPassword},
                    new SqlParameter {ParameterName = "@Reset_token", Value = user.Reset_token},
                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[resetUserPassword]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Password has been reset" : "0";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return "0";
        }
        #endregion

        #region LogoutUser
        public string LogoutUser(int UserID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@UserID", Value = UserID},
                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[dbo].[logoutUser]", CommandType.StoredProcedure, sParameter));

                return result > 0 ? "Logout Successfully" : "Error";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            //return "0";
        }
        #endregion

        #region DeleteUser
        public string DeleteAccount(int UserID)
        {
            DataSet dt = new DataSet();

            try
            {
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID}
                };



                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[DeleteAccount]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? DataValidation.userdeleted : DataValidation.dbError; ;



            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            #endregion
        }
    }
}