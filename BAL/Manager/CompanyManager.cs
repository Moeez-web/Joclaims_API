using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using MODEL.Models.Request_Draft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IronPdf;
using System.IO;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using MODEL.Models.Report.Common;
using MODEL.Models.Inspekt;
using System.ComponentModel.Design;
using MODEL.InspectAI;

namespace BAL.Manager
{
    public class CompanyManager : ICompanyManager
    {

        #region GetAccidentMetaData
        public AccidentMetaData GetAccidentMetaData(int CompanyID, int? WorkshopID)
        {
            DataSet dt = new DataSet();
            try
            {
                var accidentMetaData = new AccidentMetaData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getAccidentMetaData]", CommandType.StoredProcedure, sParameter))
                {
                    //accidentMetaData.Makes = dt.Tables[0].AsEnumerable().Select(make => new Make
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

                    //accidentMetaData.Models = dt.Tables[1].AsEnumerable().Select(model => new Model
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

                    //accidentMetaData.Years = dt.Tables[2].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    accidentMetaData.Workshops = dt.Tables[0].AsEnumerable().Select(req => new ICWorkshop
                    {
                        ICWorkshopID = req.Field<int>("ICWorkshopID"),
                        WorkshopID = req.Field<int>("WorkshopID"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        CompanyTypeID = req.Field<int?>("CompanyTypeID"),
                    }).ToList();
                    accidentMetaData.AccidentMarkers = dt.Tables[1].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),
                    }).ToList();
                    accidentMetaData.Agency = dt.Tables[2].AsEnumerable().Select(req => new ICWorkshop
                    {
                        ICWorkshopID = req.Field<int>("ICWorkshopID"),
                        WorkshopID = req.Field<int>("WorkshopID"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        CompanyTypeID = req.Field<int?>("CompanyTypeID")
                    }).ToList();
                    accidentMetaData.AllCountries = dt.Tables[3].AsEnumerable().Select(req => new Country
                    {
                        CountryID = req.Field<Int16>("CountryID"),
                        CountryName = req.Field<string>("countryName")

                    }).ToList();
                    accidentMetaData.ReplacementCar = dt.Tables[4].AsEnumerable().Select(req => new ReplacementCar
                    {
                        ReplacementCarID = req.Field<int>("ReplacementCarID"),
                        ReplacementCarName = req.Field<string>("ReplacementCarName"),
                        ReplacementCarEmail = req.Field<string>("ReplacementCarEmail")

                    }).ToList();

                    if (dt.Tables.Count > 5)
                    {
                        accidentMetaData.Users = dt.Tables[5].AsEnumerable().Select(u => new Employee
                        {
                            EmployeeID = u.Field<int>("EmployeeID"),
                            CompanyID = u.Field<int>("CompanyID"),
                            PositionID = u.Field<Int16>("PositionID"),
                            Email = u.Field<string>("Email"),
                            SurveyorName = u.Field<string>("SurveyorName"),
                            IsDeleted = u.Field<bool>("IsDeleted"),
                            SurveyorID = u.Field<int>("UserID")


                        }).ToList();


                    }
                    if(dt.Tables.Count > 6)
                    {
                        accidentMetaData.faultyCompany = dt.Tables[6].AsEnumerable().Select(fc => new FaultyCompany
                        {
                            FaultyCompanyID = fc.Field<int>("FaultyCompanyID"),
                            FaultyCompanyName = fc.Field<string>("FaultyCompanyName"),
                            FaultyCompanyEmail = fc.Field<string>("FaultyCompanyEmail"),
                            FaultyCompanyNameArabic = fc.Field<string>("FaultyCompanyNameArabic")

                        }).ToList();
                    }

                    //accidentMetaData.AutomotivePart = dt.Tables[2].AsEnumerable().Select(pti => new AutomotivePart
                    //{
                    //    AutomotivePartID = pti.Field<int>("AutomotivePartID"),
                    //    MakeID = pti.Field<int>("MakeID"),
                    //    ModelID = pti.Field<int>("ModelID"),
                    //    PartName = pti.Field<string>("PartName"),
                    //    ProductionYear = pti.Field<Int16>("ProductionYear"),

                    //}).ToList();

                    //accidentMetaData.ObjectTypes = dt.Tables[3].AsEnumerable().Select(ot => new ObjectType
                    //{
                    //    ObjectTypeID = ot.Field<Int16>("ObjectTypeID"),
                    //    ObjectName = ot.Field<string>("ObjectName"),
                    //    ArabicTypeName = ot.Field<string>("ArabicTypeName"),
                    //    Icon = ot.Field<string>("Icon"),
                    //    TypeName = ot.Field<string>("TypeName"),

                    //}).ToList();

                }

