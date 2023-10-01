using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace BAL.Manager
{
    public class PartShopManager : IPartShopManager
    {
        #region GetPartShopProfile
        public SupplierProfile GetPartShopProfile(int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                var supplierProfile = new SupplierProfile();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getPartShopProfile]", CommandType.StoredProcedure, sParameter))
                {
                    supplierProfile.Supplier = dt.Tables[0].AsEnumerable().Select(cmp => new Supplier
                    {
                        SupplierID = cmp.Field<int>("SupplierID"),
                        SupplierName = cmp.Field<string>("SupplierName"),
                        PhoneNumber = cmp.Field<string>("PhoneNumber"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        CityID = cmp.Field<Int16?>("CityID"),
                        CityName = cmp.Field<string>("CityName"),
                        CityNameArabic = cmp.Field<string>("CityNameArabic"),
                        CountryID = cmp.Field<Int16?>("CountryID"),
                        CountryName = cmp.Field<string>("CountryName"),
                        CountryNameArabic = cmp.Field<string>("CountryNameArabic"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        GoogleMapLink = cmp.Field<string>("GoogleMapLink"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPositionID = cmp.Field<Int16?>("CPPositionID"),
                        CPPositionName = cmp.Field<string>("CPPositionName"),
                        CPPositionArabicName = cmp.Field<string>("CPPositionArabicName"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        StatusName = cmp.Field<string>("StatusName"),
                        ArabicStatusName = cmp.Field<string>("ArabicStatusName"),
                        UserID = cmp.Field<int>("UserID"),
                        Email = cmp.Field<string>("Email"),
                        RoleID = cmp.Field<byte>("RoleID"),
                        RoleName = cmp.Field<string>("RoleName"),
                        UserName = cmp.Field<string>("UserName"),
                        EmailConfirmed = cmp.Field<bool>("EmailConfirmed"),
                        IsSellNew = cmp.Field<bool?>("EmailConfirmed"),
                        IsSellUsed = cmp.Field<bool?>("EmailConfirmed"),
                        RegistrationNumber = cmp.Field<string>("RegistrationNumber"),
                        RejectNote = cmp.Field<string>("RejectNote"),
                        ProfessionLicenseImg = cmp.Field<string>("ProfessionLicenseImg"),
                        CommercialRegisterImg = cmp.Field<string>("CommercialRegisterImg"),
                        OffersCount = cmp.Field<int>("OffersCount"),
                        PaymentTypeID = cmp.Field<Int16?>("PaymentTypeID"),
                        PaymentTypeName = cmp.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = cmp.Field<string>("PaymentTypeArabicName"),
                        IBAN = cmp.Field<string>("IBAN")
                    }).FirstOrDefault();

                    supplierProfile.Countries = dt.Tables[1].AsEnumerable().Select(country => new Country
                    {
                        LanguageCountryID = country.Field<Int16>("LanguageCountryID"),
                        CountryName = country.Field<string>("CountryName"),
                        CountryNameArabic = country.Field<string>("CountryNameArabic"),
                        LanguageID = country.Field<byte>("LanguageID"),
                        CountryID = country.Field<Int16>("CountryID")

                    }).ToList();

                    supplierProfile.Cities = dt.Tables[2].AsEnumerable().Select(city => new City
                    {
                        LanguageCityID = city.Field<int>("LanguageCityID"),
                        CityCode = city.Field<string>("CityCode"),
                        CountryID = city.Field<Int16>("CountryID"),
                        LanguageID = city.Field<byte>("LanguageID"),
                        CityID = city.Field<int>("CityID"),
                        Latitude = city.Field<double>("Latitude"),
                        Longitude = city.Field<double>("Longitude"),
                        CityName = city.Field<string>("CityName"),
                        CityNameArabic = city.Field<string>("CityNameArabic"),

                    }).ToList();

                    //supplierProfile.Positions = dt.Tables[3].AsEnumerable().Select(object1 => new ObjectType
                    //{
                    //    ObjectTypeID = object1.Field<Int16>("ObjectTypeID"),
                    //    ObjectName = object1.Field<string>("ObjectName"),
                    //    TypeName = object1.Field<string>("TypeName"),
                    //    ArabicTypeName = object1.Field<string>("ArabicTypeName")

                    //}).ToList();

                    //supplierProfile.Makes = dt.Tables[4].AsEnumerable().Select(make => new Make
                    //{
                    //    //LanguageMakeID = make.Field<Int16>("LanguageMakeID"),
                    //    MakeCode = make.Field<string>("MakeCode"),
                    //    ImgURL = make.Field<string>("ImgURL"),
                    //    //LanguageID = make.Field<Byte>("LanguageID"),
                    //    MakeID = make.Field<int>("MakeID"),
                    //    ModifiedBy = make.Field<int?>("ModifiedBy"),
                    //    CreatedOn = make.Field<DateTime?>("CreatedOn"),
                    //    CreatedBy = make.Field<int?>("CreatedBy"),
                    //    ModifiedOn = make.Field<DateTime?>("ModifiedOn"),
                    //    MakeName = make.Field<string>("EnglishMakeName"),
                    //    ArabicMakeName = make.Field<string>("ArabicMakeName"),


                    //}).ToList();

                    //supplierProfile.Models = dt.Tables[5].AsEnumerable().Select(model => new Model
                    //{
                    //    //LanguageModelID = model.Field<Int32>("LanguageModelID"),
                    //    MakeID = model.Field<int>("MakeID"),
                    //    //YearCode = model.Field<Int16>("YearCode"),
                    //    //LanguageID = model.Field<byte>("LanguageID"),
                    //    ModelID = model.Field<int>("ModelID"),
                    //    ModelCode = model.Field<string>("EnglishModelName"),
                    //    ArabicModelName = model.Field<string>("ArabicModelName"),
                    //    GroupName = model.Field<string>("GroupName")


                    //}).ToList();

                    //supplierProfile.Years = dt.Tables[6].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID")

                    //}).ToList();

                    supplierProfile.PartsInfo = dt.Tables[3].AsEnumerable().Select(pti => new PartInfo
                    {
                        PartInfoID = pti.Field<int>("PartInfoID"),
                        ProductionYearFrom = pti.Field<Int16>("ProductionYearFrom"),
                        ProductionYearTo = pti.Field<Int16>("ProductionYearTo"),
                        SupplierID = pti.Field<int>("SupplierID"),
                        MakeName = pti.Field<string>("EnglishMakeName"),
                        ArabicMakeName = pti.Field<string>("ArabicMakeName"),
                        //ConditionTypeID = pti.Field<Int16>("ConditionTypeID"),
                        ModelID = pti.Field<int>("ModelID"),
                        MakeID = pti.Field<int>("MakeID"),
                        ModelName = pti.Field<string>("EnglishModelName"),
                        GroupName = pti.Field<string>("GroupName"),
                        MaxYearCode = pti.Field<int>("MaxYearCode"),
                        MinYearCode = pti.Field<int>("MinYearCode"),
                        IsConditionNew = pti.Field<bool?>("IsConditionNew"),
                        IsConditionUsed = pti.Field<bool?>("IsConditionUsed"),
                        IsConditionOriginal = pti.Field<bool?>("IsConditionOriginal"),
                        IsConditionAfterMarket = pti.Field<bool?>("IsConditionAfterMarket"),
                        ArabicModelName = pti.Field<string>("ArabicModelName"),
                        IsApproved = pti.Field<Int16>("IsApproved"),

                    }).ToList();

                    //supplierProfile.AutomotiveParts = dt.Tables[4].AsEnumerable().Select(pti => new AutomotivePart
                    //{
                    //    AutomotivePartID = pti.Field<int>("AutomotivePartID"),
                    //    PartName = pti.Field<string>("PartName"),
                    //    //PartNameArabic = pti.Field<string>("PartNameArabic"),

                    //}).ToList();

                    supplierProfile.UniversalPart = dt.Tables[4].AsEnumerable().Select(uvPart => new AutomotivePart
                    {

                        UniversalPartID = uvPart.Field<int>("UniversalPartID"),
                        AutomotivePartID = uvPart.Field<int>("AutomotivePartID"),
                        SupplierID = uvPart.Field<int>("SupplierID"),
                        PartName = uvPart.Field<string>("PartName"),
                        IsConditionNew = uvPart.Field<bool>("IsConditionNew"),
                        IsConditionUsed = uvPart.Field<bool>("IsConditionUsed"),
                        IsConditionOriginal = uvPart.Field<bool>("IsConditionOriginal"),
                        IsConditionAfterMarket = uvPart.Field<bool>("IsConditionAfterMarket"),
                        IsApproved = uvPart.Field<Int16>("IsApproved"),

                    }).ToList();

                    supplierProfile.MakeGroups = dt.Tables[5].AsEnumerable().Select(model => new Model
                    {
                        MakeID = model.Field<int>("MakeID"),
                        GroupName = model.Field<string>("GroupName"),
                        ModelGroupName = model.Field<string>("GroupName"),

                    }).ToList();

                }
                return supplierProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartShopProfile
        public Supplier UpdatePartShopProfile(SupplierProfile profileData)
        {
            DataSet dt = new DataSet();
            try
            {
                //var partsInfo = new List<PartInfo>();
                //var XMLPartsInfo = profileData.PartsInfo != null && profileData.PartsInfo.Count > 0 ? profileData.PartsInfo.ToXML("ArrayOfPartInfo") : null;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = profileData.Supplier.SupplierID},
                        new SqlParameter { ParameterName = "@UserID" , Value = profileData.Supplier.UserID},
                        new SqlParameter { ParameterName = "@AddressLine1" , Value = profileData.Supplier.AddressLine1},
                        new SqlParameter { ParameterName = "@AddressLine2" , Value = profileData.Supplier.AddressLine2},
                        new SqlParameter { ParameterName = "@GoogleMapLink" , Value = profileData.Supplier.GoogleMapLink},
                        new SqlParameter { ParameterName = "@SupplierName" , Value = profileData.Supplier.SupplierName},
                        new SqlParameter { ParameterName = "@CPEmail" , Value = profileData.Supplier.CPEmail},
                        new SqlParameter { ParameterName = "@CPFirstName" , Value = profileData.Supplier.CPFirstName},
                        new SqlParameter { ParameterName = "@CPLastName" , Value = profileData.Supplier.CPLastName},
                        new SqlParameter { ParameterName = "@CPPhone" , Value = profileData.Supplier.CPPhone},
                        new SqlParameter { ParameterName = "@CPPositionID" , Value = profileData.Supplier.CPPositionID},
                        new SqlParameter { ParameterName = "@LogoURL" , Value = profileData.Supplier.LogoURL},
                        new SqlParameter { ParameterName = "@CityID" , Value = profileData.Supplier.CityID},
                        new SqlParameter { ParameterName = "@CountryID" , Value = profileData.Supplier.CountryID},
                        new SqlParameter { ParameterName = "@ProfessionLicenseImg" , Value = profileData.Supplier.ProfessionLicenseImg},
                        new SqlParameter { ParameterName = "@CommercialRegisterImg" , Value = profileData.Supplier.CommercialRegisterImg},
                        new SqlParameter { ParameterName = "@PaymentTypeID" , Value = profileData.Supplier.PaymentTypeID},
                        new SqlParameter { ParameterName = "@IBAN" , Value = profileData.Supplier.IBAN},
                        new SqlParameter { ParameterName = "@PhoneNumber" , Value = profileData.Supplier.PhoneNumber}
                };

                using (dt = ADOManager.Instance.DataSet("[updatePartShopProfile]", CommandType.StoredProcedure, sParameter))
                {
                    //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updatePartShopProfile]", CommandType.StoredProcedure, sParameter));
                    Supplier s;
                    s = dt.Tables[0].AsEnumerable().Select(cmp => new Supplier
                    {
                        SupplierID = cmp.Field<int>("SupplierID"),
                        SupplierName = cmp.Field<string>("SupplierName"),
                        PhoneNumber = cmp.Field<string>("PhoneNumber"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        CityID = cmp.Field<Int16?>("CityID"),
                        CityName = cmp.Field<string>("CityName"),
                        CityNameArabic = cmp.Field<string>("CityNameArabic"),
                        CountryID = cmp.Field<Int16?>("CountryID"),
                        CountryName = cmp.Field<string>("CountryName"),
                        CountryNameArabic = cmp.Field<string>("CountryNameArabic"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        GoogleMapLink = cmp.Field<string>("GoogleMapLink"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPositionID = cmp.Field<Int16?>("CPPositionID"),
                        CPPositionName = cmp.Field<string>("CPPositionName"),
                        CPPositionArabicName = cmp.Field<string>("CPPositionArabicName"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        StatusName = cmp.Field<string>("StatusName"),
                        UserID = cmp.Field<int>("UserID"),
                        Email = cmp.Field<string>("Email"),
                        RoleID = cmp.Field<byte>("RoleID"),
                        RoleName = cmp.Field<string>("RoleName"),
                        UserName = cmp.Field<string>("UserName"),
                        EmailConfirmed = cmp.Field<bool>("EmailConfirmed"),
                        IsSellNew = cmp.Field<bool?>("EmailConfirmed"),
                        IsSellUsed = cmp.Field<bool?>("EmailConfirmed"),
                        RegistrationNumber = cmp.Field<string>("RegistrationNumber"),
                        CommercialRegisterImg = cmp.Field<string>("CommercialRegisterImg"),
                        ProfessionLicenseImg = cmp.Field<string>("ProfessionLicenseImg"),
                        IBAN = cmp.Field<string>("IBAN")

                    }).FirstOrDefault();

                    return s;
                }
                    /*return result > 0 ? "Profile Updated Successfully" : 0.ToString()*/
                    ;

                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveParts
        public SavePart SaveParts(SavePart partInfoData)
        {
            DataSet dt = new DataSet();
            try
            {
                //List<PartInfo> PartsInfo = new List<PartInfo> ();
                //var partsInfo = new List<PartInfo>();
                var XMLPartsInfo = partInfoData.PartsInfo != null && partInfoData.PartsInfo.Count > 0 ? partInfoData.PartsInfo.ToXML("ArrayOfPartInfo") : null;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = partInfoData.SupplierID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = partInfoData.CreatedBy},
                        new SqlParameter { ParameterName = "@XMLPartsInfo" , Value = XMLPartsInfo},
                };
                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveParts]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Part Saved Successfully" : DataValidation.dbError.ToString();
                using (dt = ADOManager.Instance.DataSet("[saveParts]", CommandType.StoredProcedure, sParameter))
                {
                    partInfoData.PartsInfo = dt.Tables[0].AsEnumerable().Select(pti => new PartInfo
                    {
                        PartInfoID = pti.Field<int>("PartInfoID"),
                        ProductionYearFrom = pti.Field<Int16>("ProductionYearFrom"),
                        ProductionYearTo = pti.Field<Int16>("ProductionYearTo"),
                        SupplierID = pti.Field<int>("SupplierID"),
                        MakeName = pti.Field<string>("EnglishMakeName"),
                        ArabicMakeName = pti.Field<string>("ArabicMakeName"),
                        //ConditionTypeID = pti.Field<Int16>("ConditionTypeID"),
                        ModelID = pti.Field<int>("ModelID"),
                        MakeID = pti.Field<int>("MakeID"),
                        ModelName = pti.Field<string>("EnglishModelName"),
                        MaxYearCode = pti.Field<int>("MaxYearCode"),
                        MinYearCode = pti.Field<int>("MinYearCode"),
                        IsConditionNew = pti.Field<bool?>("IsConditionNew"),
                        IsConditionUsed = pti.Field<bool?>("IsConditionUsed"),
                        IsConditionOriginal = pti.Field<bool?>("IsConditionOriginal"),
                        IsConditionAfterMarket = pti.Field<bool?>("IsConditionAfterMarket"),
                        ArabicModelName = pti.Field<string>("ArabicModelName"),
                        IsApproved = pti.Field<Int16>("IsApproved"),


                    }).ToList();

                    partInfoData.ExistsPartsInfo = dt.Tables[1].AsEnumerable().Select(pti => new PartInfo
                    {
                        MakeName = pti.Field<string>("EnglishMakeName"),
                        ModelName = pti.Field<string>("EnglishModelName"),

                    }).ToList();
                    return partInfoData;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartInfo
        public string UpdatePartInfo(UpdatePart partInfoData)
        {
            DataSet dt = new DataSet();
            try
            {
                //var partsInfo = new PartInfo();
                
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = partInfoData.SupplierID},
                        new SqlParameter { ParameterName = "@PartInfoID" , Value = partInfoData.PartInfo.PartInfoID },
                        new SqlParameter { ParameterName = "@ConditionTypeID" , Value = partInfoData.PartInfo.ConditionTypeID },
                        new SqlParameter { ParameterName = "@IsConditionNew" , Value = partInfoData.PartInfo.IsConditionNew },
                        new SqlParameter { ParameterName = "@IsConditionUsed" , Value = partInfoData.PartInfo.IsConditionUsed },
                        new SqlParameter { ParameterName = "@MakeID" , Value = partInfoData.PartInfo.MakeID },
                        new SqlParameter { ParameterName = "@ModelID" , Value = partInfoData.PartInfo.ModelID },
                        new SqlParameter { ParameterName = "@ProductionYearFrom" , Value = partInfoData.PartInfo.ProductionYearFrom },
                        new SqlParameter { ParameterName = "@ProductionYearTo" , Value = partInfoData.PartInfo.ProductionYearTo },
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = partInfoData.PartInfo.ModifiedBy },

                };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updatePart]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Part Updated Successfully" : DataValidation.dbError.ToString();

                var result = ADOManager.Instance.ExecuteScalar("[updatePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DeletePart
        public string DeletePart(int PartInfoID, int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                //var partsInfo = new PartInfo();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID },
                        new SqlParameter { ParameterName = "@PartInfoID" , Value = PartInfoID },
                        
                };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[deletePart]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Part Deleted Successfully" : DataValidation.dbError.ToString();

                var result = ADOManager.Instance.ExecuteScalar("[deletePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        //#region SavePartShopProfile
        //public List<PartInfo> SavePartShopProfile(SupplierProfile profileData)
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        //var partsInfo = new List<PartInfo>();
        //        //var XMLPartsInfo = profileData.PartsInfo != null && profileData.PartsInfo.Count > 0 ? profileData.PartsInfo.ToXML("ArrayOfPartInfo") : null;
        //        var sParameter = new List<SqlParameter>
        //        {
        //                new SqlParameter { ParameterName = "@SupplierID" , Value = profileData.Supplier.SupplierID},
        //                new SqlParameter { ParameterName = "@UserID" , Value = profileData.Supplier.UserID},
        //                new SqlParameter { ParameterName = "@AddressLine1" , Value = profileData.Supplier.AddressLine1},
        //                new SqlParameter { ParameterName = "@AddressLine2" , Value = profileData.Supplier.AddressLine2},
        //                new SqlParameter { ParameterName = "@SupplierName" , Value = profileData.Supplier.SupplierName},
        //                new SqlParameter { ParameterName = "@CPEmail" , Value = profileData.Supplier.CPEmail},
        //                new SqlParameter { ParameterName = "@CPFirstName" , Value = profileData.Supplier.CPFirstName},
        //                new SqlParameter { ParameterName = "@CPLastName" , Value = profileData.Supplier.CPLastName},
        //                new SqlParameter { ParameterName = "@CPPhone" , Value = profileData.Supplier.CPPhone},
        //                new SqlParameter { ParameterName = "@CPPositionID" , Value = profileData.Supplier.CPPositionID},
        //                new SqlParameter { ParameterName = "@LogoURL" , Value = profileData.Supplier.LogoURL},
        //                new SqlParameter { ParameterName = "@CityID" , Value = profileData.Supplier.CityID},
        //                new SqlParameter { ParameterName = "@CountryID" , Value = profileData.Supplier.CountryID},
        //                //new SqlParameter { ParameterName = "@XMLPartsInfo" , Value = XMLPartsInfo},
        //        };
        //        var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[savePartShopProfile]", CommandType.StoredProcedure, sParameter));
        //        return result > 0 ? "Request saved successfully" : 0.ToString();

        //        //using (dt = ADOManager.Instance.DataSet("[savePartShopProfile]", CommandType.StoredProcedure, sParameter))
        //        //{
        //        //    partsInfo = dt.Tables[0].AsEnumerable().Select(pti => new PartInfo
        //        //    {
        //        //        PartInfoID = pti.Field<int>("PartInfoID"),
        //        //        ProductionYearFrom = pti.Field<Int16>("ProductionYearFrom"),
        //        //        ProductionYearTo = pti.Field<Int16>("ProductionYearTo"),
        //        //        SupplierID = pti.Field<int>("SupplierID"),
        //        //        MakeID = pti.Field<int>("MakeID"),
        //        //        ModelID = pti.Field<int>("ModelID"),
        //        //        //ConditionTypeID = pti.Field<Int16>("ConditionTypeID"),
        //        //        MaxYearCode = pti.Field<int>("MaxYearCode"),
        //        //        MinYearCode = pti.Field<int>("MinYearCode"),
        //        //        IsConditionNew = pti.Field<bool?>("IsConditionNew"),
        //        //        IsConditionUsed = pti.Field<bool?>("IsConditionUsed"),
        //        //        MakeName = pti.Field<string>("EnglishMakeName"),
        //        //        ArabicMakeName = pti.Field<string>("ArabicMakeName"),
        //        //        ModelName = pti.Field<string>("EnglishModelName"),
        //        //        ArabicModelName = pti.Field<string>("ArabicModelName"),


        //        //    }).ToList();

        //        //}

        //        //return partsInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //#endregion

        #region GetPartsDemands
        public Companyrequests GetPartsDemands(int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                var partsDemands = new Companyrequests();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getPartsDemands]", CommandType.StoredProcedure, sParameter))
                {
                    partsDemands.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        QuotationID = req.Field<int?>("QuotationID"),
                        DemandID = req.Field<int?>("DemandID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),                        
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        PublishedOn = req.Field<DateTime>("PublishedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("Name"),
                        CPName = req.Field<string>("CPName"),
                        StatusName = req.Field<string>("StatusName"),
                        ImgURL = req.Field<string>("ImgURL"),
                        StatusID = req.Field<Int16>("StatusID"),


                        WorkshopCityName = req.Field<string>("WorkshopCityName"),                        
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")

                    }).ToList();

                    partsDemands.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        //DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        DemandID = rp.Field<int?>("DemandID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved")

                    }).ToList();

                    //partsDemands.Makes = dt.Tables[2].AsEnumerable().Select(make => new Make
                    //{
                    //    //LanguageMakeID = make.Field<Int16>("LanguageMakeID"),
                    //    MakeCode = make.Field<string>("MakeCode"),
                    //    ImgURL = make.Field<string>("ImgURL"),
                    //    //LanguageID = make.Field<Byte>("LanguageID"),
                    //    MakeID = make.Field<int>("MakeID"),
                    //    ModifiedBy = make.Field<int?>("ModifiedBy"),
                    //    CreatedOn = make.Field<DateTime?>("CreatedOn"),
                    //    CreatedBy = make.Field<int?>("CreatedBy"),
                    //    ModifiedOn = make.Field<DateTime?>("ModifiedOn"),
                    //    MakeName = make.Field<string>("EnglishMakeName"),
                    //    ArabicMakeName = make.Field<string>("ArabicMakeName"),


                    //}).ToList();

                    //partsDemands.Models = dt.Tables[3].AsEnumerable().Select(model => new Model
                    //{
                    //    //LanguageModelID = model.Field<Int32>("LanguageModelID"),
                    //    MakeID = model.Field<int>("MakeID"),
                    //    //YearCode = model.Field<Int16>("YearCode"),
                    //    //LanguageID = model.Field<byte>("LanguageID"),
                    //    ModelID = model.Field<int>("ModelID"),
                    //    ModelCode = model.Field<string>("EnglishModelName"),
                    //    ArabicModelName = model.Field<string>("ArabicModelName"),
                    //    GroupName = model.Field<string>("GroupName")


                    //}).ToList();

                    //partsDemands.Years = dt.Tables[4].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    partsDemands.RequestedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        RequestID = img.Field<int>("RequestID"),

                    }).ToList();

                }
                return partsDemands;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SavePartsQuotation
        public string SavePartsQuotation(RequestData request)
        {

            try
            {
                //var length = request.RequestedParts.FindAll(part => part.IsIncluded == true).Count();
                //if (length > 0)
                //{
                    var XMLRequestedParts = request.QuotationParts.ToXML("ArrayOfQuotationParts");
                    var XMLQuotationPartsImages = request.QuotationPartsImages.ToXML("ArrayOfQuotationPartsImages");
                    var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLQuotationParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@XMLQuotationPartsImages" , Value = XMLQuotationPartsImages},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = request.Request.SupplierID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = request.Request.DemandID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = request.Request.CreatedBy},
                        new SqlParameter { ParameterName = "@WillDeliver" , Value = request.Request.WillDeliver == null ? false : request.Request.WillDeliver},
                        new SqlParameter { ParameterName = "@DeliveryCost" , Value = request.Request.DeliveryCost},
                        new SqlParameter { ParameterName = "@Comment" , Value = request.Request.Comment},
                        new SqlParameter { ParameterName = "@IsDiscountAvailable" , Value = request.Request.IsDiscountAvailable},
                        new SqlParameter { ParameterName = "@DiscountValue" , Value = request.Request.DiscountValue},
                        new SqlParameter { ParameterName = "@IsMobileApp" , Value = request.IsMobileApp},
                        new SqlParameter { ParameterName = "@IsPartialSellings" , Value = request.Request.IsPartialSellings},
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[savePartsQuotation]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Request saved successfully" : 0.ToString();

                var result = Convert.ToInt32 (ADOManager.Instance.ExecuteScalar("[savePartsQuotation]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Request saved successfully" : 0.ToString();

                //}
                //return "Please Choose Atleast One Part";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartsQuotation
        public string UpdatePartsQuotation(QuotationData QuotationData)
        {
            try
            {
                var XMLRequestedParts = QuotationData.RequestedParts.ToXML("ArrayOfRequestedParts");
                var XMLQuotationParts = QuotationData.QuotationParts.ToXML("ArrayOfQuotationParts");
                var XMLQuotationPartsImages = QuotationData.QuotationPartsImages.ToXML("ArrayOfRequestedPartsImages");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@XMLQuotationParts" , Value = XMLQuotationParts},
                        new SqlParameter { ParameterName = "@XMLQuotationPartsImages" , Value = XMLQuotationPartsImages},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = QuotationData.Request.SupplierID},
                        new SqlParameter { ParameterName = "@RequestID" , Value = QuotationData.Request.RequestID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = QuotationData.Request.DemandID},
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationData.Quotation.QuotationID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = QuotationData.Quotation.ModifiedBy},
                        new SqlParameter { ParameterName = "@Comment" , Value = QuotationData.Request.Comment},
                        new SqlParameter { ParameterName = "@IsDiscountAvailable" , Value = QuotationData.Request.IsDiscountAvailable},
                        new SqlParameter { ParameterName = "@DiscountValue" , Value = QuotationData.Request.DiscountValue},
                        new SqlParameter { ParameterName = "@IsPartialSellings" , Value = QuotationData.Request.IsPartialSellings},

                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updatePartsQuotation]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Request updated successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[updatePartsQuotation]", CommandType.StoredProcedure, sParameter);
                return result.ToString();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetShopQuotations
        public ShopQuotations GetShopQuotations(int SupplierID, int StatusID, int PageNo, int? MakeID, int? ModelID, int? YearID, DateTime? POStartDate, DateTime? POEndDate, string AccidentNo, int? CompanyID, string SearchQuery)
        {
            DataSet dt = new DataSet();
            try
            {
                var shopQuotations = new ShopQuotations();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@POStartDate" , Value = POStartDate},
                        new SqlParameter { ParameterName = "@POEndDate" , Value = POEndDate},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo == null||AccidentNo == "undefined"?null: AccidentNo},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery}
                    };

                using (dt = ADOManager.Instance.DataSet("[getShopQuotations]", CommandType.StoredProcedure, sParameter))
                {
                    shopQuotations.Quotations = dt.Tables[0].AsEnumerable().Select(req => new Quotation
                    {
                        QuotationID = req.Field<int>("QuotationID"),
                        RequestID = req.Field<int>("RequestID"),
                        RequestNumber = req.Field<int>("RequestNumber"),
                        DemandID = req.Field<int>("DemandID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        WillDeliver = req.Field<bool?>("WillDeliver"),
                        DeliveryCost = req.Field<decimal?>("DeliveryCost"),
                        ImgURL = req.Field<string>("ImgURL"),
                        CountDownTime = req.Field<int?>("CountDownTime"),
                        CountDownDate = req.Field<DateTime?>("CountDownDate"),
                        JoReviewNote = req.Field<string>("JoReviewNote"),
                        JoReviewStatusID = req.Field<Int16?>("JoReviewStatusID"),
                        IsNotAvailable = req.Field<bool?>("IsNotAvailable"),
                        PublishedOn = req.Field<DateTime>("PublishedOn"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        PONote = req.Field<string>("PONote"),
                        CompanyName = req.Field<string>("CompanyName"),
                        POTotalAmount = req.Field<Double?>("POTotalAmount"),
                        OrderOn = req.Field<DateTime?>("OrderOn"),
                        IsPartialSellings = req.Field<bool?>("IsPartialSellings"),
                        TotalAmount = req.Field<double?>("TotalAmount"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")


                    }).ToList();

                    shopQuotations.QuotationParts = dt.Tables[1].AsEnumerable().Select(rp => new QuotationPart
                    {
                        QuotationPartID = rp.Field<int>("QuotationPartID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        QuotationID = rp.Field<int>("QuotationID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        isSpliced = rp.Field<int>("isSpliced"),

                    }).ToList();

                    shopQuotations.QuotationPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int?>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int?>("ObjectID"),
                        QuotationID = img.Field<int?>("QuotationID"),

                    }).ToList();

                    //shopQuotations.ObjectStatuses = dt.Tables[3].AsEnumerable().Select(img => new ObjectStatus
                    //{
                    //    ObjectStatusID = img.Field<Int16>("ObjectStatusID"),
                    //    ObjectName = img.Field<string>("ObjectName"),
                    //    StatusName = img.Field<string>("StatusName"),

                    //}).ToList();

                    shopQuotations.TabInfoData = dt.Tables[3].AsEnumerable().Select(tbi => new TabInfo
                    {
                        InprogressRequests = tbi.Field<int>("InprogressRequests"),
                        ReferredRequests = tbi.Field<int>("ReferredRequests"),
                        PendingApprovalRequests = tbi.Field<int>("PendingApprovalRequests"),
                        OrderPlacedRequests = tbi.Field<int>("OrderPlacedRequests"),
                        DeliveredRequests = tbi.Field<int>("DeliveredRequests"),
                        PaidRequests = tbi.Field<int>("PaidRequests"),
                        ClosedRequests = tbi.Field<int>("ClosedRequests"),
                        RejectedQuotation = tbi.Field<int?>("RejectedQuotation")

                    }).FirstOrDefault();

                    //shopQuotations.Makes = dt.Tables[4].AsEnumerable().Select(make => new Make
                    //{
                    //    //LanguageMakeID = make.Field<Int16>("LanguageMakeID"),
                    //    MakeName = make.Field<string>("EnglishMakeName"),
                    //    ArabicMakeName = make.Field<string>("ArabicMakeName"),
                    //    ImgURL = make.Field<string>("ImgURL"),
                    //    //LanguageID = make.Field<Byte>("LanguageID"),
                    //    MakeID = make.Field<int>("MakeID"),
                    //    ModifiedBy = make.Field<int?>("ModifiedBy"),
                    //    CreatedOn = make.Field<DateTime?>("CreatedOn"),
                    //    CreatedBy = make.Field<int?>("CreatedBy"),
                    //    ModifiedOn = make.Field<DateTime?>("ModifiedOn"),


                    //}).ToList();

                    //shopQuotations.Models = dt.Tables[5].AsEnumerable().Select(model => new Model
                    //{
                    //    //LanguageModelID = model.Field<Int32>("LanguageModelID"),
                    //    ModelCode = model.Field<string>("EnglishModelName"),
                    //    MakeID = model.Field<int>("MakeID"),
                    //    //YearCode = model.Field<Int16>("YearCode"),
                    //    //LanguageID = model.Field<byte>("LanguageID"),
                    //    ModelID = model.Field<int>("ModelID"),
                    //    GroupName = model.Field<string>("GroupName")

                    //}).ToList();

                    //shopQuotations.Years = dt.Tables[6].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();
                    shopQuotations.Companies = dt.Tables[4].AsEnumerable().Select(com => new Company
                    {
                        CompanyID = com.Field<int>("CompanyID"),
                        CompanyName = com.Field<string>("CompanyName"),
                        TotalQuotations = com.Field<int>("TotalQuotations")
                    }).ToList();
                }

                return shopQuotations;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetSingleQuotation
        public QuotationData GetSingleQuotation(int QuotationID)
        {
            DataSet dt = new DataSet();
            try
            {
                var quotationData = new QuotationData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getSingleQuotation]", CommandType.StoredProcedure, sParameter))
                {
                    quotationData.Quotation = dt.Tables[0].AsEnumerable().Select(req => new Quotation
                    {
                        QuotationID = req.Field<int>("QuotationID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        SupplierID = req.Field<int>("SupplierID"),
                        VIN = req.Field<string>("VIN"),
                        YearCode = req.Field<int>("YearCode"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int>("DemandID"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        WillDeliver = req.Field<bool>("WillDeliver"),
                        DeliveryCost = req.Field<decimal?>("DeliveryCost")


                    }).FirstOrDefault();

                    quotationData.QuotationParts = dt.Tables[1].AsEnumerable().Select(rp => new QuotationPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        QuotationID = rp.Field<int>("QuotationID"),
                        QuotationPartID = rp.Field<int>("QuotationPartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        ManufacturerID = rp.Field<Int16>("ManufacturerID"),
                        Price = rp.Field<double>("Price"),
                        Quantity = rp.Field<int>("Quantity"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ManufacturerName = rp.Field<string>("ManufacturerName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID")

                    }).ToList();

                    quotationData.QuotationPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                }

                return quotationData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetQuotationRequestData
        public QuotationData GetQuotationRequestData(int RequestID, int? QuotationID, int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                var quotationData = new QuotationData();

                List<PartsBargain> partsBargains = new List<PartsBargain>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getQuotationRequestData]", CommandType.StoredProcedure, sParameter))
                {
                    quotationData.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        RequestNumber = req.Field<int>("RequestNumber"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        QuotationID = req.Field<int?>("QuotationID"),
                        DeliveryCost = req.Field<decimal?>("DeliveryCost"),
                        WillDeliver = req.Field<bool?>("WillDeliver"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        VIN = req.Field<string>("VIN"),
                        EngineTypeName = req.Field<string>("EngineTypeName"),
                        EngineTypeArabicName = req.Field<string>("EngineTypeArabicName"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        BodyTypeArabicName = req.Field<string>("BodyTypeArabicName"),
                        BodyTypeIcon = req.Field<string>("BodyTypeIcon"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        PublishedOn = req.Field<DateTime>("PublishedOn"),
                        IcCancelOrderNote = req.Field<string>("IcCancelOrderNote"),
                        JoCancelOrderNote = req.Field<string>("JoCancelOrderNote"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        Comment = req.Field<string>("Comment"),
                        IsDiscountAvailable = req.Field<bool?>("IsDiscountAvailable"),
                        DiscountValue = req.Field<Double?>("DiscountValue"),
                        IsNotAvailable = req.Field<bool?>("IsNotAvailable"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ICName = req.Field<string>("ICName"),
                        PlateNo = req.Field<string>("PlateNo"),
                        TotalOffersCount = req.Field<int?>("TotalOffersCount"),
                        BestOfferPrice = req.Field<double?>("BestOfferPrice"),
                        BestMinOfferPrice = req.Field<double?>("BestMinOfferPrice"),
                        BestOfferQuotationID = req.Field<int?>("BestOfferQuotationID"),
                        BestOfferSupplierID = req.Field<int?>("BestOfferSupplierID"),
                        RejectedOffersCount = req.Field<int?>("RejectedOffersCount"),
                        OriginalPrice = req.Field<double?>("OriginalPrice"),
                        BestOfferMatchingSupplierID = req.Field<int?>("BestOfferMatchingSupplierID"),
                        BestMinOfferMatchingPrice = req.Field<double?>("BestMinOfferMatchingPrice"),
                        BestOfferMatchingQuotationID = req.Field<int?>("BestOfferMatchingQuotationID"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsPartialSellings = req.Field<bool?>("IsPartialSellings"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")
                    }).FirstOrDefault();

                    quotationData.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        //DesiredManufacturerRegionID = rp.Field<Int16>("DesiredManufacturerRegionID"),
                        //DesiredManufacturerID = rp.Field<Int16>("DesiredManufacturerID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        DemandID = rp.Field<int?>("DemandID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        //DesiredManufacturerName = rp.Field<string>("ManufacturerName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsIncluded = rp.Field<bool?>("IsIncluded"),
                        QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        IsPartAvailable = rp.Field<int?>("IsPartAvailable"),

                    }).ToList();

                    quotationData.RequestedPartsPrice = dt.Tables[2].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        Price = rp.Field<double?>("Price"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                       
                    }).ToList();

                    quotationData.RequestedPartsImages = dt.Tables[3].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),

                    }).ToList();

                    quotationData.Branches = dt.Tables[4].AsEnumerable().Select(req => new PartBranch
                    {
                        BranchID = req.Field<int>("BranchID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        BranchCityID = req.Field<int>("BranchCityID"),
                        BranchName = req.Field<string>("BranchName"),
                        BranchGoogleMapLink = req.Field<string>("BranchGoogleMapLink"),
                        BranchPhone = req.Field<string>("BranchPhone"),
                        BranchAreaName = req.Field<string>("BranchAreaName"),

                    }).ToList();

                    quotationData.AccidentNotes = dt.Tables[5].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),

                    }).ToList();

                    quotationData.AccidentImages = dt.Tables[6].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        Description = img.Field<string>("Description"),
                        IsDocument = img.Field<bool?>("IsDocument"),

                    }).ToList();

                    if (QuotationID != null)
                    {
                        quotationData.Quotation = dt.Tables[7].AsEnumerable().Select(req => new Quotation
                        {
                            QuotationID = req.Field<int>("QuotationID"),
                            SupplierID = req.Field<int>("SupplierID"),
                            StatusID = req.Field<Int16?>("StatusID"),
                            StatusName = req.Field<string>("StatusName"),
                            SupplierName = req.Field<string>("SupplierName"),
                            ArabicStatusName = req.Field<string>("ArabicStatusName"),
                            CreatedOn = req.Field<DateTime>("CreatedOn"),
                            CreatedSince = req.Field<string>("CreatedSince"),
                            CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                            DemandID = req.Field<int>("DemandID"),
                            CountDownTime = req.Field<int?>("CountDownTime"),
                            CountDownDate = req.Field<DateTime?>("CountDownDate"),
                            JoReviewNote = req.Field<string>("JoReviewNote"),
                            JoReviewStatusID = req.Field<Int16?>("JoReviewStatusID"),
                            IsNotAvailable = req.Field<bool?>("IsNotAvailable"),
                            Comment = req.Field<string>("Comment"),
                            DiscountValue = req.Field<Double?>("DiscountValue"),
                            IsDiscountAvailable = req.Field<bool?>("IsDiscountAvailable"),    
                            AddressLine1 = req.Field<string>("AddressLine1"),
                            AddressLine2 = req.Field<string>("AddressLine2"),
                            CPPhone = req.Field<string>("CPPhone"),
                            CPEmail = req.Field<string>("CPEmail"),
                            Rating = req.Field<byte>("Rating"),
                            OriginalPrice = req.Field<double?>("OriginalPrice"),
                            PaymentTypeID = req.Field<Int16?>("PaymentTypeID"),
                            PaymentTypeName = req.Field<string>("PaymentTypeName"),
                            PaymentTypeArabicName = req.Field<string>("PaymentTypeArabicName"),
                            SuggestedPrice = req.Field<double?>("SuggestedPrice"),
                            IsSuggestionAccepted = req.Field<bool?>("IsSuggestionAccepted"),
                            FillingRate = req.Field<decimal?>("FillingRate"),
                            BestOfferPrice = req.Field<double?>("BestOfferPrice"),
                            MatchingFillingRate = req.Field<decimal?>("MatchingFillingRate"),
                            LowestOfferMatchingPrice = req.Field<double?>("LowestOfferMatchingPrice"),
                            OldLowestOfferMatchingPrice = req.Field<double?>("OldLowestOfferMatchingPrice"),
                            MatchingOfferSortNo = req.Field<int>("MatchingOfferSortNo"),
                            OfferSortNo = req.Field<int>("OfferSortNo"),
                            SupplierType = req.Field<Int16?>("SupplierType"),
                            PreviousBestOfferPrice = req.Field<double?>("PreviousBestOfferPrice"),
                            PreviousOriginalPrice = req.Field<double?>("PreviousOriginalPrice"),
                            PreviousLowestOfferMatchingPrice = req.Field<double?>("PreviousLowestOfferMatchingPrice"),
                            CounterOfferPrice = req.Field<double?>("CounterOfferPrice"),
                            ISCounterOfferAccepted = req.Field<bool?>("ISCounterOfferAccepted"),
                            ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                            SuggestionCounterTime = req.Field<DateTime?>("SuggestionCounterTime")


                            //   IsSuggestedPriceSet = req.Field<bool?>("IsSuggestedPriceSet")
                        }).FirstOrDefault();

                        quotationData.QuotationParts = dt.Tables[8].AsEnumerable().Select(rp => new QuotationPart
                        {
                            RequestedPartID = rp.Field<int>("RequestedPartID"),
                            QuotationID = rp.Field<int>("QuotationID"),
                            QuotationPartID = rp.Field<int>("QuotationPartID"),
                            ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                            ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                            ConditionTypeNameArabic = rp.Field<string>("ConditionTypeArabicName"),

                            NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                            NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                            NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                            
                            Price = rp.Field<double>("Price"),
                            WithoutDiscountPrice = rp.Field<double>("WithoutDiscountPrice"),
                            Quantity = rp.Field<int>("Quantity"),
                            SupplierID = rp.Field<int>("SupplierID"),
                            AutomotivePartName = rp.Field<string>("PartName"),
                            AdminRejectNote = rp.Field<string>("AdminRejectNote"),
                            DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                            IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                            IsOrdered = rp.Field<bool?>("IsOrdered"),
                            IsIncluded = rp.Field<bool?>("IsIncluded"),
                            IsReferred = rp.Field<bool?>("IsReferred"),
                            IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                            IsAccepted = rp.Field<bool?>("IsAccepted"),
                            PartBranchID = rp.Field<int?>("PartBranchID"),
                            OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                            RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                            RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                            RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                            RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                            PreviousPrice = rp.Field<double?>("PreviousPrice"),
                            DepriciationPrice = rp.Field<double?>("DepriciationPrice"),
                            Depriciationvalue = rp.Field<double?>("Depriciationvalue")

                        }).ToList();

                        quotationData.QuotationPartsImages = dt.Tables[9].AsEnumerable().Select(img => new Image
                        {
                            ImageID = img.Field<int?>("ImageID"),
                            EncryptedName = img.Field<string>("EncryptedName"),
                            OriginalName = img.Field<string>("OriginalName"),
                            ImageURL = img.Field<string>("ImageURL"),
                            ObjectID = img.Field<int>("ObjectID"),
                            ObjectTypeID = img.Field<Int16?>("ObjectTypeID"),
                        }).ToList();

                        quotationData.QuotationIDUnderTenPercent = dt.Tables[10].AsEnumerable().Select(ID => ID.Field<int>("QuotationID")).ToList();

                        quotationData.partsBargains = dt.Tables[11].AsEnumerable().Select(rp => new PartsBargain
                        {
                            PartsBargainID = rp.Field<int?>("PartsBargainID"),
                            SuggestedPrice = rp.Field<double?>("SuggestedPrice"),
                            CounterOffer = rp.Field<double?>("CounterOffer"),
                            CreatedOn = rp.Field<DateTime?>("CreatedOn"),
                            IcUserName = rp.Field<string>("InsuraneCompanyName"),
                            SupplierName = rp.Field<string>("SupplierName"),
                            ISSuggestionAccepted = rp.Field<bool?>("ISSuggestionAccepted"),
                            ISCounterAccepted = rp.Field<bool?>("ISCounterAccepted")
                        }).ToList();
                    }
                   
                }

                return quotationData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetShopInvoice
        public ShopQuotations GetShopInvoice(int QuotationID, int DemandID)
        {
            DataSet dt = new DataSet();
            try
            {
                var shopQuotations = new ShopQuotations();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getShopInvoice]", CommandType.StoredProcedure, sParameter))
                {
                    shopQuotations.Quotations = dt.Tables[0].AsEnumerable().Select(req => new Quotation
                    {
                        QuotationID = req.Field<int>("QuotationID"),
                        DemandID = req.Field<int>("DemandID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("MakeName"),
                        ModelCode = req.Field<string>("ModelCode"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusName = req.Field<string>("StatusName"),

                    }).ToList();

                    shopQuotations.QuotationParts = dt.Tables[1].AsEnumerable().Select(rp => new QuotationPart
                    {
                        QuotationPartID = rp.Field<int>("QuotationPartID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        QuotationID = rp.Field<int>("QuotationID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),

                    }).ToList();

                    shopQuotations.QuotationPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        QuotationID = img.Field<int>("QuotationID"),

                    }).ToList();

                }

                return shopQuotations;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetPSInvoice
        public QuotationData GetPSInvoice(int QuotationID, int DemandID)
        {
            DataSet dt = new DataSet();
            try
            {
                var companyrequests = new QuotationData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getPSInvoice]", CommandType.StoredProcedure, sParameter))
                {
                    companyrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),

                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        ICName = req.Field<string>("ICName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusName = req.Field<string>("StatusName"),
                        SupplierName = req.Field<string>("SupplierName"),
                        CPName = req.Field<string>("CPName"),
                        /////
                        LogoURL = req.Field<string>("LogoURL"),
                        CPPhone = req.Field<string>("CPPhone"),
                        CPEmail = req.Field<string>("CPEmail"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        CountryName = req.Field<string>("CountryName"),
                        ////
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        CompanyAddress1 = req.Field<string>("CompanyAddress1"),
                        CompanyAddress2 = req.Field<string>("CompanyAddress2"),
                        CompanyCountryName = req.Field<string>("CompanyCountryName"),
                        TotalLabour = req.Field<int?>("TotalLabour"),
                        TotalCost = req.Field<double?>("TotalCost")

                    }).ToList();

                    companyrequests.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
                        DemandID = rp.Field<int?>("DemandID"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),

                    }).ToList();

                    companyrequests.RequestedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        //RequestID = img.Field<int>("RequestID"),

                    }).ToList();

                    companyrequests.QuotationParts = dt.Tables[3].AsEnumerable().Select(rp => new QuotationPart
                    {
                        QuotationPartID = rp.Field<int>("QuotationPartID"),
                        QuotationID = rp.Field<int>("QuotationID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        Quantity = rp.Field<int>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ManufacturerID = rp.Field<Int16?>("ManufacturerID"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        Price = rp.Field<double?>("Price"),
                        ManufacturerName = rp.Field<string>("ManufacturerName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                    }).ToList();

                    companyrequests.QuotationPartsImages = dt.Tables[4].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        //QuotationID = img.Field<int>("QuotationID"),

                    }).ToList();

                    companyrequests.Quotation = dt.Tables[5].AsEnumerable().Select(img => new Quotation
                    {
                        InvoiceImage = img.Field<string>("InvoiceImage"),
                        QuotationID = img.Field<int>("QuotationID"),

                    }).FirstOrDefault();
                }

                return companyrequests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetDemandRequestData
        public QuotationData GetDemandRequestData(int RequestID, int? QuotationID, int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                var quotationData = new QuotationData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getDemandRequestData]", CommandType.StoredProcedure, sParameter))
                {
                    quotationData.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),                        
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        QuotationID = req.Field<int?>("QuotationID"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        PublishedOn = req.Field<DateTime>("PublishedOn"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                    }).FirstOrDefault();

                    quotationData.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        //DesiredManufacturerID = rp.Field<Int16>("DesiredManufacturerID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        DemandID = rp.Field<int?>("DemandID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        //DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsIncluded = rp.Field<bool?>("IsIncluded"),
                        IsRecycled = rp.Field<bool?>("IsRecycled"),

                        //DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),

                    }).ToList();

                    quotationData.RequestedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                }

                return quotationData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetPSDashboard
        public PSDashboard GetPSDashboard(int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                PSDashboard psDashboard = new PSDashboard();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };
                using (dt = ADOManager.Instance.DataSet("[getPSDashboardData]", CommandType.StoredProcedure, sParameter))
                {
                    psDashboard.Requests = dt.Tables[0].AsEnumerable().Select(req => new GraphInfo
                    {
                        TotalCount = req.Field<int?>("TotalDemands"),
                        MonthName = req.Field<string>("MonthName"),

                    }).ToList();

                    psDashboard.Quotations = dt.Tables[1].AsEnumerable().Select(req => new GraphInfo
                    {
                        TotalCount = req.Field<int?>("TotalQuotations"),
                        MonthName = req.Field<string>("MonthName"),

                    }).ToList();

                }
                return psDashboard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveBranch
        public string SaveBranch(PartBranch PartBranch)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = PartBranch.CreatedBy},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = PartBranch.SupplierID},
                        new SqlParameter { ParameterName = "@BranchGoogleMapLink" , Value = PartBranch.BranchGoogleMapLink},
                        new SqlParameter { ParameterName = "@BranchName" , Value = PartBranch.BranchName},
                        new SqlParameter { ParameterName = "@BranchPhone" , Value = PartBranch.BranchPhone},
                        new SqlParameter { ParameterName = "@BranchAreaName" , Value = PartBranch.BranchAreaName},
                        new SqlParameter { ParameterName = "@BranchCityID" , Value = PartBranch.BranchCityID},
                        new SqlParameter { ParameterName = "@RegistrationNo" , Value = PartBranch.RegistrationNo},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[savePartBranch]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region UpdateBranch
        public string UpdateBranch(PartBranch PartBranch)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@BranchID" , Value = PartBranch.BranchID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = PartBranch.ModifiedBy},
                        new SqlParameter { ParameterName = "@BranchGoogleMapLink" , Value = PartBranch.BranchGoogleMapLink},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = PartBranch.SupplierID},
                        new SqlParameter { ParameterName = "@BranchName" , Value = PartBranch.BranchName},
                        new SqlParameter { ParameterName = "@BranchPhone" , Value = PartBranch.BranchPhone},
                        new SqlParameter { ParameterName = "@BranchAreaName" , Value = PartBranch.BranchAreaName},
                        new SqlParameter { ParameterName = "@BranchCityID" , Value = PartBranch.BranchCityID},
                        new SqlParameter { ParameterName = "@RegistrationNo" , Value = PartBranch.RegistrationNo},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updatePartBranch]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Branch updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region DeleteBranch
        public string DeleteBranch(int BranchID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@BranchID" , Value = BranchID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[deletePartBranch]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Branch deleted successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[deletePartBranch]", CommandType.StoredProcedure, sParameter);
                return result.ToString();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAllBranchs
        public List<PartBranch> GetAllBranches(int SupplierID, int LoginUserID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Branchs = new List<PartBranch>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@LoginUserID" , Value = LoginUserID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getPartBranches]", CommandType.StoredProcedure, sParameter))
                {
                    Branchs = dt.Tables[0].AsEnumerable().Select(req => new PartBranch
                    {
                        BranchID = req.Field<int>("BranchID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        BranchCityID = req.Field<int>("BranchCityID"),
                        BranchName = req.Field<string>("BranchName"),
                        BranchGoogleMapLink = req.Field<string>("BranchGoogleMapLink"),
                        BranchPhone = req.Field<string>("BranchPhone"),
                        BranchCityName = req.Field<string>("BranchCityName"),
                        BranchAreaName = req.Field<string>("BranchAreaName"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        RegistrationNo = req.Field<string>("RegistrationNo"),
                    }).ToList();

                }

                return Branchs;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetSingleBranch
        public PartBranch GetSingleBranch(int BranchID, int LoginUserID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Branch = new PartBranch();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@BranchID" , Value = BranchID},
                        new SqlParameter { ParameterName = "@LoginUserID" , Value = LoginUserID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getSinglePartBranch]", CommandType.StoredProcedure, sParameter))
                {
                    Branch = dt.Tables[0].AsEnumerable().Select(req => new PartBranch
                    {
                        BranchID = req.Field<int>("BranchID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        BranchCityID = req.Field<int>("BranchCityID"),
                        BranchName = req.Field<string>("BranchName"),
                        BranchGoogleMapLink = req.Field<string>("BranchGoogleMapLink"),
                        BranchPhone = req.Field<string>("BranchPhone"),
                        BranchCityName = req.Field<string>("BranchCityName"),
                        BranchAreaName = req.Field<string>("BranchAreaName"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        RegistrationNo = req.Field<string>("RegistrationNo"),
                    }).FirstOrDefault();

                }

                return Branch;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveInvoiceImage
        public string SaveInvoiceImage(Quotation quotation)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = quotation.QuotationID},
                        new SqlParameter { ParameterName = "@InvoiceImage" , Value = quotation.InvoiceImage},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveInvoiceImage]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region DeleteUvParteBranch
        public string DeleteUvPart(int UniversalPartID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@UniversalPartID" , Value = UniversalPartID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };

             
                var result = ADOManager.Instance.ExecuteScalar("[deleteUniversalPart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveUvParts
        public string SaveUvPart(AutomotivePart uvPart)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AutomativePartID" , Value = uvPart.AutomotivePartID},
                        new SqlParameter { ParameterName = "@IsConditionNew" , Value = uvPart.IsConditionNew},
                        new SqlParameter { ParameterName = "@IsConditionAfterMarket" , Value = uvPart.IsConditionAfterMarket},
                        new SqlParameter { ParameterName = "@IsConditionOriginal" , Value = uvPart.IsConditionOriginal},
                        new SqlParameter { ParameterName = "@IsConditionUsed" , Value = uvPart.IsConditionUsed},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = uvPart.SupplierID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = uvPart.CreatedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveUniversalPart]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region QuotationNotAvailable
        public string QuotationNotAvailable(int RequestID, int SupplierID, int DemandID, string notAvailableNote)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                        new SqlParameter { ParameterName = "@NotAvailableNote" , Value = notAvailableNote},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[quotationNotAvailable]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAppVersion
        public AppSetting GetAppVersion(string MobileAppVersion, int? SupplierID)
        {
            try
            {
                DataSet dt = new DataSet();
                var setting = new AppSetting();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@MobileAppVersion" , Value = MobileAppVersion},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getAppVersion]", CommandType.StoredProcedure, sParameter))
                {
                    setting = dt.Tables[0].AsEnumerable().Select(req => new AppSetting
                    {
                        AppVersion = req.Field<string>("AppVersion"),
                        IsForceUpdated = req.Field<bool?>("IsForceUpdated"),
                        IsLogout = req.Field<bool?>("IsLogout")

                    }).FirstOrDefault();

                }
               // var result = ADOManager.Instance.ExecuteScalar("[getAppVersion]", CommandType.StoredProcedure, sParameter);
                return setting;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion



        #region
       public string updatePartiallySellingStatus(int QuotationID, bool IsPartialSellings, int ModifiedBy)
       {
            try
            {
                DataSet dt = new DataSet();
                List<Quotation> Quotations = new List<Quotation>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@IsPartialSellings" , Value = IsPartialSellings},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[updatePartiallySellingStatus]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetMobileAppVersion
        public AppSetting GetMobileAppVersion(string MobileAppVersion, int? UserID)
        {
            try
            {
                DataSet dt = new DataSet();
                var setting = new AppSetting();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@MobileAppVersion" , Value = MobileAppVersion},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getMobileAppVersion]", CommandType.StoredProcedure, sParameter))
                {
                    setting = dt.Tables[0].AsEnumerable().Select(req => new AppSetting
                    {
                        AppVersion = req.Field<string>("AppVersion"),
                        IsForceUpdated = req.Field<bool?>("IsForceUpdated"),
                        IsLogout = req.Field<bool?>("IsLogout")

                    }).FirstOrDefault();

                }
                // var result = ADOManager.Instance.ExecuteScalar("[getAppVersion]", CommandType.StoredProcedure, sParameter);
                return setting;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region
        public string saveTotallabourPartsPrice(int demandID, int TotallabourPartsPrice, int UserID, int CompanyTypeID, int RoleID)
        {
            try
            {
                DataSet dt = new DataSet();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@DemandID" , Value = demandID},
                        new SqlParameter { ParameterName = "@TotalCost" , Value = TotallabourPartsPrice},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@CompanyTypeID" , Value = CompanyTypeID},
                        new SqlParameter { ParameterName = "@RoleID" , Value = RoleID}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[SaveTotallabourPartsPrice]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
