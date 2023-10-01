using MODEL.Models;
using MODEL.Models.Report.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface IShubeddakManager
    {
        Object GetInsuranceCompanies(int? CountryID);
        Companyrequests GetAllRequests(DateTime? StartDate, DateTime? EndDate, int? MakeID,
   int? ModelID, int? YearID, string SearchQuery, int PageNo, int StatusID, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate, int? ICWorkshopID , int? CompanyTypeID,int? CountryID);
        Companyrequests GetRequestsHistory();

        string UpdateCompanyStatus(int CompanyID, Int16 StatusID, string CompanyCode, int ModifiedBy);
        List<Supplier> GetPartShopes(PSFilter psFilter);
        string UpdatePartshopStatus(int SupplierID, Int16 StatusID, string RejectNote, int ModifiedBy);
        string SaveDemand(RequestData request);
        string UpdateDemand(RequestData request);
        //Companyrequests GetAllDemands();
        DemandQuotations GetQuotations(int DemandID);
        DemandQuotations GetQuotationsByFilter(QuotationFilterModel model);
        DemandProfile GetDemand(int DemandID);
        List<Quotation> SaveReferredQuotationParts(int UserID, List<QuotationPart> referredQuotationParts, int tabId);
        ShubeddakDashboard GetShubeddakDashboard();

        List<Supplier> GetReferredSupplier(int RequestID);

        string OnApprovePart(int PartInfoID, Int16 StatusID, int ModifiedBy);
        string OnApproveUvPart(int UniversalPartID, Int16 StatusID, int ModifiedBy);
        string OnApproveOrder(int RequestID, int DemandID, int ModifiedBy, int CountDownTime, int POApprovalID, bool IsApproved);
        string AcceptQuotationPart(int QuotationPartID, bool IsAccepted, int ModifiedBy, string AdminRejectNote);
        string OnReviewQuotation(int QuotationID, string ReviewNote, int ModifiedBy, int SupplierID, int RequestID);
        string CancelPurchaseOrder(int RequestID, Int16 StatusID, int ModifiedBy, string RejectNote);
        string UndoCancelPurchaseOrder(int RequestID, int ModifiedBy, int QuotationID, int DemandID, int? QuotationPartID);
        string OnApproveAllParts(int SupplierID, Int16 StatusID, int ModifiedBy);
        string OnApproveAllUvParts(Int16 StatusID, int ModifiedBy);
        List<Supplier> GetQualifiedSuppliers(int RequestID);
        RequestSuppliers GetRequestQualifiedSuppliers();
        string SaveQuotationComment(string Comment, int QuotationID, int UserID);
        string StopBiddingHours(int RequestID, int ModifiedBy);
        string UpdateSupplierOffersStatus(int QuotationID, bool IsAccepted, int ModifiedBy, string AdminRejectNote);
        Companyrequests GetHistoryRequests(int StartRow, int RowsPerPage, int? MakeID, int? ModelID, int? YearID, string searchQuery, int? CountryID);
        List<Company> GetJoAccidentMeta(int? CountryID);
        RequestAllOffersData GetRequestAllOffers(int RequestID);
        string PrintOffersPdf(PdfData pdfData);
        string UnPublishPartsRequest(int RequestID, int ModifiedBy, string ReturnReason);
        List<Workshop> GetWorkshops(int? CountryID);
        string UpdateWorkshopStatus(int WorkshopID, Int16 StatusID, int ModifiedBy);
        PartsMeta GetParts(int StatusID, int? DamagePointID, string SearchQuery, int PageNo, int? CountryID);
        string UpdatePartInfo(AutomotivePart automotivePart);
        PartsMeta GetPartsDetails(AutomotivePart automotivePart);
        List<ShubeddakUser> GetAdminUsers(int UserID, int? countryID);
        CommonMeta GetAdminMeta(int? UserID, int? ShubeddakUserID);
        ShubeddakUser SaveAdminUser(ShubeddakUser user);
        string DeleteAdminUser(int UserID, int ModifiedBy);
        string UpdateAdminUser(ShubeddakUser User);
        List<Request>GetRequestsByPart(int AutomotivePartID);
        Object SaveMake(string MakeName, int UserID, string ImgURL, int? MakeID, string ArabicMakeName);
        Object SaveModel(string ModelCode, int UserID, int MakeID, int? ModelID, string GroupName,string ArabicModelName);
        List<Request> GetModelAllAccident(int ModelID, int UserID);
        string AcceptAndRejectHybridIC(int WorkshopID,bool IsCompanyApproved,int ModifiedBy);
        SupplierReport GetSupplierReport(int SupplierID, DateTime StartDate, DateTime EndDate,
          int? PageNo);
        Object GetAccidentBySeries(string JCSeriesID, int PageNo);

        List<AutomotivePartLog> GetAutomotivePartLog(int AutomotivePartID);
        Object GetAllRequestsByAccidentID(int AccidentID);

        string UpdateAccidentLimit(int CompanyID, int? AccidentLimit);

        List<PartInfo> GetSupplierByModelID(int MakeID, int ModelID2, int? ModelID1);

        string AddSupplier(List<PartInfo> PartInfo);
        string CreateAutomotivepartMultiDamagePoints(AutomotivePart automotivePart);
        Object GetMultiAutomotiveParts(string Name1, string FinalCode);  
        string UpdateMultiAutomotiveParts(AutomotivePart automotivePart);
        string RemoveSeries(SeriesCase SeriesCase);
        string CreateCase(SeriesCase seriesCase);
        string UpdateSeriesCases(SeriesCase seriesCase);
        string DeleteRejectedCase(string JCSeriesID);
        Object getmakesandmodels();
        bool deleteModelAndReplace(int MakeID, int DeleteModelID, int ReplaceModelID);

        bool restoreAccount(int UserID);

        bool ResetPasswordOthers(string UserID, string Password);

        List<RequestTask> getCarReadyTaskDetail(int RequestID);

        Object getCarReadyImages(int RequestID);

        List<Workshop> getAllWorkShopForAdmin(int? CompanyID , int? CountryID);

        bool ChangePriceEstimateStatus(int companyID, bool IsPriceEstimate, int userID);

        int ChangeFeatureApproval(FeaturePermission featurePermission);

        Object GetCompanyStats(int? CompanyID, string StartDate, string EndDate, int? PageNo, int? CountryID);

        int saveCompanyRateValue(int CompanyRateValue, int CompanyID);

        Object getCompanyDetailedStatistics(int? CompanyID, string StartDate, string EndDate , int? PageNo);

        List<FeaturePermission> getFeaturesPermissions(int CompanyID);

        
    }
}
