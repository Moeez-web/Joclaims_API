using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.InspectAI;
using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Tchek;
using MODEL.Tchek;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace BAL.Manager
{

    public class AIManager : IAIManager
    {

        #region  AI
        public string AI(InspektObj inspektObj)
        {
            string serializedData = JsonConvert.SerializeObject(inspektObj);

            try
            {
                //var XMLAdditionalFeature = inspektObj.AdditionalFeature.ToXML("ArrayOfAdditionalFeature");
                var XMLDamagedPart = inspektObj.DamagedPart.ToXML("ArrayOfDamagedPart");
                var XMLRelevantImage = inspektObj.RelevantImage.ToXML("ArrayOfRelevantImage");
                var sParameter = new List<SqlParameter>
                    {
                        //new SqlParameter { ParameterName = "@XMLAdditionalFeature" , Value = XMLAdditionalFeature},
                        //new SqlParameter { ParameterName = "@confidenceScore" , Value = inspektObj.BoundingBoxInformation.confidenceScore},
                        //new SqlParameter { ParameterName = "@damageSeverityScore" , Value = inspektObj.BoundingBoxInformation.damageSeverityScore},
                        //new SqlParameter { ParameterName = "@damageType" , Value = inspektObj.BoundingBoxInformation.damageType},
                        //new SqlParameter { ParameterName = "@partComponent" , Value = inspektObj.BoundingBoxInformation.partComponent},
                        //new SqlParameter { ParameterName = "@RelevantImageID" , Value = inspektObj.BoundingBoxInformation.RelevantImageID},
                        //new SqlParameter { ParameterName = "@height" , Value = inspektObj.BoundingBoxInformation.coordinates.height},
                        //new SqlParameter { ParameterName = "@width" , Value = inspektObj.BoundingBoxInformation.coordinates.width},
                        //new SqlParameter { ParameterName = "@x" , Value = inspektObj.BoundingBoxInformation.coordinates.x},
                        //new SqlParameter { ParameterName = "@y" , Value = inspektObj.BoundingBoxInformation.coordinates.y},
                        new SqlParameter { ParameterName = "@caseId" , Value = inspektObj.CaseDamageReport.caseId},
                        new SqlParameter { ParameterName = "@inspectionId" , Value = inspektObj.CaseDamageReport.inspectionId},
                        new SqlParameter { ParameterName = "@vendor" , Value = inspektObj.CaseDamageReport.vendor},
                        new SqlParameter { ParameterName = "@version" , Value = inspektObj.CaseDamageReport.version},
                        new SqlParameter { ParameterName = "@vinReading" , Value = inspektObj.CaseDamageReport.vehicleReadings != null ?  inspektObj.CaseDamageReport.vehicleReadings.vinReading: null},
                        new SqlParameter { ParameterName = "@fuelMeterReading" , Value =
                            inspektObj.CaseDamageReport.vehicleReadings != null ?
                            inspektObj.CaseDamageReport.vehicleReadings.fuelMeterReading : null},

                        new SqlParameter { ParameterName = "@licensePlateReading" , Value =
                        inspektObj.CaseDamageReport.vehicleReadings != null ?
                        inspektObj.CaseDamageReport.vehicleReadings.licensePlateReading: null},
                        new SqlParameter { ParameterName = "@observations" , Value =
                        inspektObj.CaseDamageReport.vehicleReadings != null ?
                        inspektObj.CaseDamageReport.vehicleReadings.observations: null},
                        new SqlParameter { ParameterName = "@odometerReading" , Value =
                        inspektObj.CaseDamageReport.vehicleReadings != null ?
                        inspektObj.CaseDamageReport.vehicleReadings.odometerReading : null },
                        new SqlParameter { ParameterName = "@laborHoursEstimate" , Value =
                        inspektObj.CaseDamageReport.vehicleReadings != null ?
                        inspektObj.CaseDamageReport.vehicleReadings.laborHoursEstimate: null},
                        new SqlParameter { ParameterName = "@message" , Value = inspektObj.PreInspection.message},
                        new SqlParameter { ParameterName = "@recommendationStatus" , Value = inspektObj.PreInspection.recommendationStatus},
                        new SqlParameter { ParameterName = "@XMLDamagedPart" , Value = XMLDamagedPart},
                        new SqlParameter { ParameterName = "@XMLRelevantImage" , Value = XMLRelevantImage},
                        new SqlParameter { ParameterName = "@InspectLabmessage" , Value = inspektObj.inpektlabResponse.message},
                        new SqlParameter { ParameterName = "@status" , Value = inspektObj.inpektlabResponse.status},
                        new SqlParameter { ParameterName = "@CompleteReponse" , Value = serializedData },
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[AI]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        #region saveCustomerData
        public bool saveCustomerData(AIInspectionRequest customerData)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@AICaseID" , Value = customerData.AICaseID},
                        new SqlParameter { ParameterName = "@AIToken" , Value = customerData.AIToken},
                        new SqlParameter { ParameterName = "@VIN" , Value = customerData.VIN},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = customerData.AccidentNo},
                        new SqlParameter { ParameterName = "@VehicleOwnerName" , Value = customerData.VehicleOwnerName},
                        new SqlParameter { ParameterName = "@OwnerPhoneNo" , Value = customerData.OwnerPhoneNo},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value = customerData.UserID},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = customerData.PlateNo},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = customerData.CompanyID},
                        new SqlParameter { ParameterName = "@ServiceID" , Value = customerData.ServiceID}

                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveCustomerData]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region getAICustomerRequest
        public Object getAICustomerRequest(int CompanyID, int? PageNo)
        {
            DataSet dt = new DataSet();
            int PageCount;
            try
            {
                var customerRequests = new List<AIInspectionRequest>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},

                    };

                using (dt = ADOManager.Instance.DataSet("[getAICustomerRequest]", CommandType.StoredProcedure, sParameter))
                {
                    customerRequests = dt.Tables[0].AsEnumerable().Select(customerRequest => new AIInspectionRequest
                    {
                        CustomerRequestID = customerRequest.Field<int?>("CustomerRequestID"),
                        VehicleOwnerName = customerRequest.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = customerRequest.Field<string>("OwnerPhoneNo"),
                        VIN = customerRequest.Field<string>("VIN"),
                        AccidentNo = customerRequest.Field<string>("AccidentNo"),
                        PlateNo = customerRequest.Field<string>("PlateNo"),
                        AIResponseStatus = customerRequest.Field<string>("AIResponseStatus"),
                        AIResponseMessage = customerRequest.Field<string>("AIResponseMessage"),
                        CreatedOn = customerRequest.Field<DateTime?>("CreatedOn"),
                        AICaseID = customerRequest.Field<string>("AICaseID"),
                        AIToken = customerRequest.Field<string>("AIToken"),
                        ServiceID = customerRequest.Field<int?>("ServiceID")


                    }).ToList();
                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["PageCount"]);

                }
                Object data = new { customerRequests = customerRequests, PageCount = PageCount };
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region getSingleCustomerRequest
        public AIInspectionRequest getSingleCustomerRequest(int CustomerRequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                var customerRequests = new AIInspectionRequest();
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CustomerRequestID" , Value = CustomerRequestID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getSingleCustomerRequest]", CommandType.StoredProcedure, sParameter))
                {
                    customerRequests = dt.Tables[0].AsEnumerable().Select(customerRequest => new AIInspectionRequest
                    {
                        CustomerRequestID = customerRequest.Field<int?>("CustomerRequestID"),
                        VehicleOwnerName = customerRequest.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = customerRequest.Field<string>("OwnerPhoneNo"),
                        VIN = customerRequest.Field<string>("VIN"),
                        AccidentNo = customerRequest.Field<string>("AccidentNo"),
                        PlateNo = customerRequest.Field<string>("PlateNo")


                    }).FirstOrDefault();

                }

                return customerRequests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion


        #region saveCustomerData
        public bool updateCustomerAIRequest(AIInspectionRequest customerData)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {


                        new SqlParameter { ParameterName = "@CustomerRequestID" , Value = customerData.CustomerRequestID},
                        new SqlParameter { ParameterName = "@VIN" , Value = customerData.VIN},
                        new SqlParameter { ParameterName = "@VehicleOwnerName" , Value = customerData.VehicleOwnerName},
                        new SqlParameter { ParameterName = "@OwnerPhoneNo" , Value = customerData.OwnerPhoneNo},
                        new SqlParameter { ParameterName = "@PlateNo" , Value = customerData.PlateNo},
                        new SqlParameter { ParameterName = "@UserID" , Value = customerData.UserID},
                        new SqlParameter { ParameterName = "@ServiceID" , Value = customerData.ServiceID},
                        new SqlParameter { ParameterName = "@AccidentNo" , Value = customerData.AccidentNo}

                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateCustomerAIRequest]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion


        #region GetAICustomerRequestReport
        public InspektObj GetAICustomerRequestReport(int CustomerRequestID)
        {
            DataSet dt = new DataSet();
            InspektObj inspektObj = new InspektObj();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CustomerRequestID" , Value = CustomerRequestID}

                };
                using (dt = ADOManager.Instance.DataSet("[GetAICustomerRequestReport]", CommandType.StoredProcedure, sParameter))
                {
                    inspektObj.CaseDamageReport = dt.Tables[0].AsEnumerable().Select(cr => new CaseDamageReport
                    {
                        CaseDamageReportID = cr.Field<int>("CaseDamageID"),
                        caseId = cr.Field<string>("CaseID"),
                        inspectionId = cr.Field<string>("InspectionId"),
                        vendor = cr.Field<string>("vendor"),
                        version = cr.Field<string>("version"),
                        //EnglishMakeName = cr.Field<string>("EnglishMakeName"),
                        //ArabicMakeName = cr.Field<string>("ArabicMakeName"),
                        //ImgURL = cr.Field<string>("ImgURL"),
                        //EnglishModelName = cr.Field<string>("EnglishModelName"),
                        //ArabicModelName = cr.Field<string>("ArabicModelName")

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


        #region getFilteredAICustomerRequest
        public Object getFilteredAICustomerRequest(int CompanyID,string SearchQuery, DateTime? StartDate, DateTime? Enddate, int? PageNo)
        {
            DataSet dt = new DataSet();
            int PageCount;
            try
            {
                var customerRequests = new List<AIInspectionRequest>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@Enddate" , Value = Enddate},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},

                    };

                using (dt = ADOManager.Instance.DataSet("[getFilteredAICustomerRequest]", CommandType.StoredProcedure, sParameter))
                {
                    customerRequests = dt.Tables[0].AsEnumerable().Select(customerRequest => new AIInspectionRequest
                    {
                        CustomerRequestID = customerRequest.Field<int?>("CustomerRequestID"),
                        VehicleOwnerName = customerRequest.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = customerRequest.Field<string>("OwnerPhoneNo"),
                        VIN = customerRequest.Field<string>("VIN"),
                        AccidentNo = customerRequest.Field<string>("AccidentNo"),
                        PlateNo = customerRequest.Field<string>("PlateNo"),
                        ServiceID = customerRequest.Field<int>("ServiceID"),
                        AIResponseStatus = customerRequest.Field<string>("AIResponseStatus"),
                        AIResponseMessage = customerRequest.Field<string>("AIResponseMessage"),
                        CreatedOn = customerRequest.Field<DateTime?>("CreatedOn"),
                        AICaseID = customerRequest.Field<string>("AICaseID"),
                        AIToken = customerRequest.Field<string>("AIToken"),
                    }).ToList();
                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["PageCount"]);

                }
                Object data = new { customerRequests = customerRequests, PageCount = PageCount };
                return data;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region saveCustomerData
        public bool reSendSMS(AIInspectionRequest customerData)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@AICaseID" , Value = customerData.AICaseID},
                        new SqlParameter { ParameterName = "@AIToken" , Value = customerData.AIToken},
                        new SqlParameter { ParameterName = "@CustomerRequestID " , Value = customerData.CustomerRequestID},
                        new SqlParameter { ParameterName = "@ServiceID" , Value = customerData.ServiceID},
                        new SqlParameter { ParameterName = "@UserID " , Value = customerData.UserID}


                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateCustomerRequestRegenrateSMS]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region RejectionReportResponse
        public string RejectionReportResponse(InpektlabResponse inspektresponse)
        {
            try
            {

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@caseId" , Value = inspektresponse.CaseID},
                        new SqlParameter { ParameterName = "@InspectLabmessage" , Value = inspektresponse.message},
                        new SqlParameter { ParameterName = "@status" , Value = inspektresponse.status}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[updateAIRejectionResponse]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region changeAIRequestCustomerStatus
        public bool changeAIRequestCustomerStatus(int CustomerRequestID, int UserID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@CustomerRequestID" , Value = CustomerRequestID},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID}


                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[changeAIRequestCustomerStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region getDraftID
        public string getDraftID(string caseID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@caseID" , Value = caseID}


                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[getdraftID]", CommandType.StoredProcedure, sParameter));
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveTchekToken
        public TchekToken SaveTchekToken(TchekToken tchekobj)
        {
            DataSet dt = new DataSet();
            TchekToken TokenObj = new TchekToken();
            try
            {

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@TchekTokenID" , Value = tchekobj.TchekTokenID},
                        new SqlParameter { ParameterName = "@Token" , Value = tchekobj.Token},
                        new SqlParameter { ParameterName = "@TradeInStatus" , Value = tchekobj.TradeInStatus},
                        new SqlParameter { ParameterName = "@TradeInVehicle" , Value = tchekobj.TradeInVehicle},
                        new SqlParameter { ParameterName = "@TchekVehicleID" , Value = tchekobj.TchekVehicleID},
                        new SqlParameter { ParameterName = "@TchekAPITokenID" , Value = tchekobj.TchekAPITokenID},
                        new SqlParameter { ParameterName = "@CreatedOnTchek" , Value = tchekobj.CreatedOnTchek},
                        new SqlParameter { ParameterName = "@ExpiresIn" , Value = tchekobj.ExpiresIn},
                        new SqlParameter { ParameterName = "@TchekCustomerID" , Value = tchekobj.TchekCustomerID},
                        new SqlParameter { ParameterName = "@SendingType" , Value = tchekobj.SendingType},
                        new SqlParameter { ParameterName = "@AppID" , Value = tchekobj.AppID},
                    };

                using (dt = ADOManager.Instance.DataSet("[tchek].[SaveTchekToken]", CommandType.StoredProcedure, sParameter))
                {
                    TokenObj = dt.Tables[0].AsEnumerable().Select(token => new TchekToken
                    {
                        TokenID = token.Field<int?>("TokenID"),
                        TchekTokenID = token.Field<string>("TchekTokenID"),
                        TradeInStatus = token.Field<int?>("TradeInStatus"),
                        TradeInVehicle = token.Field<bool?>("TradeInVehicle"),
                        TchekVehicleID = token.Field<string>("TchekVehicleID"),
                        TchekAPITokenID = token.Field<string>("TchekAPITokenID"),
                        ExpiresIn = token.Field<DateTime?>("ExpiresIn"),
                        Token = token.Field<string>("Token"),
                        CreatedOnTchek = token.Field<DateTime?>("CreatedOnTchek"),
                        TchekCustomerID = token.Field<string>("TchekCustomerID"),
                        SendingType = token.Field<int?>("SendingType"),
                        AppID = token.Field<int?>("AppID")

                    }).FirstOrDefault();

                }
                return TokenObj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region SaveTchekResponse
        public string SaveTchekResponse(TchekResponse TchekResponse)
        {
            try
            {
                var XMLTchekImages = TchekResponse.images.ToXML("ArrayOfImages");
                var XMLTchekInspection = TchekResponse.tchek.ObjectToXml();
                var XMLTchekDevice = TchekResponse.device.ObjectToXml();
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@XMLTchekImages" , Value = XMLTchekImages != null ? XMLTchekImages : null},
                        new SqlParameter { ParameterName = "@XMLTchekInspection" , Value = XMLTchekInspection != null ? XMLTchekInspection : null},
                        new SqlParameter { ParameterName = "@XMLTchekDevice" , Value = XMLTchekDevice != null ? XMLTchekDevice : null},
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[tchek].[SaveTchekResponse]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "true" : "false";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region SaveHookTchekResponse
        public string SaveHookTchekResponse(TchekResponse tchek)
        {
            try
            {
                var XMLTchekDamages = tchek.damages.ToXML("ArrayOfDamage");
                var XMLTchekdamageImageList = tchek.damageImageList.ToXML("ArrayOfDamagesImageList");
                var XMLTchekDamageCost = tchek.damageCost.ToXML("ArrayOfDamageCost");
                var sParameter = new List<SqlParameter>
                    {

                        new SqlParameter { ParameterName = "@tchekID" , Value = tchek.tchek.TchekID },
                        new SqlParameter { ParameterName = "@Message" , Value = tchek.tchek.Message },
                        new SqlParameter { ParameterName = "@Token" , Value = tchek.tchek.Token},
                        new SqlParameter { ParameterName = "@DamageNumber" , Value = tchek.tchek.DamageNumber},
                        new SqlParameter { ParameterName = "@ReportUrlWithoutCosts" , Value = tchek.tchek.ReportUrlWithoutCosts},
                        new SqlParameter { ParameterName = "@ReportUrl" , Value = tchek.tchek.ReportUrl},
                        new SqlParameter { ParameterName = "@XMLTchekDamages" , Value = XMLTchekDamages != null ? XMLTchekDamages : null},
                        new SqlParameter { ParameterName = "@XMLTchekdamageImageList" , Value = XMLTchekdamageImageList != null ? XMLTchekdamageImageList : null},
                        new SqlParameter { ParameterName = "@XMLTchekDamageCost" , Value = XMLTchekDamageCost != null ? XMLTchekDamageCost : null},
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[tchek].[SaveHookResponseFromTchek]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "true" : "false";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region getCustomerRequestTchekReport
        public TchekResponse getCustomerRequestTchekReport(int CustomerRequestID)
        {
            DataSet dt = new DataSet();
            int PageCount;
            try
            {
                var tchekReport = new TchekResponse();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CustomerRequestID" , Value = CustomerRequestID}
                    };

                using (dt = ADOManager.Instance.DataSet("[tchek].[getCustomerRequestTchekReport]", CommandType.StoredProcedure, sParameter))
                {
                    tchekReport.CustomerData = dt.Tables[0].AsEnumerable().Select(customerRequest => new AIInspectionRequest
                    {
                        CustomerRequestID = customerRequest.Field<int?>("CustomerRequestID"),
                        VehicleOwnerName = customerRequest.Field<string>("VehicleOwnerName"),
                        OwnerPhoneNo = customerRequest.Field<string>("OwnerPhoneNo"),
                        VIN = customerRequest.Field<string>("VIN"),
                        AccidentNo = customerRequest.Field<string>("AccidentNo"),
                        PlateNo = customerRequest.Field<string>("PlateNo"),
                        ServiceID = customerRequest.Field<int>("ServiceID"),
                        AIResponseStatus = customerRequest.Field<string>("AIResponseStatus"),
                        AIResponseMessage = customerRequest.Field<string>("AIResponseMessage"),
                        CreatedOn = customerRequest.Field<DateTime?>("CreatedOn"),
                        AICaseID = customerRequest.Field<string>("AICaseID"),
                        AIToken = customerRequest.Field<string>("AIToken"),
                    }).FirstOrDefault();
                    tchekReport.damages = dt.Tables[1].AsEnumerable().Select(dmg => new TchekDamages
                    {
                        DamageID = dmg.Field<int?>("DamagepointID"),
                        PointName = dmg.Field<string>("PointName"),
                        PointNameArabic = dmg.Field<string>("PointNameArabic"),
                        SeverityMapEnglishName = dmg.Field<string>("SeverityMapEnglishname"),
                        TchekID = dmg.Field<string>("TchekID")
                    }).ToList();
                    tchekReport.damagePoint = dt.Tables[2].AsEnumerable().Select(dmg => new DamagePoint
                    {
                        DamagePointID = dmg.Field<int>("DamagePointID"),
                        PointName = dmg.Field<string>("PointName"),
                        PointNameArabic = dmg.Field<string>("PointNameArabic"),
                        IsDamage = dmg.Field<bool>("IsDamage"),
                    }).ToList();
                    tchekReport.damageImageList = dt.Tables[3].AsEnumerable().Select(dmg => new TchekDamageImageList
                    {
                        DamageImageListID = dmg.Field<int>("DamageImageListID"),
                        ImageUrl = dmg.Field<string>("ImageUrl"),
                        EncryptedName = dmg.Field<string>("EncryptedName")
                    }).ToList();
                    tchekReport.markers = dt.Tables[4].AsEnumerable().Select(dmg => new TchekDamageImageList
                    {
                        DamageImageListID = dmg.Field<int>("DamageImageListID"),
                        ImageUrl = dmg.Field<string>("ImageUrl"),
                        Height = dmg.Field<double?>("Height"),
                        Width = dmg.Field<double?>("Width"),
                        CenterX = dmg.Field<double?>("CenterX"),
                        CenterY = dmg.Field<double?>("CenterY")
                    }).ToList();

                }
                return tchekReport;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region getImagesFromTchek
        public TchekResponse getImagesFromTchek(int? PointID, string TchekID)
        {
            DataSet dt = new DataSet();
            int PageCount;
            try
            {
                var tchekReport = new TchekResponse();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@PointID" , Value = PointID},
                        new SqlParameter { ParameterName = "@TchekID" , Value = TchekID}
                    };

                using (dt = ADOManager.Instance.DataSet("[tchek].[gettchekImages]", CommandType.StoredProcedure, sParameter))
                {
                    tchekReport.damageImageList = dt.Tables[0].AsEnumerable().Select(dmg => new TchekDamageImageList
                    {
                        DamageImageListID = dmg.Field<int>("DamageImageListID"),
                        ImageUrl = dmg.Field<string>("ImageUrl"),
                        EncryptedName = dmg.Field<string>("EncryptedName")
                    }).ToList();
                    tchekReport.markers = dt.Tables[1].AsEnumerable().Select(dmg => new TchekDamageImageList
                    {
                        DamageImageListID = dmg.Field<int>("DamageImageListID"),
                        ImageUrl = dmg.Field<string>("ImageUrl"),
                        Height = dmg.Field<double?>("Height"),
                        Width = dmg.Field<double?>("Width"),
                        CenterX = dmg.Field<double?>("CenterX"),
                        CenterY = dmg.Field<double?>("CenterY")
                    }).ToList();

                }
                return tchekReport;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
