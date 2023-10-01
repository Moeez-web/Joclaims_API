using BAL.IManager;
using BAL.Manager;
using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShubeddakAPI.Controllers
{
    [Authorize]
    public class PartShopController : NotificationController
    {
        private readonly IPartShopManager _partShopManager;

        public PartShopController()
        {
            _partShopManager = new PartShopManager();
        }

        #region GetPartShopProfile
        [HttpGet]
        [Route("api/Partshop/GetPartShopProfile")]
        public IHttpActionResult GetPartShopProfile(int SupplierID)
        {
            //DependencyTrigger();
            SupplierProfile result = null;
            result = _partShopManager.GetPartShopProfile(SupplierID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        //#region SavePartShopProfile
        //[HttpPost]
        //[Route("api/Partshop/SavePartShopProfile")]
        //public HttpResponseMessage SavePartShopProfile(SupplierProfile profileData)
        //{
        //    string result = null;
        //    result = _partShopManager.SavePartShopProfile(profileData);
        //    if (result != null)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        //}
        //#endregion

        #region UpdatePartShopProfile
        [HttpPost]
        [Route("api/Partshop/UpdatePartShopProfile")]
        public HttpResponseMessage UpdatePartShopProfile(SupplierProfile profileData)
        {
            Supplier result;
            result = _partShopManager.UpdatePartShopProfile(profileData);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region SaveParts
        [HttpPost]
        [Route("api/Partshop/SaveParts")]
        public HttpResponseMessage SaveParts(SavePart partInfoData)
        {
            SavePart result = null;
            result = _partShopManager.SaveParts(partInfoData);
            if (result.PartsInfo != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion
        #region SaveUvParts
        [HttpPost]
        [Route("api/Partshop/SaveUvPart")]

        public HttpResponseMessage SaveUvPart(AutomotivePart uvPart)
        {
            string result = null;
            result = _partShopManager.SaveUvPart(uvPart);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdatePartInfo
        [HttpPost]
        [Route("api/Partshop/UpdatePartInfo")]
        public HttpResponseMessage UpdatePartInfo(UpdatePart partInfoData)
        {
            string result = null;
            result = _partShopManager.UpdatePartInfo(partInfoData);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Part Updated Successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region DeletePart
        [HttpPost]
        [Route("api/Partshop/DeletePart")]
        public HttpResponseMessage DeletePart(int PartInfoID, int SupplierID)
        {
            string result = null;
            result = _partShopManager.DeletePart(PartInfoID, SupplierID);
            if (Convert.ToInt32(result) > 0)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Part Deleted Successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetPartsDemands
        [HttpGet]
        [Route("api/Partshop/GetPartsDemands")]
        public IHttpActionResult GetPartsDemands(int SupplierID)
        {
            Companyrequests result = null;
            result = _partShopManager.GetPartsDemands(SupplierID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SavePartsQuotation
        [HttpPost]
        [Route("api/Partshop/SavePartsQuotation")]
        public HttpResponseMessage SavePartsQuotation(RequestData request)
        {
            string result = null;
            var lenght = request.RequestedParts.FindAll(part => part.IsIncluded == true).Count();
            if (lenght > 0)
            {
                result = _partShopManager.SavePartsQuotation(request);
                if (result.Equals("Request saved successfully") && !result.Equals(0.ToString()))
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (result.Equals(0.ToString()))
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Supplier Already Quoted");
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest, result);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Please Choose Atleast One Part");
            }

        }
        #endregion

        #region UpdatePartsQuotation
        [HttpPost]
        [Route("api/Partshop/UpdatePartsQuotation")]
        public HttpResponseMessage UpdatePartsQuotation(QuotationData QuotationData)
        {
            string result = null;
            result = _partShopManager.UpdatePartsQuotation(QuotationData);
            if (result.Contains("Success"))
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Request updated successfully");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetShopQuotations
        [HttpGet]
        [Route("api/Partshop/GetShopQuotations")]
        //[Route("api/Partshop/GetShopQuotations/{SupplierID:int}/{StatusID:int}/{PageNo:int}/{MakeID:int?}/{ModelID:int?}/{YearID:int?}/{POStartDate:DateTime?}/{POEndDate:DateTime?}/{AccidentNo}/{CompanyID:int?}/{SearchQuery}")]
        public IHttpActionResult GetShopQuotations(int SupplierID, int StatusID, int PageNo, int? MakeID, int? ModelID, int? YearID, DateTime? POStartDate, DateTime? POEndDate, string AccidentNo, int? CompanyID, string SearchQuery)
        {
            ShopQuotations result = null;
            result = _partShopManager.GetShopQuotations(SupplierID, StatusID, PageNo, MakeID, ModelID, YearID, POStartDate, POEndDate, AccidentNo, CompanyID, SearchQuery);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [Route("api/Partshop/GetShopQuotations")]
        public IHttpActionResult GetShopQuotations(int SupplierID, int StatusID, int PageNo, int? MakeID, int? ModelID, int? YearID, DateTime? POStartDate, DateTime? POEndDate, string AccidentNo, int? CompanyID)
        {
            ShopQuotations result = null;
            result = _partShopManager.GetShopQuotations(SupplierID, StatusID, PageNo, MakeID, ModelID, YearID, POStartDate, POEndDate, AccidentNo, CompanyID, null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetSingleQuotation
        [HttpGet]
        [Route("api/Partshop/GetSingleQuotation")]
        public IHttpActionResult GetSingleQuotation(int QuotationID)
        {
            QuotationData result = null;
            result = _partShopManager.GetSingleQuotation(QuotationID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetQuotationRequestData
        [HttpGet]
        [Route("api/Partshop/GetQuotationRequestData")]
        public IHttpActionResult GetQuotationRequestData(int RequestID, int? QuotationID, int SupplierID)
        {
            QuotationData result = null;
            result = _partShopManager.GetQuotationRequestData(RequestID, QuotationID, SupplierID);
            if (result.QuotationParts != null)
            {
                foreach (var item in result.QuotationParts)
                {
                    item.Price = item.WithoutDiscountPrice;
                }
            }
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetQuotationRequestData
        [HttpGet]
        [Route("api/Partshop/GetQuotationData")]
        public IHttpActionResult GetQuotationRequestData(int RequestID, int? QuotationID, int SupplierID, int? RoleID)
        {
            QuotationData result = null;
            result = _partShopManager.GetQuotationRequestData(RequestID, QuotationID, SupplierID);
            if (RoleID != null && RoleID == 6 && QuotationID != null)
            {
                foreach (var item in result.QuotationParts)
                {
                    item.Price = item.WithoutDiscountPrice;
                }
            }
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region GetPSInvoice
        [HttpGet]
        [Route("api/Partshop/GetPSInvoice")]
        public IHttpActionResult GetPSInvoice(int QuotationID, int DemandID)
        {
            QuotationData result = null;
            result = _partShopManager.GetPSInvoice(QuotationID, DemandID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetDemandRequestData
        [HttpGet]
        [Route("api/Partshop/GetDemandRequestData")]
        public IHttpActionResult GetDemandRequestData(int RequestID, int? QuotationID, int SupplierID)
        {
            QuotationData result = null;
            result = _partShopManager.GetDemandRequestData(RequestID, QuotationID, SupplierID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetPSDashboard
        [HttpGet]
        [Route("api/Partshop/GetPSDashboard")]
        public IHttpActionResult GetPSDashboard(int SupplierID)
        {
            PSDashboard result = null;
            result = _partShopManager.GetPSDashboard(SupplierID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveBranch
        [HttpPost]
        [Route("api/Partshop/SaveBranch")]
        public HttpResponseMessage SaveBranch(PartBranch PartBranch)
        {
            string result = null;
            result = _partShopManager.SaveBranch(PartBranch);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region UpdateBranch
        [HttpPost]
        [Route("api/Partshop/UpdateBranch")]
        public HttpResponseMessage UpdateBranch(PartBranch PartBranch)
        {
            string result = null;
            result = _partShopManager.UpdateBranch(PartBranch);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region DeleteBranch
        [HttpDelete]
        [Route("api/Partshop/DeleteBranch")]
        public IHttpActionResult DeleteBranch(int BranchID, int ModifiedBy)
        {
            string result = null;
            result = _partShopManager.DeleteBranch(BranchID, ModifiedBy);
            if (result.Equals("Success") || result.Equals("Error"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        #endregion


        #region DeleteUvPart
        [HttpDelete]
        [Route("api/Partshop/DeleteUvPart")]
        public IHttpActionResult DeleteUvPart(int UniversalPartID, int ModifiedBy)
        {
            string result = null;
            result = _partShopManager.DeleteUvPart(UniversalPartID, ModifiedBy);
            if (result.Equals("Success"))
            {
                return Ok("Part deleted successfully");
            }
            return BadRequest(result);
        }
        #endregion


        #region GetAllBranchs
        [HttpGet]
        [Route("api/Partshop/GetAllBranches")]
        public IHttpActionResult GetAllBranches(int SupplierID, int LoginUserID)
        {
            List<PartBranch> result = null;
            result = _partShopManager.GetAllBranches(SupplierID, LoginUserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetSingleBranch
        [HttpGet]
        [Route("api/Partshop/GetSingleBranch")]
        public IHttpActionResult GetSingleBranch(int BranchID, int LoginUserID)
        {
            PartBranch result = null;
            result = _partShopManager.GetSingleBranch(BranchID, LoginUserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveInvoiceImage
        [HttpPost]
        [Route("api/Partshop/SaveInvoiceImage")]
        public HttpResponseMessage SaveInvoiceImage(Quotation quotation)
        {
            string result = null;
            result = _partShopManager.SaveInvoiceImage(quotation);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region updatePartiallySellingStatus
        [HttpGet]
        [Route("api/Partshop/updatePartiallySellingStatus")]
        public HttpResponseMessage updatePartiallySellingStatus(int QuotationID, bool IsPartialSellings, int ModifiedBy)
        {
            string result = null;
            result = _partShopManager.updatePartiallySellingStatus(QuotationID, IsPartialSellings, ModifiedBy);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }


        #endregion


        #region QuotationNotAvailable
        [HttpGet]
        [Route("api/Partshop/QuotationNotAvailable")]
        public HttpResponseMessage QuotationNotAvailable(int RequestID, int SupplierID, int DemandID, string notAvailableNote)
        {
            string result = null;
            result = _partShopManager.QuotationNotAvailable(RequestID, SupplierID, DemandID, notAvailableNote);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region GetAppVersion
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Partshop/GetAppVersion")]
        public HttpResponseMessage GetAppVersion(string MobileAppVersion, int? SupplierID)
        {
            AppSetting result = null;
            result = _partShopManager.GetAppVersion(MobileAppVersion, SupplierID);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion
        #region GetMobileAppVersion
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Partshop/GetMobileAppVersion")]
        public HttpResponseMessage GetMobileAppVersion(string MobileAppVersion, int? UserID)
        {
            AppSetting result = null;
            result = _partShopManager.GetMobileAppVersion(MobileAppVersion, UserID);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }
        #endregion

        #region CheckQuotationPartPrice
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Partshop/CheckQuotationPartPrice")]
        public IHttpActionResult CheckQuotationPartPrice(double price)
        {
            bool result = false;

            return Ok(result);
        }
        #endregion

        #region saveTotallabourPartsPrice
        [HttpGet]
        [Route("api/Partshop/saveTotallabourPartsPrice")]
        public HttpResponseMessage saveTotallabourPartsPrice(int demandID, int TotallabourPartsPrice,int UserID,int CompanyTypeID, int RoleID)
        {
            string result = null;
            result = _partShopManager.saveTotallabourPartsPrice(demandID, TotallabourPartsPrice, UserID, CompanyTypeID, RoleID);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, result);
        }


        #endregion

    }
}
