using BAL.Common;
using BAL.IManager;
using DAL;
using MODEL.Models;
using MODEL.Models.Report.Common;
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
using System.ComponentModel.Design;
using System.Runtime.InteropServices.ComTypes;
using System.Web.UI;

namespace BAL.Manager
{
    public class ShubeddakManager : IShubeddakManager
    {

        #region GetInsuranceCompanies
        public Object GetInsuranceCompanies(int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Companies = new List<Company>();
                var FeaturePermission = new List<FeaturePermission>();
                var sParameter = new List<SqlParameter>
                {

                    new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                };
                using (dt = ADOManager.Instance.DataSet("[getInsuranceCompanies]", CommandType.StoredProcedure,sParameter))
                {
                    Companies = dt.Tables[0].AsEnumerable().Select(cmp => new Company
                    {
                        CompanyID = cmp.Field<int>("CompanyID"),
                        CompanyName = cmp.Field<string>("Name"),
                        PhoneNumber = cmp.Field<string>("PhoneNumber"),
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
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        UserID = cmp.Field<int>("UserID"),
                        Email = cmp.Field<string>("Email"),
                        RoleID = cmp.Field<byte>("RoleID"),
                        RoleName = cmp.Field<string>("RoleName"),
                        UserName = cmp.Field<string>("UserName"),
                        EmailConfirmed = cmp.Field<bool>("EmailConfirmed"),
                        CityName = cmp.Field<string>("CityName"),
                        CountryName = cmp.Field<string>("CountryName"),
                        TypeName = cmp.Field<string>("TypeName"),
                        StatusName = cmp.Field<string>("StatusName"),
                        AccidentLimit = cmp.Field<int?>("AccidentLimit"),
                        Id = cmp.Field<string>("Id"),
                        isPriceEstimate = cmp.Field<bool>("isPriceEstimate"),
                        CountryNameArabic = cmp.Field<string>("CountryNameArabic")

                    }).ToList();
                    FeaturePermission = dt.Tables[1].AsEnumerable().Select(fp => new FeaturePermission
                    {
                        FeaturePermissionsID = fp.Field<int?>("FeaturePermissionsID"),
                        FeatureID = fp.Field<int?>("featureID"),
                        CompanyID = fp.Field<int>("CompanyID"),
                        IsApproved = fp.Field<bool>("IsApproved")
                    }).ToList();

                }
                Object CompaniesFeatures = new { Companies = Companies, FeaturePermission = FeaturePermission };
                return CompaniesFeatures;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAllRequests
        public Companyrequests GetAllRequests(DateTime? StartDate, DateTime? EndDate, int? MakeID,
            int? ModelID, int? YearID, string SearchQuery, int PageNo, int StatusID, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate, int? ICWorkshopID,int? CompanyTypeID, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var allrequests = new Companyrequests();
                var sParameter = new List<SqlParameter>
                {
                    
                    new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                    new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                    new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                    new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                    new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                    new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery},
                    new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                    new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                    new SqlParameter { ParameterName = "@ApprovalStartDate" , Value = ApprovalStartDate},
                    new SqlParameter { ParameterName = "@ApprovalEndDate" , Value = ApprovalEndDate},
                    new SqlParameter { ParameterName = "@ICWorkshopID" , Value = ICWorkshopID},
                    new SqlParameter { ParameterName = "@CompanyTypeID" , Value = CompanyTypeID},
                    new SqlParameter { ParameterName = "@CountryID" , Value = CountryID},
                };

                using (dt = ADOManager.Instance.DataSet("[getAllRequests]", CommandType.StoredProcedure, sParameter))
                {
                    allrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        DemandID = req.Field<int?>("DemandID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        ImgURL = req.Field<string>("ImgURL"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("Name"),
                        CPName = req.Field<string>("CPName"),
                        StatusID = req.Field<Int16>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        PlateNo = req.Field<string>("PlateNo"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        RequestRowNumber = req.Field<int?>("RequestRowNumber"),
                        TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
                        ClearanceRoute = req.Field<string>("ClearanceRoute"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        POPdfUrl = req.Field<string>("POPdfUrl"),
                        SupplierName = req.Field<string>("SupplierName"),
                        SupplierPhone = req.Field<string>("SupplierPhone"),
                        ReturnReason = req.Field<string>("ReturnReason"),
                        ROPdfURL = req.Field<string>("ROPdfURL"),
                        JCSeriesID = req.Field<string>("JCSeriesCode"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        NotPurchasingLMOReason = req.Field<string>("NotPurchasingLMOReason"),
                        IsInstantPrice = req.Field<bool?>("IsInstantPrice")

                    }).ToList();

                    allrequests.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        //RequestedPartID = rp.Field<int>("RequestedPartID"),
                        //AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        //PartImgURL = rp.Field<string>("PartImgURL"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        isSpliced = rp.Field<int>("isSpliced"),
                        //IsPartApproved = rp.Field<bool?>("IsPartApproved"),



                    }).ToList();

                    //allrequests.Makes = dt.Tables[2].AsEnumerable().Select(make => new Make
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

                    //allrequests.Models = dt.Tables[3].AsEnumerable().Select(model => new Model
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

                    //allrequests.Years = dt.Tables[4].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    //allrequests.Accidents = dt.Tables[5].AsEnumerable().Select(acd => new Accident
                    //{
                    //    AccidentID = acd.Field<int>("AccidentID"),
                    //    AccidentNo = acd.Field<string>("AccidentNo"),
                    //    CompanyID = acd.Field<int>("CompanyID"),
                    //    MakeID = acd.Field<int>("MakeID"),
                    //    ModelID = acd.Field<int>("ModelID"),
                    //    PlateNo = acd.Field<string>("PlateNo"),
                    //    ProductionYear = acd.Field<Int16>("ProductionYear"),
                    //    VIN = acd.Field<string>("VIN"),

                    //}).ToList();

                    //allrequests.RequestedPartsImages = dt.Tables[5].AsEnumerable().Select(img => new Image
                    //{
                    //    ImageID = img.Field<int>("ImageID"),
                    //    EncryptedName = img.Field<string>("EncryptedName"),
                    //    OriginalName = img.Field<string>("OriginalName"),
                    //    ImageURL = img.Field<string>("ImageURL"),
                    //    ObjectID = img.Field<int>("ObjectID"),
                    //    RequestID = img.Field<int>("RequestID"),

                    //}).ToList();

                    allrequests.Companies = dt.Tables[2].AsEnumerable().Select(cmp => new Company
                    {
                        Name = cmp.Field<String>("Name"),
                        CompanyID = cmp.Field<int>("CompanyID"),

                    }).ToList();
                    allrequests.TabInfoData = dt.Tables[3].AsEnumerable().Select(tbi => new TabInfo
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
                        DemandedRequests = tbi.Field<int>("DemandedRequests"),
                        EstimationPriceRequests = tbi.Field<int>("EstimationPriceRequests")

                    }).FirstOrDefault();
                    allrequests.PurchaseOrders = dt.Tables[4].AsEnumerable().Select(tbi => new PurchaseOrder
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

                    allrequests.ObjectType = dt.Tables[5].AsEnumerable().Select(tbi => new ObjectType
                    {
                        ObjectTypeID = tbi.Field<Int16>("ObjectTypeID"),
                        ObjectName = tbi.Field<string>("ObjectName"),
                        TypeName = tbi.Field<string>("TypeName"),
                        ArabicTypeName = tbi.Field<string>("ArabicTypeName"),
                        Icon = tbi.Field<string>("Icon")
                    }).ToList();
                }
                return allrequests;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region GetRequestsHistory
        public Companyrequests GetRequestsHistory()
        {
            DataSet dt = new DataSet();
            try
            {
                var companyrequests = new Companyrequests();

                using (dt = ADOManager.Instance.DataSet("[getRequestsHistory]", CommandType.StoredProcedure))
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

                    }).ToList();

                    companyrequests.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),

                    }).ToList();

                    //companyrequests.Makes = dt.Tables[2].AsEnumerable().Select(make => new Make
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

                    //companyrequests.Models = dt.Tables[3].AsEnumerable().Select(model => new Model
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

                    //companyrequests.Years = dt.Tables[4].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    companyrequests.Accidents = dt.Tables[2].AsEnumerable().Select(acd => new Accident
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

                    }).ToList();

                    companyrequests.RequestedPartsImages = dt.Tables[3].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        RequestID = img.Field<int>("RequestID"),

                    }).ToList();

                    companyrequests.POApprovalEmployees = dt.Tables[4].AsEnumerable().Select(po => new POApproval
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

                return companyrequests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region UpdateCompanyStatus
        public string UpdateCompanyStatus(int CompanyID, Int16 StatusID, string CompanyCode, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@CompanyCode" , Value = CompanyCode},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateCompanyStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetPartShopes
        public List<Supplier> GetPartShopes(PSFilter psFilter)
        {
            DataSet dt = new DataSet();
            try
            {
                var suppliers = new List<Supplier>();
                var sParameter = new List<SqlParameter>();
                if (psFilter != null)
                {
                    sParameter = new List<SqlParameter>
                    {
                        //new SqlParameter { ParameterName = "@CPEmail" , Value = psFilter.CPEmail},
                        //new SqlParameter { ParameterName = "@CPName" , Value = psFilter.CPName},
                        new SqlParameter { ParameterName = "@MakeID" , Value = psFilter.MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = psFilter.ModelID},
                        //new SqlParameter { ParameterName = "@ShopName" , Value = psFilter.ShopName},
                        new SqlParameter { ParameterName = "@YearID" , Value = psFilter.YearID},
                        new SqlParameter { ParameterName = "@SortBy" , Value = psFilter.SortBy},
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = psFilter.SearchQuery},
                        new SqlParameter { ParameterName = "@WorkingFrom" , Value = psFilter.WorkingFrom},
                        new SqlParameter { ParameterName = "@WorkingTo" , Value = psFilter.WorkingTo},
                        new SqlParameter { ParameterName = "@CountryID" , Value = psFilter.CountryID},
                    };
                }

                using (dt = ADOManager.Instance.DataSet("[getPartShopes]", CommandType.StoredProcedure, sParameter))
                {
                    suppliers = dt.Tables[0].AsEnumerable().Select(cmp => new Supplier
                    {
                        SupplierID = cmp.Field<int>("SupplierID"),
                        SupplierName = cmp.Field<string>("SupplierName"),
                        PhoneNumber = cmp.Field<string>("PhoneNumber"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        CityID = cmp.Field<Int16?>("CityID"),
                        CountryID = cmp.Field<Int16?>("CountryID"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        GoogleMapLink = cmp.Field<string>("GoogleMapLink"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPositionID = cmp.Field<Int16?>("CPPositionID"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        UserID = cmp.Field<int>("UserID"),
                        Email = cmp.Field<string>("Email"),
                        RoleID = cmp.Field<byte>("RoleID"),
                        RoleName = cmp.Field<string>("RoleName"),
                        UserName = cmp.Field<string>("UserName"),
                        EmailConfirmed = cmp.Field<bool>("EmailConfirmed"),
                        CityName = cmp.Field<string>("CityName"),
                        CountryName = cmp.Field<string>("CountryName"),
                        TypeName = cmp.Field<string>("TypeName"),
                        StatusName = cmp.Field<string>("StatusName"),
                        IsSellNew = cmp.Field<bool?>("IsSellNew"),
                        IsSellUsed = cmp.Field<bool?>("IsSellUsed"),
                        RejectNote = cmp.Field<string>("RejectNote"),
                        OffersCount = cmp.Field<int>("OffersCount"),
                        TotalDemands = cmp.Field<int>("TotalDemands"),
                        ApplyRatio = cmp.Field<int?>("ApplyRatio"),
                        PaymentTypeID = cmp.Field<Int16?>("PaymentTypeID"),
                        PaymentTypeName = cmp.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = cmp.Field<string>("PaymentTypeArabicName"),
                        AppVersion = cmp.Field<string>("AppVersion"),
                        IsDeleted = cmp.Field<bool>("IsDeleted"),
                        Id = cmp.Field<string>("UserEncryptedID")

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

        #region UpdatePartshopStatus
        public string UpdatePartshopStatus(int SupplierID, Int16 StatusID, string RejectNote, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@RejectNote" , Value = RejectNote},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updatePartshopStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveDemand
        public string SaveDemand(RequestData request)
        {
            try
            {
                var XMLRequestedParts = request.RequestedParts.ToXML("ArrayOfRequestedParts");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@RequestID" , Value = request.Request.RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = request.Request.ModifiedBy}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateDemand
        public string UpdateDemand(RequestData request)
        {
            try
            {
                var XMLRequestedParts = request.RequestedParts.ToXML("ArrayOfRequestedParts");
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = XMLRequestedParts},
                        new SqlParameter { ParameterName = "@RequestID" , Value = request.Request.RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = request.Request.ModifiedBy},
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateDemand]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Demand updated successfully" : DataValidation.dbError;

                var result = ADOManager.Instance.ExecuteScalar("[updateDemand]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        //#region GetAllDemands
        //public Companyrequests GetAllDemands()
        //{
        //    DataSet dt = new DataSet();
        //    try
        //    {
        //        var allDemands = new Companyrequests();

        //        using (dt = ADOManager.Instance.DataSet("[getAllDemands]", CommandType.StoredProcedure))
        //        {
        //            allDemands.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
        //            {
        //                RequestID = req.Field<int>("RequestID"),
        //                DemandID = req.Field<int?>("DemandID"),
        //                MakeID = req.Field<int>("MakeID"),
        //                ModelID = req.Field<int>("ModelID"),
        //                ProductionYear = req.Field<Int16>("ProductionYear"),                        
        //                CompanyID = req.Field<int>("CompanyID"),
        //                VIN = req.Field<string>("VIN"),
        //                CreatedOn = req.Field<DateTime>("CreatedOn"),
        //                MakeName = req.Field<string>("EnglishMakeName"),
        //                ArabicMakeName = req.Field<string>("ArabicMakeName"),
        //                ModelCode = req.Field<string>("EnglishModelName"),
        //                ArabicModelName = req.Field<string>("ArabicModelName"),
        //                YearCode = req.Field<int>("YearCode"),
        //                CreatedSince = req.Field<string>("CreatedSince"),
        //                AccidentID = req.Field<int?>("AccidentID"),
        //                AccidentNo = req.Field<string>("AccidentNo"),
        //                WorkshopName = req.Field<string>("WorkshopName"),
        //                WorkshopCityName = req.Field<string>("WorkshopCityName"),
        //                WorkshopPhone = req.Field<string>("WorkshopPhone"),
        //                WorkshopAreaName = req.Field<string>("WorkshopAreaName"),

        //                CPPhone = req.Field<string>("CPPhone"),
        //                ICName = req.Field<string>("Name"),
        //                CPName = req.Field<string>("CPName"),
        //                StatusName = req.Field<string>("StatusName"),

        //            }).ToList();

        //            allDemands.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
        //            {
        //                RequestedPartID = rp.Field<int>("RequestedPartID"),
        //                RequestID = rp.Field<int>("RequestID"),
        //                ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
        //                Quantity = rp.Field<int?>("Quantity"),
        //                DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
        //                DesiredManufacturerID = rp.Field<Int16?>("DesiredManufacturerID"),
        //                AutomotivePartID = rp.Field<int>("AutomotivePartID"),
        //                DemandID = rp.Field<int>("DemandID"),
        //                DesiredPrice = rp.Field<double?>("DesiredPrice"),
        //                NoteInfo = rp.Field<string>("NoteInfo"),
        //                AutomotivePartName = rp.Field<string>("AutomotivePartName"),
        //                ConditionTypeName = rp.Field<string>("ConditionTypeName"),
        //                DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),

        //            }).ToList();

        //            allDemands.Makes = dt.Tables[2].AsEnumerable().Select(make => new Make
        //            {
        //                //LanguageMakeID = make.Field<Int16>("LanguageMakeID"),
        //                MakeCode = make.Field<string>("MakeCode"),
        //                ImgURL = make.Field<string>("ImgURL"),
        //                //LanguageID = make.Field<Byte>("LanguageID"),
        //                MakeID = make.Field<int>("MakeID"),
        //                ModifiedBy = make.Field<int?>("ModifiedBy"),
        //                CreatedOn = make.Field<DateTime?>("CreatedOn"),
        //                CreatedBy = make.Field<int?>("CreatedBy"),
        //                ModifiedOn = make.Field<DateTime?>("ModifiedOn"),
        //                MakeName = make.Field<string>("EnglishMakeName"),
        //                ArabicMakeName = make.Field<string>("ArabicMakeName"),
                        

        //            }).ToList();

        //            allDemands.Models = dt.Tables[3].AsEnumerable().Select(model => new Model
        //            {
        //                //LanguageModelID = model.Field<Int32>("LanguageModelID"),
        //                MakeID = model.Field<int>("MakeID"),
        //                //YearCode = model.Field<Int16>("YearCode"),
        //                //LanguageID = model.Field<byte>("LanguageID"),
        //                ModelID = model.Field<int>("ModelID"),
        //                ModelCode = model.Field<string>("EnglishModelName"),
        //                ArabicModelName = model.Field<string>("ArabicModelName"),
                        
        //            }).ToList();

        //            allDemands.Years = dt.Tables[4].AsEnumerable().Select(year => new Year
        //            {
        //                LanguageYearID = year.Field<Int16>("LanguageYearID"),
        //                YearCode = year.Field<int>("YearCode"),
        //                LanguageID = year.Field<byte>("LanguageID"),
        //                YearID = year.Field<Int16>("YearID"),

        //            }).ToList();

        //            allDemands.RequestedPartsImages = dt.Tables[5].AsEnumerable().Select(img => new Image
        //            {
        //                ImageID = img.Field<int>("ImageID"),
        //                EncryptedName = img.Field<string>("EncryptedName"),
        //                OriginalName = img.Field<string>("OriginalName"),
        //                ImageURL = img.Field<string>("ImageURL"),
        //                ObjectID = img.Field<int>("ObjectID"),
        //                RequestID = img.Field<int>("RequestID"),

        //            }).ToList();

        //            allDemands.Companies = dt.Tables[6].AsEnumerable().Select(cmp => new Company
        //            {
        //                CompanyID = cmp.Field<int>("CompanyID"),
        //                CompanyName = cmp.Field<string>("Name"),
        //                LogoURL = cmp.Field<string>("LogoURL"),

        //            }).ToList();

        //        }
        //        return allDemands;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        //#endregion

        #region GetDemand
        public DemandProfile GetDemand(int DemandID)
        {
            DataSet dt = new DataSet();
            try
            {
                DemandProfile demandProfile = new DemandProfile();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getDemand]", CommandType.StoredProcedure, sParameter))
                {
                    demandProfile.demand = dt.Tables[0].AsEnumerable().Select(req => new Request
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
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("ICName"),
                        CPName = req.Field<string>("CPName"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                    }).FirstOrDefault();
                  
                    demandProfile.DemandedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        RequestID = rp.Field<int>("RequestID"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        Quantity = rp.Field<int?>("Quantity"),
                        DemandedQuantity = rp.Field<int?>("DemandedQuantity"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        DemandID = rp.Field<int>("DemandID"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DamageName = rp.Field<string>("DamageName"),
                        isExistInAccident = rp.Field<bool?>("isExistInAccident")
                        //DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        //DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),
                    }).ToList();
                   
                    demandProfile.DemandedPartsImages = dt.Tables[2].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID")
                    }).ToList();
                }
                return demandProfile;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetQuotations
        public DemandQuotations GetQuotations(int DemandID)
        {
            DataSet dt = new DataSet();
            try
            {
                DemandQuotations DemandQuotationsData = new DemandQuotations();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getQuotations]", CommandType.StoredProcedure, sParameter))
                {
                    DemandQuotationsData.RequestInfo = dt.Tables[0].AsEnumerable().Select(req => new Request
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
                        StatusID = req.Field<Int16?>("StatusID"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        CPName = req.Field<string>("CPName"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("CompanyName"),
                        CountryName = req.Field<string>("CountryName"),
                        CityName = req.Field<string>("CityName"),
                        AddressLine1 = req.Field<string>("AddressLine1"),
                        AddressLine2 = req.Field<string>("AddressLine2"),
                        BiddingHours = req.Field<Int16?>("BiddingHours")

                    }).FirstOrDefault();

                    DemandQuotationsData.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ConditionTypeID = rp.Field<Int16>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int>("Quantity"),
                        DesiredManufacturerID = rp.Field<Int16>("DesiredManufacturerID"),
                        DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        DemandedQuantity = rp.Field<int>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        DesiredManufacturerRegionID = rp.Field<Int16>("DesiredManufacturerRegionID"),
                        DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),
                        NoteInfo = rp.Field<string>("NoteInfo")

                    }).ToList();

                    DemandQuotationsData.Quotations = dt.Tables[2].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        WillDeliver = qt.Field<bool>("WillDeliver"),
                        DeliveryCost = qt.Field<decimal?>("DeliveryCost"),
                        StatusID = qt.Field<Int16?>("StatusID"),
                        StatusName = qt.Field<string>("StatusName"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        CreatedSince = qt.Field<string>("CreatedSince")

                    }).ToList();

                    DemandQuotationsData.QuotationParts = dt.Tables[3].AsEnumerable().Select(rp => new QuotationPart
                    {
                        QuotationPartID = rp.Field<int>("QuotationPartID"),
                        QuotationID = rp.Field<int>("QuotationID"),
                        RequestedPartID = rp.Field<int>("RequestedPartID"),
                        AutomotivePartID = rp.Field<int>("AutomotivePartID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        ManufacturerID = rp.Field<Int16?>("ManufacturerID"),
                        ManufacturerName = rp.Field<string>("ManufacturerName"),
                        Price = rp.Field<double?>("Price"),
                        Quantity = rp.Field<int>("Quantity"),
                        ConditionTypeID = rp.Field<Int16?>("ConditionTypeID"),
                        ConditionTypeName = rp.Field<string>("ConditionTypeName"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        CityName = rp.Field<string>("CityName"),
                        ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID"),
                        ManufacturerRegionName = rp.Field<string>("ManufacturerRegionName"),
                        NoteInfo = rp.Field<string>("NoteInfo"),

                    }).ToList();

                    DemandQuotationsData.QuotationPartsImages = dt.Tables[4].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID")

                    }).ToList();
                }
                return DemandQuotationsData;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetQuotationsByFilter
        public DemandQuotations GetQuotationsByFilter(QuotationFilterModel model)
        {
            DataSet dt = new DataSet();
            try
            {
                DemandQuotations QuotationsFilter = new DemandQuotations();

                var XMLRequestedParts = model.RequestedParts !=null && model.RequestedParts.Count() > 0 ? model.RequestedParts.ToXML("ArrayOfRequestedParts") : null;
                //var XMLManufacturers = model.PartManufacturers != null && model.PartManufacturers.Count() > 0 ? model.PartManufacturers.ToXML("ArrayOfManufacturers") : null;
                //var XMLManufacturerRegions = model.ManufacturerRegions != null && model.ManufacturerRegions.Count() > 0 ? model.ManufacturerRegions.ToXML("ArrayOfCountries") : null;
                var XMLCities = model.Cities != null && model.Cities.Count() > 0 ? model.Cities.ToXML("ArrayOfCities") : null;

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLRequestedParts" , Value = model.RequestedParts != null && model.RequestedParts.Count > 0 ? XMLRequestedParts : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturers" , Value = model.PartManufacturers!= null && model.PartManufacturers.Count > 0 ? XMLManufacturers : null},
                        new SqlParameter { ParameterName = "@XMLCities" , Value = model.Cities!=null && model.Cities.Count > 0 ? XMLCities : null},
                        //new SqlParameter { ParameterName = "@XMLManufacturerRegions" , Value =  model.ManufacturerRegions.Count > 0 ? XMLManufacturerRegions : null},
                        new SqlParameter { ParameterName = "@DemandID" , Value = model.DemandID },
                        new SqlParameter { ParameterName = "@AreaName" , Value = model.AreaName },
                        new SqlParameter { ParameterName = "@IsReferred" , Value = model.IsReferred },
                        new SqlParameter { ParameterName = "@IsPaid" , Value = model.IsPaid },
                        new SqlParameter { ParameterName = "@Availibility", Value = model.Availability},
                        new SqlParameter { ParameterName = "@ConditionTypeID" , Value = model.ConditionTypeID },
                        new SqlParameter { ParameterName = "@NewConditionTypeID" , Value = model.NewConditionTypeID },
                        new SqlParameter { ParameterName = "@MinPrice" , Value = model.MinPrice },
                        new SqlParameter { ParameterName = "@MaxPrice" , Value = model.MaxPrice },
                        new SqlParameter { ParameterName = "@SortByPrice" , Value = model.SortByPrice },
                        new SqlParameter { ParameterName = "@SortByFillingRate" , Value = model.SortByFillingRate },
                        new SqlParameter { ParameterName = "@SortByRating" , Value = model.SortByRating },
                        new SqlParameter { ParameterName = "@Price" , Value = model.Price },
                };

                using (dt = ADOManager.Instance.DataSet("[getQuotationsByFilter]", CommandType.StoredProcedure, sParameter))
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
                        DemandCreatedSince = req.Field<string>("DemandCreatedSince"),
                        DemandCreatedSinceArabic = req.Field<string>("DemandCreatedSinceArabic"),
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
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        DesiredPrice = rp.Field<double?>("DesiredPrice"),
                        Quantity = rp.Field<int>("Quantity"),
                        //DesiredManufacturerID = rp.Field<Int16>("DesiredManufacturerID"),
                        //DesiredManufacturerName = rp.Field<string>("DesiredManufacturerName"),
                        DemandedQuantity = rp.Field<int>("DemandedQuantity"),
                        PartImgURL = rp.Field<string>("PartImgURL"),
                        //DesiredManufacturerRegionID = rp.Field<Int16>("DesiredManufacturerRegionID"),
                        //DesiredManufacturerRegionName = rp.Field<string>("DesiredManufacturerRegionName"),
                        NoteInfo = rp.Field<string>("NoteInfo"),
                        IsPartApproved = rp.Field<bool?>("IsPartApproved")
                        

                    }).ToList();

                    QuotationsFilter.Quotations = dt.Tables[2].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        DemandID = qt.Field<int>("DemandID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        WillDeliver = qt.Field<bool>("WillDeliver"),
                        DeliveryCost = qt.Field<decimal?>("DeliveryCost"),
                        StatusID = qt.Field<Int16?>("StatusID"),
                        StatusName = qt.Field<string>("StatusName"),
                        ArabicStatusName = qt.Field<string>("ArabicStatusName"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        CreatedSince = qt.Field<string>("CreatedSince"),
                        CreatedSinceArabic = qt.Field<string>("CreatedSinceArabic"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        IsNotAvailable = qt.Field<bool?>("IsNotAvailable"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        LowestOfferMatchingPrice = qt.Field<double?>("LowestOfferMatchingPrice"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
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
                        RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                        RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                        RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                        RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                        NewPartConditionTypeID = rp.Field<Int16?>("NewPartConditionTypeID"),
                        NewPartConditionTypeName = rp.Field<string>("NewPartConditionTypeName"),
                        NewPartConditionTypeArabicName = rp.Field<string>("NewPartConditionTypeArabicName"),
                        IsReferred = rp.Field<bool?>("IsReferred"),
                        IsOrdered = rp.Field<bool?>("IsOrdered"),
                        IsAccepted = rp.Field<bool?>("IsAccepted"),
                        IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                        IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                        DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                        ReferredPrice = rp.Field<double?>("ReferredPrice"),
                        SupplierName = rp.Field<string>("SupplierName"),
                        CityName = rp.Field<string>("CityName"),
                        WillDeliver = rp.Field<bool>("WillDeliver"),
                        DeliveryCost = rp.Field<decimal?>("DeliveryCost"),
                        BranchAreaName = rp.Field<string>("BranchAreaName"),
                        BranchCityName = rp.Field<string>("CityName"),
                        BranchName = rp.Field<string>("BranchName"),
                        //ManufacturerRegionID = rp.Field<Int16>("ManufacturerRegionID"),
                        //ManufacturerRegionName = rp.Field<string>("ManufacturerRegionName"),
                        Rating = rp.Field<byte?>("Rating"),
                        OrderedOn = rp.Field<DateTime?>("OrderedOn"),
                        CreatedOn = rp.Field<DateTime>("CreatedOn"),
                        OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                        FillingRate = rp.Field<decimal?>("FillingRate"),
                        RequestPartCount = rp.Field<int?>("RequestPartCount"),
                        SupplierID = rp.Field<int>("SupplierID"),
                        ItemRank = rp.Field<int>("ItemRank"),
                        PreviousPrice = rp.Field<double?>("PreviousPrice")
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

                    QuotationsFilter.SupplierQuotations = dt.Tables[6].AsEnumerable().Select(qt => new Quotation
                    {
                        QuotationID = qt.Field<int>("QuotationID"),
                        SupplierID = qt.Field<int>("SupplierID"),
                        SupplierName = qt.Field<string>("SupplierName"),
                        RequestID = qt.Field<int>("RequestID"),
                        CPEmail = qt.Field<string>("CPEmail"),
                        CPPhone = qt.Field<string>("CPPhone"),
                        Rating = qt.Field<byte>("Rating"),
                        PendingQuotationParts = qt.Field<int>("PendingQuotationParts"),
                        JoReviewNote = qt.Field<string>("JoReviewNote"),
                        JoReviewStatusID = qt.Field<Int16?>("JoReviewStatusID"),
                        RequestPartCount = qt.Field<int?>("RequestPartCount"),
                        ProcessedQuotationParts = qt.Field<int?>("ProcessedQuotationParts"),
                        QuotedPartCount = qt.Field<int?>("QuotedPartCount"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        IsDiscountAvailable = qt.Field<bool?>("IsDiscountAvailable"),
                        DiscountValue = qt.Field<double?>("DiscountValue"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        IsPrioritySupplier = qt.Field<bool?>("IsPrioritySupplier")
                    }).ToList();

                    QuotationsFilter.TotalPendingParts = Convert.ToInt32(dt.Tables[7].Rows[0]["TotalPendingParts"]);

                    QuotationsFilter.ReferredSupplierQuotations = dt.Tables[8].AsEnumerable().Select(qt => new Quotation
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
                        QuotedPartCount = qt.Field<int?>("QuotedPartCount"),
                        PendingQuotationParts = qt.Field<int>("PendingQuotationParts"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        IsDiscountAvailable = qt.Field<bool?>("IsDiscountAvailable"),
                        DiscountValue = qt.Field<double?>("DiscountValue"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                        StatusID = qt.Field<short?>("StatusID"),
                        FillingRate = qt.Field<decimal?>("FillingRate"),
                        Comment = qt.Field<string>("Comment"),
                        IsPartialSellings = qt.Field<bool?>("IsPartialSellings"),
                        MatchingFillingRate = qt.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = qt.Field<double?>("LowestOfferMatchingPrice"),
                        IsNotAvailable = qt.Field<bool?>("IsNotAvailable"),
                        OfferSortNo = qt.Field<int>("OfferSortNo"),
                        MatchingOfferSortNo = qt.Field<int>("MatchingOfferSortNo"),
                        IsPrioritySupplier = qt.Field<bool?>("IsPrioritySupplier")


                    }).ToList();

                    QuotationsFilter.RequestedPartsImages = dt.Tables[9].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),

                    }).ToList();

                    QuotationsFilter.AccidentMarkers = dt.Tables[10].AsEnumerable().Select(am => new AccidentMarker
                    {
                        DamagePointID = am.Field<int>("DamagePointID"),
                        AccidentMarkerID = am.Field<int>("AccidentMarkerID"),
                        PointName = am.Field<string>("PointName"),
                        PointNameArabic = am.Field<string>("PointNameArabic"),
                        IsDamage = am.Field<bool>("IsDamage"),

                    }).ToList();

                    QuotationsFilter.RequestTasks = dt.Tables[11].AsEnumerable().Select(rt => new RequestTask
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

                    QuotationsFilter.Notes = dt.Tables[12].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue"),
                        IsPublic = nt.Field<bool?>("IsPublic"),

                    }).ToList();

                    QuotationsFilter.AccidentImages = dt.Tables[13].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
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

        #region SaveReferredQuotationParts
        public List<Quotation> SaveReferredQuotationParts(int UserID, List<QuotationPart> referredQuotationParts, int tabId)
        {
            try
            {
                DataSet dt = new DataSet();
                var referredSupplierQuotations = new List<Quotation>();
                string XMLReferredQuotationParts = referredQuotationParts.ToXML("ArrayOfReferredQuotationParts");
                List<SqlParameter> sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLReferredQuotationParts" , Value = XMLReferredQuotationParts},                        
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = UserID},
                        new SqlParameter { ParameterName = "@tabId" , Value = tabId}
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveReferredQuotationParts]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "Parts referred successfully" : DataValidation.dbError;

                //var result = ADOManager.Instance.ExecuteScalar("[saveReferredQuotationParts]", CommandType.StoredProcedure, sParameter);
                //return result.ToString();

                using (dt = ADOManager.Instance.DataSet("[saveReferredQuotationParts]", CommandType.StoredProcedure, sParameter))
                {
                    referredSupplierQuotations = dt.Tables[0].AsEnumerable().Select(qt => new Quotation
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
                        ReferredPartsCount = qt.Field<int?>("ReferredPartsCount"),
                        BranchAreaName = qt.Field<string>("BranchAreaName"),
                        BranchName = qt.Field<string>("BranchName"),
                        QuotedPartCount = qt.Field<int?>("QuotedPartCount"),
                        AddressLine1 = qt.Field<string>("AddressLine1"),
                        AddressLine2 = qt.Field<string>("AddressLine2"),
                        BestOfferPrice = qt.Field<double?>("BestOfferPrice"),
                        CreatedOn = qt.Field<DateTime>("CreatedOn"),
                        PaymentTypeName = qt.Field<string>("PaymentTypeName"),
                        PaymentTypeArabicName = qt.Field<string>("PaymentTypeArabicName"),
                    }).ToList();
                }

                return referredSupplierQuotations;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetShubeddakDashboard
        public ShubeddakDashboard GetShubeddakDashboard()
        {
            DataSet dt = new DataSet();
            try
            {
                ShubeddakDashboard shubeddakDashboard = new ShubeddakDashboard();

                using (dt = ADOManager.Instance.DataSet("[getDashboardData]", CommandType.StoredProcedure))
                {
                    shubeddakDashboard.Suppliers = dt.Tables[0].AsEnumerable().Select(req => new GraphInfo
                    {
                        TotalCount = req.Field<int?>("TotalSuppliers"),
                        MonthName = req.Field<string>("MonthName"),

                    }).ToList();

                    shubeddakDashboard.InsuranceCompanies = dt.Tables[1].AsEnumerable().Select(req => new GraphInfo
                    {
                        TotalCount = req.Field<int?>("TotalInsuranceCompanies"),
                        MonthName = req.Field<string>("MonthName"),

                    }).ToList();

                    shubeddakDashboard.Requests = dt.Tables[2].AsEnumerable().Select(req => new GraphInfo
                    {
                        TotalCount = req.Field<int?>("TotalRequests"),
                        MonthName = req.Field<string>("MonthName"),

                    }).ToList();

                }
                return shubeddakDashboard;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetReferredSupplier
        public List<Supplier> GetReferredSupplier(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<Supplier> refferdSuppliers = new List<Supplier>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };

                using (dt = ADOManager.Instance.DataSet("[GetReferredSupplier]", CommandType.StoredProcedure, sParameter))
                {
                    refferdSuppliers = dt.Tables[0].AsEnumerable().Select(sup => new Supplier
                    {
                        SupplierID = sup.Field<int>("SupplierID"),
                        SupplierName = sup.Field<string>("SupplierName"),
                        LogoURL = sup.Field<string>("LogoURL"),
                        CityID = sup.Field<Int16?>("CityID"),
                        CityName = sup.Field<string>("CityName"),
                        CountryID = sup.Field<Int16?>("CountryID"),
                        CountryName = sup.Field<string>("CountryName"),
                        AddressLine1 = sup.Field<string>("AddressLine1"),
                        AddressLine2 = sup.Field<string>("AddressLine2"),
                        CPFirstName = sup.Field<string>("CPFirstName"),
                        CPLastName = sup.Field<string>("CPLastName"),
                        CPPositionID = sup.Field<Int16?>("CPPositionID"),
                        CPPhone = sup.Field<string>("CPPhone"),
                        CPEmail = sup.Field<string>("CPEmail"),
                        StatusID = sup.Field<Int16?>("StatusID"),
                        UserID = sup.Field<int>("UserID"),
                        IsSellNew = sup.Field<bool?>("IsSellNew"),
                        IsSellUsed = sup.Field<bool?>("IsSellUsed"),
                        RegistrationNumber = sup.Field<string>("RegistrationNumber"),
                        DemandID = sup.Field<int>("DemandID"),
                        QuotationID = sup.Field<int>("QuotationID"),
                        TotalOrderedParts = sup.Field<int>("TotalOrderedParts"),
                        
                    }).ToList();


                }
                return refferdSuppliers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnApprovePart
        public string OnApprovePart(int PartInfoID, Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@PartInfoID" , Value = PartInfoID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onApprovePart]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 1 ? "Pending" : StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion     
        
        #region OnApproveUvPart
        public string OnApproveUvPart(int UniversalPartID, Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@UniversalPartID" , Value = UniversalPartID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onApproveUvPart]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 1 ? "Pending" : StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnApproveOrder
        public string OnApproveOrder(int RequestID, int DemandID, int ModifiedBy, int CountDownTime, int POApprovalID, bool IsApproved)
        {
            DataSet dt = new DataSet();
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@CountDownTime" , Value = CountDownTime},
                        new SqlParameter { ParameterName = "@POApprovalID" , Value = POApprovalID},
                        new SqlParameter { ParameterName = "@IsApproved" , Value = IsApproved},
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onApproveOrder]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? result.ToString() : DataValidation.dbError;
                var result = "";
               // var result = ADOManager.Instance.ExecuteScalar("[onApproveOrder]", CommandType.StoredProcedure, sParameter);
                using (dt = ADOManager.Instance.DataSet("[onApproveOrder]", CommandType.StoredProcedure, sParameter))
                {
                    if (dt.Tables.Count > 1)
                    {
                        result = Convert.ToString(dt.Tables[1].Rows[0]["RemainingCount"]);
                    }
                    else {
                        result = Convert.ToString(dt.Tables[0].Rows[0]["RemainingCount"]);
                    }
                    }
                    return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region AcceptQuotationPart
        public string AcceptQuotationPart(int QuotationPartID, bool IsAccepted, int ModifiedBy, string AdminRejectNote)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationPartID" , Value = QuotationPartID},
                        new SqlParameter { ParameterName = "@IsAccepted" , Value = IsAccepted},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@AdminRejectNote" , Value = AdminRejectNote}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[acceptQuotationPart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnReviewQuotation
        public string OnReviewQuotation(int QuotationID, string ReviewNote, int ModifiedBy, int SupplierID, int RequestID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@JoReviewNote" , Value = ReviewNote},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[onReviewQuotation]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region CancelPurchaseOrder
        public string CancelPurchaseOrder(int RequestID, Int16 StatusID, int ModifiedBy, string RejectNote)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@RejectNote" , Value = RejectNote}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[cancelPurchaseOrder]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UndoCancelPurchaseOrder
        public string UndoCancelPurchaseOrder(int RequestID, int ModifiedBy, int QuotationID, int DemandID, int? QuotationPartID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@QuotationPartID" , Value = QuotationPartID},
                        new SqlParameter { ParameterName = "@DemandID" , Value = DemandID}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[undoCancelPurchaseOrder]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnApproveAllParts
        public string OnApproveAllParts(int SupplierID, Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onApproveAllParts]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 1 ? "Pending" : StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region OnApproveAllUvParts
        public string OnApproveAllUvParts(Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[onApproveAllUvParts]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? StatusID == 1 ? "Pending" : StatusID == 2 ? "Approved" : "Rejected" : result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetQualifiedSuppliers
        public List<Supplier> GetQualifiedSuppliers(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                var suppliers = new List<Supplier>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };
                using (dt = ADOManager.Instance.DataSet("[getQualifiedSuppliers]", CommandType.StoredProcedure, sParameter))
                {
                    suppliers = dt.Tables[0].AsEnumerable().Select(cmp => new Supplier
                    {
                        SupplierID = cmp.Field<int>("SupplierID"),
                        SupplierName = cmp.Field<string>("SupplierName"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        UserID = cmp.Field<int>("UserID"),
                        CityName = cmp.Field<string>("CityName"),
                        CityNameArabic = cmp.Field<string>("CityNameArabic"),
                        CountryName = cmp.Field<string>("CountryName"),
                        CountryNameArabic = cmp.Field<string>("CountryNameArabic"),
                        RejectNote = cmp.Field<string>("RejectNote"),
                        AlreadyApplied = cmp.Field<int>("AlreadyApplied"),
                        Rating = cmp.Field<byte>("Rating"),

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

        #region GetRequestQualifiedSuppliers
        public RequestSuppliers GetRequestQualifiedSuppliers()
        {
            DataSet dt = new DataSet();
            try
            {
                RequestSuppliers data = new RequestSuppliers();

                using (dt = ADOManager.Instance.DataSet("[getRequestQualifiedSuppliers]", CommandType.StoredProcedure))
                {
                    data.Suppliers = dt.Tables[0].AsEnumerable().Select(cmp => new Supplier
                    {
                        SupplierID = cmp.Field<int>("SupplierID"),
                        SupplierName = cmp.Field<string>("SupplierName"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        StatusID = cmp.Field<Int16?>("StatusID"),
                        UserID = cmp.Field<int>("UserID"),
                        CityName = cmp.Field<string>("CityName"),
                        CityNameArabic = cmp.Field<string>("CityNameArabic"),
                        CountryName = cmp.Field<string>("CountryName"),
                        CountryNameArabic = cmp.Field<string>("CountryNameArabic"),
                        RejectNote = cmp.Field<string>("RejectNote"),
                        //AlreadyApplied = cmp.Field<int>("AlreadyApplied"),
                        //RequestID = cmp.Field<int>("RequestID"),
                        //Rating = cmp.Field<byte>("Rating"),

                    }).ToList();
                    
                    data.Requests = dt.Tables[1].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        SupplierID = req.Field<int>("SupplierID"),
                        DemandID = req.Field<int?>("DemandID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        //TotalQuotations = req.Field<int>("TotalQuotations"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        //WorkshopName = req.Field<string>("WorkshopName"),
                        //WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        //WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        //WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        ImgURL = req.Field<string>("ImgURL"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("Name"),
                        CPName = req.Field<string>("CPName"),
                        StatusID = req.Field<Int16>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),

                    }).ToList();


                }
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveQuotationComment
        public string SaveQuotationComment(string Comment, int QuotationID, int UserID)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@Comment" , Value = Comment},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveQuotationComment]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region StopBiddingHours
        public string StopBiddingHours(int RequestID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[stopBiddingHours]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? result.ToString() : null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateSupplierOffersStatus
        public string UpdateSupplierOffersStatus(int QuotationID, bool IsAccepted, int ModifiedBy, string AdminRejectNote)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@QuotationID" , Value = QuotationID},
                        new SqlParameter { ParameterName = "@IsAccepted" , Value = IsAccepted},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@AdminRejectNote" , Value = AdminRejectNote}
                    };

                var result = ADOManager.Instance.ExecuteScalar("[updateSupplierOffersStatus]", CommandType.StoredProcedure, sParameter);
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetHistoryRequests
        public Companyrequests GetHistoryRequests(int StartRow, int RowsPerPage, int? MakeID, int? ModelID, int? YearID, string searchQuery, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                if (searchQuery == "null" || searchQuery == "")
                    searchQuery = null;

                var allrequests = new Companyrequests();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@StartRow" , Value = StartRow},
                        new SqlParameter { ParameterName = "@RowsPerPage" , Value = RowsPerPage},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                        new SqlParameter { ParameterName = "@YearID" , Value = YearID},
                        new SqlParameter { ParameterName = "@searchQuery" , Value = searchQuery},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getHistoryRequests]", CommandType.StoredProcedure, sParameter))
                {
                    allrequests.Requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        DemandID = req.Field<int?>("DemandID"),
                        MakeID = req.Field<int>("MakeID"),
                        ModelID = req.Field<int>("ModelID"),
                        ProductionYear = req.Field<Int16>("ProductionYear"),
                        CompanyID = req.Field<int>("CompanyID"),
                        TotalQuotations = req.Field<int>("TotalQuotations"),
                        VIN = req.Field<string>("VIN"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        YearCode = req.Field<int>("YearCode"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        AccidentID = req.Field<int?>("AccidentID"),
                        AccidentNo = req.Field<string>("AccidentNo"),
                        WorkshopName = req.Field<string>("WorkshopName"),
                        WorkshopCityName = req.Field<string>("WorkshopCityName"),
                        WorkshopPhone = req.Field<string>("WorkshopPhone"),
                        WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
                        ImgURL = req.Field<string>("ImgURL"),
                        CPPhone = req.Field<string>("CPPhone"),
                        ICName = req.Field<string>("Name"),
                        CPName = req.Field<string>("CPName"),
                        StatusID = req.Field<Int16>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        PlateNo = req.Field<string>("PlateNo"),
                        RequestNumber = req.Field<int?>("RequestNumber"),
                        BiddingHours = req.Field<double?>("BiddingHours"),
                        BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
                        IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
                        VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        RequestRowNumber = req.Field<int?>("RequestRowNumber"),
                        TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
                        ClearanceRoute = req.Field<string>("ClearanceRoute"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        
                    }).ToList();

                    allrequests.RequestedParts = dt.Tables[1].AsEnumerable().Select(rp => new RequestedPart
                    {
                        RequestID = rp.Field<int>("RequestID"),
                        AutomotivePartName = rp.Field<string>("AutomotivePartName"),
                        isSpliced = rp.Field<int>("isSpliced"),

                    }).ToList();

                    //allrequests.Makes = dt.Tables[2].AsEnumerable().Select(make => new Make
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

                    //allrequests.Models = dt.Tables[3].AsEnumerable().Select(model => new Model
                    //{
                    //    MakeID = model.Field<int>("MakeID"),
                    //    ModelID = model.Field<int>("ModelID"),
                    //    ModelCode = model.Field<string>("EnglishModelName"),
                    //    ArabicModelName = model.Field<string>("ArabicModelName"),
                    //    GroupName = model.Field<string>("GroupName")


                    //}).ToList();

                    //allrequests.Years = dt.Tables[4].AsEnumerable().Select(year => new Year
                    //{
                    //    LanguageYearID = year.Field<Int16>("LanguageYearID"),
                    //    YearCode = year.Field<int>("YearCode"),
                    //    LanguageID = year.Field<byte>("LanguageID"),
                    //    YearID = year.Field<Int16>("YearID"),

                    //}).ToList();

                    allrequests.Companies = dt.Tables[2].AsEnumerable().Select(cmp => new Company
                    {
                        Name = cmp.Field<String>("Name"),
                        CompanyID = cmp.Field<int>("CompanyID"),

                    }).ToList();

                    allrequests.TotalPages = Convert.ToInt32(dt.Tables[3].Rows[0]["TotalPages"]);
                }
                return allrequests;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetJoAccidentMeta
        public List<Company> GetJoAccidentMeta(int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Companies = new List<Company>();

                var sParameter = new List<SqlParameter>
                {

                    new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                };
                using (dt = ADOManager.Instance.DataSet("[getJoAccidentMeta]", CommandType.StoredProcedure,sParameter))
                {
                    Companies = dt.Tables[0].AsEnumerable().Select(cmp => new Company
                    {
                        CompanyID = cmp.Field<int>("CompanyID"),
                        CompanyName = cmp.Field<string>("Name"),
                        LogoURL = cmp.Field<string>("LogoURL"),
                        AddressLine1 = cmp.Field<string>("AddressLine1"),
                        AddressLine2 = cmp.Field<string>("AddressLine2"),
                        CPFirstName = cmp.Field<string>("CPFirstName"),
                        CPLastName = cmp.Field<string>("CPLastName"),
                        CPPhone = cmp.Field<string>("CPPhone"),
                        CPEmail = cmp.Field<string>("CPEmail"),
                        CountryID = cmp.Field<Int16?>("CountryID")

                    }).ToList();

                }
                return Companies;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetRequestAllOffers
        public RequestAllOffersData GetRequestAllOffers(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                var requestAllOffersData = new RequestAllOffersData();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getRequestAllOffers]", CommandType.StoredProcedure, sParameter))
                {
                    requestAllOffersData.Request = dt.Tables[0].AsEnumerable().Select(req => new Request
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
                        LogoURL = req.Field<string>("LogoURL"),
                        ICName = req.Field<string>("ICName"),
                        PlateNo = req.Field<string>("PlateNo"),
                        TotalOffersCount = req.Field<int?>("TotalOffersCount"),
                        BestMinOfferPrice = req.Field<double?>("BestMinOfferPrice"),
                        BestOfferQuotationID = req.Field<int?>("BestOfferQuotationID"),
                        BestOfferSupplierID = req.Field<int?>("BestOfferSupplierID"),
                        RejectedOffersCount = req.Field<int?>("RejectedOffersCount"),
                        AllOffersPdfUrl = req.Field<string>("AllOffersPdfUrl"),
                        SerialNo = req.Field<int?>("SerialNo"),
                        BestOfferMatchingSupplierID = req.Field<int?>("BestOfferMatchingSupplierID"),
                        BestMinOfferMatchingPrice = req.Field<double?>("BestMinOfferMatchingPrice"),
                        BestOfferMatchingQuotationID = req.Field<int?>("BestOfferMatchingQuotationID"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC"),
                        PolicyNumber = req.Field<string>("PolicyNumber"),
                        OwnerPhoneNo = req.Field<string>("OwnerPhoneNo"),
                        AccidentHappendOn = req.Field<DateTime?>("AccidentHappendOn"),
                        SurveyorAppointmentDate = req.Field<DateTime?>("SurveyorAppointmentDate"),
                    }).FirstOrDefault();

                    requestAllOffersData.SupplierQuotations = dt.Tables[1].AsEnumerable().Select(req => new Quotation
                    {
                        QuotationID = req.Field<int>("QuotationID"),
                        StatusID = req.Field<Int16?>("StatusID"),
                        StatusName = req.Field<string>("StatusName"),
                        ArabicStatusName = req.Field<string>("ArabicStatusName"),
                        SupplierID = req.Field<int>("SupplierID"),
                        SupplierName = req.Field<string>("SupplierName"),
                        //RequestID = req.Field<int>("RequestID"),
                        //ReferredPartsCount = req.Field<int?>("ReferredPartsCount"),
                        //RequestPartCount = req.Field<int?>("RequestPartCount"),
                        //BranchAreaName = req.Field<string>("BranchAreaName"),
                        //BranchName = req.Field<string>("BranchName"),
                        //QuotedPartCount = req.Field<int?>("QuotedPartCount"),
                        //PendingQuotationParts = req.Field<int>("PendingQuotationParts"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        CreatedSince = req.Field<string>("CreatedSince"),
                        CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
                        //DemandID = req.Field<int>("DemandID"),
                        CountDownDate = req.Field<DateTime?>("CountDownDate"),
                        CountDownTime = req.Field<int?>("CountDownTime"),
                        JoReviewNote = req.Field<string>("JoReviewNote"),
                        JoReviewStatusID = req.Field<Int16?>("JoReviewStatusID"),
                        IsNotAvailable = req.Field<bool?>("IsNotAvailable"),
                        Comment = req.Field<string>("Comment"),
                        DiscountValue = req.Field<double?>("DiscountValue"),
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
                        FillingRate = req.Field<decimal?>("FillingRate"),
                        BestOfferPrice = req.Field<double?>("BestOfferPrice"),
                        OfferSortNo = req.Field<int>("OfferSortNo"),
                        MatchingOfferSortNo = req.Field<int>("MatchingOfferSortNo"),
                        MatchingFillingRate = req.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = req.Field<double?>("LowestOfferMatchingPrice"),
                        SupplierType = req.Field<Int16>("SupplierType"),
                        POROAmount = req.Field<Double?>("POROAmount"),
                        LabourDays = req.Field<Double?>("LabourDays"),
                        
                    }).ToList();

                    requestAllOffersData.RequestedParts = dt.Tables[2].AsEnumerable().Select(rp => new RequestedPart
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

                    requestAllOffersData.RequestedPartsImages = dt.Tables[3].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        ObjectTypeID = img.Field<Int16>("ObjectTypeID"),

                    }).ToList();

                    //requestAllOffersData.Branches = dt.Tables[4].AsEnumerable().Select(req => new PartBranch
                    //{
                    //    BranchID = req.Field<int>("BranchID"),
                    //    SupplierID = req.Field<int>("SupplierID"),
                    //    BranchCityID = req.Field<int>("BranchCityID"),
                    //    BranchName = req.Field<string>("BranchName"),
                    //    BranchGoogleMapLink = req.Field<string>("BranchGoogleMapLink"),
                    //    BranchPhone = req.Field<string>("BranchPhone"),
                    //    BranchAreaName = req.Field<string>("BranchAreaName"),

                    //}).ToList();

                    requestAllOffersData.AccidentNotes = dt.Tables[4].AsEnumerable().Select(nt => new Note
                    {
                        NoteID = nt.Field<int>("NoteID"),
                        AccidentID = nt.Field<int>("AccidentID"),
                        TextValue = nt.Field<string>("TextValue")
                    }).ToList();

                    requestAllOffersData.AccidentImages = dt.Tables[5].AsEnumerable().Select(img => new Image
                    {
                        ImageID = img.Field<int>("ImageID"),
                        EncryptedName = img.Field<string>("EncryptedName"),
                        OriginalName = img.Field<string>("OriginalName"),
                        ImageURL = img.Field<string>("ImageURL"),
                        ObjectID = img.Field<int>("ObjectID"),
                        Description = img.Field<string>("Description"),
                        IsDocument = img.Field<bool?>("IsDocument")
                    }).ToList();

                    requestAllOffersData.QuotationParts = dt.Tables[6].AsEnumerable().Select(rp => new QuotationPart
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
                            Quantity = rp.Field<int>("Quantity"),
                            SupplierID = rp.Field<int>("SupplierID"),
                            AutomotivePartName = rp.Field<string>("PartName"),
                            AdminRejectNote = rp.Field<string>("AdminRejectNote"),
                            DeliveryTime = rp.Field<byte?>("DeliveryTime"),
                            IsAvailableNow = rp.Field<Int16?>("IsAvailableNow"),
                            IsOrdered = rp.Field<bool?>("IsOrdered"),
                            IsReferred = rp.Field<bool?>("IsReferred"),
                            IsAdminAccepted = rp.Field<bool?>("IsAdminAccepted"),
                            IsAccepted = rp.Field<bool?>("IsAccepted"),
                            PartBranchID = rp.Field<int?>("PartBranchID"),
                            OrderedQuantity = rp.Field<int?>("OrderedQuantity"),
                            RPConditionTypeName = rp.Field<string>("RPConditionTypeName"),
                            RPConditionTypeNameArabic = rp.Field<string>("RPConditionTypeNameArabic"),
                            RPNewPartConditionTypeName = rp.Field<string>("RPNewPartConditionTypeName"),
                            RPNewPartConditionTypeArabicName = rp.Field<string>("RPNewPartConditionTypeArabicName"),
                            DepriciationPrice=rp.Field<double?>("DepriciationPrice"),
                            Depriciationvalue=rp.Field<double?>("Depriciationvalue")
                        }).ToList();

                    requestAllOffersData.QuotationPartsImages = dt.Tables[7].AsEnumerable().Select(img => new Image
                        {
                            ImageID = img.Field<int>("ImageID"),
                            EncryptedName = img.Field<string>("EncryptedName"),
                            OriginalName = img.Field<string>("OriginalName"),
                            ImageURL = img.Field<string>("ImageURL"),
                            ObjectID = img.Field<int>("ObjectID"),
                            ObjectTypeID = img.Field<Int16>("ObjectTypeID"),
                        }).ToList();
                    }

                return requestAllOffersData;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region PrintOffersPdf
        public string PrintOffersPdf(PdfData pdfData)
        {
            //int errorOnLine = 0;

            try
            {
                //Iron Pdf Licence
                IronPdf.License.LicenseKey = "IRONPDF.DEVTEAM.IRO210217.7379.21124.712012-2E7C41D45D-DXYBL6HZA6UJTDP-UNLHZLW3RMZI-AXDBVXIZCEHF-KJFWAHYFDEBW-OVA6R5MJFS2S-4E3SYD-LYEDON74BKOEEA-DEVELOPER.5APP.1YR-Z7RF37.RENEW.SUPPORT.17.FEB.2022";
                //errorOnLine ++;
                //"IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019";
                //bool result2 = IronPdf.License.IsValidLicense("IRONPDF-507372A640-108944-4F5481-1E3106A2C9-C6CCF902-UEx6BCBD2F73F257D8-PROJECT.LICENSE.1.DEVELOPER.SUPPORTED.UNTIL.17.OCT.2019");

                pdfData.elementHtml = (string)JsonConvert.DeserializeObject(pdfData.elementHtml);
                //errorOnLine++;

                HtmlToPdf HtmlToPdf = new IronPdf.HtmlToPdf();
                //errorOnLine++;
                HtmlToPdf.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginTop = 2;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginBottom = 2;
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginLeft = 2;  //millimeters
                //errorOnLine++;
                HtmlToPdf.PrintOptions.MarginRight = 2;
                //errorOnLine++;


                var footer = "";

                if (pdfData.CountryID == 2)
                {
                    footer = "<b style=\"display:block;text-align: center;color:grey\">P.O Box 5282 Manama, Kingdom of Bahrain, Telephone: 00973 17784847<br>www.soldarity.com</b><br><center><i>Page&nbsp;{page} of {total-pages}<i></center>";
                }
                else
                {
                footer = "<center><i>{page} of {total-pages}<i></center>";
                }

                HtmlToPdf.PrintOptions.Footer = new HtmlHeaderFooter()
                {
                    Height = 25,
                    HtmlFragment = footer,
                    DrawDividerLine = false
                };
                //errorOnLine++;
                HtmlToPdf.PrintOptions.Zoom = 80;

                //errorOnLine++;
                PdfDocument PDF = HtmlToPdf.RenderHtmlAsPdf(pdfData.elementHtml);
                //errorOnLine++;

                string fileName = "All-Offers-Request-No" + pdfData.RequestNumber + "-RequestID-" + pdfData.RequestID + ".pdf";

                //errorOnLine++;

                DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("/offers-reports"));
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

                //errorOnLine++;

                var sParameter = new List<SqlParameter> {

                    new SqlParameter {ParameterName = "@RequestID", Value = pdfData.RequestID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = pdfData.ModifiedBy},
                    new SqlParameter {ParameterName = "@FileName", Value = "offers-reports/" + fileName},

                };


                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updaterequestOffersPdfUrl]", CommandType.StoredProcedure, sParameter));
                //errorOnLine++;

                PDF.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("/offers-reports"), fileName));

                //errorOnLine++;

                return "offers-reports/" + fileName;
            }
            catch (Exception ex)
            {
                //Log.Error("Method in context DeleteEmailRequest(): " + ex.Message);
                return ex.Message;
                //return  errorOnLine.ToString();
            }
        }
        #endregion

        #region GetWorkshops
        public List<Workshop> GetWorkshops(int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                var Workshops = new List<Workshop>();

                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}
                };
                using (dt = ADOManager.Instance.DataSet("[getWorkshops]", CommandType.StoredProcedure,sParameter))
                {
                    Workshops = dt.Tables[0].AsEnumerable().Select(wp => new Workshop
                    {
                        WorkshopID = wp.Field<int>("WorkshopID"),
                        WorkshopName = wp.Field<string>("WorkshopName"),
                        WorkshopAreaName = wp.Field<string>("WorkshopAreaName"),
                        StatusID = wp.Field<Int16?>("StatusID"),
                        UserID = wp.Field<int>("UserID"),
                        WorkshopPhone = wp.Field<string>("WorkshopPhone"),
                        WorkshopCityName = wp.Field<string>("WorkshopCityName"),
                        WorkshopGoogleMapLink = wp.Field<string>("WorkshopGoogleMapLink"),
                        ProfileImageURL = wp.Field<string>("ProfileImageURL"),
                        Email = wp.Field<string>("Email"),
                        IsCompanyApproved = wp.Field<bool?>("IsCompanyApproved"),
                        CompanyID = wp.Field<int?>("CompanyID"),
                        RequestLimit = wp.Field<int?>("AccidentLimit"),
                        IsDeleted = wp.Field<bool>("AccountIsDeleted"),
                        Id = wp.Field<string>("Id"),
                        CountryID = wp.Field<Int16?>("CountryID")
                      
                    }).ToList();

                }
                return Workshops;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateWorkshopStatus
        public string UpdateWorkshopStatus(int WorkshopID, Int16 StatusID, int ModifiedBy)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateWorkshopStatus]", CommandType.StoredProcedure, sParameter));
                return result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UnPublishPartsRequest
        public string UnPublishPartsRequest(int RequestID, int ModifiedBy, string ReturnReason)
        {
            
                try
                {
                   var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy},
                        new SqlParameter { ParameterName = "@ReturnReason" , Value = ReturnReason}
                       };

                int result = ADOManager.Instance.ExecuteNonQuery("[unPublishPartsRequest]", CommandType.StoredProcedure, sParameter);
                return result > 0 ? "Request unpublished successfully" : "Request already unpublished";

                //var result = ADOManager.Instance.ExecuteScalar("[PublishPartsRequest]", CommandType.StoredProcedure, sParameter);
                //    return result.ToString();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            
        }
        #endregion

        #region GetParts
        public PartsMeta GetParts(int StatusID, int? DamagePointID, string SearchQuery, int PageNo,int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {
                PartsMeta partsMeta = new PartsMeta();

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@StatusID" , Value = StatusID  },
                        new SqlParameter { ParameterName = "@DamagePointID" , Value = DamagePointID },
                        new SqlParameter { ParameterName = "@SearchQuery" , Value = SearchQuery == null || SearchQuery == "undefined" ? null : SearchQuery},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo },
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID },

                };

                using (dt = ADOManager.Instance.DataSet("[getParts]", CommandType.StoredProcedure, sParameter))
                {


                   partsMeta.AutomotivePart = dt.Tables[0].AsEnumerable().Select(dm => new AutomotivePart
                    {
                        AutomotivePartID = dm.Field<int?>("AutomotivePartID"),
                        DamagePointID = dm.Field<string>("DamagePointID"),
                        PartName = dm.Field<string>("PartName"),
                        Name1 = dm.Field<string>("Name1"),
                        ItemNumber = dm.Field<string>("ItemNumber"),
                        OptionName = dm.Field<string>("OptionName"),
                        FinalCode = dm.Field<string>("FinalCode"),
                        OptionNumber = dm.Field<string>("OptionNumber"),
                        Name2 = dm.Field<string>("Name2"),
                       Name3 = dm.Field<string>("Name3"),
                       Name4 = dm.Field<string>("Name4"),
                       Name5 = dm.Field<string>("Name5"),
                       Name6 = dm.Field<string>("Name6"),
                       Name7 = dm.Field<string>("Name7"),
                       Name8 = dm.Field<string>("Name8"),
                       DamageName = dm.Field<string>("DamageName"),
                       OffersCount = dm.Field<int?>("OffersCount"),
                       RequestCount = dm.Field<int?>("RequestCount"),
                       EnglishMakeName = dm.Field<string>("EnglishMakeName"),
                       ArabicMakeName = dm.Field<string>("ArabicMakeName"),
                       EnglishModelName = dm.Field<string>("EnglishModelName"),
                       ArabicModelName = dm.Field<string>("ArabicModelName"),
                       TotalParts = dm.Field<int?>("TotalParts"),
                       

                   }).ToList();
                    partsMeta.TabData = dt.Tables[1].AsEnumerable().Select(tbi => new TabInfo
                    {
                        PendingParts = tbi.Field<int?>("PendingParts"),
                        ApprovedParts = tbi.Field<int?>("ApprovedParts"),
                        RejectedParts = tbi.Field<int?>("RejectedParts")

                    }).FirstOrDefault();

                }
                return partsMeta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region UpdatePartInfo
        public string UpdatePartInfo(AutomotivePart automotivePart)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@AutomotivePartID" , Value = automotivePart.AutomotivePartID  },
                        new SqlParameter { ParameterName = "@PartName" , Value = automotivePart.PartName },
                        new SqlParameter { ParameterName = "@StatusID" , Value = automotivePart.StatusID},
                        new SqlParameter { ParameterName = "@DamagePointID" , Value =automotivePart.DamagePointID  },
                        new SqlParameter { ParameterName = "@DamageName" , Value =automotivePart.DamageName  },
                        new SqlParameter { ParameterName = "@ItemNumber" , Value =automotivePart.ItemNumber  },
                        new SqlParameter { ParameterName = "@Name1" , Value =automotivePart.Name1  },
                        new SqlParameter { ParameterName = "@OptionName" , Value =automotivePart.OptionName  },
                        new SqlParameter { ParameterName = "@OptionNumber" , Value =automotivePart.OptionNumber  },
                        new SqlParameter { ParameterName = "@FinalCode" , Value =automotivePart.FinalCode  },
                        new SqlParameter { ParameterName = "@Name2" , Value =automotivePart.Name2 },
                        new SqlParameter { ParameterName = "@Name3" , Value =automotivePart.Name3  },
                        new SqlParameter { ParameterName = "@Name4" , Value =automotivePart.Name4  },
                        new SqlParameter { ParameterName = "@Name5" , Value =automotivePart.Name5  },
                        new SqlParameter { ParameterName = "@Name6" , Value =automotivePart.Name6  },
                        new SqlParameter { ParameterName = "@Name7" , Value =automotivePart.Name7  },
                        new SqlParameter { ParameterName = "@Name8" , Value =automotivePart.Name8  },
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value =automotivePart.ModifiedBy  }



                };
                int result = ADOManager.Instance.ExecuteNonQuery("[updatePartInfo]", CommandType.StoredProcedure, sParameter);
                return result > 0 ? "Part Updated successfully" : "Part is not updated";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetPartsDetails
        public PartsMeta GetPartsDetails(AutomotivePart automotivePart)
        {
            DataSet dt = new DataSet();
            try
            {
               var XMLDamagePoint = automotivePart.DamagePoint.ToXML("ArrayOfDamagePoint");
                PartsMeta partsMeta = new PartsMeta();

                var sParameter = new List<SqlParameter>
                {
                      
                        new SqlParameter { ParameterName = "@DamagePointID" , Value = XMLDamagePoint },
                        new SqlParameter { ParameterName = "@PageNo" , Value = automotivePart.PageNo },
                        new SqlParameter { ParameterName = "@Name1" , Value = automotivePart.Name1 != "undefined"? automotivePart.Name1:null  },
                        new SqlParameter { ParameterName = "@CountryID" , Value = automotivePart.CountryID }


                };

                using (dt = ADOManager.Instance.DataSet("[getPartsDetail]", CommandType.StoredProcedure, sParameter))
                {


                    partsMeta.Name1 = dt.Tables[0].AsEnumerable().Select(dm => new AutomotivePart
                    {
                        Name1 = dm.Field<string>("Name1"),
                        
                    }).ToList();

                    partsMeta.AutomotivePart = dt.Tables[1].AsEnumerable().Select(dm => new AutomotivePart
                    {
                        AutomotivePartID = dm.Field<int?>("AutomotivePartID"),
                        DamagePointID = dm.Field<string>("DamagePointID"),
                        PartName = dm.Field<string>("PartName"),
                        Name1 = dm.Field<string>("Name1"),
                        ItemNumber = dm.Field<string>("ItemNumber"),
                        OptionName = dm.Field<string>("OptionName"),
                        FinalCode = dm.Field<string>("FinalCode"),
                        OptionNumber = dm.Field<string>("OptionNumber"),
                        Name2 = dm.Field<string>("Name2"),
                        Name3 = dm.Field<string>("Name3"),
                        Name4 = dm.Field<string>("Name4"),
                        Name5 = dm.Field<string>("Name5"),
                        Name6 = dm.Field<string>("Name6"),
                        Name7 = dm.Field<string>("Name7"),
                        Name8 = dm.Field<string>("Name8"),
                        DamageName = dm.Field<string>("DamageName"),
                        OffersCount = dm.Field<int?>("OffersCount"),
                        StatusID = dm.Field<Int16?>("StatusID")


                    }).ToList();
                    partsMeta.AutomotivePartCount = Convert.ToInt32(dt.Tables[2].Rows[0]["AutomotivePartCount"]);


                }
                return partsMeta;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region GetAdminUsers
        public List<ShubeddakUser> GetAdminUsers(int UserID, int? countryID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<ShubeddakUser> shubeddakUsers = new List<ShubeddakUser>();

                var sParameter = new List<SqlParameter>
                {
                     new SqlParameter { ParameterName = "@UserID" , Value = UserID },
                     new SqlParameter { ParameterName = "@countryID" , Value = countryID },
                };

                using (dt = ADOManager.Instance.DataSet("[getAdminUsers]", CommandType.StoredProcedure, sParameter))
                {

                    shubeddakUsers = dt.Tables[0].AsEnumerable().Select(su => new ShubeddakUser
                    {
                        Email = su.Field<string>("Email"),
                        PhoneNumber = su.Field<string>("PhoneNumber"),
                        UserID = su.Field<int>("UserID"),
                        ShubeddakUserID = su.Field<int>("ShubeddakUserID"),
                        RoleID = su.Field<byte>("RoleID"),
                        RoleName = su.Field<string>("Name"),
                        RoleIcon = su.Field<string>("Icon"),
                        FirstName = su.Field<string>("FirstName"),
                        LastName = su.Field<string>("LastName"),
                        ProfileImageURL = su.Field<string>("ProfileImageURL"),
                        RoleNameArabic = su.Field<string>("RoleNameArabic"),
                        CountryID = su.Field<int?>("CountryID")
                    }).ToList();

                   

                }
                return shubeddakUsers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAdminMeta
        public CommonMeta GetAdminMeta(int? UserID,int? ShubeddakUserID)
        {
            DataSet dt = new DataSet();
            try
            {
                var commonMeta = new CommonMeta();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@ShubeddakUserID" , Value = ShubeddakUserID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getAdminMeta]", CommandType.StoredProcedure, sParameter))
                {

                    commonMeta.Roles = dt.Tables[0].AsEnumerable().Select(rl => new Roles
                    {
                        Id = rl.Field<byte>("Id"),
                        Name = rl.Field<string>("Name"),
                        ObjectName = rl.Field<string>("ObjectName"),
                        Icon = rl.Field<string>("Icon"),
                        RolesNameArabic = rl.Field<string>("RolesNameArabic")

                    }).ToList();

                    //commonMeta.ICWorkshops = dt.Tables[1].AsEnumerable().Select(icw => new ICWorkshop
                    //{
                    //    ICWorkshopID = icw.Field<int>("ICWorkshopID"),
                    //    WorkshopName = icw.Field<string>("WorkshopName"),

                    //}).ToList();

                    commonMeta.UserPermissions = dt.Tables[1].AsEnumerable().Select(pm => new Permission
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

                    if (ShubeddakUserID > 0)
                    {
                        commonMeta.ShubeddakUserObj = dt.Tables[2].AsEnumerable().Select(req => new ShubeddakUser
                        {
                            ShubeddakUserID = req.Field<int>("ShubeddakUserID"),
                            LastName = req.Field<string>("LastName"),
                            FirstName = req.Field<string>("FirstName"),
                            UserID = req.Field<int>("UserID"),
                            ProfileImageURL = req.Field<string>("ProfileImageURL"),
                            CreatedOn = req.Field<DateTime>("CreatedOn"),
                            CreatedBy = req.Field<int?>("CreatedBy"),
                            ModifiedBy = req.Field<int?>("ModifiedBy"),
                            ModifiedOn = req.Field<DateTime?>("ModifiedOn"),
                            Email = req.Field<string>("Email"),
                            PhoneNumber = req.Field<string>("PhoneNumber"),
                            RoleID = req.Field<byte>("RoleID"),
                            

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

        #region SaveAdminUser

        public ShubeddakUser SaveAdminUser(ShubeddakUser user)
        {
            DataSet dt = new DataSet();
            try
            {
                user.Id = Guid.NewGuid().ToString();
                var employee = new ShubeddakUser();
                var XMLUserPermissions = user.UserPermissions.ToXML("ArrayOfUserPermissions");

                var sParameter = new List<SqlParameter>
                {
                   
                      new SqlParameter { ParameterName = "@Email" , Value = user.Email},
                      new SqlParameter { ParameterName = "@RoleID" , Value = user.RoleID},
                      new SqlParameter { ParameterName = "@CreatedBy" , Value = user.CreatedBy},
                      new SqlParameter { ParameterName = "@Id", Value = user.Id },
                      new SqlParameter { ParameterName = "@FirstName" , Value = user.FirstName},
                      new SqlParameter { ParameterName = "@LastName" , Value = user.LastName},
                      new SqlParameter { ParameterName = "@Password", Value = EncryptionDecryption.EncryptString(user.Password)},
                      new SqlParameter { ParameterName = "@PhoneNumber" , Value = user.PhoneNumber},
                      new SqlParameter { ParameterName = "@XMLUserPermissions" , Value = XMLUserPermissions},
                      new SqlParameter { ParameterName = "@ImgURL" , Value = user.ProfileImageURL},
                      new SqlParameter { ParameterName = "@CountryID" , Value = user.CountryID},
                };
                using (dt = ADOManager.Instance.DataSet("[saveAdminUser]", CommandType.StoredProcedure, sParameter))
                {
                    employee.ShubeddakUserID = Convert.ToInt32(dt.Tables[0].Rows[0]["EmployeeID"]);
                    employee.UserID = Convert.ToInt32(dt.Tables[0].Rows[0]["UserID"]);
                    employee.ErrorMessage = Convert.ToString(dt.Tables[0].Rows[0]["ErrorMessage"]);
                    employee.RoleID = Convert.ToByte(dt.Tables[0].Rows[0]["RoleID"]);
                }
                //    var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveUser]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? result.ToString() : result.ToString();
                return employee;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        #region DeleteAdminUser
        public string DeleteAdminUser(int UserID, int ModifiedBy)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                    new SqlParameter {ParameterName = "@UserID", Value = UserID},
                    new SqlParameter {ParameterName = "@ModifiedBy", Value = ModifiedBy},

                };
                var result = Convert.ToString(ADOManager.Instance.ExecuteScalar("[deleteAdminUser]", CommandType.StoredProcedure, sParameter));
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region UpdateUser
        public string UpdateAdminUser(ShubeddakUser User)
        {
            try
            {
                var XMLUserPermissions = User.UserPermissions.ToXML("ArrayOfUserPermissions");
                var sParameter = new List<SqlParameter>
                    {

                         new SqlParameter { ParameterName = "@RoleID" , Value =  User.RoleID},
                         new SqlParameter { ParameterName = "@ModifiedBy" , Value = User.ModifiedBy},
                         new SqlParameter { ParameterName = "@FirstName" , Value = User.FirstName},
                         new SqlParameter { ParameterName = "@LastName" , Value =  User.LastName},
                         new SqlParameter { ParameterName = "@PhoneNumber" , Value = User.PhoneNumber},
                         new SqlParameter { ParameterName = "@ShubeddakUserID" , Value =  User.ShubeddakUserID},
                         new SqlParameter { ParameterName = "@UserID" , Value =  User.UserID},
                         new SqlParameter { ParameterName = "@Password", Value = User.Password != null && User.Password != "" ?EncryptionDecryption.EncryptString(User.Password) : null},
                         new SqlParameter { ParameterName = "@ImgURL" , Value = User.ProfileImageURL},
                         new SqlParameter { ParameterName = "@Email" , Value = User.Email},


                         new SqlParameter { ParameterName = "@XMLUserPermissions" , Value = XMLUserPermissions},
                         //new SqlParameter { ParameterName = "@ConfirmPassword" , Value = User.ConfirmPassword},
                           
                    };

                //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[updateUser]", CommandType.StoredProcedure, sParameter));
                //return result > 0 ? "User Updated successfully" : null;

                var result = ADOManager.Instance.ExecuteScalar("[updateAdminUser]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetRequestsByPart
        public List<Request> GetRequestsByPart(int AutomotivePartID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<Request> requests = new List<Request>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@AutomotivePartID" , Value = AutomotivePartID}
                    };

                using (dt = ADOManager.Instance.DataSet("[getRequestsByPart]", CommandType.StoredProcedure, sParameter))
                {
                    requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentNo = req.Field<string>("AccidentNo"),
                        AccidentID = req.Field<int>("AccidentID"),
                        RequestNumber = req.Field<int>("RequestNumber"),
                        RequestID = req.Field<int>("RequestID"),
                        CompanyName = req.Field<string>("CompanyName"),
                        CreatedOn = req.Field<DateTime>("CreatedOn"),
                        CreatedByName = req.Field<string>("CreatedByName"),
                        IsRequestWorkshopIC = req.Field<bool?>("IsRequestWorkshopIC")
                    }).ToList();

                }

                return requests;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveMake
        public Object SaveMake(string MakeName, int UserID, string ImgURL, int? MakeID,string ArabicMakeName)
        {
            DataSet dt = new DataSet();
            Make make = new Make();
            var data = new Object();
            string Error;
            bool IsError;
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@EnglishMakeName" , Value =  MakeName},
                        new SqlParameter { ParameterName = "@ArabicMakeName" , Value = ArabicMakeName},
                        new SqlParameter { ParameterName = "@ImgURL" , Value = ImgURL},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID}
                    };

                using (dt = ADOManager.Instance.DataSet("[saveAndUpdateMake]", CommandType.StoredProcedure, sParameter))
                {
                    if (!dt.Tables[0].Columns.Contains("Error"))
                    {
                        make = dt.Tables[0].AsEnumerable().Select(req => new Make
                        {
                            MakeID = req.Field<int>("MakeID"),
                            MakeName = req.Field<string>("EnglishMakeName"),
                            ArabicMakeName = req.Field<string>("ArabicMakeName"),
                            ImgURL = req.Field<string>("ImgURL"),
                            UserID = req.Field<int>("UserID"),
                        }).FirstOrDefault();
                        data = new { make = make, IsError = false };

                    }else
                    {
                        Error = Convert.ToString(dt.Tables[0].Rows[0]["Error"]);
                        data = new { Error = Error, IsError = true };
                    }

                }

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region SaveModel
        public Object SaveModel(string ModelCode, int UserID, int MakeID, int? ModelID, string GroupName,string ArabicModelName)
        {
            DataSet dt = new DataSet();
            bool IsError;
            Model model = new Model();
            var data = new Object();
            string Error;

            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@GroupName" , Value = GroupName},
                        new SqlParameter { ParameterName = "@EnglishModelName" , Value = ModelCode},
                        new SqlParameter { ParameterName = "@ArabicModelName" , Value = ArabicModelName},
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                    };

                using (dt = ADOManager.Instance.DataSet("[saveAndUpdateModel]", CommandType.StoredProcedure, sParameter))
                {
                    if (!dt.Tables[0].Columns.Contains("Error"))
                    {

                        model = dt.Tables[0].AsEnumerable().Select(req => new Model
                        {
                            MakeID = req.Field<int>("MakeID"),
                            ModelID = req.Field<int>("ModelID"),
                            MakeName = req.Field<string>("EnglishMakeName"),
                            ArabicMakeName = req.Field<string>("ArabicMakeName"),
                            ArabicModelName = req.Field<string>("ArabicModelName"),
                            ModelCode = req.Field<string>("EnglishModelName"),
                            ImgURL = req.Field<string>("ImgURL"),
                        }).FirstOrDefault();

                        data = new { model = model, IsError = false };
                    }
                    else
                    {
                        Error = Convert.ToString(dt.Tables[0].Rows[0]["Error"]);
                        data = new {Error= Error , IsError = true};

                    }

                }
                

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetModelAllAccident
        public List<Request> GetModelAllAccident(int ModelID, int UserID)
        {
            DataSet dt = new DataSet();
            List<Request> requests = new List<Request>();
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID},
                        new SqlParameter { ParameterName = "@ModelID" , Value = ModelID},
                    };

                using (dt = ADOManager.Instance.DataSet("[getModelAllAccident]", CommandType.StoredProcedure, sParameter))
                {
                    requests = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        AccidentNo = req.Field<string>("AccidentNo"),
                        RequestNumber = req.Field<int>("RequestNumber"),
                        MakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        ArabicModelName = req.Field<string>("ArabicModelName"),
                        ModelCode = req.Field<string>("EnglishModelName"),
                        AccidentID = req.Field<int>("AccidentID"),
                        RequestID = req.Field<int>("RequestID"),
                    }).ToList();

                }

                return requests;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAccidentBySeries

        public Object GetAccidentBySeries(string JCSeriesID, int PageNo)
        {
            DataSet dt = new DataSet();
            List<Request> accidents = new List<Request>();
            int AccidentCount = 0;
            SignInResponse signInResponse = new SignInResponse();
            try
            {
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@JCSeriesID" , Value = JCSeriesID},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                };



                using (dt = ADOManager.Instance.DataSet("[GetAccidentBySeries]", CommandType.StoredProcedure, sParameter))
                {

                    accidents = dt.Tables[0].AsEnumerable().Select(ac => new Request
                    {
                        AccidentID = ac.Field<int>("AccidentID"),
                        AccidentNo = ac.Field<string>("AccidentNo"),
                        CreatedByName = ac.Field<string>("CreatedByName"),
                        CreatedOn = ac.Field<DateTime>("CreatedOn"),
                        CompanyName = ac.Field<string>("CompanyName"),
                        JCSeriesID = ac.Field<string>("JCSeriesID"),
                        RequestID = ac.Field<int>("RequestID"),
                        RequestNumber = ac.Field<int?>("RequestNumber"),
                        IsRequestWorkshopIC = ac.Field<bool?>("IsRequestWorkshopIC")
                    }).ToList();
                    AccidentCount = Convert.ToInt32(dt.Tables[1].Rows[0]["AccidentCount"]);

                    Object data = new { accidents = accidents, AccidentCount = AccidentCount };
                    return data;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region AcceptAndRejectHybridIC
        public string AcceptAndRejectHybridIC(int WorkshopID, bool IsCompanyApproved, int ModifiedBy)
        {
            DataSet dt = new DataSet();
            
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@WorkshopID" , Value = WorkshopID},
                        new SqlParameter { ParameterName = "@IsCompanyApproved" , Value = IsCompanyApproved},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value = ModifiedBy}
                       
                    };

               var result =  ADOManager.Instance.ExecuteScalar("[AcceptAndRejectHybridIC]", CommandType.StoredProcedure, sParameter);

                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    #endregion

        #region GetSupplierReport

        public SupplierReport GetSupplierReport(int SupplierID, DateTime StartDate, DateTime EndDate,
              int? PageNo)
        {
              DataSet dt = new DataSet();
              SupplierReport supplierReport = new SupplierReport();
            try
            {
                var sParameter = new List<SqlParameter>
                            {
                               new SqlParameter { ParameterName = "@SupplierID" , Value = SupplierID},
                               new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                               new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                               new SqlParameter { ParameterName = "@PageNo" , Value = PageNo},
                            };

                using (dt = ADOManager.Instance.DataSet("[getSupplierReport]", CommandType.StoredProcedure, sParameter))
                {
                      supplierReport.TotalRequestsReceived = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalRequestsReceived"]);
                      supplierReport.TotalRequestsApplied = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalRequestsApplied"]);
                      supplierReport.TotalRequestsAppliedWithNotAvailable = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalRequestsAppliedWithNotAvailable"]);
                      supplierReport.TotalPricingAndProvidingRequestsReceived = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingAndProvidingRequestsReceived"]);
                      supplierReport.TotalPricingAndProvidingRequestsApplied = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingAndProvidingRequestsApplied"]);
                      supplierReport.TotalPricingOnlyRequestsAppliedWithNotAvailable = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingOnlyRequestsAppliedWithNotAvailable"]);
                      supplierReport.TotalPricingOnlyRequestsReceived = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingOnlyRequestsReceived"]);
                      supplierReport.TotalPricingOnlyRequestsApplied = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingOnlyRequestsApplied"]);
                      supplierReport.TotalPricingOnlyRequestsAppliedWithNotAvailable = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingOnlyRequestsAppliedWithNotAvailable"]);
                    supplierReport.TotalPricingAndProvidingRequestsAppliedWithNotAvailable = Convert.ToInt32(dt.Tables[0].Rows[0]["TotalPricingAndProvidingRequestsAppliedWithNotAvailable"]);


                    supplierReport.MakeOffers = dt.Tables[1].AsEnumerable().Select(req => new MakeOffer
                     {
                        EnglishMakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        TotalRequestsReceived = req.Field<int?>("TotalRequestsReceived"),
                        TotalRequestsApplied = req.Field<int?>("TotalRequestsApplied"),
                        TotalRequestsAppliedWithNotAvailable = req.Field<int?>("TotalRequestsAppliedWithNotAvailable"),
                        TotalPricingAndProvidingRequestsReceived = req.Field<int?>("TotalPricingAndProvidingRequestsReceived"),
                        TotalPricingAndProvidingRequestsApplied = req.Field<int?>("TotalPricingAndProvidingRequestsApplied"),
                        TotalPricingAndProvidingRequestsAppliedWithNotAvailable = req.Field<int?>("TotalPricingAndProvidingRequestsAppliedWithNotAvailable"),
                        TotalPricingOnlyRequestsReceived = req.Field<int?>("TotalPricingOnlyRequestsReceived"),
                        TotalPricingOnlyRequestsApplied = req.Field<int?>("TotalPricingOnlyRequestsApplied"),
                        TotalPricingOnlyRequestsAppliedWithNotAvailable = req.Field<int?>("TotalPricingOnlyRequestsAppliedWithNotAvailable"),
                      }).ToList();
                      supplierReport.SupplierOffers = dt.Tables[2].AsEnumerable().Select(req => new SupplierOffer
                      {
                        RequestID = req.Field<int>("RequestID"),
                        Requestnumber = req.Field<int>("Requestnumber"),
                        StatusID = req.Field<Int16>("StatusID"),
                        RequestStatusName = req.Field<string>("RequestStatusName"),
                        RequestArabicStatusName = req.Field<string>("RequestArabicStatusName"),
                        EnglishMakeName = req.Field<string>("EnglishMakeName"),
                        ArabicMakeName = req.Field<string>("ArabicMakeName"),
                        IsPurchasing = req.Field<bool?>("IsPurchasing"),
                        CompanyName = req.Field<string>("CompanyName"),
                        QuotationID = req.Field<int>("QuotationID"),
                        IsNotAvailable = req.Field<bool?>("IsNotAvailable"),
                        FillingRate = req.Field<decimal?>("FillingRate"),
                        BestOfferPrice = req.Field<double?>("BestOfferPrice"),
                        MatchingFillingRate = req.Field<decimal?>("MatchingFillingRate"),
                        LowestOfferMatchingPrice = req.Field<double?>("LowestOfferMatchingPrice"),
                        POTotalAmount = req.Field<double?>("POTotalAmount"),
                        POWinStatus = req.Field<string>("POWinStatus"),
                        Differnece = req.Field<decimal?>("Difference")
                      }).ToList();
                     supplierReport.TotalSupplierOffers = Convert.ToInt32(dt.Tables[3].Rows[0]["TotalSupplierOffers"]);
        }

                return supplierReport;
              }
              catch (Exception ex)
              {
                throw new Exception(ex.Message);
              }
        }

        #endregion

        #region GetAutomotivePartLog
        public List<AutomotivePartLog> GetAutomotivePartLog(int AutomotivePartID)
        {
            DataSet dt = new DataSet();
            try
            {
                List<AutomotivePartLog> AutomotivePartLogData;

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@AutomotivePartID" , Value = AutomotivePartID},
                };



                using (dt = ADOManager.Instance.DataSet("[GetAutomotivePartLog]", CommandType.StoredProcedure, sParameter))
                {

                    AutomotivePartLogData = dt.Tables[0].AsEnumerable().Select(ampl => new AutomotivePartLog
                    {
                        AutomotivePartID = ampl.Field<int>("AutomotivePartID"),
                        PartName = ampl.Field<string>("PartName"),
                        CreatedByName = ampl.Field<string>("CreatedByName"),
                        CreatedOn = ampl.Field<DateTime?>("CreatedOn"),
                        StatusID = ampl.Field<short?>("StatusID"),
                        ApprovedByName = ampl.Field<string>("ApprovedByName"),
                        ApprovedDateTime = ampl.Field<DateTime?>("ApprovedDateTime"),
                        RejectedByName = ampl.Field<string>("RejectedByName"),
                        RejectedDateTime = ampl.Field<DateTime?>("RejectDateTime"),
                        EditedBy = ampl.Field<string>("EditedBy"),
                        EditedDateTime = ampl.Field<DateTime?>("EditDateTime")
                    }).ToList();


                    return AutomotivePartLogData;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    #endregion

        #region GetAllRequestsByAccidentID
    public Object GetAllRequestsByAccidentID(int AccidentID)
    {
      DataSet dt = new DataSet();
      try
      {
        var allrequests = new List<Request>();
        var allrequestedpart = new List<RequestedPart>();
        var accident = new Accident();
        var allrequesttask = new List<RequestTask>();
        var allorderedparts = new List<OrderedParts>();
        var allnotes = new List<Note>();
        var sParameter = new List<SqlParameter>
        {
          new SqlParameter { ParameterName = "@AccidentID", Value = AccidentID }

        };

        using (dt = ADOManager.Instance.DataSet("[getAllRequestsbyaccidentID]", CommandType.StoredProcedure, sParameter))
        {
          accident = dt.Tables[0].AsEnumerable().Select(acci=> new Accident 
            {
            AccidentID = acci.Field<int>("AccidentID"),
            AccidentNo = acci.Field<string>("AccidentNo"),
            MakeID = acci.Field<int>("MakeID"),
            ModelID = acci.Field<int>("ModelID"),
            ProductionYear = acci.Field<Int16>("ProductionYear"),
            VIN = acci.Field<string>("VIN"),
            MakeName = acci.Field<string>("EnglishMakeName"),
            ArabicMakeName = acci.Field<string>("ArabicMakeName"),
            ModelCode = acci.Field<string>("EnglishModelName"),
            ArabicModelName = acci.Field<string>("ArabicModelName"),
            YearCode = acci.Field<int>("YearCode"),
            WorkshopName = acci.Field<string>("WorkshopName"),
            VehicleOwnerName = acci.Field<string>("VehicleOwnerName"),
            ImgURL = acci.Field<string>("ImgURL"),
            POAmount = acci.Field<double?>("TotalPO"),
            TotalLabourCost = acci.Field<double?>("Labour"),
            CreatedByName = acci.Field<string>("AccidentCreatedByName"),
            PlateNo = acci.Field<string>("PlateNo"),
            CreatedOn = acci.Field<DateTime>("AccidentCreatedOn"),
            ImportantNote = acci.Field<string>("ImportantNote")
            

          }).FirstOrDefault();



          allrequests = dt.Tables[1].AsEnumerable().Select(req => new Request
          {
            RequestID = req.Field<int>("RequestID"),
            DemandID = req.Field<int?>("DemandID"),
            MakeID = req.Field<int>("MakeID"),
            ModelID = req.Field<int>("ModelID"),
            ProductionYear = req.Field<Int16>("ProductionYear"),
            CompanyID = req.Field<int>("CompanyID"),
           //TotalQuotations = req.Field<int>("TotalQuotations"),
            VIN = req.Field<string>("VIN"),
            CreatedOn = req.Field<DateTime>("CreatedOn"),
            MakeName = req.Field<string>("EnglishMakeName"),
            ArabicMakeName = req.Field<string>("ArabicMakeName"),
            ModelCode = req.Field<string>("EnglishModelName"),
            ArabicModelName = req.Field<string>("ArabicModelName"),
            YearCode = req.Field<int>("YearCode"),
            CreatedSince = req.Field<string>("CreatedSince"),
            CreatedSinceArabic = req.Field<string>("CreatedSinceArabic"),
            AccidentID = req.Field<int?>("AccidentID"),
            AccidentNo = req.Field<string>("AccidentNo"),
            WorkshopName = req.Field<string>("WorkshopName"),
            WorkshopCityName = req.Field<string>("WorkshopCityName"),
            WorkshopPhone = req.Field<string>("WorkshopPhone"),
            WorkshopAreaName = req.Field<string>("WorkshopAreaName"),
            ImgURL = req.Field<string>("ImgURL"),
            CPPhone = req.Field<string>("CPPhone"),
            ICName = req.Field<string>("Name"),
            CPName = req.Field<string>("CPName"),
            StatusID = req.Field<Int16>("StatusID"),
            StatusName = req.Field<string>("StatusName"),
            PlateNo = req.Field<string>("PlateNo"),
            RequestNumber = req.Field<int?>("RequestNumber"),
            BiddingHours = req.Field<double?>("BiddingHours"),
            //BiddingDateTime = req.Field<DateTime>("BiddingDateTime"),
            //IsBiddingTimeExpired = req.Field<int>("IsBiddingTimeExpired"),
            VehicleOwnerName = req.Field<string>("VehicleOwnerName"),
            SerialNo = req.Field<int?>("SerialNo"),
            POTotalAmount = req.Field<double?>("POTotalAmount"),
            TotalRequestCount = req.Field<int?>("RequestRowNumber"),
            TotalNotAvailableQuotations = req.Field<int?>("TotalNotAvailableQuotations"),
            ClearanceRoute = req.Field<string>("ClearanceRoute"),
            IsPurchasing = req.Field<bool?>("IsPurchasing"),
            POPdfUrl = req.Field<string>("POPdfUrl"),
            SupplierName = req.Field<string>("SupplierName"),
            SupplierPhone = req.Field<string>("SupplierPhone"),
            ReturnReason = req.Field<string>("ReturnReason"),
            ROPdfURL = req.Field<string>("ROPdfURL"),
            JCSeriesID = req.Field<string>("JCSeriesCode"),
            CreatedByName = req.Field<string>("CreatedByName"),
            ArabicStatusName = req.Field<string>("ArabicStatusName"),
            LabourPrice = req.Field<double?>("LabourPrice")

          }).ToList();

          allrequestedpart = dt.Tables[2].AsEnumerable().Select(requestpart => new RequestedPart
          {
            RequestedPartID = requestpart.Field<int>("RequestedPartID"),
            RequestID = requestpart.Field<int>("RequestID"),
            DemandID        = requestpart.Field<int?>("DemandID"),
            AutomotivePartID  = requestpart.Field<int>("AutomotivePartID"),
            //ConditionTypeID = requestpart.Field<Int16?>("ConditionTypeID"),
            AutomotivePartName = requestpart.Field<string>("AutomotivePartName"),
            DamageName = requestpart.Field<string>("DamageName"),
            Quantity = requestpart.Field<int>("Quantity"),
            IsPartApproved = requestpart.Field<bool?>("IsPartApproved")
            //NoteInfo = requestpart.Field<string>("NoteInfo"),
            //ApprovedByName = requestpart.Field<string>("ApprovedByName"),
            //CreatedByName = requestpart.Field<string>("CreatedByName"),

          }).ToList();
          allorderedparts = dt.Tables[3].AsEnumerable().Select(orderpart => new OrderedParts
          {
            AutomotivePartID = orderpart.Field<int?>("AutomotivePartID"),
            RequestID = orderpart.Field<int?>("RequestID"),
            RequestedPartID = orderpart.Field<int?>("RequestedPartID"),
            PartName = orderpart.Field<string>("PartName"),
            Price = orderpart.Field<double?>("Price"),
            Quantity = orderpart.Field<int>("Quantity")
          }).ToList();
        }

        allrequesttask = dt.Tables[4].AsEnumerable().Select(requesttask=> new RequestTask {
            RequestTaskID = requesttask.Field<int>("RequestTaskID"),
            RequestID     = requesttask.Field<int>("RequestID"),
            TaskDescription = requesttask.Field<string>("TaskDescription"),
            LabourPrice = requesttask.Field<double?>("LabourPrice"),
            LabourTime = requesttask.Field<int>("LabourTime"),
            TaskRejectReason = requesttask.Field<string>("TaskRejectReason")
          }).ToList();

        allnotes = dt.Tables[5].AsEnumerable().Select(notes => new Note {
          AccidentID = notes.Field<int>("AccidentID"),
          TextValue = notes.Field<string>("TextValue"),
          IsPublic = notes.Field<bool>("IsPublic")
        }).ToList();

        Object alldata = new {accident = accident, requests = allrequests, requestedpart = allrequestedpart, requesttask = allrequesttask ,
          orderparts = allorderedparts, notes = allnotes};
        return alldata;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
        #endregion

        #region UpdateAccidentLimit
        public string UpdateAccidentLimit(int CompanyID, int? AccidentLimit)
        {
            try
            {
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@AccidentLimit" , Value = AccidentLimit},
                       
                    };

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[UpdateAccidentLimit]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? "Accident limit updated" : result.ToString();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    #endregion

        #region GetSupplierByModelID
    public List<PartInfo> GetSupplierByModelID(int MakeID,int ModelID2,int? ModelID1)
    {
      DataSet dt = new DataSet();
      try
      {
        List<PartInfo> partInfoData  = new List<PartInfo>();

        var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@MakeID" , Value = MakeID },
                        new SqlParameter { ParameterName = "@ModelID1" , Value = ModelID1 },
                        new SqlParameter { ParameterName = "@ModelID2" , Value = ModelID2 }


                };

        using (dt = ADOManager.Instance.DataSet("[getsupplierbymodelID]", CommandType.StoredProcedure, sParameter))
        {

          partInfoData = dt.Tables[0].AsEnumerable().Select(info => new PartInfo
          {
            SupplierID = info.Field<int?>("SupplierID"),
            SupplierName = info.Field<string>("SupplierName"),
            MakeID = info.Field<int>("MakeID"),
            ModelID = info.Field<int>("ModelID"),
/*            ProductionYearFrom = dm.Field<string>("ItemNumber"),
            ProductionYearTo = dm.Field<string>("OptionName"),*/
            IsConditionNew = info.Field<bool?>("IsConditionNew"),
            IsConditionUsed = info.Field<bool?>("IsConditionUsed"),
            IsApproved = info.Field<Int16>("IsApproved"),
            IsConditionOriginal = info.Field<bool?>("IsConditionOriginal"),
            IsConditionAfterMarket = info.Field<bool?>("IsConditionAfterMarket"),
            IsCheck = info.Field<bool?>("IsCheck")



          }).ToList();

        }
        return partInfoData;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    #endregion

        #region AddSupplier
    public string AddSupplier(List<PartInfo> partInfo)
    {
      try
      {
        var XMLPartInfo = partInfo.ToXML("ArrayofPartInfo");
        var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@XMLPartInfo" , Value = XMLPartInfo},
  
                    };

        //var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveDemand]", CommandType.StoredProcedure, sParameter));
        //return result > 0 ? "Demand saved successfully" : DataValidation.dbError;

        var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[AddSupllier]", CommandType.StoredProcedure, sParameter));
        return result > 0 ? "true":"false";
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }
        #endregion

        #region CreateAutomotivepartMultiDamagePoints
        public string CreateAutomotivepartMultiDamagePoints(AutomotivePart automotivePart)
        {
            try
            {
                var XMLDamagePoint = automotivePart.DamagePoint.ToXML("ArrayofDamagePoint");
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@XMLDamagePoint" , Value =  XMLDamagePoint},
                        new SqlParameter { ParameterName = "@ItemNumber" , Value =  automotivePart.ItemNumber},
                        new SqlParameter { ParameterName = "@Name1" , Value =  automotivePart.Name1},
                        new SqlParameter { ParameterName = "@OptionName" , Value =  automotivePart.OptionName},
                        new SqlParameter { ParameterName = "@OptionNumber" , Value =  automotivePart.OptionNumber},
                        new SqlParameter { ParameterName = "@FinalCode" , Value =  automotivePart.FinalCode},
                        new SqlParameter { ParameterName = "@PartName" , Value =  automotivePart.PartName},
                        new SqlParameter { ParameterName = "@Name2" , Value =  automotivePart.Name2},
                        new SqlParameter { ParameterName = "@Name3" , Value =  automotivePart.Name3},
                        new SqlParameter { ParameterName = "@Name4" , Value =  automotivePart.Name4},
                        new SqlParameter { ParameterName = "@Name5" , Value =  automotivePart.Name5},
                        new SqlParameter { ParameterName = "@Name6" , Value =  automotivePart.Name6},
                        new SqlParameter { ParameterName = "@Name7" , Value =  automotivePart.Name7},
                        new SqlParameter { ParameterName = "@Name8" , Value =  automotivePart.Name8},
                        new SqlParameter { ParameterName = "@CreatedBy" , Value =  automotivePart.CreatedBy},
                        

                };
                var result = ADOManager.Instance.ExecuteScalar("[CreateAutomotivepartMultiDamagePoints]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetMultiAutomotiveParts
        public Object GetMultiAutomotiveParts(string Name1, string FinalCode)
        {
            DataSet dt = new DataSet();
            try
            {
                AutomotivePart automotivePart = new AutomotivePart();


                var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@Name1" , Value =  Name1},
                        new SqlParameter { ParameterName = "@FinalCode" , Value = FinalCode },
                     


                };

                using (dt = ADOManager.Instance.DataSet("[GetMultiAutomotivePart]", CommandType.StoredProcedure, sParameter))
                {

                    automotivePart = dt.Tables[0].AsEnumerable().Select(info => new AutomotivePart
                    {
                        Name1 = info.Field<string>("Name1"),
                        FinalCode = info.Field<string>("FinalCode"),
                        OptionName = info.Field<string>("OptionName"),
                        OptionNumber = info.Field<string>("OptionNumber"),
                        ItemNumber = info.Field<string>("ItemNumber"),
                        PartName = info.Field<string>("PartName"),
                        Name2 = info.Field<string>("Name2"),
                        Name3 = info.Field<string>("Name3"),
                        Name4 = info.Field<string>("Name4"),
                        Name5 = info.Field<string>("Name5"),
                        Name6 = info.Field<string>("Name6"),
                        Name7 = info.Field<string>("Name7"),
                        Name8 = info.Field<string>("Name8")
                    }).FirstOrDefault();

                    automotivePart.DamagePoint = dt.Tables[1].AsEnumerable().Select(dm => new DamagePoint
                    {
                        DamagePointID = dm.Field<int>("DamagePointID"),
                        PointName = dm.Field<string>("PointName"),
                        PointNameArabic = dm.Field<string>("PointNameArabic")
                      
                    }).ToList();

                }
                var data = new { automotivePart = automotivePart };
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateMultiAutomotiveParts
        public string UpdateMultiAutomotiveParts(AutomotivePart automotivePart)
        {
            try
            {
                var XMLDamagePoint = automotivePart.DamagePoint.ToXML("ArrayOfDamagePoints");
                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@DamagePoints" , Value =  XMLDamagePoint},
                        new SqlParameter { ParameterName = "@ItemNumber" , Value =  automotivePart.ItemNumber},
                        new SqlParameter { ParameterName = "@Name1" , Value =  automotivePart.Name1},
                        new SqlParameter { ParameterName = "@OptionName" , Value =  automotivePart.OptionName},
                        new SqlParameter { ParameterName = "@OptionNumber" , Value =  automotivePart.OptionNumber},
                        new SqlParameter { ParameterName = "@FinalCode" , Value =  automotivePart.FinalCode},
                        new SqlParameter { ParameterName = "@PartName" , Value =  automotivePart.PartName},
                        new SqlParameter { ParameterName = "@Name2" , Value =  automotivePart.Name2},
                        new SqlParameter { ParameterName = "@Name3" , Value =  automotivePart.Name3},
                        new SqlParameter { ParameterName = "@Name4" , Value =  automotivePart.Name4},
                        new SqlParameter { ParameterName = "@Name5" , Value =  automotivePart.Name5},
                        new SqlParameter { ParameterName = "@Name6" , Value =  automotivePart.Name6},
                        new SqlParameter { ParameterName = "@Name7" , Value =  automotivePart.Name7},
                        new SqlParameter { ParameterName = "@Name8" , Value =  automotivePart.Name8},
                        new SqlParameter { ParameterName = "@ModifiedBy" , Value =  automotivePart.CreatedBy},
                        new SqlParameter { ParameterName = "@OldName1" , Value =  automotivePart.OldName1},
                        new SqlParameter { ParameterName = "@OldFinalCode" , Value =  automotivePart.OldFinalCode},


                };
                var result = ADOManager.Instance.ExecuteScalar("[UpdateMultiAutomotivePart]", CommandType.StoredProcedure, sParameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    #endregion

        #region RemoveSeries
    public string RemoveSeries(SeriesCase SeriesCase)
    {
      DataSet dt = new DataSet();
      try
      {
        var XMLJoClaimSeries = SeriesCase.seriesremoved.ToXML("ArrayofJoClaims");
        var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@JCSeriesID", Value= SeriesCase.JoClaimsSeriesData.JCSeriesID},
                        new SqlParameter { ParameterName = "@XMLJoClaimSeries", Value= XMLJoClaimSeries},

                };
        var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[RemoveSeries]", CommandType.StoredProcedure, sParameter));


        return result > 0 ? "Successfully Updated" : "Data is not updated";

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
        #endregion

        #region CreateCase
        public string CreateCase(SeriesCase SeriesCase)
        {
            DataSet dt = new DataSet();
            try
            {

                var XMLMapExistingSeries = SeriesCase.MapExistingSeries.ToXML("ArrayOfMapExistingSeries");
                var XMLCurrentseries = SeriesCase.MapCurrentSeries.ToXML("ArrayOfMapCurrentSeries");
                var XMLSelfseries = SeriesCase.SelfSeriesCases.ToXML("ArrayOfSelfSeries");
                var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@SeriesID", Value= SeriesCase.JoClaimsSeriesData.JCSeriesID},
                         //new SqlParameter { ParameterName = "@OverLappingYear", Value= SeriesCase.SelfSeriesCase.OverLappingYear},
                         //new SqlParameter { ParameterName = "@FuelType", Value= SeriesCase.SelfSeriesCase.FuelType},
                         //new SqlParameter { ParameterName = "@BodyType", Value= SeriesCase.SelfSeriesCase.BodyType},
                         //new SqlParameter { ParameterName = "@CarImageEncryptedName", Value= SeriesCase.SelfSeriesCase.CarImageEncryptedName},
                        new SqlParameter { ParameterName = "@XMLMapExistingSeries", Value= XMLMapExistingSeries},
                        new SqlParameter { ParameterName = "@XMLCurrentseries", Value= XMLCurrentseries},
                        new SqlParameter { ParameterName = "@XMLSelfseries", Value= XMLSelfseries},

                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[CreateCases]", CommandType.StoredProcedure, sParameter));


                return result > 0 ? "true" : "false";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateSeriesCases
        public string UpdateSeriesCases(SeriesCase SeriesCase)
        {
            DataSet dt = new DataSet();
            try
            {

                var XMLMapExistingSeries = SeriesCase.MapExistingSeries.ToXML("ArrayOfMapExistingSeries");
                var XMLCurrentseries = SeriesCase.MapCurrentSeries.ToXML("ArrayOfMapCurrentSeries");
                var XMLSelfseries = SeriesCase.SelfSeriesCases.ToXML("ArrayOfSelfSeries");
                var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@JCSeriesID", Value= SeriesCase.JoClaimsSeriesData.JCSeriesID},
                         //new SqlParameter { ParameterName = "@OverLappingYear", Value= SeriesCase.SelfSeriesCase.OverLappingYear},
                         //new SqlParameter { ParameterName = "@FuelType", Value= SeriesCase.SelfSeriesCase.FuelType},
                         //new SqlParameter { ParameterName = "@BodyType", Value= SeriesCase.SelfSeriesCase.BodyType},
                         //new SqlParameter { ParameterName = "@CarImageEncryptedName", Value= SeriesCase.SelfSeriesCase.CarImageEncryptedName},
                         new SqlParameter { ParameterName = "@XMLMapExistingSeries", Value= XMLMapExistingSeries},
                         new SqlParameter { ParameterName = "@XMLCurrentseries", Value= XMLCurrentseries},
                         new SqlParameter { ParameterName = "@XMLSelfseries", Value= XMLSelfseries},

                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[UpdateCaseInSeries]", CommandType.StoredProcedure, sParameter));


                return result > 0 ? "true" : "false";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region UpdateSeriesCases
        public string DeleteRejectedCase(string JCSeriesID)
        {
            DataSet dt = new DataSet();
            try
            {


                var sParameter = new List<SqlParameter>
                {
                         new SqlParameter { ParameterName = "@JCSeriesID", Value= JCSeriesID}
                       

                };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[DeleteRejectedCase]", CommandType.StoredProcedure, sParameter));


                return result > 0 ? "true" : "false";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    #endregion

        #region getmakesandmodels
    public Object getmakesandmodels()
    {
      DataSet dt = new DataSet();
      try
      {
        var Makes = new List<Make>();
        var Models = new List<Model>();

        using (dt = ADOManager.Instance.DataSet("[getMakeAndModel]", CommandType.StoredProcedure))
        {

          Makes = dt.Tables[0].AsEnumerable().Select(make => new Make
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

          Models = dt.Tables[1].AsEnumerable().Select(model => new Model
          {
            MakeID = model.Field<int>("MakeID"),
            ModelID = model.Field<int>("ModelID"),
            ModelCode = model.Field<string>("EnglishModelName"),
            ArabicModelName = model.Field<string>("ArabicModelName"),
            GroupName = model.Field<string>("GroupName"),
            MakeName = model.Field<string>("EnglishMakeName"),
            ArabicMakeName = model.Field<string>("ArabicMakeName"),
            ImgURL = model.Field<string>("ImgURL"),
            AccidentCount = model.Field<int>("AccidentCount")

          }).ToList();

        }
        Object Response = new { Makes = Makes, Models = Models };
        return Response;
      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }
    }
    #endregion

        # region deleteModelAndReplace
    public bool deleteModelAndReplace(int MakeID, int DeleteModelID, int ReplaceModelID)
    {
      DataSet dt = new DataSet();
      try
      {
        var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@MakeID" , Value = MakeID},
                        new SqlParameter { ParameterName = "@DeleteModelID" , Value = DeleteModelID},
                        new SqlParameter { ParameterName = "@ReplaceModelID" , Value = ReplaceModelID}
                    };

        var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[DeleteAndReplaceModel]", CommandType.StoredProcedure, sParameter));

        return result > 0 ? true : false; 

      }
      catch (Exception ex)
      {
        throw new Exception(ex.Message);
      }

    }
        #endregion

        #region restoreAccount
        public bool restoreAccount(int UserID)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@UserID" , Value = UserID  },

                };
                int result = Convert.ToInt32( ADOManager.Instance.ExecuteScalar("[RestoreAccount]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region ResetPasswordOthers

        public bool ResetPasswordOthers(string UserID, string Password)
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

                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[resetPasswordOthers]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true : false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion


        #region getCarReadyTaskDetail
        public List<RequestTask> getCarReadyTaskDetail(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                var requestTask = new List<RequestTask>();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };
                using (dt = ADOManager.Instance.DataSet("[getCarReadyTaskDetail]", CommandType.StoredProcedure, sParameter))
                {
                    requestTask = dt.Tables[0].AsEnumerable().Select(cmp => new RequestTask
                    {
                        RequestTaskID = cmp.Field<int>("RequestTaskID"),
                        TaskName = cmp.Field<string>("TaskName"),
                        TaskDescription = cmp.Field<string>("TaskDescription"),
                        TaskTypeID = cmp.Field<Int16>("TaskTypeID"),
                        RequestID = cmp.Field<int>("RequestID")


                    }).ToList();

                }
                return requestTask;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region getCarReadyImages
        public Object getCarReadyImages(int RequestID)
        {
            DataSet dt = new DataSet();
            try
            {
                Object RequestTaskImages;
                var requestTask = new List<RequestTask>();
                var imageRequestTask = new List<Image>();
                var request = new Request();
                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@RequestID" , Value = RequestID},
                    };
                using (dt = ADOManager.Instance.DataSet("[getCarReadyImages]", CommandType.StoredProcedure, sParameter))
                {
                    request = dt.Tables[0].AsEnumerable().Select(req => new Request
                    {
                        RequestID = req.Field<int>("RequestID"),
                        IsAgencyRequest = req.Field<int?>("IsAgencyRequest"),
                        TotalPartLabourPrice = req.Field<double?>("TotalPartLabourPrice"),
                        TotalCost = req.Field<double?>("TotalCost"),
                        IsEnterLabourPartPriceChecked = req.Field<int?>("IsEnterLabourPartPriceChecked")
                    }).FirstOrDefault();
                    requestTask = dt.Tables[1].AsEnumerable().Select(cmp => new RequestTask
                    {
                        RequestTaskID = cmp.Field<int>("RequestTaskID"),
                        TaskName = cmp.Field<string>("TaskName"),
                        TaskDescription = cmp.Field<string>("TaskDescription"),
                        TaskTypeID = cmp.Field<Int16>("TaskTypeID"),
                        RequestID = cmp.Field<int>("RequestID")
                    }).ToList();
                    imageRequestTask = dt.Tables[2].AsEnumerable().Select(cmp => new Image
                    {
                        ImageID = cmp.Field<int?>("ImageID"),
                        ImageURL = cmp.Field<string>("ImageURL"),
                        ObjectID = cmp.Field<int?>("ObjectID"),
                        ObjectTypeID = cmp.Field<Int16?>("ObjectTypeID"),
                        EncryptedName = cmp.Field<string>("EncryptedName"),
                        RequestTaskId = cmp.Field<int?>("RequestTaskID"),
                        IsDeleted = cmp.Field<bool>("IsDeleted")



                    }).ToList();
                    RequestTaskImages = new { Request = request ,RequestTask = requestTask, Images = imageRequestTask };

                }
                return RequestTaskImages;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region GetAllWorkshops
        public List<Workshop> getAllWorkShopForAdmin(int? CompanyID , int? CountryID)
        {
            DataSet dt = new DataSet();

            if(CompanyID == 0)
            {
                CompanyID = null;
            }
            try
            {
                var workshops = new List<Workshop>();
                var sParameter = new List<SqlParameter>
                    {
                         new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                         new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}

                    };

                using (dt = ADOManager.Instance.DataSet("[getAllWorkShopForAdmin]", CommandType.StoredProcedure, sParameter))
                {
                    workshops = dt.Tables[0].AsEnumerable().Select(req => new Workshop
                    {
                        
                        WorkshopID = req.Field<int>("WorkshopID"),
                        WorkshopName = req.Field<string>("WorkshopName")
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

        #region ChangePriceEstimateStatus
        public bool ChangePriceEstimateStatus(int companyID, bool IsPriceEstimate, int userID)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@companyID" , Value = companyID},
                        new SqlParameter { ParameterName = "@IsPriceEstimate" , Value = IsPriceEstimate},
                        new SqlParameter { ParameterName = "@UserID" , Value = userID},
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[ChangePriceEstimateStatus]", CommandType.StoredProcedure, sParameter));
                return result > 0 ? true: false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region ChangeFeatureApproval
        public int ChangeFeatureApproval(FeaturePermission featurePermission)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@companyID" , Value = featurePermission.CompanyID},
                        new SqlParameter { ParameterName = "@FeaturePermissionsID" , Value = featurePermission.FeaturePermissionsID},
                        new SqlParameter { ParameterName = "@FeatureID" , Value = featurePermission.FeatureID},
                        new SqlParameter { ParameterName = "@IsApproved" , Value = featurePermission.IsApproved},
                        new SqlParameter { ParameterName = "@UserID" , Value = featurePermission.UserID},
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[ChangeFeatureApproval]", CommandType.StoredProcedure, sParameter));
                return result; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region GetCompanyStats
        public Object GetCompanyStats(int? CompanyID, string StartDate, string EndDate, int? PageNo, int? CountryID)
        {
            DataSet dt = new DataSet();
            try
            {

                List<CompanyStats> companyStats = new List<CompanyStats>();
                int PageCount;

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@CountryID" , Value = CountryID}

                    };
                using (dt = ADOManager.Instance.DataSet("[getCompanyStats]", CommandType.StoredProcedure, sParameter))
                {
                    companyStats = dt.Tables[0].AsEnumerable().Select(cs => new CompanyStats
                    {
                        CompanyID = cs.Field<int>("CompanyID"),
                        CompanyName = cs.Field<string>("CompanyName"),
                        AccidentWithrequestCount = cs.Field<int?>("AccidentWithrequestCount"),
                        RequestedPartsCount = cs.Field<int?>("RequestedPartsCount"),
                        CompanyRate = cs.Field<int?>("CompanyRate"),
                        TotalAccidentCount = cs.Field<int?>("TotalAccidentCount"),
                        DuplicateAccidentCount = cs.Field<int?>("DuplicateAccidentCount"),
                        AccidentWithZeroQuotation = cs.Field<int?>("AccidentWithZeroQuotation"),
                        AccidentWithoutRequest = cs.Field<int?>("AccidentWithoutRequest"),
                        AccidentWithUnpublishRequest = cs.Field<int?>("AccidentWithUnpublishRequest")


                    }).ToList();

                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);

                    Object data = new { companyStats = companyStats, PageCount = PageCount };


                    return data;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region saveCompanyRateValue
        public int saveCompanyRateValue(int CompanyRateValue, int CompanyID)
        {
            DataSet dt = new DataSet();
            try
            {

                var sParameter = new List<SqlParameter>
                {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@CompanyRateValue" , Value = CompanyRateValue}
                    };
                var result = Convert.ToInt32(ADOManager.Instance.ExecuteScalar("[saveCompanyRateValue]", CommandType.StoredProcedure, sParameter));
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region getCompanyDetailedStatistics
        public Object getCompanyDetailedStatistics(int? CompanyID, string StartDate, string EndDate, int? PageNo)
        {
            DataSet dt = new DataSet();
            try
            {

                List<Accident> accidents = new List<Accident>();
                int PageCount;

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID},
                        new SqlParameter { ParameterName = "@StartDate" , Value = StartDate},
                        new SqlParameter { ParameterName = "@EndDate" , Value = EndDate},
                        new SqlParameter { ParameterName = "@PageNo" , Value = PageNo}

                    };
                using (dt = ADOManager.Instance.DataSet("[getCompanyDetailedStatistics]", CommandType.StoredProcedure, sParameter))
                {
                    accidents = dt.Tables[0].AsEnumerable().Select(cs => new Accident
                    {
                        AccidentID = cs.Field<int>("AccidentID"),
                        AccidentNo = cs.Field<string>("AccidentNo"),
                        EnglishMakeName = cs.Field<string>("EnglishMakeName"),
                        ArabicMakeName = cs.Field<string>("ArabicMakeName"),
                        EnglishModelName = cs.Field<string>("EnglishModelName"),
                        ArabicModelName = cs.Field<string>("ArabicModelName"),
                        CreatedOn = cs.Field<DateTime>("CreatedOn"),
                        PublishedRequestCount = cs.Field<int?>("PublishedRequestCount"),
                        UnPublishedRequestCount = cs.Field<int?>("UnPublishedRequestCount"),
                        QuotationCount = cs.Field<int?>("QuotationCount"),
                        MatchingOffer = cs.Field<int?>("MatchingOffer"),
                        VIN = cs.Field<string>("VIN"),
                        CompanyID = cs.Field<int?>("CompanyID"),
                        CompanyName = cs.Field<string>("Name"),
                        RequestCount = cs.Field<int?>("requestCount"),
                        RequestPartCount = cs.Field<int?>("RequestPartCount"),
                        AccidentTypeName = cs.Field<string>("TypeName"),
                        ArabicAccidentTypeName = cs.Field<string>("ArabicTypeName"),
                        NotMatchingOffers = cs.Field<int?>("NotMatchingOffers")


                    }).ToList();

                    PageCount = Convert.ToInt32(dt.Tables[1].Rows[0]["TotalCount"]);

                    Object data = new { AccidentDetail = accidents, PageCount = PageCount };


                    return data;

                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region getFeaturesPermissions
        public List<FeaturePermission> getFeaturesPermissions(int CompanyID)
        {
            DataSet dt = new DataSet();
            try
            {

                List<FeaturePermission> featurepermission = new List<FeaturePermission>();

                var sParameter = new List<SqlParameter>
                    {
                        new SqlParameter { ParameterName = "@CompanyID" , Value = CompanyID}

                    };
                using (dt = ADOManager.Instance.DataSet("[getFeaturesPermissions]", CommandType.StoredProcedure, sParameter))
                {
                    featurepermission = dt.Tables[0].AsEnumerable().Select(fp => new FeaturePermission
                    {
                        FeaturePermissionsID = fp.Field<int?>("FeaturePermissionsID"),
                        FeatureID = fp.Field<int?>("FeatureID"),
                        IsApproved = fp.Field<bool>("IsApproved"),
                        IsDeleted = fp.Field<bool>("IsDeleted"),
                        CompanyID = fp.Field<int?>("CompanyID"),
                        FeatureName = fp.Field<string>("FeatureName"),
                        FeatureNameArabic = fp.Field<string>("FeatureNameArabic")


                    }).ToList();

                    return featurepermission;

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
