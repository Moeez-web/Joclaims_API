using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using MODEL.Models.Report.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace BAL.Manager
{
    public class CommonManager : ICommonManager
    {
        #region SearchPart
        public List<AutomotivePart> SearchPart(string PartName, int? DamagePointID,int? CountryID)
        {

            DataSet dt = new DataSet();
            try
            {
                var PartsData = new List<AutomotivePart>();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@PartName" , Value = PartName},
                       new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePointID},
                       new SqlParameter { ParameterName = "@CountryID" , Value = CountryID},
                };
                using (dt = ADOManager.Instance.DataSet("[SearchPart]", CommandType.StoredProcedure, sParameter))
                {
                    PartsData = dt.Tables[0].AsEnumerable().Select(part => new AutomotivePart
                    {
                        AutomotivePartID = part.Field<int>("AutomotivePartID"),
                        //  Name1 = part.Field<string>("Name1"),
                        PartName = part.Field<string>("PartName"),
                        DamagePointID = part.Field<string>("DamagePointID"),
                        ItemNumber = part.Field<string>("ItemNumber"),
                        FinalCode = part.Field<string>("FinalCode")

                    }).ToList();


                    return PartsData;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SearchName
        public Object SearchName(string PartName, int DamagePointID, int? CountryID)
        {

            DataSet dt = new DataSet();
            try
            {
                var Name1 = new List<AutomotivePart>();
                var AutomotivePart = new List<AutomotivePart>();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@PartName" , Value = PartName},
                        new SqlParameter {ParameterName="@DamagePointID",Value= DamagePointID},
                        new SqlParameter {ParameterName="@CountryID",Value= CountryID},
                };
                using (dt = ADOManager.Instance.DataSet("[SearchName1]", CommandType.StoredProcedure, sParameter))
                {
                    Name1 = dt.Tables[0].AsEnumerable().Select(part => new AutomotivePart
                    {

                        Name1 = part.Field<string>("Name1"),


                    }).ToList();

                    AutomotivePart = dt.Tables[1].AsEnumerable().Select(ap => new AutomotivePart
                    {
                        PartName = ap.Field<string>("PartName"),
                        Name1 = ap.Field<string>("Name1"),
                        ItemNumber = ap.Field<string>("ItemNumber"),
                        OptionName = ap.Field<string>("OptionName"),
                        FinalCode = ap.Field<string>("FinalCode"),
                        OptionNumber = ap.Field<string>("OptionNumber"),
                        StatusID = ap.Field<short?>("StatusID"),
                        DamagePointID = ap.Field<string>("DamagePointID"),
                        Name2 = ap.Field<string>("Name2"),
                        Name3 = ap.Field<string>("Name3"),
                        Name4 = ap.Field<string>("Name4"),
                        Name5 = ap.Field<string>("Name5"),
                        Name6 = ap.Field<string>("Name6"),
                        Name7 = ap.Field<string>("Name7"),
                        Name8 = ap.Field<string>("Name8"),
                        DamageName = ap.Field<string>("DamageName")

                    }).ToList();

                    Object data = new { Name1 = Name1, AutomotivePart = AutomotivePart };
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetCommonMeta
        public CommonMeta GetCommonMeta(int? userID, int? RoleID)
        {
            DataSet dt = new DataSet();
            try
            {
                var commonMeta = new CommonMeta();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = userID},
                        new SqlParameter { ParameterName = "@RoleID" , Value = RoleID},
                };

                using (dt = ADOManager.Instance.DataSet("[getCommonMeta]", CommandType.StoredProcedure, sParameter))
                {


                    //commonMeta.Notifications = dt.Tables[1].AsEnumerable().Select(n => new Notification
                    //{
                    //    NotificationID = n.Field<int>("NotificationID"),
                    //    RecipientID = n.Field<int?>("RecipientID"),
                    //    TextData = n.Field<string>("TextData"),
                    //    IsSent = n.Field<bool?>("IsSent"),
                    //    CreatedOn = n.Field<DateTime>("CreatedOn"),
                    //    CreatedBy = n.Field<int>("CreatedBy"),
                    //    RedirectURL = n.Field<string>("RedirectURL"),
                    //    Icon = n.Field<string>("Icon"),
                    //    IsRead = n.Field<bool?>("IsRead"),
                    //    CreatedSince = n.Field<string>("CreatedSince"),
                    //    CreatedSinceArabic = n.Field<string>("CreatedSinceArabic"),
                    //    RoleID = n.Field<byte?>("RoleID"),
                    //    NotificationTypeId = n.Field<short?>("NotificationTypeID"),
                    //    RequestID = n.Field<int?>("RequestID"),
                    //    QuotationID = n.Field<int?>("QuotationID"),
                    //    DemandID = n.Field<int?>("DemandID"),
                    //    MobileScreenName = n.Field<string>("MobileScreenName"),
                    //    MobileNotificationToken = n.Field<string>("MobileNotificationToken"),
                    //    SupplierID = n.Field<int?>("SupplierID"),
                    //    TextArabicData = n.Field<string>("TextArabicData"),
                    //}).ToList();

                    commonMeta.ObjectTypes = dt.Tables[0].AsEnumerable().Select(ot => new ObjectType
                    {
                        ObjectTypeID = ot.Field<short>("ObjectTypeID"),
                        ObjectName = ot.Field<string>("ObjectName"),
                        TypeName = ot.Field<string>("TypeName"),
                        Icon = ot.Field<string>("Icon"),
                        ArabicTypeName = ot.Field<string>("ArabicTypeName"),
                        IsEditAllowed = ot.Field<bool?>("IsEditAllowed"),

                    }).ToList();



                    commonMeta.Makes = dt.Tables[1].AsEnumerable().Select(make => new Make
                    {
                        MakeCode = make.Field<string>("MakeCode"),
                        ImgURL = make.Field<string>("ImgURL"),
                        MakeID = make.Field<int>("MakeID"),
                        ModifiedBy = make.Field<int?>("ModifiedBy"),
                        CreatedOn = make.Field<DateTime?>("CreatedOn"),
                        CreatedBy = make.Field<int?>("CreatedBy"),
                        ModifiedOn = make.Field<DateTime?>("ModifiedOn"),
                        MakeName = make.Field<string>("EnglishMakeName"),
                        ArabicMakeName = make.Field<string>("ArabicMakeName"),

                    }).ToList();

                    commonMeta.Models = dt.Tables[2].AsEnumerable().Select(model => new Model
                    {
                        MakeID = model.Field<int>("MakeID"),
                        ModelID = model.Field<int>("ModelID"),
                        ModelCode = model.Field<string>("EnglishModelName"),
                        ArabicModelName = model.Field<string>("ArabicModelName"),
                        GroupName = model.Field<string>("GroupName"),
                        MakeName = model.Field<string>("EnglishMakeName"),
                        ArabicMakeName = model.Field<string>("ArabicMakeName"),
                        ImgURL = model.Field<string>("ImgURL"),


                    }).ToList();

                    commonMeta.Years = dt.Tables[3].AsEnumerable().Select(year => new Year
                    {
                        LanguageYearID = year.Field<Int16>("LanguageYearID"),
                        YearCode = year.Field<int>("YearCode"),
                        LanguageID = year.Field<byte>("LanguageID"),
                        YearID = year.Field<Int16>("YearID"),

                    }).ToList();




                    commonMeta.UnReadNotification = Convert.ToInt32(dt.Tables[4].Rows[0]["UnReadNotification"]);
                    RoleID = Convert.ToInt32(dt.Tables[4].Rows[0]["RoleID"]);
                    commonMeta.TrNotificationCount = Convert.ToInt32(dt.Tables[4].Rows[0]["TrNotificationCount"]);


                    commonMeta.GroupName = dt.Tables[5].AsEnumerable().Select(mk => new VehicleGroups
                    {
                        MakeID = mk.Field<int>("MakeID"),
                        GroupName = mk.Field<string>("GroupName")

                    }).ToList();
                    //if (RoleID == 1 || RoleID == 2 || RoleID == 3 || RoleID == 4 || RoleID == 8 || RoleID == 9 || RoleID == 10 || RoleID == 12 || RoleID == 13)
                    //{

                    //}
                    //else if (RoleID == 4 || RoleID == 8 || RoleID == 9 || RoleID == 10 || RoleID == 12 || RoleID == 13 )
                    //{ 
                    //    commonMeta.UserPermissions = dt.Tables[6].AsEnumerable().Select(pm => new Permission
                    //    {
                    //        PermissionID = pm.Field<Int16>("PermissionID"),
                    //        RoleID = pm.Field<int?>("RoleID"),
                    //        PermissionName = pm.Field<string>("PermissionName"),
                    //        PermissionArabicName = pm.Field<string>("PermissionArabicName"),
                    //        UserPermissionID = pm.Field<int?>("UserPermissionID"),
                    //        UserID = pm.Field<int?>("UserID"),
                    //        IsChecked = pm.Field<bool?>("IsChecked"),
                    //        MinPrice = pm.Field<int?>("MinPrice"),
                    //        MaxPrice = pm.Field<int?>("MaxPrice"),
                    //    }).ToList();
                    //}

                  

                    if (RoleID != 6)
                    {
                        commonMeta.UserPermissions = dt.Tables[6].AsEnumerable().Select(pm => new Permission
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

                      

                        commonMeta.Cities = dt.Tables[7].AsEnumerable().Select(cty => new City
                        {
                            CityID = cty.Field<int>("CityID"),
                            CityName = cty.Field<string>("CityName"),
                            CityNameArabic = cty.Field<string>("CityNameArabic"),

                        }).ToList();
                        commonMeta.DamagePoints = dt.Tables[8].AsEnumerable().Select(dPoint => new DamagePoint
                        {
                            DamagePointID = dPoint.Field<int>("DamagePointID"),
                            PointName = dPoint.Field<string>("PointName"),
                            PointNameArabic = dPoint.Field<string>("PointNameArabic")
                        }).ToList();
                        commonMeta.Roles = dt.Tables[9].AsEnumerable().Select(rl => new Roles
                        {
                            Id = rl.Field<byte>("Id"),
                            Icon = rl.Field<string>("Icon"),
                            Name = rl.Field<string>("Name"),
                            ObjectName = rl.Field<string>("ObjectName"),
                            RolesNameArabic = rl.Field<string>("RolesNameArabic")
                        }).ToList();
                        commonMeta.featurePermissions = dt.Tables[10].AsEnumerable().Select(fp => new FeaturePermission
                        {
                            FeaturePermissionsID = fp.Field<int?>("FeaturePermissionsID"),
                            FeatureID = fp.Field<int?>("FeatureID"),
                            IsApproved = fp.Field<bool>("IsApproved"),
                            CompanyID = fp.Field<int>("ObjectID"),
                            IsDeleted = fp.Field<bool?>("IsDeleted"),
                            AICustomerRequestApproval = fp.Field<bool?>("AICustomerRequestApproval")
                        }).ToList();

                    

                        commonMeta.joclaimsSetting = dt.Tables[11].AsEnumerable().Select(js => new JoclaimsSetting
                        {

                            JoclaimsSettingID = js.Field<int>("JoclaimsSettingID"),
                            ServiceID = js.Field<int>("ServiceID"),
                            ServiceName = js.Field<string>("ServiceName"),
                            SettingTypeID = js.Field<int>("SettingTypeID"),
                            SettingTypeName = js.Field<string>("SettingTypeName")

                        }).ToList();

                        commonMeta.Countries = dt.Tables[12].AsEnumerable().Select(co => new Country
                        {

                            CountryID = co.Field<Int16>("CountryID"),
                            CountryName = co.Field<string>("CountryName"),
                            CountryNameArabic = co.Field<string>("CountryNameArabic")

                        }).ToList();

                        if (RoleID == 4)
                        {
                            commonMeta.IndividualReturns = dt.Tables[13].AsEnumerable().Select(mk => new IndividualReturn
                            {
                                IndividualReturnID = mk.Field<int>("IndividualReturnID"),
                                IndividualReturnText = mk.Field<string>("IndividualReturnText"),
                                IndividualReturnEnglishText = mk.Field<string>("IndividualReturnEnglishText")

                            }).ToList();
                        }






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

        // place it in CommonManager
        public List<City> GetAllCities(int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var cityData = new List<City>();
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                };


                using (dt = ADOManager.Instance.DataSet("[getCity]", CommandType.StoredProcedure, sParameter))
                {
                    cityData = dt.Tables[0].AsEnumerable().Select(cty => new City
                    {
                        CityID = cty.Field<int>("CityID"),
                        CityName = cty.Field<string>("CityName"),
                        CityNameArabic = cty.Field<string>("CityNameArabic"),
                        CountryID = cty.Field<Int16>("CountryID")

                    }).ToList();


                    return cityData;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #region UpdatePartInfo
        public Object UpdatePartInfo(UpdateAutomotivePart automotivePart)
        {
            DataSet dt = new DataSet();
            List<Request> request = new List<Request>();

            try
            {
                List<AutomotivePart> OldAutomotivePart = new List<AutomotivePart>();
                OldAutomotivePart.Add(automotivePart.oldAutomotivePart);
                List<AutomotivePart> MergeAutomotivePart = new List<AutomotivePart>();
                MergeAutomotivePart.Add(automotivePart.mergeAutomotivePart);

                var XMLOldAutomotivePart = OldAutomotivePart.ToXML("ArrayOfOldAutomotivePart");
                var XMLMergeAutomotivePart = automotivePart.mergeAutomotivePart != null ? MergeAutomotivePart.ToXML("ArrayOfMergeAutomotivePart") : null;
                var XMLReplaceAndSplit = automotivePart.ReplaceWithPart != null ? automotivePart.ReplaceWithPart.ToXML("ArrayOfReplacewithPart") : null;

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLOldAutomotivePart" , Value =  XMLOldAutomotivePart},
                        new SqlParameter { ParameterName = "@XMLMergeAutomotivePart" , Value = XMLMergeAutomotivePart },
                        new SqlParameter { ParameterName = "@XMLReplaceAndSplit" , Value = XMLReplaceAndSplit},
                        new SqlParameter { ParameterName = "@UserID" , Value =automotivePart.UserID},
                        new SqlParameter { ParameterName = "@StatusID" , Value =automotivePart.StatusID},

                };
                using (dt = ADOManager.Instance.DataSet("[updatePartInfo]", CommandType.StoredProcedure, sParameter))
                {
                    if (dt.Tables.Count > 0)
                    {
                        request = dt.Tables[0].AsEnumerable().Select(req => new Request
                        {
                            RequestID = req.Field<int>("RequestID"),
                            RequestNumber = req.Field<int>("RequestNumber"),
                            AccidentNo = req.Field<string>("AccidentNo"),
                            CompanyName = req.Field<string>("Name"),
                            CreatedByName = req.Field<string>("CreatedByName"),
                            IsRequestWorkshopIC = req.Field<bool>("IsRequestWorkshopIC"),
                            CreatedOn = req.Field<DateTime>("CreatedOn")

                        }).ToList();
                    }
                }
                Object data = new { requests = request, status = request.Count > 0 ? 0 : 1 };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region OnApproveTechnicalReport
        public string OnApproveTechnicalReport(string AccidentNo, int ModifiedBy, int ObjectTypeID, int TRApprovalID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = AccidentNo},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@ObjectTypeID" , Value = ObjectTypeID},
                        new SqlParameter { ParameterName = "@TRApprovalID" , Value = TRApprovalID}
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

        #region checkVINHistory
        public RequestLog checkVINHistory(string VIN)
        {
            DataSet dt = new DataSet();
            try
            {

                var requestLog = new RequestLog();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@VIN" , Value = VIN}
                    };

                using (dt = ADOManager.Instance.DataSet("[getVINHistory]", CommandType.StoredProcedure, sParameter))
                {
                    requestLog.Vehicle = dt.Tables[0].AsEnumerable().Select(req => new Vehicle
                    {
                        VIN = req.Field<string>("VIN"),
                        PlateNo = req.Field<string>("PlateNo"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        EngineTypeID = req.Field<Int16?>("EngineTypeID"),
                        BodyTypeID = req.Field<Int16?>("BodyTypeID"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        EngineTypeArabicName = req.Field<string>("EngineTypeArabicName"),
                        EngineTypeName = req.Field<string>("EngineTypeName"),
                        BodyTypeArabicName = req.Field<string>("BodyTypeArabicName"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        BodyTypeIcon = req.Field<string>("BodyTypeIcon")
                    }).FirstOrDefault();
                    requestLog.Accidents = dt.Tables[1].AsEnumerable().Select(acd => new Accident
                    {
                        AccidentNo = acd.Field<string>("AccidentNo"),
                        VIN = acd.Field<string>("VIN"),
                        PlateNo = acd.Field<string>("PlateNo"),
                        MakeID = acd.Field<int>("MakeID"),
                        ModelID = acd.Field<int>("ModelID"),
                        ProductionYear = acd.Field<Int16>("ProductionYear"),
                        CompanyName = acd.Field<string>("CompanyName"),
                        WorkshopName = acd.Field<string>("WorkshopName"),
                        CarLicenseFront = acd.Field<string>("CarLicenseFront"),
                        CarLicenseBack = acd.Field<string>("CarLicenseBack"),
                        OwnerIDFront = acd.Field<string>("OwnerIDFront"),
                        OwnerIDBack = acd.Field<string>("OwnerIDBack"),
                        PoliceReport = acd.Field<string>("PoliceReport"),
                        PDFReport = acd.Field<string>("PDFReport"),
                        CarLicenseFrontDescription = acd.Field<string>("CarLicenseFrontDescription"),
                        CarLicenseBackDescription = acd.Field<string>("CarLicenseBackDescription"),
                        OwnerIDFrontDescription = acd.Field<string>("OwnerIDFrontDescription"),
                        OwnerIDBackDescription = acd.Field<string>("OwnerIDBackDescription"),
                        PoliceReportDescription = acd.Field<string>("PoliceReportDescription"),
                        PDFReportDescription = acd.Field<string>("PDFReportDescription"),
                        AccidentHappendOn = acd.Field<DateTime?>("AccidentHappendOn"),
                        EngineTypeID = acd.Field<Int16?>("EngineTypeID"),
                        BodyTypeID = acd.Field<Int16?>("BodyTypeID"),
                        ImportantNote = acd.Field<string>("ImportantNote"),
                        //FinalLabourCost = acd.Field<double?>("FinalLabourCost"),
                        ArabicMakeName = acd.Field<string>("ArabicMakeName"),
                        MakeName = acd.Field<string>("EnglishMakeName"),
                        ArabicModelName = acd.Field<string>("ArabicModelName"),
                        ModelCode = acd.Field<string>("EnglishModelName"),
                        YearCode = acd.Field<int>("YearCode"),
                        EngineTypeArabicName = acd.Field<string>("EngineTypeArabicName"),
                        EngineTypeName = acd.Field<string>("EngineTypeName"),
                        BodyTypeArabicName = acd.Field<string>("BodyTypeArabicName"),
                        BodyTypeName = acd.Field<string>("BodyTypeName"),
                        BodyTypeIcon = acd.Field<string>("BodyTypeIcon"),
                        AccidentTypeName = acd.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = acd.Field<string>("ArabicAccidentTypeName")
                    }).ToList();
                    requestLog.RequestedParts = dt.Tables[2].AsEnumerable().Select(rp => new RequestedPart
                    {
                        AccidentNo = rp.Field<string>("AccidentNo"),
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        ConditionTypeNameArabic = rp.Field<string>("ConditionTypeNameArabic"),
                        TypeName = rp.Field<string>("TypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        PartName = rp.Field<string>("PartName"),
                        PartNameEnglish = rp.Field<string>("PartNameEnglish")
                    }).ToList();

                    requestLog.RequestedPartsImages = dt.Tables[3].AsEnumerable().Select(img => new Image
                    {
                        AccidentNo = img.Field<string>("AccidentNo"),
                        ImageID = img.Field<int?>("ImageID"),
                        ImageURL = img.Field<string>("ImageURL"),
                        OriginalName = img.Field<string>("OriginalName"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        ObjectID = img.Field<int?>("ObjectID"),
                        ObjectTypeID = img.Field<Int16?>("ObjectTypeID"),
                        IsDocument = img.Field<bool?>("IsDocument"),
                        Description = img.Field<string>("Description"),
                        PartNameEnglish = img.Field<string>("PartNameEnglish"),
                        PartName = img.Field<string>("PartName"),

                    }).ToList();

                    requestLog.RequestTasks = dt.Tables[4].AsEnumerable().Select(rt => new RequestTask
                    {
                        AccidentNo = rt.Field<string>("AccidentNo"),
                        RequestID = rt.Field<int?>("RequestID"),
                        RequestTaskID = rt.Field<int>("RequestTaskID"),
                        TaskName = rt.Field<string>("TaskName"),
                        TaskDescription = rt.Field<string>("TaskDescription"),
                        LabourTime = rt.Field<int?>("LabourTime"),
                        LabourPrice = rt.Field<double?>("LabourPrice"),
                        TaskArabicTypeName = rt.Field<string>("TaskArabicTypeName"),
                        TaskTypeName = rt.Field<string>("TaskTypeName"),
                    }).ToList();

                    requestLog.AccidentNotes = dt.Tables[5].AsEnumerable().Select(nt => new Note
                    {
                        AccidentNo = nt.Field<string>("AccidentNo"),
                        NoteID = nt.Field<int>("NoteID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),
                    }).ToList();
                    requestLog.AccidentImages = dt.Tables[6].AsEnumerable().Select(img => new Image
                    {
                        AccidentNo = img.Field<string>("AccidentNo"),
                        ImageID = img.Field<int?>("ImageID"),
                        ImageURL = img.Field<string>("ImageURL"),
                        OriginalName = img.Field<string>("OriginalName"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        ObjectID = img.Field<int?>("ObjectID"),
                        ObjectTypeID = img.Field<Int16?>("ObjectTypeID"),
                        IsDocument = img.Field<bool?>("IsDocument"),
                        Description = img.Field<string>("Description"),
                    }).ToList();

                    requestLog.AccidentMarkers = dt.Tables[7].AsEnumerable().Select(am => new AccidentMarker
                    {
                        AccidentNo = am.Field<string>("AccidentNo"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        DamagePointID = am.Field<int>("DamagePointID"),
                        IsDamage = am.Field<bool>("IsDamage"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),

                    }).ToList();

                }

                return requestLog;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region AccidentCostReport
        public Object GetAccidentCostReport(RequestPartObj RPObj)
        {
            DataSet dt = new DataSet();
            try
            {
                var Accidents = new List<Request>();
                var XMLMakeID = (RPObj.MakeID != null && RPObj.MakeID.Count() > 0) ? RPObj.MakeID.ToXML("ArrayofMake") : null;
                var XMLModelID = (RPObj.ModelID != null && RPObj.ModelID.Count() > 0 ? RPObj.ModelID.ToXML("ArrayofModel") : null);
                int TotalPages;
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = RPObj.CompanyId},
                        new SqlParameter { ParameterName = "@PageNo" , Value = RPObj.PageNo},
                        new SqlParameter { ParameterName = "@XMLMake" , Value = XMLMakeID},
                        new SqlParameter { ParameterName = "@XMLModel" , Value = XMLModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = RPObj.YearID},
                        new SqlParameter { ParameterName = "@IsExcel" , Value = RPObj.IsExcel},
                        new SqlParameter { ParameterName = "@StartDate" , Value = RPObj.StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = RPObj.EndDate},
                        new SqlParameter { ParameterName = "@CountryID" , Value = RPObj.CountryID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getAccidentCostReport]", CommandType.StoredProcedure, sParameter))
                {
                    TotalPages = Convert.ToInt32(dt.Tables[0].Rows[0][0].ToString());
                    Accidents = dt.Tables[1].AsEnumerable().Select(req => new Request
                    {
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        LabourPrice = req.Field<double?>("LabourPrice"),
                        LowestMatchingOfferPrice = req.Field<double?>("LowestMatchingOfferPrice"),
                        AccidentCount = req.Field<int?>("AccidentCount"),
                        CarCost = req.Field<double?>("CarCost"),
                        NumberRequestedPart = req.Field<int>("RequestPartCount"),
                        BrokerName = req.Field<string>("BrokerName"),
                    }).ToList();

                    Object data = new { totalPages = TotalPages, Accidents = Accidents };
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetPartPriceEstimate
        public Object GetPartPriceEstimate(string SeriesID, int RequestID, int UserID, int? DemandID, int? monthfilter)
        {
            DataSet dt = new DataSet();
            try
            {

                List<QuotationPart> EstimatedPrices = new List<QuotationPart>();
                //List<QuotationPart> minPriceParts = new List<QuotationPart>();
                //List<QuotationPart> maxPriceParts = new List<QuotationPart>();
                //List<Supplier> minSupplier = new List<Supplier>();
                List<RequestedPart> RequestedParts = new List<RequestedPart>();
                Request request = new Request();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter {ParameterName="@SeriesID", Value = SeriesID},
                        new SqlParameter {ParameterName="@monthfilter", Value = monthfilter},

                    };
                using (dt = ADOManager.Instance.DataSet("[GetPartsPriceEstimation]", CommandType.StoredProcedure, sParameter))
                {
                    request = dt.Tables[0].AsEnumerable().Select(req => new Request
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
                        //WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        //WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        //BiddingHours = req.Field<double?>("BiddingHours"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        //BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        //IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),

                        UserName = req.Field<string>("UserName"),
                        AccidentTypeID = req.Field<Int16?>("AccidentTypeID"),
                        AccidentTypeName = req.Field<string>("AccidentTypeName"),
                        ArabicAccidentTypeName = req.Field<string>("ArabicAccidentTypeName"),
                        CreatedOnAccident = req.Field<DateTime?>("CreatedOnAccident"),
                        //OwnerIDFront = req.Field<string>("OwnerIDFront"),
                        //OwnerIDBack = req.Field<string>("OwnerIDBack"),
                        //CarLicenseBack = req.Field<string>("CarLicenseBack"),
                        //CarLicenseFront = req.Field<string>("CarLicenseFront"),
                        //PoliceReport = req.Field<string>("PoliceReport"),
                        IsPublished = req.Field<bool?>("IsPublished"),
                        IsReady = req.Field<bool?>("IsReady"),
                        IsDeleted = req.Field<bool>("IsDeleted"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        //PDFReport = req.Field<string>("PDFReport"),
                        //CarLicenseFrontDescription = req.Field<string>("CarLicenseFrontDescription"),
                        //CarLicenseBackDescription = req.Field<string>("CarLicenseBackDescription"),
                        //OwnerIDFrontDescription = req.Field<string>("OwnerIDFrontDescription"),
                        //OwnerIDBackDescription = req.Field<string>("OwnerIDBackDescription"),
                        //PoliceReportDescription = req.Field<string>("PoliceReportDescription"),
                        //PDFReportDescription = req.Field<string>("PDFReportDescription"),
                        //LogoURL = req.Field<string>("LogoURL"),
                        //ESignatureURL = req.Field<string>("ESignatureURL"),
                        ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                        FaultyCompanyName = req.Field<string>("FaultyCompanyName"),
                        AccidentCreatedBy = req.Field<string>("AccidentCreatedBy"),
                        ImportantNote = req.Field<string>("ImportantNote"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        AccidentCreatedOn = req.Field<DateTime?>("AccidentCreatedOn"),
                        ResponsibilityTypeID = req.Field<int>("ResponsibilityTypeID"),
                        IsOldPartsRequired = req.Field<bool?>("IsOldPartsRequired"),
                        //DemandCount = req.Field<int?>("DemandCount"),
                        //QuotationsCount = req.Field<int?>("QuotationsCount"),
                        BodyTypeName = req.Field<string>("BodyTypeName"),
                        CarsInvolved = req.Field<int?>("CarsInvolved"),
                        ArabicBodyTypeName = req.Field<string>("ArabicBodyTypeName"),
                        ResponsibilityTypeName = req.Field<string>("ResponsibilityTypeName"),
                        ArabicResponsibilityTypeName = req.Field<string>("ArabicResponsibilityTypeName"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),

                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        CPName = req.Field<string>("CPName"),
                        //CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        //CountryName = req.Field<string>("CountryName"),
                        //CityName = req.Field<string>("CityName"),
                        //AddressLine1 = req.Field<string>("AddressLine1"),
                        //AddressLine2 = req.Field<string>("AddressLine2"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        //IcCancelOrderNote = req.Field<string>("IcCancelOrderNote"),
                        //JoCancelOrderNote = req.Field<string>("JoCancelOrderNote"),
                        DemandCreatedOn = req.Field<DateTime?>("DemandCreatedOn"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        //IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        JCSeriesCode = req.Field<string>("JCSeriesCode"),
                        EstimatedPrice = req.Field<double?>("EstimatedPrice")
                    }).FirstOrDefault();
                    RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        AutomotivePartID = rp.Field<int?>("AutomotivePartID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        PartName = rp.Field<string>("PartName"),
                        IsCheck = rp.Field<int>("SelectedPartConditionForEstimation"),
                        IsUniversal = rp.Field<int>("IsUniversal"),
                        NewOriginalPriceByUser = rp.Field<double?>("NewOriginalPrice"),
                        NewAfterMarketPriceByUser = rp.Field<double?>("NewAfterMartketPrice"),
                        UsedPriceByUser = rp.Field<double?>("UsedPrice"),
                        UniversalPartPriceByUser = rp.Field<double?>("SelectedPartPriceForEstimation"),

                    }).ToList();
                    EstimatedPrices = dt.Tables[2].AsEnumerable().Select(q => new QuotationPart
                    {
                        AutomotivePartID = q.Field<int>("AutomotivePartID"),
                        //RequestedPartID = q.Field<int>("RequestedPartID"),
                        ConditionTypeID = q.Field<Int16?>("ConditionTypeID"),
                        NewPartConditionTypeID = q.Field<Int16?>("NewPartConditionTypeID"),
                        IsUniversal = q.Field<int>("IsUniversal"),
                        MinimumPrice = q.Field<double?>("AveragePrice"),
                        MaximumPrice = q.Field<double?>("MaximumPrice"),
                        AvgPrice = q.Field<double?>("MinimumPrice")

                    }).ToList();
                    var EstimatedPartsprice = Convert.ToInt64(dt.Tables[3].Rows[0]["EstimatedPartPrice"]);

                    Object data = new { request = request, RequestedParts = RequestedParts, EstimatedPrices = EstimatedPrices, EstimatedPartsprice = EstimatedPartsprice };
                    return data;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdatePartInfo
        public AutomotivePart GetNewAutomotivePartDetail(int DamagePointID, string Name1, int? AutomotivePartID)
        {
            DataSet dt = new DataSet();
            try
            {
                var aPart = new AutomotivePart();
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@DamagePointID" , Value =  DamagePointID},
                        new SqlParameter { ParameterName = "@Name1" , Value = Name1 },
                        new SqlParameter { ParameterName = "@AutomotivePartID" , Value = AutomotivePartID },
                };


                using (dt = ADOManager.Instance.DataSet("[getNewAutomotivePartDetail]", CommandType.StoredProcedure, sParameter))
                {
                    aPart = dt.Tables[0].AsEnumerable().Select(ap => new AutomotivePart
                    {
                        ItemNumber = ap.Field<string>("ItemNumber"),
                        FinalCode = ap.Field<string>("FinalCode"),
                        OptionNumber = ap.Field<string>("OptionNumber"),

                    }).FirstOrDefault();


                    return aPart;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region CreateAutomotivePart
        public string CreateAutomotivePart(AutomotivePart automotivePart)
        {
            DataSet dt = new DataSet();

            try
            {
                List<AutomotivePart> OldAutomotivePart = new List<AutomotivePart>();
                OldAutomotivePart.Add(automotivePart);



                var XMLOldAutomotivePart = OldAutomotivePart.ToXML("ArrayOfAutomotivePart");


                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLAutomotivePart" , Value =  XMLOldAutomotivePart},

                };
                var result = ADOManager.Instance.ExecuteScalar("[CreateAutomotivePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion
        #region RejectAutomotivePart
        public string RejectAutomotivePart(int automotivePartID, int UserID)
        {
            DataSet dt = new DataSet();

            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@AutomotivepartID" , Value =  automotivePartID},
                        new SqlParameter { ParameterName = "@UserID" , Value =  UserID},

                };
                var result = ADOManager.Instance.ExecuteScalar("[RejectAutomotivePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetICStaffKPI
        public Object GetICStaffKPI(int companyID, int UserID, string StartDate, string EndDate, int? ReceptionID, int? LossAdjusterID, int? PublishBy, int? POOrderBy, int? POApproveBy, int? PageNo)
        {
            DataSet dt = new DataSet();
            try
            {
                List<ICStaffKPI> iCStaffKPIs = new List<ICStaffKPI>();
                int PageCount;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value =  companyID},
                        new SqlParameter { ParameterName = "@UserID" , Value =  UserID},
                        new SqlParameter { ParameterName = "@StartDate" , Value =  StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value =  EndDate},
                        new SqlParameter { ParameterName = "@ReceptionID" , Value = ReceptionID },
                        new SqlParameter { ParameterName = "@LossAdjusterID" , Value = LossAdjusterID },
                        new SqlParameter { ParameterName = "@PublishBy" , Value =  PublishBy},
                        new SqlParameter { ParameterName = "@POOrderBy" , Value = POOrderBy },
                        new SqlParameter { ParameterName = "@POApproveBy" , Value =  POApproveBy},
                        new SqlParameter { ParameterName = "@PageNo" , Value =  PageNo}

                };


                using (dt = ADOManager.Instance.DataSet("[getICStaffKPI]", CommandType.StoredProcedure, sParameter))
                {
                    iCStaffKPIs = dt.Tables[0].AsEnumerable().Select(ic => new ICStaffKPI
                    {
                        AccidentNo = ic.Field<string>("AccidentNo"),
                        VIN = ic.Field<string>("VIN"),
                        PlateNo = ic.Field<string>("PlateNo"),
                        AccidentCreatedOn = ic.Field<string>("AccidentCreatedOn"),
                        AccidentCreatedBy = ic.Field<string>("AccidentCreatedBy"),
                        DelayInFirstRequestCreation = ic.Field<string>("DelayInFirstRequestCreation"),
                        RequestNumber = ic.Field<string>("RequestNumber"),
                        RequestCreatedOn = ic.Field<string>("RequestCreatedOn"),
                        RequestCreatedBy = ic.Field<string>("RequestCreatedBy"),
                        PublishedOn = ic.Field<string>("PublishedOn"),
                        PublishedBy = ic.Field<string>("PublishedBy"),
                        DelayInPublishRequest = ic.Field<string>("DelayInPublishRequest"),
                        CurrentStatus = ic.Field<string>("CurrentStatus"),
                        ReferredOn = ic.Field<string>("ReferredOn"),
                        DelayInPOCreation = ic.Field<string>("DelayInPOCreation"),
                        OrderOn = ic.Field<string>("OrderOn"),
                        OrderByName = ic.Field<string>("OrderByName"),
                        DelayInPOApproval = ic.Field<string>("DelayInPOApproval"),
                        Approval = ic.Field<string>("Approval"),
                        DelayInPOApprovalInMin = ic.Field<int?>("DelayInPOApprovalInMin"),
                        DelayInFirstRequestCreationInMin = ic.Field<int?>("DelayInFirstRequestCreationInMin"),
                        DelayInPublishRequestInMin = ic.Field<int?>("DelayInPublishRequestInMin")

                    }).ToList();

                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["PageCount"]);

                    Object data = new { icStaffKPI = iCStaffKPIs, PageCount = PageCount };

                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetMonthlyClaimsReport
        public Object GetMonthlyClaimsReport(int companyID, int UserID, string StartDate, string EndDate, int? PageNo)
        {
            DataSet dt = new DataSet();
            try
            {
                List<MonthlyClaims> monthlyClaims = new List<MonthlyClaims>();
                int PageCount;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value =  companyID},
                        new SqlParameter { ParameterName = "@UserID" , Value =  UserID},
                        new SqlParameter { ParameterName = "@StartDate" , Value =  StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value =  EndDate},
                        new SqlParameter {ParameterName = "@PageNo" , Value =  PageNo}

                };


                using (dt = ADOManager.Instance.DataSet("[getMonthlyClaimsReport]", CommandType.StoredProcedure, sParameter))
                {
                    monthlyClaims = dt.Tables[0].AsEnumerable().Select(ic => new MonthlyClaims
                    {
                        AccidentNo = ic.Field<string>("AccidentNo"),
                        VIN = ic.Field<string>("VIN"),
                        AccidentID = ic.Field<int>("AccidentID"),
                        ArabicMakeName = ic.Field<string>("ArabicMakeName"),
                        AccidentType = ic.Field<string>("AccidentType"),
                        ArabicModelName = ic.Field<string>("ArabicModelName"),
                        RequestCount = ic.Field<int?>("RequestCount"),
                        PublishedRequestCount = ic.Field<int?>("PublishedRequestCount"),
                        UnPublishedRequestCount = ic.Field<int?>("UnPublishedRequestCount"),
                        QuotationCount = ic.Field<int?>("QuotationCount"),
                        AvailableOffers = ic.Field<int?>("AvailableOffers"),
                        NOTAvailableOffers = ic.Field<int?>("NOTAvailableOffers"),
                        CreatedOn = ic.Field<string>("CreatedOn"),
                        MatchingOffers = ic.Field<int?>("MatchingOffers"),
                        NotMatchingOffers = ic.Field<int?>("NotMatchingOffers"),
                        BrokerName = ic.Field<string>("BrokerName"),
                        IsInstantPriceCount = ic.Field<int?>("IsInstantPriceCount")

                    }).ToList();

                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["PageCount"]);

                    Object data = new { monthlyClaims = monthlyClaims, PageCount = PageCount };

                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region getWorkshopCompany
        public SignInResponse GetWorkshopCompany(int UserID, int RoleID)
        {
            DataSet dt = new DataSet();
            SignInResponse signInResponse = new SignInResponse();
            try
            {
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@RoleID" , Value = RoleID},
                };



                using (dt = ADOManager.Instance.DataSet("[getWorkshopCompany]", CommandType.StoredProcedure, sParameter))
                {
                    if (RoleID == 4)
                    {
                        signInResponse.Company = dt.Tables[0].AsEnumerable().Select(cmp => new Company
                        {
                            CompanyID = cmp.Field<int>("CompanyID"),
                            EmployeeID = cmp.Field<int>("EmployeeID"),
                            Name = cmp.Field<string>("Name"),
                            CompanyCode = cmp.Field<string>("CompanyCode"),
                        }).FirstOrDefault();
                    }
                    else if (RoleID == 11)
                    {
                        signInResponse.Workshop = dt.Tables[0].AsEnumerable().Select(sup => new Workshop
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
                            Email = sup.Field<string>("Email")

                        }).FirstOrDefault();

                        signInResponse.ICWorkshops = dt.Tables[1].AsEnumerable().Select(pm => new ICWorkshop
                        {
                            CompanyID = pm.Field<int>("CompanyID"),
                            WorkshopID = pm.Field<int>("WorkshopID"),
                            CompanyName = pm.Field<string>("CompanyName"),
                            ICWorkshopID = pm.Field<int>("ICWorkshopID"),
                            CompanyCode = pm.Field<string>("CompanyCode"),

                        }).ToList();
                    }
                    return signInResponse;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetMobileAPIUrl
        public Object GetMobileAPIUrl(string MobileAppVersion)
        {
            DataSet dt = new DataSet();

            string AppVersion;
            string APIUrl;
            int AIPersonaID;
            int AppID;

            try
            {
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@MobileAppVersion" , Value = MobileAppVersion}
                };



                using (dt = ADOManager.Instance.DataSet("[GetMobileAPIUrl]", CommandType.StoredProcedure, sParameter))
                {
                    AppVersion = Convert.ToString(dt.Tables[0].Rows[0]["AppVersion"]);
                    APIUrl = Convert.ToString(dt.Tables[0].Rows[0]["APIUrl"]);
                    AIPersonaID = Convert.ToInt32(dt.Tables[0].Rows[0]["AIPersonaID"]);
                    AppID = Convert.ToInt32(dt.Tables[0].Rows[0]["AppID"]);
                }
                var data = new { AppVersion = AppVersion, APIUrl = APIUrl, AIPersonaID = AIPersonaID, AIAppID = AppID };
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SearchNameInMultipleDamagePoint
        public Object SearchNameInMultipleDamagePoint(AutomotivePart automotivePart)
        {


            DataSet dt = new DataSet();
            try
            {
                var Name1 = new List<AutomotivePart>();
                var AutomotivePart = new List<AutomotivePart>();
                var XMLDamagePoints = automotivePart.DamagePoint.ToXML("ArrayofDamagePoint");

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@PartName" , Value = automotivePart.PartName},
                        new SqlParameter {ParameterName="@DamagePointID",Value= XMLDamagePoints}
                };
                using (dt = ADOManager.Instance.DataSet("[SearchNameInMultipleDamagePoint]", CommandType.StoredProcedure, sParameter))
                {
                    Name1 = dt.Tables[0].AsEnumerable().Select(part => new AutomotivePart
                    {
                        Name1 = part.Field<string>("Name1"),

                    }).ToList();

                    AutomotivePart = dt.Tables[1].AsEnumerable().Select(ap => new AutomotivePart
                    {
                        PartName = ap.Field<string>("PartName"),
                        Name1 = ap.Field<string>("Name1"),
                        ItemNumber = ap.Field<string>("ItemNumber"),
                        OptionName = ap.Field<string>("OptionName"),
                        FinalCode = ap.Field<string>("FinalCode"),
                        OptionNumber = ap.Field<string>("OptionNumber"),
                        StatusID = ap.Field<short?>("StatusID"),
                        DamagePointID = ap.Field<string>("DamagePointID"),
                        Name2 = ap.Field<string>("Name2"),
                        Name3 = ap.Field<string>("Name3"),
                        Name4 = ap.Field<string>("Name4"),
                        Name5 = ap.Field<string>("Name5"),
                        Name6 = ap.Field<string>("Name6"),
                        Name7 = ap.Field<string>("Name7"),
                        Name8 = ap.Field<string>("Name8"),
                        DamageName = ap.Field<string>("DamageName")

                    }).ToList();

                    Object data = new { Name1 = Name1, AutomotivePart = AutomotivePart };
                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region GenerateMultiAutomotivePartCode

        public Object generateMultiAutoMotivePartCode(AutomotivePart automotivePart)
        {

            DataSet dt = new DataSet();
            try
            {
                Object Response;
                var FinalCode = new AutomotivePart();
                var suggestedFinalCodes = new List<AutomotivePart>();
                var XMLDamagePoints = automotivePart.DamagePoint.ToXML("ArrayofDamagePoint");

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@ItemNumber1" , Value = automotivePart.ItemNumber},
                        new SqlParameter {ParameterName="@PartName",Value= automotivePart.Name1},
                        new SqlParameter {ParameterName="@XMLDamagePoint",Value= XMLDamagePoints}
                };
                using (dt = ADOManager.Instance.DataSet("[GenerateMultiAutomotivePartCode]", CommandType.StoredProcedure, sParameter))
                {
                    if (dt.Tables[0].Columns.Contains("Error"))
                    {
                        suggestedFinalCodes = dt.Tables[0].AsEnumerable().Select(part => new AutomotivePart
                        {
                            PartName = part.Field<string>("PartName"),
                            Name1 = part.Field<string>("Name1"),
                            ItemNumber = part.Field<string>("ItemNumber"),
                            FinalCode = part.Field<string>("FinalCode"),
                            StatusID = part.Field<Int16?>("Error")

                        }).ToList();
                        Response = new { suggestedFinalCodes = suggestedFinalCodes, Status = false };

                    }
                    else
                    {
                        FinalCode = dt.Tables[0].AsEnumerable().Select(ap => new AutomotivePart
                        {
                            PartName = ap.Field<string>("PartName"),
                            ItemNumber = ap.Field<string>("ItemNumber"),
                            OptionNumber = ap.Field<string>("OptionNumber"),
                            FinalCode = ap.Field<string>("FinalCode"),
                            StatusID = ap.Field<Int16?>("Status")
                        }).FirstOrDefault();

                        Response = new { FinalCode = FinalCode, Status = true };

                    }





                    return Response;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region PoRoDetailReport
        public Object PoRoDetailReport(int CompanyID, string StartDate, string EndDate, bool IsExcel, int PageNo)
        {
            DataSet dt = new DataSet();
            try
            {
                List<PoRoDetailsReport> Po_Ro_Detail = new List<PoRoDetailsReport>();
                int PageCount;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value =  CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value =  StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value =  EndDate},
                        new SqlParameter {ParameterName = "@PageNo" , Value =  PageNo},
                        new SqlParameter {ParameterName = "@IsExcel" , Value =  IsExcel}

                };


                using (dt = ADOManager.Instance.DataSet("[PoRoDetailReport]", CommandType.StoredProcedure, sParameter))
                {
                    Po_Ro_Detail = dt.Tables[0].AsEnumerable().Select(ic => new PoRoDetailsReport
                    {
                        AccidentNo = ic.Field<string>("AccidentNo"),
                        PlateNo = ic.Field<string>("PlateNo"),
                        WorkshopName = ic.Field<string>("WorkshopName"),
                        labour = ic.Field<double?>("labour"),
                        SupplierName = ic.Field<string>("SupplierName"),
                        PartsCost = ic.Field<double?>("PartsCost"),
                        RepairOrderDate = ic.Field<DateTime?>("RepairOrderDate"),

                    }).ToList();
                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);

                    Object data = new { Po_Ro_Report = Po_Ro_Detail, PageCount = PageCount };

                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion


        #region GetlatestQuotationSuppliers
        public Object GetlatestQuotationSuppliers(string SeriesID, int AutomotivePartID, int CoditionTypeID, int NewPartConditionTypeID, int monthfilter, int userRoleID, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<QuotationPart> SupplierwithMinVal = new List<QuotationPart>();
                List<QuotationPart> SupplierwithMaxVal = new List<QuotationPart>();
                int PageCount;
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@SeriesID" , Value =  SeriesID},
                        new SqlParameter { ParameterName = "@AutoMotivePartID" , Value =  AutomotivePartID},
                        new SqlParameter { ParameterName = "@ConditionTypeID" , Value =  CoditionTypeID},
                        new SqlParameter {ParameterName = "@NewConditionTypeID" , Value =  NewPartConditionTypeID},
                        new SqlParameter {ParameterName = "@monthfilter" , Value =  monthfilter},
                        new SqlParameter {ParameterName = "@userRoleID" , Value =  userRoleID},
                        new SqlParameter {ParameterName = "@CountryID" , Value =  CountryID},

                };


                using (dt = ADOManager.Instance.DataSet("[getLatestSuppliersQuoatation]", CommandType.StoredProcedure, sParameter))
                {
                    SupplierwithMinVal = dt.Tables[0].AsEnumerable().Select(qt => new QuotationPart
                    {
                        SupplierID = qt.Field<int>("SupplierID"),
                        QuotationID = qt.Field<int>("QuotationID"),
                        Price = qt.Field<double?>("Price"),
                        RequestID = qt.Field<int?>("RequestID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        SupplierPhoneNumber = qt.Field<string>("CPPhone"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),

                    }).ToList();
                    SupplierwithMaxVal = dt.Tables[1].AsEnumerable().Select(qt => new QuotationPart
                    {
                        SupplierID = qt.Field<int>("SupplierID"),
                        QuotationID = qt.Field<int>("QuotationID"),
                        Price = qt.Field<double?>("Price"),
                        RequestID = qt.Field<int?>("RequestID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        SupplierPhoneNumber = qt.Field<string>("CPPhone"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn")

                    }).ToList();
                    var MinAveragePrice = Convert.ToDouble(dt.Tables[2].Rows[0]["MinAveragePrice"]);
                    var MaxAveragePrice = Convert.ToDouble(dt.Tables[2].Rows[0]["MaxAveragePrice"]);

                    Object data = new { SupplierwithMinVal = SupplierwithMinVal, SupplierwithMaxVal = SupplierwithMaxVal, MinAveragePrice = MinAveragePrice, MaxAveragePrice = MaxAveragePrice };

                    return data;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region saveandprintRequestPdf
        public bool printrequest(int requestID, string filename)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@requestID" , Value =  requestID},
                        new SqlParameter { ParameterName = "@filename" , Value =  filename}

                };
                var result = Convert.ToInt16(ADOManager.Instance.ExecuteScalar("[saveRequestPdf]", CommandType.StoredProcedure, sParameter));

                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        #endregion


        #region saveEstimationPriceAndCondition

        public bool saveEstimationPriceAndCondition(EstimationPrice estimationPrice)
        {

            DataSet dt = new DataSet();
            try
            {
                var XMLRequestedPart = estimationPrice.RequestedPart.ToXML("ArrayofRequestedPart");

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@RequestID" , Value = estimationPrice.RequestID},
                        new SqlParameter {ParameterName="@XMLRequestedPart",Value= XMLRequestedPart},
                        new SqlParameter {ParameterName="@TotalEsitimatedPartsPrice",Value= Math.Round( estimationPrice.TotalEsitimatedPartsPrice,2)}
                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveEstimationPriceAndCondition]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion


        #region updateTokenAndCaseID

        public bool updateTokenAndCaseID(string CaseId, string Token, int draftID, int UserID)
        {

            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CaseId" , Value = CaseId},
                        new SqlParameter {ParameterName="@Token",Value= Token},
                        new SqlParameter {ParameterName="@draftID",Value= draftID},
                        new SqlParameter {ParameterName="@UserID",Value= UserID},
                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateTokenAndCaseID]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region getDraftRequestreport
        public Object getDraftRequestreport(int? WorkShopID, int? PageNo, string StartDate, string EndDate,int? CompanyID, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var ReportData = new List<DraftRequestReport>();
                int TotalPages;
                if (CompanyID == 0) 
                {
                    CompanyID = null; // For Admin CompanyID is 0 
                }
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkShopID" , Value = WorkShopID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getDraftRequestReport]", CommandType.StoredProcedure, sParameter))
                {

                    ReportData = dt.Tables[0].AsEnumerable().Select(req => new DraftRequestReport
                    {
                        AccidentNo = req.Field<string>("AccidentNo"),
                        DraftRequestPartCount = req.Field<double?>("DraftRequestPartCount"),
                        DrafTotalLabour = req.Field<double?>("DrafTotalLabour"),
                        RequestPartcount = req.Field<double?>("RequestPartcount"),
                        RequestLabor = req.Field<double?>("RequestLabor"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        createdOn = req.Field<DateTime?>("createdOn"),
                        LabourDescription = req.Field<string>("LabourDescription")

                    }).ToList();
                    TotalPages = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);
                    Object data = new { TotalPages = TotalPages, ReportData = ReportData };
                    return data;


                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        


    }
    }
