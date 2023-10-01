using BAL.IManager;
using BAL.Manager;
using MODEL.Models;
using MODEL.Models.Report.Common;
using Newtonsoft.Json;
using ShubeddakAPI.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ShubeddakAPI.Controllers
{
    [Authorize]
    public class ShubeddakController : NotificationController
    {
        private readonly IShubeddakManager _shubeddakManager;
        public ShubeddakController()
        {
            _shubeddakManager = new ShubeddakManager();
        }

        #region GetInsuranceCompanies
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetInsuranceCompanies")]
        public IHttpActionResult GetInsuranceCompanies(int? CountryID)
        {
            //DependencyTrigger();

            Object result = null;
            result = _shubeddakManager.GetInsuranceCompanies(CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAllRequests
        //[Authorize]
        //[HttpGet]
        //[Route("api/Shubeddak/GetAllRequests")]
        //public IHttpActionResult GetAllRequests(DateTime? StartDate, DateTime? EndDate, int? MakeID, int? ModelID, int? YearID, string SearchQuery, int PageNo, int StatusID, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate, int? ICWorkshopID, int? CompanyTypeID)
        //{

        //    Companyrequests result = null;
        //    result = _shubeddakManager.GetAllRequests(StartDate, EndDate, MakeID, ModelID, YearID, SearchQuery, PageNo, StatusID, ApprovalStartDate, ApprovalEndDate, ICWorkshopID, CompanyTypeID, null);
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound();
        //}
        [HttpGet]
        [Route("api/Shubeddak/GetAllRequests")]
        public IHttpActionResult GetAllRequests(DateTime? StartDate, DateTime? EndDate, int? MakeID,int? ModelID, int? YearID, string SearchQuery, int PageNo, int StatusID, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate, int? ICWorkshopID, int? CompanyTypeID, int? CountryID)
        {

            Companyrequests result = null;
            result = _shubeddakManager.GetAllRequests(StartDate, EndDate, MakeID, ModelID, YearID, SearchQuery, PageNo, StatusID, ApprovalStartDate, ApprovalEndDate, ICWorkshopID, CompanyTypeID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region GetRequestsHistory
        [HttpGet]
        [Route("api/Shubeddak/GetRequestsHistory")]
        public IHttpActionResult GetRequestsHistory()
        {
            Companyrequests result = null;
            result = _shubeddakManager.GetRequestsHistory();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateCompanyStatus
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/UpdateCompanyStatus")]
        public IHttpActionResult UpdateCompanyStatus(int CompanyID, Int16 StatusID, string CompanyCode, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.UpdateCompanyStatus(CompanyID, StatusID, CompanyCode, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected") || result.Contains("0"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetPartShopes
        //[Authorize]
        [HttpPost]
        [Route("api/Shubeddak/GetPartShopes")]
        public IHttpActionResult GetPartShopes(PSFilter psFilter)
        {
            List<Supplier> result = null;
            result = _shubeddakManager.GetPartShopes(psFilter);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdatePartshopStatus
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/UpdatePartshopStatus")]
        public IHttpActionResult UpdatePartshopStatus(int SupplierID, Int16 StatusID, string RejectNote, int ModifiedBy)
        {
            //DependencyTrigger();

            string result = null;
            result = _shubeddakManager.UpdatePartshopStatus(SupplierID, StatusID, RejectNote, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region SaveDemand
        //[Authorize]
        [HttpPost]
        [Route("api/Shubeddak/SaveDemand")]
        public HttpResponseMessage SaveDemand(RequestData request)
        {
            string result = null;
            result = _shubeddakManager.SaveDemand(request);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Demand saved successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdateDemand
        //[Authorize]
        [HttpPost]
        [Route("api/Shubeddak/UpdateDemand")]
        public HttpResponseMessage UpdateDemand(RequestData request)
        {
            string result = null;
            result = _shubeddakManager.UpdateDemand(request);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Demand updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        //#region GetAllDemands
        ////[Authorize]
        //[HttpGet]
        //[Route("api/Shubeddak/GetAllDemands")]
        //public IHttpActionResult GetAllDemands()
        //{
        //    Companyrequests result = null;
        //    result = _shubeddakManager.GetAllDemands();
        //    if (result != null)
        //    {
        //        return Ok(result);
        //    }
        //    return NotFound();
        //}
        //#endregion

        #region GetDemand
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetDemand")]
        public IHttpActionResult GetDemand(int DemandID)
        {
            DemandProfile result = null;
            result = _shubeddakManager.GetDemand(DemandID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetQuotations
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetQuotations")]
        public IHttpActionResult GetQuotations(int DemandID)
        {
            DemandQuotations result = null;
            result = _shubeddakManager.GetQuotations(DemandID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetQuotationsByFilter
        //[Authorize]
        [HttpPost]
        [Route("api/Shubeddak/GetQuotationsByFilter")]
        public IHttpActionResult GetQuotationsByFilter(QuotationFilterModel model)
        {
            DemandQuotations result = null;
            result = _shubeddakManager.GetQuotationsByFilter(model);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveReferredQuotationParts
        //[Authorize]
        [HttpPost]
        [Route("api/shubeddak/saveReferredQuotationParts")]
        public IHttpActionResult SaveReferredQuotationParts(ReferredQuotationParts rqp)
        {
            List<Quotation> result = null;
            result = _shubeddakManager.SaveReferredQuotationParts(rqp.UserID, rqp.referredQuotationParts, rqp.tabId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        #endregion

        #region GetShubeddakDashboard
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetShubeddakDashboard")]
        public IHttpActionResult GetShubeddakDashboard()
        {
            ShubeddakDashboard result = null;
            result = _shubeddakManager.GetShubeddakDashboard();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetReferredQuotationData
        [HttpGet]
        [Route("api/Shubeddak/GetReferredSupplier")]
        public IHttpActionResult GetReferredSupplier(int RequestID)
        {
            List<Supplier> result = null;
            result = _shubeddakManager.GetReferredSupplier(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnApprovePart
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnApprovePart")]
        public IHttpActionResult OnApprovePart(int PartInfoID, Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.OnApprovePart(PartInfoID, StatusID, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion    

        #region OnApproveUvPart
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnApproveUvPart")]
        public IHttpActionResult OnApproveUvPart(int UniversalPartID, Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.OnApproveUvPart(UniversalPartID, StatusID, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region OnApproveOrder
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnApproveOrder")]
        public IHttpActionResult OnApproveOrder(int RequestID, int DemandID, int ModifiedBy, int CountDownTime, int POApprovalID, bool IsApproved)
        {
            string result = null;
            result = _shubeddakManager.OnApproveOrder(RequestID, DemandID, ModifiedBy, CountDownTime, POApprovalID, IsApproved);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        #endregion

        #region AcceptQuotationPart
        [HttpGet]
        [Route("api/Shubeddak/AcceptQuotationPart")]
        public IHttpActionResult AcceptQuotationPart(int QuotationPartID, bool IsAccepted, int ModifiedBy, string AdminRejectNote)
        {
            string result = null;
            result = _shubeddakManager.AcceptQuotationPart(QuotationPartID, IsAccepted, ModifiedBy, AdminRejectNote);
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

        #region OnReviewQuotation
        [Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnReviewQuotation")]
        public IHttpActionResult OnReviewQuotation(int QuotationID, string ReviewNote, int ModifiedBy, int SupplierID, int RequestID)
        {
            string result = null;
            result = _shubeddakManager.OnReviewQuotation(QuotationID, ReviewNote, ModifiedBy, SupplierID, RequestID);
            if (result.Equals("Success"))
            {
                return Ok("Quotation submited for review.");
            }
            return BadRequest();
        }
        #endregion

        #region CancelPurchaseOrder
        [HttpGet]
        [Route("api/Shubeddak/CancelPurchaseOrder")]
        public IHttpActionResult CancelPurchaseOrder(int RequestID, Int16 StatusID, int ModifiedBy, string RejectNote)
        {
            string result = null;
            result = _shubeddakManager.CancelPurchaseOrder(RequestID, StatusID, ModifiedBy, RejectNote);
            if (result != null)
            {
                return Ok("Cancelled successfully");
            }
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region UndoCancelPurchaseOrder
        [HttpGet]
        [Route("api/Shubeddak/UndoCancelPurchaseOrder")]
        public IHttpActionResult UndoCancelPurchaseOrder(int RequestID,int ModifiedBy, int QuotationID, int DemandID, int? QuotationPartID)
        {
            string result = null;
            result = _shubeddakManager.UndoCancelPurchaseOrder( RequestID,  ModifiedBy,  QuotationID,  DemandID, QuotationPartID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region OnApproveAllParts
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnApproveAllParts")]
        public IHttpActionResult OnApproveAllParts(int SupplierID, Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.OnApproveAllParts(SupplierID, StatusID, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion    

        #region OnApproveAllUvParts
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/OnApproveAllUvParts")]
        public IHttpActionResult OnApproveAllUvParts(Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.OnApproveAllUvParts(StatusID, ModifiedBy);
            if (result.Contains("Approved") || result.Contains("Rejected"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetQualifiedSuppliers
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetQualifiedSuppliers")]
        public IHttpActionResult GetQualifiedSuppliers(int RequestID)
        {
            List<Supplier> result = null;
            result = _shubeddakManager.GetQualifiedSuppliers(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetRequestQualifiedSuppliers
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetRequestQualifiedSuppliers")]
        public IHttpActionResult GetRequestQualifiedSuppliers()
        {
            RequestSuppliers result;
            result = _shubeddakManager.GetRequestQualifiedSuppliers();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveQuotationComment
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/SaveQuotationComment")]
        public IHttpActionResult SaveQuotationComment(string Comment, int QuotationID, int UserID)
        {
            string result = null;
            result = _shubeddakManager.SaveQuotationComment(Comment, QuotationID, UserID);
            if (result != null)
            {
                return Ok("Quotation comment Saved Successfully");
            }
            return BadRequest();
        }
        #endregion

        #region StopBiddingHours
        [HttpGet]
        [Route("api/Shubeddak/StopBiddingHours")]
        public IHttpActionResult StopBiddingHours(int RequestID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.StopBiddingHours(RequestID, ModifiedBy);
            if (result != null)
            {
                return Ok("Bidding process stopped successfully.");
            }
            return BadRequest();
        }
        #endregion

        #region UnpublishPartsRequest
        [HttpGet]
        [Route("api/Shubeddak/UnpublishPartsRequest")]
        public IHttpActionResult UnPublishPartsRequest(int RequestID, int ModifiedBy, string ReturnReason)
        {
            string result = null;
            result = _shubeddakManager.UnPublishPartsRequest(RequestID, ModifiedBy, ReturnReason);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        #endregion

        #region UpdateSupplierOffersStatus
        [HttpGet]
        [Route("api/Shubeddak/UpdateSupplierOffersStatus")]
        public IHttpActionResult UpdateSupplierOffersStatus(int QuotationID, bool IsAccepted, int ModifiedBy, string AdminRejectNote)
        {
            string result = null;
            result = _shubeddakManager.UpdateSupplierOffersStatus(QuotationID, IsAccepted, ModifiedBy, AdminRejectNote);
            if (Convert.ToInt32(result)>=-1)
            {
                return Ok(Convert.ToInt32(result));
            }            
            else
            {
                return BadRequest(result);
            }
        }
        #endregion

        #region GetHistoryRequests
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetHistoryRequests")]
        public IHttpActionResult GetHistoryRequests(int StartRow, int RowsPerPage, int? MakeID, int? ModelID, int? YearID, string searchQuery, int? CountryID)
        {
            Companyrequests result = null;
            result = _shubeddakManager.GetHistoryRequests(StartRow, RowsPerPage, MakeID, ModelID, YearID, searchQuery, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetJoAccidentMeta
        [HttpGet]
        [Route("api/Shubeddak/GetJoAccidentMeta")]
        public IHttpActionResult GetJoAccidentMeta(int? CountryID)
        {
            List<Company> result = null;
            result = _shubeddakManager.GetJoAccidentMeta(CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

      
        #endregion

        #region GetRequestAllOffers
        [HttpGet]
        [Route("api/Shubeddak/GetRequestAllOffers")]
        public IHttpActionResult GetRequestAllOffers(int RequestID)
        {
            RequestAllOffersData result = null;
            result = _shubeddakManager.GetRequestAllOffers(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region PrintOffersPdf
        [HttpPost]
        [Route("api/Shubeddak/PrintOffersPdf")]
        public IHttpActionResult PrintOffersPdf(PdfData pdfData)
        {
            string result = null;
            result = _shubeddakManager.PrintOffersPdf(pdfData);
            if (result != null && result.IndexOf("offers-reports") > -1)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetWorkshops
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetWorkshops")]
        public IHttpActionResult GetWorkshops(int? CountryID)
        {
            //DependencyTrigger();

            List<Workshop> result = null;
            result = _shubeddakManager.GetWorkshops(CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateWorkshopStatus
        //[Authorize]
        [HttpGet]
        [Route("api/Shubeddak/UpdateWorkshopStatus")]
        public IHttpActionResult UpdateWorkshopStatus(int WorkshopID, Int16 StatusID, int ModifiedBy)
        {
            string result = null;
            result = _shubeddakManager.UpdateWorkshopStatus(WorkshopID, StatusID, ModifiedBy);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion

        #region GetParts
        [HttpGet]
        [Route("api/Shubeddak/GetParts")]
        public IHttpActionResult GetParts(int StatusID, int? DamagePointID, string  SearchQuery, int PageNo,int? CountryID)
        {
            PartsMeta result = null;
            result = _shubeddakManager.GetParts(StatusID,DamagePointID,SearchQuery, PageNo,CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdatePartInfo
        [HttpPost]
        [Route("api/Shubeddak/UpdatePartInfo")]
        public IHttpActionResult UpdatPartInfo(AutomotivePart automotivePart)
        {
            string result = null;
            result = _shubeddakManager.UpdatePartInfo(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetPartsDetails
        [HttpPost]
        [Route("api/Shubeddak/GetPartsDetails")]
        public IHttpActionResult GetPartsDetails(AutomotivePart automotivePart)
        {
            PartsMeta result = null;
            result = _shubeddakManager.GetPartsDetails(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAdminUsers
        [HttpGet]
        [Route("api/Shubeddak/GetAdminUsers")]
        public IHttpActionResult GetAdminUsers(int UserID, int? CountryID)
        {
            List<ShubeddakUser> result = new List<ShubeddakUser>();
            result = _shubeddakManager.GetAdminUsers(UserID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAdminMeta
        [HttpGet]
        [Route("api/Shubeddak/GetAdminMeta")]
        public IHttpActionResult GetAdminMeta(int? UserID, int? ShubeddakUserID)
        {
            CommonMeta result = null;
            result = _shubeddakManager.GetAdminMeta(UserID, ShubeddakUserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveAdminUser
        [HttpPost]
        [Route("api/Shubeddak/SaveAdminUser")]

        public HttpResponseMessage SaveAdminUser(ShubeddakUser user)
        {
            ShubeddakUser result = null;
            result = _shubeddakManager.SaveAdminUser(user);
            if (result != null && (result.ErrorMessage == null || result.ErrorMessage == ""))
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region DeleteUser
        [HttpDelete]
        [Route("api/Shubeddak/DeleteAdminUser")]
        public IHttpActionResult DeleteAdminUser(int UserID, int ModifiedBy)
        {
            try
            {
                string result = null;
                result = _shubeddakManager.DeleteAdminUser(UserID, ModifiedBy);
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

        #region UpdateAdminUser
        [HttpPost]
        [Route("api/Shubeddak/UpdateAdminUser")]

        public HttpResponseMessage UpdateAdminUser(ShubeddakUser User)
        {
            string result = null;
            result = _shubeddakManager.UpdateAdminUser(User);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "User Updated successfully");
            }
            return Request.CreateErrorResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetRequestsByPart
        [HttpGet]
        [Route("api/Shubeddak/GetRequestsByPart")]
        public IHttpActionResult GetRequestsByPart(int AutomotivePartID)
        {
            List<Request> result = new List<Request>();
            result = _shubeddakManager.GetRequestsByPart(AutomotivePartID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveMake
        [Authorize]
        [HttpGet]
        [Route("api/Shubeddak/SaveMake")]
        public IHttpActionResult SaveMake( string MakeName,int UserID,string ImgURL,int? MakeID,string ArabicMakeName)
        {
            Object result = null;
            result = _shubeddakManager.SaveMake(MakeName,UserID,ImgURL,MakeID,ArabicMakeName);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveModel
        [Authorize]
        [HttpGet]
        [Route("api/Shubeddak/SaveModel")]
        public IHttpActionResult SaveModel(string ModelCode,int UserID,int MakeID,int? ModelID,string GroupName,string ArabicModelName)
        {
            Object result = null;
            result = _shubeddakManager.SaveModel(ModelCode,UserID,MakeID,ModelID,GroupName, ArabicModelName);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetModelAllAccident
        [Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetModelAllAccident")]
        public IHttpActionResult GetModelAllAccident(int ModelID, int UserID)
        {
            List<Request> result = new List<Request>();
            result = _shubeddakManager.GetModelAllAccident(ModelID, UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region AcceptAndRejectHybridIC
        [HttpGet]
        [Route("api/Shubeddak/AcceptAndRejectHybridIC")]
        public IHttpActionResult AcceptAndRejectHybridIC(int WorkshopID, bool IsCompanyApproved, int ModifiedBy)
        {
            string result;
            result = _shubeddakManager.AcceptAndRejectHybridIC(WorkshopID, IsCompanyApproved, ModifiedBy);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region SearchJoClaimSeriesData
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Shubeddak/SearchJoClaimSeriesData")]
        public async Task<IHttpActionResult> SearchJoClaimSeriesData(string MakeName, string ModelName, string YearCode, string GroupName,int StatusID, string JCSeriesID,string CreatedFrom,string CreatedTo, bool? face_lift,int pageNo)
        {
            int? year = null;

            if (YearCode == null)
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
                GroupName = GroupName == "null" ? null : GroupName,
                StatusID = Convert.ToInt16(StatusID),
                JCSeriesID = JCSeriesID,
                CreatedFrom = CreatedFrom,
                CreatedTo = CreatedTo,
                face_lift = face_lift,
                PageNO = pageNo

            };

            VehicleSeriesMeta response = await this.PostSearchJoClaimSeriesData(data);

            Object SeriesDataObj = new
            {
                joClaimsSeries = response.joClaimsSeries,
                BodyType = response.BodyType,
                FuelType = response.FuelType,
                PendingCount = response.PendingCount,
                ApprovedCount = response.ApprovedCount,
                RejectedCount = response.RejectedCount

            };
            if (SeriesDataObj != null)
            {
                return Ok(SeriesDataObj);
            }
            return NotFound();
        }


        #endregion


        #region CreateJoClaimSeries
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Shubeddak/CreateJoClaimSeries")]
        public async Task<IHttpActionResult> CreateJoClaimSeries(joClaimsSeriesData joClaimsSeriesData)
        {


    
        joClaimsSeriesData data = new joClaimsSeriesData
        {
          EnglishMakeName = joClaimsSeriesData.EnglishMakeName,
          EnglishModelName = joClaimsSeriesData.EnglishModelName,
          StartYear = joClaimsSeriesData.StartYear,
          EndYear = joClaimsSeriesData.EndYear,
          ModelVariant = joClaimsSeriesData.ModelVariant == "null" ? null : joClaimsSeriesData.ModelVariant,
          face_lift = joClaimsSeriesData.face_lift,
          UserID = joClaimsSeriesData.UserID,
          EncryptedName = joClaimsSeriesData.EncryptedName,
          BodyType = joClaimsSeriesData.BodyType,
          FuelType = joClaimsSeriesData.FuelType,
          BodyCode = joClaimsSeriesData.BodyCode,
          OldJoclaimSeriesID = joClaimsSeriesData.OldJoclaimSeriesID,
          OldJoclaimSeriesImage = joClaimsSeriesData.OldJoclaimSeriesImage
        };
      
 

      Object response = null;
  
        response = await this.PostCreateJoClaimSeries(data);
  
    

            Object SeriesDataObj = new
            {
                joClaimsSeriesData = response
            };
            if (SeriesDataObj != null)
            {
                return Ok(SeriesDataObj);
            }
            return NotFound();
        }
        #endregion

        #region GetSingleSeriesData
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Shubeddak/GetSingleSeriesData")]
        public async Task<IHttpActionResult> GetSingleSeriesData(string SeriesID)
        {

            SeriesCase response = null;
            response = await this.GetSingleJoClaimSeries(SeriesID);


            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }
        #endregion

        #region UpdateJoClaimSeriesData
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Shubeddak/UpdateJoClaimSeriesData")]
        public async Task<IHttpActionResult> UpdateJoClaimSeriesData(SeriesCase seriesCase)
        {





            string response = null;
            response = await this.PostUpdateJoClaimSeriesData(seriesCase);


            if (response.Equals("Success"))
            {

               var result =  this._shubeddakManager.UpdateSeriesCases(seriesCase);
                if (result.Equals("true"))
                {
                    return Ok("Updated Successfully.");
                }else
                {
                    return Ok("Error occur while updateing the series cases");
                }
            }
            else if(response.Equals("Error"))
            {
                return Ok("Series Already Exists.");
            }
            return BadRequest(response);
            
        }
        #endregion

        #region UpdateJoClaimSeriesDataStatus
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Shubeddak/UpdateJoClaimSeriesStatus")]
        public async Task<IHttpActionResult> UpdateJoClaimSeriesDataStatus(string SeriesID, int StatusID, int ModifiedBy, string NewSeriesID)
        {


            string response = null;
            response = await this.PostUpdateJoClaimSeriesDataStatus(SeriesID,StatusID,ModifiedBy, NewSeriesID);

            if(StatusID == 3)
            {
                var result = this._shubeddakManager.DeleteRejectedCase(SeriesID);
            }

            if (response.Equals("Success"))
            {
                return Ok("Updated Successfully.");
            }
            return BadRequest(response);

        }
        #endregion

        #region SaveAllImage
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Shubeddak/SaveJoClaimSeriesImage")]

        public HttpResponseMessage SaveAllImage(List<Image> images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<Image>();
            var savedImage = new Image();
            try
            {
                foreach (var img in images)
                {
                    //var gid = Guid.NewGuid();
                    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(img.OriginalName);
                    var gid = Convert.ToBase64String(plainTextBytes);
                    
                    var filename = gid + "." + "jpg";
                    filename.Replace("/", "a");
                    filename.Replace("+", "b");
                    filename.Replace("-", "c");
                    filename.Replace("=", "d");
                    img.ImageDataUrl = img.ImageDataUrl.Remove(0, 23);
                    var filepath = "CarSeriesImages/" + filename;
                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/CarSeriesImages"), filename);
                    if (!File.Exists(fileSavePath))
                    {
                        File.WriteAllBytes(fileSavePath, Convert.FromBase64String(img.ImageDataUrl));
                    }
                    savedImage.ImageURL = filepath;
                    savedImage.OriginalName = img.OriginalName;
                    savedImage.EncryptedName = filename;
                    savedImages.Add(savedImage);
                    savedImage = new Image();
                }
                response = Request.CreateResponse(HttpStatusCode.OK, savedImages, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex, Configuration.Formatters.JsonFormatter);
                throw ex;
            }
            return response;
        }
        #endregion
        #region GetAccidentBySeries
        [HttpGet]
        [Route("api/Shubeddak/GetAccidentBySeries")]
        public IHttpActionResult GetAccidentBySeries(string JCSeriesID, int PageNo)
        {
            Object data = null;
            data = _shubeddakManager.GetAccidentBySeries(JCSeriesID, PageNo);
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound();
        }
        #endregion


        public async Task<VehicleSeriesMeta> PostSearchJoClaimSeriesData(joClaimsSeriesData data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/SearchJoClaimSeriesData", data);
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

        public async Task<List<joClaimsSeriesData>> PostCreateJoClaimSeries(joClaimsSeriesData data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/CreateJoClaimSeries", data);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            List < joClaimsSeriesData> result = await Task.Run(() => JsonConvert.DeserializeObject< List<joClaimsSeriesData>>(responseContent));
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

        public async Task<string> PostUpdateJoClaimSeriesData(SeriesCase data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/UpdateJoClaimSeriesData", data);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        string result = await Task.Run(() => JsonConvert.DeserializeObject<string>(responseContent));
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

        public async Task<string> PostUpdateJoClaimSeriesDataStatus(string SeriesID, int StatusID, int ModifiedBy,string NewSeriesID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(SeriesID.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("api/JoClaims/UpdateJoClaimSeriesStatus?SeriesID="+SeriesID + "&StatusID="+StatusID + "&ModifiedBy="+ModifiedBy + "&NewSeriesID="+NewSeriesID);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        string result = await Task.Run(() => JsonConvert.DeserializeObject<string>(responseContent));
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

        public async Task<SeriesCase> GetSingleJoClaimSeries(string data)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("api/JoClaims/GetSingleJoClaimSeries?SeriesID="+data);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        SeriesCase result = await Task.Run(() => JsonConvert.DeserializeObject<SeriesCase>(responseContent));
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

        #region GetSupplierReport
    [Authorize]
    [HttpGet]
    [Route("api/Shubeddak/GetSupplierReport")]
    public IHttpActionResult GetSupplierReport(int SupplierID, DateTime StartDate, DateTime EndDate,
      int? PageNo)
    {

      SupplierReport result = new SupplierReport();
      result = _shubeddakManager.GetSupplierReport(SupplierID, StartDate, EndDate, PageNo);
      if (result != null)
      {
        return Ok(result);
      }
      return NotFound();
    }
        #endregion

        #region GetAutomotivePartLog
        [Authorize]
        [HttpGet]
        [Route("api/Shubeddak/GetAutomotivePartLog")]
        public IHttpActionResult AutomotivePartLog(int AutomotivePartID)
        {
            List<AutomotivePartLog> result;
            result = _shubeddakManager.GetAutomotivePartLog(AutomotivePartID);
            if(result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    #endregion

        #region AddSupplier
    [Authorize]
    [HttpPost]
    [Route("api/Shubeddak/AddSupplier")]
    public IHttpActionResult AddSupplier(List<PartInfo> PartInfo)
    {
      string result = null;
      result = _shubeddakManager.AddSupplier(PartInfo);
      if (result != null)
      {
        return Ok(result);
      }
      return BadRequest();
    }
    #endregion

        #region GetSupplierByModelID
    [Authorize]
    [HttpGet]
    [Route("api/Shubeddak/GetSupplierByModelID")]
    public IHttpActionResult GetSupplierByModelID(int MakeID, int ModelID2, int? ModelID1)
    {

      List<PartInfo> result = new List<PartInfo>();
      result = _shubeddakManager.GetSupplierByModelID(MakeID,ModelID2,ModelID1);
      if (result != null)
      {
        return Ok(result);
      }
      return NotFound();
    }
    #endregion

        #region GetAllRequestsByAccidentID
    [HttpGet]
    [Route("api/Shubeddak/GetAllRequests")]
    public IHttpActionResult GetAllRequestsByAccidentID(int AccidentID)
    {
      //DependencyTrigger();

      Object result = null;
      result = _shubeddakManager.GetAllRequestsByAccidentID(AccidentID);
      if (result != null)
      {
        return Ok(result);
      }
      return NotFound();
    }
        #endregion

        #region UpdateAccidentLimit
        [HttpGet]
        [Route("api/Shubeddak/UpdateAccidentLimit")]
        public IHttpActionResult UpdateAccidentLimit(int CompanyID, int? AccidentLimit)
        {
            //DependencyTrigger();

            Object result = null;
            result = _shubeddakManager.UpdateAccidentLimit(CompanyID,AccidentLimit);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region createAutomotivePartMultiDamagePoint
        [HttpPost]
        [Route("api/Shubeddak/createAutomotivePartMultiDamagePoint")]
        public IHttpActionResult CreateAutomotivepartMultiDamagePoints(AutomotivePart automotivePart)
        {
            //DependencyTrigger();

            string result = null;
            result = _shubeddakManager.CreateAutomotivepartMultiDamagePoints(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion 

        #region GetMultiAutomotiveParts
        [HttpGet]
        [Route("api/Shubeddak/GetMultiAutomotiveParts")]
        public IHttpActionResult GetMultiAutomotiveParts(string Name1, string FinalCode)
        {
            //DependencyTrigger();

            Object result = null;
            result = _shubeddakManager.GetMultiAutomotiveParts(Name1,FinalCode);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdateMultiAutomotiveParts
        [HttpPost]
        [Route("api/Shubeddak/UpdateMultiAutomotiveParts")]
        public IHttpActionResult UpdateMultiAutomotiveParts(AutomotivePart automotivePart)
        {
            //DependencyTrigger();

            string result = null;
            result = _shubeddakManager.UpdateMultiAutomotiveParts(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
    #endregion

        #region CreateSeriesCases
    [AllowAnonymous]
    [HttpPost]
    [Route("api/Shubeddak/createseriescase")]
    public async Task<IHttpActionResult> CreateSeriesCases(SeriesCase SeriesCaseObj)
    {
            string result = null;
      string response = null;
      response = await this.PostSeriesCases(SeriesCaseObj);

            if (response != null &&  response != "false" )
            {
                SeriesCaseObj.JoClaimsSeriesData.JCSeriesID = response;
                result = this._shubeddakManager.CreateCase(SeriesCaseObj);
            }

            Object SeriesDataObj = new
      {
        joClaimsSeriesData = response
      };

           
      if (SeriesDataObj != null)
      {
        return Ok(SeriesDataObj);
      }
      return NotFound();
    }
    #endregion

    public async Task<string> PostSeriesCases(SeriesCase SeriesCaseObj)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
        try
        {
          var content = new StringContent(SeriesCaseObj.ToString(), Encoding.UTF8, "application/json");

          HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/createseriescase", SeriesCaseObj);
          if (response.IsSuccessStatusCode)
          {
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            string result = await Task.Run(() => JsonConvert.DeserializeObject<string>(responseContent));
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


    #region CheckConflictedSeries
    [AllowAnonymous]
    [HttpPost]
    [Route("api/Shubeddak/checkconflictSeries")]
    public async Task<IHttpActionResult> CheckConflictseries(SeriesCase SeriesCaseObj)
    {

      Object response = null;
      response = await this.PostCheckConflictseries(SeriesCaseObj);

      if (response != null)
      {
        return Ok(response);
      }
      return NotFound();
    }
    #endregion

    public async Task<Object> PostCheckConflictseries(SeriesCase SeriesCaseObj)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
        try
        {
          var content = new StringContent(SeriesCaseObj.ToString(), Encoding.UTF8, "application/json");

          HttpResponseMessage response = await client.PostAsJsonAsync("api/JoClaims/checkconflictSeries", SeriesCaseObj);
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

        #region RemoveSeries
    [HttpPost]
    [Route("api/Shubeddak/removeseries")]
    public async Task<IHttpActionResult> RemoveSeries(SeriesCase joClaimsSeriesData)
    {

      string response = null;
      response = this._shubeddakManager.RemoveSeries(joClaimsSeriesData);

      if (response != null)
      {
        return Ok(response);
      }
      return NotFound();
    }



        #endregion

        #region UpdateSeriesModel
        [HttpGet]
        [Route("api/Shubeddak/UpdateSeriesModel")]
        public async Task<IHttpActionResult> UpdateSeriesModel(string make, string OldModel, string model)
        {

            string response = null;
            response = await this.UpdateModelInSeries(make, OldModel, model);

        
            if (response != null)
            {
                return Ok(response);
            }
            return NotFound();
        }


        public async Task<string> UpdateModelInSeries(string make, string OldModel, string model)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
                try
                {
                    var content = new StringContent(make.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("api/JoClaims/UpdateModelInSeries?Make=" + make + "&OldModel=" + OldModel + "&Model=" + model);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        string result = await Task.Run(() => JsonConvert.DeserializeObject<string>(responseContent));
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


    #region getseriesByModel
    [HttpGet]
    [Route("api/Shubeddak/getseriesByModel")]
    public async Task<IHttpActionResult> getseriesByModel(string make,string model)
    {

      List<JCSeriesCase> response = null;
      response = await this.getseriesByModelFromSeries(make, model);

      if (response != null)
      {
        return Ok(response);
      }
      return NotFound();
    }

    public async Task<List<JCSeriesCase>> getseriesByModelFromSeries(string make, string model)
    {
      using (var client = new HttpClient())
      {
        client.BaseAddress = new Uri(ConfigurationManager.AppSettings["VehicleDataAPIURL"]);
        try
        {
          var content = new StringContent(make.ToString(), Encoding.UTF8, "application/json");

          HttpResponseMessage response = await client.GetAsync("api/JoClaims/getseriesByModel?Make=" + make + "&Model=" + model);
          if (response.IsSuccessStatusCode)
          {
            var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            List<JCSeriesCase> result = await Task.Run(() => JsonConvert.DeserializeObject<List<JCSeriesCase>>(responseContent));
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

    #region getmakesandmodels
    [HttpGet]
    [Route("api/Shubeddak/getmakesandmodels")]
    public async Task<IHttpActionResult> getMakeAndModels()
    {

      Object response = null;
      response =this._shubeddakManager.getmakesandmodels();

      if (response != null)
      {
        return Ok(response);
      }
      return NotFound();
    }
    #endregion

    #region deleteModelAndReplace
    [HttpGet]
    [Route("api/Shubeddak/deleteModelAndReplace")]
    public async Task<IHttpActionResult> deleteModelAndReplace(int MakeID, int DeleteModelID, int ReplaceModelID)
    {

      bool response = false;
      response = this._shubeddakManager.deleteModelAndReplace(MakeID, DeleteModelID, ReplaceModelID);

      if (response != false)
      {
        return Ok(response);
      }
      return NotFound();
    }
        #endregion

        #region resotreSupplierAccount
        [HttpGet]
        [Route("api/Shubeddak/restoreAccount")]
        public IHttpActionResult restoreAccount(int UserID)
        {
           bool result = false;
            result = _shubeddakManager.restoreAccount(UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        #endregion

        #region ResetPasswordOthers
        [HttpGet]
        [Route("api/Shubeddak/ResetPasswordOthers")]
        public IHttpActionResult ResetPasswordOthers(string UserID, string Password)
        {
            bool result = false;
            result = _shubeddakManager.ResetPasswordOthers(UserID,Password);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        #endregion

        #region getCarReadyTaskDetail
        [HttpGet]
        [Route("api/Shubeddak/getCarReadyTaskDetail")]
        public IHttpActionResult getCarReadyTaskDetail(int RequestID)
        {
            List<RequestTask> result = null;
            result = _shubeddakManager.getCarReadyTaskDetail(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion


        #region getCarReadyImages
        [HttpGet]
        [Route("api/Shubeddak/getCarReadyImages")]
        public IHttpActionResult getCarReadyImages(int RequestID)
        {
            Object result = null;
            result = _shubeddakManager.getCarReadyImages(RequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion


        #region getAllWorkShopForAdmin
        [HttpGet]
        [Route("api/Shubeddak/getAllWorkShopForAdmin")]
        public IHttpActionResult getAllWorkShopForAdmin(int? CompanyID , int? CountryID)
        {
            List<Workshop> result = null;
            result = _shubeddakManager.getAllWorkShopForAdmin(CompanyID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region ChangePriceEstimateStatus
        [HttpGet]
        [Route("api/Shubeddak/ChangePriceEstimateStatus")]
        public IHttpActionResult ChangePriceEstimateStatus(int companyID, bool IsPriceEstimate,int userID)
        {
            bool result = false;
            result = _shubeddakManager.ChangePriceEstimateStatus(companyID,IsPriceEstimate, userID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region ChangeFeatureApproval

        [HttpPost]
        [Route("api/Shubeddak/ChangeFeatureApproval")]
        public IHttpActionResult ChangeFeatureApproval(FeaturePermission featurePermission)
        {
            int result ;
            result = _shubeddakManager.ChangeFeatureApproval(featurePermission);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetCompanyStats
        [HttpGet]
        [Route("api/Shubeddak/GetCompanyStats")]
        public IHttpActionResult GetCompanyStats(int? CompanyID, string StartDate, string EndDate, int? PageNo,int? CountryID)
        {
            Object result = null;
            result = _shubeddakManager.GetCompanyStats(CompanyID, StartDate, EndDate, PageNo, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region saveCompanyRateValue

        [HttpGet]
        [Route("api/Shubeddak/saveCompanyRateValue")]
        public IHttpActionResult saveCompanyRateValue(int CompanyRateValue,int CompanyID)
        {
            int result;
            result = _shubeddakManager.saveCompanyRateValue(CompanyRateValue, CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getCompanyDetailedStatistics

        [HttpGet]
        [Route("api/Shubeddak/getCompanyDetailedStatistics")]
        public IHttpActionResult getCompanyDetailedStatistics(int CompanyID, string StartDate,string Enddate, int? PageNo)
        {
            object result;
            result = _shubeddakManager.getCompanyDetailedStatistics(CompanyID, StartDate, Enddate,PageNo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getFeaturesPermissions

        [HttpGet]
        [Route("api/Shubeddak/getFeaturesPermissions")]
        public IHttpActionResult getFeaturesPermissions(int CompanyID)
        {
            object result;
            result = _shubeddakManager.getFeaturesPermissions(CompanyID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        
       
        


    }

}
