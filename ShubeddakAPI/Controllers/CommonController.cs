using BAL.IManager;
using BAL.Manager;
using IronPdf;
using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Report.Common;
using MODEL.Models.Request_Draft;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using System.Configuration;

namespace ShubeddakAPI.Controllers
{
    public class CommonController : ApiController
    {
        private readonly ICommonManager _commonManager;

        public CommonController()
        {
            _commonManager = new CommonManager();
        }

        #region SaveAllImage
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveAllImage")]

        public HttpResponseMessage SaveAllImage(List<Image> images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<Image>();
            var savedImage = new Image();
            try
            {
                foreach (var img in images)
                {
                    var gid = Guid.NewGuid();
                    var filename = gid + "." + "jpg";
                    img.ImageDataUrl = img.ImageDataUrl.Remove(0, 23);
                    var filepath = "UploadedFiles/" + filename;
                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), filename)   ;
                    File.WriteAllBytes(fileSavePath, Convert.FromBase64String(img.ImageDataUrl));
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

        #region SaveImage
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveImage")]

        public HttpResponseMessage SaveImage(Image image)
        {
            var response = new HttpResponseMessage();
            try
            {
                    var gid = Guid.NewGuid();
                    var filename = gid + "." + "jpg";
                    image.ImageDataUrl = image.ImageDataUrl.Remove(0, 23);
                    var filepath = "UploadedFiles/" + filename;
                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), filename);
                    File.WriteAllBytes(fileSavePath, Convert.FromBase64String(image.ImageDataUrl));
                    image.EncryptedName = filename;
                    image.ImageURL = filepath;

                response = Request.CreateResponse(HttpStatusCode.OK, image, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex, Configuration.Formatters.JsonFormatter);
                throw ex;
            }
            return response;
        }
        #endregion

        #region DeleteImage

