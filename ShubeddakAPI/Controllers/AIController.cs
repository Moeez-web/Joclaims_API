using BAL.IManager;
using BAL.IManager;
using BAL.Manager;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.SignalR.Hosting;
using MODEL.InspectAI;
using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Tchek;
using MODEL.Tchek;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ShubeddakAPI.Controllers
{
    public class AIController : ApiController
    {
        private readonly IAIManager _AIManager;
        private readonly ICompanyManager _CompanyManager;

        public AIController()
        {
            _AIManager = new AIManager();
            _CompanyManager = new CompanyManager();
        }
        #region InspectIA
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Shubeddak/AI")]
        public IHttpActionResult AI(InspektObj inspektObj)
        {
            string result = null;
            result = _AIManager.AI(inspektObj);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region saveCustomerData
        [Authorize]
        [HttpPost]
        [Route("api/AI/saveCustomerData")]
        public IHttpActionResult saveCustomerData(AIInspectionRequest customerData)
        {
            bool result = false;
            result = _AIManager.saveCustomerData(customerData);
            this.SendSMS(customerData.OwnerPhoneNo, customerData.SMSbody, customerData.CompanyID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getAICustomerRequest
        [Authorize]
        [HttpGet]
        [Route("api/AI/getAICustomerRequest")]
        public IHttpActionResult getAICustomerRequest(int CompanyID, int? PageNo)
        {
            Object result = null;
            result = _AIManager.getAICustomerRequest(CompanyID, PageNo);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getSingleCustomerRequest
        [Authorize]
        [HttpGet]
        [Route("api/AI/getSingleCustomerRequest")]
        public IHttpActionResult getSingleCustomerRequest(int CustomerRequestID)
        {
            AIInspectionRequest result = null;
            result = _AIManager.getSingleCustomerRequest(CustomerRequestID);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region updateCustomerAIRequest
        [Authorize]
        [HttpPost]
        [Route("api/AI/updateCustomerAIRequest")]
        public IHttpActionResult updateCustomerAIRequest(AIInspectionRequest customerData)
        {
            CompanyController companycontroller = new CompanyController();
            bool result = false;
            result = _AIManager.updateCustomerAIRequest(customerData);
            // var respone = companycontroller.po_sms_service(customerData.OwnerPhoneNo, customerData.SMSbody);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetAICustomerRequestReport
        [HttpGet]
        [Route("api/AI/GetAICustomerRequestReport")]
        public IHttpActionResult GetAICustomerRequestReport(int CustomerRequestID)
        {
            InspektObj result = null;
            result = _AIManager.GetAICustomerRequestReport(CustomerRequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region getFilteredAICustomerRequest
        [Authorize]
        [HttpGet]
        [Route("api/AI/getFilteredAICustomerRequest")]
        public IHttpActionResult getFilteredAICustomerRequest(int CompanyID, string SearchQuery, DateTime? StartDate, DateTime? Enddate, int? PageNo)
        {
            Object result = null;
            result = _AIManager.getFilteredAICustomerRequest(CompanyID, SearchQuery, StartDate, Enddate, PageNo);

            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region reSendSMS
        [Authorize]
        [HttpPost]
        [Route("api/AI/reSendSMS")]
        public IHttpActionResult reSendSMS(AIInspectionRequest customerData)
        {
            bool result = false;
            result = _AIManager.reSendSMS(customerData);
            this.SendSMS(customerData.OwnerPhoneNo, customerData.SMSbody, customerData.CompanyID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SendSMS
        [Authorize]
        [HttpGet]
        [Route("api/AI/SendSMS")]
        public IHttpActionResult SendSMS(string OwnerPhoneNo, string SMSbody, int CompanyID)
        {
            CompanyController companycontroller = new CompanyController();
            bool result = false;
            if (CompanyID == 48)
            {
                companycontroller.po_sms_service(OwnerPhoneNo, SMSbody);
                result = true;
            }
            else
            {
                companycontroller.customer_request_SMS(OwnerPhoneNo, SMSbody, CompanyID);
                result = true;
            }

            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region RejectionReportResponse
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Shubeddak/RejectionReportResponse")]
        public IHttpActionResult RejectionReportResponse(InpektlabResponse inspektresponse)
        {
            string result = null;
            result = _AIManager.RejectionReportResponse(inspektresponse);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region FetchAIReport
        [HttpGet]
        [Route("api/AI/FetchAIReport")]
        public async Task<IHttpActionResult> FetchAIReport(string AICaseID)
        {
            string result = null;
            var response = await this.FetchAIDataFromAutoScore(AICaseID);
            if (response.CaseDamageReport != null && response.CaseDamageReport.inspectionId != null)
            {
                result = _AIManager.AI(response); ;
                if (result != null)
                {
                    return Ok(result);
                }
            }

            return Ok(result);
        }
        public async Task<InspektObj> FetchAIDataFromAutoScore(string CaseID)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AutoScore"]);
                try
                {
                    var content = new StringContent(CaseID.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.GetAsync("ai/FetchAIData?CaseID=" + CaseID);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var result = await Task.Run(() => JsonConvert.DeserializeObject<InspektObj>(responseContent));
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
        #region tchekGenerateToken
        [HttpPost]
        [Route("api/AI/tchekGenerateToken")]
        public async Task<IHttpActionResult> tchekGenerateToken(Object tokenobj)
        {   
            TchekToken result = null;
            TchekToken response = await this.GenerateTokenFromTchek(tokenobj);
            if (response != null)
            {

                if(response.Token != null)
                {
                    result = _AIManager.SaveTchekToken(response);
                    if (result != null)
                    {
                        return Ok(result);
                    }
                    return NotFound();
                }
            }

            return Ok(response);
        }
        public async Task<TchekToken> GenerateTokenFromTchek(Object tokenobj)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AIDoozAPi"]);
                try
                {
                    var content = new StringContent(tokenobj.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("api/tchek/GenerateToken", content);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var result = await Task.Run(() => JsonConvert.DeserializeObject<TchekToken>(responseContent));
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

        #region SaveTchekResponse
        [AllowAnonymous]
        [HttpPost]
        [Route("api/AI/SaveTchekResponse")]
        public IHttpActionResult SaveTchekResponse(TchekResponse TchekResponse)
        {
            string result = null;
            result = _AIManager.SaveTchekResponse(TchekResponse);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region changeAIRequestCustomerStatus
        [HttpGet]
        [Route("api/AI/changeAIRequestCustomerStatus")]
        public IHttpActionResult changeAIRequestCustomerStatus(int CustomerRequestID,int UserID)
        {
            bool result = false;
            result = _AIManager.changeAIRequestCustomerStatus(CustomerRequestID, UserID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveHookTchekResponse
        [HttpPost]
        [Route("api/AI/SaveHookTchekResponse")]
        public IHttpActionResult SaveHookTchekResponse(TchekResponse tchek)
        {
            string result;
            result = _AIManager.SaveHookTchekResponse(tchek);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region getDraftID
        [HttpGet]
        [Route("api/AI/getDraftID")]
        public IHttpActionResult getDraftID(string caseID)
        {
            string result = null;
            result = _AIManager.getDraftID(caseID);
            if (result != null)
            {
                InspektObj response = _CompanyManager.getAIdraftData(Convert.ToInt32(result));
                return Ok(response);
            }
            return NotFound();
        }
        #endregion

        #region getCustomerRequestTchekReport
        [HttpGet]
        [Route("api/AI/getCustomerRequestTchekReport")]
        public IHttpActionResult getCustomerRequestTchekReport(int CustomerRequestID)
        {
            TchekResponse result = null;
            result = _AIManager.getCustomerRequestTchekReport(CustomerRequestID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion

        #region getImagesFromTchek
        [HttpGet]
        [Route("api/Common/getImagesFromTchek")]
        public IHttpActionResult getImagesFromTchek(int? PointID, string TchekID)
        {
            TchekResponse result = null;
            result = _AIManager.getImagesFromTchek(PointID, TchekID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion
    }

}