                return accidentMetaData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetRequestMetaData
        public RequestMetaData GetRequestMetaData(int? CompanyID, string AccidentNo, int? AccidentID)
        {
            DataSet dt = new DataSet();
            try
            {
                var requestMetaData = new RequestMetaData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getRequestMetaData]", CommandType.StoredProcedure, sParameter))
                {
                    //requestMetaData.Makes = dt.Tables[0].AsEnumerable().Select(make => new Make
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

                    //requestMetaData.Models = dt.Tables[1].AsEnumerable().Select(model => new Model
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

                    //requestMetaData.Years = dt.Tables[2].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    //requestMetaData.AutomotivePart = dt.Tables[0].AsEnumerable().Select(pti => new AutomotivePart
                    //{
                    //    AutomotivePartID = pti.Field<int>("AutomotivePartID"),
                    //    MakeID = pti.Field<int>("MakeID"),
                    //    ModelID = pti.Field<int>("ModelID"),
                    //    IsVerified = pti.Field<bool>("IsVerified"),
                    //    ImageURL = pti.Field<string>("ImageURL"),
                    //    PartName = pti.Field<string>("PartName"),
                    //    ProductionYear = pti.Field<Int16>("ProductionYear"),

                    //}).ToList();

                    requestMetaData.Accidents = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {
                        AccidentID = acd.Field<int>("AccidentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        TotalRequestCount = acd.Field<int?>("TotalRequestCount"),
                        IsPurchasing = acd.Field<bool?>("IsPurchasing"),
                        WorkshopDetails = acd.Field<string>("WorkshopDetails"),
                        PendingRequestID = acd.Field<int>("PendingRequestID"),
                        ImageCaseEncryptedName = acd.Field<string>("ImageCaseEncryptedName"),
                        ImageCaseURL = acd.Field<string>("ImageCaseURL"),
                        BodyTypeID = acd.Field<Int16?>("BodyTypeID"),
                        EngineTypeID = acd.Field<Int16?>("EngineTypeID"),
                        AskForPartCondition = acd.Field<bool?>("AskForPartCondition"),
                        JCSeriesID = acd.Field<string>("JCSeriesCode"),
                        AccidentHappendOn = acd.Field<DateTime?>("AccidentHappendOn")

                    }).ToList();

                    requestMetaData.AccidentParts = dt.Tables[1].AsEnumerable().Select(rp => new AccidentPart
                    {
                        AccidentPartID = rp.Field<int>("AccidentPartID"),
                        AccidentID = rp.Field<int>("AccidentID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsRequestCreated = rp.Field<bool?>("IsRequestCreated"),
                        DamagePointName = rp.Field<string>("DamagePointName")
                    }).ToList();

                    requestMetaData.AccidentPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    requestMetaData.AccidentMarkers = dt.Tables[3].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();
                    requestMetaData.jCSeriesCases = dt.Tables[4].AsEnumerable().Select(am => new JCSeriesCase
                    {
                        JCSeriesID = am.Field<string>("JCSeriesID"),
                        CarImageEncryptedName = am.Field<string>("CarImageEncryptedName"),
                        FuelType = am.Field<string>("FuelType"),
                        OverLappingYear = am.Field<int?>("OverLappingYear"),
                        CorrectSeriesID = am.Field<string>("CorrectSeriesID"),
                        AccidentID = am.Field<int>("AccidentID"),
                        CarImageURL = am.Field<string>("CarImageURL"),
                        BodyType = am.Field<string>("BodyType"),
                        EnglishModelName = am.Field<string>("EnglishModelName"),
                    }).ToList();
                }

                return requestMetaData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetCompanyProfile
        public CompanyProfile GetCompanyProfile(int CompanyID)
        {
            DataSet dt = new DataSet();
            try
            {
                var companyProfile = new CompanyProfile();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getCompanyProfile]", CommandType.StoredProcedure, sParameter))
                {
                    companyProfile.Company = dt.Tables[0].AsEnumerable().Select(cmp => new Company
                    {
                        CompanyID = cmp.Field<int>("CompanyID"),
                        CompanyName = cmp.Field<string>("Name"),
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
                        ESignatureURL = cmp.Field<string>("ESignatureURL"),
                        FaxNumber = cmp.Field<string>("FaxNumber"),
                        WebsiteAddress = cmp.Field<string>("WebsiteAddress"),
                        DiscountValue = cmp.Field<double?>("DiscountValue"),
                        EmailTo = cmp.Field<string>("EmailTo"),
                        EmailCC = cmp.Field<string>("EmailCC"),
                        // SuggestionOfferTime = cmp.Field<int?>("SuggestionOfferTime")

                    }).FirstOrDefault();

                    companyProfile.Countries = dt.Tables[1].AsEnumerable().Select(country => new Country
                    {
                        LanguageCountryID = country.Field<Int16>("LanguageCountryID"),
                        CountryName = country.Field<string>("CountryName"),
                        CountryNameArabic = country.Field<string>("CountryNameArabic"),
                        LanguageID = country.Field<byte>("LanguageID"),
                        CountryID = country.Field<Int16>("CountryID"),

                    }).ToList();

                    companyProfile.Cities = dt.Tables[2].AsEnumerable().Select(city => new City
                    {
                        LanguageCityID = city.Field<int>("LanguageCityID"),
                        CityCode = city.Field<string>("CityCode"),
                        CountryID = city.Field<Int16>("CountryID"),
                        LanguageID = city.Field<byte>("LanguageID"),
                        CityID = city.Field<int>("CityID"),
                        Latitude = city.Field<double>("Latitude"),
                        Longitude = city.Field<double>("Longitude"),
                        CityName = city.Field<string>("CityName"),

                    }).ToList();

                    companyProfile.Positions = dt.Tables[3].AsEnumerable().Select(object1 => new ObjectType
                    {
                        ObjectTypeID = object1.Field<Int16>("ObjectTypeID"),
                        ObjectName = object1.Field<string>("ObjectName"),
                        TypeName = object1.Field<string>("TypeName"),
                        ArabicTypeName = object1.Field<string>("ArabicTypeName")
                    }).ToList();
                }
                return companyProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveCompanyProfile
        public string SaveCompanyProfile(Company company)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = company.CompanyID},
                        new SqlParameter { ParameterName = "@UserID" , Value = company.UserID},
                        new SqlParameter { ParameterName = "@AddressLine1" , Value = company.AddressLine1},
                        new SqlParameter { ParameterName = "@AddressLine2" , Value = company.AddressLine2},
                        new SqlParameter { ParameterName = "@CompanyName" , Value = company.CompanyName},
                        new SqlParameter { ParameterName = "@CPEmail" , Value = company.CPEmail},
                        new SqlParameter { ParameterName = "@CPFirstName" , Value = company.CPFirstName},
                        new SqlParameter { ParameterName = "@CPLastName" , Value = company.CPLastName},
                        new SqlParameter { ParameterName = "@CPPhone" , Value = company.CPPhone},
                        new SqlParameter { ParameterName = "@CPPositionID" , Value = company.CPPositionID},
                        new SqlParameter { ParameterName = "@LogoURL" , Value = company.LogoURL},
                        new SqlParameter { ParameterName = "@CityID" , Value = company.CityID},
                        new SqlParameter { ParameterName = "@CountryID" , Value = company.CountryID},
                        new SqlParameter { ParameterName = "@ESignatureURL" , Value = company.ESignatureURL},
                        new SqlParameter { ParameterName = "@FaxNumber" , Value = company.FaxNumber},
                        new SqlParameter { ParameterName = "@WebsiteAddress" , Value = company.WebsiteAddress},
                        new SqlParameter { ParameterName = "@DiscountValue" , Value = company.DiscountValue},
                        new SqlParameter { ParameterName = "@EmailTo" , Value = company.EmailTo},
                        new SqlParameter { ParameterName = "@EmailCC" , Value = company.EmailCC},
                        new SqlParameter { ParameterName = "@SuggestionOfferTime" , Value = company.SuggestionOfferTime},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveCompanyProfile]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Profile saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SavePartsRequest
        public RequestResponse SavePartsRequest(RequestData request)
        {
            try
            {
                DataSet dt = new DataSet();
                var requestResponse = new RequestResponse();

                var XMLRequestedParts = request.RequestedParts.ToXML("ArrayOfRequestedParts");
                var XMLNotes = request.Notes.ToXML("ArrayOfNotes");
                var XMLRequestedPartsImages = request.RequestedPartsImages != null && request.RequestedPartsImages.Count() > 0 ? request.RequestedPartsImages.ToXML("ArrayOfRequestedPartsImages") : null;
                var XMLRequestTasks = request.RequestTasks != null && request.RequestTasks.Count() > 0 ? request.RequestTasks.ToXML("ArrayOfRequestTasks") : null;
                var XMLAccidentMarker = request.AccidentMarkers != null && request.AccidentMarkers.Count() > 0 ? request.AccidentMarkers.ToXML("ArrayOfAccidentMarker") : null;
                var XMLAccidentImages = request.AccidentImages != null && request.AccidentImages.Count() > 0 ? request.AccidentImages.ToXML("ArrayOfAccidentImages") : null;
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLNotes" , Value = XMLNotes},
                        new SqlParameter { ParameterName = "@XMLRequestTasks" , Value = XMLRequestTasks},
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@XMLRequestedPartsImages" , Value = XMLRequestedPartsImages},
                        new SqlParameter { ParameterName = "@VIN" , Value = request.Request.VIN},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = request.Request.CompanyID},
                        new SqlParameter { ParameterName = "@BiddingHours" , Value = request.Request.BiddingHours},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = request.Request.AccidentID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = request.Request.MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = request.Request.ModelID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = request.Request.CreatedBy},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = request.Request.ProductionYear},
                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = request.Request.EngineTypeID},
                        new SqlParameter { ParameterName = "@IsOldPartsRequired" , Value = request.Request.IsOldPartsRequired},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = request.Request.BodyTypeID},

                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = request.Request.JCSeriesID},
                         new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = request.Request.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = request.Request.ImageCaseURL},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = request.Request.PlateNo},
                        new SqlParameter { ParameterName = "@OwnerPhoneNo" , Value = request.Request.OwnerPhoneNo},
                        new SqlParameter { ParameterName = "@VehicleOwnerName" , Value = request.Request.VehicleOwnerName},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = request.Request.IsPurchasing},
                        new SqlParameter { ParameterName = "@XMLAccidentImages" , Value = XMLAccidentImages},
                        new SqlParameter { ParameterName = "@AccidentMarkerXML" , Value = XMLAccidentMarker},
                        new SqlParameter { ParameterName = "@DraftID" , Value = request.Request.DraftID},

                    };
                var procedureName = request.Request.IsAgencyRequest == 1 ? "[dbo].[saveAgencyPartsRequest]" : "[dbo].[savePartsRequest]";
                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet(procedureName, CommandType.StoredProcedure, sParameter))
                {
                    if (dt.Tables[0].Columns.Contains("LIMIT_EXCEED_ERROR"))
                    {

                        requestResponse.Result = "false";
                    }
                    else
                    {
                        requestResponse.Result = Convert.ToString(dt.Tables[0].Rows[0]["Result"]);
                        requestResponse.RequestID = Convert.ToInt32(dt.Tables[0].Rows[0]["RequestID"]);
                        requestResponse.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                        {
                            RequestedPartID = rp.Field<int>("RequestedPartID"),
                            AutomotivePartID = rp.Field<int>("AutomotivePartID"),

                        }).ToList();
                    }
                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartsRequest
        public RequestResponse UpdatePartsRequest(RequestData request)
        {
            try
            {
                DataSet dt = new DataSet();
                var requestResponse = new RequestResponse();

                var XMLRequestedParts = request.RequestedParts.ToXML("ArrayOfRequestedParts");
                var XMLNotes = request.Notes.ToXML("ArrayOfNotes");
                var XMLRequestedPartsImages = request.RequestedPartsImages != null && request.RequestedPartsImages.Count() > 0 ? request.RequestedPartsImages.ToXML("ArrayOfRequestedPartsImages") : null;
                var XMLRequestTasks = request.RequestTasks != null && request.RequestTasks.Count() > 0 ? request.RequestTasks.ToXML("ArrayOfRequestTasks") : null;
                var XMLAccidentImages = request.AccidentImages != null && request.AccidentImages.Count() > 0 ? request.AccidentImages.ToXML("ArrayOfAccidentImages") : null;
                var XMLAccidentMarker = request.AccidentMarkers != null && request.AccidentMarkers.Count() > 0 ? request.AccidentMarkers.ToXML("ArrayOfAccidentMarker") : null;
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLNotes" , Value = XMLNotes},
                        new SqlParameter { ParameterName = "@XMLRequestTasks" , Value = XMLRequestTasks},
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@XMLRequestedPartsImages" , Value = XMLRequestedPartsImages},
                        new SqlParameter { ParameterName = "@VIN" , Value = request.Request.VIN},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = request.Request.CompanyID},
                        new SqlParameter { ParameterName = "@RequestID" , Value = request.Request.RequestID},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = request.Request.AccidentID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = request.Request.MakeID},
                        new SqlParameter { ParameterName = "@BiddingHours" , Value = request.Request.BiddingHours},
                        new SqlParameter { ParameterName = "@ModelID" , Value = request.Request.ModelID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = request.Request.ModifiedBy},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = request.Request.ProductionYear},
                        new SqlParameter { ParameterName = "@IsOldPartsRequired" , Value = request.Request.IsOldPartsRequired},

                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = request.Request.EngineTypeID},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = request.Request.BodyTypeID},
                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = request.Request.JCSeriesID},
                        new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = request.Request.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = request.Request.ImageCaseURL},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = request.Request.PlateNo},
                        new SqlParameter { ParameterName = "@XMLAccidentImages" , Value = XMLAccidentImages},
                        new SqlParameter { ParameterName = "@AccidentMarkerXML" , Value = XMLAccidentMarker}

                    };

                //var result = ADOManager.Instance.ExecuteScalar("[updatePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[updatePartsRequest]", CommandType.StoredProcedure, sParameter))
                {
                    requestResponse.Result = Convert.ToString(dt.Tables[0].Rows[0]["Result"]);
                    requestResponse.RequestID = Convert.ToInt32(dt.Tables[0].Rows[0]["RequestID"]);
                    requestResponse.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),

                    }).ToList();
                }
                return requestResponse;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveAccident
        public string SaveAccident(AccidentData accident)
        {
            try
            {
                var XMLNotes = accident.Notes.ToXML("ArrayOfNotes");
                var XMLAccidentMarker = accident.AccidentMarkers.ToXML("ArrayOfAccidentMarker");
                var XMLAccidentImages = accident.AccidentImages.ToXML("ArrayOfAccidentImages");
                var XMLAccidentParts = accident.AccidentParts.ToXML("ArrayOfAccidentParts");
                var XMLAccidentPartsImages = accident.AccidentPartsImages.ToXML("ArrayOfAccidentPartsImages");
                List<TechnicalNotes> technical = new List<TechnicalNotes>();
                string XMLTechnicalNotes = null;
                if (accident.TechnicalNotes != null)
                {
                    technical.Add(accident.TechnicalNotes);


                    XMLTechnicalNotes = technical.ToXML("ArrayOfTechnicalNotes");
                }


                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLNotes" , Value = accident.Notes.Count > 0 ? XMLNotes : null},
                        new SqlParameter { ParameterName = "@AccidentMarkerXML" , Value = accident.AccidentMarkers.Count > 0 ? XMLAccidentMarker : null},
                        new SqlParameter { ParameterName = "@XMLAccidentImages" , Value = accident.AccidentImages.Count > 0 ? XMLAccidentImages : null},
                        new SqlParameter { ParameterName = "@XMLAccidentParts" , Value = accident.AccidentParts.Count > 0 ? XMLAccidentParts : null},
                        new SqlParameter { ParameterName = "@XMLAccidentPartsImages" , Value = accident.AccidentPartsImages.Count > 0 ? XMLAccidentPartsImages : null},
                        new SqlParameter { ParameterName = "@MakeID" , Value = accident.Accident.MakeID},
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = accident.Accident.ICWorkshopID},
                        new SqlParameter { ParameterName = "@VehicleOwnerName" , Value = accident.Accident.VehicleOwnerName},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = accident.Accident.AccidentNo},
                        new SqlParameter { ParameterName = "@ModelID" , Value = accident.Accident.ModelID},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = accident.Accident.PlateNo},
                        new SqlParameter { ParameterName = "@VIN" , Value = accident.Accident.VIN},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = accident.Accident.ProductionYear},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = accident.Accident.CompanyID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = accident.Accident.CreatedBy},
                        new SqlParameter { ParameterName = "@CarLicenseFront" , Value = accident.Accident.CarLicenseFront},
                        new SqlParameter { ParameterName = "@CarLicenseBack" , Value = accident.Accident.CarLicenseBack},
                        new SqlParameter { ParameterName = "@OwnerIDFront" , Value = accident.Accident.OwnerIDFront},
                        new SqlParameter { ParameterName = "@OwnerIDBack" , Value = accident.Accident.OwnerIDBack},
                        new SqlParameter { ParameterName = "@PoliceReport" , Value = accident.Accident.PoliceReport},
                        new SqlParameter { ParameterName = "@AccidentType" , Value = accident.Accident.AccidentTypeID},
                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = accident.Accident.JCSeriesID},
                        new SqlParameter { ParameterName = "@ResponsibilityType" , Value = accident.Accident.ResponsibilityTypeID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = accident.Accident.StatusID},
                        new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = accident.Accident.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = accident.Accident.ImageCaseURL},
                        new SqlParameter { ParameterName = "@PDFReport" , Value = accident.Accident.PDFReport},
                        new SqlParameter { ParameterName = "@CarLicenseFrontDescription" , Value = accident.Accident.CarLicenseFrontDescription},
                        new SqlParameter { ParameterName = "@CarLicenseBackDescription" , Value = accident.Accident.CarLicenseBackDescription},
                        new SqlParameter { ParameterName = "@OwnerIDFrontDescription" , Value = accident.Accident.OwnerIDFrontDescription},
                        new SqlParameter { ParameterName = "@OwnerIDBackDescription" , Value = accident.Accident.OwnerIDBackDescription},
                        new SqlParameter { ParameterName = "@PoliceReportDescription" , Value = accident.Accident.PoliceReportDescription},
                        new SqlParameter { ParameterName = "@PDFReportDescription" , Value = accident.Accident.PDFReportDescription},
                        new SqlParameter { ParameterName = "@CarsInvolved" , Value = accident.Accident.CarsInvolved},
                        new SqlParameter { ParameterName = "@AccidentHappendOn" , Value = accident.Accident.AccidentHappendOn},
                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = accident.Accident.EngineTypeID},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = accident.Accident.BodyTypeID},
                        new SqlParameter { ParameterName = "@ImportantNote" , Value = accident.Accident.ImportantNote},
                        new SqlParameter { ParameterName = "@FaultyCompanyName" , Value = accident.Accident.FaultyCompanyName},
                        new SqlParameter { ParameterName = "@OwnerPhoneNo" , Value = accident.Accident.OwnerPhoneNo},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = accident.Accident.IsPurchasing},
                        new SqlParameter { ParameterName = "@WorkshopDetails" , Value = accident.Accident.WorkshopDetails},
                        new SqlParameter { ParameterName = "@IndividualReturnID" , Value = accident.Accident.IndividualReturnID},
                        new SqlParameter { ParameterName = "@IndividualReturnText" , Value = accident.Accident.IndividualReturnText},
                        new SqlParameter { ParameterName = "@BrokerName" , Value = accident.Accident.BrokerName},
                        new SqlParameter { ParameterName = "@XMLTechnicalNotes" , Value = XMLTechnicalNotes},
                        new SqlParameter { ParameterName = "@ClaimentID" , Value = accident.Accident.ClaimentID},
                        new SqlParameter { ParameterName = "@OurResponsibility" , Value = accident.Accident.OurResponsibility},
                        new SqlParameter { ParameterName = "@isHighPriority" , Value = accident.Accident.isHighPriority},
                        new SqlParameter { ParameterName = "@isLearningSelected" , Value = accident.Accident.isLearningSelected},
                        new SqlParameter { ParameterName = "@SurveyorID" , Value = accident.Accident.SurveyorID},
                        new SqlParameter { ParameterName = "@SurveyorName" , Value = accident.Accident.SurveyorName},
                        new SqlParameter { ParameterName = "@SurveyorAppointmentDate" , Value = accident.Accident.SurveyorAppointmentDate},
                        new SqlParameter { ParameterName = "@PolicyNumber" , Value = accident.Accident.PolicyNumber},
                        new SqlParameter { ParameterName = "@isTotalLossSelected" , Value = accident.Accident.isTotalLossSelected},
                        //new SqlParameter { ParameterName = "@isSalvageSelected" , Value = accident.Accident.isSalvageSelected},
                        new SqlParameter { ParameterName = "@MarketInsureValue" , Value = accident.Accident.MarketInsureValue},
                        new SqlParameter { ParameterName = "@SalvageAmount" , Value = accident.Accident.SalvageAmount},
                        new SqlParameter { ParameterName = "@TotalLossOfferAmount" , Value = accident.Accident.TotalLossOfferAmount},
                        new SqlParameter { ParameterName = "@AgencyID" , Value = accident.Accident.AgencyID},
                        new SqlParameter { ParameterName = "@IsAgencyAccident" , Value = accident.Accident.IsAgencyAccident},
                        new SqlParameter { ParameterName = "@IsAgencyRepair" , Value = accident.Accident.IsAgencyRepair},
                        new SqlParameter { ParameterName = "@FaultyPolicyNumber" , Value = accident.Accident.FaultyPolicyNumber},
                        new SqlParameter { ParameterName = "@FaultyCompanyID" , Value = accident.Accident.FaultyCompanyID},
                        new SqlParameter { ParameterName = "@FaultyVehiclePlateNo" , Value = accident.Accident.FaultyVehiclePlateNo},
                        new SqlParameter { ParameterName = "@FaultyVehicleMakeID" , Value = accident.Accident.FaultyVehicleMakeID},
                        new SqlParameter { ParameterName = "@FaultyVehicleModelID" , Value = accident.Accident.FaultyVehicleModelID},
                        new SqlParameter { ParameterName = "@FaultyVehicleYearID" , Value = accident.Accident.FaultyVehicleYearID},
                        new SqlParameter { ParameterName = "@VehicleCountryID" , Value = accident.Accident.VehicleCountryID},
                        new SqlParameter { ParameterName = "@AccidentHappendOnTime" , Value = accident.Accident.AccidentHappendOnTime},
                        new SqlParameter { ParameterName = "@IsReplacementCar" , Value = accident.Accident.IsReplacementCar},
                        new SqlParameter { ParameterName = "@ReplacementCarID" , Value = accident.Accident.ReplacementCarID},
                        new SqlParameter { ParameterName = "@IsDeductible" , Value = accident.Accident.IsDeductible},
                        new SqlParameter { ParameterName = "@DeductibleAmount" , Value = accident.Accident.DeductibleAmount},
                        new SqlParameter { ParameterName = "@DeductibleStatus" , Value = accident.Accident.DeductibleStatus},
                        new SqlParameter { ParameterName = "@IsSumInsured" , Value = accident.Accident.IsSumInsured},
                        new SqlParameter { ParameterName = "@SumInsuredAmount" , Value = accident.Accident.SumInsuredAmount},
                        new SqlParameter { ParameterName = "@GeographicalExtension" , Value = accident.Accident.GeographicalExtension},
                        new SqlParameter { ParameterName = "@AOGCover" , Value = accident.Accident.AOGCover},
                        new SqlParameter { ParameterName = "@SRCCCover" , Value = accident.Accident.SRCCCover},
                        new SqlParameter { ParameterName = "@IsWindshieldCover" , Value = accident.Accident.IsWindshieldCover},
                        new SqlParameter { ParameterName = "@WindshieldCoverAmount" , Value = accident.Accident.WindshieldCoverAmount},
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnName" , Value = accident.Accident.AccidentIndividualReturnName},
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnPhoneNumber" , Value = accident.Accident.AccidentIndividualReturnPhoneNumber},
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnAddress" , Value = accident.Accident.AccidentIndividualReturnAddress}

                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveAccident]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : result == -1 ? DataValidation.accidentLimit : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateAccident
        public string UpdateAccident(AccidentData accident)
        {
            try
            {
                var XMLNotes = accident.Notes.ToXML("ArrayOfNotes");
                var XMLAccidentMarker = accident.AccidentMarkers.ToXML("ArrayOfAccidentMarker");
                var XMLAccidentImages = accident.AccidentImages.ToXML("ArrayOfAccidentImages");
                var XMLAccidentParts = accident.AccidentParts.ToXML("ArrayOfAccidentParts");
                var XMLAccidentPartsImages = accident.AccidentPartsImages.ToXML("ArrayOfAccidentPartsImages");
                List<TechnicalNotes> technical = new List<TechnicalNotes>();
                string XMLTechnicalNotes = null;
                if (accident.TechnicalNotes != null)
                {
                    technical.Add(accident.TechnicalNotes);


                    XMLTechnicalNotes = technical.ToXML("ArrayOfTechnicalNotes");
                }
                //technical.Add(accident.TechnicalNotes);


                //var XMLTechnicalNotes = technical.ToXML("ArrayOfTechnicalNotes");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLNotes" , Value = XMLNotes},
                        new SqlParameter { ParameterName = "@AccidentMarkerXML" , Value = accident.AccidentMarkers.Count > 0 ? XMLAccidentMarker : null},
                        new SqlParameter { ParameterName = "@XMLAccidentImages" , Value = accident.AccidentImages.Count > 0 ? XMLAccidentImages : null},
                        new SqlParameter { ParameterName = "@XMLAccidentParts" , Value = accident.AccidentParts.Count > 0 ? XMLAccidentParts : null},
                        new SqlParameter { ParameterName = "@XMLAccidentPartsImages" , Value = accident.AccidentPartsImages.Count > 0 ? XMLAccidentPartsImages : null},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = accident.Accident.AccidentID},
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = accident.Accident.ICWorkshopID},
                        new SqlParameter { ParameterName = "@VehicleOwnerName" , Value = accident.Accident.VehicleOwnerName},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = accident.Accident.AccidentNo},
                        new SqlParameter { ParameterName = "@MakeID" , Value = accident.Accident.MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = accident.Accident.ModelID},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = accident.Accident.PlateNo},
                        new SqlParameter { ParameterName = "@VIN" , Value = accident.Accident.VIN},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = accident.Accident.ProductionYear},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = accident.Accident.CompanyID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = accident.Accident.ModifiedBy},
                        new SqlParameter { ParameterName = "@CarLicenseFront" , Value = accident.Accident.CarLicenseFront},
                        new SqlParameter { ParameterName = "@CarLicenseBack" , Value = accident.Accident.CarLicenseBack},
                        new SqlParameter { ParameterName = "@OwnerIDFront" , Value = accident.Accident.OwnerIDFront},
                        new SqlParameter { ParameterName = "@OwnerIDBack" , Value = accident.Accident.OwnerIDBack},
                        new SqlParameter { ParameterName = "@PoliceReport" , Value = accident.Accident.PoliceReport},
                        new SqlParameter { ParameterName = "@AccidentType" , Value = accident.Accident.AccidentTypeID},
                        new SqlParameter { ParameterName = "@ResponsibilityType" , Value = accident.Accident.ResponsibilityTypeID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = accident.Accident.StatusID},
                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = accident.Accident.JCSeriesID},
                        new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = accident.Accident.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = accident.Accident.ImageCaseURL},

                        new SqlParameter { ParameterName = "@PDFReport" , Value = accident.Accident.PDFReport},
                        new SqlParameter { ParameterName = "@CarLicenseFrontDescription" , Value = accident.Accident.CarLicenseFrontDescription},
                        new SqlParameter { ParameterName = "@CarLicenseBackDescription" , Value = accident.Accident.CarLicenseBackDescription},
                        new SqlParameter { ParameterName = "@OwnerIDFrontDescription" , Value = accident.Accident.OwnerIDFrontDescription},
                        new SqlParameter { ParameterName = "@OwnerIDBackDescription" , Value = accident.Accident.OwnerIDBackDescription},
                        new SqlParameter { ParameterName = "@PoliceReportDescription" , Value = accident.Accident.PoliceReportDescription},
                        new SqlParameter { ParameterName = "@PDFReportDescription" , Value = accident.Accident.PDFReportDescription},
                        new SqlParameter { ParameterName = "@CarsInvolved" , Value = accident.Accident.CarsInvolved},
                        new SqlParameter { ParameterName = "@AccidentHappendOn" , Value = accident.Accident.AccidentHappendOn},
                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = accident.Accident.EngineTypeID},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = accident.Accident.BodyTypeID},
                        new SqlParameter { ParameterName = "@ImportantNote" , Value = accident.Accident.ImportantNote},
                        new SqlParameter { ParameterName = "@FaultyCompanyName" , Value = accident.Accident.FaultyCompanyName},
                        new SqlParameter { ParameterName = "@OwnerPhoneNo" , Value = accident.Accident.OwnerPhoneNo},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = accident.Accident.IsPurchasing},
                        new SqlParameter { ParameterName = "@WorkshopDetails" , Value = accident.Accident.WorkshopDetails},
                        new SqlParameter { ParameterName = "@IndividualReturnID" , Value = accident.Accident.IndividualReturnID},
                        new SqlParameter { ParameterName = "@IndividualReturnText" , Value = accident.Accident.IndividualReturnText},
                        new SqlParameter { ParameterName = "@BrokerName" , Value = accident.Accident.BrokerName},
                        new SqlParameter { ParameterName = "@XMLTechnicalNotes" , Value =  XMLTechnicalNotes},
                        new SqlParameter { ParameterName = "@isHighPriority" , Value =  accident.Accident.isHighPriority},
                        new SqlParameter { ParameterName = "@isLearningSelected" , Value =  accident.Accident.isLearningSelected},
                        new SqlParameter { ParameterName = "@SurveyorID" , Value =  accident.Accident.SurveyorID},
                        new SqlParameter { ParameterName = "@SurveyorName" , Value =  accident.Accident.SurveyorName},
                        new SqlParameter { ParameterName = "@SurveyorAppointmentDate" , Value =  accident.Accident.SurveyorAppointmentDate},
                        new SqlParameter { ParameterName = "@PolicyNumber" , Value =  accident.Accident.PolicyNumber},
                        new SqlParameter { ParameterName = "@isTotalLossSelected" , Value = accident.Accident.isTotalLossSelected},
                        new SqlParameter { ParameterName = "@FaultyPolicyNumber" , Value = accident.Accident.FaultyPolicyNumber},
                        //new SqlParameter { ParameterName = "@isSalvageSelected" , Value = accident.Accident.isSalvageSelected},
                        new SqlParameter { ParameterName = "@MarketInsureValue" , Value = accident.Accident.isTotalLossSelected == true?accident.Accident.MarketInsureValue:0},
                        new SqlParameter { ParameterName = "@SalvageAmount" , Value = accident.Accident.isTotalLossSelected == true?accident.Accident.SalvageAmount:0},
                        new SqlParameter { ParameterName = "@AgencyID" , Value = accident.Accident.AgencyID},
                        new SqlParameter { ParameterName = "@IsAgencyAccident" , Value = accident.Accident.IsAgencyAccident},
                        new SqlParameter { ParameterName = "@IsAgencyRepair" , Value = accident.Accident.IsAgencyRepair},
                        new SqlParameter { ParameterName = "@TotalLossOfferAmount" , Value = accident.Accident.isTotalLossSelected == true?accident.Accident.TotalLossOfferAmount:0},
                        new SqlParameter { ParameterName = "@FaultyCompanyID" , Value = accident.Accident.FaultyCompanyID},
                        new SqlParameter { ParameterName = "@FaultyVehiclePlateNo" , Value = accident.Accident.FaultyVehiclePlateNo},
                        new SqlParameter { ParameterName = "@FaultyVehicleMakeID" , Value = accident.Accident.FaultyVehicleMakeID},
                        new SqlParameter { ParameterName = "@FaultyVehicleModelID" , Value = accident.Accident.FaultyVehicleModelID},
                        new SqlParameter { ParameterName = "@FaultyVehicleYearID" , Value = accident.Accident.FaultyVehicleYearID},
                        new SqlParameter { ParameterName = "@VehicleCountryID" , Value = accident.Accident.VehicleCountryID},
                        new SqlParameter { ParameterName = "@AccidentHappendOnTime" , Value = accident.Accident.AccidentHappendOnTime},
                        new SqlParameter { ParameterName = "@IsReplacementCar" , Value = accident.Accident.IsReplacementCar},
                        new SqlParameter { ParameterName = "@ReplacementCarID" , Value = accident.Accident.ReplacementCarID},
                        new SqlParameter { ParameterName = "@IsDeductible" , Value = accident.Accident.IsDeductible},
                        new SqlParameter { ParameterName = "@DeductibleAmount" , Value = accident.Accident.DeductibleAmount},
                        new SqlParameter { ParameterName = "@DeductibleStatus" , Value = accident.Accident.DeductibleStatus},
                        new SqlParameter { ParameterName = "@IsSumInsured" , Value = accident.Accident.IsSumInsured},
                        new SqlParameter { ParameterName = "@SumInsuredAmount" , Value = accident.Accident.SumInsuredAmount},
                        new SqlParameter { ParameterName = "@GeographicalExtension" , Value = accident.Accident.GeographicalExtension},
                        new SqlParameter { ParameterName = "@AOGCover" , Value = accident.Accident.AOGCover},
                        new SqlParameter { ParameterName = "@SRCCCover" , Value = accident.Accident.SRCCCover},
                        new SqlParameter { ParameterName = "@IsWindshieldCover" , Value = accident.Accident.IsWindshieldCover},
                        new SqlParameter { ParameterName = "@WindshieldCoverAmount" , Value = accident.Accident.WindshieldCoverAmount}, 
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnName" , Value = accident.Accident.AccidentIndividualReturnName},
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnPhoneNumber" , Value = accident.Accident.AccidentIndividualReturnPhoneNumber},
                        new SqlParameter { ParameterName = "@AccidentIndividualReturnAddress" , Value = accident.Accident.AccidentIndividualReturnAddress},
                        new SqlParameter { ParameterName = "@FaultyVehicleCountryID" , Value = accident.Accident.FaultyVehicleCountryID}
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateAccident]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Accident updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetCompanyRequest
        public Companyrequests GetCompanyRequests(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
   int? ModelID, int? YearID, int? ICWorkshopID, string SearchQuery, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate,int? SurveyorID)

        {
            DataSet dt = new DataSet();
            try
            {
                var companyrequests = new Companyrequests();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID},
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = ICWorkshopID},
                        new SqlParameter { ParameterName = "@ApprovalStartDate" , Value = ApprovalStartDate},
                        new SqlParameter { ParameterName = "@ApprovalEndDate" , Value = ApprovalEndDate},
                        new SqlParameter { ParameterName = "@SurveyorID" , Value = SurveyorID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getCompanyRequests]", CommandType.StoredProcedure, sParameter))
                {
                    companyrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        ICName = req.Field<string>("Name"),
                        CPPhone = req.Field<string>("CPPhone"),
                        CPName = req.Field<string>("CPFirstName") + " " + req.Field<string>("CPLastName"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusID = req.Field<Int16>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        DemandID = req.Field<int?>("DemandID"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        OrderByName = req.Field<string>("OrderByName"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        ImgURL = req.Field<string>("ImgURL"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        PlateNo = req.Field<string>("PlateNo"),
                        SerialNo = req.Field<int?>("serialNo"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsCarRepairStarted = req.Field<bool?>("IsCarRepairStarted"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        TotalRequestedParts = req.Field<int?>("TotalRequestedParts"),
                        RequestRowNumber = req.Field<int?>("RequestRowNumber"),
                        TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
                        ClearanceRoute = req.Field<string>("ClearanceRoute"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        ReturnReason = req.Field<string>("ReturnReason"),
                        SupplierPhone = req.Field<string>("SupplierPhone"),
                        SupplierName = req.Field<string>("SupplierName"),
                        TotalTaskAmount = req.Field<double?>("TotalTaskAmount"),
                        ROPdfURL = req.Field<string>("ROPdfURL"),
                        JCSeriesCode = req.Field<string>("JCSeriesCode"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        DeletedStatusID = req.Field<short?>("DeletedStatusID"),
                        ClearanceSummaryPdfUrl = req.Field<string>("ClearanceSummaryPdfUrl"),
                        NotPurchasingLMOReason = req.Field<string>("NotPurchasingLMOReason"),
                        OwnerPhoneNo = req.Field<string>("OwnerPhoneNo"),
                        IsTaskApproved = req.Field<bool?>("IsTaskApproved"),
                        IsPartApproved = req.Field<bool?>("IsPartApproved"),
                        IsInstantPrice = req.Field<bool?>("IsInstantPrice"),
                        isHighPriority = req.Field<bool?>("isHighPriority"),
                        isLearningSelected = req.Field<bool?>("isLearningSelected"),
                        SurveyorID = req.Field<int?>("SurveyorID"),
                        SurveyorName = req.Field<string>("SurveyorName"),
                        SurveyorAppointmentDate = req.Field<DateTime?>("SurveyorAppointmentDate"),
                        IsAgencyRequest = req.Field<int?>("IsAgencyRequest"),
                        DeductibleStatus = req.Field<int?>("DeductibleStatus"),
                        IsEnterLabourPartPriceChecked = req.Field<int?>("IsEnterLabourPartPriceChecked"),

                    }).ToList();

                    //companyrequests.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    //{
                    //    RequestedPartID = rp.Field<int>("RequestedPartID"),
                    //    RequestID = rp.Field<int>("RequestID"),
                    //    AutomotivePartName = rp.Field<string>("AutomotivePartName"),

                    //}).ToList();

                    //companyrequests.Makes = dt.Tables[1].AsEnumerable().Select(make => new Make
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

                    //companyrequests.Models = dt.Tables[2].AsEnumerable().Select(model => new Model
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

                    //companyrequests.Years = dt.Tables[3].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    //companyrequests.Accidents = dt.Tables[4].AsEnumerable().Select(acd => new Accident
                    //{
                    //    AccidentID = acd.Field<int>("AccidentID"),
                    //    //AccidentNo = acd.Field<string>("AccidentNo"),
                    //    //CompanyID = acd.Field<int>("CompanyID"),
                    //    //MakeID = acd.Field<int>("MakeID"),
                    //    //ModelID = acd.Field<int>("ModelID"),
                    //    //PlateNo = acd.Field<string>("PlateNo"),
                    //    //ProductionYear = acd.Field<Int16>("ProductionYear"),
                    //    //VIN = acd.Field<string>("VIN"),
                    //    AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),

                    //}).ToList();

                    //companyrequests.RequestedPartsImages = dt.Tables[6].AsEnumerable().Select(img => new Image
                    //{
                    //    ImageID = img.Field<int>("ImageID"),
                    //    EncryptedName = img.Field<string>("EncryptedName"),
                    //    OriginalName = img.Field<string>("OriginalName"),
                    //    ImageURL = img.Field<string>("ImageURL"),
                    //    ObjectID = img.Field<int>("ObjectID"),
                    //    RequestID = img.Field<int>("RequestID"),

                    //}).ToList();

                    companyrequests.POApprovalEmployees = dt.Tables[1].AsEnumerable().Select(po => new POApproval
                    {
                        POApprovalID = po.Field<int>("POApprovalID"),
                        RequestID = po.Field<int>("RequestID"),
                        UserID = po.Field<int>("UserID"),
                        EmployeeID = po.Field<int>("EmployeeID"),
                        LastName = po.Field<string>("LastName"),
                        FirstName = po.Field<string>("FirstName"),
                        Email = po.Field<string>("Email"),
                        IsApproved = po.Field<bool?>("IsApproved"),
                        CreatedOn = po.Field<DateTime>("CreatedOn"),
                        ModifiedOn = po.Field<DateTime?>("ModifiedOn"),
                        MaxPrice = po.Field<int?>("MaxPrice"),

                    }).ToList();
                    companyrequests.TabInfoData = dt.Tables[2].AsEnumerable().Select(tbi => new TabInfo
                    {
                        PendingRequests = tbi.Field<int>("PendingRequests"),
                        InprogressRequests = tbi.Field<int>("InprogressRequests"),
                        ReferredRequests = tbi.Field<int>("ReferredRequests"),
                        PendingApprovalRequests = tbi.Field<int>("PendingApprovalRequests"),
                        OrderPlacedRequests = tbi.Field<int>("OrderPlacedRequests"),
                        DeliveredRequests = tbi.Field<int>("DeliveredRequests"),
                        PaidRequests = tbi.Field<int>("PaidRequests"),
                        CancelledRequests = tbi.Field<int>("CancelledRequests"),
                        ClosedRequests = tbi.Field<int>("ClosedRequests"),
                        DeletedRequests = tbi.Field<int>("DeletedRequests"),
                        DraftCount = tbi.Field<int?>("DraftCount")

                    }).FirstOrDefault();
                    companyrequests.PurchaseOrders = dt.Tables[3].AsEnumerable().Select(tbi => new PurchaseOrder
                    {
                        PurchaseOrderID = tbi.Field<int>("PurchaseOrderID"),
                        POAmount = tbi.Field<double?>("POAmount"),
                        RequestID = tbi.Field<int?>("RequestID"),
                        PONote = tbi.Field<string>("PONote"),
                        StatusID = tbi.Field<Int16?>("StatusID"),
                        SupplierID = tbi.Field<int?>("SupplierID"),
                        QuotationID = tbi.Field<int?>("QuotationID"),
                        POPdfURL = tbi.Field<string>("POPdfURL"),
                        IsDeleted = tbi.Field<bool>("IsDeleted"),
                        ModifiedOn = tbi.Field<DateTime?>("ModifiedOn"),
                        CreatedBy = tbi.Field<int?>("CreatedBy"),
                        CreatedOn = tbi.Field<DateTime?>("CreatedOn"),
                        ModifiedBY = tbi.Field<int?>("ModifiedBY"),
                        SupplierName = tbi.Field<string>("SupplierName"),
                        SupplierPhone = tbi.Field<string>("SupplierPhone")

                    }).ToList();

                }
                return companyrequests;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetSingleRequest
        public RequestData GetSingleRequest(int RequestID)
        {
            DataSet dt = new DataSet();
            var imageRequestTask = new List<Image>();
            try
            {
                var requestData = new RequestData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},

                    };

                using (dt = ADOManager.Instance.DataSet("[getSingleRequest]", CommandType.StoredProcedure, sParameter))
                {
                    requestData.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        PlateNo = req.Field<string>("PlateNo"),
                        YearCode = req.Field<int>("YearCode"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        UserName = req.Field<string>("UserName"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("AccidentCreatedOn"),
                        OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        PDFReport = req.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        DemandCount = req.Field<int?>("DemandCount"),
                        QuotationsCount = req.Field<int?>("QuotationsCount"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        WorkshopDetails = req.Field<string>("WorkshopDetails"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        PONote = req.Field<string>("PONote"),
                        IsLowestMatching = req.Field<bool?>("IsLowestMatching"),
                        JCSeriesCode = req.Field<string>("JCSeriesCode"),
                        BodyTypeID = req.Field<Int16?>("BodyTypeID"),
                        EngineTypeID = req.Field<Int16?>("EngineTypeID"),
                        IndividualReturnText = req.Field<string>("IndividualReturnText"),
                        IndividualReturnID = req.Field<int?>("IndividualReturnID"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        POApprovalOn = req.Field<DateTime?>("POApprovalOn"),
                        RequestPdfUrl = req.Field<string>("RequestPdfUrl"),
                        IsInstantPrice = req.Field<bool?>("IsInstantPrice"),
                        IndividualReturnEnglishText = req.Field<string>("IndividualReturnEnglishText"),
                        Depreciation = req.Field<double?>("Depriciation"),
                        PolicyNumber = req.Field<string>("PolicyNumber"),
                        FaultyPolicyNumber = req.Field<string>("FaultyPolicyNumber"),
                        FaultyCompanyNameDropDown = req.Field<string>("FaultyCompanyNameDropDown"),
                        SurveyorName = req.Field<string>("SurveyorName"),
                        SurveyorAppointmentDate = req.Field<DateTime?>("SurveyorAppointmentDate"),
                        HappendOnTime = req.Field<string>("HappendOnTime"),
                        FaultyVehicleMakeName = req.Field<string>("FaultyVehicleEnglishMakeName"),
                        FaultyVehicleModelName = req.Field<string>("FaultyVehicleEnglishModelName"),
                        FaultyVehicleYearCode = req.Field<int?>("FaultyVehicleYearCode"),
                        VehicleCountryName = req.Field<string>("VehicleCountryName"),
                        InvoiceImage = req.Field<string>("InvoiceImage"),
                        CompanyName = req.Field<string>("CompanyName"),
                        VAT = req.Field<int?>("VAT"),
                        LPO = req.Field<string>("LPO"),
                        AccidentIndividualReturnName = req.Field<string>("AccidentIndividualReturnName"),
                        AccidentIndividualReturnPhoneNumber = req.Field<string>("AccidentIndividualReturnPhoneNumber"),
                        AccidentIndividualReturnAddress = req.Field<string>("AccidentIndividualReturnAddress")
                    }).FirstOrDefault();

                    requestData.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        //DesiredManufacturerID = rp.Field<Int16>("DesiredManufacturerID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        DemandID = rp.Field<int?>("DemandID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        //DesiredManufacturerName = rp.Field<string>("ManufacturerName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        PartRejectReason = rp.Field<string>("PartRejectReason"),
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        ItemNumber = rp.Field<string>("ItemNumber"),
                        DamagePointID = rp.Field<string>("DamagePointID"),
                        ApprovedByName = rp.Field<string>("ApprovedByName"),
                        ModifiedOn = rp.Field<DateTime?>("ModifiedOn"),
                        PartStatus = rp.Field<Int16?>("PartStatus"),
                        isExistInAccident = rp.Field<bool?>("isExistInAccident"),
                        DamageName = rp.Field<string>("DamageName"),
                        Name1 = rp.Field<string>("Name1"),
                        FinalCode = rp.Field<string>("FinalCode"),
                        Depriciationvalue = rp.Field<double?>("Depriciationvalue")
                        //RejectReason = rp.Field<string>("RejectReason"),
                        //IsAccepted = rp.Field<bool?>("IsAccepted"),
                        //QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        //DesiredManufacturerRegionName= rp.Field<string>("CountryName"),

                    }).ToList();

                    requestData.RequestedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    requestData.OrderedParts = dt.Tables[3].AsEnumerable().Select(qp => new QuotationPart
                    {
                        QuotationPartID = qp.Field<int>("QuotationPartID"),
                        RequestedPartID = qp.Field<int>("RequestedPartID"),
                        DeliveryTime = qp.Field<byte?>("DeliveryTime"),
                        ConditionTypeID = qp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeName = qp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = qp.Field<string>("NewPartConditionTypeArabicName"),
                        OrderedQuantity = qp.Field<int?>("OrderedQuantity"),
                        AutomotivePartName = qp.Field<string>("PartName"),
                        ConditionTypeName = qp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = qp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = qp.Field<string>("NoteInfo"),
                        RejectReason = qp.Field<string>("RejectReason"),
                        IsAccepted = qp.Field<bool?>("IsAccepted"),
                        IsRecycled = qp.Field<bool?>("IsRecycled"),
                        IsReceived = qp.Field<bool?>("IsReceived"),
                        ReceivedDate = qp.Field<DateTime?>("ReceivedDate"),
                        SupplierName = qp.Field<string>("SupplierName"),
                        AutomotivePartArabicName = qp.Field<string>("PartNameArabic"),
                        ReferredPrice = qp.Field<double?>("ReferredPrice"),
                        OrderRowNumber = qp.Field<int>("OrderRowNumber"),
                        IsReturn = qp.Field<bool?>("IsReturn"),
                        SupplierID = qp.Field<int>("SupplierID"),
                        QuotationID = qp.Field<int>("QuotationID"),
                        IsWsAccepted = qp.Field<bool?>("IsWsAccepted"),
                        POPdfURL = qp.Field<string>("POPdfURL"),
                        PartStatus = qp.Field<Int16?>("PartStatus"),
                        QuotationStatus = qp.Field<Int16?>("QuotationStatus"),
                        WsRejectionNote = qp.Field<string>("WsRejectionNote"),
                        RejectedWorkshopPartReasonID = qp.Field<int>("RejectedWorkshopPartReasonID"),
                        DepriciationPrice = qp.Field<double?>("DepriciationPrice"),
                        Depriciationvalue = qp.Field<double?>("Depriciationvalue"),
                        Price = qp.Field<double?>("Price"),

                    }).ToList();

                    requestData.QuotationPartsImages = dt.Tables[4].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<short>("ObjectTypeID")

                    }).ToList();
                    //requestData.OrderedParts[].RejectedPartImage = dt.Tables[4].AsEnumerable().Select(img => new Image
                    //{
                    //    ImageID = img.Field<int>("ImageID"),
                    //    EncryptedName = img.Field<string>("EncryptedName"),
                    //    OriginalName = img.Field<string>("OriginalName"),
                    //    ImageURL = img.Field<string>("ImageURL"),
                    //    ObjectID = img.Field<int>("ObjectID"),

                    //}).ToList();



                    requestData.AccidentMarkers = dt.Tables[5].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();

                    requestData.RequestTasks = dt.Tables[6].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskRejectReason = rt.Field<string>("TaskRejectReason"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),
                        LabourPriceWithoutDiscount = rt.Field<double?>("LabourPriceWithoutDiscount"),
                        EditTaskReason = rt.Field<string>("EditTaskReason"),
                        OldLabourPrice = rt.Field<double?>("OldLabourPrice"),
                        OldLabourPriceWithoutDiscount = rt.Field<double?>("OldLabourPriceWithoutDiscount")


                    }).ToList();

                    requestData.Notes = dt.Tables[7].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),
                        CreatedBy = nt.Field<int>("CreatedBy"),


                    }).ToList();

                    requestData.AccidentImages = dt.Tables[8].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                    }).ToList();

                    requestData.POApprovalEmployees = dt.Tables[9].AsEnumerable().Select(po => new POApproval
                    {
                        POApprovalID = po.Field<int>("POApprovalID"),
                        RequestID = po.Field<int>("RequestID"),
                        UserID = po.Field<int>("UserID"),
                        EmployeeID = po.Field<int>("EmployeeID"),
                        LastName = po.Field<string>("LastName"),
                        FirstName = po.Field<string>("FirstName"),
                        Email = po.Field<string>("Email"),
                        IsApproved = po.Field<bool?>("IsApproved"),
                        CreatedOn = po.Field<DateTime>("CreatedOn"),
                        ModifiedOn = po.Field<DateTime?>("ModifiedOn"),
                        ESignatureURL = po.Field<string>("ESignatureURL")

                    }).ToList();

                    requestData.SurveyorsSignatures = dt.Tables[10].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),

                    }).ToList();

                    requestData.PartsApprovedBySignatures = dt.Tables[11].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        CreatedOn = rp.Field<DateTime?>("CreatedOn"),

                    }).ToList();

                    requestData.TasksApprovedBySignatures = dt.Tables[12].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        CreatedOn = rp.Field<DateTime?>("CreatedOn"),

                    }).ToList();

                    requestData.Suppliers = dt.Tables[13].AsEnumerable().Select(rp => new PurchaseOrder
                    {
                        SupplierID = rp.Field<int?>("SupplierID"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        POAmount = rp.Field<double?>("POAmount"),
                        DiscountValue = rp.Field<double?>("DiscountValue"),
                    }).ToList();

                    requestData.POApprovedSignatures = dt.Tables[14].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("Name"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        CreatedOn = rp.Field<DateTime?>("CreatedOn"),

                    }).ToList();
                    requestData.CarReadyImages = dt.Tables[15].AsEnumerable().Select(cmp => new Image
                    {
                        ImageID = cmp.Field<int?>("ImageID"),
                        ImageURL = cmp.Field<string>("ImageURL"),
                        ObjectID = cmp.Field<int?>("ObjectID"),
                        ObjectTypeID = cmp.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = cmp.Field<string>("EncryptedName"),
                        RequestTaskId = cmp.Field<int?>("RequestTaskID"),
                        IsDeleted = cmp.Field<bool>("IsDeleted")



                    }).ToList();
                    requestData.PDFDetail = dt.Tables[16].AsEnumerable().Select(pd => new PDFDetail
                    {
                      CompanyID = pd.Field<int>("CompanyID"),
                      ObjectTypeID = pd.Field<int>("ObjectTypeID"),
                      SectionOne = pd.Field<string>("SectionOne"),
                      SectionTwo = pd.Field<string>("SectionTwo"),
                      SectionThree = pd.Field<string>("SectionThree"),
                      SectionFour = pd.Field<string>("SectionFour")

                    }).ToList();
                }

                return requestData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region DeletePartsRequest
        public string DeletePartsRequest(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[deletePartsRequest]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Request deleted successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[deletePartsRequest]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetCompanyAccidents
        public AccidentMetaData GetCompanyAccidents(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
   int? ModelID, int? YearID, string SearchQuery, int? TechnicalNotesStatusID, int? UserID, int? ICWorkshopID,int? SurveyorID)
        {
            DataSet dt = new DataSet();
            try
            {
                var accidentData = new AccidentMetaData();
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                    new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                    new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                    new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                    new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                    new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                    new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                    new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                    new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery},
                    new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID},
                    new SqlParameter { ParameterName = "@TechnicalNotesStatus" , Value = TechnicalNotesStatusID},
                    new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                    new SqlParameter { ParameterName = "@ICWorkshopID" , Value = ICWorkshopID},
                    new SqlParameter { ParameterName = "@SurveyorID" , Value = SurveyorID}

                };

                using (dt = ADOManager.Instance.DataSet("[getCompanyAccidents]", CommandType.StoredProcedure, sParameter))
                {
                    accidentData.Accidents = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {
                        AccidentID = acd.Field<int>("AccidentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        ICName = acd.Field<string>("Name"),
                        CPPhone = acd.Field<string>("CPPhone"),
                        CPName = acd.Field<string>("CPFirstName") + " " + acd.Field<string>("CPLastName"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        MakeName = acd.Field<string>("EnglishMakeName"),
                        ArabicMakeName = acd.Field<string>("ArabicMakeName"),
                        ModelCode = acd.Field<string>("EnglishModelName"),
                        YearCode = acd.Field<int>("YearCode"),
                        CreatedOn = acd.Field<DateTime>("CreatedOn"),
                        CreatedSince = acd.Field<string>("CreatedSince"),
                        ArabicModelName = acd.Field<string>("ArabicModelName"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        ImgURL = acd.Field<string>("ImgURL"),
                        WorkshopName = acd.Field<string>("WorkshopName"),
                        WorkshopCityName = acd.Field<string>("WorkshopCityName"),
                        WorkshopPhone = acd.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = acd.Field<string>("WorkshopAreaName"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        StatusID = acd.Field<Int16?>("StatusID"),
                        UserName = acd.Field<string>("UserName"),
                        IsDeleted = acd.Field<bool>("IsDeleted"),
                        InprogressRequestCount = acd.Field<int?>("InprogressRequestCount"),
                        TotalRequestCount = acd.Field<int?>("TotalRequestCount"),
                        ClearanceRoute = acd.Field<string>("ClearanceRoute"),
                        CloseReason = acd.Field<string>("CloseReason"),
                        JCSeriesID = acd.Field<string>("JCSeriesCode"),
                        TechnicalNotesStatus = acd.Field<Int16?>("TechnicalNotesStatus"),
                        RequestStatusName = acd.Field<string>("RequestStatusName"),
                        RequestArabicStatusName = acd.Field<string>("RequestArabicStatusName"),
                        ClearanceSummaryPdfUrl = acd.Field<string>("ClearanceSummaryPdfUrl"),
                        ClosedByName = acd.Field<string>("ClosedByName"),
                        BrokerName = acd.Field<string>("BrokerName"),
                        isHighPriority = acd.Field<bool?>("isHighPriority"),
                        isLearningSelected = acd.Field<bool?>("isLearningSelected"),
                        SurveyorID = acd.Field<int?>("SurveyorID"),
                        SurveyorName = acd.Field<string>("SurveyorName"),
                        SurveyorAppointmentDate = acd.Field<DateTime?>("SurveyorAppointmentDate"),
                        PolicyNumber = acd.Field<string>("PolicyNumber"),
                        DeductibleStatus = acd.Field<int?>("DeductibleStatus"),
                    }).ToList();

                    //accidentData.Makes = dt.Tables[1].AsEnumerable().Select(make => new Make
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

                    //accidentData.Models = dt.Tables[2].AsEnumerable().Select(model => new Model
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

                    //accidentData.Years = dt.Tables[3].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    accidentData.TabInfoData = dt.Tables[1].AsEnumerable().Select(tbi => new TabInfo
                    {
                        OpenedAccidents = tbi.Field<int>("OpenedAccidents"),
                        ClosedAccidents = tbi.Field<int>("ClosedAccidents"),
                        DeletedAccidents = tbi.Field<int>("DeletedAccidents"),
                        //AccidentDraftCount = tbi.Field<int>("AccidentDraftCount")

                    }).FirstOrDefault();

                    if (CompanyID == 26)
                    {
                        accidentData.TRApprovalEmployees = dt.Tables[2].AsEnumerable().Select(tr => new TRApproval
                        {
                            FirstName = tr.Field<string>("FirstName"),
                            LastName = tr.Field<string>("LastName"),
                            IsApproved = tr.Field<bool?>("IsApproved"),
                            AccidentNo = tr.Field<string>("AccidentNo"),
                            TRApprovalID = tr.Field<int>("TRApprovalID"),
                            TRObjectType = tr.Field<int>("TRObjectType")
                        }).ToList();

                        accidentData.Users = dt.Tables[3].AsEnumerable().Select(req => new Employee
                        {
                            EmployeeID = req.Field<int>("EmployeeID"),
                            CompanyID = req.Field<int>("CompanyID"),
                            RoleID = req.Field<byte>("RoleID"),
                            RoleName = req.Field<string>("Name"),
                            RoleIcon = req.Field<string>("Icon"),
                            //WorkshopName = req.Field<string>("WorkshopName"),
                            //ICWorkshopID = req.Field<int?>("ICWorkshopID"),
                            UserID = req.Field<int>("UserID"),
                            FirstName = req.Field<string>("FirstName"),
                            LastName = req.Field<string>("LastName"),
                            Email = req.Field<string>("Email"),
                            PhoneNumber = req.Field<string>("Phone"),
                            ProfileImageURL = req.Field<string>("ProfileImageURL"),
                            ImgURL = req.Field<string>("ImgURL"),
                            RoleNameArabic = req.Field<string>("RoleNameArabic"),
                            EmployeeName = req.Field<string>("EmployeeName")


                        }).ToList();
                    }

                }
                return accidentData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetSingleAccident
        public AccidentData GetSingleAccident(int AccidentID)
        {
            DataSet dt = new DataSet();
            System.Net.WebClient myWebClient = new System.Net.WebClient();
            try
            {
                var accidentData = new AccidentData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getSingleAccident]", CommandType.StoredProcedure, sParameter))
                {
                    accidentData.Accident = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {

                        DraftID = acd.Field<int?>("DraftID"),
                        AccidentID = acd.Field<int>("AccidentID"),
                        ICWorkshopID = acd.Field<int?>("ICWorkshopID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        GroupName = acd.Field<string>("GroupName"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        YearCode = acd.Field<int>("YearCode"),
                        MakeName = acd.Field<string>("EnglishMakeName"),
                        ArabicMakeName = acd.Field<string>("ArabicMakeName"),
                        ModelCode = acd.Field<string>("EnglishModelName"),
                        ArabicModelName = acd.Field<string>("ArabicModelName"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        OwnerIDFront = acd.Field<string>("OwnerIDFront"),
                        OwnerIDBack = acd.Field<string>("OwnerIDBack"),
                        CarLicenseBack = acd.Field<string>("CarLicenseBack"),
                        CarLicenseFront = acd.Field<string>("CarLicenseFront"),
                        PoliceReport = acd.Field<string>("PoliceReport"),
                        AccidentTypeID = acd.Field<Int16>("AccidentTypeID"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        ResponsibilityTypeID = acd.Field<int>("ResponsibilityTypeID"),
                        ResponsibilityTypeName = acd.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = acd.Field<string>("ArabicResponsibilityTypeName"),
                        StatusID = acd.Field<Int16?>("StatusID"),
                        StatusName = acd.Field<string>("StatusName"),
                        ArabicStatusName = acd.Field<string>("ArabicStatusName"),
                        UserName = acd.Field<string>("UserName"),
                        WorkshopName = acd.Field<string>("WorkshopName"),
                        PDFReport = acd.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = acd.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = acd.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = acd.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = acd.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = acd.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = acd.Field<string>("PDFReportDescription"),
                        CarsInvolved = acd.Field<int?>("CarsInvolved"),
                        AccidentHappendOn = acd.Field<DateTime?>("AccidentHappendOn"),
                        IsDeleted = acd.Field<bool>("IsDeleted"),
                        EngineTypeID = acd.Field<Int16?>("EngineTypeID"),
                        BodyTypeID = acd.Field<Int16?>("BodyTypeID"),
                        ImportantNote = acd.Field<string>("ImportantNote"),
                        EngineTypeName = acd.Field<string>("EngineTypeName"),
                        EngineTypeArabicName = acd.Field<string>("EngineTypeArabicName"),
                        BodyTypeName = acd.Field<string>("BodyTypeName"),
                        BodyTypeArabicName = acd.Field<string>("BodyTypeArabicName"),
                        FaultyCompanyName = acd.Field<string>("FaultyCompanyName"),
                        TotalRequestCount = acd.Field<int?>("TotalRequestCount"),
                        DemandCount = acd.Field<int?>("DemandCount"),
                        OwnerPhoneNo = acd.Field<string>("OwnerPhoneNo"),
                        IsPurchasing = acd.Field<bool?>("IsPurchasing"),
                        WorkshopDetails = acd.Field<string>("WorkshopDetails"),
                        IndividualReturnID = acd.Field<int?>("IndividualReturnID"),
                        IndividualReturnText = acd.Field<string>("IndividualReturnText"),
                        BrokerName = acd.Field<string>("BrokerName"),
                        IndividualReturnEnglishText = acd.Field<string>("IndividualReturnEnglishText"),
                        isHighPriority=acd.Field<bool?>("isHighPriority"),
                        isLearningSelected=acd.Field<bool?>("isLearningSelected"),
                        SurveyorID = acd.Field<int?>("SurveyorID"),
                        SurveyorName = acd.Field<string>("SurveyorName"),
                        SurveyorAppointmentDate = acd.Field<DateTime?>("SurveyorAppointmentDate"),
                        PolicyNumber = acd.Field<string>("PolicyNumber"),
                        MarketInsureValue = acd.Field<int?>("MarketInsureValue"),
                        SalvageAmount = acd.Field<int?>("SalvageAmount"),
                        TotalLossOfferAmount = acd.Field<int?>("TotalLossOfferAmount"),
                        isTotalLossSelected  = acd.Field<bool?>("isTotalLossSelected"),
                        //isSalvageSelected  = acd.Field<bool?>("isSalvageSelected"),
                        IsAgencyAccident = acd.Field<bool?>("IsAgencyAccident"),
                        IsAgencyRepair = acd.Field<int?>("IsAgencyRepair"),
                        FaultyPolicyNumber = acd.Field<string>("FaultyPolicyNumber"),
                        AgencyID = acd.Field<int?>("AgencyID"),
                        FaultyCompanyID = acd.Field<int?>("FaultyCompanyID"),
                        FaultyCompanyNameDropDown = acd.Field<string>("FaultyCompanyNameDropDown"),
                        FaultyVehiclePlateNo = acd.Field<string>("FaultyVehiclePlateNo"),
                        FaultyVehicleMakeID = acd.Field<int?>("FaultyVehicleMakeID"),
                        FaultyVehicleModelID = acd.Field<int?>("FaultyVehicleModelID"),
                        FaultyVehicleYearID = acd.Field<int?>("FaultyVehicleYearID"),
                        VehicleCountryID = acd.Field<int?>("VehicleCountryID"),
                        AccidentHappendOnTime = acd.Field<TimeSpan?>("AccidentHappendOnTime"),
                        FaultyVehicleMakeName = acd.Field<string>("FaultyVehicleEnglishMakeName"),
                        FaultyVehicleModelName = acd.Field<string>("FaultyVehicleEnglishModelName"),
                        FaultyVehicleYearCode = acd.Field<int?>("FaultyVehicleYearCode"),
                        VehicleCountryName = acd.Field<string>("VehicleCountryName"),
                        IsReplacementCar = acd.Field<bool?>("IsReplacementCar"),
                        ReplacementCarID = acd.Field<int?>("ReplacementCarID"),
                        ReplacementCarName = acd.Field<string>("ReplacementCarName"),
                        HappendOnTime = acd.Field<string>("HappendOnTime"),
                        IsDeductible = acd.Field<bool?>("IsDeductible"),
                        DeductibleStatus = acd.Field<int?>("DeductibleStatus"),
                        DeductibleAmount = acd.Field<int?>("DeductibleAmount"),
                        IsSumInsured = acd.Field<bool?>("IsSumInsured"),
                        SumInsuredAmount = acd.Field<int?>("SumInsuredAmount"),
                        GeographicalExtension = acd.Field<string>("GeographicalExtension"),
                        AOGCover = acd.Field<bool?>("AOGCover"),
                        SRCCCover = acd.Field<bool?>("SRCCCover"),
                        IsWindshieldCover = acd.Field<bool?>("IsWindshieldCover"),
                        WindshieldCoverAmount = acd.Field<int?>("WindshieldCoverAmount"),
                        FaultyVehicleCountryID = acd.Field<int?>("FaultyVehicleCountryID"),
                        FaultyVehicleCountryName = acd.Field<string>("FaultyVehicleCountryName"),
                        AccidentIndividualReturnName = acd.Field<string>("AccidentIndividualReturnName"),
                        AccidentIndividualReturnPhoneNumber = acd.Field<string>("AccidentIndividualReturnPhoneNumber"),
                        AccidentIndividualReturnAddress = acd.Field<string>("AccidentIndividualReturnAddress")
                    }).FirstOrDefault();

                    accidentData.Notes = dt.Tables[1].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        CreatedByName = nt.Field<string>("CreatedByName"),
                        ModifiedByName = nt.Field<string>("ModifiedByName"),
                        LastModifiedOn = nt.Field<DateTime?>("LastModifiedOn"),
                        IsPublic = nt.Field<bool?>("IsPublic"),

                    }).ToList();

                    accidentData.AccidentImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        Description = img.Field<string>("Description"),
                        IsDocument = img.Field<bool?>("IsDocument"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                    }).ToList();

                    accidentData.AccidentMarkers = dt.Tables[3].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();

                    accidentData.AccidentParts = dt.Tables[4].AsEnumerable().Select(rp => new AccidentPart
                    {
                        AccidentPartID = rp.Field<int>("AccidentPartID"),
                        AccidentID = rp.Field<int>("AccidentID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsRequestCreated = rp.Field<bool?>("IsRequestCreated"),

                    }).ToList();

                    accidentData.AccidentPartsImages = dt.Tables[5].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();
                    accidentData.TechnicalNotes = dt.Tables[6].AsEnumerable().Select(tnote => new TechnicalNotes
                    {
                        TechnicalNoteID = tnote.Field<int>("TechnicalNoteID"),
                        AccidentNo = tnote.Field<string>("AccidentNo"),
                        ReportSubject = tnote.Field<string>("ReportSubject"),
                        FromEmployee = tnote.Field<string>("FromEmployee"),
                        ContractNumber = tnote.Field<string>("ContractNumber"),
                        DebitAccount = tnote.Field<double?>("DebitAccount"),
                        CreditBalance = tnote.Field<double?>("CreditBalance"),
                        ToEmployee = tnote.Field<string>("ToEmployee"),
                        InsuredAmount = tnote.Field<double?>("InsuredAmount"),
                        Repair = tnote.Field<bool?>("Repair"),
                        PrevioutAccidentRecord = tnote.Field<string>("PrevioutAccidentRecord"),
                        Premium = tnote.Field<double?>("Premium"),
                        Compensation = tnote.Field<double?>("Compensation"),
                        InsuredResult = tnote.Field<string>("InsuredResult"),
                        AccidentResponsibility = tnote.Field<int>("AccidentResponsibility"),
                        KrokiSketch = tnote.Field<string>("KrokiSketch"),
                        PoliceReport = tnote.Field<string>("PoliceReport"),
                        Cause = tnote.Field<string>("Cause"),
                        IVDriverFault = tnote.Field<string>("IVDriverFault"),
                        TPLDriverFault = tnote.Field<string>("TPLDriverFault"),
                        ReverseCase = tnote.Field<string>("ReverseCase"),
                        PreviousAccident = tnote.Field<bool?>("PreviousAccident"),
                        PreliminaryAgreementInsured = tnote.Field<string>("PreliminaryAgreementInsured"),
                        DisagreementPoints = tnote.Field<string>("DisagreementPoints"),
                        ProposalSolution = tnote.Field<string>("ProposalSolution"),
                        LossAdjusterName = tnote.Field<string>("LossAdjusterName"),
                        LossAdjusterReportResult = tnote.Field<string>("LossAdjusterReportResult"),
                        TechnicalExplanation = tnote.Field<string>("TechnicalExplanation"),
                        CompensationProposal = tnote.Field<string>("CompensationProposal"),
                        Recovery = tnote.Field<double?>("Recovery"),
                        PaidCompensation = tnote.Field<double?>("PaidCompensation"),
                        FullPeriodPremium = tnote.Field<double?>("FullPeriodPremium"),
                        LossAdjusterReport = tnote.Field<double?>("LossAdjusterReport"),
                        CompensationCost = tnote.Field<double?>("CompensationCost"),
                        MarketValue = tnote.Field<double?>("MarketValue"),
                        ApproxSalvageAmount = tnote.Field<double?>("ApproxSalvageAmount"),
                        CompensationAmount = tnote.Field<double?>("CompensationAmount"),
                        SalvageBestAmount = tnote.Field<double?>("SalvageBestAmount"),
                        NetLoss = tnote.Field<double?>("NetLoss"),
                        InjuredName = tnote.Field<string>("InjuredName"),
                        InjuredAge = tnote.Field<int?>("InjuredAge"),
                        InjuredLevel = tnote.Field<string>("InjuredLevel"),
                        LegalOpenion = tnote.Field<string>("LegalOpenion"),
                        MedicalOpenion = tnote.Field<string>("MedicalOpenion"),
                        MedicalExplanation = tnote.Field<string>("MedicalExplanation"),
                        TechnicalProcedure = tnote.Field<string>("TechnicalProcedure"),
                        TreatmentExpense = tnote.Field<double?>("TreatmentExpense"),
                        DisabilityCompensation = tnote.Field<double?>("DisabilityCompensation"),
                        FutureOperation = tnote.Field<double?>("FutureOperation"),
                        FinalMedicalReportUrl = tnote.Field<string>("FinalMedicalReportUrl"),
                        PenaltyRulingDecisionUrl = tnote.Field<string>("PenaltyRulingDecisionUrl"),
                        RegionalCommitteUrl = tnote.Field<string>("RegionalCommitteUrl"),
                        AssignLossAdjuster = tnote.Field<bool?>("AssignLossAdjuster"),
                        Note = tnote.Field<string>("Note"),
                        ToDate = tnote.Field<DateTime?>("ToDate"),
                        FromDate = tnote.Field<DateTime?>("FromDate"),
                        ATCElabour = tnote.Field<double?>("ATCElabour"),
                        ATCEsparePart = tnote.Field<double?>("ATCEsparePart"),
                        ATCEValueLoss = tnote.Field<double?>("ATCEValueLoss"),
                        ATCETortCompensation = tnote.Field<double?>("ATCETortCompensation"),
                        ATElabour = tnote.Field<double?>("ATElabour"),
                        ATEsparePart = tnote.Field<double?>("ATEsparePart"),
                        ATEValueLoss = tnote.Field<double?>("ATEValueLoss"),
                        ATETortCompensation = tnote.Field<double?>("ATETortCompensation"),
                    }).FirstOrDefault();



                    accidentData.customerRequests = dt.Tables[7].AsEnumerable().Select(cr => new AIInspectionRequest
                    {
                        CustomerRequestID = cr.Field<int?>("CustomerRequestID"),
                        AccidentNo = cr.Field<string>("AccidentNo"),
                        ServiceID = cr.Field<int?>("ServiceID"),
                        VIN = cr.Field<string>("VIN"),
                        VehicleOwnerName = cr.Field<string>("OwnerPhoneNo"),
                        CreatedOn = cr.Field<DateTime?>("CreatedOn"),
                        PlateNo = cr.Field<string>("PlateNo"),


                    }).ToList();
                }
                return accidentData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region DeleteAccident
        public string DeleteAccident(int AccidentID, int userID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = userID}
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[deleteAccident]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Accident deleted successfully" : "Unable to delete accident due to active request";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetICRequestQuotationData
        public DemandQuotations GetICRequestQuotationData(QuotationFilterModel model)
        {
            DataSet dt = new DataSet();
            try
            {
                DemandQuotations QuotationsFilter = new DemandQuotations();


                var XMLRequestedParts = model.RequestedParts != null && model.RequestedParts.Count() > 0 ? model.RequestedParts.ToXML("ArrayOfRequestedParts") : null;
                //var XMLManufacturers = model.PartManufacturers != null && model.PartManufacturers.Count() > 0 ? model.PartManufacturers.ToXML("ArrayOfManufacturers") : null;
                //var XMLManufacturerRegions = model.ManufacturerRegions != null && model.ManufacturerRegions.Count() > 0 ? model.ManufacturerRegions.ToXML("ArrayOfCountries") : null;
                var XMLCities = model.Cities != null && model.Cities.Count() > 0 ? model.Cities.ToXML("ArrayOfCities") : null;

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = model.RequestedParts.Count > 0 ? XMLRequestedParts : null},
                        new SqlParameter { ParameterName = "@XMLCities" , Value = model.Cities!=null && model.Cities.Count > 0 ? XMLCities : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturers" , Value =  model.PartManufacturers.Count > 0 ? XMLManufacturers : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturerRegions" , Value =  model.ManufacturerRegions.Count > 0 ? XMLManufacturerRegions : null},
                        new SqlParameter { ParameterName = "@RequestID" , Value = model.RequestID },
                        //new SqlParameter { ParameterName = "@IsReferred" , Value = model.IsReferred },
                        new SqlParameter { ParameterName = "@Availability", Value = model.Availability},
                        new SqlParameter { ParameterName = "@ConditionTypeID" , Value = model.ConditionTypeID },
                        new SqlParameter { ParameterName = "@NewConditionTypeID" , Value = model.NewConditionTypeID },
                        new SqlParameter { ParameterName = "@MinPrice" , Value = model.MinPrice },
                        new SqlParameter { ParameterName = "@MaxPrice" , Value = model.MaxPrice },
                        new SqlParameter { ParameterName = "@SortByPrice" , Value = model.SortByPrice },
                        new SqlParameter { ParameterName = "@SortByRating" , Value = model.SortByRating },
                        new SqlParameter { ParameterName = "@SortByFillingRate" , Value = model.SortByFillingRate },
                        new SqlParameter { ParameterName = "@AreaName" , Value = model.AreaName },
                        new SqlParameter { ParameterName = "@IsPaid" , Value = model.IsPaid },
                        new SqlParameter { ParameterName = "@Price" , Value = model.Price },
                        new SqlParameter { ParameterName = "@MinFillingRate" , Value = model.MinFillingRate },
                        new SqlParameter { ParameterName = "@MaxFillingRate" , Value = model.MaxFillingRate },
                        new SqlParameter { ParameterName = "@Rating" , Value = model.Rating },

                };

                using (dt = ADOManager.Instance.DataSet("[getICRequestQuotationData]", CommandType.StoredProcedure, sParameter))
                {
                    QuotationsFilter.RequestInfo = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        PlateNo = req.Field<string>("PlateNo"),
                        YearCode = req.Field<int>("YearCode"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        UserName = req.Field<string>("UserName"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("CreatedOnAccident"),
                        OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        PDFReport = req.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        DemandCount = req.Field<int?>("DemandCount"),
                        QuotationsCount = req.Field<int?>("QuotationsCount"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),

                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        CPName = req.Field<string>("CPName"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        CountryName = req.Field<string>("CountryName"),
                        CityName = req.Field<string>("CityName"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        IcCancelOrderNote = req.Field<string>("IcCancelOrderNote"),
                        JoCancelOrderNote = req.Field<string>("JoCancelOrderNote"),
                        DemandCreatedOn = req.Field<DateTime?>("DemandCreatedOn"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        NotPurchasingLMOReason = req.Field<string>("NotPurchasingLMOReason")
                    }).FirstOrDefault();

                    QuotationsFilter.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int>("Quantity"),
                        DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
                        DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        DemandedQuantity = rp.Field<int>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        DesiredManufacturerRegionID = rp.Field<Int16?>("DesiredManufacturerRegionID"),
                        DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        //IsAccepted = rp.Field<bool?>("IsAccepted"),
                    }).ToList();

                    QuotationsFilter.Quotations = dt.Tables[2].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        StatusID = qt.Field<Int16?>("StatusID"),
                        StatusName = qt.Field<string>("StatusName"),
                        ArabicStatusName = qt.Field<string>("ArabicStatusName"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        CreatedSince = qt.Field<string>("CreatedSince"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings")

                    }).ToList();

                    QuotationsFilter.QuotationPartRef = dt.Tables[3].AsEnumerable().Select(rp => new QuotationPartRef
                    {
                        QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        QuotationID = rp.Field<int?>("QuotationID"),
                        RequestedPartID = rp.Field<int?>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int?>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        //ManufacturerID = rp.Field<Int16?>("ManufacturerID"),
                        //ManufacturerName = rp.Field<string>("ManufacturerName"),
                        Price = rp.Field<double?>("Price"),
                        Quantity = rp.Field<int?>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        CityID = rp.Field<int?>("CityID"),
                        CityName = rp.Field<string>("CityName"),
                        BranchAreaName = rp.Field<string>("BranchAreaName"),
                        //ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID"),
                        //ManufacturerRegionName = rp.Field<string>("ManufacturerRegionName"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        WillDeliver = rp.Field<bool>("WillDeliver"),
                        DeliveryCost = rp.Field<decimal?>("DeliveryCost"),
                        OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                        IsAccepted = rp.Field<bool?>("IsAccepted"),
                        RowNumber = rp.Field<int>("RowNumber"),
                        OrderedOn = rp.Field<DateTime?>("OrderedOn"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        AcceptedByName = rp.Field<string>("AcceptedByName"),
                        WorkshopName = rp.Field<string>("WorkshopName"),
                        FillingRate = rp.Field<decimal?>("FillingRate"),
                        RequestPartCount = rp.Field<int?>("RequestPartCount"),
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                        IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                        ItemRank = rp.Field<int>("ItemRank"),
                        IsPartialSellings = rp.Field<bool?>("IsPartialSellings"),
                        IsReturn = rp.Field<bool?>("IsReturn"),
                        PreviousPrice = rp.Field<double?>("PreviousPrice"),
                        DepriciationPrice = rp.Field<double?>("DepriciationPrice"),
                        Depriciationvalue = rp.Field<double?>("Depriciationvalue")


                    }).ToList();

                    QuotationsFilter.QuotationPartsImages = dt.Tables[4].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID")

                    }).ToList();

                    //QuotationsFilter.PartManufacturers = dt.Tables[5].AsEnumerable().Select(pm => new PartManufacturer
                    //{
                    //    PartManufacturerID = pm.Field<short>("ManufacturerID"),
                    //    ManufacturerName = pm.Field<string>("ManufacturerName"),
                    //    TotalQuotations = pm.Field<int>("TotalQuotations"),
                    //    IsSelected = pm.Field<bool?>("IsSelected"),

                    //}).ToList();

                    //QuotationsFilter.ManufacturerRegions = dt.Tables[6].AsEnumerable().Select(pm => new ManufacturerRegion
                    //{
                    //    ManufacturerRegionID = pm.Field<short>("ManufacturerRegionID"),
                    //    ManufacturerRegionName = pm.Field<string>("ManufacturerRegionName"),
                    //    TotalQuotations = pm.Field<int>("TotalQuotations"),
                    //    IsSelected = pm.Field<bool?>("IsSelected"),

                    //}).ToList();

                    QuotationsFilter.Cities = dt.Tables[5].AsEnumerable().Select(cty => new City
                    {
                        CityID = cty.Field<int>("CityID"),
                        CityName = cty.Field<string>("CityName"),
                        TotalQuotations = cty.Field<int>("TotalQuotations"),
                        IsSelected = cty.Field<bool?>("IsSelected"),
                        CityNameArabic = cty.Field<string>("CityNameArabic")

                    }).ToList();

                    QuotationsFilter.RequestedPartsImages = dt.Tables[6].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    QuotationsFilter.ReferredSupplierQuotations = dt.Tables[7].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        RequestID = qt.Field<int>("RequestID"),
                        CPEmail = qt.Field<string>("CPEmail"),
                        CPPhone = qt.Field<string>("CPPhone"),
                        Rating = qt.Field<byte>("Rating"),
                        JoReviewNote = qt.Field<string>("JoReviewNote"),
                        JoReviewStatusID = qt.Field<Int16?>("JoReviewStatusID"),
                        RequestPartCount = qt.Field<int?>("RequestPartCount"),
                        ReferredPartsCount = qt.Field<int?>("ReferredPartsCount"),
                        BranchAreaName = qt.Field<string>("BranchAreaName"),
                        BranchName = qt.Field<string>("BranchName"),
                        AddressLine1 = qt.Field<string>("AddressLine1"),
                        AddressLine2 = qt.Field<string>("AddressLine2"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        IsDiscountAvailable = qt.Field<bool?>("IsDiscountAvailable"),
                        IsNotAvailable = qt.Field<bool?>("IsNotAvailable"),
                        DiscountValue = qt.Field<double?>("DiscountValue"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        PaymentTypeID = qt.Field<Int16?>("PaymentTypeID"),
                        SuggestedPrice = qt.Field<double?>("SuggestedPrice"),
                        IsSuggestionAccepted = qt.Field<bool?>("IsSuggestionAccepted"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        MatchingFillingRate = qt.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = qt.Field<double?>("LowestOfferMatchingPrice"),
                        MatchingOfferSortNo = qt.Field<int>("MatchingOfferSortNo"),
                        OfferSortNo = qt.Field<int>("OfferSortNo"),
                        StatusID = qt.Field<short?>("StatusID"),
                        Comment = qt.Field<string>("Comment"),
                        SupplierType = qt.Field<Int16?>("SupplierType"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
                        DemandID = qt.Field<int>("DemandID"),
                        IsPrioritySupplier = qt.Field<bool?>("IsPrioritySupplier"),
                    }).ToList();

                    QuotationsFilter.AccidentMarkers = dt.Tables[8].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();

                    QuotationsFilter.RequestTasks = dt.Tables[9].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskRejectReason = rt.Field<string>("TaskRejectReason"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),

                    }).ToList();

                    QuotationsFilter.Notes = dt.Tables[10].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),

                    }).ToList();

                    QuotationsFilter.AccidentImages = dt.Tables[11].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                    }).ToList();
                    QuotationsFilter.RejectedSupplierParts = dt.Tables[12].AsEnumerable().Select(rp => new QuotationPartRef
                    {
                        QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        QuotationID = rp.Field<int?>("QuotationID"),
                        RequestedPartID = rp.Field<int?>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int?>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        //ManufacturerID = rp.Field<Int16?>("ManufacturerID"),
                        //ManufacturerName = rp.Field<string>("ManufacturerName"),
                        Price = rp.Field<double?>("Price"),
                        Quantity = rp.Field<int?>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        CityID = rp.Field<int?>("CityID"),
                        CityName = rp.Field<string>("CityName"),
                        BranchAreaName = rp.Field<string>("BranchAreaName"),
                        //ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID"),
                        //ManufacturerRegionName = rp.Field<string>("ManufacturerRegionName"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        WillDeliver = rp.Field<bool>("WillDeliver"),
                        DeliveryCost = rp.Field<decimal?>("DeliveryCost"),
                        OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                        IsAccepted = rp.Field<bool?>("IsAccepted"),
                        RowNumber = rp.Field<int>("RowNumber"),
                        OrderedOn = rp.Field<DateTime?>("OrderedOn"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        AcceptedByName = rp.Field<string>("AcceptedByName"),
                        WorkshopName = rp.Field<string>("WorkshopName"),
                        FillingRate = rp.Field<decimal?>("FillingRate"),
                        RequestPartCount = rp.Field<int?>("RequestPartCount"),
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                        IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                        ItemRank = rp.Field<int>("ItemRank"),
                        IsPartialSellings = rp.Field<bool?>("IsPartialSellings"),
                        IsReturn = rp.Field<bool?>("IsReturn"),
                        PreviousPrice = rp.Field<double?>("PreviousPrice"),
                    }).ToList();
                    QuotationsFilter.RejectedSupplierOffers = dt.Tables[13].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        RequestID = qt.Field<int>("RequestID"),
                        CPEmail = qt.Field<string>("CPEmail"),
                        CPPhone = qt.Field<string>("CPPhone"),
                        Rating = qt.Field<byte>("Rating"),
                        JoReviewNote = qt.Field<string>("JoReviewNote"),
                        JoReviewStatusID = qt.Field<Int16?>("JoReviewStatusID"),
                        RequestPartCount = qt.Field<int?>("RequestPartCount"),
                        ReferredPartsCount = qt.Field<int?>("ReferredPartsCount"),
                        BranchAreaName = qt.Field<string>("BranchAreaName"),
                        BranchName = qt.Field<string>("BranchName"),
                        AddressLine1 = qt.Field<string>("AddressLine1"),
                        AddressLine2 = qt.Field<string>("AddressLine2"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        IsDiscountAvailable = qt.Field<bool?>("IsDiscountAvailable"),
                        IsNotAvailable = qt.Field<bool?>("IsNotAvailable"),
                        DiscountValue = qt.Field<double?>("DiscountValue"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        PaymentTypeID = qt.Field<Int16?>("PaymentTypeID"),
                        SuggestedPrice = qt.Field<double?>("SuggestedPrice"),
                        IsSuggestionAccepted = qt.Field<bool?>("IsSuggestionAccepted"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        MatchingFillingRate = qt.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = qt.Field<double?>("LowestOfferMatchingPrice"),
                        MatchingOfferSortNo = qt.Field<int>("MatchingOfferSortNo"),
                        OfferSortNo = qt.Field<int>("OfferSortNo"),
                        StatusID = qt.Field<short?>("StatusID"),
                        Comment = qt.Field<string>("Comment"),
                        SupplierType = qt.Field<Int16?>("SupplierType"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
                        DemandID = qt.Field<int>("DemandID"),
                    }).ToList();
                    QuotationsFilter.ReviewNotes = dt.Tables[14].AsEnumerable().Select(reviewnotes => new Quotation
                    {
                        QuotationID = reviewnotes.Field<int>("QuotationID"),
                        JoReviewNote = reviewnotes.Field<string>("JoReviewNote"),
                        ModifiedOn = reviewnotes.Field<DateTime>("ModifiedOn"),
                        ModifiedBy = reviewnotes.Field<int>("ModifiedBy"),
                        ModifiedByName = reviewnotes.Field<string>("UserName")
                    }).ToList();
                }
                return QuotationsFilter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveOrderedParts
        public string SaveOrderedParts(QuotationData quotationData)
        {
            try
            {
                var XMLOrderedParts = quotationData.QuotationParts.ToXML("ArrayOfOrderedParts");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLOrderedParts" , Value = quotationData.QuotationParts.Count > 0 ? XMLOrderedParts : null},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = quotationData.ModifiedBy},
                        new SqlParameter { ParameterName = "@RequestID" , Value = quotationData.RequestID},
                        new SqlParameter { ParameterName = "@WorkShopID" , Value = quotationData.WorkShopID},
                        new SqlParameter { ParameterName = "@ReasonNotSelectLMO" , Value = quotationData.ReasonNotSelectLMO},
                        new SqlParameter { ParameterName = "@LMO_QuotationID" , Value = quotationData.LMO_QuotationID}

                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveOrderedParts]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Order saved successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[saveOrderedParts]", CommandType.StoredProcedure, sParameter);
                return result.ToString();


            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region GetCompanyInvoice
        public QuotationData GetCompanyInvoice(int RequestID, int DemandID)
        {
            DataSet dt = new DataSet();
            try
            {
                var companyrequests = new QuotationData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getICInvoice]", CommandType.StoredProcedure, sParameter))
                {
                    companyrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        GroupName = req.Field<string>("GroupName"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),

                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        CountryName = req.Field<string>("CountryName"),
                        CityName = req.Field<string>("CityName"),

                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusName = req.Field<string>("StatusName"),
                        ICName = req.Field<string>("ICName"),
                        CPName = req.Field<string>("CPName"),
                        /////
                        LogoURL = req.Field<string>("LogoURL"),
                        CPPhone = req.Field<string>("CPPhone"),
                        CPEmail = req.Field<string>("CPEmail"),
                        SpAddressLine1 = req.Field<string>("SpAddressLine1"),
                        SpAddressLine2 = req.Field<string>("SpAddressLine2"),
                        SpCountryName = req.Field<string>("SpCountryName"),
                        SpCityname = req.Field<string>("SpCityname"),
                        InvoiceImage = req.Field<string>("InvoiceImage"),
                        SupplierName = req.Field<string>("SupplierName"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        /////
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),

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
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),

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
                        SupplierName = rp.Field<string>("SupplierName")

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
                }

                return companyrequests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetICDashboard
        public ICDashboard GetICDashboard(DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID, int? CompanyID, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                ICDashboard icDashboard = new ICDashboard();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@MakeID" , Value =MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = IsPurchasing},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID},
                    };
                using (dt = ADOManager.Instance.DataSet("[getICDashboardData]", CommandType.StoredProcedure, sParameter))
                {
                    icDashboard.DashboardStatsInfo = dt.Tables[0].AsEnumerable().Select(req => new DashboardStats
                    {
                        AccidentCount = req.Field<int?>("AccidentCount"),
                        POCount = req.Field<int?>("POCount"),
                        AVGLabourAmount = req.Field<double?>("AVGLabourAmount"),
                        TotalLabourAmount = req.Field<double?>("TotalLabourAmount"),
                        AVGClaimCost = req.Field<double?>("AVGClaimCost"),
                        TotalPOAmount = req.Field<double?>("TotalPOAmount"),
                        RepairOrderCount = req.Field<int?>("RepairOrderCount"),
                        RequestCount = req.Field<int?>("RequestCount"),
                        PricingAndProviding = req.Field<int?>("PricingAndProviding"),
                        AccidentCountWithPO = req.Field<int?>("AccidentCountWithPO"),
                        RequestCountWithPO = req.Field<int?>("RequestCountWithPO"),
                        TotalLabourWithRO = req.Field<double?>("TotalLabourWithRO"),
                        LowestMatchingPrices = req.Field<double?>("LowestMatchingPrices")
                        //PricingOnly = req.Field<int?>("PricingOnly")


                    }).FirstOrDefault();

                    icDashboard.DashboardAdditionalStatsData = dt.Tables[1].AsEnumerable().Select(req => new DashboardAdditionalStats
                    {
                        CreatedBy = req.Field<int>("CreatedBy"),
                        AdditionalRequestCount = req.Field<int>("AdditionalRequestCount"),
                        EmployeeName = req.Field<string>("EmployeeName")


                    }).ToList();

                    icDashboard.FinalAccidentCosts = dt.Tables[2].AsEnumerable().Select(req => new Accident
                    {
                        AccidentNo = req.Field<string>("AccidentNo"),
                        FinalAccidentCost = req.Field<double?>("FinalAccidentCost"),
                        IsClearance = req.Field<int?>("IsClearance"),
                        EnglishMakeName = req.Field<string>("EnglishMakeName"),
                        AccidentTypeName = req.Field<string>("AccidentType"),
                        PricingType = req.Field<string>("PricingType"),
                        TotalLabourCost = req.Field<double?>("LabourAmount"),
                        POAmount = req.Field<double?>("POAmount"),

                    }).ToList();

                    icDashboard.AccidentClearance = dt.Tables[3].AsEnumerable().Select(req => new Accident
                    {
                        AccidentNo = req.Field<string>("AccidentNo"),
                        ClearanceRoute = req.Field<string>("ClearanceRoute"),
                        ClearanceCreatedByName = req.Field<string>("ClearanceCreatedByName"),
                        ClearanceModifiedByName = req.Field<string>("ClearanceModifiedByName"),
                        ClearanceSummaryPdfUrl = req.Field<string>("ClearanceSummaryPdfUrl"),
                        AccidentID = req.Field<int>("AccidentID"),
                        Saving = req.Field<double?>("Saving"),
                        ClearanceCreatedOn = req.Field<DateTime?>("ClearanceCreatedOn"),
                        ClearanceModifiedOn = req.Field<DateTime?>("ClearanceModifiedOn"),

                    }).ToList();

                    icDashboard.SupplierStats = dt.Tables[4].AsEnumerable().Select(req => new Supplier
                    {
                        SupplierName = req.Field<string>("SupplierName"),
                        SupplierID = req.Field<int>("SupplierID"),
                        TotalPO = req.Field<int?>("TotalPO"),
                        TotalPOAmount = req.Field<double?>("TotalPOAmount"),

                    }).ToList();

                    icDashboard.WorkshopStats = dt.Tables[5].AsEnumerable().Select(req => new ICWorkshop
                    {
                        WorkshopID = req.Field<int>("WorkshopID"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        RepairOrderCount = req.Field<int?>("RepairOrderCount"),
                        TotalRepairOrderAmount = req.Field<double?>("TotalRepairOrderAmount"),

                    }).ToList();

                }
                return icDashboard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetSupplierPODetails
        public List<Request> GetSupplierPODetails(int SupplierID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID)
        {
            DataSet dt = new DataSet();
            try
            {
                var request = new List<Request>();
                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@MakeID" , Value =MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = IsPurchasing},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID}
                    };
                using (dt = ADOManager.Instance.DataSet("[getSupplierPODetails]", CommandType.StoredProcedure, sParameter))
                {
                    request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        RequestID = req.Field<int>("RequestID"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        OrderOn = req.Field<DateTime>("OrderOn"),
                        StatusID = req.Field<short?>("StatusID"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        CompanyID = req.Field<int>("CompanyID"),
                        DemandID = req.Field<int?>("DemandID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn")

                    }).ToList();
                }
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetWorkshopRODeatils
        public List<Request> GetWorkshopRODeatils(int WorkshopID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID)
        {
            DataSet dt = new DataSet();
            try
            {
                var request = new List<Request>();
                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@MakeID" , Value =MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = IsPurchasing},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID}
                    };
                using (dt = ADOManager.Instance.DataSet("[getWorkshopRODetails]", CommandType.StoredProcedure, sParameter))
                {
                    request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        RequestID = req.Field<int>("RequestID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        OrderOn = req.Field<DateTime?>("OrderOn"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        CompanyID = req.Field<int>("CompanyID"),
                        DemandID = req.Field<int?>("DemandID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        TotalRepairOrderAmount = req.Field<double?>("TotalRepairOrderAmount")
                    }).ToList();
                }
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region CheckAccidentNo
        public string CheckAccidentNo(string AccidentNo, int CompanyID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    };

                var result = Convert.ToString(ADOManager.Instance.ExecuteScalar("[checkAccidentNo]", CommandType.StoredProcedure, sParameter));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveWorkshop
        public string SaveWorkshop(ICWorkshop icWorkShop)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = icWorkShop.CreatedBy},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = icWorkShop.CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopGoogleMapLink" , Value = icWorkShop.WorkshopGoogleMapLink},
                        new SqlParameter { ParameterName = "@WorkshopName" , Value = icWorkShop.WorkshopName},
                        new SqlParameter { ParameterName = "@WorkshopPhone" , Value = icWorkShop.WorkshopPhone},
                        new SqlParameter { ParameterName = "@WorkshopAreaName" , Value = icWorkShop.WorkshopAreaName},
                        new SqlParameter { ParameterName = "@WorkshopCityID" , Value = icWorkShop.WorkshopCityID},
                        new SqlParameter { ParameterName = "@IsApproved" , Value = icWorkShop.IsApproved}
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveICWorkshop]", CommandType.StoredProcedure, sParameter));

                return result > 0 ? result.ToString() : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region UpdateWorkshop
        public string UpdateWorkshop(ICWorkshop icWorkShop)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = icWorkShop.ICWorkshopID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = icWorkShop.ModifiedBy},
                        new SqlParameter { ParameterName = "@WorkshopGoogleMapLink" , Value = icWorkShop.WorkshopGoogleMapLink},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = icWorkShop.CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopName" , Value = icWorkShop.WorkshopName},
                        new SqlParameter { ParameterName = "@WorkshopPhone" , Value = icWorkShop.WorkshopPhone},
                        new SqlParameter { ParameterName = "@WorkshopAreaName" , Value = icWorkShop.WorkshopAreaName},
                        new SqlParameter { ParameterName = "@WorkshopCityID" , Value = icWorkShop.WorkshopCityID},
                        new SqlParameter { ParameterName = "@IsApproved" , Value = icWorkShop.IsApproved}
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateICWorkshop]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Workshop updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

        #region DeleteWorkshop
        public string DeleteWorkshop(int WorkshopID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[deleteICWorkshop]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Workshop deleted successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[deleteICWorkshop]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAllWorkshops
        public List<ICWorkshop> GetAllWorkshops(int CompanyID, int LoginUserID, int? StatusID)
        {
            DataSet dt = new DataSet();
            try
            {
                var workshops = new List<ICWorkshop>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@LoginUserID" , Value = LoginUserID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getICWorkshops]", CommandType.StoredProcedure, sParameter))
                {
                    workshops = dt.Tables[0].AsEnumerable().Select(req => new ICWorkshop
                    {
                        ICWorkshopID = req.Field<int>("ICWorkshopID"),
                        Email = req.Field<string>("Email"),
                        WorkshopID = req.Field<int>("WorkshopID"),
                        WorkshopOwnerName = req.Field<string>("WorkshopOwnerName"),
                        EmployeeID = req.Field<int?>("EmployeeID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        WorkshopCityID = req.Field<int>("WorkshopCityID"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopGoogleMapLink = req.Field<string>("WorkshopGoogleMapLink"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        IsApproved = req.Field<Boolean>("IsApproved"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        StatusID = req.Field<Int16>("StatusID"),
                        AccountID = req.Field<int?>("AccountID"),
                        AccountNumber = req.Field<string>("AccountNumber"),
                        IsAIDraft = req.Field<bool?>("IsAIDraft")
                    }).ToList();

                }

                return workshops;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetSingleWorkshop
        public ICWorkshop GetSingleWorkshop(int WorkshopID, int LoginUserID)
        {
            DataSet dt = new DataSet();
            try
            {
                var workshop = new ICWorkshop();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@LoginUserID" , Value = LoginUserID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getSingleWorkshop]", CommandType.StoredProcedure, sParameter))
                {
                    workshop = dt.Tables[0].AsEnumerable().Select(req => new ICWorkshop
                    {
                        ICWorkshopID = req.Field<int>("ICWorkshopID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        WorkshopCityID = req.Field<int>("WorkshopCityID"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopGoogleMapLink = req.Field<string>("WorkshopGoogleMapLink"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        IsApproved = req.Field<Boolean>("IsApproved"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),

                    }).FirstOrDefault();

                }

                return workshop;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveUser

        public Employee SaveUser(Employee user)
        {
            DataSet dt = new DataSet();
            try
            {
                user.Id = Guid.NewGuid().ToString();
                var employee = new Employee();
                var XMLUserPermissions = user.UserPermissions.ToXML("ArrayOfUserPermissions");

                var sParameter = new List<SqlParameter>
                {
                      //new SqlParameter { ParameterName = "@UserID" , Value = user.UserID},
                      new SqlParameter { ParameterName = "@WorkshopID" , Value = user.ICWorkshopID},
                      new SqlParameter { ParameterName = "@CompanyID" , Value = user.CompanyID},
                      new SqlParameter { ParameterName = "@Email" , Value = user.Email},
                      new SqlParameter { ParameterName = "@RoleID" , Value = user.RoleID},
                      new SqlParameter { ParameterName = "@CreatedBy" , Value = user.CreatedBy},
                      new SqlParameter {ParameterName = "@Id", Value = user.Id },
                      new SqlParameter { ParameterName = "@FirstName" , Value = user.FirstName},
                      new SqlParameter { ParameterName = "@LastName" , Value = user.LastName},
                      new SqlParameter {ParameterName = "@Password", Value = EncryptionDecryption.EncryptString(user.Password)},
                      new SqlParameter { ParameterName = "@PhoneNumber" , Value = user.PhoneNumber},
                      new SqlParameter { ParameterName = "@XMLUserPermissions" , Value = XMLUserPermissions},
                      new SqlParameter { ParameterName = "@ImgURL" , Value = user.ImgURL},
                };
                using (dt = ADOManager.Instance.DataSet("[saveUser]", CommandType.StoredProcedure, sParameter))
                {
                    employee.EmployeeID = Convert.ToInt32(dt.Tables[0].Rows[0]["EmployeeID"]);
                    employee.UserID = Convert.ToInt32(dt.Tables[0].Rows[0]["UserID"]);
                    employee.ErrorMessage = Convert.ToString(dt.Tables[0].Rows[0]["ErrorMessage"]);
                }
                //    var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveUser]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? result.ToString() : result.ToString();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        #endregion

        #region UpdateUser
        public string UpdateUser(Employee User)
        {
            try
            {
                var XMLUserPermissions = User.UserPermissions.ToXML("ArrayOfUserPermissions");
                var sParameter = new List<SqlParameter>
                    {

                         new SqlParameter { ParameterName = "@RoleID" , Value =  User.RoleID},
                         new SqlParameter { ParameterName = "@UserID" , Value =  User.UserID},
                         new SqlParameter { ParameterName = "@EmployeeID" , Value =  User.EmployeeID},
                         new SqlParameter { ParameterName = "@WorkshopID" , Value =  User.ICWorkshopID},
                         new SqlParameter { ParameterName = "@FirstName" , Value = User.FirstName},
                         new SqlParameter { ParameterName = "@LastName" , Value =  User.LastName},
                         new SqlParameter { ParameterName = "@PhoneNumber" , Value = User.PhoneNumber},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = User.ModifiedBy},
                         new SqlParameter {ParameterName = "@Password", Value = User.Password != null && User.Password != "" ?EncryptionDecryption.EncryptString(User.Password) : null},
                         new SqlParameter { ParameterName = "@XMLUserPermissions" , Value = XMLUserPermissions},
                         //new SqlParameter { ParameterName = "@ConfirmPassword" , Value = User.ConfirmPassword},
                           new SqlParameter { ParameterName = "@ImgURL" , Value = User.ImgURL},
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateUser]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "User Updated successfully" : null;

                var result = ADOManager.Instance.ExecuteScalar("[updateUser]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAllUsers
        public List<Employee> GetAllUsers(int UserID, int LoginCompanyID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Users = new List<Employee>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@LoginCompanyID" , Value = LoginCompanyID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getAllUsers]", CommandType.StoredProcedure, sParameter))
                {
                    Users = dt.Tables[0].AsEnumerable().Select(req => new Employee
                    {
                        EmployeeID = req.Field<int>("EmployeeID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        RoleID = req.Field<byte>("RoleID"),
                        RoleName = req.Field<string>("Name"),
                        RoleIcon = req.Field<string>("Icon"),
                        //WorkshopName = req.Field<string>("WorkshopName"),
                        //ICWorkshopID = req.Field<int?>("ICWorkshopID"),
                        UserID = req.Field<int>("UserID"),
                        FirstName = req.Field<string>("FirstName"),
                        LastName = req.Field<string>("LastName"),
                        Email = req.Field<string>("Email"),
                        PhoneNumber = req.Field<string>("Phone"),
                        ProfileImageURL = req.Field<string>("ProfileImageURL"),
                        ImgURL = req.Field<string>("ImgURL"),
                        RoleNameArabic = req.Field<string>("RoleNameArabic"),
                        EmployeeName = req.Field<string>("EmployeeName")


                    }).ToList();

                }

                return Users;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region DeleteUser
        public string DeleteUser(int UserID, int ModifiedBy)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@UserID", Value = UserID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = ModifiedBy},

                };
                var result = Convert.ToString(ADOManager.Instance.ExecuteScalar("[deleteUser]", CommandType.StoredProcedure, sParameter));
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetEmployeeMeta
        public CommonMeta GetEmployeeMeta(int CompanyID, int? EmployeeID)
        {
            DataSet dt = new DataSet();
            try
            {
                var commonMeta = new CommonMeta();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                         new SqlParameter { ParameterName = "@EmployeeID" , Value = EmployeeID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getEmployeeMeta]", CommandType.StoredProcedure, sParameter))
                {

                    commonMeta.Roles = dt.Tables[0].AsEnumerable().Select(rl => new Roles
                    {
                        Id = rl.Field<byte>("Id"),
                        Name = rl.Field<string>("Name"),
                        ObjectName = rl.Field<string>("ObjectName"),
                        Icon = rl.Field<string>("Icon"),
                        RolesNameArabic = rl.Field<string>("RolesNameArabic")

                    }).ToList();

                    commonMeta.ICWorkshops = dt.Tables[1].AsEnumerable().Select(icw => new ICWorkshop
                    {
                        ICWorkshopID = icw.Field<int>("ICWorkshopID"),
                        WorkshopName = icw.Field<string>("WorkshopName"),

                    }).ToList();

                    commonMeta.UserPermissions = dt.Tables[2].AsEnumerable().Select(pm => new Permission
                    {
                        PermissionID = pm.Field<Int16>("PermissionID"),
                        RoleID = pm.Field<int?>("RoleID"),
                        PermissionName = pm.Field<string>("PermissionName"),
                        PermissionArabicName = pm.Field<string>("PermissionArabicName"),
                        UserPermissionID = pm.Field<int?>("UserPermissionID"),
                        UserID = pm.Field<int?>("UserID"),
                        MinPrice = pm.Field<int?>("MinPrice"),
                        MaxPrice = pm.Field<int?>("MaxPrice"),
                        IsChecked = pm.Field<bool?>("IsChecked"),
                        UserObjectName = pm.Field<string>("UserObjectName"),

                    }).ToList();

                    if (EmployeeID > 0)
                    {
                        commonMeta.EmployeeObj = dt.Tables[3].AsEnumerable().Select(req => new Employee
                        {
                            EmployeeID = req.Field<int>("EmployeeID"),
                            UserID = req.Field<int>("UserID"),
                            FirstName = req.Field<string>("FirstName"),
                            LastName = req.Field<string>("LastName"),
                            Email = req.Field<string>("Email"),
                            PhoneNumber = req.Field<string>("Phone"),
                            ICWorkshopID = req.Field<int?>("WorkshopID"),
                            RoleID = req.Field<byte>("RoleID"),
                            PositionID = req.Field<Int16>("PositionID"),
                            CompanyID = req.Field<int>("CompanyID"),
                            ImgURL = req.Field<string>("ImgURL"),
                            ESignatureURL = req.Field<string>("ESignatureURL"),

                        }).FirstOrDefault();
                    }
                }

                return commonMeta;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region ClosePartsRequest
        public string ClosePartsRequest(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[closePartsRequest]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Request Closed successfully" : null;

                var result = ADOManager.Instance.ExecuteScalar("[closePartsRequest]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PublishPartsRequest
        public string PublishPartsRequest(PublishRequest publishRequest)
        {
            try
            {
                var XMLRequestedParts = publishRequest.RequestedParts.ToXML("ArrayOfRequestedParts");
                var XMLRequestedTasks = publishRequest.RequestedTask.ToXML("ArrayOfRequestedTask");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = publishRequest.RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = publishRequest.ModifiedBy},
                        new SqlParameter { ParameterName = "@SerialNo" , Value = publishRequest.SerialNo},
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = publishRequest.RequestedParts != null && publishRequest.RequestedParts.Count > 0 ? XMLRequestedParts : null},
                        new SqlParameter { ParameterName = "@XMLRequestedTasks" , Value = publishRequest.RequestedTask != null && publishRequest.RequestedTask.Count > 0 ? XMLRequestedTasks : null}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[closePartsRequest]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Request Closed successfully" : null;

                var result = ADOManager.Instance.ExecuteScalar("[PublishPartsRequest]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region AcceptRequestedPart
        public List<QuotationPart> AcceptRequestedPart(QuotationPart RejectedPart)
        {
            try
            {
                if (RejectedPart.IsAccepted == false)
                {
                    if (RejectedPart.POPdfUrl != null && RejectedPart.POPdfURL != null)
                    {

                        string b = RejectedPart.POPdfURL.Substring(0, 11);
                        int SupplierPdfLen = RejectedPart.POPdfURL.Length;
                        int ICPdfLen = RejectedPart.POPdfUrl.Length;


                        string SupplierPDF = RejectedPart.POPdfURL.Substring(b.Length - 0, SupplierPdfLen - b.Length);
                        string ICPDF = RejectedPart.POPdfUrl.Substring(b.Length - 0, ICPdfLen - b.Length);

                        DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/po-reports"));
                        FileInfo[] Supplierpdfs = di.GetFiles(SupplierPDF)
                             .Where(p => p.Extension == ".pdf").ToArray();

                        FileInfo[] ICPdfs = di.GetFiles(ICPDF)
                            .Where(p => p.Extension == ".pdf").ToArray();


                        foreach (FileInfo file in Supplierpdfs)
                        {
                            try
                            {
                                if (file.Name != SupplierPDF)
                                {
                                    File.Delete(file.FullName);
                                }
                            }
                            catch { }
                        }
                        foreach (FileInfo file in ICPdfs)
                        {
                            try
                            {
                                if (file.Name != ICPDF)
                                {
                                    File.Delete(file.FullName);
                                }
                            }
                            catch { }
                        }
                    }

                }



                DataSet dt = new DataSet();

                string s = null;
                var XMLRejectedPartImg = s;
                if (RejectedPart.RejectedPartImage != null)
                {
                    XMLRejectedPartImg = RejectedPart.RejectedPartImage.ToXML("ArrayOfRejectedpartImg");
                }
                else
                {
                    XMLRejectedPartImg = null;
                }

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationPartID" , Value = RejectedPart.QuotationPartID},
                        new SqlParameter { ParameterName = "@IsAccepted" , Value = RejectedPart.IsAccepted},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = RejectedPart.ModifiedBy},
                        new SqlParameter { ParameterName = "@RejectReason" , Value = RejectedPart.RejectReason},
                        new SqlParameter { ParameterName = "@XMLRejectedPartImg" , Value = XMLRejectedPartImg},

                    };
                var OrderedParts = new List<QuotationPart>();

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter));
                //return result == 1 ? "accepted successfully" : "rejected successfully";

                //var result = ADOManager.Instance.ExecuteScalar("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter))
                {
                    OrderedParts = dt.Tables[0].AsEnumerable().Select(qp => new QuotationPart
                    {
                        QuotationPartID = qp.Field<int>("QuotationPartID"),
                        RequestedPartID = qp.Field<int>("RequestedPartID"),
                        DeliveryTime = qp.Field<byte?>("DeliveryTime"),
                        ConditionTypeID = qp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeName = qp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = qp.Field<string>("NewPartConditionTypeArabicName"),
                        OrderedQuantity = qp.Field<int?>("OrderedQuantity"),
                        AutomotivePartName = qp.Field<string>("PartName"),
                        ConditionTypeName = qp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = qp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = qp.Field<string>("NoteInfo"),
                        RejectReason = qp.Field<string>("RejectReason"),
                        IsAccepted = qp.Field<bool?>("IsAccepted"),
                        IsRecycled = qp.Field<bool?>("IsRecycled"),
                        IsReceived = qp.Field<bool?>("IsReceived"),
                        ReceivedDate = qp.Field<DateTime?>("ReceivedDate"),
                        SupplierName = qp.Field<string>("SupplierName"),
                        AutomotivePartArabicName = qp.Field<string>("PartNameArabic"),
                        ReferredPrice = qp.Field<double?>("ReferredPrice"),
                        OrderRowNumber = qp.Field<int>("OrderRowNumber"),
                        IsReturn = qp.Field<bool?>("IsReturn")
                    }).ToList();
                }
                return OrderedParts;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ChangeRequestStatus
        public string ChangeRequestStatus(int RequestID, int ModifiedBy, int StatusID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                         new SqlParameter { ParameterName = "@StatusID" , Value = StatusID}
                    };

                var procedure = "[changeRequestStatus]";

                if (StatusID == 11) // for deliver
                {
                    procedure = "[changeRequestStatus]";
                }
                else if (StatusID == 17)  // for paid
                {
                    procedure = "[changeRequestPaidStatus]";
                }
                else if (StatusID == 18)  // for paid
                {
                    procedure = "[changeRequestCompleteStatus]";
                }

                var result = ADOManager.Instance.ExecuteScalar(procedure, CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region RecycleDemand
        public string RecycleDemand(QuotationData quotationData)
        {
            try
            {
                var XMLOrderedParts = quotationData.QuotationParts.ToXML("ArrayOfRecycledParts");
                var sParameter = new List<SqlParameter>
                    {
                     new SqlParameter { ParameterName = "@XMLRecycledParts" , Value = quotationData.QuotationParts.Count > 0 ? XMLOrderedParts : null},
                        new SqlParameter { ParameterName = "@RequestID" , Value = quotationData.RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = quotationData.ModifiedBy},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[recycleDemand]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateAccidentStatus
        public string UpdateAccidentStatus(int AccidentID, int StatusID, int ModifiedBy, string Reason)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@Reason" , Value = Reason},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateAccidentStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ApprovePart
        public string ApprovePart(int RequestedPartID, bool IsPartApproved, int ModifiedBy, string PartRejectReason)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestedPartID" , Value = RequestedPartID},
                        new SqlParameter { ParameterName = "@IsPartApproved" , Value = IsPartApproved},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@PartRejectReason" , Value = PartRejectReason}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter));
                //return result == 1 ? "accepted successfully" : "rejected successfully";

                var result = ADOManager.Instance.ExecuteScalar("[approvePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ReceiveRequestedPart
        public string ReceiveRequestedPart(int QuotationPartID, int ModifiedBy, int RequestID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationPartID" , Value = QuotationPartID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[receiveRequestedPart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ApproveTask
        public string ApproveTask(int RequestTaskID, bool IsTaskApproved, int ModifiedBy, string TaskRejectReason)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestTaskID" , Value = RequestTaskID},
                        new SqlParameter { ParameterName = "@IsTaskApproved" , Value = IsTaskApproved},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@TaskRejectReason" , Value = TaskRejectReason}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[approveTask]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region MarkRequestReady
        public string MarkRequestReady(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[markRequestReady]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region StartCarRepair
        public string StartCarRepair(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[startCarRepair]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region RestorePartsRequest
        public string RestorePartsRequest(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@UserID" , Value = ModifiedBy}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[restorePartsRequest]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveICUserProfile
        public string SaveICUserProfile(Employee icEmployee)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@EmployeeID" , Value = icEmployee.EmployeeID},
                        new SqlParameter { ParameterName = "@ImgURL" , Value = icEmployee.ImgURL},
                        new SqlParameter { ParameterName = "@ESignatureURL" , Value = icEmployee.ESignatureURL},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveICUserProfile]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Profile saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OpenPartsRequest
        public string OpenPartsRequest(int RequestID, int ModifiedBy, int? TempStatusID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                         new SqlParameter { ParameterName = "@TempStatusID" , Value = TempStatusID}
                    };


                var result = ADOManager.Instance.ExecuteScalar("[openPartsRequest]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetQuotationSummary
        public QuotationSummary GetQuotationSummary(int DemandID, int UserID)
        {
            DataSet dt = new DataSet();
            try
            {
                QuotationSummary quotationSummary = new QuotationSummary();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID },
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID },
                };

                using (dt = ADOManager.Instance.DataSet("[getQuotationsSummary]", CommandType.StoredProcedure, sParameter))
                {
                    quotationSummary.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        DemandID = req.Field<int>("DemandID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        VIN = req.Field<string>("VIN"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        CPName = req.Field<string>("CPName"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        CountryName = req.Field<string>("CountryName"),
                        CityName = req.Field<string>("CityName"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        AccidentID = req.Field<int>("AccidentID"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        //BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        //IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        //TotalQuotations = req.Field<int>("TotalQuotations"),
                        IcCancelOrderNote = req.Field<string>("IcCancelOrderNote"),
                        JoCancelOrderNote = req.Field<string>("JoCancelOrderNote"),
                        DemandCreatedOn = req.Field<DateTime?>("DemandCreatedOn"),
                        RecommendedTotalPrice = req.Field<double?>("RecommendedTotalPrice"),
                        LowestMatchingOffer = req.Field<double?>("LowestMatchingOffer"),
                        LowestNotMatchingOffer = req.Field<double?>("LowestNotMatchingOffer"),
                        SavingByRecommended = req.Field<double?>("SavingByRecommended"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        QuotationSummaryPdfUrl = req.Field<string>("QuotationSummaryPdfUrl"),

                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        PlateNo = req.Field<string>("PlateNo"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("CreatedOnAccident"),
                        OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        PDFReport = req.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        NotPurchasingLMOReason = req.Field<string>("NotPurchasingLMOReason")
                    }).FirstOrDefault();

                    quotationSummary.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int>("Quantity"),
                        //DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
                        //DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        DemandedQuantity = rp.Field<int>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        RecommendedSupplierID = rp.Field<int?>("RecommendedSupplierID"),
                        RecommendedSupplierName = rp.Field<string>("RecommendedSupplierName"),
                        MinPriceQuotationPartID = rp.Field<int?>("MinPriceQuotationPartID"),
                        MinPrice = rp.Field<double?>("MinPrice"),
                    }).ToList();

                    quotationSummary.RequestedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    quotationSummary.Quotations = dt.Tables[3].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        StatusID = qt.Field<Int16?>("StatusID"),
                        StatusName = qt.Field<string>("StatusName"),
                        ArabicStatusName = qt.Field<string>("ArabicStatusName"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        CreatedSince = qt.Field<string>("CreatedSince"),
                        CreatedSinceArabic = qt.Field<string>("CreatedSinceArabic"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        OriginalPrice = qt.Field<double?>("OriginalPrice"),
                        PaymentTypeID = qt.Field<Int16?>("PaymentTypeID"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        RecommendationType = qt.Field<int>("RecommendationType"),
                        SupplierType = qt.Field<Int16?>("SupplierType"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
                        MatchingFillingRate = qt.Field<decimal?>("MatchingFillingRate")
                    }).ToList();

                    quotationSummary.QuotationParts = dt.Tables[4].AsEnumerable().Select(rp => new QuotationPartRef
                    {
                        QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        QuotationID = rp.Field<int?>("QuotationID"),
                        RequestedPartID = rp.Field<int?>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        Price = rp.Field<double?>("Price"),
                        Quantity = rp.Field<int?>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        //DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        //CityID = rp.Field<int?>("CityID"),
                        CityName = rp.Field<string>("CityName"),
                        BranchAreaName = rp.Field<string>("BranchAreaName"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        //WillDeliver = rp.Field<bool>("WillDeliver"),
                        //DeliveryCost = rp.Field<decimal?>("DeliveryCost"),
                        OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                        IsAccepted = rp.Field<bool?>("IsAccepted"),
                        //RowNumber = rp.Field<int>("RowNumber"),
                        OrderedOn = rp.Field<DateTime?>("OrderedOn"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        AcceptedByName = rp.Field<string>("AcceptedByName"),
                        WorkshopName = rp.Field<string>("WorkshopName"),
                        FillingRate = rp.Field<decimal?>("FillingRate"),
                        //RequestPartCount = rp.Field<int?>("RequestPartCount"),
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                        IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                        NetPrice = rp.Field<double?>("NetPrice"),
                        IsRecommended = rp.Field<bool?>("IsRecommended"),
                        IsReturn = rp.Field<bool?>("IsReturn"),
                        IsWsAccepted = rp.Field<bool?>("IsWsAccepted")

                    }).ToList();

                    quotationSummary.QuotationPartsImages = dt.Tables[5].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID")

                    }).ToList();

                }
                return quotationSummary;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetClearanceSummary
        public ClearanceSummary GetClearanceSummary(int CompanyID, int AccidentID)
        {
            DataSet dt = new DataSet();
            try
            {
                ClearanceSummary clearanceSummary = new ClearanceSummary();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID },
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID },
                };

                using (dt = ADOManager.Instance.DataSet("[getClearanceSummary]", CommandType.StoredProcedure, sParameter))
                {
                    clearanceSummary.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentID = req.Field<int>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        VIN = req.Field<string>("VIN"),
                        YearCode = req.Field<int>("YearCode"),
                        CPName = req.Field<string>("CPName"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        LogoURL = req.Field<string>("LogoURL"),
                        UserName = req.Field<string>("UserName"),
                        Email = req.Field<string>("Email"),
                        PhoneNumber = req.Field<string>("PhoneNumber"),
                        UserID = req.Field<int>("UserID"),
                        WebsiteAddress = req.Field<string>("WebsiteAddress"),
                        FaxNumber = req.Field<string>("FaxNumber"),
                        CompanyName = req.Field<string>("CompanyName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        PlateNo = req.Field<string>("PlateNo"),
                        OwnerPhoneNo = req.Field<string>("OwnerPhoneNo"),
                        VernishPrice = req.Field<double?>("VernishPrice"),
                        MechanicLabourPrice = req.Field<double?>("MechanicLabourPrice"),
                        DamagePrice = req.Field<double?>("DamagePrice"),
                        RentPrice = req.Field<double?>("RentPrice"),
                        TotalLabourCost = req.Field<double?>("TotalLabourCost"),
                        TotalLabourTime = req.Field<int?>("TotalLabourTime"),
                        FinalPartsAmount = req.Field<double?>("FinalPartsAmount"),
                        FinalLabourCost = req.Field<double?>("FinalLabourCost"),
                        TotalPartsAmount = req.Field<double?>("TotalPartsAmount"),
                        GasPrice = req.Field<double?>("GasPrice"),
                        TotalAccidentCost = req.Field<double?>("TotalAccidentCost"),
                        FinalAccidentCost = req.Field<double?>("FinalAccidentCost"),
                        FinalLabourTime = req.Field<int?>("FinalLabourTime"),
                        TotalOldPartsAmount = req.Field<double?>("TotalOldPartsAmount"),
                        TotalMechanicLabourPrice = req.Field<double?>("TotalMechanicLabourPrice"),
                        ClearanceModifiedByName = req.Field<string>("ClearanceModifiedByName"),
                        ClearanceModifiedOn = req.Field<DateTime?>("ClearanceModifiedOn"),
                        ClearanceSummaryPdfUrl = req.Field<string>("ClearanceSummaryPdfUrl"),
                        CustomerESignatureURL = req.Field<string>("CustomerESignatureURL"),
                        faultyPlateNo = req.Field<string>("faultyPlateNo"),
                        AccidentLocation = req.Field<string>("AccidentLocation"),
                        FaultyPartyName = req.Field<string>("FaultyPartyName"),
                        AccidentConsequences = req.Field<string>("AccidentConsequences"),
                        TotalAmountInWritten = req.Field<string>("TotalAmountInWritten"),
                        InsuranceDocumentNo = req.Field<string>("InsuranceDocumentNo"),
                        OtherPlusText = req.Field<string>("OtherPlusText"),
                        OtherMinText = req.Field<string>("OtherMinText"),
                        FirstRequestLabourPrice = req.Field<double?>("FirstRequestLabourPrice"),
                        OurResponsibility = req.Field<int?>("OurResponsibility"),
                        ClearanceSummaryFreeText = req.Field<string>("ClearanceSummaryFreeText"),
                        ClearanceSummaryImage = req.Field<string>("ClearanceSummaryImage"),
                        PoliceReport = req.Field<string>("PoliceReportNumber")
                    }).FirstOrDefault();

                    clearanceSummary.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        Quantity = rp.Field<int>("Quantity"),
                        IsBringOldPart = rp.Field<bool?>("IsBringOldPart"),
                    }).ToList();

                    clearanceSummary.RequestTasks = dt.Tables[2].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),
                        TaskTypeName = rt.Field<string>("TypeName"),
                        TaskArabicTypeName = rt.Field<string>("ArabicTypeName"),

                    }).ToList();

                    clearanceSummary.Quotations = dt.Tables[3].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        RequestID = qt.Field<int>("RequestID"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        OriginalPrice = qt.Field<double?>("OriginalPrice"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                    }).ToList();
                    if (CompanyID == 136)
                    {
                        clearanceSummary.Employees = dt.Tables[4].AsEnumerable().Select(e => new Employee
                        {
                            UserID = e.Field<int>("UserID"),
                            FirstName = e.Field<string>("FirstName"),
                            LastName = e.Field<string>("LastName"),
                            CompanyID = e.Field<int>("CompanyID"),
                            IsDeleted = e.Field<bool>("IsDeleted"),
                            IsApproved = e.Field<bool?>("IsApproved"),
                            EmployeeID = e.Field<int>("EmployeeID"),
                            ClearanceSummaryApprovalID = e.Field<int>("ClearanceSummaryApprovalID")
                        }).ToList();
                    }

                }
                return clearanceSummary;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region PrintQuotationSummaryPdf
        public string PrintQuotationSummaryPdf(PdfData pdfData)
        {
            try
            {    //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");
                pdfData.elementHtml = (string)JsonConvert.DeserializeObject(pdfData.elementHtml);
                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4Rotated;
                HtmlToPdf.PrintOptions.MarginTop = 4;  //millimeters
                HtmlToPdf.PrintOptions.MarginBottom = 2;
                HtmlToPdf.PrintOptions.MarginLeft = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginRight = 2;

                HtmlToPdf.PrintOptions.Zoom = 60;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);

                string fileName = "Quotation-Summary-Accident-No" + pdfData.AccidentNo.Replace('/', '-') + "-CompanyName-" + pdfData.CompanyName + ".pdf";

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/summary-reports"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }
                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@AccidentID", Value = pdfData.AccidentID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "summary-reports/" + fileName},

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateQuotationSummaryUrl]", CommandType.StoredProcedure, sParameter));
                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/summary-reports"), fileName));
                return "summary-reports/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
            }
        }
        #endregion

        #region GetLatestRequest
        public RequestData GetLatestRequest(int RequestID, int? WorkshopID)
        {
            DataSet dt = new DataSet();
            try
            {
                var requestData = new RequestData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getLatestRequest]", CommandType.StoredProcedure, sParameter))
                {
                    requestData.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16>("StatusID"),
                        AdminStatusID = req.Field<Int16>("AdminStatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        DemandID = req.Field<int?>("DemandID"),
                        TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        AdminTotalQuotations = req.Field<int>("AdminTotalQuotations"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        OrderByName = req.Field<string>("OrderByName"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        IsPurchasing = req.Field<bool>("IsPurchasing"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        ImgURL = req.Field<string>("ImgURL"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        PlateNo = req.Field<string>("PlateNo"),
                        SerialNo = req.Field<int?>("serialNo"),
                        RequestRowNumber = req.Field<int?>("RequestRowNumber"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsCarRepairStarted = req.Field<bool?>("IsCarRepairStarted"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        TotalRequestedParts = req.Field<int?>("TotalRequestedParts"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("Name"),
                        CPName = req.Field<string>("CPName"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")
                    }).ToList();

                    requestData.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        isSpliced = rp.Field<int>("isSpliced"),

                    }).ToList();

                    requestData.POApprovalEmployees = dt.Tables[2].AsEnumerable().Select(po => new POApproval
                    {
                        POApprovalID = po.Field<int>("POApprovalID"),
                        RequestID = po.Field<int>("RequestID"),
                        UserID = po.Field<int>("UserID"),
                        EmployeeID = po.Field<int>("EmployeeID"),
                        LastName = po.Field<string>("LastName"),
                        FirstName = po.Field<string>("FirstName"),
                        Email = po.Field<string>("Email"),
                        IsApproved = po.Field<bool?>("IsApproved"),
                        CreatedOn = po.Field<DateTime>("CreatedOn"),
                        ModifiedOn = po.Field<DateTime?>("ModifiedOn"),

                    }).ToList();
                }

                return requestData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetCompanyHistoryRequests
        public Companyrequests GetCompanyHistoryRequests(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int PageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery)
        {
            DataSet dt = new DataSet();
            try
            {
                if (searchQuery == "null" || searchQuery == "")
                    searchQuery = null;

                var companyrequests = new Companyrequests();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID == 0 ? null : WorkshopID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@searchQuery" , Value = searchQuery},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getCompanyHistoryRequests]", CommandType.StoredProcedure, sParameter))
                {
                    companyrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        StatusID = req.Field<Int16>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        DemandID = req.Field<int?>("DemandID"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        OrderByName = req.Field<string>("OrderByName"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        ImgURL = req.Field<string>("ImgURL"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        PlateNo = req.Field<string>("PlateNo"),
                        SerialNo = req.Field<int?>("serialNo"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsCarRepairStarted = req.Field<bool?>("IsCarRepairStarted"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        TotalRequestedParts = req.Field<int?>("TotalRequestedParts"),
                        RequestRowNumber = req.Field<int?>("RequestRowNumber"),
                        TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
                        ClearanceRoute = req.Field<string>("ClearanceRoute"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),

                    }).ToList();

                    //companyrequests.Makes = dt.Tables[1].AsEnumerable().Select(make => new Make
                    //{
                    //    MakeCode = make.Field<string>("MakeCode"),
                    //    ImgURL = make.Field<string>("ImgURL"),
                    //    MakeID = make.Field<int>("MakeID"),
                    //    ModifiedBy = make.Field<int?>("ModifiedBy"),
                    //    CreatedOn = make.Field<DateTime?>("CreatedOn"),
                    //    CreatedBy = make.Field<int?>("CreatedBy"),
                    //    ModifiedOn = make.Field<DateTime?>("ModifiedOn"),
                    //    MakeName = make.Field<string>("EnglishMakeName"),
                    //    ArabicMakeName = make.Field<string>("ArabicMakeName"),

                    //}).ToList();

                    //companyrequests.Models = dt.Tables[2].AsEnumerable().Select(model => new Model
                    //{
                    //    MakeID = model.Field<int>("MakeID"),
                    //    ModelID = model.Field<int>("ModelID"),
                    //    ModelCode = model.Field<string>("EnglishModelName"),
                    //    ArabicModelName = model.Field<string>("ArabicModelName"),
                    //    GroupName = model.Field<string>("GroupName")

                    //}).ToList();

                    //companyrequests.Years = dt.Tables[3].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    //companyrequests.Accidents = dt.Tables[4].AsEnumerable().Select(acd => new Accident
                    //{
                    //    AccidentID = acd.Field<int>("AccidentID"),
                    //    AccidentNo = acd.Field<string>("AccidentNo"),
                    //    CompanyID = acd.Field<int>("CompanyID"),
                    //    MakeID = acd.Field<int>("MakeID"),
                    //    ModelID = acd.Field<int>("ModelID"),
                    //    PlateNo = acd.Field<string>("PlateNo"),
                    //    ProductionYear = acd.Field<Int16>("ProductionYear"),
                    //    VIN = acd.Field<string>("VIN"),
                    //    AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),

                    //}).ToList();

                    //companyrequests.POApprovalEmployees = dt.Tables[1].AsEnumerable().Select(po => new POApproval
                    //{
                    //    POApprovalID = po.Field<int>("POApprovalID"),
                    //    RequestID = po.Field<int>("RequestID"),
                    //    UserID = po.Field<int>("UserID"),
                    //    EmployeeID = po.Field<int>("EmployeeID"),
                    //    LastName = po.Field<string>("LastName"),
                    //    FirstName = po.Field<string>("FirstName"),
                    //    Email = po.Field<string>("Email"),
                    //    IsApproved = po.Field<bool>("IsApproved"),
                    //    CreatedOn = po.Field<DateTime>("CreatedOn"),
                    //    ModifiedOn = po.Field<DateTime?>("ModifiedOn")
                    //}).ToList();

                    companyrequests.TabInfoData = dt.Tables[1].AsEnumerable().Select(tbi => new TabInfo
                    {

                        CancelledRequests = tbi.Field<int>("CancelledRequests"),
                        ClosedRequests = tbi.Field<int>("ClosedRequests"),
                        DeletedRequests = tbi.Field<int>("DeletedRequests")

                    }).FirstOrDefault();
                    //companyrequests.TotalPages = Convert.ToInt32(dt.Tables[2].Rows[0]["TotalPages"]);
                }

                return companyrequests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveClearanceReport
        public string SaveClearanceReport(ClearanceSummary clearanceSummaryObj)
        {
            try
            {
                var XMLRequestedParts = clearanceSummaryObj.RequestedParts.ToXML("ArrayOfRequestedParts");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = clearanceSummaryObj.Request.AccidentID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = clearanceSummaryObj.Request.ModifiedBy},
                        new SqlParameter { ParameterName = "@VernishPrice" , Value = clearanceSummaryObj.Request.VernishPrice},
                        new SqlParameter { ParameterName = "@MechanicLabourPrice" , Value = clearanceSummaryObj.Request.MechanicLabourPrice},
                        new SqlParameter { ParameterName = "@DamagePrice" , Value = clearanceSummaryObj.Request.DamagePrice},
                        new SqlParameter { ParameterName = "@RentPrice" , Value = clearanceSummaryObj.Request.RentPrice},
                        new SqlParameter { ParameterName = "@TotalLabourCost" , Value = clearanceSummaryObj.Request.TotalLabourCost},
                        new SqlParameter { ParameterName = "@TotalLabourTime" , Value = clearanceSummaryObj.Request.TotalLabourTime},
                        new SqlParameter { ParameterName = "@FinalPartsAmount" , Value = clearanceSummaryObj.Request.FinalPartsAmount},
                        new SqlParameter { ParameterName = "@FinalLabourCost" , Value = clearanceSummaryObj.Request.FinalLabourCost},
                        new SqlParameter { ParameterName = "@TotalPartsAmount" , Value = clearanceSummaryObj.Request.TotalPartsAmount},
                        new SqlParameter { ParameterName = "@GasPrice" , Value = clearanceSummaryObj.Request.GasPrice},
                        new SqlParameter { ParameterName = "@TotalAccidentCost" , Value = clearanceSummaryObj.Request.TotalAccidentCost},
                        new SqlParameter { ParameterName = "@FinalAccidentCost" , Value = clearanceSummaryObj.Request.FinalAccidentCost},
                        new SqlParameter { ParameterName = "@FinalLabourTime" , Value = clearanceSummaryObj.Request.FinalLabourTime},
                        new SqlParameter { ParameterName = "@TotalOldPartsAmount" , Value = clearanceSummaryObj.Request.TotalOldPartsAmount},
                        new SqlParameter { ParameterName = "@TotalMechanicLabourPrice" , Value = clearanceSummaryObj.Request.TotalMechanicLabourPrice},
                        new SqlParameter { ParameterName = "@CustomerESignatureURL" , Value = clearanceSummaryObj.Request.CustomerESignatureURL},
                        new SqlParameter { ParameterName = "@faultyPlateNo", Value = clearanceSummaryObj.Request.faultyPlateNo},
                        new SqlParameter { ParameterName = "@AccidentLocation", Value = clearanceSummaryObj.Request.AccidentLocation},
                        new SqlParameter { ParameterName = "@FaultyPartyName", Value = clearanceSummaryObj.Request.FaultyPartyName},
                        new SqlParameter { ParameterName = "@AccidentConsequences", Value = clearanceSummaryObj.Request.AccidentConsequences},
                        new SqlParameter { ParameterName = "@TotalAmountInWritten", Value = clearanceSummaryObj.Request.TotalAmountInWritten},
                        new SqlParameter { ParameterName = "@InsuranceDocumentNo", Value = clearanceSummaryObj.Request.InsuranceDocumentNo},
                        new SqlParameter { ParameterName = "@OtherPlusText", Value = clearanceSummaryObj.Request.OtherPlusText},
                        new SqlParameter { ParameterName = "@OtherMinText", Value = clearanceSummaryObj.Request.OtherMinText},

                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = clearanceSummaryObj.RequestedParts.Count() > 0 ? XMLRequestedParts : null},
                        new SqlParameter { ParameterName = "@VehicleOwnerName", Value = clearanceSummaryObj.Request.VehicleOwnerName},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveClearanceReport]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Summary saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DeleteRequestedPart
        public string DeleteRequestedPart(int RequestedPartID, int RequestID, int ModifiedBy, int? DamagePointID)
        {
            try
            {

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestedPartID" , Value = RequestedPartID},
                         new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                         new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePointID != null? DamagePointID: null}
                    };

                var result = Convert.ToString(ADOManager.Instance.ExecuteScalar("[deleteRequestedPart]", CommandType.StoredProcedure, sParameter));
                return result != null ? "Part deleted successfully" : "Unable to delete part. Error!";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PrintClearanceSummaryPdf
        public string PrintClearanceSummaryPdf(PdfData pdfData)
        {
            try
            {    //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");
                pdfData.elementHtml = (string)JsonConvert.DeserializeObject(pdfData.elementHtml);
                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginBottom = 2;
                HtmlToPdf.PrintOptions.MarginLeft = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginRight = 2;

                HtmlToPdf.PrintOptions.Zoom = 85;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);

                string fileName = "Clearance-Summary-Accident-No" + pdfData.AccidentNo.Replace('/', '-') + "-CompanyName-" + pdfData.CompanyName + ".pdf";

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/summary-reports"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }
                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@AccidentID", Value = pdfData.AccidentID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "summary-reports/" + fileName},

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateClearanceSummaryUrl]", CommandType.StoredProcedure, sParameter));
                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/summary-reports"), fileName));
                return "summary-reports/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
            }
        }
        #endregion

        #region GetAccidentDamageParts
        public AccidentDamagePartsMeta GetAccidentDamageParts(int? AccidentID, int? DamagePointID, string SearchQuery,int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                AccidentDamagePartsMeta accidentDamagePartsMeta = new AccidentDamagePartsMeta();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID },
                        new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePointID },
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery },
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID }
                };

                using (dt = ADOManager.Instance.DataSet("[getAccidentDamageParts]", CommandType.StoredProcedure, sParameter))
                {
                    accidentDamagePartsMeta.DamagePoints = dt.Tables[0].AsEnumerable().Select(dm => new DamagePoint
                    {
                        DamagePointID = dm.Field<int>("DamagePointID"),
                        PointName = dm.Field<string>("PointName"),
                        PointNameArabic = dm.Field<string>("PointNameArabic"),

                    }).ToList();

                    accidentDamagePartsMeta.DamagePointParts = dt.Tables[1].AsEnumerable().Select(dm => new AutomotivePart
                    {
                        DamagePointID = dm.Field<string>("DamagePointID"),
                        //DamagePointPartID = dm.Field<int>("DamagePointPartID"),
                        //AutomotivePartID = dm.Field<int>("AutomotivePartID"),
                        //PartName = dm.Field<string>("PartName"),
                        Name1 = dm.Field<string>("Name1"),
                        Name2 = dm.Field<string>("Name2"),
                        Name3 = dm.Field<string>("Name3"),
                        Name4 = dm.Field<string>("Name4"),
                        Name5 = dm.Field<string>("Name5"),
                        Name6 = dm.Field<string>("Name6"),
                        Name7 = dm.Field<string>("Name7"),
                        Name8 = dm.Field<string>("Name8"),
                        ItemNumber = dm.Field<string>("ItemNumber"),
                        OptionCount = dm.Field<int>("OptionCount"),
                        AutomotivePartID = dm.Field<int>("AutomotivePartID"),
                        PartName = dm.Field<string>("PartName"),
                        IsModified = dm.Field<bool?>("IsModified"),
                        DamageName = dm.Field<string>("DamageName"),
                        FinalCode = dm.Field<string>("FinalCode")
                    }).ToList();

                }
                return accidentDamagePartsMeta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetDamagePartOptions
        public AccidentDamagePartsMeta GetDamagePartOptions(int DamagePointID, int ItemNumber, string Name1,int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                AccidentDamagePartsMeta accidentDamagePartsMeta = new AccidentDamagePartsMeta();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePointID },
                        new SqlParameter { ParameterName = "@ItemNumber" , Value = ItemNumber },
                        new SqlParameter { ParameterName = "@Name1" , Value = Name1 },
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID }
                };

                using (dt = ADOManager.Instance.DataSet("[getDamagePartOptions]", CommandType.StoredProcedure, sParameter))
                {
                    //accidentDamagePartsMeta.DamagePoints = dt.Tables[0].AsEnumerable().Select(dm => new DamagePoint
                    //{
                    //    DamagePointID = dm.Field<int>("DamagePointID"),
                    //    PointName = dm.Field<string>("PointName"),
                    //    PointNameArabic = dm.Field<string>("PointNameArabic"),

                    //}).ToList();

                    accidentDamagePartsMeta.DamagePointParts = dt.Tables[0].AsEnumerable().Select(dm => new AutomotivePart
                    {
                        AutomotivePartID = dm.Field<int>("AutomotivePartID"),
                        DamagePointID = dm.Field<string>("DamagePointID"),
                        PartName = dm.Field<string>("PartName"),
                        Name1 = dm.Field<string>("Name1"),
                        Name2 = dm.Field<string>("Name2"),
                        Name3 = dm.Field<string>("Name3"),
                        Name4 = dm.Field<string>("Name4"),
                        Name5 = dm.Field<string>("Name5"),
                        Name6 = dm.Field<string>("Name6"),
                        Name7 = dm.Field<string>("Name7"),
                        Name8 = dm.Field<string>("Name8"),
                        ItemNumber = dm.Field<string>("ItemNumber"),
                        OptionName = dm.Field<string>("OptionName"),
                        FinalCode = dm.Field<string>("FinalCode"),
                        IsModified = dm.Field<bool?>("IsModified")
                    }).ToList();

                }
                return accidentDamagePartsMeta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion

        #region GetClearanceSavingReport
        public List<Accident> GetClearanceSavingReport(int CompanyID, DateTime? SearchDateFrom, DateTime? SearchDateTo)
        {
            DataSet dt = new DataSet();
            try
            {
                List<Accident> accidents = new List<Accident>();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID },
                        new SqlParameter { ParameterName = "@SearchDateFrom" , Value = SearchDateFrom },
                        new SqlParameter { ParameterName = "@SearchDateTo" , Value = SearchDateTo },
                };

                using (dt = ADOManager.Instance.DataSet("[getClearanceSavingReport]", CommandType.StoredProcedure, sParameter))
                {
                    accidents = dt.Tables[0].AsEnumerable().Select(ac => new Accident
                    {
                        AccidentNo = ac.Field<string>("AccidentNo"),
                        VernishPrice = ac.Field<double?>("VernishPrice"),
                        AccidentID = ac.Field<int>("AccidentID"),
                        MechanicLabourPrice = ac.Field<double?>("MechanicLabourPrice"),
                        DamagePrice = ac.Field<double?>("DamagePrice"),
                        RentPrice = ac.Field<double?>("RentPrice"),
                        TotalLabourCost = ac.Field<double?>("TotalLabourCost"),
                        TotalLabourTime = ac.Field<int?>("TotalLabourTime"),
                        FinalPartsAmount = ac.Field<double?>("FinalPartsAmount"),
                        FinalLabourCost = ac.Field<double?>("FinalLabourCost"),
                        TotalPartsAmount = ac.Field<double?>("TotalPartsAmount"),
                        GasPrice = ac.Field<double?>("GasPrice"),
                        TotalAccidentCost = ac.Field<double?>("TotalAccidentCost"),
                        FinalAccidentCost = ac.Field<double?>("FinalAccidentCost"),
                        FinalLabourTime = ac.Field<int?>("FinalLabourTime"),
                        TotalOldPartsAmount = ac.Field<double?>("TotalOldPartsAmount"),
                        TotalMechanicLabourPrice = ac.Field<double?>("TotalMechanicLabourPrice"),
                        ClearanceModifiedByName = ac.Field<string>("ClearanceModifiedByName"),
                        ClearanceModifiedOn = ac.Field<DateTime?>("ClearanceModifiedOn"),
                        ClearanceCreatedOn = ac.Field<DateTime?>("ClearanceCreatedOn"),
                        ClearanceSummaryPdfUrl = ac.Field<string>("ClearanceSummaryPdfUrl"),
                        ClearanceCreatedByName = ac.Field<string>("ClearanceCreatedByName"),
                        ClearanceRoute = ac.Field<string>("ClearanceRoute"),
                        Saving = ac.Field<double?>("Saving"),
                    }).ToList();

                }
                return accidents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveAccidentIsPurchasing
        public string SaveAccidentIsPurchasing(int AccidentID, bool IsPurchasing, int ModifiedBy, string WorkshopDetails, int? WorkShopID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@IsPurchasing" , Value = IsPurchasing},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@WorkshopDetails" , Value = WorkshopDetails},
                        new SqlParameter { ParameterName = "@WorkShopID" , Value = WorkShopID},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveAccidentIsPurchasing]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Accident saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetCompanyHistoryAccidents
        public AccidentMetaData GetCompanyHistoryAccidents(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int PageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery)
        {
            DataSet dt = new DataSet();
            try
            {
                if (searchQuery == "null" || searchQuery == "")
                    searchQuery = null;

                var accidentData = new AccidentMetaData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID == 0 ? null : WorkshopID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@searchQuery" , Value = searchQuery},
                        new SqlParameter { ParameterName = "@AccidentTypeID" , Value = AccidentTypeID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getCompanyHistoryAccidents]", CommandType.StoredProcedure, sParameter))
                {
                    accidentData.Accidents = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {
                        AccidentID = acd.Field<int>("AccidentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        ICName = acd.Field<string>("Name"),
                        CPPhone = acd.Field<string>("CPPhone"),
                        CPName = acd.Field<string>("CPFirstName") + " " + acd.Field<string>("CPLastName"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        MakeName = acd.Field<string>("EnglishMakeName"),
                        ArabicMakeName = acd.Field<string>("ArabicMakeName"),
                        ModelCode = acd.Field<string>("EnglishModelName"),
                        YearCode = acd.Field<int>("YearCode"),
                        CreatedOn = acd.Field<DateTime>("CreatedOn"),
                        CreatedSince = acd.Field<string>("CreatedSince"),
                        ArabicModelName = acd.Field<string>("ArabicModelName"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        ImgURL = acd.Field<string>("ImgURL"),
                        WorkshopName = acd.Field<string>("WorkshopName"),
                        WorkshopCityName = acd.Field<string>("WorkshopCityName"),
                        WorkshopPhone = acd.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = acd.Field<string>("WorkshopAreaName"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        StatusID = acd.Field<Int16?>("StatusID"),
                        UserName = acd.Field<string>("UserName"),
                        IsDeleted = acd.Field<bool>("IsDeleted"),
                        InprogressRequestCount = acd.Field<int?>("InprogressRequestCount"),
                        TotalRequestCount = acd.Field<int?>("TotalRequestCount"),
                        ClearanceRoute = acd.Field<string>("ClearanceRoute"),
                        CloseReason = acd.Field<string>("CloseReason")
                    }).ToList();

                    //accidentData.Makes = dt.Tables[1].AsEnumerable().Select(make => new Make
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

                    //accidentData.Models = dt.Tables[2].AsEnumerable().Select(model => new Model
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

                    //accidentData.Years = dt.Tables[3].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    //accidentData.TotalPages = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalPages"]);
                    accidentData.TabInfoData = dt.Tables[1].AsEnumerable().Select(tbi => new TabInfo
                    {
                        OpenedAccidents = tbi.Field<int>("OpenedAccidents"),
                        ClosedAccidents = tbi.Field<int>("ClosedAccidents"),
                        DeletedAccidents = tbi.Field<int>("DeletedAccidents")

                    }).FirstOrDefault();
                }

                return accidentData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region ResetPassword

        public string ResetPassword(int UserID, string Password)
        {
            DataSet dt = new DataSet();
            try
            {
                var employee = new Employee();

                var sParameter = new List<SqlParameter>
                {
                      new SqlParameter {ParameterName = "@UserID", Value = UserID },
                      new SqlParameter {ParameterName = "@Password", Value = EncryptionDecryption.EncryptString(Password)},
                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[resetPassword]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : result.ToString();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion

        #region GetRequestLog
        public RequestLog GetRequestLog(string RequestNumber, int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {

                var requestLog = new RequestLog();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestNumber" , Value = RequestNumber},
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getRequestLog]", CommandType.StoredProcedure, sParameter))
                {
                    requestLog.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        DemandID = req.Field<int?>("DemandID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        OrderByName = req.Field<string>("OrderByName"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsCarRepairStarted = req.Field<bool?>("IsCarRepairStarted"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        CreatedByName = req.Field<string>("CreatedByName"),
                        ModifiedByName = req.Field<string>("ModifiedByName"),
                        CreatedByEmail = req.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = req.Field<string>("ModifiedByEmail"),
                        EventDateTime = req.Field<DateTime?>("EventDateTime"),
                        AllOffersPdfUrl = req.Field<string>("AllOffersPdfUrl"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        CompanyName = req.Field<string>("CompanyName"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        ReturnReason = req.Field<string>("ReturnReason"),

                    }).ToList();

                    requestLog.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        DemandID = rp.Field<int?>("DemandID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        PartRejectReason = rp.Field<string>("PartRejectReason"),
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        ApprovedByName = rp.Field<string>("ApprovedByName"),
                        ModifiedByName = rp.Field<string>("ModifiedByName"),
                        CreatedByEmail = rp.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = rp.Field<string>("ModifiedByEmail"),
                        EventDateTime = rp.Field<DateTime?>("EventDateTime"),
                        ApprovedOn = rp.Field<DateTime?>("ApprovedOn"),
                        IsDeleted = rp.Field<bool>("IsDeleted"),
                        ModifiedOn = rp.Field<DateTime?>("ModifiedOn"),

                    }).ToList();

                    requestLog.RequestTasks = dt.Tables[2].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskRejectReason = rt.Field<string>("TaskRejectReason"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),
                        CreatedByName = rt.Field<string>("CreatedByName"),
                        ModifiedByName = rt.Field<string>("ModifiedByName"),
                        CreatedByEmail = rt.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = rt.Field<string>("ModifiedByEmail"),
                        EventDateTime = rt.Field<DateTime?>("EventDateTime"),
                        TaskArabicTypeName = rt.Field<string>("TaskArabicTypeName"),
                        TaskTypeName = rt.Field<string>("TaskTypeName"),
                        IsDeleted = rt.Field<bool>("IsDeleted"),
                        CreatedOn = rt.Field<DateTime>("CreatedOn"),
                        ModifiedOn = rt.Field<DateTime?>("ModifiedOn"),
                    }).ToList();

                    requestLog.Accidents = dt.Tables[3].AsEnumerable().Select(acd => new Accident
                    {
                        AccidentID = acd.Field<int>("AccidentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        MakeName = acd.Field<string>("EnglishMakeName"),
                        ArabicMakeName = acd.Field<string>("ArabicMakeName"),
                        ModelCode = acd.Field<string>("EnglishModelName"),
                        YearCode = acd.Field<int>("YearCode"),
                        CreatedOn = acd.Field<DateTime>("CreatedOn"),
                        ArabicModelName = acd.Field<string>("ArabicModelName"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        WorkshopName = acd.Field<string>("WorkshopName"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        StatusID = acd.Field<Int16?>("StatusID"),
                        IsDeleted = acd.Field<bool>("IsDeleted"),
                        VernishPrice = acd.Field<double?>("VernishPrice"),
                        MechanicLabourPrice = acd.Field<double?>("MechanicLabourPrice"),
                        DamagePrice = acd.Field<double?>("DamagePrice"),
                        RentPrice = acd.Field<double?>("RentPrice"),
                        TotalLabourCost = acd.Field<double?>("TotalLabourCost"),
                        IsPurchasing = acd.Field<bool?>("IsPurchasing"),
                        TotalLabourTime = acd.Field<int?>("TotalLabourTime"),
                        FinalPartsAmount = acd.Field<double?>("FinalPartsAmount"),
                        FinalLabourCost = acd.Field<double?>("FinalLabourCost"),
                        TotalPartsAmount = acd.Field<double?>("TotalPartsAmount"),
                        GasPrice = acd.Field<double?>("GasPrice"),
                        TotalAccidentCost = acd.Field<double?>("TotalAccidentCost"),
                        FinalAccidentCost = acd.Field<double?>("FinalAccidentCost"),
                        FinalLabourTime = acd.Field<int?>("FinalLabourTime"),
                        TotalOldPartsAmount = acd.Field<double?>("TotalOldPartsAmount"),
                        TotalMechanicLabourPrice = acd.Field<double?>("TotalMechanicLabourPrice"),
                        ClearanceModifiedByName = acd.Field<string>("ClearanceModifiedByName"),
                        ClearanceModifiedOn = acd.Field<DateTime?>("ClearanceModifiedOn"),
                        ClearanceCreatedOn = acd.Field<DateTime?>("ClearanceCreatedOn"),
                        ClearanceSummaryPdfUrl = acd.Field<string>("ClearanceSummaryPdfUrl"),
                        ClearanceCreatedByName = acd.Field<string>("ClearanceCreatedByName"),
                        CreatedByName = acd.Field<string>("CreatedByName"),
                        ModifiedByName = acd.Field<string>("ModifiedByName"),
                        CreatedByEmail = acd.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = acd.Field<string>("ModifiedByEmail"),
                        EventDateTime = acd.Field<DateTime?>("EventDateTime"),
                        ModifiedOn = acd.Field<DateTime?>("ModifiedOn"),

                    }).ToList();

                    requestLog.AccidentParts = dt.Tables[4].AsEnumerable().Select(rp => new AccidentPart
                    {
                        AccidentPartID = rp.Field<int>("AccidentPartID"),
                        AccidentID = rp.Field<int>("AccidentID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsRequestCreated = rp.Field<bool?>("IsRequestCreated"),
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        ModifiedByName = rp.Field<string>("ModifiedByName"),
                        CreatedByEmail = rp.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = rp.Field<string>("ModifiedByEmail"),
                        EventDateTime = rp.Field<DateTime?>("EventDateTime"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        ModifiedOn = rp.Field<DateTime?>("ModifiedOn"),
                    }).ToList();

                    requestLog.AccidentNotes = dt.Tables[5].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),
                        CreatedByName = nt.Field<string>("CreatedByName"),
                        ModifiedByName = nt.Field<string>("ModifiedByName"),
                        CreatedByEmail = nt.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = nt.Field<string>("ModifiedByEmail"),
                        EventDateTime = nt.Field<DateTime?>("EventDateTime"),
                        CreatedOn = nt.Field<DateTime>("CreatedOn"),
                        ModifiedOn = nt.Field<DateTime?>("ModifiedOn"),
                        IsDeleted = nt.Field<bool>("IsDeleted"),
                    }).ToList();

                    requestLog.POApprovals = dt.Tables[6].AsEnumerable().Select(po => new POApproval
                    {
                        POApprovalID = po.Field<int>("POApprovalID"),
                        RequestID = po.Field<int>("RequestID"),
                        UserID = po.Field<int>("UserID"),
                        EmployeeID = po.Field<int?>("EmployeeID"),
                        LastName = po.Field<string>("LastName"),
                        FirstName = po.Field<string>("FirstName"),
                        Email = po.Field<string>("Email"),
                        IsApproved = po.Field<bool?>("IsApproved"),
                        ESignatureURL = po.Field<string>("ESignatureURL"),
                        CreatedByName = po.Field<string>("CreatedByName"),
                        ModifiedByName = po.Field<string>("ModifiedByName"),
                        CreatedByEmail = po.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = po.Field<string>("ModifiedByEmail"),
                        EventDateTime = po.Field<DateTime?>("EventDateTime"),
                        CreatedOn = po.Field<DateTime>("CreatedOn"),
                        ModifiedOn = po.Field<DateTime?>("ModifiedOn"),
                        IsDeleted = po.Field<bool>("IsDeleted"),
                    }).ToList();

                    requestLog.RequestedPartsImages = dt.Tables[7].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        CreatedByName = img.Field<string>("CreatedByName"),
                        ModifiedByName = img.Field<string>("ModifiedByName"),
                        CreatedByEmail = img.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = img.Field<string>("ModifiedByEmail"),
                        EventDateTime = img.Field<DateTime?>("EventDateTime"),
                        CreatedOn = img.Field<DateTime>("CreatedOn"),
                        ModifiedOn = img.Field<DateTime?>("ModifiedOn"),
                        IsDeleted = img.Field<bool>("IsDeleted"),
                        PartName = img.Field<string>("PartName"),
                        PartNameEnglish = img.Field<string>("PartNameEnglish"),

                    }).ToList();

                    requestLog.AccidentImages = dt.Tables[8].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                        CreatedByName = img.Field<string>("CreatedByName"),
                        ModifiedByName = img.Field<string>("ModifiedByName"),
                        CreatedByEmail = img.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = img.Field<string>("ModifiedByEmail"),
                        EventDateTime = img.Field<DateTime?>("EventDateTime"),
                        CreatedOn = img.Field<DateTime>("CreatedOn"),
                        ModifiedOn = img.Field<DateTime?>("ModifiedOn"),
                        IsDeleted = img.Field<bool>("IsDeleted"),
                    }).ToList();

                    requestLog.AccidentPartsImages = dt.Tables[9].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        CreatedByName = img.Field<string>("CreatedByName"),
                        ModifiedByName = img.Field<string>("ModifiedByName"),
                        CreatedByEmail = img.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = img.Field<string>("ModifiedByEmail"),
                        EventDateTime = img.Field<DateTime?>("EventDateTime"),
                        CreatedOn = img.Field<DateTime>("CreatedOn"),
                        ModifiedOn = img.Field<DateTime?>("ModifiedOn"),
                        IsDeleted = img.Field<bool>("IsDeleted"),
                        PartName = img.Field<string>("PartName"),
                        PartNameEnglish = img.Field<string>("PartNameEnglish"),

                    }).ToList();

                    requestLog.AccidentMarkers = dt.Tables[10].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),
                        CreatedByName = am.Field<string>("CreatedByName"),
                        ModifiedByName = am.Field<string>("ModifiedByName"),
                        CreatedByEmail = am.Field<string>("CreatedByEmail"),
                        ModifiedByEmail = am.Field<string>("ModifiedByEmail"),
                        EventDateTime = am.Field<DateTime?>("EventDateTime"),
                        CreatedOn = am.Field<DateTime>("CreatedOn"),
                        ModifiedOn = am.Field<DateTime?>("ModifiedOn"),


                    }).ToList();
                    requestLog.IsRequestWorkshopIC = Convert.ToBoolean(dt.Tables[11].Rows[0][0].ToString());
                }

                return requestLog;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region OnSuggestQuotationPrice
        public string OnSuggestQuotationPrice(int QuotationID, int ModifiedBy, double SuggestedPrice, int RequestID, int SupplierID, bool IsMatching, bool? ISCounterOfferAccepted, int? SuggestionOfferTime)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@SuggestedPrice" , Value = SuggestedPrice},
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@IsMatching" , Value = IsMatching},
                        new SqlParameter { ParameterName = "@ISCounterOfferAccepted" , Value = ISCounterOfferAccepted},
                        new SqlParameter { ParameterName = "@SuggestionOfferTime" , Value = SuggestionOfferTime},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onSuggestQuotationPrice]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Quotation saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnAcceptSuggestedPrice
        public string OnAcceptSuggestedPrice(int QuotationID, int ModifiedBy, bool IsSuggestionAccepted, int RequestID, int CompanyID, int SupplierID, double? CounterOfferPrice)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@IsSuggestionAccepted" , Value = IsSuggestionAccepted},
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@CounterOfferPrice" , Value = CounterOfferPrice}

                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onAcceptSuggestedPrice]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Quotation saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetPendingRequestCount
        public string GetPendingRequestCount(int AccidentID)
        {
            DataSet dt = new DataSet();
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[getPendingRequestCount]", CommandType.StoredProcedure, sParameter));
                return result >= 0 ? result.ToString() : DataValidation.dbError;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetRequestPendingParts
        public Object GetRequestPendingParts(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                // var requestedPart = new RequestedPart();
                List<RequestedPart> requestedParts = new List<RequestedPart>();
                List<RequestTask> requestedTask = new List<RequestTask>();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},

                    };

                using (dt = ADOManager.Instance.DataSet("[getRequestPendingParts]", CommandType.StoredProcedure, sParameter))
                {
                    requestedParts = dt.Tables[0].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        DemandID = rp.Field<int?>("DemandID"),
                        AutomotivePartName = rp.Field<string>("PartName"),
                        ConditionTypeName = rp.Field<string>("TypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        PartRejectReason = rp.Field<string>("PartRejectReason"),

                    }).ToList();

                    requestedTask = dt.Tables[1].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskTypeID = rt.Field<short>("TaskTypeID"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved")

                    }).ToList();

                    //requestedPart.requestedPart = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    //{
                    //    IsImgExist = rp.Field<int?>("IsImgExist")
                    //}).FirstOrDefault();
                    int isImgExist = Convert.ToInt32(dt.Tables[2].Rows[0]["IsImgExist"]);

                    Object data = new { RequestedParts = requestedParts, IsImgExist = isImgExist, RequestedTask = requestedTask };
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region UpdateICWorkshopStatus
        public string UpdateICWorkshopStatus(int ICWorkshopID, Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = ICWorkshopID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[updateICWorkshopStatus]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetWSICMetaData
        public WSICMeta GetWSICMetaData(int WorkshopID)
        {
            DataSet dt = new DataSet();
            try
            {

                var WSICMetaData = new WSICMeta();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getWSICMetaData]", CommandType.StoredProcedure, sParameter))
                {
                    WSICMetaData.ICWorkshops = dt.Tables[0].AsEnumerable().Select(pm => new ICWorkshop
                    {
                        CompanyID = pm.Field<int>("CompanyID"),
                        WorkshopID = pm.Field<int>("WorkshopID"),
                        CompanyName = pm.Field<string>("CompanyName"),
                        ICWorkshopID = pm.Field<int>("ICWorkshopID"),
                        CompanyCode = pm.Field<string>("CompanyCode"),
                        LogoURL = pm.Field<string>("LogoURL"),
                        AddressLine1 = pm.Field<string>("AddressLine1"),
                        AddressLine2 = pm.Field<string>("AddressLine2"),
                        CPFirstName = pm.Field<string>("CPFirstName"),
                        CPLastName = pm.Field<string>("CPLastName"),
                        CPPhone = pm.Field<string>("CPPhone"),
                        CPEmail = pm.Field<string>("CPEmail"),
                        IsApproved = pm.Field<bool>("IsApproved"),
                        StatusID = pm.Field<Int16>("StatusID"),
                        IsAIDraft = pm.Field<bool?>("IsAIDraft"),
                        IsLossDate = pm.Field<bool?>("IsLossDate"),
                        IsPoliceReportNumber = pm.Field<bool?>("IsPoliceReportNumber"),
                        IsMilage = pm.Field<bool?>("IsMilage"),
                        IsMilageImage = pm.Field<bool?>("IsMilageImage"),
                        IsVINImage = pm.Field<bool?>("IsVINImage"),
                        IsVIN = pm.Field<bool?>("IsVIN"),
                        AskForPartCondition = pm.Field<bool?>("AskForPartCondition")
                    }).ToList();

                    WSICMetaData.Companies = dt.Tables[1].AsEnumerable().Select(cmp => new Company
                    {
                        CompanyID = cmp.Field<int>("CompanyID"),
                        CompanyName = cmp.Field<string>("Name"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        CityID = cmp.Field<Int16?>("CityID"),
                        CountryID = cmp.Field<Int16?>("CountryID"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPositionID = cmp.Field<Int16?>("CPPositionID"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),

                    }).ToList();
                }

                return WSICMetaData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region RequestCompanyWork
        public string RequestCompanyWork(int WorkshopID, int CompanyID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = ADOManager.Instance.ExecuteScalar("[requestCompanyWork]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PrintPOPdf
        public string PrintPOPdf(PdfData pdfData)
        {
            //int errorOnLine = 0;

            try
            {
                //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //errorOnLine ++;
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");

                pdfData.elementHtml = HttpUtility.HtmlDecode(pdfData.elementHtml);
                pdfData.SupplierPdfs.ForEach(item =>
                {
                    item.elementHtml = HttpUtility.HtmlDecode(item.elementHtml);
                });
                //errorOnLine++;

                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                //errorOnLine++;

                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;

                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginBottom = 4;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginLeft = 4;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginRight = 2;
               
                var  footer = "<center><i>{page} of {total-pages}<i></center>";
                
                HtmlToPdf.PrintOptions.Footer = new HtmlHeaderFooter()
                {
                    Height = 10,
                    HtmlFragment = footer ,
                    DrawDividerLine = false
                };
                //errorOnLine++;
                HtmlToPdf.PrintOptions.Zoom = 90;

                //errorOnLine++;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);

                //errorOnLine++;
                string fileName = "po-" + pdfData.RequestNumber + ".pdf";

                //errorOnLine++;

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/po-reports"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }

                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/po-reports"), fileName));

                int count = pdfData.SupplierPdfs.Count();
                string[] supplierFileName = new string[count];
                string[] supplierFileName2 = new string[count];

                FileInfo[] suppliersFiles = new FileInfo[count];


                for (int i = 0; i < count; i++)
                {
                    supplierFileName[i] = "po-" + pdfData.SupplierPdfs[i].RequestNumber + "-" + pdfData.SupplierPdfs[i].SupplierID + ".pdf";
                    supplierFileName2[i] = "po-reports/" + supplierFileName[i];
                    suppliersFiles = di.GetFiles(supplierFileName[i])
                    .Where(p => p.Extension == ".pdf").ToArray();

                }

                foreach (FileInfo file in suppliersFiles)
                {
                    try
                    {
                        if (file.Name.Equals(suppliersFiles))
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }

                for (int i = 0; i < count; i++)
                {
                    PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.SupplierPdfs[i].elementHtml);
                    PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/po-reports"), supplierFileName[i]));
                }

                List<PurchaseOrder> purchasesPdf = new List<PurchaseOrder>();

                for (int i = 0; i < count; i++)
                {
                    var item1 = new PurchaseOrder();
                    item1.POPdfURL = supplierFileName2[i];
                    item1.SupplierID = pdfData.SupplierPdfs[i].SupplierID;
                    purchasesPdf.Add(item1);
                }


                var XMLPurchaseOrder = purchasesPdf != null ? purchasesPdf.ToXML("ArrayOfPurchaseOrder") : null;

                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@RequestID", Value = pdfData.RequestID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "po-reports/" + fileName},
                    new SqlParameter { ParameterName = "@XMLPurchaseOrder" , Value = XMLPurchaseOrder},
                    new SqlParameter { ParameterName = "@CountryID" , Value = pdfData.CountryID}
                };


                var result = Convert.ToString(ADOManager.Instance.ExecuteScalar("[updaterequestPOPdfUrl]", CommandType.StoredProcedure, sParameter));
                //errorOnLine++;




                //errorOnLine++;

                return  result;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
                //return  errorOnLine.ToString();
            }
        }
        #endregion

        #region SavePONote
        public string SavePONote(Request request)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = request.RequestID},
                        new SqlParameter { ParameterName = "@PONote" , Value = request.PONote}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[savePONote]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region ReturnOrderedParts
        public double ReturnOrderedParts(QuotationPart quotationPart)
        {
            DataSet dt = new DataSet();
            try
            {
                double result;
                var sParameter = new List<SqlParameter>
                {
                     new SqlParameter { ParameterName = "@QuotationPartID" , Value = quotationPart.QuotationPartID},
                     new SqlParameter { ParameterName = "@ModifiedBy" , Value = quotationPart.ModifiedBy },
                };
                //int result = ADOManager.Instance.ExecuteNonQuery("[returnOrderedParts]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[returnOrderedParts]", CommandType.StoredProcedure, sParameter))
                {
                    result = Convert.ToDouble(dt.Tables[0].Rows[0][0].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        #region UndoReturnOrderedParts
        public double UndoReturnOrderedParts(QuotationPart quotationPart)
        {
            DataSet dt = new DataSet();
            try
            {
                double result;
                var sParameter = new List<SqlParameter>
                {
                     new SqlParameter { ParameterName = "@QuotationPartID" , Value = quotationPart.QuotationPartID},
                     new SqlParameter { ParameterName = "@ModifiedBy" , Value = quotationPart.ModifiedBy },
                };
                //int result = ADOManager.Instance.ExecuteNonQuery("[returnOrderedParts]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[undoreturnOrderedParts]", CommandType.StoredProcedure, sParameter))
                {
                    result = Convert.ToDouble(dt.Tables[0].Rows[0][0].ToString());
                }

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetTechnicalNotesData
        public TechnicalNotesData GetTechnicalNotesData(string AccidentNo, int ObjectTypeID)
        {
            DataSet dt = new DataSet();
            try
            {
                var technicalNotesData = new TechnicalNotesData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                        new SqlParameter { ParameterName = "@ObjectTypeID" , Value = ObjectTypeID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getTechnicalNotesData]", CommandType.StoredProcedure, sParameter))
                {



                    technicalNotesData.TechnicalNotes = dt.Tables[0].AsEnumerable().Select(tnote => new TechnicalNotes
                    {
                        TechnicalNoteID = tnote.Field<int>("TechnicalNoteID"),
                        AccidentNo = tnote.Field<string>("AccidentNo"),
                        ReportSubject = tnote.Field<string>("ReportSubject"),
                        FromEmployee = tnote.Field<string>("FromEmployee"),
                        ContractNumber = tnote.Field<string>("ContractNumber"),
                        DebitAccount = tnote.Field<double?>("DebitAccount"),
                        CreditBalance = tnote.Field<double?>("CreditBalance"),
                        ToEmployee = tnote.Field<string>("ToEmployee"),
                        InsuredAmount = tnote.Field<double?>("InsuredAmount"),
                        Repair = tnote.Field<bool?>("Repair"),
                        PrevioutAccidentRecord = tnote.Field<string>("PrevioutAccidentRecord"),
                        Premium = tnote.Field<double?>("Premium"),
                        Compensation = tnote.Field<double?>("Compensation"),
                        InsuredResult = tnote.Field<string>("InsuredResult"),
                        AccidentResponsibility = tnote.Field<int?>("AccidentResponsibility"),
                        KrokiSketch = tnote.Field<string>("KrokiSketch"),
                        PoliceReport = tnote.Field<string>("PoliceReport"),
                        Cause = tnote.Field<string>("Cause"),
                        IVDriverFault = tnote.Field<string>("IVDriverFault"),
                        TPLDriverFault = tnote.Field<string>("TPLDriverFault"),
                        ReverseCase = tnote.Field<string>("ReverseCase"),
                        PreliminaryAgreementInsured = tnote.Field<string>("PreliminaryAgreementInsured"),
                        DisagreementPoints = tnote.Field<string>("DisagreementPoints"),
                        ProposalSolution = tnote.Field<string>("ProposalSolution"),
                        LossAdjusterName = tnote.Field<string>("LossAdjusterName"),
                        LossAdjusterReportResult = tnote.Field<string>("LossAdjusterReportResult"),
                        TechnicalExplanation = tnote.Field<string>("TechnicalExplanation"),
                        CompensationProposal = tnote.Field<string>("CompensationProposal"),
                        Recovery = tnote.Field<double?>("Recovery"),
                        PaidCompensation = tnote.Field<double?>("PaidCompensation"),
                        FullPeriodPremium = tnote.Field<double?>("FullPeriodPremium"),
                        LossAdjusterReport = tnote.Field<double?>("LossAdjusterReport"),
                        CompensationCost = tnote.Field<double?>("CompensationCost"),
                        MarketValue = tnote.Field<double?>("MarketValue"),
                        ApproxSalvageAmount = tnote.Field<double?>("ApproxSalvageAmount"),
                        CompensationAmount = tnote.Field<double?>("CompensationAmount"),
                        SalvageBestAmount = tnote.Field<double?>("SalvageBestAmount"),
                        NetLoss = tnote.Field<double?>("NetLoss"),
                        InjuredName = tnote.Field<string>("InjuredName"),
                        InjuredAge = tnote.Field<int?>("InjuredAge"),
                        InjuredLevel = tnote.Field<string>("InjuredLevel"),
                        LegalOpenion = tnote.Field<string>("LegalOpenion"),
                        MedicalOpenion = tnote.Field<string>("MedicalOpenion"),
                        MedicalExplanation = tnote.Field<string>("MedicalExplanation"),
                        TechnicalProcedure = tnote.Field<string>("TechnicalProcedure"),
                        DisabilityCompensation = tnote.Field<double?>("DisabilityCompensation"),
                        TreatmentExpense = tnote.Field<double?>("TreatmentExpense"),
                        FutureOperation = tnote.Field<double?>("FutureOperation"),
                        FinalMedicalReportUrl = tnote.Field<string>("FinalMedicalReportUrl"),
                        PenaltyRulingDecisionUrl = tnote.Field<string>("PenaltyRulingDecisionUrl"),
                        RegionalCommitteUrl = tnote.Field<string>("RegionalCommitteUrl"),
                        ToDate = tnote.Field<DateTime?>("ToDate"),
                        FromDate = tnote.Field<DateTime?>("FromDate"),
                        CreatedOn = tnote.Field<DateTime?>("CreatedOn"),
                        AssignLossAdjuster = tnote.Field<bool?>("AssignLossAdjuster"),
                        Note = tnote.Field<string>("Note"),
                        ComVehicleOwnerName = tnote.Field<string>("ComVehicleOwnerName"),
                        ComPlateNo = tnote.Field<string>("ComPlateNo"),
                        ComCarMake = tnote.Field<string>("ComCarMake"),
                        ComCarModel = tnote.Field<string>("ComCarModel"),
                        ComCarYear = tnote.Field<string>("ComCarYear"),
                        CINote = tnote.Field<string>("CINote"),
                        TPLNote = tnote.Field<string>("TPLNote"),
                        TotalLossNote = tnote.Field<string>("TotalLossNote"),
                        HINote = tnote.Field<string>("HINote"),
                        ComAccidentNote = tnote.Field<string>("ComAccidentNote"),
                        ContractNotes = tnote.Field<string>("ContractNotes"),
                        ATCElabour = tnote.Field<double>("ATCElabour"),
                        ATCEsparePart = tnote.Field<double>("ATCEsparePart"),
                        ATCEValueLoss = tnote.Field<double>("ATCEValueLoss"),
                        ATCETortCompensation = tnote.Field<double>("ATCETortCompensation"),
                        ATElabour = tnote.Field<double>("ATElabour"),
                        ATEsparePart = tnote.Field<double>("ATEsparePart"),
                        ATEValueLoss = tnote.Field<double>("ATEValueLoss"),
                        ATETortCompensation = tnote.Field<double>("ATETortCompensation"),
                        TPReserves = tnote.Field<bool?>("TPReserves"),
                        ThirdTeamReserve1 = tnote.Field<double?>("ThirdTeamReserve1"),
                        ThirdTeamReserve2 = tnote.Field<double?>("ThirdTeamReserve2"),
                        ThirdTeamReserve3 = tnote.Field<double?>("ThirdTeamReserve3"),
                        ThirdTeamReserve4 = tnote.Field<double?>("ThirdTeamReserve4"),
                        ThirdTeamReserve5 = tnote.Field<double?>("ThirdTeamReserve5"),
                        BodilyHarm1 = tnote.Field<double?>("BodilyHarm1"),
                        BodilyHarm2 = tnote.Field<double?>("BodilyHarm2"),
                        BodilyHarm3 = tnote.Field<double?>("BodilyHarm3"),
                        BodilyHarm4 = tnote.Field<double?>("BodilyHarm4"),
                        BodilyHarm5 = tnote.Field<double?>("BodilyHarm5"),
                        CPReserve = tnote.Field<double?>("CPReserve"),
                        COMReserves = tnote.Field<bool?>("COMReserves"),
                    }).FirstOrDefault();

                    technicalNotesData.accidents = dt.Tables[1].AsEnumerable().Select(acc => new Accident
                    {
                        AccidentID = acc.Field<int>("AccidentID"),
                        PlateNo = acc.Field<string>("PlateNo"),
                        ArabicMakeName = acc.Field<string>("ArabicMakeName"),
                        ArabicModelName = acc.Field<string>("ArabicModelName"),
                        AccidentTypeID = acc.Field<short>("AccidentTypeID"),
                        YearCode = acc.Field<int>("YearCode"),
                        ICName = acc.Field<string>("ICName"),
                        LogoURL = acc.Field<string>("LogoURL"),
                        AccidentHappendOn = acc.Field<DateTime?>("AccidentHappendOn"),
                        FaultyCompanyName = acc.Field<string>("FaultyCompanyName"),
                        VehicleOwnerName = acc.Field<string>("VehicleOwnerName"),
                        ResponsibilityTypeID = acc.Field<int>("ResponsibilityTypeID")

                    }).ToList();

                    technicalNotesData.accidentMarker = dt.Tables[2].AsEnumerable().Select(acc => new AccidentMarker
                    {
                        DamagePointID = acc.Field<int>("DamagePointID"),
                        AccidentID = acc.Field<int>("AccidentID"),
                        AccidentTypeID = acc.Field<short>("AccidentTypeID")
                    }).ToList();

                    technicalNotesData.accidentCosts = dt.Tables[3].AsEnumerable().Select(acost => new AccidentCost
                    {
                        AccidentCostID = acost.Field<int?>("AccidentCostID"),
                        AccidentID = acost.Field<int>("AccidentID"),
                        Labour = acost.Field<double?>("Labour"),
                        SpareParts = acost.Field<double?>("SpareParts"),
                        ValueLoss = acost.Field<double?>("ValueLoss"),
                        CommercialDiscount = acost.Field<double?>("CommercialDiscount"),
                        Excess = acost.Field<double?>("Excess"),
                        Depreciation = acost.Field<double?>("Depreciation"),
                        NetLoss = acost.Field<double?>("NetLoss"),
                        RentPerDay = acost.Field<double?>("RentPerDay"),
                        NetCost = acost.Field<double?>("NetCost"),
                        Total = acost.Field<double?>("Total"),
                        AccidentTypeID = acost.Field<short?>("AccidentTypeID"),
                        TortCompensation = acost.Field<double?>("TortCompensation"),
                        ResponsibilityTypeID = acost.Field<int>("ResponsibilityTypeID"),
                        LabourTime = acost.Field<int?>("LabourTime"),
                        Percentage = acost.Field<double?>("Percentage"),
                        ValueLossNote = acost.Field<string>("ValueLossNote"),
                        OldSpareParts = acost.Field<double?>("OldSpareParts"),
                        SupplierName = acost.Field<string>("SupplierName"),
                        WorkshopName = acost.Field<string>("WorkshopName"),
                        RequestID = acost.Field<int?>("RequestID"),
                        CreatedByName = acost.Field<string>("CreatedByName"),
                        OtherExpenses = acost.Field<double?>("OtherExpenses"),
                        OtherExpensesNote = acost.Field<string>("OtherExpensesNote"),
                        IsApproved = acost.Field<bool?>("IsApproved"),
                        SerialNo = acost.Field<int?>("SerialNo"),
                        RequestRowNumber = acost.Field<int?>("RequestRowNumber"),
                        Refunds = acost.Field<double?>("Refunds"),
                        ChangePriceReason = acost.Field<string>("ChangePriceReason"),
                        NetVehicleCostNote = acost.Field<string>("NetVehicleCostNote"),
                        ResponsibilityPercentage = acost.Field<double?>("ResponsibilityPercentage"),
                        SurveyorName = acost.Field<string>("SurveyorName"),
                        TotalQuotationCount = acost.Field<int?>("TotalQuotationCount"),
                        TotalNotAvailableCount = acost.Field<int?>("TotalNotAvailableCount"),
                        IsChecked = acost.Field<bool?>("IsChecked"),
                        IsCheckedtemp = acost.Field<bool?>("IsChecked"),
                        IsPublished = acost.Field<bool?>("IsPublished"),
                        CreatedOn = acost.Field<DateTime?>("CreatedOn")




                    }).ToList();
                    technicalNotesData.TRApprovalEmployees = dt.Tables[4].AsEnumerable().Select(tr => new TRApproval
                    {
                        TRApprovalID = tr.Field<int>("TRApprovalID"),
                        UserID = tr.Field<int?>("UserID"),
                        AccidentNo = tr.Field<string>("AccidentNo"),
                        ObjectTypeID = tr.Field<int?>("ObjectTypeID"),
                        EmployeeID = tr.Field<int>("EmployeeID"),
                        LastName = tr.Field<string>("LastName"),
                        FirstName = tr.Field<string>("FirstName"),
                        Email = tr.Field<string>("Email"),
                        IsApproved = tr.Field<bool?>("IsApproved"),
                        CreatedOn = tr.Field<DateTime?>("CreatedOn"),
                        ModifiedOn = tr.Field<DateTime?>("ModifiedOn"),
                        MaxPrice = tr.Field<int?>("MaxPrice"),
                        Note = tr.Field<string>("Note"),
                        IsReturn = tr.Field<bool?>("IsReturn"),
                        ReturnNote = tr.Field<string>("ReturnNote"),
                        IsExist = tr.Field<bool?>("IsExist")


                    }).ToList();
                    technicalNotesData.TRApprovalLogEmployees = dt.Tables[5].AsEnumerable().Select(tr => new TRApproval
                    {
                        UserID = tr.Field<int>("UserID"),
                        EmployeeID = tr.Field<int>("EmployeeID"),
                        ReturnNote = tr.Field<string>("ReturnNote")
                    }).ToList();
                    technicalNotesData.CommonDamagePoints = dt.Tables[6].AsEnumerable().Select(tr => new DamagePoint
                    {
                        DamagePointID = tr.Field<int>("DamagePointID")
                    }).ToList();

                    technicalNotesData.accidentInfo = dt.Tables[7].AsEnumerable().Select(tr => new AccidentCost
                    {
                        AccidentID = tr.Field<int>("AccidentID"),
                        damagePointText = tr.Field<string>("damagePointText"),
                        PreviousAccident = tr.Field<bool?>("PreviousAccident"),
                        AccidentNote = tr.Field<string>("AccidentNote"),
                        IsApproved = tr.Field<bool?>("IsApproved")
                    }).ToList();
                    technicalNotesData.AccidentImages = dt.Tables[8].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        Description = img.Field<string>("Description"),
                        IsDocument = img.Field<bool?>("IsDocument"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                    }).ToList();


                }
                return technicalNotesData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        #region PrintTechnicalReport
        public string PrintCITechnicalReportPdf(PdfData pdfData)
        {
            try
            {    //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");
                pdfData.elementHtml = (string)JsonConvert.DeserializeObject(pdfData.elementHtml);
                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                                                       // HtmlToPdf.PrintOptions.MarginBottom = 0;
                HtmlToPdf.PrintOptions.MarginLeft = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginRight = 2;

                HtmlToPdf.PrintOptions.Zoom = 80;

                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);



                var allPageIndexes = Enumerable.Range(0, PDF.PageCount);
                var header = new HtmlHeaderFooter();
                header.HtmlFragment = "{page} of {total-pages}";

                // Example 1
                // Apply header to even page index only. (page number will be odd number because index start at 0 but page number start at 1)
                var evenPageIndexes = allPageIndexes.Where(i => i % 2 == 0);
                PDF.AddHTMLFooters(header, false, evenPageIndexes);

                var oddPageIndexes = allPageIndexes.Where(i => i % 2 != 0);
                PDF.AddHTMLFooters(header, true, oddPageIndexes);

                string fileName = "Technical-Report-Accident-No" + pdfData.AccidentNo.Replace('/', '-') + "-CompanyName-" + pdfData.CompanyName + ".pdf";

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/technical-reports"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }
                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@AccidentNo", Value = pdfData.AccidentNo},
                    new SqlParameter {ParameterName = "@StatusID", Value = pdfData.StatusID},
                    new SqlParameter {ParameterName = "@FileName", Value = "technica-report/" + fileName},

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateCITechnicalReportUrl]", CommandType.StoredProcedure, sParameter));
                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/technical-reports"), fileName));
                return "technical-reports/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
            }
        }
        #endregion

        #region SaveTechnicalReport
        public string SaveTechnicalReport(TechnicalNotesData technical)
        {
            try
            {

                var XMLAccidentCost = technical.accidentCosts.ToXML("ArrayOfAccidentsCost");
                var XMLAccidentCostInfo = technical.accidentInfo.ToXML("ArrayOfAccidentsCostInfo");

                var sParameter = new List<SqlParameter>
                    {
                         new SqlParameter { ParameterName = "@AccidentNo" , Value = technical.TechnicalNotes.AccidentNo},
                         new SqlParameter { ParameterName = "@XMLAccidentCost" , Value = XMLAccidentCost},
                         new SqlParameter { ParameterName = "@AgencyRepair" , Value = technical.TechnicalNotes.Repair},
                         new SqlParameter { ParameterName = "@CompanyID" , Value = technical.TechnicalNotes.CompanyID},
                         new SqlParameter { ParameterName = "@ObjectTypeID" , Value = technical.TechnicalNotes.ObjectTypeID},
                         new SqlParameter { ParameterName = "@TRTotalAmount" , Value = technical.TechnicalNotes.TRTotalAmount},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = technical.TechnicalNotes.ModifiedBy},
                         new SqlParameter { ParameterName = "@ComVehicleOwnerName" , Value = technical.TechnicalNotes.ComVehicleOwnerName},
                         new SqlParameter { ParameterName = "@ComPlateNo" , Value = technical.TechnicalNotes.ComPlateNo},
                         new SqlParameter { ParameterName = "@ComCarMake" , Value = technical.TechnicalNotes.ComCarMake},
                         new SqlParameter { ParameterName = "@ComCarModel" , Value = technical.TechnicalNotes.ComCarModel},
                         new SqlParameter { ParameterName = "@ComCarYear" , Value = technical.TechnicalNotes.ComCarYear},
                         new SqlParameter { ParameterName = "@CINote" , Value = technical.TechnicalNotes.CINote},
                         new SqlParameter { ParameterName = "@TPLNote" , Value = technical.TechnicalNotes.TPLNote},
                         new SqlParameter { ParameterName = "@TotalLossNote" , Value = technical.TechnicalNotes.TotalLossNote},
                         new SqlParameter { ParameterName = "@HINote" , Value = technical.TechnicalNotes.HINote},
                         new SqlParameter { ParameterName = "@ComAccidentNote" , Value = technical.TechnicalNotes.ComAccidentNote},
                         new SqlParameter { ParameterName = "@AccidentResponsibility" , Value = technical.TechnicalNotes.AccidentResponsibility},
                         new SqlParameter { ParameterName = "@XMLAccidentCostInfo" , Value = XMLAccidentCostInfo.Length>0?XMLAccidentCostInfo:null},
                         new SqlParameter { ParameterName = "@ContractNotes" , Value = technical.TechnicalNotes.ContractNotes},
                         new SqlParameter { ParameterName = "@TPReserves" , Value = technical.TechnicalNotes.TPReserves},
                         new SqlParameter { ParameterName = "@ThirdTeamReserve1" , Value = technical.TechnicalNotes.ThirdTeamReserve1},
                         new SqlParameter { ParameterName = "@ThirdTeamReserve2" , Value = technical.TechnicalNotes.ThirdTeamReserve2},
                         new SqlParameter { ParameterName = "@ThirdTeamReserve3" , Value = technical.TechnicalNotes.ThirdTeamReserve3},
                         new SqlParameter { ParameterName = "@ThirdTeamReserve4" , Value = technical.TechnicalNotes.ThirdTeamReserve4},
                         new SqlParameter { ParameterName = "@ThirdTeamReserve5" , Value = technical.TechnicalNotes.ThirdTeamReserve5},
                         new SqlParameter { ParameterName = "@BodilyHarm1" , Value = technical.TechnicalNotes.BodilyHarm1},
                         new SqlParameter { ParameterName = "@BodilyHarm2" , Value = technical.TechnicalNotes.BodilyHarm2},
                         new SqlParameter { ParameterName = "@BodilyHarm3" , Value = technical.TechnicalNotes.BodilyHarm3},
                         new SqlParameter { ParameterName = "@BodilyHarm4" , Value = technical.TechnicalNotes.BodilyHarm4},
                         new SqlParameter { ParameterName = "@BodilyHarm5" , Value = technical.TechnicalNotes.BodilyHarm5},
                         new SqlParameter { ParameterName = "@CPReserve" , Value = technical.TechnicalNotes.CPReserve},
                         new SqlParameter { ParameterName = "@COMReserves" , Value = technical.TechnicalNotes.COMReserves},


                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[SaveTechnicalReport]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Technical report saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region SaveBodyDamageReport
        public string SaveBodyDamageReport(TechnicalNotes technical)
        {
            try
            {
                List<TechnicalNotes> technicalNotes = new List<TechnicalNotes>();
                technicalNotes.Add(technical);
                var XMLBodyDamageCost = technicalNotes.ToXML("ArrayOfTechnicalNotes");

                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@AccidentNo" , Value = technical.AccidentNo},
                    new SqlParameter { ParameterName = "@XMLBodyDamageCost" , Value = XMLBodyDamageCost},
                    new SqlParameter { ParameterName = "@CompanyID", Value = technical.CompanyID },
                    new SqlParameter { ParameterName = "@ObjectTypeID", Value = technical.ObjectTypeID },
                    new SqlParameter { ParameterName = "@TRTotalAmount", Value = technical.TRTotalAmount },
                    new SqlParameter { ParameterName = "@ModifiedBy", Value = technical.ModifiedBy }


                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[SaveBodyDamageReport]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Technical report saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnApproveTechnicalReport
        public string OnApproveTechnicalReport(string AccidentNo, int ObjectTypeID, int TRApprovalID, int? ModifiedBy, bool? IsApproved, string Note, bool? IsReturn, string ReturnNote)
        {
            try
            {



                var sParameter = new List<SqlParameter>
                    {
                         new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                         new SqlParameter { ParameterName = "@ObjectTypeID" , Value = ObjectTypeID},
                         new SqlParameter { ParameterName = "@TRApprovalID" , Value = TRApprovalID},
                         new SqlParameter { ParameterName = "@IsApproved" , Value = IsApproved!= null? IsApproved : null},
                         new SqlParameter { ParameterName = "@Note" , Value = Note != null || Note != ""? Note : null},
                         new SqlParameter { ParameterName = "@IsReturn" , Value = IsReturn != null? IsReturn : null},
                         new SqlParameter { ParameterName = "@ReturnNote" , Value = ReturnNote != null || ReturnNote != ""? ReturnNote : null},

                    };

                var result = ADOManager.Instance.ExecuteScalar("[onApproveTechnicalReport]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region RejectedPartByWorkshop
        public List<QuotationPart> RejectedPartByWorkshop(QuotationPart RejectedPart)
        {
            try
            {
                DataSet dt = new DataSet();

                string s = null;
                var XMLRejectedPartImg = s;
                if (RejectedPart.RejectedPartImage != null)
                {
                    XMLRejectedPartImg = RejectedPart.RejectedPartImage.ToXML("ArrayOfRejectedpartImg");
                }
                else
                {
                    XMLRejectedPartImg = null;
                }

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationPartID" , Value = RejectedPart.QuotationPartID},
                        new SqlParameter { ParameterName = "@IsWsAccepted " , Value = RejectedPart.IsWsAccepted},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = RejectedPart.ModifiedBy},
                        new SqlParameter { ParameterName = "@WsRejectionNote" , Value = RejectedPart.WsRejectionNote},
                        new SqlParameter { ParameterName = "@XMLRejectedPartImg" , Value = XMLRejectedPartImg},
                        new SqlParameter { ParameterName = "@RejectedWorkshopPartReasonID" , Value = RejectedPart.RejectedWorkshopPartReasonID},

                    };
                var OrderedParts = new List<QuotationPart>();

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter));
                //return result == 1 ? "accepted successfully" : "rejected successfully";

                //var result = ADOManager.Instance.ExecuteScalar("[acceptRequestedPart]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[rejectedPartByWorkshop]", CommandType.StoredProcedure, sParameter))
                {
                    OrderedParts = dt.Tables[0].AsEnumerable().Select(qp => new QuotationPart
                    {
                        QuotationPartID = qp.Field<int>("QuotationPartID"),
                        RequestedPartID = qp.Field<int>("RequestedPartID"),
                        DeliveryTime = qp.Field<byte?>("DeliveryTime"),
                        ConditionTypeID = qp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeName = qp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = qp.Field<string>("NewPartConditionTypeArabicName"),
                        OrderedQuantity = qp.Field<int?>("OrderedQuantity"),
                        AutomotivePartName = qp.Field<string>("PartName"),
                        ConditionTypeName = qp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = qp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = qp.Field<string>("NoteInfo"),
                        RejectReason = qp.Field<string>("RejectReason"),
                        IsAccepted = qp.Field<bool?>("IsAccepted"),
                        IsRecycled = qp.Field<bool?>("IsRecycled"),
                        IsReceived = qp.Field<bool?>("IsReceived"),
                        ReceivedDate = qp.Field<DateTime?>("ReceivedDate"),
                        SupplierName = qp.Field<string>("SupplierName"),
                        AutomotivePartArabicName = qp.Field<string>("PartNameArabic"),
                        ReferredPrice = qp.Field<double?>("ReferredPrice"),
                        OrderRowNumber = qp.Field<int>("OrderRowNumber"),
                        IsOrdered = qp.Field<bool?>("IsOrdered"),
                        OrderedOn = qp.Field<DateTime?>("OrderedOn"),
                        IsWsAccepted = qp.Field<bool?>("IsWsAccepted"),
                        WsRejectionNote = qp.Field<string>("WsRejectionNote"),
                        IsReturn = qp.Field<bool?>("IsReturn"),
                        RejectedWorkshopPartReasonID = qp.Field<int>("RejectedWorkshopPartReasonID")

                    }).ToList();
                }
                return OrderedParts;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetRepairOrder
        public RequestData GetRepairOrder(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                var requestData = new RequestData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},

                    };

                using (dt = ADOManager.Instance.DataSet("[getRepairOrder]", CommandType.StoredProcedure, sParameter))
                {
                    requestData.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        PlateNo = req.Field<string>("PlateNo"),
                        YearCode = req.Field<int>("YearCode"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        UserName = req.Field<string>("UserName"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("CreatedOnAccident"),
                        OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        PDFReport = req.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        DemandCount = req.Field<int?>("DemandCount"),
                        QuotationsCount = req.Field<int?>("QuotationsCount"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        WorkshopDetails = req.Field<string>("WorkshopDetails"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        PONote = req.Field<string>("PONote"),
                        IsLowestMatching = req.Field<bool?>("IsLowestMatching"),
                        ROPdfURL = req.Field<string>("ROPdfURL"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        ReferenceNo = req.Field<string>("ReferenceNo"),
                        PolicyNumber = req.Field<string>("PolicyNumber"),
                        section1 = req.Field<string>("section1"),
                        RepairOrderApprovedDate = req.Field<DateTime?>("RepairOrderApprovedDate") == null ? null : req.Field<DateTime>("RepairOrderApprovedDate").ToString("dd/MM/yyyy"),
                        SectionTwo = req.Field<string>("SectionTwo"),
                        SectionThree = req.Field<string>("SectionThree"),
                        SectionFour = req.Field<string>("SectionFour"),
                        Discount = req.Field<int>("Discount"),
                        VAT = req.Field<Double?>("VAT"),
                        VATValue = req.Field<Double?>("VATValue"),
                        TOTAL = req.Field<Double?>("TOTAL"),

                    }).FirstOrDefault();

                    requestData.OrderedParts = dt.Tables[1].AsEnumerable().Select(qp => new QuotationPart
                    {
                        QuotationPartID = qp.Field<int>("QuotationPartID"),
                        RequestedPartID = qp.Field<int>("RequestedPartID"),
                        DeliveryTime = qp.Field<byte?>("DeliveryTime"),
                        ConditionTypeID = qp.Field<Int16>("ConditionTypeID"),
                        NewPartConditionTypeName = qp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = qp.Field<string>("NewPartConditionTypeArabicName"),
                        OrderedQuantity = qp.Field<int?>("OrderedQuantity"),
                        AutomotivePartName = qp.Field<string>("PartName"),
                        ConditionTypeName = qp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = qp.Field<string>("ConditionTypeNameArabic"),
                        NoteInfo = qp.Field<string>("NoteInfo"),
                        RejectReason = qp.Field<string>("RejectReason"),
                        IsAccepted = qp.Field<bool?>("IsAccepted"),
                        IsRecycled = qp.Field<bool?>("IsRecycled"),
                        IsReceived = qp.Field<bool?>("IsReceived"),
                        ReceivedDate = qp.Field<DateTime?>("ReceivedDate"),
                        SupplierName = qp.Field<string>("SupplierName"),
                        SupplierPhoneNumber = qp.Field<string>("SupplierPhone"),
                        AutomotivePartArabicName = qp.Field<string>("PartNameArabic"),
                        ReferredPrice = qp.Field<double?>("ReferredPrice"),
                        OrderRowNumber = qp.Field<int>("OrderRowNumber"),
                        IsReturn = qp.Field<bool?>("IsReturn"),
                        SupplierID = qp.Field<int>("SupplierID"),
                        QuotationID = qp.Field<int>("QuotationID"),
                        IsWsAccepted = qp.Field<bool?>("IsWsAccepted"),
                        POPdfURL = qp.Field<string>("POPdfURL"),
                        PartStatus = qp.Field<Int16?>("PartStatus"),
                        QuotationStatus = qp.Field<Int16?>("QuotationStatus"),
                    }).ToList();

                    requestData.RequestTasks = dt.Tables[2].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskRejectReason = rt.Field<string>("TaskRejectReason"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),
                        LabourPriceWithoutDiscount = rt.Field<double?>("LabourPriceWithoutDiscount"),

                    }).ToList();

                    requestData.TasksApprovedBySignatures = dt.Tables[3].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("CreatedByName"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        CreatedOn = rp.Field<DateTime?>("CreatedOn"),

                    }).ToList();


                    requestData.AccidentMarkers = dt.Tables[4].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID")

                    }).ToList();

                    requestData.POApprovedSignatures = dt.Tables[5].AsEnumerable().Select(rp => new SurveyorsSignature
                    {
                        CreatedByName = rp.Field<string>("Name"),
                        ESignatureURL = rp.Field<string>("ESignatureURL"),
                        CreatedOn = rp.Field<DateTime?>("CreatedOn"),

                    }).ToList();

                }

                return requestData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region PrintROPdf
        public string PrintROPdf(PdfData pdfData)
        {
            //int errorOnLine = 0;

            try
            {
                //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //errorOnLine ++;
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");

                pdfData.elementHtml = HttpUtility.HtmlDecode(pdfData.elementHtml);
                //errorOnLine++;

                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                //errorOnLine++;

                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;

                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginBottom = 4;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginLeft = 4;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginRight = 2;
                //errorOnLine++;
                var footer = "";
                if (pdfData.CountryID == 2)
                {
                    footer = "<div style=\"text-align:center; color: grey;\"> <h4 >P.O.Box5282 Manama, Kingdom of Bahrain, Tel: +973.17-585222, Fax: +973.17-784847</h4></div>"; ;
                }
                else
                {
                    footer = "<center><i>{page} of {total-pages}<i></center>";
                }
                HtmlToPdf.PrintOptions.Footer = new HtmlHeaderFooter()
                {
                    Height = 15,
                    HtmlFragment = footer,
                    DrawDividerLine = false
                };
                //errorOnLine++;
                HtmlToPdf.PrintOptions.Zoom = 90;

                //errorOnLine++;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);

                //errorOnLine++;
                string fileName = "ro-" + pdfData.RequestNumber + ".pdf";

                //errorOnLine++;

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/ro-reports"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }

                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/ro-reports"), fileName));

                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@RequestID", Value = pdfData.RequestID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "ro-reports/" + fileName}

                };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updaterequestROPdfUrl]", CommandType.StoredProcedure, sParameter));
                //errorOnLine++;

                //errorOnLine++;
                 
                return "ro-reports/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
                //return  errorOnLine.ToString();
            }
        }
        #endregion

        #region GetAccidentCost
        public List<Request> GetAccidentCost(string AccidentNo, string StartDate, string EndDate)
        {
            DataSet dt = new DataSet();
            try
            {
                var request = new List<Request>();
                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                     new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                      new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},

                    };
                using (dt = ADOManager.Instance.DataSet("[getAccidentCost]", CommandType.StoredProcedure, sParameter))
                {
                    request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        RequestID = req.Field<int>("RequestID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        OrderOn = req.Field<DateTime?>("OrderOn"),
                        CompanyID = req.Field<int>("CompanyID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        TotalTaskAmount = req.Field<double?>("TotalTaskAmount")
                    }).ToList();
                }
                return request;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetICSuppliers
        public List<Supplier> GetICSuppliers(int CompanyID, string StartDate, string EndDate)
        {
            DataSet dt = new DataSet();
            try
            {
                var suppliers = new List<Supplier>();
                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                    new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},

                    };
                using (dt = ADOManager.Instance.DataSet("[getICSuppliers]", CommandType.StoredProcedure, sParameter))
                {
                    suppliers = dt.Tables[0].AsEnumerable().Select(sup => new Supplier
                    {
                        SupplierID = sup.Field<int>("SupplierID"),
                        SupplierName = sup.Field<string>("SupplierName"),
                        ContactPerson = sup.Field<string>("ContactPerson"),
                        IBAN = sup.Field<string>("IBAN"),
                        TotalPO = sup.Field<int>("TotalPO"),
                        TotalPOAmount = sup.Field<double>("TotalPOAmount"),
                        IsBlocked = sup.Field<bool>("IsBlocked"),
                        TotalRequestsReceived = sup.Field<int?>("TotalRequestsReceived"),
                        TotalRequestsApplied = sup.Field<int?>("TotalRequestsApplied"),
                        SuccessRate = sup.Field<double?>("SuccessRate"),
                        AccountNumber = sup.Field<string>("AccountNumber"),
                        AccountID = sup.Field<int?>("AccountID")

                    }).ToList();
                }
                return suppliers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateSupplierBlockStatus

        public string UpdateSupplierBlockStatus(int CompanyID, int SupplierID, int UserID, bool IsBlocked)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID },
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID },
                        new SqlParameter { ParameterName = "@IsBlocked" , Value = IsBlocked },
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID },


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateSupplierBlockStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Status updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
        #region GetSupplierWorkDetail
        public SupplierWorkDetail GetSupplierWorkDetail(int CompanyID, int SupplierID)
        {
            DataSet dt = new DataSet();
            try
            {
                var deatil = new SupplierWorkDetail();
                var sParameter = new List<SqlParameter>
                    {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},

                    };
                using (dt = ADOManager.Instance.DataSet("[getSupplierWorkDetail]", CommandType.StoredProcedure, sParameter))
                {
                    deatil.supplier = dt.Tables[0].AsEnumerable().Select(sup => new Supplier
                    {
                        SupplierID = sup.Field<int>("SupplierID"),
                        SupplierName = sup.Field<string>("SupplierName"),
                        CPPhone = sup.Field<string>("CPPhone"),
                        IBAN = sup.Field<string>("IBAN"),
                        ProfessionLicenseImg = sup.Field<string>("ProfessionLicenseImg"),
                        CommercialRegisterImg = sup.Field<string>("CommercialRegisterImg"),
                        CPFirstName = sup.Field<string>("CPFirstName"),
                        CPLastName = sup.Field<string>("CPLastName"),
                        CPEmail = sup.Field<string>("CPEmail")

                    }).FirstOrDefault();



                    deatil.model = dt.Tables[1].AsEnumerable().Select(mo => new Model
                    {
                        ModelID = mo.Field<int>("ModelID"),
                        MakeID = mo.Field<int>("MakeID"),
                        ArabicModelName = mo.Field<string>("ArabicModelName"),
                        ModelCode = mo.Field<string>("EnglishModelName"),

                    }).ToList();
                    deatil.make = dt.Tables[2].AsEnumerable().Select(mk => new Make
                    {
                        MakeID = mk.Field<int>("MakeID"),
                        ArabicMakeName = mk.Field<string>("ArabicMakeName"),
                        MakeName = mk.Field<string>("EnglishMakeName"),

                    }).ToList();

                }
                return deatil;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetAllCompanies
        public List<Company> GetAllCompanies(int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Companies = new List<Company>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                    };
                using (dt = ADOManager.Instance.DataSet("[getAllCompanies]", CommandType.StoredProcedure, sParameter))
                {
                    
                    Companies = dt.Tables[0].AsEnumerable().Select(c => new Company
                    {
                        CompanyID = c.Field<int>("CompanyID"),
                        Name = c.Field<string>("Name")
                    }).ToList();

                    return Companies;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        #region GetICLivetQuotationData
        public DemandQuotations GetICLivetQuotationData(QuotationFilterModel model)
        {
            DataSet dt = new DataSet();
            try
            {
                DemandQuotations QuotationsFilter = new DemandQuotations();


                var XMLRequestedParts = model.RequestedParts != null && model.RequestedParts.Count() > 0 ? model.RequestedParts.ToXML("ArrayOfRequestedParts") : null;
                //var XMLManufacturers = model.PartManufacturers != null && model.PartManufacturers.Count() > 0 ? model.PartManufacturers.ToXML("ArrayOfManufacturers") : null;
                //var XMLManufacturerRegions = model.ManufacturerRegions != null && model.ManufacturerRegions.Count() > 0 ? model.ManufacturerRegions.ToXML("ArrayOfCountries") : null;
                var XMLCities = model.Cities != null && model.Cities.Count() > 0 ? model.Cities.ToXML("ArrayOfCities") : null;

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = model.RequestedParts.Count > 0 ? XMLRequestedParts : null},
                        new SqlParameter { ParameterName = "@XMLCities" , Value = model.Cities!=null && model.Cities.Count > 0 ? XMLCities : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturers" , Value =  model.PartManufacturers.Count > 0 ? XMLManufacturers : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturerRegions" , Value =  model.ManufacturerRegions.Count > 0 ? XMLManufacturerRegions : null},
                        new SqlParameter { ParameterName = "@RequestID" , Value = model.RequestID },
                        //new SqlParameter { ParameterName = "@IsReferred" , Value = model.IsReferred },
                        new SqlParameter { ParameterName = "@Availability", Value = model.Availability},
                        new SqlParameter { ParameterName = "@ConditionTypeID" , Value = model.ConditionTypeID },
                        new SqlParameter { ParameterName= "@NewConditionTypeID", Value=model.NewConditionTypeID},
                        new SqlParameter { ParameterName = "@MinPrice" , Value = model.MinPrice },
                        new SqlParameter { ParameterName = "@MaxPrice" , Value = model.MaxPrice },
                        new SqlParameter { ParameterName = "@SortByPrice" , Value = model.SortByPrice },
                        new SqlParameter { ParameterName = "@SortByRating" , Value = model.SortByRating },
                        new SqlParameter { ParameterName = "@SortByFillingRate" , Value = model.SortByFillingRate },
                        new SqlParameter { ParameterName = "@AreaName" , Value = model.AreaName },
                        new SqlParameter { ParameterName = "@IsPaid" , Value = model.IsPaid },
                        new SqlParameter { ParameterName = "@Price" , Value = model.Price },
                        new SqlParameter { ParameterName = "@MinFillingRate" , Value = model.MinFillingRate },
                        new SqlParameter { ParameterName = "@MaxFillingRate" , Value = model.MaxFillingRate },
                        new SqlParameter { ParameterName = "@Rating" , Value = model.Rating },

                };

                using (dt = ADOManager.Instance.DataSet("[getICLiveQuotationData]", CommandType.StoredProcedure, sParameter))
                {
                    QuotationsFilter.RequestInfo = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        GroupName = req.Field<string>("GroupName"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        CompanyID = req.Field<int>("CompanyID"),
                        VIN = req.Field<string>("VIN"),
                        PlateNo = req.Field<string>("PlateNo"),
                        YearCode = req.Field<int>("YearCode"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        DemandID = req.Field<int?>("DemandID"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        RejectionCount = req.Field<int?>("RejectionCount"),
                        UserName = req.Field<string>("UserName"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("CreatedOnAccident"),
                        OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        PDFReport = req.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        LogoURL = req.Field<string>("LogoURL"),
                        ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        DemandCount = req.Field<int?>("DemandCount"),
                        QuotationsCount = req.Field<int?>("QuotationsCount"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),

                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        CPName = req.Field<string>("CPName"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        CountryName = req.Field<string>("CountryName"),
                        CityName = req.Field<string>("CityName"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        IcCancelOrderNote = req.Field<string>("IcCancelOrderNote"),
                        JoCancelOrderNote = req.Field<string>("JoCancelOrderNote"),
                        DemandCreatedOn = req.Field<DateTime?>("DemandCreatedOn"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")
                    }).FirstOrDefault();

                    QuotationsFilter.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int>("Quantity"),
                        DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
                        DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        DemandedQuantity = rp.Field<int>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        DesiredManufacturerRegionID = rp.Field<Int16?>("DesiredManufacturerRegionID"),
                        DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved"),
                        //IsAccepted = rp.Field<bool?>("IsAccepted"),
                    }).ToList();

                    QuotationsFilter.Quotations = dt.Tables[2].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        StatusID = qt.Field<Int16?>("StatusID"),
                        StatusName = qt.Field<string>("StatusName"),
                        ArabicStatusName = qt.Field<string>("ArabicStatusName"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        CreatedSince = qt.Field<string>("CreatedSince"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings")

                    }).ToList();

                    QuotationsFilter.QuotationPartRef = dt.Tables[3].AsEnumerable().Select(rp => new QuotationPartRef
                    {
                        QuotationPartID = rp.Field<int?>("QuotationPartID"),
                        QuotationID = rp.Field<int?>("QuotationID"),
                        RequestedPartID = rp.Field<int?>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int?>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        //ManufacturerID = rp.Field<Int16?>("ManufacturerID"),
                        //ManufacturerName = rp.Field<string>("ManufacturerName"),
                        Price = rp.Field<double?>("Price"),
                        Quantity = rp.Field<int?>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        CityID = rp.Field<int?>("CityID"),
                        CityName = rp.Field<string>("CityName"),
                        BranchAreaName = rp.Field<string>("BranchAreaName"),
                        //ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID"),
                        //ManufacturerRegionName = rp.Field<string>("ManufacturerRegionName"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        WillDeliver = rp.Field<bool>("WillDeliver"),
                        DeliveryCost = rp.Field<decimal?>("DeliveryCost"),
                        OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                        IsAccepted = rp.Field<bool?>("IsAccepted"),
                        RowNumber = rp.Field<int>("RowNumber"),
                        OrderedOn = rp.Field<DateTime?>("OrderedOn"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        AcceptedByName = rp.Field<string>("AcceptedByName"),
                        WorkshopName = rp.Field<string>("WorkshopName"),
                        FillingRate = rp.Field<decimal?>("FillingRate"),
                        RequestPartCount = rp.Field<int?>("RequestPartCount"),
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                        IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                        ItemRank = rp.Field<int>("ItemRank"),
                        IsPartialSellings = rp.Field<bool?>("IsPartialSellings"),
                        IsReturn = rp.Field<bool?>("IsReturn"),


                    }).ToList();

                    QuotationsFilter.QuotationPartsImages = dt.Tables[4].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID")

                    }).ToList();

                    //QuotationsFilter.PartManufacturers = dt.Tables[5].AsEnumerable().Select(pm => new PartManufacturer
                    //{
                    //    PartManufacturerID = pm.Field<short>("ManufacturerID"),
                    //    ManufacturerName = pm.Field<string>("ManufacturerName"),
                    //    TotalQuotations = pm.Field<int>("TotalQuotations"),
                    //    IsSelected = pm.Field<bool?>("IsSelected"),

                    //}).ToList();

                    //QuotationsFilter.ManufacturerRegions = dt.Tables[6].AsEnumerable().Select(pm => new ManufacturerRegion
                    //{
                    //    ManufacturerRegionID = pm.Field<short>("ManufacturerRegionID"),
                    //    ManufacturerRegionName = pm.Field<string>("ManufacturerRegionName"),
                    //    TotalQuotations = pm.Field<int>("TotalQuotations"),
                    //    IsSelected = pm.Field<bool?>("IsSelected"),

                    //}).ToList();

                    QuotationsFilter.Cities = dt.Tables[5].AsEnumerable().Select(cty => new City
                    {
                        CityID = cty.Field<int>("CityID"),
                        CityName = cty.Field<string>("CityName"),
                        TotalQuotations = cty.Field<int>("TotalQuotations"),
                        IsSelected = cty.Field<bool?>("IsSelected"),
                        CityNameArabic = cty.Field<string>("CityNameArabic")

                    }).ToList();

                    QuotationsFilter.RequestedPartsImages = dt.Tables[6].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    QuotationsFilter.ReferredSupplierQuotations = dt.Tables[7].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        RequestID = qt.Field<int>("RequestID"),
                        CPEmail = qt.Field<string>("CPEmail"),
                        CPPhone = qt.Field<string>("CPPhone"),
                        Rating = qt.Field<byte>("Rating"),
                        JoReviewNote = qt.Field<string>("JoReviewNote"),
                        JoReviewStatusID = qt.Field<Int16?>("JoReviewStatusID"),
                        RequestPartCount = qt.Field<int?>("RequestPartCount"),
                        ReferredPartsCount = qt.Field<int?>("ReferredPartsCount"),
                        BranchAreaName = qt.Field<string>("BranchAreaName"),
                        BranchName = qt.Field<string>("BranchName"),
                        AddressLine1 = qt.Field<string>("AddressLine1"),
                        AddressLine2 = qt.Field<string>("AddressLine2"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        IsDiscountAvailable = qt.Field<bool?>("IsDiscountAvailable"),
                        IsNotAvailable = qt.Field<bool?>("IsNotAvailable"),
                        DiscountValue = qt.Field<double?>("DiscountValue"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        PaymentTypeID = qt.Field<Int16?>("PaymentTypeID"),
                        SuggestedPrice = qt.Field<double?>("SuggestedPrice"),
                        IsSuggestionAccepted = qt.Field<bool?>("IsSuggestionAccepted"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        MatchingFillingRate = qt.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = qt.Field<double?>("LowestOfferMatchingPrice"),
                        MatchingOfferSortNo = qt.Field<int>("MatchingOfferSortNo"),
                        OfferSortNo = qt.Field<int>("OfferSortNo"),
                        StatusID = qt.Field<short?>("StatusID"),
                        Comment = qt.Field<string>("Comment"),
                        SupplierType = qt.Field<Int16?>("SupplierType"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
                        IsPrioritySupplier = qt.Field<bool?>("IsPrioritySupplier")
                    }).ToList();

                    QuotationsFilter.AccidentMarkers = dt.Tables[8].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();

                    QuotationsFilter.RequestTasks = dt.Tables[9].AsEnumerable().Select(rt => new RequestTask
                    {
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        TaskRejectReason = rt.Field<string>("TaskRejectReason"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        RequestID = rt.Field<int?>("RequestID"),
                        TaskTypeID = rt.Field<Int16>("TaskTypeID"),
                        IsTaskApproved = rt.Field<bool?>("IsTaskApproved"),

                    }).ToList();

                    QuotationsFilter.Notes = dt.Tables[10].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),

                    }).ToList();

                    QuotationsFilter.AccidentImages = dt.Tables[11].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                    }).ToList();

                    QuotationsFilter.RequestedPartsPrice = dt.Tables[12].AsEnumerable().Select(r => new RequestedPart
                    {

                        RequestedPartID = r.Field<int>("RequestedPartID"),
                        MaxRequestedPartPrice = r.Field<double>("MaxRequestedPartPrice"),
                        MinRequestedPartPrice = r.Field<double>("MinRequestedPartPrice"),
                        AvgRequestedPartPrice = r.Field<double>("AvgRequestedPartPrice")

                    }).ToList();
                }
                return QuotationsFilter;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        #region SaveWorkshopProfile
        public string SaveWorkshopProfile(Workshop workshop)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = workshop.WorkshopID},
                        new SqlParameter { ParameterName = "@UserID" , Value = workshop.UserID},
                        new SqlParameter { ParameterName = "@WorkshopName" , Value =workshop.WorkshopName},
                        new SqlParameter { ParameterName = "@WorkshopPhone" , Value = workshop.WorkshopPhone},
                        new SqlParameter { ParameterName = "@LogoURL" , Value = workshop.ProfileImageURL},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveWorkshopProfile]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Profile saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region GetJCMainSeries

        public Object GetJCMainSeries(string MakeName, string ModelName)
        {
            DataSet dt = new DataSet();
            try
            {
                List<joClaimsSeriesData> joClaimsSeries = new List<joClaimsSeriesData>();
                List<joClaimsSeriesData> BodyCode = new List<joClaimsSeriesData>();
                List<joClaimsSeriesData> FuelType = new List<joClaimsSeriesData>();
                List<joClaimsSeriesData> Images = new List<joClaimsSeriesData>();
                int MinYear = 0;
                int MaxYear = 0;

                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@MakeName" , Value = MakeName },
                        new SqlParameter { ParameterName = "@ModelName" , Value = ModelName },

                };

                using (dt = ADOManager.Instance.DataSet("[getJCMainSeries]", CommandType.StoredProcedure, sParameter))
                {

                    joClaimsSeries = dt.Tables[0].AsEnumerable().Select(rt => new joClaimsSeriesData
                    {
                        JCSeriesID = rt.Field<string>("SeriesID"),
                        EnglishMakeName = rt.Field<string>("Car_make"),
                        EnglishModelName = rt.Field<string>("Car_model"),
                        ModelVariant = rt.Field<string>("Model_Variant"),
                        StartYear = rt.Field<int?>("Start_Year"),
                        EndYear = rt.Field<int?>("End_year"),
                        face_lift = rt.Field<bool?>("FaceLift"),
                    }).ToList();
                    BodyCode = dt.Tables[1].AsEnumerable().Select(rt => new joClaimsSeriesData
                    {
                        JCSeriesID = rt.Field<string>("SeriesID"),
                        BodyCode = rt.Field<string>("BodyCode"),
                    }).ToList();
                    FuelType = dt.Tables[2].AsEnumerable().Select(rt => new joClaimsSeriesData
                    {
                        JCSeriesID = rt.Field<string>("SeriesID"),
                        FuelType = rt.Field<string>("FuelType"),
                    }).ToList();
                    Images = dt.Tables[3].AsEnumerable().Select(rt => new joClaimsSeriesData
                    {
                        JCSeriesID = rt.Field<string>("SeriesID"),
                        ImageURL = rt.Field<string>("ImageURL"),
                    }).ToList();
                    MinYear = Convert.ToInt32(dt.Tables[4].Rows[0]["MinYear"]);
                    MaxYear = Convert.ToInt32(dt.Tables[4].Rows[0]["MaxYear"]);
                }
                Object data = new { joClaimsSeries = joClaimsSeries, BodyCode = BodyCode, FuelType = FuelType, Images = Images, MinYear = MinYear, MaxYear = MaxYear };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetJCMainSeriesAndJCSeriesCas

        public List<JCSeriesCase> GetJCMainSeriesAndJCSeriesCase(VehicleSeriesMeta joClaimsSeriesData, int? ProductionYear)
        {
            DataSet dt = new DataSet();
            try
            {
                var XMLJoClaimsSeries = joClaimsSeriesData.joClaimsSeries != null && joClaimsSeriesData.joClaimsSeries.Count() > 0 ? joClaimsSeriesData.joClaimsSeries.ToXML("ArrayOfJoClaimsSeries") : null;
                List<JCSeriesCase> jCSeriesCases = new List<JCSeriesCase>();
                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@XMLJoClaimsSeries" , Value = XMLJoClaimsSeries },
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = ProductionYear },

                };
                using (dt = ADOManager.Instance.DataSet("[getJCMainSeriesAndJCSeriesCase]", CommandType.StoredProcedure, sParameter))
                {
                    jCSeriesCases = dt.Tables[0].AsEnumerable().Select(am => new JCSeriesCase
                    {
                        JCSeriesID = am.Field<string>("JCSeriesID"),
                        CarImageEncryptedName = am.Field<string>("CarImageEncryptedName"),
                        FuelType = am.Field<string>("FuelType"),
                        OverLappingYear = am.Field<int?>("OverLappingYear"),
                        CorrectSeriesID = am.Field<string>("CorrectSeriesID"),
                        CarImageURL = am.Field<string>("CarImageURL"),
                        BodyType = am.Field<string>("BodyType"),
                        EnglishModelName = am.Field<string>("EnglishModelName"),
                    }).ToList();
                }
                return jCSeriesCases;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetJCSeriesCase

        public List<JCSeriesCase> GetJCSeriesCase(string JCSeriesID, int? YearCode)
        {
            DataSet dt = new DataSet();
            try
            {

                List<JCSeriesCase> jCSeriesCases = new List<JCSeriesCase>();
                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = JCSeriesID },
                        new SqlParameter { ParameterName = "@YearCode" , Value = YearCode == 0 ? null : YearCode },


                };
                using (dt = ADOManager.Instance.DataSet("[getJCSeriesCase]", CommandType.StoredProcedure, sParameter))
                {
                    jCSeriesCases = dt.Tables[0].AsEnumerable().Select(am => new JCSeriesCase
                    {
                        JCSeriesID = am.Field<string>("JCSeriesID"),
                        CarImageEncryptedName = am.Field<string>("CarImageEncryptedName"),
                        FuelType = am.Field<string>("FuelType"),
                        OverLappingYear = am.Field<int?>("OverLappingYear"),
                        CorrectSeriesID = am.Field<string>("CorrectSeriesID"),
                        CarImageURL = am.Field<string>("CarImageURL"),
                        BodyType = am.Field<string>("BodyType"),
                        EnglishModelName = am.Field<string>("EnglishModelName")
                    }).ToList();
                }
                return jCSeriesCases;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion


        #region DownloadCarseerImages
        public List<Gallery> DownloadCarseerImages()
        {
            DataSet dt = new DataSet();

            try
            {
                List<Gallery> galleryArray = new List<Gallery>();
                List<Gallery> galleryArray2 = new List<Gallery>();
                var XMLGallery = "";
                var XMLGallery2 = "";
                using (dt = ADOManager.Instance.DataSet("[getJCMainSeriesImages]", CommandType.StoredProcedure))
                {
                    galleryArray = dt.Tables[0].AsEnumerable().Select(img => new Gallery
                    {
                        SeriesID = img.Field<string>("JCSeriesID"),
                        ImageURL = img.Field<string>("ImageURL"),

                    }).ToList();
                    galleryArray2 = dt.Tables[1].AsEnumerable().Select(img => new Gallery
                    {
                        SeriesID = img.Field<string>("JCSeriesID"),
                        ImageURL2 = img.Field<string>("ImageURL2"),

                    }).ToList();
                    // Create a new WebClient instance.
                    System.Net.WebClient myWebClient = new System.Net.WebClient();

                    //myWebClient.DownloadFile("https://www.carseer.com/images/Cars_Images/14f2c791ff520dc5_e441e38e198f0a.jpg", @"D:\Technocares\Autoscore-jo\Code\InspectionAPI\BAL\CarseerImages\"+"14f2c791ff520dc5_e441e38e198f0a.jpg");

                    for (int index = 0; index < galleryArray.Count; index++)
                    {
                        var gid = Guid.NewGuid();
                        var filename = gid + "." + "jpg";
                        galleryArray[index].EncryptedName = filename;
                        // Download the Web resource and save it into the current filesystem folder.
                        if (!File.Exists(@"D:\project\Joclaims\Code\Joclaims_API\ShubeddakAPI\CarSeriesImages\" + galleryArray[index].ImageURL))
                        {
                            myWebClient.DownloadFile(galleryArray[index].ImageURL, @"D:\project\Joclaims\Code\Joclaims_API\ShubeddakAPI\CarSeriesImages\" + galleryArray[index].EncryptedName);
                        }
                        galleryArray[index].NewEncryptedName = "CarSeriesImages\\" + galleryArray[index].EncryptedName;

                    }
                    //for (int index = 0; index < galleryArray2.Count; index++)
                    //{
                    //    var gid = Guid.NewGuid();
                    //    var filename = gid + "." + "jpg";
                    //    galleryArray2[index].EncryptedName2 = filename;
                    //    // Download the Web resource and save it into the current filesystem folder.
                    //    if (!File.Exists(@"D:\project\Joclaims\Code\Joclaims_API\ShubeddakAPI\CarSeriesImages\" + galleryArray2[index].ImageURL2))
                    //    {
                    //        myWebClient.DownloadFile(galleryArray2[index].ImageURL2, @"D:\project\Joclaims\Code\Joclaims_API\ShubeddakAPI\CarSeriesImages\" + galleryArray2[index].EncryptedName2);
                    //    }
                    //    galleryArray2[index].NewEncryptedName2 = "CarSeriesImages\\" + galleryArray2[index].EncryptedName2;

                    //}
                    XMLGallery = galleryArray != null && galleryArray.Count() > 0 ? galleryArray.ToXML("ArrayOfGallery") : null;
                    XMLGallery2 = galleryArray2 != null && galleryArray2.Count() > 0 ? galleryArray2.ToXML("ArrayOfGallery2") : null;


                }
                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@XMLGallery" , Value = XMLGallery },
                        new SqlParameter { ParameterName = "@XMLGallery2" , Value = XMLGallery2 },

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveJCMainSeriesImages]", CommandType.StoredProcedure, sParameter));

                return galleryArray;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //Log.Error("Method in context GetGalleryImages(): " + ex.Message);
            }

        }
        #endregion
        #region GetImageCase

        public List<JCSeriesCase> GetImageCase(int AccidentID)
        {
            DataSet dt = new DataSet();
            try
            {

                List<JCSeriesCase> jCSeriesCases = new List<JCSeriesCase>();


                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID },


                };

                using (dt = ADOManager.Instance.DataSet("[getImageCase]", CommandType.StoredProcedure, sParameter))
                {

                    jCSeriesCases = dt.Tables[0].AsEnumerable().Select(am => new JCSeriesCase
                    {
                        JCSeriesID = am.Field<string>("JCSeriesID"),
                        CarImageEncryptedName = am.Field<string>("CarImageEncryptedName"),
                        FuelType = am.Field<string>("FuelType"),
                        OverLappingYear = am.Field<int?>("OverLappingYear"),
                        CorrectSeriesID = am.Field<string>("CorrectSeriesID"),
                        AccidentID = am.Field<int>("AccidentID"),
                        CarImageURL = am.Field<string>("CarImageURL"),
                    }).ToList();
                }

                return jCSeriesCases;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetTechnicalNotesLog
        public Object GetTechnicalNotesLog(string AccidentNo, string columnName, int? AccidentID, int? RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<Log> log = new List<Log>();
                var sParameter = new List<SqlParameter>
                {

                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo },
                        new SqlParameter { ParameterName = "@colName" , Value = columnName },
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID },
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID }

                };
                using (dt = ADOManager.Instance.DataSet("[getTechnicalNotesLog]", CommandType.StoredProcedure, sParameter))
                {
                    log = dt.Tables[0].AsEnumerable().Select(l => new Log
                    {
                        columnName = l.Field<string>("ValueText"),
                        ModifiedByName = l.Field<string>("ModifiedByName"),
                        EventDateTime = l.Field<DateTime?>("EventDateTime"),
                        ValueText2 = l.Field<string>("ValueText2")
                    }).ToList();



                    Object data = new { TechnicalNoteLog = log };

                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region OnAcceptCounterOffer
        public string OnAcceptCounterOffer(int QuotationID, int ModifiedBy, int RequestID, int CompanyID, int SupplierID, bool ISCounterOfferAccepted)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},

                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@ISCounterOfferAccepted" , Value = ISCounterOfferAccepted},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onAcceptCounterOffer]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Quotation saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region SaveRequestDraft
        public RequestResponse SaveRequestDraft(DraftData request)
        {
            try
             {
                DataSet dt = new DataSet();
                RequestResponse requestResponse = new RequestResponse();

                var XMLRequestedDraftParts = request.RequestDraftParts.ToXML("ArrayOfRequestDraftParts");

                var XMLRequestDraftImage = request.RequestDraftImage != null && request.RequestDraftImage.Count() > 0 ? request.RequestDraftImage.ToXML("ArrayOfRequestDraftImages") : null;
                var XMLRequestDraftTasks = request.RequestDraftTask != null && request.RequestDraftTask.Count() > 0 ? request.RequestDraftTask.ToXML("ArrayOfRequestDraftTasks") : null;
                var XMLRequestDraftPartImage = request.RequestDraftPartImage != null && request.RequestDraftPartImage.Count() > 0 ? request.RequestDraftPartImage.ToXML("ArrayOfRequestDraftPartImages") : null;
                var XMLDraftMarker = request.DraftMarkers != null && request.DraftMarkers.Count() > 0 ? request.DraftMarkers.ToXML("ArrayOfDraftMarker") : null;
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@XMLRequestDraftTasks" , Value = XMLRequestDraftTasks},
                        new SqlParameter { ParameterName = "@XMLRequestedDraftParts" , Value = XMLRequestedDraftParts},
                        new SqlParameter { ParameterName = "@XMLRequestDraftImage" , Value = XMLRequestDraftImage},
                        new SqlParameter { ParameterName = "@VIN" , Value = request.RequestDraft.VIN},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = request.RequestDraft.CompanyID},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = request.RequestDraft.AccidentID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = request.RequestDraft.MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = request.RequestDraft.ModelID},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = request.RequestDraft.CreatedBy},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = request.RequestDraft.ProductionYear},
                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = request.RequestDraft.EngineTypeID},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = request.RequestDraft.BodyTypeID},
                         new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = request.RequestDraft.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = request.RequestDraft.ImageCaseURL},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = request.RequestDraft.PlateNo},
                        new SqlParameter { ParameterName = "@XMLRequestDraftPartImage" , Value = XMLRequestDraftPartImage != null? XMLRequestDraftPartImage: null},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value =request.RequestDraft.ICWorkshopID},
                        new SqlParameter { ParameterName = "@XMLDraftMarker " , Value =XMLDraftMarker},
                        new SqlParameter { ParameterName = "@AIToken " , Value =request.RequestDraft.AIToken},
                        new SqlParameter { ParameterName = "@AICaseID " , Value =request.RequestDraft.AICaseID},
                        new SqlParameter { ParameterName = "@IsAIDraft " , Value =request.RequestDraft.IsAIDraft},
                        new SqlParameter { ParameterName = "@LossDate " , Value =request.RequestDraft.LossDate},
                        new SqlParameter { ParameterName = "@PoliceReportNumber" , Value =request.RequestDraft.PoliceReportNumber},
                        new SqlParameter { ParameterName = "@Milage" , Value =request.RequestDraft.Milage},
                        new SqlParameter { ParameterName = "@MilageUnit" , Value =request.RequestDraft.MilageUnit},
                        new SqlParameter { ParameterName = "@IsAgencyDraft" , Value =request.RequestDraft.IsAgencyDraft}

                    };

                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[SaveRequestDraft]", CommandType.StoredProcedure, sParameter))
                {
                    requestResponse.Result = Convert.ToString(dt.Tables[0].Rows[0]["Result"]);
                    requestResponse.DraftID = Convert.ToInt32(dt.Tables[0].Rows[0]["DraftID"]);
                    requestResponse.EmailTo = Convert.ToString(dt.Tables[0].Rows[0]["EmailTo"]);
                    requestResponse.EmailCC = Convert.ToString(dt.Tables[0].Rows[0]["EmailCC"]);
                    requestResponse.WorkshopName = Convert.ToString(dt.Tables[0].Rows[0]["WorkshopName"]);
                    requestResponse.VIN = Convert.ToString(dt.Tables[0].Rows[0]["VIN"]);
                    requestResponse.AccidentNo = Convert.ToString(dt.Tables[0].Rows[0]["AccidentNo"]);
                    requestResponse.VehicleOwnerName = Convert.ToString(dt.Tables[0].Rows[0]["VehicleOwnerName"]);
                    requestResponse.ArabicMakeName = Convert.ToString(dt.Tables[0].Rows[0]["ArabicMakeName"]);
                    requestResponse.ArabicModelName = Convert.ToString(dt.Tables[0].Rows[0]["ArabicModelName"]);
                    requestResponse.YearCode = Convert.ToInt32(dt.Tables[0].Rows[0]["YearCode"]);
                    requestResponse.AccidentID = Convert.ToInt32(dt.Tables[0].Rows[0]["AccidentID"]);
                    requestResponse.IsAIDraft = Convert.ToBoolean(dt.Tables[0].Rows[0]["IsAIDraft"]);
                    requestResponse.PoliceReportNumber = Convert.ToString(dt.Tables[0].Rows[0]["PoliceReportNumber"]);
                    requestResponse.LossDate = Convert.ToDateTime(dt.Tables[0].Rows[0]["LossDate"]).Date;
                    requestResponse.PolicyNumber = Convert.ToString(dt.Tables[0].Rows[0]["PolicyNumber"]);
                    requestResponse.PlateNo = Convert.ToString(dt.Tables[0].Rows[0]["PlateNo"]);
                    requestResponse.RepairDays = Convert.ToInt32(dt.Tables[0].Rows[0]["RepairDays"]);
                    requestResponse.CountryID = Convert.ToInt32(dt.Tables[0].Rows[0]["CountryID"]);
                    requestResponse.ReplacementCarFooter = Convert.ToString(dt.Tables[0].Rows[0]["ReplacementCarFooter"]);
                    requestResponse.ReplacementCarFooter = HttpUtility.HtmlDecode(requestResponse.ReplacementCarFooter);
                    requestResponse.EnglishMakeName = Convert.ToString(dt.Tables[0].Rows[0]["EnglishMakeName"]);
                    requestResponse.EnglishModelName = Convert.ToString(dt.Tables[0].Rows[0]["EnglishModelName"]);


                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetWorkshopDraftData
        public Object GetWorkshopDraftData(int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery)
        {
            try
            {
                DataSet dt = new DataSet();
                List<RequestDraft> requestDraft = new List<RequestDraft>();
                int PendingDraft = 0;
                int RequestCreated = 0;
                int RejectedDraft = 0;


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery != "undefined"?SearchQuery:null},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo}
                    };

                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[GetWorkshopDraftData]", CommandType.StoredProcedure, sParameter))
                {
                    requestDraft = dt.Tables[0].AsEnumerable().Select(am => new RequestDraft
                    {
                        DraftID = am.Field<int>("DraftID"),
                        AccidentID = am.Field<int?>("AccidentID"),
                        AccidentNo = am.Field<string>("AccidentNo"),
                        VehicleOwnerName = am.Field<string>("VehicleOwnerName"),
                        CompanyID = am.Field<int>("CompanyID"),
                        Name = am.Field<string>("Name"),
                        CPPhone = am.Field<string>("CPPhone"),
                        CPFirstName = am.Field<string>("CPFirstName"),
                        CPLastName = am.Field<string>("CPLastName"),
                        MakeID = am.Field<int>("MakeID"),
                        ModelID = am.Field<int>("ModelID"),
                        PlateNo = am.Field<string>("PlateNo"),
                        ProductionYear = am.Field<int>("ProductionYear"),
                        VIN = am.Field<string>("VIN"),
                        AccidentTypeID = am.Field<short?>("AccidentTypeID"),
                        AccidentTypeName = am.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = am.Field<string>("ArabicAccidentTypeName"),
                        CreatedOn = am.Field<DateTime>("CreatedOn"),
                        EnglishMakeName = am.Field<string>("EnglishMakeName"),
                        ArabicMakeName = am.Field<string>("ArabicMakeName"),
                        EnglishModelName = am.Field<string>("EnglishModelName"),
                        ArabicModelName = am.Field<string>("ArabicModelName"),
                        GroupName = am.Field<string>("GroupName"),
                        YearCode = am.Field<int>("YearCode"),
                        ImgURL = am.Field<string>("ImgURL"),
                        StatusID = am.Field<Int16>("StatusID"),
                        UserName = am.Field<string>("UserName"),
                        JCSeriesCode = am.Field<string>("JCSeriesCode"),
                        ImageCaseEncryptedName = am.Field<string>("ImageCaseEncryptedName"),
                        IsAccidentExist = am.Field<bool?>("IsAccidentExist"),
                        CreatedByName = am.Field<string>("CreatedByName"),
                        IsAIReport = am.Field<bool?>("IsAIReport"),
                        IsAIDraft = am.Field<bool?>("IsAIDraft"),
                        AICaseID = am.Field<string>("AICaseID"),
                        AIToken = am.Field<string>("AIToken"),
                        RejectDraftReason = am.Field<string>("RejectDraftReason"),
                        PoliceReportNumber = am.Field<string>("PoliceReportNumber"),
                        Milage=am.Field<int?>("Milage"),
                        MilageUnit=am.Field<int?>("MilageUnit"),
                        LossDate=am.Field<DateTime?>("LossDate"),
                        RestoreDraftReason=am.Field<string>("RestoreDraftReason"),
                        IsAgencyDraft=am.Field<int?>("IsAgencyDraft"),
                        IsDeductible = am.Field<bool?>("IsDeductible"),
                        DeductibleStatus = am.Field<int?>("DeductibleStatus")

                    }).ToList();

                    PendingDraft = Convert.ToInt32(dt.Tables[1].Rows[0]["PendingDraft"]);
                    RequestCreated = Convert.ToInt32(dt.Tables[1].Rows[0]["RequestCreated"]);
                    RejectedDraft = Convert.ToInt32(dt.Tables[1].Rows[0]["RejectedDraft"]);

                }

                Object data = new { draft = requestDraft, PendingDraft = PendingDraft, RequestCreated = RequestCreated, RejectedDraft = RejectedDraft };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region GetSingleRequestDraft
        public Object GetSingleRequestDraft(int DraftID)
        {
            try
            {
                DataSet dt = new DataSet();
                RequestDraft requestDraft = new RequestDraft();
                List<RequestDraftParts> parts = new List<RequestDraftParts>();
                List<RequestDraftTask> requestDraftTasks = new List<RequestDraftTask>();
                List<RequestDraftImage> requestDraftImages = new List<RequestDraftImage>();
                List<RequestDraftImage> requestDraftPartImage = new List<RequestDraftImage>();
                List<AccidentMarker> draftMarkers = new List<AccidentMarker>();


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@DraftID" , Value = DraftID}
                    };

                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[GetSingleRequestDraft]", CommandType.StoredProcedure, sParameter))
                {
                    requestDraft = dt.Tables[0].AsEnumerable().Select(am => new RequestDraft
                    {
                        DraftID = am.Field<int>("DraftID"),
                        LossDate=am.Field<DateTime?>("LossDate"),
                        Milage=am.Field<int?>("Milage"),
                        MilageUnit = am.Field<int?>("MilageUnit"),
                        MakeID = am.Field<int>("MakeID"),
                        ModelID = am.Field<int>("ModelID"),
                        PlateNo = am.Field<string>("PlateNo"),
                        VIN = am.Field<string>("VIN"),
                        AccidentID = am.Field<int?>("AccidentID"),
                        AccidentNo = am.Field<string>("AccidentNo"),
                        VehicleOwnerName = am.Field<string>("VehicleOwnerName"),
                        WorkshopID = am.Field<int?>("WorkshopID"),
                        WorkshopName = am.Field<string>("WorkshopName"),
                        EnglishMakeName = am.Field<string>("EnglishMakeName"),
                        ArabicMakeName = am.Field<string>("ArabicMakeName"),
                        EnglishModelName = am.Field<string>("EnglishModelName"),
                        ArabicModelName = am.Field<string>("ArabicModelName"),
                        Name = am.Field<string>("Name"),
                        CreatedOn = am.Field<DateTime>("CreatedOn"),
                        BodyTypeID = am.Field<short?>("BodyTypeID"),
                        BodyTypeName = am.Field<string>("BodyTypeName"),
                        BodyTypeArabicName = am.Field<string>("BodyTypeArabicName"),
                        AccidentTypeID = am.Field<short?>("AccidentTypeID"),
                        AccidentTypeName = am.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = am.Field<string>("AccidentTypeName"),
                        AccidentHappendOn = am.Field<DateTime?>("AccidentHappendOn"),
                        YearCode = am.Field<int>("YearCode"),
                        ProductionYear = am.Field<int>("ProductionYear"),
                        WorkshopAreaName = am.Field<string>("WorkshopAreaName"),
                        WorkshopPhone = am.Field<string>("WorkshopPhone"),
                        WorkshopCityName = am.Field<string>("WorkshopCityName"),
                        CreatedOnAccident = am.Field<DateTime?>("CreatedOnAccident"),
                        IsPurchasing = am.Field<bool?>("IsPurchasing"),
                        GroupName = am.Field<string>("GroupName"),
                        StatusID = am.Field<short>("StatusID"),
                        AICaseID = am.Field<string>("AICaseID"),
                        IsAIDraft = am.Field<bool?>("IsAIDraft"),
                        IsAIReport = am.Field<bool?>("IsAIReport"),
                        PoliceReportNumber = am.Field<string>("PoliceReportNumber"),
                        IsAgencyDraft = am.Field<int?>("IsAgencyDraft")

                    }).FirstOrDefault();

                    parts = dt.Tables[1].AsEnumerable().Select(am => new RequestDraftParts
                    {
                        DraftID = am.Field<int>("DraftID"),
                        DraftPartID = am.Field<int>("DraftPartID"),
                        AutomotivePartID = am.Field<int?>("AutomotivePartID"),
                        AutomotivePartName = am.Field<string>("PartName"),
                        Quantity = am.Field<int>("Quantity"),
                        ConditionTypeID = am.Field<short>("ConditionTypeID"),
                        ConditionTypeName = am.Field<string>("ConditionTypeName"),
                        ConditionTypeNameArabic = am.Field<string>("ConditionTypeName"),
                        NewPartConditionTypeID = am.Field<short?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = am.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = am.Field<string>("NewPartConditionTypeName"),
                        DamagePointID = am.Field<string>("DamagePointID"),
                        DamageName = am.Field<string>("DamageName"),
                        NoteInfo = am.Field<string>("NoteInfo"),
                        CreatedByName = am.Field<string>("CreatedByName"),
                        CreatedOn = am.Field<DateTime?>("CreatedOn"),
                        isExistInAccident = am.Field<bool?>("isExistInAccident"),
                        IsSelected = am.Field<bool?>("IsSelected"),
                        ItemNumber = am.Field<string>("ItemNumber"),
                        DraftRequestedPartPrice = am.Field<double?>("PartPrice")
                    }).ToList();

                    requestDraftImages = dt.Tables[2].AsEnumerable().Select(am => new RequestDraftImage
                    {
                        DraftImageID = am.Field<int>("DraftImageID"),
                        ImageURL = am.Field<string>("ImageURL"),
                        EncryptedName = am.Field<string>("EncryptedName"),
                        ObjectTypeID = am.Field<short?>("ObjectTypeID"),
                        ObjectID = am.Field<int?>("ObjectID"),
                        IsVideo = am.Field<bool>("IsVideo"),
                        CreatedOn = am.Field<DateTime?>("CreatedOn")


                    }).ToList();
                    requestDraftPartImage = dt.Tables[3].AsEnumerable().Select(am => new RequestDraftImage
                    {
                        DraftImageID = am.Field<int>("DraftImageID"),
                        ImageURL = am.Field<string>("ImageURL"),
                        EncryptedName = am.Field<string>("EncryptedName"),
                        ObjectTypeID = am.Field<short?>("ObjectTypeID"),
                        ObjectID = am.Field<int?>("ObjectID"),
                        IsVideo = am.Field<bool>("IsVideo")


                    }).ToList();

                    requestDraftTasks = dt.Tables[4].AsEnumerable().Select(am => new RequestDraftTask
                    {
                        DraftTaskID = am.Field<int>("DraftTaskID"),
                        DraftID = am.Field<int>("DraftID"),
                        TaskName = am.Field<string>("TaskName"),
                        TaskDescription = am.Field<string>("TaskDescription"),
                        TaskTypeID = am.Field<short>("TaskTypeID"),
                        TaskTypeName = am.Field<string>("TaskTypeName"),
                        TaskArabicTypeName = am.Field<string>("TaskArabicTypeName"),
                        LabourPrice = am.Field<double?>("LabourPrice"),
                        LabourTime = am.Field<int?>("LabourTime"),
                        CreatedOn = am.Field<DateTime>("CreatedOn"),
                        CreatedByName = am.Field<string>("CreatedByName"),
                        SurveyorPrice = am.Field<double?>("SurveyorPrice"),
                    }).ToList();

                    draftMarkers = dt.Tables[5].AsEnumerable().Select(dm => new AccidentMarker
                    {
                        DraftMarkerID = dm.Field<int>("DraftMarkerID"),
                        DraftID = dm.Field<int>("DraftID"),
                        IsDamage = dm.Field<bool>("IsDamage"),
                        DamagePointID = dm.Field<int>("DamagePointID"),
                        PointName = dm.Field<string>("PointName"),
                        PointNameArabic = dm.Field<string>("PointNameArabic"),
                    }).ToList();

                }

                Object data = new { draft = requestDraft, draftParts = parts, requestDraftTasks = requestDraftTasks, requestDraftImages = requestDraftImages, requestDraftPartImage = requestDraftPartImage, draftMarkers = draftMarkers };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region DeleteDraftPart
        public string DeleteDraftPart(int DraftPartID, int ModifiedBy, int DamagePintID)
        {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@DraftPartID" , Value = DraftPartID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePintID},
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[DeleteDraftPart]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Part deleted successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartsRequest
        public RequestResponse UpdateRequestDraft(DraftData request)
        {
            try
            {
                DataSet dt = new DataSet();
                RequestResponse requestResponse = new RequestResponse();
                var XMLRequestedDraftParts = request.RequestDraftParts.ToXML("ArrayOfRequestDraftParts");
                var XMLRequestDraftImage = request.RequestDraftImage != null && request.RequestDraftImage.Count() > 0 ? request.RequestDraftImage.ToXML("ArrayOfRequestDraftImages") : null;
                var XMLRequestDraftTasks = request.RequestDraftTask != null && request.RequestDraftTask.Count() > 0 ? request.RequestDraftTask.ToXML("ArrayOfRequestDraftTasks") : null;
                var XMLRequestDraftPartImage = request.RequestDraftPartImage != null && request.RequestDraftPartImage.Count() > 0 ? request.RequestDraftPartImage.ToXML("ArrayOfRequestDraftPartImages") : null;
                var XMLDraftMarker = request.DraftMarkers != null && request.DraftMarkers.Count() > 0 ? request.DraftMarkers.ToXML("ArrayOfDraftMarker") : null;

                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@XMLRequestDraftTasks" , Value = XMLRequestDraftTasks},
                        new SqlParameter { ParameterName = "@XMLRequestedDraftParts" , Value = XMLRequestedDraftParts},
                        new SqlParameter { ParameterName = "@XMLRequestDraftImage" , Value = XMLRequestDraftImage},
                        new SqlParameter { ParameterName = "@VIN" , Value = request.RequestDraft.VIN},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = request.RequestDraft.CompanyID},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = request.RequestDraft.AccidentID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = request.RequestDraft.MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = request.RequestDraft.ModelID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = request.RequestDraft.ModifiedBy},
                        new SqlParameter { ParameterName = "@ProductionYear" , Value = request.RequestDraft.ProductionYear},
                        new SqlParameter { ParameterName = "@EngineTypeID" , Value = request.RequestDraft.EngineTypeID},
                        new SqlParameter { ParameterName = "@BodyTypeID" , Value = request.RequestDraft.BodyTypeID},
                         new SqlParameter { ParameterName = "@ImageCaseEncryptedName" , Value = request.RequestDraft.ImageCaseEncryptedName},
                        new SqlParameter { ParameterName = "@ImageCaseURL" , Value = request.RequestDraft.ImageCaseURL},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = request.RequestDraft.PlateNo},
                        new SqlParameter { ParameterName = "@XMLRequestDraftPartImage" , Value = XMLRequestDraftPartImage != null? XMLRequestDraftPartImage: null},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value =request.RequestDraft.ICWorkshopID},
                        new SqlParameter { ParameterName = "@DraftID" , Value =request.RequestDraft.DraftID},
                        new SqlParameter { ParameterName = "@XMLDraftMarker" , Value = XMLDraftMarker},
                        new SqlParameter { ParameterName = "@IsAIDraft" , Value = request.RequestDraft.IsAIDraft},
                        new SqlParameter { ParameterName = "@LossDate" , Value = request.RequestDraft.LossDate},
                        new SqlParameter { ParameterName = "@PoliceReportNumber" , Value = request.RequestDraft.PoliceReportNumber},
                        new SqlParameter { ParameterName = "@Milage" , Value = request.RequestDraft.Milage},
                        new SqlParameter { ParameterName = "@MilageUnit" , Value = request.RequestDraft.MilageUnit},
                        new SqlParameter { ParameterName = "@IsAgencyDraft" , Value = request.RequestDraft.IsAgencyDraft}
                    };

                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[UpdateRequestDraft]", CommandType.StoredProcedure, sParameter))
                {
                    requestResponse.Result = Convert.ToString(dt.Tables[0].Rows[0]["Result"]);
                    requestResponse.DraftID = Convert.ToInt32(dt.Tables[0].Rows[0]["DraftID"]);

                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAccidentMarkers
        public List<AccidentMarker> GetAccidentMarkers()
        {
            try
            {
                DataSet dt = new DataSet();
                List<AccidentMarker> markers = new List<AccidentMarker>();





                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[GetAccidentMarkers]", CommandType.StoredProcedure))
                {
                    markers = dt.Tables[0].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage")
                    }).ToList();



                }


                return markers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SearchAccident
        public List<Accident> SearchAccident(string searchQuery, int CompanyID)
        {
            try
            {
                DataSet dt = new DataSet();
                List<Accident> accidents = new List<Accident>();


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@SearchQuery" , Value = searchQuery},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    };

                using (dt = ADOManager.Instance.DataSet("[SearchAccident]", CommandType.StoredProcedure, sParameter))
                {
                    accidents = dt.Tables[0].AsEnumerable().Select(ac => new Accident
                    {
                        AccidentID = ac.Field<int>("AccidentID"),
                        PlateNo = ac.Field<string>("PlateNo"),
                        AccidentNo = ac.Field<string>("AccidentNo"),
                        VIN = ac.Field<string>("VIN")
                    }).ToList();



                }


                return accidents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateDraftData
        public RequestResponse UpdateDraftData(int DraftID, int AccidentID, int ModifiedBy)
        {
            try
            {
                DataSet dt = new DataSet();

                RequestResponse requestResponse = new RequestResponse();


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@DraftID" , Value = DraftID},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };


                using (dt =  ADOManager.Instance.DataSet("[UpdateDraftData]", CommandType.StoredProcedure, sParameter))
                {
                    if (dt.Tables.Count > 0)
                    {
                        requestResponse.Result = Convert.ToString(dt.Tables[0].Rows[0]["Result"]);
                        requestResponse.DraftID = Convert.ToInt32(dt.Tables[0].Rows[0]["DraftID"]);
                        requestResponse.EmailTo = Convert.ToString(dt.Tables[0].Rows[0]["EmailTo"]);
                        requestResponse.WorkshopName = Convert.ToString(dt.Tables[0].Rows[0]["WorkshopName"]);
                        requestResponse.VIN = Convert.ToString(dt.Tables[0].Rows[0]["VIN"]);
                        requestResponse.AccidentNo = Convert.ToString(dt.Tables[0].Rows[0]["AccidentNo"]);
                        requestResponse.VehicleOwnerName = Convert.ToString(dt.Tables[0].Rows[0]["VehicleOwnerName"]);
                        requestResponse.ArabicMakeName = Convert.ToString(dt.Tables[0].Rows[0]["ArabicMakeName"]);
                        requestResponse.ArabicModelName = Convert.ToString(dt.Tables[0].Rows[0]["ArabicModelName"]);
                        requestResponse.YearCode = Convert.ToInt32(dt.Tables[0].Rows[0]["YearCode"]);
                        requestResponse.AccidentID = Convert.ToInt32(dt.Tables[0].Rows[0]["AccidentID"]);
                        requestResponse.IsAIDraft = Convert.ToBoolean(dt.Tables[0].Rows[0]["IsAIDraft"]);
                        requestResponse.PoliceReportNumber = Convert.ToString(dt.Tables[0].Rows[0]["PoliceReportNumber"]);
                        requestResponse.LossDate = Convert.ToDateTime(dt.Tables[0].Rows[0]["LossDate"]).Date;
                        requestResponse.PolicyNumber = Convert.ToString(dt.Tables[0].Rows[0]["PolicyNumber"]);
                        requestResponse.PlateNo = Convert.ToString(dt.Tables[0].Rows[0]["PlateNo"]);
                        requestResponse.RepairDays = Convert.ToInt32(dt.Tables[0].Rows[0]["RepairDays"]);
                        requestResponse.CountryID = Convert.ToInt32(dt.Tables[0].Rows[0]["CountryID"]);
                        requestResponse.ReplacementCarFooter = Convert.ToString(dt.Tables[0].Rows[0]["ReplacementCarFooter"]);
                        requestResponse.ReplacementCarFooter = HttpUtility.HtmlDecode(requestResponse.ReplacementCarFooter);
                        requestResponse.EnglishMakeName = Convert.ToString(dt.Tables[0].Rows[0]["EnglishMakeName"]);
                        requestResponse.EnglishModelName = Convert.ToString(dt.Tables[0].Rows[0]["EnglishModelName"]);

                    }
                   


                }

                return requestResponse;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetPendingDraft
        public Object GetPendingDraft(int StatusID, string VIN, int? WorkshopID)
        {
            try
            {
                DataSet dt = new DataSet();
                RequestDraft requestDraft = new RequestDraft();



                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@VIN" , Value = VIN},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                    };


                using (dt = ADOManager.Instance.DataSet("[GetPendingDraft]", CommandType.StoredProcedure, sParameter))
                {
                    requestDraft = dt.Tables[0].AsEnumerable().Select(am => new RequestDraft
                    {
                        DraftID = am.Field<int>("DraftID"),
                        AccidentID = am.Field<int?>("AccidentID"),
                        AccidentNo = am.Field<string>("AccidentNo"),
                        VehicleOwnerName = am.Field<string>("VehicleOwnerName"),
                        CompanyID = am.Field<int?>("CompanyID"),
                        Name = am.Field<string>("Name"),
                        CPPhone = am.Field<string>("CPPhone"),
                        CPFirstName = am.Field<string>("CPFirstName"),
                        CPLastName = am.Field<string>("CPLastName"),
                        MakeID = am.Field<int>("MakeID"),
                        ModelID = am.Field<int>("ModelID"),
                        PlateNo = am.Field<string>("PlateNo"),
                        ProductionYear = am.Field<int>("ProductionYear"),
                        VIN = am.Field<string>("VIN"),
                        AccidentTypeID = am.Field<short?>("AccidentTypeID"),
                        AccidentTypeName = am.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = am.Field<string>("ArabicAccidentTypeName"),
                        CreatedOn = am.Field<DateTime>("CreatedOn"),
                        EnglishMakeName = am.Field<string>("EnglishMakeName"),
                        ArabicMakeName = am.Field<string>("ArabicMakeName"),
                        EnglishModelName = am.Field<string>("EnglishModelName"),
                        ArabicModelName = am.Field<string>("ArabicModelName"),
                        GroupName = am.Field<string>("GroupName"),
                        YearCode = am.Field<int>("YearCode"),
                        ImgURL = am.Field<string>("ImgURL"),
                        StatusID = am.Field<Int16>("StatusID"),
                        UserName = am.Field<string>("UserName"),
                        JCSeriesCode = am.Field<string>("JCSeriesCode"),
                        ImageCaseEncryptedName = am.Field<string>("ImageCaseEncryptedName"),
                        IsAccidentExist = am.Field<bool?>("IsAccidentExist")


                    }).FirstOrDefault();





                }

                Object data = new { requestDraft = requestDraft };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateDraftStatus
        public string UpdateDraftStatus(int StatusID, int DraftID, int ModifiedBy, string RejectDraftReason,string RestoreDraftReason)
        {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@DraftID" , Value = DraftID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@RejectDraftReason" , Value = RejectDraftReason},
                        new SqlParameter { ParameterName = "@RestoreDraftReason" , Value = RestoreDraftReason}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[UpdateDraftStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Draft Updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAccident
        public List<Accident> GetAccident(string VIN, int? CompanyID)
        {
            try
            {
                DataSet dt = new DataSet();
                List<Accident> accident = new List<Accident>();



                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@VIN" , Value = VIN},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID}
                    };


                using (dt = ADOManager.Instance.DataSet("[GetAccident]", CommandType.StoredProcedure, sParameter))
                {
                    accident = dt.Tables[0].AsEnumerable().Select(ac => new Accident
                    {

                        AccidentID = ac.Field<int>("AccidentID"),
                        AccidentNo = ac.Field<string>("AccidentNo"),
                        CompanyID = ac.Field<int>("CompanyID"),
                        ICWorkshopID = ac.Field<int?>("ICWorkshopID"),
                        StatusID = ac.Field<short?>("StatusID"),
                        IsDeleted = ac.Field<bool>("IsDeleted"),
                        CreatedOnAccident = ac.Field<DateTime>("CreatedOn"),
                        EnglishMakeName = ac.Field<string>("EnglishMakeName"),
                        ArabicMakeName = ac.Field<string>("ArabicMakeName"),
                        EnglishModelName = ac.Field<string>("EnglishModelName"),
                        ArabicModelName = ac.Field<string>("ArabicModelName"),
                        PlateNo = ac.Field<string>("PlateNo"),
                        VIN = ac.Field<string>("VIN"),
                        CompanyName = ac.Field<string>("CompanyName")
                    }).ToList();
                }

                return accident;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAccidentForCloseRequest
        public Object GetAccidentForCloseRequest(int RequestID)
        {
            try
            {
                DataSet dt = new DataSet();
                Accident accident = new Accident();



                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID}
                    };


                using (dt = ADOManager.Instance.DataSet("[GetAccidentForCloseRequest]", CommandType.StoredProcedure, sParameter))
                {
                    accident = dt.Tables[0].AsEnumerable().Select(ac => new Accident
                    {

                        AccidentID = ac.Field<int>("AccidentID"),
                        AccidentNo = ac.Field<string>("AccidentNo"),
                        CompanyID = ac.Field<int>("CompanyID"),
                        ICWorkshopID = ac.Field<int?>("ICWorkshopID"),
                        StatusID = ac.Field<short?>("StatusID"),
                        IsDeleted = ac.Field<bool>("IsDeleted")
                    }).FirstOrDefault();
                }

                Object data = new { accident = accident };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region PublishLabourOnlyRequest
        public string PublishLabourOnlyRequest(PublishRequest publishRequest)
        {
            try
            {
                var XMLRequestedTasks = publishRequest.RequestedTask.ToXML("ArrayOfRequestedTask");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = publishRequest.RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = publishRequest.ModifiedBy},
                        new SqlParameter { ParameterName = "@XMLRequestedTasks" , Value = publishRequest.RequestedTask != null && publishRequest.RequestedTask.Count > 0 ? XMLRequestedTasks : null}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[PublishLabourOnlyRequest]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Request Status changed Successfully" : DataValidation.dbError;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetPriceReductionReport
        public Object GetPriceReductionReport(int CompanyID, string StartDate, string EndDate, int? PageNo, int? CountryID)
        {
            try
            {
                DataSet dt = new DataSet();
                List<PriceReductionReport> priceReduction = new List<PriceReductionReport>();
                int PageCount = 0;


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID},
                    };


                using (dt = ADOManager.Instance.DataSet("[getPriceReductionReport]", CommandType.StoredProcedure, sParameter))
                {
                    priceReduction = dt.Tables[0].AsEnumerable().Select(pr => new PriceReductionReport
                    {
                        AccidentID = pr.Field<int>("AccidentID"),
                        AccidentNo = pr.Field<string>("AccidentNo"),
                        RequestNumber = pr.Field<int>("RequestNumber"),
                        RequestCreatedDate = pr.Field<DateTime>("RequestCreatedDate"),
                        PurchaseOrderCreatedOn = pr.Field<DateTime>("PurchaseOrderCreatedOn"),
                        PurchaseOrderApprovedDate = pr.Field<DateTime>("PurchaseOrderApprovedDate"),
                        OldLowestOfferMatchingPrice = pr.Field<double?>("OldLowestOfferMatchingPrice"),
                        SuggestedPrice = pr.Field<double?>("SuggestedPrice"),
                        Savings = pr.Field<double?>("Savings"),
                        LowestOfferMatchingPrice = pr.Field<double?>("LowestOfferMatchingPrice"),
                        IsSuggestionAccepted = pr.Field<bool?>("IsSuggestionAccepted"),
                        CounterOfferPrice = pr.Field<double?>("CounterOfferPrice"),
                        ISCounterOfferAccepted = pr.Field<bool?>("ISCounterOfferAccepted"),
                        BrokerName = pr.Field<string>("BrokerName"),
                        ICUserName = pr.Field<string>("ICUserName")

                    }).ToList();
                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);

                }


                Object data = new { PriceReduction = priceReduction, PageCount = PageCount };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAccidentCarPartReport
        public Object GetAccidentCarPartReport(int CompanyID, int? PageNo, int? MakeID, int? ModelID, int? YearID, string StartDate, string EndDate,string PartName, int? CountryID)
        {
            try
            {
                DataSet dt = new DataSet();
                List<AccidentCarPartsPrice> carParts = new List<AccidentCarPartsPrice>();
                int PageCount = 0;


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@PartName" , Value = PartName},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                    };


                using (dt = ADOManager.Instance.DataSet("[getAccidentCarPartsReport]", CommandType.StoredProcedure, sParameter))
                {
                    carParts = dt.Tables[0].AsEnumerable().Select(pr => new AccidentCarPartsPrice
                    {
                        EnglishMakeName = pr.Field<string>("EnglishMakeName"),
                        ArabicMakeName = pr.Field<string>("ArabicMakeName"),
                        EnglishModelName = pr.Field<string>("EnglishModelName"),
                        ArabicModelName = pr.Field<string>("ArabicModelName"),
                        YearCode = pr.Field<int>("YearCode"),
                        PartName = pr.Field<string>("PartName"),
                        PartPrice = pr.Field<double?>("PartPrice"),
                        AccidentCount = pr.Field<int?>("AccidentCount"),

                    }).ToList();
                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);

                }


                Object data = new { CarPartsPrice = carParts, PageCount = PageCount };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region ReferTRApproval
        public string ReferTRApproval(int UserID, string AccidentNo, int ObjectTypeID, double Total)
        {
            try
            {
                DataSet dt = new DataSet();

                int PageCount = 0;


                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                        new SqlParameter { ParameterName = "@ObjectTypeID" , Value = ObjectTypeID},
                        new SqlParameter { ParameterName = "@Total" , Value = Total},

                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[ReferTRApproval]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Technical Report refer to higher authority" : DataValidation.dbError;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveAccount
        public Object SaveAccountNumber(Account Account)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccountID" , Value = Account.AccountID },
                        new SqlParameter { ParameterName = "@CompanyID" , Value = Account.CompanyID},
                        new SqlParameter { ParameterName = "@AccountNumber" , Value = Account.AccountNumber},
                        new SqlParameter { ParameterName = "@ObjectTypeID" , Value = Account.ObjectTypeID},
                        new SqlParameter { ParameterName = "@ClientID" , Value = Account.ClientID},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[SaveAccountNumber]", CommandType.StoredProcedure, sParameter));
                Object Response = new { Status = false };
                return result == null || result == 2627 ? Response = new { Status = false, Result = result } : Response = new { Status = true, Result = result };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region TRApprovalNoteLog
        public List<TRApproval> TRApprovalNoteLog(int UserID, int ObjectTypeID, string @AccidentNo)
        {
            try
            {
                DataSet dt = new DataSet();
                List<TRApproval> TRNoteLog = new List<TRApproval>();
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@ObjectTypeID" , Value = ObjectTypeID},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo}

                    };


                using (dt = ADOManager.Instance.DataSet("[GetTRApprovalLogByUserID]", CommandType.StoredProcedure, sParameter))
                {
                    TRNoteLog = dt.Tables[0].AsEnumerable().Select(log => new TRApproval
                    {
                        UserID = log.Field<int>("UserID"),
                        EmployeeID = log.Field<int>("EmployeeID"),
                        IsReturn = log.Field<bool?>("IsReturn"),
                        IsApproved = log.Field<bool?>("IsApproved"),
                        ModifiedOn = log.Field<DateTime?>("ModifiedOn"),
                        ReturnNote = log.Field<string>("ReturnNote"),
                        Note = log.Field<string>("Note")

                    }).ToList();

                }



                return TRNoteLog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetChequeData
        public Object GetChequeData(int AccidentID, int CompanyID)
        {
            try
            {
                DataSet dt = new DataSet();

                Accident accident = new Accident();
                Company company = new Company();

                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID}

                    };


                using (dt = ADOManager.Instance.DataSet("[GetChequeData]", CommandType.StoredProcedure, sParameter))
                {


                    company = dt.Tables[0].AsEnumerable().Select(a => new Company
                    {
                        CompanyID = a.Field<int>("CompanyID"),
                        Name = a.Field<string>("Name"),
                        Email = a.Field<string>("Email"),
                        CPPhone = a.Field<string>("CPPhone"),
                        LogoURL = a.Field<string>("LogoURL"),
                        AddressLine1 = a.Field<string>("AddressLine1"),
                        WebsiteAddress = a.Field<string>("WebsiteAddress"),
                        FaxNumber = a.Field<string>("FaxNumber")

                    }).FirstOrDefault();

                    accident = dt.Tables[1].AsEnumerable().Select(a => new Accident
                    {
                        AccidentID = a.Field<int>("AccidentID"),
                        VehicleOwnerName = a.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = a.Field<string>("OwnerPhoneNo"),
                        ChequeFreeText = a.Field<string>("ChequeFreeText"),
                        ChequeDate = a.Field<DateTime?>("ChequeDate"),
                        ChequeCreatedOn = a.Field<DateTime?>("ChequeCreatedOn"),
                        ChecqueApprovalSignature = a.Field<string>("ChecqueApprovalSignature"),
                        ChequePdfUrl = a.Field<string>("ChequePdfUrl"),
                        ArabicMakeName = a.Field<string>("ArabicMakeName"),
                        ArabicModelName = a.Field<string>("ArabicModelName"),
                        PlateNo = a.Field<string>("PlateNo"),
                        YearCode = a.Field<int>("YearCode"),
                        SignaturedBy = a.Field<int?>("SignaturedBy"),
                        SignaturedByName = a.Field<string>("SignaturedByName"),
                        //AccidentNo = a.Field<string>("AccidentNo")
                    }).FirstOrDefault();

                }
                var data = new { Accident = accident, Company = company };


                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateChequeDate
        public string UpdateChequeDate(ChequeData cheque)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentID" , Value = cheque.AccidentID},
                        new SqlParameter { ParameterName = "@FreeText" , Value = cheque.FreeText},
                        new SqlParameter { ParameterName = "@ChequeCreatedOn" , Value = cheque.ChequeCreatedOn},
                        new SqlParameter { ParameterName = "@ChequeDate" , Value = cheque.ChequeDate},
                        new SqlParameter { ParameterName = "@SignatureUrl" , Value = cheque.SignatureUrl},
                        new SqlParameter { ParameterName = "@SignaturedBy" , Value = cheque.SignaturedBy },

                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[UpdateChequeDate]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Saved Successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region PrintChequeDatePdf
        public string PrintChequeDatePdf(PdfData pdfData)
        {
            try
            {    //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");
                pdfData.elementHtml = (string)JsonConvert.DeserializeObject(pdfData.elementHtml);
                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginBottom = 2;
                HtmlToPdf.PrintOptions.MarginLeft = 2;  //millimeters
                HtmlToPdf.PrintOptions.MarginRight = 2;
                //var data = "?? ??";
                //HtmlToPdf.PrintOptions.Footer = new HtmlHeaderFooter()

                //{
                //    Height = 10,
                //    HtmlFragment = "<div> <i class='fas fa-circle print-circle'> </i>"+ data.ToString() +": <span>03218484709</span></div>",
                //    DrawDividerLine = false
                //};
                HtmlToPdf.PrintOptions.Zoom = 85;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);

                string fileName = "Cheque-Date-Accident-ID" + pdfData.AccidentID + ".pdf";

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/ChequeDate"));
                FileInfo[] files = di.GetFiles(fileName)
                     .Where(p => p.Extension == ".pdf").ToArray();
                foreach (FileInfo file in files)
                {
                    try
                    {
                        if (file.Name != fileName)
                        {
                            File.Delete(file.FullName);
                        }
                    }
                    catch { }
                }
                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@AccidentID", Value = pdfData.AccidentID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "ChequeDate/" + fileName},

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateChequePdfUrl]", CommandType.StoredProcedure, sParameter));
                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/ChequeDate"), fileName));
                return "ChequeDate/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
            }
        }
        #endregion


        #region GetAccidentDocuments
        public Object GetAccidentDocuments(string StartDate, string EndDate, int CompanyID, int? TabID, int? StatusID, int? PageNo, int? MakeID, int? ModelID, string searchQuery, int? LossAdjusterID)
        {
            try
            {
                DataSet dt = new DataSet();

                List<Image> images = new List<Image>();
                int GalleryCount;
                int DocumentsCount;
                int CarDocumentCount;
                int PurchaseOrderCount;
                int RepairOrderCount;
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@TabID" , Value = TabID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = searchQuery},
                        new SqlParameter { ParameterName = "@LossAdjusterID" , Value = LossAdjusterID}

                    };


                using (dt = ADOManager.Instance.DataSet("[GetAccidentDocuments]", CommandType.StoredProcedure, sParameter))
                {


                    images = dt.Tables[0].AsEnumerable().Select(a => new Image
                    {
                        ImageID = a.Field<int?>("ImageID"),
                        ImageURL = a.Field<string>("ImageURL"),
                        OriginalName = a.Field<string>("OriginalName"),
                        EncryptedName = a.Field<string>("EncryptedName"),
                        AccidentID = a.Field<int>("AccidentID"),
                        AccidentNo = a.Field<string>("AccidentNo"),
                        IsImage = a.Field<bool?>("IsImage"),
                        DocType = a.Field<int?>("DocType")


                    }).ToList();

                    GalleryCount = Convert.ToInt32(dt.Tables[1].Rows[0]["GalleryCount"]);
                    DocumentsCount = Convert.ToInt32(dt.Tables[1].Rows[0]["DocumentsCount"]);
                    CarDocumentCount = Convert.ToInt32(dt.Tables[1].Rows[0]["CarDocumentCount"]);
                    PurchaseOrderCount = Convert.ToInt32(dt.Tables[1].Rows[0]["PurchaseOrderCount"]);
                    RepairOrderCount = Convert.ToInt32(dt.Tables[1].Rows[0]["RepairOrderCount"]);

                }
                var data = new
                {
                    images = images,
                    GalleryCount = GalleryCount,
                    DocumentsCount = DocumentsCount,
                    CarDocumentCount = CarDocumentCount,
                    PurchaseOrderCount = PurchaseOrderCount,
                    RepairOrderCount = RepairOrderCount
                };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetSurveyorReport
        public Object GetSurveyorReport(string StartDate, string EndDate, int CompanyID, int? MakeID, int? ModelID, int? YearID, int? UserID)
        {
            try
            {
                DataSet dt = new DataSet();

                List<SurveyorRequestReport> surveyorRequests = new List<SurveyorRequestReport>();
                List<SurveyorRequestReport> surveyors = new List<SurveyorRequestReport>();
                List<SurveyorRequestCarsReport> surveyorRequestCarsReports = new List<SurveyorRequestCarsReport>();
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID}

                    };
                using (dt = ADOManager.Instance.DataSet("[SurveyorReportIC]", CommandType.StoredProcedure, sParameter))
                {
                    surveyorRequests = dt.Tables[0].AsEnumerable().Select(a => new SurveyorRequestReport
                    {
                        UserID = a.Field<int>("UserID"),
                        UserName = a.Field<string>("UserName"),
                        TotalRequests = a.Field<int?>("TotalRequests"),
                        TotalParts = a.Field<int?>("TotalParts"),
                        TotalLabourPrice = a.Field<double?>("TotalLaborPrice"),
                        ZeroLabour = a.Field<int?>("ZeroLabor"),
                        AverageLabour = a.Field<double?>("AverageLabor"),
                        AveragePartsPerRequest = a.Field<double?>("AveragePartsPerRequest"),

                    }).ToList();
                    surveyors = dt.Tables[1].AsEnumerable().Select(a => new SurveyorRequestReport
                    {
                        UserID = a.Field<int>("UserID"),
                        UserName = a.Field<string>("UserName"),


                    }).ToList();

                    surveyorRequestCarsReports = dt.Tables[2].AsEnumerable().Select(a => new SurveyorRequestCarsReport
                    {
                        UserID = a.Field<int>("UserID"),
                        MakeID = a.Field<int>("MakeID"),
                        EnglishMakeName = a.Field<string>("EnglishMakeName"),
                        TotalVehicleCount = a.Field<int>("TotalVehicleCount"),
                        VehicleCost = a.Field<double?>("VehicleCost"),
                        AverageCost = a.Field<double?>("AverageCost")
                    }).ToList();
                }
                var data = new
                {
                    Surveyor = surveyors,
                    SurveyorRequests = surveyorRequests,
                    SurveyorRequestCarsReports = surveyorRequestCarsReports
                };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetSurveyorDetailRequestReport
        public Object GetSurveyorDetailRequestReport(string StartDate, string EndDate, int CompanyID)
        {
            try
            {
                DataSet dt = new DataSet();

                List<SurveyorDetailRequestReport> SurveyorDetailRequestReport = new List<SurveyorDetailRequestReport>();
                List<SurveyorRequestReport> surveyors = new List<SurveyorRequestReport>();
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},


                    };
                using (dt = ADOManager.Instance.DataSet("[SurveyorDetailRequestReport]", CommandType.StoredProcedure, sParameter))
                {
                    SurveyorDetailRequestReport = dt.Tables[0].AsEnumerable().Select(a => new SurveyorDetailRequestReport
                    {
                        RequestID = a.Field<int>("RequestID"),
                        RequestNumber = a.Field<int>("RequestNumber"),
                        LabourPrice = a.Field<double>("LabourPrice"),
                        RequestedPartCount = a.Field<int>("RequestedPartCount"),
                        POTotalAmount = a.Field<double>("POTotalAmount"),
                        UserID = a.Field<int>("UserID"),
                        CreatedOn = a.Field<string>("CreatedOn"),
                        BrokerName = a.Field<string>("BrokerName")

                    }).ToList();
                    surveyors = dt.Tables[1].AsEnumerable().Select(a => new SurveyorRequestReport
                    {
                        UserID = a.Field<int>("UserID"),
                        UserName = a.Field<string>("UserName"),
                        TotalRequests = a.Field<int>("TotalRequestCount")

                    }).ToList();


                }
                var data = new
                {
                    Surveyor = surveyors,
                    SurveyorDetailRequestReport = SurveyorDetailRequestReport
                };

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region getaccidentdraft
        public AccidentMetaData getaccidentdraft(int CompanyID, int? PageNo, string SearchQuery, DateTime? StartDate, DateTime? EndDate, int? MakeID, int? ModelID, int? YearID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Accidents = new AccidentMetaData();
                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                    new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                    new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                    new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                    new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                    new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                    new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery},

                };

                using (dt = ADOManager.Instance.DataSet("[GetAccidentDraft]", CommandType.StoredProcedure, sParameter))
                {
                    Accidents.Accidents = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {
                        ClaimentID = acd.Field<int?>("ClaimentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        ICName = acd.Field<string>("ICName"),
                        CPPhone = acd.Field<string>("CPPhone"),
                        CPName = acd.Field<string>("CPFirstName") + " " + acd.Field<string>("CPLastName"),
                        MakeID = acd.Field<int?>("MakeID"),
                        ModelID = acd.Field<int?>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<int>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        MakeName = acd.Field<string>("MakeName"),
                        ModelCode = acd.Field<string>("ModelCode"),
                        YearCode = acd.Field<int>("YearCode"),
                        CreatedOn = acd.Field<DateTime>("CreatedOn"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        CreatedByName = acd.Field<string>("CreatedByName")
                    }).ToList();

                    Accidents.TabInfoData = dt.Tables[1].AsEnumerable().Select(tbi => new TabInfo
                    {
                        OpenedAccidents = tbi.Field<int>("OpenedAccidents"),
                        ClosedAccidents = tbi.Field<int>("ClosedAccidents"),
                        DeletedAccidents = tbi.Field<int>("DeletedAccidents"),
                        AccidentDraftCount = tbi.Field<int>("AccidentDraftCount"),

                    }).FirstOrDefault();





                }
                return Accidents;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region getaccidentdraft
        public Object getSingleAccidentDraft(int CompanyID, int ClaimentID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Accidents = new Accident();
                var AccidentMarkers = new List<AccidentMarker>();

                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@ClaimentID" , Value = ClaimentID},


                };

                using (dt = ADOManager.Instance.DataSet("[getSingleAccidentDraft]", CommandType.StoredProcedure, sParameter))
                {
                    Accidents = dt.Tables[0].AsEnumerable().Select(acd => new Accident
                    {
                        ClaimentID = acd.Field<int?>("ClaimentID"),
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        CompanyID = acd.Field<int>("CompanyID"),
                        MakeID = acd.Field<int?>("MakeID"),
                        ModelID = acd.Field<int?>("ModelID"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        ProductionYear = acd.Field<int>("ProductionYear"),
                        VIN = acd.Field<string>("VIN"),
                        MakeName = acd.Field<string>("MakeName"),
                        ModelCode = acd.Field<string>("ModelCode"),
                        AccidentHappendOn = acd.Field<DateTime?>("AccidentHappendOn"),
                        YearCode = acd.Field<int>("YearCode"),
                        VehicleOwnerName = acd.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = acd.Field<string>("OwnerPhoneNo"),
                        AccidentTypeID = acd.Field<Int16?>("AccidentTypeID"),
                        BodyTypeID = acd.Field<Int16?>("BodyTypeID"),
                        EngineTypeID = acd.Field<Int16?>("EngineTypeID"),
                        ResponsibilityTypeID = acd.Field<int>("ResponsibilityTypeID"),
                        IsPurchasing = acd.Field<bool?>("IsPurchasing"),
                        CarsInvolved = acd.Field<int?>("CarInvolved"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName"),
                        ResponsibilityTypeName = acd.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = acd.Field<string>("ArabicResponsibilityTypeName"),
                        EngineTypeName = acd.Field<string>("EngineTypeName"),
                        EngineTypeArabicName = acd.Field<string>("EngineTypeArabicName"),
                        BodyTypeName = acd.Field<string>("BodyTypeName"),
                        BodyTypeArabicName = acd.Field<string>("BodyTypeArabicName")
                    }).FirstOrDefault();

                    AccidentMarkers = dt.Tables[1].AsEnumerable().Select(mk => new AccidentMarker
                    {
                        AccidentID = mk.Field<int>("AccidentID"),
                        AccidentMarkerID = mk.Field<int>("AccidentMarkerID"),
                        IsDamage = mk.Field<bool>("IsDamage"),
                        PointName = mk.Field<string>("PointName"),
                        PointNameArabic = mk.Field<string>("PointNameArabic"),
                        DamagePointID = mk.Field<int>("DamagePointID")
                    }).ToList();




                }
                var data = new { Accidents = Accidents, AccidentMarkers = AccidentMarkers };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        public bool saveRequestTaskImage(List<Image> image, double? TotalPrice, int? RequestID,int? IsEnterLabourPartPriceChecked)
        {
            DataSet dt = new DataSet();
            try
            {
               
                var XMLsaveRequestTaskImage = image != null && image.Count > 0 ?image.ToXML("ArrayOfSaveRequestTaskImages"): null;

                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@XMLsaveRequestTaskImage" , Value = XMLsaveRequestTaskImage  },
                    new SqlParameter { ParameterName = "@TotalPrice" , Value = TotalPrice },
                    new SqlParameter { ParameterName = "@RequestID" , Value = RequestID },
                    new SqlParameter { ParameterName = "@IsEnterLabourPartPriceChecked" , Value = IsEnterLabourPartPriceChecked }

                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveRequestTaskImage]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool getDuplicateVinCheck(int CompanyID, string VIN)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                    new SqlParameter { ParameterName = "@VIN" , Value = VIN},


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[getDuplicateVinCheck]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public bool getInstantPriceBitUpdate(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@RequestID" , Value = RequestID}


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[getInstantPriceBitUpdate]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool savePhoneNumber(string VehicalOwnerPhoneNumber, int AccidentID)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@VehicalOwnerPhoneNumber" , Value = VehicalOwnerPhoneNumber},
                    new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID}


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[savePhoneNumber]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool updateAprrovalStatusInClearance(bool ISApproved, int ClearanceSummaryApprovalID, int UserID)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@ISApproved" , Value = ISApproved},
                    new SqlParameter { ParameterName = "@ClearanceSummaryApprovalID" , Value = ClearanceSummaryApprovalID},
                    new SqlParameter { ParameterName = "@UserID" , Value = UserID}


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateAprrovalStatusInClearance]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region publishRequest
        public bool publishRequest(int RequestID, int UserID)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    new SqlParameter { ParameterName = "@UserID" , Value = UserID}


                };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[publishRequest]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion



        #region getAIdraftData
        public InspektObj getAIdraftData(int DraftID)
        {
            DataSet dt = new DataSet();
            InspektObj inspektObj = new InspektObj();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@DraftID" , Value = DraftID}

                };
                using (dt = ADOManager.Instance.DataSet("[getAIdraftData]", CommandType.StoredProcedure, sParameter))
                {
                    inspektObj.CaseDamageReport = dt.Tables[0].AsEnumerable().Select(cr => new CaseDamageReport
                    {
                        CaseDamageReportID = cr.Field<int>("CaseDamageID"),
                        caseId = cr.Field<string>("CaseID"),
                        inspectionId = cr.Field<string>("InspectionId"),
                        vendor = cr.Field<string>("vendor"),
                        version = cr.Field<string>("version"),
                        EnglishMakeName = cr.Field<string>("EnglishMakeName"),
                        ArabicMakeName = cr.Field<string>("ArabicMakeName"),
                        ImgURL = cr.Field<string>("ImgURL"),
                        EnglishModelName = cr.Field<string>("EnglishModelName"),
                        ArabicModelName = cr.Field<string>("ArabicModelName")

                    }).FirstOrDefault();
                    inspektObj.RelevantImage = dt.Tables[1].AsEnumerable().Select(ri => new RelevantImage
                    {

                        RelevantImageID = ri.Field<int>("RelevantImageID"),
                        ImageID = ri.Field<string>("ImageID"),
                        OriginalImageID = ri.Field<string>("OriginalImageID"),
                        ImageURL = ri.Field<string>("ImageURL"),
                        OriginalImageURL = ri.Field<string>("OriginalImageURL"),
                        RequestImageURL = ri.Field<string>("RequestImageURL"),
                        QualityScore = ri.Field<int>("QualityScore"),
                        DetectedAngle = ri.Field<string>("DetectedAngle"),
                        CaseDamageID = ri.Field<int>("CaseDamageID"),
                        EncryptedName = ri.Field<string>("EncryptedName"),
                        isDeleted = ri.Field<bool?>("isDeleted")
                    }).ToList();
                    inspektObj.PreInspection = dt.Tables[2].AsEnumerable().Select(pi => new PreInspection
                    {
                        PreInspectionID = pi.Field<int>("PreInspectionID"),
                        recommendationStatus = pi.Field<string>("RecommendationStatus"),
                        message = pi.Field<string>("Message"),
                        CaseDamageID = pi.Field<int>("CaseDamageID")

                    }).FirstOrDefault();
                    inspektObj.DamagedPart = dt.Tables[3].AsEnumerable().Select(ri => new DamagedPart
                    {
                        DamagePartID = ri.Field<int>("DamagePartID"),
                        partName = ri.Field<string>("PartName"),
                        partNameArabic = ri.Field<string>("PartNameArabic"),
                        listOfDamages = ri.Field<string>("ListOfDamages"),
                        damageSeverityScore = ri.Field<string>("DamageSeverityScore"),
                        laborOperation = ri.Field<string>("LaborOperation"),
                        confidenceScore = ri.Field<string>("ConfidenceScore"),
                        paintLaborUnits = ri.Field<string>("PaintLaborUnits"),
                        removalRefitUnits = ri.Field<string>("RemovalRefitUnits"),
                        laborRepairUnits = ri.Field<string>("LaborRepairUnits"),
                        PreInspectionID = ri.Field<int>("PreInspectionID"),
                        ListOfDamagesAr = ri.Field<string>("ListOfDamageAr")
                    }).ToList();
                    inspektObj.DamagePoint = dt.Tables[4].AsEnumerable().Select(dp => new DamagePoint
                    {
                        DamagePointID = dp.Field<int>("DamagePointID"),
                        PointName = dp.Field<string>("PointName"),
                        PointNameArabic = dp.Field<string>("PointNameArabic"),
                        IsDamage = dp.Field<bool>("IsDamage")
                    }).ToList();
                    inspektObj.inpektlabResponse = dt.Tables[5].AsEnumerable().Select(inpektlabResponse => new InpektlabResponse
                    {
                        status = inpektlabResponse.Field<string>("AIResponseStatus"),
                        message = inpektlabResponse.Field<string>("AIResponseMessage")

                    }).FirstOrDefault();
                    //inspektObj.AdditionalFeature = dt.Tables[3].AsEnumerable().Select(af => new AdditionalFeature
                    //{
                    //    FeatureID = af.Field<int>("FeatureID"),
                    //    TypeID = af.Field<int>("TypeID"),
                    //    ImageId = af.Field<string>("ImageId"),
                    //    Width = af.Field<string>("Width"),
                    //    Height = af.Field<string>("Height"),
                    //    Depth = af.Field<string>("Depth"),
                    //    DamagedPartID = af.Field<int>("DamagedPartID"),
                    //    DamageSizeName = af.Field<string>("DamageSizeName"),
                    //    PreInspectionID = af.Field<int>("PreInspectionID")

                    //}).ToList();
                }
                return inspektObj;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region changePermissionStatus
        public bool changePermissionStatus(ICWorkshop workshop)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@ICWorkshopID" , Value = workshop.ICWorkshopID},
                        new SqlParameter { ParameterName = "@IsAIDraft" , Value = workshop.IsAIDraft},
                        new SqlParameter { ParameterName = "@UserID" , Value = workshop.UserID},
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[changeWorkshopPermissionStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetWorkshopAccidentData
        public Object GetWorkshopAccidentData(int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery)
        {
            try
            {
                DataSet dt = new DataSet();
                int AccidentCount;
                List<Accident> accidents = new List<Accident>();



                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery != "undefined"?SearchQuery:null},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo}
                    };

                //var result = ADOManager.Instance.ExecuteScalar("[savePartsRequest]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[GetWorkshopAccidentData]", CommandType.StoredProcedure, sParameter))
                {
                    accidents = dt.Tables[0].AsEnumerable().Select(accident => new Accident
                    {
                        AccidentID = accident.Field<int>("AccidentID"),
                        AccidentNo = accident.Field<string>("AccidentNo"),
                        VIN = accident.Field<string>("VIN"),
                        PlateNo = accident.Field<string>("PlateNo"),
                        MakeID = accident.Field<int?>("MakeID"),
                        EnglishMakeName = accident.Field<string>("EnglishMakeName"),
                        ArabicMakeName = accident.Field<string>("ArabicMakeName"),
                        ModelID = accident.Field<int?>("ModelID"),
                        EnglishModelName = accident.Field<string>("EnglishModelName"),
                        ArabicModelName = accident.Field<string>("ArabicModelName"),
                        ProductionYear = accident.Field<int>("ProductionYear"),
                        CompanyID = accident.Field<int?>("CompanyID"),
                        AccidentCreatedOn = accident.Field<DateTime?>("CreatedOn"),
                        IsDeleted = accident.Field<bool>("IsDeleted"),
                        ICWorkshopID = accident.Field<int?>("ICWorkshopID"),
                        StatusID = accident.Field<Int16?>("StatusID"),
                        StatusName = accident.Field<string>("StatusName")
                    }).ToList();


                    AccidentCount = Convert.ToInt32(dt.Tables[1].Rows[0]["AccidentCount"]);

                }

                Object data = new { accidents = accidents, accidentcount = AccidentCount };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region mapAccidentOnDraft
        public string mapAccidentOnDraft(int DraftID, int AccidentID, int ModifiedBy)
        {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@DraftID" , Value = DraftID},
                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[mapAccidentOnDraft]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Draft Updated successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region DownloadDraftDocs
        public Object downloadDraftDocs(int DraftID)
        {
            try
            {
                DataSet dt = new DataSet();
                int AccidentCount;
                RequestDraft draftdata = new RequestDraft();
                List<RequestDraftImage> carImages = new List<RequestDraftImage>();
                List<RequestDraftImage> carDocuments = new List<RequestDraftImage>();
                List<RequestDraftImage> CarVideos = new List<RequestDraftImage>();
                List<RequestDraftImage> CarVINMilage = new List<RequestDraftImage>();



                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@DraftID" , Value = DraftID}
                    };
                using (dt = ADOManager.Instance.DataSet("[GetDraftDocs]", CommandType.StoredProcedure, sParameter))
                {
                    draftdata = dt.Tables[0].AsEnumerable().Select(dr => new RequestDraft
                    {
                        DraftID = dr.Field<int>("DraftID"),
                        AccidentID = dr.Field<int?>("AccidentID"),
                        VIN = dr.Field<string>("VIN"),
                        MakeID = dr.Field<int?>("MakeID"),
                        ModelID = dr.Field<int?>("ModelID"),
                        LossDate = dr.Field<DateTime?>("LossDate"),
                        PoliceReportNumber = dr.Field<string>("PoliceReportNumber"),

                    }).FirstOrDefault();

                    carImages = dt.Tables[1].AsEnumerable().Select(image => new RequestDraftImage
                    {
                        DraftImageID = image.Field<int>("DraftImageID"),
                        ObjectID = image.Field<int?>("ObjectID"),
                        ObjectTypeID = image.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = image.Field<string>("EncryptedName"),
                        OriginalName = image.Field<string>("OriginalName"),
                        ImageURL = image.Field<string>("ImageURL"),
                        IsDeleted = image.Field<bool>("IsDeleted"),
                        IsVideo = image.Field<bool>("IsVideo")

                    }).ToList();
                    CarVideos = dt.Tables[2].AsEnumerable().Select(video => new RequestDraftImage
                    {
                        DraftImageID = video.Field<int>("DraftImageID"),
                        ObjectID = video.Field<int?>("ObjectID"),
                        ObjectTypeID = video.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = video.Field<string>("EncryptedName"),
                        OriginalName = video.Field<string>("OriginalName"),
                        ImageURL = video.Field<string>("ImageURL"),
                        IsDeleted = video.Field<bool>("IsDeleted"),
                        IsVideo = video.Field<bool>("IsVideo")

                    }).ToList();
                    carDocuments = dt.Tables[3].AsEnumerable().Select(doc => new RequestDraftImage
                    {
                        DraftImageID = doc.Field<int>("DraftImageID"),
                        ObjectID = doc.Field<int?>("ObjectID"),
                        ObjectTypeID = doc.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = doc.Field<string>("EncryptedName"),
                        OriginalName = doc.Field<string>("OriginalName"),
                        ImageURL = doc.Field<string>("ImageURL"),
                        IsDeleted = doc.Field<bool>("IsDeleted"),
                        IsVideo = doc.Field<bool>("IsVideo")

                    }).ToList();

                    CarVINMilage = dt.Tables[4].AsEnumerable().Select(vinmilage => new RequestDraftImage
                    {
                        DraftImageID = vinmilage.Field<int>("DraftImageID"),
                        ObjectID = vinmilage.Field<int?>("ObjectID"),
                        ObjectTypeID = vinmilage.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = vinmilage.Field<string>("EncryptedName"),
                        OriginalName = vinmilage.Field<string>("OriginalName"),
                        ImageURL = vinmilage.Field<string>("ImageURL"),
                        IsDeleted = vinmilage.Field<bool>("IsDeleted"),
                        IsVideo = vinmilage.Field<bool>("IsVideo")

                    }).ToList();


                }

                Object data = new { draftdata = draftdata, carImages = carImages, CarVideos = CarVideos, carDocuments = carDocuments, CarVINMilage = CarVINMilage};
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region saveDepriciationValue
        public string saveDepriciationValue(int RequestID, double depriciationValue , int? requestedPartID, int? UserID)
             {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@depriciationValue" , Value = depriciationValue},
                        new SqlParameter { ParameterName = "@requestedPartID" , Value = requestedPartID},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDepriciationValue]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Depriciation saved successfully" : DataValidation.dbError;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region updateSurveyorAppointment
        public bool updateSurveyorAppointment(int UserID, DateTime SurveyorAppointmentDate, int accidentID)
        {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@SurveyorAppointmentDate" , Value = SurveyorAppointmentDate},
                        new SqlParameter { ParameterName = "@accidentID" , Value = accidentID}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateSurveyorAppointment]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveClearanceSummaryFreeText
        public bool SaveClearanceSummaryFreeText(int AccidentID, string Text)
        {
            try
            {
                DataSet dt = new DataSet();




                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@AccidentID" , Value = AccidentID},
                        new SqlParameter { ParameterName = "@Text" , Value = Text}
                    };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[SaveClearanceSummaryFreeText]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


    }
}