        [Authorize]
        [HttpGet]
        [Route("api/Common/DeleteImage")]
        public IHttpActionResult DeleteImage(string EncryptedName)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/"));
                var index = EncryptedName.IndexOf("UploadedFiles/");
                if (index > -1)
                {
                    FileInfo[] files = di.GetFiles(EncryptedName)
                         .Where(p => p.Extension == ".jpg").ToArray();
                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            var imgName = EncryptedName.Remove(0, 14);
                            if (file.Name == imgName)
                            {
                                File.Delete(file.FullName);
                            }
                        }
                        catch { }
                    }
                }
                return Ok();
            }
            catch
            {
                throw new Exception();
            }
        }

        #endregion

        #region SearchPart
        [HttpGet]
        [Route("api/Common/SearchPart")]
        public IHttpActionResult SearchPart(string PartName, int? DamagePointID, int? CountryID)
        {
            List<AutomotivePart> result = null;

            result = _commonManager.SearchPart(PartName, DamagePointID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/Common/SearchPart")]
        public IHttpActionResult SearchPart(string PartName, int? DamagePointID)
        {
            List<AutomotivePart> result = null;

            result = _commonManager.SearchPart(PartName, DamagePointID, 1);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("api/Common/SearchPart")]
        public IHttpActionResult SearchPart(string PartName)
        {
            List<AutomotivePart> result = null;

            result = _commonManager.SearchPart(PartName,null,null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #endregion


        #region SearchName
        [HttpGet]
        [Route("api/Common/SearchName")]
        public IHttpActionResult SearchName(string PartName,int DamagePointID,int? CountryID)
        {
            Object result = null;

            result = _commonManager.SearchName(PartName,DamagePointID,CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetCommonMeta
        [HttpGet]
        [Route("api/Common/GetCommonMeta")]
        public IHttpActionResult GetCommonMeta(int? userID,int RoleID)
        {
            CommonMeta result = null;
            //NotificationController.DependencyTrigger();

            result = _commonManager.GetCommonMeta(userID,RoleID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        [HttpGet]
        [Route("api/Common/GetCommonMeta")]
        public IHttpActionResult GetCommonMeta(int? userID)
        {
            CommonMeta result = null;
            NotificationController.DependencyTrigger();

            result = _commonManager.GetCommonMeta(userID, 6);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


        #region checkVINHistory
        [HttpGet]
        [Route("api/Company/checkVINHistory")]
        public IHttpActionResult checkVINHistory(string VIN)
        {
            RequestLog result = null;
            result = _commonManager.checkVINHistory(VIN);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveFiles
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveFiles")]

        public HttpResponseMessage SaveFiles([FromBody] FileToUpload theFile)
        {
            var response = new HttpResponseMessage();
            try
            {
                var gid = Guid.NewGuid();
                var filetype = Path.GetExtension(theFile.FileName);
                var filename = gid + filetype;
                var filepath = "UploadedFiles/" + filename;
                var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), filename);

                if (theFile.FileAsBase64.Contains(","))
                {
                    theFile.FileAsBase64 = theFile.FileAsBase64
                      .Substring(theFile.FileAsBase64
                      .IndexOf(",") + 1);
                }

                theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);


                using (var fs = new FileStream(fileSavePath, FileMode.CreateNew))
                {
                    fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, filepath, Configuration.Formatters.JsonFormatter);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex, Configuration.Formatters.JsonFormatter);
                throw ex;
            }
            return response;
        }
        #endregion

        #region GetCities
        [HttpGet]
        [Route("api/Common/GetCities")]
        public IHttpActionResult GetCities()
        {
            List<City> result = null;

            result = _commonManager.GetAllCities(null);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpGet]
        [Route("api/Common/GetCities")]
        public IHttpActionResult GetCities(int CountryID)
        {
            List<City> result = null;

            result = _commonManager.GetAllCities(CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region UpdatePartInfo
        [HttpPost]
        [Route("api/Common/UpdatePartInfo")]
        public IHttpActionResult UpdatPartInfo(UpdateAutomotivePart automotivePart)
        {
            Object result = null;
            result = _commonManager.UpdatePartInfo(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region accidentCostReport
        [HttpPost]
        [Route("api/Common/GetAccidentCostReport")]
        public IHttpActionResult AccidentCostReport(RequestPartObj RPobj)
        {
            Object result = null;
            result = _commonManager.GetAccidentCostReport(RPobj);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetPartPriceEstimate
        [HttpGet]
        [Route("api/Common/GetPartPriceEstimate")]
        public IHttpActionResult GetPartPriceEstimate(string SeriesID, int RequestID, int UserID,int? DemandID,int? monthfilter)
        {
            Object result = null;
            result = _commonManager.GetPartPriceEstimate(SeriesID, RequestID, UserID,DemandID,monthfilter);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region GetNewAutomotivePartDetail

        [HttpGet]
        [Route("api/Common/GetNewAutomotivePartDetail")]
        public IHttpActionResult GetNewAutomotivePartDetail(int DamagePointID, string Name1,int? AutomotivePartID)
        {
            AutomotivePart result = null;

            result = _commonManager.GetNewAutomotivePartDetail(DamagePointID,Name1, AutomotivePartID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region CreateAutomotivePart
        [HttpPost]
        [Route("api/Common/CreateAutomotivePart")]
        public IHttpActionResult CreateAutomotivePart(AutomotivePart automotivePart)
        {
            string result = null;
            result = _commonManager.CreateAutomotivePart(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region RejectAutomotivePart
        [HttpGet]
        [Route("api/Common/RejectAutomotivePart")]
        public IHttpActionResult RejectAutomotivePart(int automotivePartID,int UserID)
        {
            string result = null;
            result = _commonManager.RejectAutomotivePart(automotivePartID,UserID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion


        #region GetICStaffKPI
        [HttpGet]
        [Route("api/Common/GetICStaffKPI")]
        public IHttpActionResult GetICStaffKPI(int companyID, int UserID, string StartDate, string EndDate, int? ReceptionID, int? LossAdjusterID, int? PublishBy, int? POOrderBy, int? POApproveBy,int? PageNo)
        {
             Object result = null;
            result = _commonManager.GetICStaffKPI(companyID,UserID,StartDate,EndDate,ReceptionID,LossAdjusterID,PublishBy,POOrderBy,POApproveBy, PageNo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetMonthlyClaimsReport
        [HttpGet]
        [Route("api/Common/GetMonthlyClaimsReport")]
        public IHttpActionResult GetMonthlyClaimsReport(int CompanyID, int UserID, string StartDate, string EndDate,int? PageNo)
        {
            Object result = null;
            result = _commonManager.GetMonthlyClaimsReport(CompanyID, UserID, StartDate, EndDate,PageNo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        [HttpGet]
        [Route("api/Common/FirstCall")]
        public IHttpActionResult FirstCall()
        {
            string result = null;
            result = "JoClaims";
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        #region GetWorkshopCompany
        [HttpGet]
        [Route("api/Common/GetWorkshopCompany")]
        public IHttpActionResult GetWorkshopCompany(int UserID,int RoleID)
        {
            SignInResponse signInResponse = new SignInResponse();
            signInResponse = _commonManager.GetWorkshopCompany(UserID, RoleID);
            if (signInResponse != null)
            {
                return Ok(signInResponse);
            }
            return NotFound();
        }
        #endregion

        #region SaveRequestDraftImage
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveRequestDraftImage")]

        public HttpResponseMessage SaveRequestDraftImage(List<RequestDraftImage> images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<RequestDraftImage>();
            var savedImage = new RequestDraftImage();
            try
            {
                foreach (var img in images)
                {
                    var gid = Guid.NewGuid();
                    var filename = gid + "." + "jpg";
                    img.ImageDataUrl = img.ImageDataUrl.Remove(0, 23);
                    var filepath = "DraftFiles/" + filename;
                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DraftFiles"), filename);
                    File.WriteAllBytes(fileSavePath, Convert.FromBase64String(img.ImageDataUrl));
                    savedImage.ImageURL = filepath;
                    savedImage.OriginalName = img.OriginalName;
                    savedImage.EncryptedName = filename;
                    savedImages.Add(savedImage);
                    savedImage = new RequestDraftImage();
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

        #region SaveRequestDraftVideo
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveRequestDraftVideo")]

        public HttpResponseMessage SaveRequestDraftVideo(List<RequestDraftImage> images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<RequestDraftImage>();
            var savedImage = new RequestDraftImage();
            bool IsIphone = false;
            try
            {
                foreach (var img in images)
                {
                    var gid = Guid.NewGuid();
                    var filename = gid + "." + "mp4";
                    var filepath = "DraftFiles/" + filename;
                    IsIphone = img.ImageDataUrl.Contains("quicktime");
                    if(IsIphone == true)
                    {
                        img.ImageDataUrl = img.ImageDataUrl.Remove(0, 28);
                    }
                    else {
                        img.ImageDataUrl = img.ImageDataUrl.Remove(0, 22);
                    }
                    
                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DraftFiles"), filename);
                    File.WriteAllBytes(fileSavePath, Convert.FromBase64String(img.ImageDataUrl));
                    savedImage.ImageURL = filepath;
                    //savedImage.OriginalName = img.OriginalName;
                    savedImage.EncryptedName = filename;
                    savedImages.Add(savedImage);
                    savedImage = new RequestDraftImage();
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

        #region SaveRequestDraftVideoMobile
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveRequestDraftVideoMobile")]

        public async Task<HttpResponseMessage> SaveRequestDraftVideoMobile()
        {

            var root = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DraftFiles"));

            string filepath = null;
            string StoragePath = HttpContext.Current.Server.MapPath("~");
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(Path.Combine(StoragePath, "DraftFiles"));
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                foreach (MultipartFileData fileData in streamProvider.FileData)
                {


                    File.Move(fileData.LocalFileName, fileData.LocalFileName + ".mp4");
                    filepath = fileData.LocalFileName + ".mp4";
                }

                filepath = filepath + "";
            }
            return Request.CreateResponse(HttpStatusCode.OK, (filepath.Substring(filepath.IndexOf("DraftFiles\\"))).Replace("\\", "//"));
        }



        #endregion


        #region SaveCarReadyVideoMobile
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveCarReadyVideoMobile")]

        public async Task<HttpResponseMessage> SaveCarReadyVideoMobile()
        {

            var root = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/CarReadyVideoFiles"));

            string filepath = null;
            string StoragePath = HttpContext.Current.Server.MapPath("~");
            if (Request.Content.IsMimeMultipartContent())
            {
                var streamProvider = new MultipartFormDataStreamProvider(Path.Combine(StoragePath, "CarReadyVideoFiles"));
                await Request.Content.ReadAsMultipartAsync(streamProvider);

                foreach (MultipartFileData fileData in streamProvider.FileData)
                {


                    File.Move(fileData.LocalFileName, fileData.LocalFileName + ".mp4");
                    filepath = fileData.LocalFileName + ".mp4";
                }

                filepath = filepath + "";
            }
            return Request.CreateResponse(HttpStatusCode.OK, (filepath.Substring(filepath.IndexOf("CarReadyVideoFiles\\"))).Replace("\\", "/"));
        }



        #endregion



        #region RemoveRequestDraftVideo
        [HttpPost]
        [Route("api/Common/RemoveFile")]

        public HttpResponseMessage RemoveFile(RequestDraftImage images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<RequestDraftImage>();
            var savedImage = new RequestDraftImage();
            try
            {


                var gid = Guid.NewGuid();
                var filename = gid + "." + "mp4";
                var fileSavePath = "";
                //var filepath = "DraftFiles/" + filename;
                if (images.ImageURL.Contains("UploadedFiles"))
                {
                    fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), images.EncryptedName);
                }
                else if (images.ImageURL.Contains("DraftFiles"))
                {
                    fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DraftFiles"), images.EncryptedName);
                }
                
                var destFilePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DeletedFiles"), images.EncryptedName);
                File.Copy(fileSavePath, destFilePath);
                File.Delete(fileSavePath);
                savedImage = new RequestDraftImage();

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

        #region GetMobileAPIUrl
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Common/GetMobileAPIUrl")]
        public IHttpActionResult GetMobileAPIUrl(string MobileAppVersion)
        {
            Object result;
            result= _commonManager.GetMobileAPIUrl(MobileAppVersion);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion
        #region SearchNameInMultipleDamagePoint
        [HttpPost]
        [Route("api/Common/SearchNameInMultipleDamagePoint")]
        public IHttpActionResult SearchNameInMultipleDamagePoint(AutomotivePart automotivePart)
   {
            Object result = null;

            result = _commonManager.SearchNameInMultipleDamagePoint(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region generatefinalCodeMultiAutoMotivePart
        [HttpPost]
        [Route("api/Common/generatecode")]
        public IHttpActionResult GenerateMultiAutoMotivePartCode(AutomotivePart automotivePart)
        {
            Object result = null;

            result = _commonManager.generateMultiAutoMotivePartCode(automotivePart);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region PoRoDetailReport
        [HttpGet]
        [Route("api/Common/po-ro-detail-report")]
        public IHttpActionResult PoRoDetailReport(int CompanyID, string StartDate, string EndDate, bool IsExcel, int PageNo)
        {
            Object result = null;

            result = _commonManager.PoRoDetailReport(CompanyID,StartDate, EndDate,IsExcel,PageNo);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region GetlatestQuotationSuppliers
        [HttpGet]
        [Route("api/Common/getlatestquotationsuppliers")]
        public IHttpActionResult GetlatestQuotationSuppliers(string SeriesID, int AutomotivePartID, int CoditionTypeID, int NewPartConditionTypeID,int monthfilter,int userRoleID, int? CountryID)
        {
            Object result = null;

            result = _commonManager.GetlatestQuotationSuppliers(SeriesID, AutomotivePartID, CoditionTypeID, NewPartConditionTypeID, monthfilter, userRoleID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region PrintrequestDetails
        [HttpPost]
        [Route("api/Common/printrequest")]
        public IHttpActionResult printrequest(PdfData PdfData)
        {
            string result = null;
          
                //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
            //errorOnLine ++;
            //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
            //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");

            PdfData.elementHtml = (string)JsonConvert.DeserializeObject(PdfData.elementHtml);
            //errorOnLine++;
            HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                //errorOnLine++;

                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;

                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginTop = 15;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginBottom = 15;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginLeft = 8;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginRight = 8;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.Header = new HtmlHeaderFooter()
                {
                    Height = 0
                };
            HtmlToPdf.PrintOptions.Footer = new HtmlHeaderFooter()
            {
                Height = 10,
                HtmlFragment = "<center><i>{page} of {total-pages}<i></center>",
                DrawDividerLine = false
            };
            //errorOnLine++;
            HtmlToPdf.PrintOptions.Zoom = 90;

            //errorOnLine++;
            PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(PdfData.elementHtml);

            //errorOnLine++;
            string fileName = "request-detail-" + PdfData.RequestID + ".pdf";

                //errorOnLine++;

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/request-report"));
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

                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/request-report"), fileName));


            result = "request-report/" + fileName;
           var res =  _commonManager.printrequest(PdfData.RequestID, result);

            if (res == true && result != null && result.IndexOf("request-report") > -1)
                {
                    return Ok(result);
                }
                return BadRequest(result);
         
            
            }
        #endregion PrintrequestDetails



        #region saveEstimationPriceAndCondition
        [HttpPost]
        [Route("api/Common/saveEstimationPriceAndCondition")]
        public IHttpActionResult saveEstimationPriceAndCondition(EstimationPrice estimationPrice)
        {
            bool result = false;

            result = _commonManager.saveEstimationPriceAndCondition(estimationPrice);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region SaveRequestDraftVideo
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/SaveCarReadyVideo")]

        public HttpResponseMessage SaveCarReadyVideo(List<Image> images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<Image>();
            var savedImage = new Image();
            bool IsIphone = false;
            try
            {
                foreach (var img in images)
                {
                    var gid = Guid.NewGuid();
                    var filename = gid + "." + "mp4";
                    var filepath = "CarReadyVideoFiles/" + filename;
                    IsIphone = img.ImageDataUrl.Contains("quicktime");
                    if (IsIphone == true)
                    {
                        img.ImageDataUrl = img.ImageDataUrl.Remove(0, 28);
                    }
                    else
                    {
                        img.ImageDataUrl = img.ImageDataUrl.Remove(0, 22);
                    }

                    var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/CarReadyVideoFiles"), filename);
                    File.WriteAllBytes(fileSavePath, Convert.FromBase64String(img.ImageDataUrl));
                    savedImage.ImageURL = filepath;
                    //savedImage.OriginalName = img.OriginalName;
                    savedImage.EncryptedName = filename;
                    savedImages.Add(savedImage);
                    savedImage = new RequestDraftImage();
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

        #region RemoveCarReadyFile
        [HttpPost]
        [Route("api/Common/RemoveCarReadyFile")]

        public HttpResponseMessage RemoveCarReadyFile(Image images)
        {
            var response = new HttpResponseMessage();
            var savedImages = new List<Image>();
            var savedImage = new Image();
            try
            {


                var gid = Guid.NewGuid();
                var filename = gid + "." + "mp4";
                var fileSavePath = "";
                //var filepath = "DraftFiles/" + filename;
                if (images.ImageURL.Contains("UploadedFiles"))
                {
                    fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/UploadedFiles"), images.EncryptedName);
                }
                else if (images.ImageURL.Contains("CarReadyVideoFiles"))
                {
                    fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/CarReadyVideoFiles"), images.EncryptedName);
                }

                var destFilePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/DeletedFiles"), images.EncryptedName);
                File.Copy(fileSavePath, destFilePath);
                File.Delete(fileSavePath);
                savedImage = new RequestDraftImage();

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

        #region updateTokenAndCaseID
        [HttpGet]
        [Route("api/Common/updateTokenAndCaseID")]
        public IHttpActionResult updateTokenAndCaseID(string CaseId, string Token, int draftID, int UserID)
        {
            bool result = false;

            result = _commonManager.updateTokenAndCaseID(CaseId, Token, draftID , UserID);
            if (result != false)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

       


        #region getDraftRequestreport
        [HttpGet]
        [Route("api/Common/getDraftRequestreport")]
        public IHttpActionResult getDraftRequestreport(int? WorkShopID, int? PageNo, string StartDate, string EndDate,int? CompanyID, int? CountryID )
        {
            Object result = null;

            result = _commonManager.getDraftRequestreport(WorkShopID, PageNo, StartDate, EndDate, CompanyID, CountryID);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        #endregion

        #region saveAIJsonFile
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Common/saveAIJsonFile")]

        public async Task<IHttpActionResult> saveAIJsonFile([FromBody] FileToUpload theFile)
        {
            var response = new HttpResponseMessage();
            try
            {
                var gid = Guid.NewGuid();
                var filetype = Path.GetExtension(theFile.FileName);
                var filename = theFile.FileName + '-' + theFile.caseID + filetype;
                var filepath = "AI-JsonFile/" + filename;
                
                var fileSavePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("/AI-JsonFile"), filename);

                if (theFile.FileAsBase64.Contains(","))
                {
                    theFile.FileAsBase64 = theFile.FileAsBase64
                      .Substring(theFile.FileAsBase64
                      .IndexOf(",") + 1);
                }

                theFile.FileAsByteArray = Convert.FromBase64String(theFile.FileAsBase64);

                string fileContent = Encoding.UTF8.GetString(theFile.FileAsByteArray);

                AIjsonresponse aijsonresponse = new AIjsonresponse();
                aijsonresponse.ApiKey = "EIypPrLFhhk3dv23DGhjUeqc3jFDdGtNTRYE";
                aijsonresponse.CaseDamageReport = JsonConvert.DeserializeObject(fileContent);

                using (var fs = new FileStream(fileSavePath, FileMode.CreateNew))
                {
                    fs.Write(theFile.FileAsByteArray, 0, theFile.FileAsByteArray.Length);
                }

                response = Request.CreateResponse(HttpStatusCode.OK, filepath, Configuration.Formatters.JsonFormatter);
                Object result = await this.SendAIjsonresponsetoAutoscore(aijsonresponse);
                return Ok(result);
            }
            catch (Exception ex)
            {
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, ex, Configuration.Formatters.JsonFormatter);
                throw ex;
            }
            
        }

        public async Task<Object> SendAIjsonresponsetoAutoscore(AIjsonresponse aijsonresponse)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["AutoScore"]);
                try
                {
                    var content = new StringContent(aijsonresponse.ToString(), Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsJsonAsync("ai/UploadInspektlabCaseDamageReport", aijsonresponse);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        var result = await Task.Run(() => JsonConvert.DeserializeObject<Object>(responseContent));
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






    }
}
