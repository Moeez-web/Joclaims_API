using BAL.IManager;
using BAL.Manager;
using ICSharpCode.SharpZipLib.Zip;
using Ionic.Zip;
using Microsoft.AspNet.SignalR.Hosting;
using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Report.Common;
using MODEL.Models.Report.Export_Documents;
using MODEL.Models.Request_Draft;
using Newtonsoft.Json;
using ShubeddakAPI.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Optimization;

namespace ShubeddakAPI.Controllers
{
    [Authorize]
    public class CompanyController : NotificationController
    {
        private readonly ICompanyManager _companyManager;
        public static string HtmlElemnet;
        public CompanyController()
        {
            _companyManager = new CompanyManager();
        }

        #region GetAccidentMetaData
        [HttpGet]
        [Route("api/Company/GetAccidentMetaData")]
        public IHttpActionResult GetAccidentMetaData(int CompanyID, int? WorkshopID)
        {
            AccidentMetaData result = null;
            result = _companyManager.GetAccidentMetaData(CompanyID, WorkshopID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetRequestMetaData
        [HttpGet]
        [Route("api/Company/GetRequestMetaData")]
        public IHttpActionResult GetRequestMetaData(int? CompanyID, string AccidentNo, int? AccidentID)
        {
            RequestMetaData result = null;
            result = _companyManager.GetRequestMetaData(CompanyID, AccidentNo, AccidentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetCompanyProfile
        [HttpGet]
        [Route("api/Company/GetCompanyProfile")]
        public IHttpActionResult GetCompanyProfile(int CompanyID)
        {
            //DependencyTrigger();
            CompanyProfile result = null;
            result = _companyManager.GetCompanyProfile(CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveCompanyProfile
        [HttpPost]
        [Route("api/Company/SaveCompanyProfile")]
        public HttpResponseMessage SaveCompanyProfile(Company company)
        {
            string result = null;
            result = _companyManager.SaveCompanyProfile(company);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region SavePartsRequest
        [HttpPost]
        [Route("api/Company/SavePartsRequest")]
        public async Task<HttpResponseMessage> SavePartsRequest(RequestData request)
        {

            if (request.Request.IsMakeChange == true)
            {
                joClaimsSeriesData data = new joClaimsSeriesData
                {
                    EnglishMakeName = request.Request.EnglishMakeName,
                    EnglishModelName = request.Request.EnglishModelName,
                    ProductionYear = request.Request.YearCode,
                    GroupName = request.Request.GroupName == "null" ? null : request.Request.GroupName,
                    BodyTypeName = request.Request.BodyTypeName,
                    FuelTypeName = request.Request.EngineTypeName
                };

                joClaimsSeriesData response = null;
                response = await this.PostSaveSeriesCall(data);

                if (response.ImageURL != null)
                {
                    request.Request.ImageCaseURL = response.ImageURL;
                }
                if (response.EncryptedName != null)
                {
                    request.Request.ImageCaseEncryptedName = response.EncryptedName;
                }
                request.Request.JCSeriesID = response.JCSeriesID;
            }


            RequestResponse result = null;
            result = _companyManager.SavePartsRequest(request);
            if (result.Result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else if (result.Result.Contains("Parts Already Exists"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else if (result.Result.Contains("false"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdatePartsRequest
        [HttpPost]
        [Route("api/Company/UpdatePartsRequest")]
        public async Task<HttpResponseMessage> UpdatePartsRequest(RequestData request)
        {
            if (request.Request.IsMakeChange == true)
            {
                joClaimsSeriesData data = new joClaimsSeriesData
                {
                    EnglishMakeName = request.Request.EnglishMakeName,
                    EnglishModelName = request.Request.EnglishModelName,
                    ProductionYear = request.Request.YearCode,
                    GroupName = request.Request.GroupName == "null" ? null : request.Request.GroupName,
                    BodyTypeName = request.Request.BodyTypeName,
                    FuelTypeName = request.Request.EngineTypeName
                };

                joClaimsSeriesData response = null;
                response = await this.PostSaveSeriesCall(data);

                if (response.ImageURL != null)
                {
                    request.Request.ImageCaseURL = response.ImageURL;
                }
                if (response.EncryptedName != null)
                {
                    request.Request.ImageCaseEncryptedName = response.EncryptedName;
                }
                request.Request.JCSeriesID = response.JCSeriesID;
            }


            RequestResponse result = null;
            result = _companyManager.UpdatePartsRequest(request);
            if (result.Result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else if (result.Result.Contains("Parts Already Exists"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region SaveAccident
        [HttpPost]
        [Route("api/Company/SaveAccident")]
        public async Task<HttpResponseMessage> SaveAccident(AccidentData accident)
        {
            joClaimsSeriesData data = new joClaimsSeriesData
            {
                EnglishMakeName = accident.Accident.EnglishMakeName,
                EnglishModelName = accident.Accident.EnglishModelName,
                ProductionYear = accident.Accident.YearCode,
                GroupName = accident.Accident.GroupName == "null" ? null : accident.Accident.GroupName,
                BodyTypeName = accident.Accident.BodyTypeName,
                FuelTypeName = accident.Accident.EngineTypeName,
                UserID = accident.Accident.UserID
            };

            joClaimsSeriesData response = null;
            response = await this.PostSaveSeriesCall(data);

            if (response != null && response.ImageURL != null)
            {
                accident.Accident.ImageCaseURL = response.ImageURL;
            }
            if (response != null && response.EncryptedName != null)
            {
                accident.Accident.ImageCaseEncryptedName = response.EncryptedName;
            }
            accident.Accident.JCSeriesID = response.JCSeriesID;


            string result = null;
            result = _companyManager.SaveAccident(accident);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdateAccident
        [HttpPost]
        [Route("api/Company/UpdateAccident")]
        public async Task<HttpResponseMessage> UpdateAccident(AccidentData accident)
        {
            joClaimsSeriesData data = new joClaimsSeriesData
            {
                EnglishMakeName = accident.Accident.EnglishMakeName,
                EnglishModelName = accident.Accident.EnglishModelName,
                ProductionYear = accident.Accident.YearCode,
                GroupName = accident.Accident.GroupName == "null" ? null : accident.Accident.GroupName,
                BodyTypeName = accident.Accident.BodyTypeName,
                FuelTypeName = accident.Accident.EngineTypeName
            };

            joClaimsSeriesData response = null;
            response = await this.PostSaveSeriesCall(data);

            if (response != null &&  response.ImageURL != null)
            {
                accident.Accident.ImageCaseURL = response.ImageURL;
            }
            if (response != null && response.EncryptedName != null)
            {
                accident.Accident.ImageCaseEncryptedName = response.EncryptedName;
            }
            if (response != null && response.JCSeriesID != null)
            {
                accident.Accident.JCSeriesID = response.JCSeriesID;
            }

                string result = null;
            result = _companyManager.UpdateAccident(accident);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetCompanyRequests
        [HttpGet]
        [Route("api/Company/GetCompanyRequests")]
        public IHttpActionResult GetCompanyRequests(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID, int? ModelID, int? YearID, int? ICWorkshopID, string SearchQuery, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate)
        {
            Companyrequests result = null;
            result = _companyManager.GetCompanyRequests(CompanyID, AccidentTypeID, PageNo, StatusID, WorkshopID, StartDate, EndDate, MakeID, ModelID, YearID, ICWorkshopID, SearchQuery, ApprovalStartDate, ApprovalEndDate, null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        public IHttpActionResult GetCompanyRequests(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID, int? ModelID, int? YearID, int? ICWorkshopID, string SearchQuery, DateTime? ApprovalStartDate,
          DateTime? ApprovalEndDate, int? SurveyorID)
        {
            Companyrequests result = null;
            result = _companyManager.GetCompanyRequests(CompanyID, AccidentTypeID, PageNo, StatusID, WorkshopID, StartDate, EndDate, MakeID, ModelID, YearID, ICWorkshopID, SearchQuery, ApprovalStartDate, ApprovalEndDate, SurveyorID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region GetSingleRequest
        [HttpGet]
        [Route("api/Company/GetSingleRequest")]
        public IHttpActionResult GetSingleRequest(int RequestID)
        {
            RequestData result = null;
            result = _companyManager.GetSingleRequest(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region DeletePartsRequest
        [HttpDelete]
        [Route("api/Company/DeletePartsRequest")]
        public IHttpActionResult DeletePartsRequest(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.DeletePartsRequest(RequestID, ModifiedBy);
            if (Convert.ToInt32(result) > 0)
            {
                return Ok("Request deleted successfully");
            }
            return BadRequest(result);
        }
        #endregion


        #region GetCompanyAccidents
        [HttpGet]
        [Route("api/Company/GetCompanyAccidents")]
        /////////////////For Joclaims Bahrain///////////////////////////
        public IHttpActionResult GetCompanyAccidents(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
int? ModelID, int? YearID, string SearchQuery, int? UserID, int? ICWorkshopID,int? SurveyorID)
        {
            AccidentMetaData result = null;
            result = _companyManager.GetCompanyAccidents(CompanyID, AccidentTypeID, PageNo, StatusID, WorkshopID, StartDate, EndDate, MakeID, ModelID, YearID, SearchQuery, null, null, ICWorkshopID, SurveyorID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        //////////////////////////////////////////////////////////////


        [HttpGet]
        [Route("api/Company/GetCompanyAccidents")]
        public IHttpActionResult GetCompanyAccidents(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
    int? ModelID, int? YearID, string SearchQuery, int? UserID, int? ICWorkshopID)
        {
            AccidentMetaData result = null;
            result = _companyManager.GetCompanyAccidents(CompanyID, AccidentTypeID, PageNo, StatusID, WorkshopID, StartDate, EndDate, MakeID, ModelID, YearID, SearchQuery, null, null, ICWorkshopID,null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/Company/GetCompanyAccidents")]
        public IHttpActionResult GetCompanyAccidents(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
            int? ModelID, int? YearID, string SearchQuery, int? UserID)
        {
            AccidentMetaData result = null;
            result = _companyManager.GetCompanyAccidents(CompanyID, AccidentTypeID, PageNo, StatusID, WorkshopID, StartDate, EndDate, MakeID, ModelID, YearID, SearchQuery, null, null, null,null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        #endregion

        #region GetSingleAccident
        [HttpGet]
        [Route("api/Company/GetSingleAccident")]
        public IHttpActionResult GetSingleAccident(int AccidentID)
        {
            AccidentData result = null;
            result = _companyManager.GetSingleAccident(AccidentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region DeleteAccident
        [HttpDelete]
        [Route("api/Company/DeleteAccident")]
        public IHttpActionResult DeleteAccident(int AccidentID, int userID)
        {
            string result = null;
            result = _companyManager.DeleteAccident(AccidentID, userID);
            if (result != null && result == "Accident deleted successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetICRequestQuotationData
        [HttpPost]
        [Route("api/Company/GetICRequestQuotationData")]
        public IHttpActionResult GetICRequestQuotationData(QuotationFilterModel model1)
        {
            DemandQuotations result = null;
            result = _companyManager.GetICRequestQuotationData(model1);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveOrderedParts
        [HttpPost]
        [Route("api/Company/SaveOrderedParts")]
        public IHttpActionResult SaveOrderedParts(QuotationData quotationData)
        {
            string result = null;
            result = _companyManager.SaveOrderedParts(quotationData);
            if (result.Equals("Success"))
            {
                return Ok("Order saved successfully.");
            }
            return BadRequest(result);
        }
        #endregion

        #region GetCompanyInvoice
        [HttpGet]
        [Route("api/Company/GetCompanyInvoice")]
        public IHttpActionResult GetCompanyInvoice(int RequestID, int DemandID)
        {
            QuotationData result = null;
            result = _companyManager.GetCompanyInvoice(RequestID, DemandID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetICDashboard
        [HttpGet]
        [Route("api/Company/GetICDashboard")]
        public IHttpActionResult GetICDashboard(int? CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID, int? CountryID)
        {
            ICDashboard result = null;
            result = _companyManager.GetICDashboard(StartDate, EndDate, MakeID, ModelID, IsPurchasing, AccidentTypeID, CompanyID,CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetSupplierPODetails
        [HttpGet]
        [Route("api/Company/GetSupplierPODetails")]
        public IHttpActionResult GetSupplierPODetails(int SupplierID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID)
        {
            List<Request> result = null;
            result = _companyManager.GetSupplierPODetails(SupplierID, CompanyID, StartDate, EndDate, MakeID, ModelID, IsPurchasing, AccidentTypeID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetWorkshopRODeatils
        [HttpGet]
        [Route("api/Company/GetWorkshopRODetails")]
        public IHttpActionResult GetWorkshopRODeatils(int WorkshopID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID)
        {
            List<Request> result = null;
            result = _companyManager.GetWorkshopRODeatils(WorkshopID, CompanyID, StartDate, EndDate, MakeID, ModelID, IsPurchasing, AccidentTypeID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region CheckAccidentNo
        [HttpGet]
        [Route("api/Company/CheckAccidentNo")]
        public HttpResponseMessage CheckAccidentNo(string AccidentNo, int CompanyID)
        {
            string result = null;
            result = _companyManager.CheckAccidentNo(AccidentNo, CompanyID);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region SaveWorkshop
        [HttpPost]
        [Route("api/Company/SaveWorkshop")]
        public HttpResponseMessage SaveWorkshop(ICWorkshop icWorkShop)
        {
            string result = null;
            result = _companyManager.SaveWorkshop(icWorkShop);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdateWorkshop
        [HttpPost]
        [Route("api/Company/UpdateWorkshop")]
        public HttpResponseMessage UpdateWorkshop(ICWorkshop icWorkShop)
        {
            string result = null;
            result = _companyManager.UpdateWorkshop(icWorkShop);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region DeleteWorkshop
        [HttpDelete]
        [Route("api/Company/DeleteWorkshop")]
        public IHttpActionResult DeleteWorkshop(int WorkshopID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.DeleteWorkshop(WorkshopID, ModifiedBy);
            if (Convert.ToInt32(result) > 0)
            {
                return Ok("Workshop deleted successfully");
            }
            return BadRequest(result);
        }
        #endregion

        #region GetAllWorkshops
        [HttpGet]
        [Route("api/Company/GetAllWorkshops")]
        public IHttpActionResult GetAllWorkshops(int CompanyID, int LoginUserID, int? StatusID)
        {
            List<ICWorkshop> result = null;
            result = _companyManager.GetAllWorkshops(CompanyID, LoginUserID, StatusID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetSingleWorkshop
        [HttpGet]
        [Route("api/Company/GetSingleWorkshop")]
        public IHttpActionResult GetSingleWorkshop(int WorkshopID, int LoginUserID)
        {
            ICWorkshop result = null;
            result = _companyManager.GetSingleWorkshop(WorkshopID, LoginUserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveUser
        [HttpPost]
        [Route("api/Company/SaveUser")]

        public HttpResponseMessage SaveUser(Employee user)
        {
            Employee result = null;
            result = _companyManager.SaveUser(user);
            if (result != null && (result.ErrorMessage == null || result.ErrorMessage == ""))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdateUser
        [HttpPost]
        [Route("api/Company/UpdateUser")]

        public HttpResponseMessage UpdateUser(Employee User)
        {
            string result = null;
            result = _companyManager.UpdateUser(User);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Updated successfully");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetAllUsers
        [HttpGet]
        [Route("api/Company/GetAllUsers")]
        public IHttpActionResult GetAllUsers(int UserID, int LoginCompanyID)
        {
            List<Employee> result = null;
            result = _companyManager.GetAllUsers(UserID, LoginCompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region DeleteUser
        [HttpDelete]
        [Route("api/Company/DeleteUser")]
        public IHttpActionResult DeleteUser(int UserID, int ModifiedBy)
        {
            try
            {
                string result = null;
                result = _companyManager.DeleteUser(UserID, ModifiedBy);
                if (result.Equals("Success"))
                {
                    return Ok("Deleted Successfully.");
                }
                return BadRequest(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

        }
        #endregion

        #region GetEmployeeMeta
        [HttpGet]
        [Route("api/Company/GetEmployeeMeta")]
        public IHttpActionResult GetEmployeeMeta(int CompanyID, int? EmployeeID)
        {
            CommonMeta result = null;
            result = _companyManager.GetEmployeeMeta(CompanyID, EmployeeID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region ClosePartsRequest
        [HttpGet]
        [Route("api/Company/ClosePartsRequest")]
        public IHttpActionResult ClosePartsRequest(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.ClosePartsRequest(RequestID, ModifiedBy);
            if (result.Equals("18"))
            {
                return Ok("Request Closed successfully");
            }
            return BadRequest(result);
        }
        #endregion

        #region PublishPartsRequest
        [HttpPost]
        [Route("api/Company/PublishPartsRequest")]
        public IHttpActionResult PublishPartsRequest(PublishRequest publishRequest)
        {
            NotificationController.DependencyTrigger();
            string result = null;
            result = _companyManager.PublishPartsRequest(publishRequest);
            if (result.Equals("7"))
            {
                return Ok("Request Published successfully");
            }
            else if (result.IndexOf("Invalid") > -1)
            {
                return Ok("Invalid Request");
            }
            return BadRequest(result);
        }
        #endregion

        #region AcceptRequestedPart
        [HttpPost]
        [Route("api/Company/AcceptRequestedPart")]
        public HttpResponseMessage AcceptRequestedPart(QuotationPart RejectedPart)
        //, bool IsAccepted, int ModifiedBy, string RejectReason, QuotationPart RejectedPart
        //)
        {
            List<QuotationPart> result = null;
            result = _companyManager.AcceptRequestedPart(RejectedPart);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

        }
        #endregion

        #region ReturnOrderedParts
        [HttpPost]
        [Route("api/Company/ReturnOrderedParts")]
        public HttpResponseMessage ReturnOrderedParts(QuotationPart quotationPart)
        //, bool IsAccepted, int ModifiedBy, string RejectReason, QuotationPart RejectedPart
        //)
        {
            double result;
            result = _companyManager.ReturnOrderedParts(quotationPart);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

        }
        #endregion

        #region UndoReturnOrderedParts
        [HttpPost]
        [Route("api/Company/UndoReturnOrderedParts")]
        public HttpResponseMessage UndoReturnOrderedParts(QuotationPart quotationPart)
        //, bool IsAccepted, int ModifiedBy, string RejectReason, QuotationPart RejectedPart
        //)
        {
            double result;
            result = _companyManager.UndoReturnOrderedParts(quotationPart);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }

        }
        #endregion

        #region ChangeRequestStatus
        [HttpGet]
        [Route("api/Company/ChangeRequestStatus")]
        public IHttpActionResult ChangeRequestStatus(int RequestID, int ModifiedBy, int StatusID)
        {
            string result = null;
            result = _companyManager.ChangeRequestStatus(RequestID, ModifiedBy, StatusID);
            if (result.Equals(StatusID.ToString()))
            {
                return Ok("Request status changed successfully");
            }
            return BadRequest(result);
        }
        #endregion

        #region RecycleDemand
        [HttpPost]
        [Route("api/Company/RecycleDemand")]
        public IHttpActionResult RecycleDemand(QuotationData quotationData)
        {
            string result = null;
            result = _companyManager.RecycleDemand(quotationData);

            if (result.Equals(quotationData.RequestID.ToString()))
            {
                return Ok("Demand recycled successfully");
            }
            return BadRequest(result);
        }
        #endregion

        #region UpdateAccidentStatus
        [HttpGet]
        [Route("api/Company/UpdateAccidentStatus")]
        public HttpResponseMessage UpdateAccidentStatus(int AccidentID, int StatusID, int ModifiedBy, string Reason)
        {
            string result = null;
            result = _companyManager.UpdateAccidentStatus(AccidentID, StatusID, ModifiedBy, Reason);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region ApprovePart
        [HttpGet]
        [Route("api/Company/ApprovePart")]
        public IHttpActionResult ApprovePart(int RequestedPartID, bool IsPartApproved, int ModifiedBy, string PartRejectReason)
        {
            string result = null;
            result = _companyManager.ApprovePart(RequestedPartID, IsPartApproved, ModifiedBy, PartRejectReason);
            if (result.Equals("True"))
            {
                return Ok("accepted successfully");
            }
            else if (result.Equals("False"))
            {
                return Ok("rejected successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region ReceiveRequestedPart
        [HttpGet]
        [Route("api/Company/ReceiveRequestedPart")]
        public IHttpActionResult ReceiveRequestedPart(int QuotationPartID, int ModifiedBy, int RequestID)
        {
            string result = null;
            result = _companyManager.ReceiveRequestedPart(QuotationPartID, ModifiedBy, RequestID);
            if (result != null)
            {
                return Ok("accepted successfully");
            }
            return BadRequest(result);
        }
        #endregion

        #region ApproveTask
        [HttpGet]
        [Route("api/Company/ApproveTask")]
        public IHttpActionResult ApproveTask(int RequestTaskID, bool IsTaskApproved, int ModifiedBy, string TaskRejectReason)
        {
            string result = null;
            result = _companyManager.ApproveTask(RequestTaskID, IsTaskApproved, ModifiedBy, TaskRejectReason);
            if (result.Equals("True"))
            {
                return Ok("accepted successfully");
            }
            else if (result.Equals("False"))
            {
                return Ok("rejected successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region MarkRequestReady
        [HttpGet]
        [Route("api/Company/MarkRequestReady")]
        public IHttpActionResult MarkRequestReady(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.MarkRequestReady(RequestID, ModifiedBy);
            if (result.Equals("1"))
            {
                return Ok("accepted successfully");
            }
            else if (result.Equals("0"))
            {
                return Ok("rejected successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region StartCarRepair
        [HttpGet]
        [Route("api/Company/StartCarRepair")]
        public IHttpActionResult StartCarRepair(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.StartCarRepair(RequestID, ModifiedBy);
            if (result.Equals("1"))
            {
                return Ok("accepted successfully");
            }
            else if (result.Equals("0"))
            {
                return Ok("rejected successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region RestorePartsRequest
        [HttpGet]
        [Route("api/Company/RestorePartsRequest")]
        public IHttpActionResult RestorePartsRequest(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.RestorePartsRequest(RequestID, ModifiedBy);
            if (Convert.ToInt32(result) > 0)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region SaveICUserProfile
        [HttpPost]
        [Route("api/Company/SaveICUserProfile")]
        public HttpResponseMessage SaveICUserProfile(Employee icEmployee)
        {
            string result = null;
            result = _companyManager.SaveICUserProfile(icEmployee);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region OpenPartsRequest
        [HttpGet]
        [Route("api/Company/OpenPartsRequest")]
        public IHttpActionResult OpenPartsRequest(int RequestID, int ModifiedBy, int? TempStatusID)
        {
            string result = null;
            result = _companyManager.OpenPartsRequest(RequestID, ModifiedBy, TempStatusID);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetQuotationSummary
        [HttpGet]
        [Route("api/Company/GetQuotationSummary")]
        public IHttpActionResult GetQuotationSummary(int DemandID, int UserID)
        {
            QuotationSummary result = null;
            result = _companyManager.GetQuotationSummary(DemandID, UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetClearanceSummary
        [HttpGet]
        [Route("api/Company/GetClearanceSummary")]
        public IHttpActionResult GetClearanceSummary(int CompanyID, int AccidentID)
        {
            ClearanceSummary result = null;
            result = _companyManager.GetClearanceSummary(CompanyID, AccidentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region PrintQuotationSummaryPdf
        [HttpPost]
        [Route("api/Company/PrintQuotationSummaryPdf")]
        public IHttpActionResult PrintQuotationSummaryPdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintQuotationSummaryPdf(pdfData);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetLatestRequest
        [HttpGet]
        [Route("api/Company/GetLatestRequest")]
        public IHttpActionResult GetLatestRequest(int RequestID, int? WorkshopID)
        {
            RequestData result = null;
            result = _companyManager.GetLatestRequest(RequestID, WorkshopID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetCompanyHistoryRequests
        [HttpGet]
        [Route("api/Company/GetCompanyHistoryRequests")]
        public IHttpActionResult GetCompanyHistoryRequests(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int PageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery)
        {
            Companyrequests result = null;
            result = _companyManager.GetCompanyHistoryRequests(CompanyID, AccidentTypeID, WorkshopID, PageNo, StatusID, MakeID, ModelID, YearID, searchQuery);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveClearanceReport
        [HttpPost]
        [Route("api/Company/SaveClearanceReport")]
        public IHttpActionResult SaveClearanceReport(ClearanceSummary clearanceSummaryObj)
        {
            string result = null;
            result = _companyManager.SaveClearanceReport(clearanceSummaryObj);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region DeleteRequestedPart
        [HttpDelete]
        [Route("api/Company/DeleteRequestedPart")]
        public IHttpActionResult DeleteRequestedPart(int RequestedPartID, int RequestID, int ModifiedBy, int? DamagePointID)
        {
            string result = null;
            result = _companyManager.DeleteRequestedPart(RequestedPartID, RequestID, ModifiedBy, DamagePointID);
            if (result != null && result == "Part deleted successfully")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region PrintClearanceSummaryPdf
        [HttpPost]
        [Route("api/Company/PrintClearanceSummaryPdf")]
        public IHttpActionResult PrintClearanceSummaryPdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintClearanceSummaryPdf(pdfData);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region PrintTechnicalReportPdf 
        [HttpPost]
        [Route("api/Company/PrintCITechnicalReportPdf")]
        public IHttpActionResult PrintCITechnicalReportPdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintCITechnicalReportPdf(pdfData);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccidentDamageParts
        [HttpGet]
        [Route("api/Company/GetAccidentDamageParts")]
        public IHttpActionResult GetAccidentDamageParts(int? AccidentID, int? DamagePointID, string SearchQuery, int? CountryID)
        {
            AccidentDamagePartsMeta result = null;
            result = _companyManager.GetAccidentDamageParts(AccidentID, DamagePointID, SearchQuery, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/Company/GetAccidentDamageParts")]
        public IHttpActionResult GetAccidentDamageParts(int? AccidentID, int? DamagePointID, string SearchQuery)
        {
            AccidentDamagePartsMeta result = null;
            result = _companyManager.GetAccidentDamageParts(AccidentID, DamagePointID, SearchQuery,1);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetDamagePartOptions
        [HttpGet]
        [Route("api/Company/GetDamagePartOptions")]
        public IHttpActionResult GetDamagePartOptions(int DamagePointID, int ItemNumber, string Name1, int? CountryID)
        {
            AccidentDamagePartsMeta result = null;
            result = _companyManager.GetDamagePartOptions(DamagePointID, ItemNumber, Name1, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/Company/GetDamagePartOptions")]
        public IHttpActionResult GetDamagePartOptions(int DamagePointID, int ItemNumber, string Name1)
        {
            AccidentDamagePartsMeta result = null;
            result = _companyManager.GetDamagePartOptions(DamagePointID, ItemNumber, Name1, 1);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetClearanceSavingReport
        [HttpGet]
        [Route("api/Company/GetClearanceSavingReport")]
        public IHttpActionResult GetClearanceSavingReport(int CompanyID, DateTime? SearchDateFrom, DateTime? SearchDateTo)
        {
            List<Accident> result = null;
            result = _companyManager.GetClearanceSavingReport(CompanyID, SearchDateFrom, SearchDateTo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveAccidentIsPurchasing
        [HttpGet]
        [Route("api/Company/SaveAccidentIsPurchasing")]
        public IHttpActionResult SaveAccidentIsPurchasing(int AccidentID, bool IsPurchasing, int ModifiedBy, string WorkshopDetails, int? WorkShopID)
        {
            string result = null;
            result = _companyManager.SaveAccidentIsPurchasing(AccidentID, IsPurchasing, ModifiedBy, WorkshopDetails, WorkShopID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetCompanyHistoryAccidents
        [HttpGet]
        [Route("api/Company/GetCompanyHistoryAccidents")]
        public IHttpActionResult GetCompanyHistoryAccidents(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int pageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery)
        {
            AccidentMetaData result = null;
            result = _companyManager.GetCompanyHistoryAccidents(CompanyID, AccidentTypeID, WorkshopID, pageNo, StatusID, MakeID, ModelID, YearID, searchQuery);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region ResetPassword
        [HttpGet]
        [Route("api/Company/ResetPassword")]

        public HttpResponseMessage ResetPassword(int UserID, string Password)
        {
            string result = null;
            result = _companyManager.ResetPassword(UserID, Password);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetRequestLog
        [HttpGet]
        [Route("api/Company/GetRequestLog")]
        public IHttpActionResult GetRequestLog(string RequestNumber, int RequestID)
        {
            RequestLog result = null;
            result = _companyManager.GetRequestLog(RequestNumber, RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnSuggestQuotationPrice
        [HttpGet]
        [Route("api/Company/OnSuggestQuotationPrice")]
        public IHttpActionResult OnSuggestQuotationPrice(int QuotationID, int ModifiedBy, double SuggestedPrice, int RequestID, int SupplierID, bool IsMatching, bool? ISCounterOfferAccepted, int? SuggestionOfferTime)
        {
            string result = null;
            result = _companyManager.OnSuggestQuotationPrice(QuotationID, ModifiedBy, SuggestedPrice, RequestID, SupplierID, IsMatching, ISCounterOfferAccepted, SuggestionOfferTime);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnAcceptSuggestedPrice
        [HttpGet]
        [Route("api/Company/OnAcceptSuggestedPrice")]
        public IHttpActionResult OnAcceptSuggestedPrice(int QuotationID, int ModifiedBy, bool IsSuggestionAccepted, int RequestID, int CompanyID, int SupplierID, double? CounterOfferPrice)
        {
            string result = null;
            result = _companyManager.OnAcceptSuggestedPrice(QuotationID, ModifiedBy, IsSuggestionAccepted, RequestID, CompanyID, SupplierID, CounterOfferPrice);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnAcceptCounterOffer
        [HttpGet]
        [Route("api/Company/OnAcceptCounterOffer")]
        public IHttpActionResult OnAcceptCounterOffer(int QuotationID, int ModifiedBy, int RequestID, int CompanyID, int SupplierID, bool ISCounterOfferAccepted)
        {
            string result = null;
            result = _companyManager.OnAcceptCounterOffer(QuotationID, ModifiedBy, RequestID, CompanyID, SupplierID, ISCounterOfferAccepted);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region GetPendingRequestCount
        [HttpGet]
        [Route("api/Company/GetPendingRequestCount")]
        public IHttpActionResult GetPendingRequestCount(int AccidentID)
        {
            string result = null;
            result = _companyManager.GetPendingRequestCount(AccidentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetRequestPendingParts
        [HttpGet]
        [Route("api/Company/GetRequestPendingParts")]
        public IHttpActionResult GetRequestPendingParts(int RequestID)
        {
            Object result = null;
            result = _companyManager.GetRequestPendingParts(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateICWorkshopStatus
        [HttpGet]
        [Route("api/Company/UpdateICWorkshopStatus")]
        public IHttpActionResult UpdateICWorkshopStatus(int ICWorkshopID, Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.UpdateICWorkshopStatus(ICWorkshopID, StatusID, ModifiedBy);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetWSICMetaData
        [HttpGet]
        [Route("api/Company/GetWSICMetaData")]
        public IHttpActionResult GetWSICMetaData(int WorkshopID)
        {
            WSICMeta result = null;
            result = _companyManager.GetWSICMetaData(WorkshopID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region RequestCompanyWork
        [HttpGet]
        [Route("api/Company/RequestCompanyWork")]
        public IHttpActionResult RequestCompanyWork(int WorkshopID, int CompanyID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.RequestCompanyWork(WorkshopID, CompanyID, ModifiedBy);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region PrintPOPdf
        [HttpPost]
        [Route("api/Company/PrintPOPdf")]
        public IHttpActionResult PrintPOPdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintPOPdf(pdfData);
            if (result != null && result.IndexOf("po-reports") > -1)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion
        #region SavePONote
        [HttpPost]
        [Route("api/Company/SavePONote")]
        public IHttpActionResult SavePONote(Request request)
        {
            string result = null;

            result = _companyManager.SavePONote(request);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion
        #region GetTechnicalNotesData
        [HttpGet]
        [Route("api/Company/GetTechnicalNotesData")]
        public IHttpActionResult GetTechnicalNotesData(string AccidentNo, int ObjectTypeID)
        {
            TechnicalNotesData result = null;
            result = _companyManager.GetTechnicalNotesData(AccidentNo, ObjectTypeID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region SaveTechnicalReport
        [HttpPost]
        [Route("api/Company/SaveTechnicalReport")]
        public IHttpActionResult SaveTechnicalReport(TechnicalNotesData technical)
        {
            string result = null;

            result = _companyManager.SaveTechnicalReport(technical);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion
        #region SaveBodyDamageReport
        [HttpPost]
        [Route("api/Company/SaveBodyDamageReport")]
        public IHttpActionResult SaveBodyDamageReport(TechnicalNotes technical)
        {
            string result = null;

            result = _companyManager.SaveBodyDamageReport(technical);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region OnApproveTechnicalReport

        [HttpGet]
        [Route("api/Company/OnApproveTechnicalReport")]
        public IHttpActionResult OnApproveTechnicalReport(string AccidentNo, int ObjectTypeID, int TRApprovalID, int? ModifiedBy ,bool? IsApproved, string Note, bool? IsReturn, string ReturnNote)
        {
            string result = null;
            result = _companyManager.OnApproveTechnicalReport(AccidentNo,  ObjectTypeID, TRApprovalID, ModifiedBy, IsApproved, Note, IsReturn, ReturnNote);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnApproveTechnicalReport

        [HttpPost]
        [Route("api/Company/RejectedPartByWorkshop")]
        public IHttpActionResult RejectedPartByWorkshop(QuotationPart RejectedPart)
        {
            List<QuotationPart> result = null;
            result = _companyManager.RejectedPartByWorkshop(RejectedPart);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        #endregion

        #region GetRepairOrder
        [HttpGet]
        [Route("api/Company/GetRepairOrder")]
        public IHttpActionResult GetRepairOrder(int RequestID)
        {
            RequestData result = null;
            result = _companyManager.GetRepairOrder(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region PrintROPdf
        [HttpPost]
        [Route("api/Company/PrintROPdf")]
        public IHttpActionResult PrintROPdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintROPdf(pdfData);
            if (result != null && result.IndexOf("ro-reports") > -1)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion
        #region GetAccidentCost
        [HttpGet]
        [Route("api/Company/GetAccidentCost")]
        public IHttpActionResult GetAccidentCost(string AccidentNo, string StartDate, string EndDate)
        {
            List<Request> result = null;
            result = _companyManager.GetAccidentCost(AccidentNo, StartDate, EndDate);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetICSupplier
        [HttpGet]
        [Route("api/Company/GetICSuppliers")]
        public IHttpActionResult GetICSuppliers(int CompanyID, string StartDate, string EndDate)
        {
            List<Supplier> result = null;
            result = _companyManager.GetICSuppliers(CompanyID, StartDate, EndDate);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region UpdateSupplierBlockStatus
        [HttpGet]
        [Route("api/Company/UpdateSupplierBlockStatus")]
        public IHttpActionResult UpdateSupplierBlockStatus(int CompanyID, int SupplierID, int UserID, bool IsBlocked)
        {
            string result = null;
            result = _companyManager.UpdateSupplierBlockStatus(CompanyID, SupplierID, UserID, IsBlocked);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetSupplierWorkDetail
        [HttpGet]
        [Route("api/Company/GetSupplierWorkDetail")]
        public IHttpActionResult GetSupplierWorkDetail(int CompanyID, int SupplierID)
        {
            SupplierWorkDetail result = null;
            result = _companyManager.GetSupplierWorkDetail(CompanyID, SupplierID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetAllCompanies
        [HttpGet]
        [Route("api/Company/GetAllCompanies")]
        public IHttpActionResult GetAllCompanies(int? CountryID)
        {
            List<Company> result = null;
            result = _companyManager.GetAllCompanies(CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetICLivetQuotationData
        [HttpPost]
        [Route("api/Company/GetICLivetQuotationData")]
        public IHttpActionResult GetICLivetQuotationData(QuotationFilterModel model)
        {
            DemandQuotations result = null;
            result = _companyManager.GetICLivetQuotationData(model);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region SaveWorkshopProfile
        [HttpPost]
        [Route("api/Company/SaveWorkshopProfile")]
        public HttpResponseMessage SaveWorkshopProfile(Workshop workshop)
        {
            string result = null;
            result = _companyManager.SaveWorkshopProfile(workshop);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetJCMainSeries
        [HttpGet]
        [Route("api/Company/GetJCMainSeries")]
        public IHttpActionResult GetJCMainSeries(string MakeName, string ModelName)
        {
            Object result = null;
            result = _companyManager.GetJCMainSeries(MakeName, ModelName);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getVehicleSeries
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Company/GetJCMainSeriesAndJCSeriesCase")]
        public async Task<IHttpActionResult> GetJCMainSeriesAndJCSeriesCase(string MakeName, string ModelName, string YearCode, string GroupName, int? AccidentID)
        {

            int? year = null;

            if (YearCode == "null")
            {
                year = null;
            }
            else
            {
                year = Convert.ToInt32(YearCode);
            }


            joClaimsSeriesData data = new joClaimsSeriesData
            {
                EnglishMakeName = MakeName,
                EnglishModelName = ModelName,
                ProductionYear = year,
                GroupName = GroupName == "null" ? null : GroupName
            };

            VehicleSeriesMeta response = await this.PostCall(data);
            List<JCSeriesCase> jCSeriesCase = null;
            if (response != null)
            {
                jCSeriesCase = _companyManager.GetJCMainSeriesAndJCSeriesCase(response, year);

                if (jCSeriesCase != null)
                {
                    foreach (var item in jCSeriesCase)
                    {
                        item.AccidentID = AccidentID;
                    }
                }
            }

            Object SeriesDataObj = new
            {
                jCSeriesCase = jCSeriesCase,
                joClaimsSeries = response.joClaimsSeries,
                BodyType = response.BodyType,
                FuelType = response.FuelType,
                MinYear = response.MinYear,
                MaxYear = response.MaxYear
            };
            if (SeriesDataObj != null)
            {
                return Ok(SeriesDataObj);
            }
            return NotFound();
        }


        #endregion


        #region GetJCSeriesCase
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Company/GetJCSeriesCase")]
        public async Task<IHttpActionResult> GetJCSeriesCase(string JCSeriesID, int? AccidentID, int? YearCode)
        {

            List<JCSeriesCase> jCSeriesCase = null;
            jCSeriesCase = _companyManager.GetJCSeriesCase(JCSeriesID, YearCode);

            if (jCSeriesCase != null)
            {
                foreach (var item in jCSeriesCase)
                {
                    item.AccidentID = AccidentID;
                }
            }
            if (jCSeriesCase != null)
            {
                return Ok(jCSeriesCase);
            }
            return NotFound();
        }


        #endregion


        #region DownloadCarseerImages
        [HttpGet]
        [Route("api/Company/DownloadCarseerImages")]
        public IHttpActionResult DownloadCarseerImages()
        {
            List<Gallery> result = null;
            result = _companyManager.DownloadCarseerImages();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetImageCase
        [HttpGet]
        [Route("api/Company/GetImageCase")]
        public IHttpActionResult DownloadCarseerImages(int AccidentID)
        {
            List<JCSeriesCase> result = null;
            result = _companyManager.GetImageCase(AccidentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region getVehicleSeries
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Company/GetJCMainSeries")]
        public async Task<IHttpActionResult> GetJCMainSeries(string MakeName, string ModelName, string YearCode, string GroupName)
        {
            int? year = null;

            if (YearCode == "null")
            {
                year = null;
            }
            else
            {
                year = Convert.ToInt32(YearCode);
            }


            joClaimsSeriesData data = new joClaimsSeriesData
            {
                EnglishMakeName = MakeName,
                EnglishModelName = ModelName,
                ProductionYear = year,
                GroupName = GroupName == "null" ? null : GroupName
            };

            VehicleSeriesMeta response = await this.PostCall(data);

            Object SeriesDataObj = new
            {
                joClaimsSeries = response.joClaimsSeries,
                BodyType = response.BodyType,
                FuelType = response.FuelType,
                MinYear = response.MinYear,
                MaxYear = response.MaxYear
            };
            if (SeriesDataObj != null)
            {
                return Ok(SeriesDataObj);
            }
            return NotFound();
        }


        #endregion

        #region GetTechnicalNotesLog
        [HttpGet]
        [Route("api/Company/GetTechnicalNotesLog")]
        public IHttpActionResult GetTechnicalNotesLog(string AccidentNo, string columnName, int? AccidentID, int? RequestID)
        {
            Object result = null;
            result = _companyManager.GetTechnicalNotesLog(AccidentNo, columnName, AccidentID, RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region SaveRequestDraft
        [HttpPost]
        [Route("api/Company/SaveRequestDraft")]
        public IHttpActionResult SaveRequestDraft(DraftData draftData)
        {
            int response;
            RequestResponse result = null;
            result = _companyManager.SaveRequestDraft(draftData);
            if (result.DraftID > 0)
            {
                if (result.EmailTo != null && result.EmailTo != "")
                {
                    string receivers = result.EmailTo;
                    string CCEmails;
                    if (result.EmailCC != null)
                    {
                        CCEmails = result.EmailCC;
                    }
                    else
                    {
                        CCEmails = "";
                    }
                    var subject = "New draft is created against VIN: " + result.VIN;

                    var body = result.CountryID ==  2 ? Properties.Resources.draftrequest_email_en :   Properties.Resources.draftrequest_email;
                    body = body.Replace("workshopname", result.WorkshopName);
                    body = body.Replace("carmake", result.CountryID == 2 ?  result.EnglishMakeName : result.ArabicMakeName);
                    body = body.Replace("carmodel", result.CountryID == 2 ? result.EnglishModelName : result.ArabicModelName);
                    body = body.Replace("caryear", result.YearCode.ToString());
                    body = body.Replace("vin", result.VIN);
                    body = body.Replace("policereportnumber", result.PoliceReportNumber == "" || result.PoliceReportNumber == null ? "N/A": result.PoliceReportNumber);
                    DateTime? lossDate = result.LossDate;
                    string formattedLossDate = lossDate.HasValue ? lossDate.Value.ToString("MM/dd/yyyy") : "N/A";
                    body = body.Replace("LossDate", formattedLossDate);
                    if(result.CountryID == 2)
                    {
                        body = body.Replace("PlateNo", result.PlateNo);
                        body = body.Replace("RepairDays", result.RepairDays.ToString());
                        body = body.Replace("ReplacementCarFooter", result.ReplacementCarFooter);
                    }

                    
                   
                    if (result.AccidentNo == "0")
                    {
                        body = body.Replace("vehicleownername", "N/A");
                        body = body.Replace("accidentnumber", "N/A");
                        if(result.CountryID == 2)
                        {
                            body = body.Replace("PolicyNumber", "N/A");
                        }
                        if(result.CountryID == 2)
                        {
                            body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaimsbahrain"] + "/ic/draft-list/view-draft?DraftID=" + result.AccidentID.ToString() + "");
                        }
                        else
                        {
                            body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaims"] + "/ic/draft-list/view-draft?DraftID=" + result.AccidentID.ToString() + "");
                        }
                        
                    }
                    else
                    {
                        body = body.Replace("vehicleownername", result.VehicleOwnerName);
                        body = body.Replace("accidentnumber", result.AccidentNo);
                        if(result.CountryID == 2)
                        {
                            body = body.Replace("PolicyNumber", result.PolicyNumber);
                        }
                        if (result.CountryID == 2)
                        {
                            body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaimsbahrain"] + "/ic/accident-list/view-accident?queryParam=" + result.AccidentID.ToString() + "");
                        }
                        else
                        {
                            body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaims"] + "/ic/accident-list/view-accident?queryParam=" + result.AccidentID.ToString() + "");
                        }
                            
                        
                    }

                    response = SendEmail.sendEmailwithCC(receivers, subject, body.ToString(), CCEmails);
                }
            }
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetWorkshopDraftData
        [HttpGet]
        [Route("api/Company/GetWorkshopDraftData")]
        public IHttpActionResult GetWorkshopDraftData(int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery)
        {
            Object result = null;
            result = _companyManager.GetWorkshopDraftData(StatusID, PageNo, WorkshopID, CompanyID, MakeID, ModelID, YearID, StartDate, EndDate, SearchQuery);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetSingleRequestDraft
        [HttpGet]
        [Route("api/Company/GetSingleRequestDraft")]
        public IHttpActionResult GetSingleRequestDraft(int DraftID)
        {
            Object result = null;
            result = _companyManager.GetSingleRequestDraft(DraftID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region DeleteDraftPart
        [HttpGet]
        [Route("api/Company/DeleteDraftPart")]
        public IHttpActionResult DeleteDraftPart(int DraftPartID, int ModifiedBy, int DamagePointID)
        {
            string result = null;
            result = _companyManager.DeleteDraftPart(DraftPartID, ModifiedBy, DamagePointID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateRequestDraft
        [HttpPost]
        [Route("api/Company/UpdateRequestDraft")]
        public IHttpActionResult UpdateRequestDraft(DraftData draftData)
        {
            RequestResponse result = null;
            result = _companyManager.UpdateRequestDraft(draftData);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccidentMarkers
        [HttpGet]
        [Route("api/Company/GetAccidentMarkers")]
        public IHttpActionResult GetAccidentMarkers()
        {
            List<AccidentMarker> result = null;
            result = _companyManager.GetAccidentMarkers();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SearchAccident
        [HttpGet]
        [Route("api/Company/SearchAccident")]
        public IHttpActionResult SearchAccident(string searchQuery, int CompanyID)
        {
            List<Accident> result = null;
            result = _companyManager.SearchAccident(searchQuery, CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateDraftData
        [HttpGet]
        [Route("api/Company/UpdateDraftData")]
        public IHttpActionResult UpdateDraftData(int DraftID, int AccidentID, int ModifiedBy)
        {
            //string result = null;
            //result = _companyManager.UpdateDraftData(DraftID, AccidentID, ModifiedBy);
            int response;
            RequestResponse result = null;
            result = _companyManager.UpdateDraftData(DraftID, AccidentID, ModifiedBy);
            if (result.DraftID > 0)
            {
                if (result.EmailTo != null && result.EmailTo != "")
                {
                    string receivers = result.EmailTo;
                    string CCEmails;
                    if (result.EmailCC != null)
                    {
                        CCEmails = result.EmailCC;
                    }
                    else
                    {
                        CCEmails = "";
                    }
                    var subject = "New draft is created against VIN: " + result.VIN;
                    var body = result.CountryID == 2 ? Properties.Resources.draftrequest_email_en : Properties.Resources.draftrequest_email;
                    body = body.Replace("workshopname", result.WorkshopName);
                    body = body.Replace("carmake", result.CountryID == 2 ? result.EnglishMakeName : result.ArabicMakeName);
                    body = body.Replace("carmodel",result.CountryID == 2 ?  result.EnglishModelName : result.ArabicModelName);
                    body = body.Replace("caryear", result.YearCode.ToString());
                    body = body.Replace("VIN", result.VIN);
                    body = body.Replace("policereportnumber", result.PoliceReportNumber == "" || result.PoliceReportNumber == null ? "N/A" : result.PoliceReportNumber);
                    DateTime? lossDate = result.LossDate;
                    string formattedLossDate = lossDate.HasValue ? lossDate.Value.ToString("MM/dd/yyyy") : "N/A";
                    body = body.Replace("LossDate", formattedLossDate);
                    if (result.CountryID == 2)
                    {
                        body = body.Replace("PlateNo", result.PlateNo);
                        body = body.Replace("RepairDays", result.RepairDays.ToString());
                        body = body.Replace("ReplacementCarFooter", result.ReplacementCarFooter);
                    }



                    if (result.AccidentNo == "0")
                    {
                        body = body.Replace("vehicleownername", "N/A");
                        body = body.Replace("accidentnumber", "N/A");
                        if (result.CountryID == 2)
                        {
                            body = body.Replace("PolicyNumber", "N/A");
                        }

                        body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaims"] + "/ic/draft-list/view-draft?DraftID=" + result.AccidentID.ToString() + "");
                    }
                    else
                    {
                        body = body.Replace("vehicleownername", result.VehicleOwnerName);
                        body = body.Replace("accidentnumber", result.AccidentNo);
                        if (result.CountryID == 2)
                        {
                            body = body.Replace("PolicyNumber", result.PolicyNumber);
                        }

                        body = body.Replace("ShowAccident", WebConfigurationManager.AppSettings["joclaims"] + "/ic/accident-list/view-accident?queryParam=" + result.AccidentID.ToString() + "");

                    }

                    response = SendEmail.sendEmailwithCC(receivers, subject, body.ToString(), CCEmails);
                }
            }
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetPendingDraft
        [HttpGet]
        [Route("api/Company/GetPendingDraft")]
        public IHttpActionResult GetPendingDraft(int StatusID, string VIN, int? WorkshopID)
        {
            Object result = null;
            result = _companyManager.GetPendingDraft(StatusID, VIN, WorkshopID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccident
        [HttpGet]
        [Route("api/Company/GetAccident")]
        public IHttpActionResult GetAccident(string VIN)
        {
            List<Accident> result = null;
            result = _companyManager.GetAccident(VIN,null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetAccidentNewVersion
        [HttpGet]
        [Route("api/Company/GetAccident")]
        public IHttpActionResult GetAccident(string VIN,int? CompanyID)
        {
            List<Accident> result = null;
            result = _companyManager.GetAccident(VIN,CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateDraftStatus
        [HttpGet]
        [Route("api/Company/UpdateDraftStatus")]
        public IHttpActionResult UpdateDraftStatus(int StatusID, int DraftID, int ModifiedBy, string RejectDraftReason,string RestoreDraftReason)
        {
            string result = null;
            result = _companyManager.UpdateDraftStatus(StatusID, DraftID, ModifiedBy, RejectDraftReason, RestoreDraftReason);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetPriceReductionReport
        [HttpGet]
        [Route("api/Company/GetPriceReductionReport")]
        public IHttpActionResult GetPriceReductionReport(int CompanyID, string StartDate, string EndDate, int? PageNo, int? CountryID)
        {
            Object result = null;
            result = _companyManager.GetPriceReductionReport(CompanyID, StartDate, EndDate, PageNo, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccidentForCloseRequest
        [HttpGet]
        [Route("api/Company/GetAccidentForCloseRequest")]
        public IHttpActionResult GetAccidentForCloseRequest(int RequestID)
        {
            Object result = null;
            result = _companyManager.GetAccidentForCloseRequest(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        //#region GetAccidentCarPartReport
        //[HttpGet]
        //[Route("api/Company/GetAccidentCarPartReport")]
        //public IHttpActionResult GetAccidentCarPartReport(int CompanyID, int? PageNo, int? MakeID, int? ModelID, int? YearID, string StartDate, string EndDate)
        //{
        //    Object result = null;
        //    result = _companyManager.GetAccidentCarPartReport(CompanyID, PageNo, MakeID, ModelID, YearID, StartDate, EndDate,null);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound();
        //}
        //#endregion


        #region PublishLabourOnlyRequest
        [HttpPost]
        [Route("api/Company/PublishLabourOnlyRequest")]
        public IHttpActionResult PublishLabourOnlyRequest(PublishRequest publishRequest)
        {
            string result = null;
            result = _companyManager.PublishLabourOnlyRequest(publishRequest);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region ReferTRApproval
        [HttpGet]
        [Route("api/Company/ReferTRApproval")]
        public IHttpActionResult ReferTRApproval(int UserID, string AccidentNo, int ObjectTypeID, double Total)
        {
            string result = null;
            result = _companyManager.ReferTRApproval(UserID, AccidentNo, ObjectTypeID, Total);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region TRApprovalNoteLog
        [HttpGet]
        [Route("api/Company/TRApprovalNoteLog")]
        public IHttpActionResult TRApprovalNoteLog(int UserID, int ObjectTypeID, string @AccidentNo)
        {
            List<TRApproval> result = null;
            result = _companyManager.TRApprovalNoteLog(UserID, ObjectTypeID, AccidentNo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region SaveAccountNumber
        [HttpPost]
        [Route("api/Company/save-accountnumber")]
        public IHttpActionResult SaveAccountNumber(Account Account)
        {
            Object result = null;
            result = _companyManager.SaveAccountNumber(Account);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetChequeData
        [HttpGet]
        [Route("api/Company/GetChequeData")]
        public IHttpActionResult GetChequeData(int AccidentID, int CompanyID)
        {
            Object result = null;
            result = _companyManager.GetChequeData(AccidentID, CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateChequeDate
        [HttpPost]
        [Route("api/Company/UpdateChequeDate")]
        public IHttpActionResult UpdateChequeDate(ChequeData cheque)
        {
            string result = null;
            result = _companyManager.UpdateChequeDate(cheque);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region PrintChequeDatePdf
        [HttpPost]
        [Route("api/Company/PrintChequeDatePdf")]
        public IHttpActionResult PrintChequeDatePdf(PdfData pdfData)
        {
            string result = null;
            result = _companyManager.PrintChequeDatePdf(pdfData);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccidentDocuments
        [HttpGet]
        [Route("api/Company/GetAccidentDocuments")]
        public IHttpActionResult GetAccidentDocuments(string StartDate, string EndDate, int CompanyID, int? TabID, int? StatusID, int? PageNo, int? MakeID, int? ModelID, string searchQuery, int? LossAdjusterID)
        {
            Object result = null;
            result = _companyManager.GetAccidentDocuments(StartDate, EndDate, CompanyID, TabID, StatusID, PageNo, MakeID, ModelID, searchQuery, LossAdjusterID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAccidentImages
        [HttpPost]
        [Route("api/Company/DownloadImagesZip")]
        public string DownloadImagesZip(List<ExportDocuments> export)
        {



            var fileName = "Gallery " + export[0].CompanyName + " " + export[0].timeStamp + ".zip";
            var tempOutPutPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/ExportDocuments/"), fileName);

            using (ZipOutputStream s = new ZipOutputStream(File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                byte[] buffer = new byte[4096];

                for (int j = 0; j < export.Count; j++)
                {
                    for (int i = 0; i < export[j].images.Count; i++)
                    {
                        if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), export[j].images[i].ImageURL)))
                        {
                            if (export[j].images[i].IsImage == true)
                            {
                                ZipEntry entry = new ZipEntry("Gallery/" + export[j].AccidentNumber + "/" + export[j].images[i].OriginalName);
                                entry.DateTime = DateTime.Now;
                                entry.IsUnicodeText = true;
                                s.PutNextEntry(entry);
                            }


                            using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), export[j].images[i].ImageURL)))
                            {
                                int sourceBytes;
                                do
                                {
                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                    s.Write(buffer, 0, sourceBytes);
                                } while (sourceBytes > 0);
                            }
                        }
                    }
                }
                    s.Finish();
                s.Flush();
                s.Close();


            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))


                if (finalResult == null || !finalResult.Any())
                    throw new Exception(String.Format("No Files found with Image"));

            string fileUrl = "ExportDocuments/" + fileName;


            return fileUrl;
        }
        #endregion

        #region GetAccidentDocuments
        [HttpPost]
        [Route("api/Company/DownloadZip")]
        public string DownloadZip(ExportDocuments export)
        {


            var fileName = "Documents" + export.CompanyName + " " + export.timeStamp + ".zip";
            var tempOutPutPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/ExportDocuments/"), fileName);

            using (ZipOutputStream s = new ZipOutputStream(File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                byte[] buffer = new byte[4096];




                for (int i = 0; i < export.images.Count; i++)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), export.images[i].ImageURL)))
                    {

                        if (export.images[i].DocType == 1)
                        {
                            ZipEntry entry = new ZipEntry("Documents/Car Documents/" + export.images[i].OriginalName + (export.images[i].OriginalName.Contains(".") ? "" : ".pdf"));
                            entry.DateTime = DateTime.Now;
                            entry.IsUnicodeText = true;
                            s.PutNextEntry(entry);
                        }
                        else if (export.images[i].DocType == 2)
                        {
                            ZipEntry entry = new ZipEntry("Documents/Purchase Order/" + export.images[i].OriginalName + (export.images[i].OriginalName.Contains(".") ? "" : ".pdf"));
                            entry.DateTime = DateTime.Now;
                            entry.IsUnicodeText = true;
                            s.PutNextEntry(entry);
                        }
                        else if (export.images[i].DocType == 3)
                        {
                            ZipEntry entry = new ZipEntry("Documents/Repair Order/" + export.images[i].OriginalName + (export.images[i].OriginalName.Contains(".") ? "" : ".pdf"));
                            entry.DateTime = DateTime.Now;
                            entry.IsUnicodeText = true;
                            s.PutNextEntry(entry);
                        }

                        using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), export.images[i].ImageURL)))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                }
                s.Finish();
                s.Flush();
                s.Close();


            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))


                if (finalResult == null || !finalResult.Any())
                    throw new Exception(String.Format("No Files found with Image"));

            string fileUrl = "ExportDocuments/" + fileName;


            return fileUrl;
        }
        #endregion

        #region GetSurveyorReport
        [HttpGet]
        [Route("api/Company/GetSurveyorReport")]
        public IHttpActionResult GetSurveyorReport(string StartDate, string EndDate, int CompanyID, int? MakeID, int? ModelID, int? YearID, int? UserID)
        {
            Object result = null;
            result = _companyManager.GetSurveyorReport(StartDate, EndDate, CompanyID, MakeID, ModelID, YearID, UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetSurveyorDetailRequestReport
        [HttpGet]
        [Route("api/Company/GetSurveyorDetailRequestReport")]
        public IHttpActionResult GetSurveyorDetailRequestReport(string StartDate, string EndDate, int CompanyID)
        {
            Object result = null;
            result = _companyManager.GetSurveyorDetailRequestReport(StartDate, EndDate, CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getaccidentdraft
        [HttpGet]
        [Route("api/Company/getaccidentdraft")]
        public IHttpActionResult getaccidentdraft(int CompanyID, int? PageNo, string SearchQuery, DateTime? StartDate, DateTime? EndDate, int? MakeID,
int? ModelID, int? YearID)
        {
            AccidentMetaData result = null;
            result = _companyManager.getaccidentdraft(CompanyID, PageNo, SearchQuery, StartDate, EndDate, MakeID, ModelID, YearID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getSingleAccidentDraft
        [HttpGet]
        [Route("api/Company/getSingleAccidentDraft")]
        public IHttpActionResult getSingleAccidentDraft(int CompanyID, int ClaimentID)
        {
            Object result = null;
            result = _companyManager.getSingleAccidentDraft(CompanyID, ClaimentID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        public async Task<VehicleSeriesMeta> PostCall(joClaimsSeriesData data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/GetJCMainSeries", data);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        VehicleSeriesMeta result = await Task.Run(() => JsonConvert.DeserializeObject<VehicleSeriesMeta>(responseContent));
                        return result;

                    }
                    else
                        Console.Write("Error");
                    return null;

                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        public async Task<joClaimsSeriesData> PostSaveSeriesCall(joClaimsSeriesData data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/SaveJoClaimSeriesData", data);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        joClaimsSeriesData result = await Task.Run(() => JsonConvert.DeserializeObject<joClaimsSeriesData>(responseContent));
                        return result;

                    }
                    else
                        Console.Write("Error");
                    return null;

                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }



        #region saveRequestTaskImage
        [HttpPost]
        [Route("api/Company/saveRequestTaskImage")]
        public IHttpActionResult saveRequestTaskImage(List<Image> image)
        {

            bool result = false;
            result = _companyManager.saveRequestTaskImage(image, null, null,null);
            if (result != false)
            {
                return Ok(result);
            }

            return NotFound();
        }
        #endregion

        #region saveRequestTaskImageAndPrice
        [HttpPost]
        [Route("api/Company/saveRequestTaskImageAndPrice")]
        public IHttpActionResult saveRequestTaskImage(CarReadyImagePrice CarReadyImagesPrice)
        {

            bool result = false;
            result = _companyManager.saveRequestTaskImage(CarReadyImagesPrice.image, CarReadyImagesPrice.TotalPrice, CarReadyImagesPrice.RequestID, CarReadyImagesPrice.IsEnterLabourPartPriceChecked);
            if (result != false)
            {
                return Ok(result);
            }

            return NotFound();
        }
        #endregion





        #region po_sms_service
        [HttpGet]
        [Route("api/Company/po_sms_service")]
        public async Task po_sms_service(string OwnerPhone, string SMSBody)
        {
            string JOMallLink = WebConfigurationManager.AppSettings["JOMallLink"];
            string JOMallSENDERID = WebConfigurationManager.AppSettings["JOMallSENDERID"];
            string JOMallSENDERNAME = WebConfigurationManager.AppSettings["JOMallSENDERNAME"];
            string JOMallACCPWD = WebConfigurationManager.AppSettings["JOMallACCPWD"];
            string JOMallGATEWAY = "RestSingleSMS_General/SendSMS";
            //byte[] utf32Bytes = Encoding.UTF32.GetBytes(SMSBody);
            //string arabicString = Encoding.UTF32.GetString(utf32Bytes);
            var _address = JOMallLink + JOMallGATEWAY + "?senderid=" + JOMallSENDERID + "&numbers=" + OwnerPhone + "&AccName=" + JOMallSENDERNAME + "&AccPass=" + JOMallACCPWD + "&msg=" + SMSBody;

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return;


        }
        #endregion

        #region customer_request_SMS
        public async Task customer_request_SMS(string OwnerPhone, string SMSBody, int? CompanyID)
        {
            string  JOMallLink = WebConfigurationManager.AppSettings["JOMallLink"];
            string  JOMallSENDERID = WebConfigurationManager.AppSettings["JOMallSENDERIDJoClaims"];
            string  JOMallSENDERNAME = WebConfigurationManager.AppSettings["JOMallSENDERNAMEJoClaims"];
            string  JOMallACCPWD = WebConfigurationManager.AppSettings["JOMallACCPWDJoClaims"];
            string  JOMallGATEWAY = "RestSingleSMS_General/SendSMS";
            
            var _address = JOMallLink + JOMallGATEWAY + "?senderid=" + JOMallSENDERID + "&numbers=" + OwnerPhone + "&AccName=" + JOMallSENDERNAME + "&AccPass=" + JOMallACCPWD + "&msg=" + SMSBody;

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return;


        }
        #endregion

        #region getDuplicateVinCheck
        [HttpGet]
        [Route("api/Company/getDuplicateVinCheck")]
        public IHttpActionResult getDuplicateVinCheck(int CompanyID, string VIN)
        {
            bool result = false;
            result = _companyManager.getDuplicateVinCheck(CompanyID, VIN);
            if (result != false)
            {
                return Ok(result);
            }
            else
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getInstantPriceBitUpdate
        [HttpGet]
        [Route("api/Company/getInstantPriceBitUpdate")]
        public IHttpActionResult getInstantPriceBitUpdate(int RequestID)
        {
            bool result = false;
            result = _companyManager.getInstantPriceBitUpdate(RequestID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveClaims
        [HttpGet]
        [Route("api/Company/SaveClaims")]
       
        public async Task<Object> SaveClaims(string date)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["SolidarityDataAPIURL"]);
                try
                {
                    var content = new StringContent(date.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("api/Company/SaveClaims?date=" + date);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        Object result = await Task.Run(() => JsonConvert.DeserializeObject<Object>(responseContent));
                        return result;

                    }
                    else
                        Console.Write("Error");
                    return null;

                }
                catch (AggregateException e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }

        #endregion

        #region chequedate_sms_service
        [HttpGet]
        [Route("api/Company/chequedate_sms_service")]
        public async Task chequedate_sms_service(string VehicalOwnerPhoneNumber, string SMSBody)
        {
            string JOMallLink = WebConfigurationManager.AppSettings["JOMallLink"];
            string JOMallSENDERID = WebConfigurationManager.AppSettings["JOMallSENDERIDJoClaims"];
            string JOMallSENDERNAME = WebConfigurationManager.AppSettings["JOMallSENDERNAMEJoClaims"];
            string JOMallACCPWD = WebConfigurationManager.AppSettings["JOMallACCPWDJoClaims"];
            string JOMallGATEWAY = "RestSingleSMS_General/SendSMS";
            //byte[] utf32Bytes = Encoding.UTF32.GetBytes(SMSBody);
            //string arabicString = Encoding.UTF32.GetString(utf32Bytes);
            var _address = JOMallLink + JOMallGATEWAY + "?senderid=" + JOMallSENDERID + "&numbers=" + VehicalOwnerPhoneNumber + "&AccName=" + JOMallSENDERNAME + "&AccPass=" + JOMallACCPWD + "&msg=" + SMSBody;

            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(_address);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();

            return;


        }
        #endregion

        #region savePhoneNumber
        [HttpGet]
        [Route("api/Company/savePhoneNumber")]
        public IHttpActionResult savePhoneNumber(string VehicalOwnerPhoneNumber , int AccidentID)
        {
            bool result = false;
            result = _companyManager.savePhoneNumber(VehicalOwnerPhoneNumber, AccidentID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region updateAprrovalStatusInClearance
        [HttpGet]
        [Route("api/Company/updateAprrovalStatusInClearance")]
        public IHttpActionResult updateAprrovalStatusInClearance(bool ISApproved, int ClearanceSummaryApprovalID, int UserID)
        {
            bool result = false;
            result = _companyManager.updateAprrovalStatusInClearance(ISApproved, ClearanceSummaryApprovalID, UserID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region publishRequest
        [HttpGet]
        [Route("api/Company/publishRequest")]
        public IHttpActionResult publishRequest(int RequestID, int UserID)
        {
            bool result = false;
            result = _companyManager.publishRequest(RequestID, UserID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region getAIdraftData
        [HttpGet]
        [Route("api/Company/getAIdraftData")]
        public IHttpActionResult getAIdraftData(int DraftID)
        {
            InspektObj result = null;
            result = _companyManager.getAIdraftData(DraftID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region changePermissionStatus
        [HttpPost]
        [Route("api/Company/changePermissionStatus")]
        public IHttpActionResult changePermissionStatus(ICWorkshop workshop)
        {
            bool result = false;
            result = _companyManager.changePermissionStatus(workshop);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetWorkshopAccidentData
        [HttpGet]
        [Route("api/Company/GetWorkshopAccidentData")]
        public IHttpActionResult GetWorkshopAccidentData(int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery)
        {
            Object result = null;
            result = _companyManager.GetWorkshopAccidentData(StatusID, PageNo, WorkshopID, CompanyID, MakeID, ModelID, YearID, StartDate, EndDate, SearchQuery);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region mapAccidentOnDraft
        [HttpGet]
        [Route("api/Company/mapAccidentOnDraft")]
        public IHttpActionResult mapAccidentOnDraft(int DraftID, int AccidentID, int ModifiedBy)
        {
            string result = null;
            result = _companyManager.mapAccidentOnDraft(DraftID, AccidentID, ModifiedBy);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region DownloadDraftDocs
        [HttpGet]
        [Route("api/Company/downloadDraftDocs")]
        public IHttpActionResult downloadDraftDocs(int DraftID)
        {
            Object result = null;
            result = _companyManager.downloadDraftDocs(DraftID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region downloadZipDraftDocs
        [HttpPost]
        [Route("api/Company/downloadZipDraftDocs")]
        public string DownloadImagesZip(ExportDraftDocuments exportDraftdocs)
        {



            var fileName = exportDraftdocs.draftdata.VIN + ".zip";
            var tempOutPutPath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DraftFiles/"), fileName);

            using (ZipOutputStream s = new ZipOutputStream(File.Create(tempOutPutPath)))
            {
                s.SetLevel(9); // 0-9, 9 being the highest compression  

                byte[] buffer = new byte[4096];

                for (int i = 0; i < exportDraftdocs.carImages.Count; i++)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.carImages[i].ImageURL)))
                    {

                        ZipEntry entry = new ZipEntry("CarImages/" + exportDraftdocs.carImages[i].OriginalName);
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        s.PutNextEntry(entry);

                        using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.carImages[i].ImageURL)))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                }
                for (int i = 0; i < exportDraftdocs.CarVideos.Count; i++)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.CarVideos[i].ImageURL)))
                    {

                        ZipEntry entry = new ZipEntry("CarVideos/" + exportDraftdocs.CarVideos[i].EncryptedName);
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        s.PutNextEntry(entry);

                        using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.CarVideos[i].ImageURL)))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                }
                for (int i = 0; i < exportDraftdocs.carDocuments.Count; i++)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.carDocuments[i].ImageURL)))
                    {

                        ZipEntry entry = new ZipEntry("carDocuments/" + exportDraftdocs.carDocuments[i].OriginalName);
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        s.PutNextEntry(entry);

                        using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.carDocuments[i].ImageURL)))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                }
                for (int i = 0; i < exportDraftdocs.CarVINMilage.Count; i++)
                {
                    if (System.IO.File.Exists(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.CarVINMilage[i].ImageURL)))
                    {

                        ZipEntry entry = new ZipEntry("CarVINMilage/" + exportDraftdocs.CarVINMilage[i].OriginalName);
                        entry.DateTime = DateTime.Now;
                        entry.IsUnicodeText = true;
                        s.PutNextEntry(entry);

                        using (System.IO.FileStream fs = System.IO.File.OpenRead(System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/"), exportDraftdocs.CarVINMilage[i].ImageURL)))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                }

                s.Finish();
                s.Flush();
                s.Close();


            }

            byte[] finalResult = System.IO.File.ReadAllBytes(tempOutPutPath);
            if (System.IO.File.Exists(tempOutPutPath))


                if (finalResult == null || !finalResult.Any())
                    throw new Exception(String.Format("No Files found with Image"));

            string fileUrl = "DraftFiles/" + fileName;


            return fileUrl;
        }
        #endregion

        #region GetAccidentCarPartReport
        [HttpGet]
        [Route("api/Company/GetAccidentCarPartReport")]
        public IHttpActionResult GetAccidentCarPartReport(int CompanyID, int? PageNo, int? MakeID, int? ModelID, int? YearID, string StartDate, string EndDate,string PartName, int? CountryID)
        {
            Object result = null;
            result = _companyManager.GetAccidentCarPartReport(CompanyID, PageNo, MakeID, ModelID, YearID, StartDate, EndDate, PartName, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region saveDepriciationValue
        [HttpGet]
        [Route("api/Company/saveDepriciationValue")]
        public IHttpActionResult saveDepriciationValue(int RequestID , double depriciationValue , int? requestedPartID ,int? UserID)
        {
            string result = null;
            result = _companyManager.saveDepriciationValue(RequestID, depriciationValue, requestedPartID, UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region updateSurveyorAppointment
        [HttpGet]
        [Route("api/Company/updateSurveyorAppointment")]
        public IHttpActionResult updateSurveyorAppointment(int UserID, DateTime SurveyorAppointmentDate, int accidentID)
        {
            bool result = false;
            result = _companyManager.updateSurveyorAppointment(UserID, SurveyorAppointmentDate, accidentID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveClearanceSummaryFreeText
        [HttpGet]
        [Route("api/Company/SaveClearanceSummaryFreeText")]
        public IHttpActionResult SaveClearanceSummaryFreeText(int AccidentID, string Text)
        {
            bool result = false;
            result = _companyManager.SaveClearanceSummaryFreeText(AccidentID, Text);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


    }


}
