using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Report.Common;
using MODEL.Models.Request_Draft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface ICompanyManager
    {
        AccidentMetaData GetAccidentMetaData(int CompanyID, int? WorkshopID);
        RequestMetaData GetRequestMetaData(int? CompanyID, string AccidentNo, int? AccidentID);
        CompanyProfile GetCompanyProfile(int CompanyID);
        string SaveCompanyProfile(Company company);
        RequestResponse SavePartsRequest(RequestData request);
        RequestResponse UpdatePartsRequest(RequestData request);
        string SaveAccident(AccidentData accident);
        string UpdateAccident(AccidentData accident);
        Companyrequests GetCompanyRequests(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID, int? ModelID, int? YearID,int? ICWorkshopID, string SearchQuery, DateTime? ApprovalStartDate, DateTime? ApprovalEndDate,
            int? SurveyorID);
        RequestData GetSingleRequest(int RequestID);
        string DeletePartsRequest(int RequestID, int ModifiedBy);
        AccidentMetaData GetCompanyAccidents(int CompanyID, Int16? AccidentTypeID, int? PageNo, int? StatusID, int? WorkshopID, DateTime? StartDate, DateTime? EndDate, int? MakeID,
   int? ModelID, int? YearID, string SearchQuery , int? TechnicalNotesStatusID, int? UserID, int? ICWorkshopID,int? SurveyorID);
        AccidentData GetSingleAccident(int AccidentID);
        string DeleteAccident(int AccidentID, int userID);
        DemandQuotations GetICRequestQuotationData(QuotationFilterModel model);
        string SaveOrderedParts(QuotationData quotationData);
        string CheckAccidentNo(string AccidentNo, int CompanyID);
        QuotationData GetCompanyInvoice(int RequestID, int DemandID);
        ICDashboard GetICDashboard( DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID, int? CompanyID, int? CountryID);
        List<Request> GetSupplierPODetails(int SupplierID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID);
        List<Request> GetWorkshopRODeatils(int WorkshopID, int CompanyID, DateTime StartDate, DateTime EndDate, int? MakeID, int? ModelID, Boolean? IsPurchasing, int? AccidentTypeID);
        string SaveWorkshop(ICWorkshop icWorkShop);
        string UpdateWorkshop(ICWorkshop icWorkShop);
        string DeleteWorkshop(int WorkshopID, int ModifiedBy);
        List<ICWorkshop> GetAllWorkshops(int CompanyID, int LoginUserID,int? StatusID);
        ICWorkshop GetSingleWorkshop(int WorkshopID, int LoginUserID);
        Employee SaveUser(Employee user);
        string UpdateUser(Employee User);

        List<Employee> GetAllUsers(int UserID, int LoginCompanyID);

        string DeleteUser(int UserID, int ModifiedBy);
        CommonMeta GetEmployeeMeta(int CompanyID, int? EmployeeID);
        string ClosePartsRequest(int RequestID, int ModifiedBy);

        string PublishPartsRequest(PublishRequest publishRequest);
        string ChangeRequestStatus(int RequestID, int ModifiedBy, int StatusID);
        List<QuotationPart> AcceptRequestedPart(QuotationPart  RejectedPart);
        string RecycleDemand(QuotationData quotationData);
        string UpdateAccidentStatus(int AccidentID, int StatusID, int ModifiedBy, string Reason);
        string ApprovePart(int RequestedPartID, bool IsPartApproved, int ModifiedBy, string PartRejectReason);
        string ReceiveRequestedPart(int QuotationPartID, int ModifiedBy, int RequestID);
        string ApproveTask(int RequestTaskID, bool IsTaskApproved, int ModifiedBy, string TaskRejectReason);
        string MarkRequestReady(int RequestID, int ModifiedBy);
        string StartCarRepair(int RequestID, int ModifiedBy);
        string RestorePartsRequest(int RequestID, int ModifiedBy);
        string SaveICUserProfile(Employee icEmployee);
        string OpenPartsRequest(int RequestID, int ModifiedBy, int? TempStatusID);
        QuotationSummary GetQuotationSummary(int DemandID, int UserID);
        ClearanceSummary GetClearanceSummary(int CompanyID, int AccidentID);
        string PrintQuotationSummaryPdf(PdfData pdfData);
        RequestData GetLatestRequest(int RequestID, int? WorkshopID);
        Companyrequests GetCompanyHistoryRequests(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int PageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery);
        string SaveClearanceReport(ClearanceSummary clearanceSummaryObj);
        string DeleteRequestedPart(int RequestedPartID, int RequestID, int ModifiedBy, int? DamagePointID);
        string PrintClearanceSummaryPdf(PdfData pdfData);
        AccidentDamagePartsMeta GetAccidentDamageParts(int? AccidentID, int? DamagePointID,string SearchQuery, int? CountryID);
        List<Accident> GetClearanceSavingReport(int CompanyID, DateTime? SearchDateFrom, DateTime? SearchDateTo);
        string SaveAccidentIsPurchasing(int AccidentID, bool IsPurchasing, int ModifiedBy, string WorkshopDetails, int? WorkshopID);
        AccidentMetaData GetCompanyHistoryAccidents(int CompanyID, Int16? AccidentTypeID, int? WorkshopID, int PageNo, int StatusID, int? MakeID, int? ModelID, int? YearID, string searchQuery);
        string ResetPassword(int UserID, string Password);
        RequestLog GetRequestLog(string RequestNumber,int RequestID);
        string OnSuggestQuotationPrice(int QuotationID, int ModifiedBy, double SuggestedPrice, int RequestID, int SupplierID, bool IsMatching, bool? ISCounterOfferAccepted , int? SuggestionOfferTime);
        string OnAcceptSuggestedPrice(int QuotationID, int ModifiedBy, bool IsSuggestionAccepted, int RequestID, int CompanyID, int SupplierID, double? CounterOfferPrice);
        string OnAcceptCounterOffer(int QuotationID, int ModifiedBy, int RequestID, int CompanyID, int SupplierID, bool ISCounterOfferAccepted);
        string GetPendingRequestCount(int AccidentID);
        Object GetRequestPendingParts(int RequestID);
        string UpdateICWorkshopStatus(int ICWorkshopID, Int16 StatusID, int ModifiedBy);
        WSICMeta GetWSICMetaData(int WorkshopID);
        string RequestCompanyWork(int WorkshopID, int CompanyID, int ModifiedBy);
        AccidentDamagePartsMeta GetDamagePartOptions(int DamagePointID, int ItemNumber,string Name1,int? CountryID);
        string PrintPOPdf(PdfData pdfData);
        string SavePONote(Request request);
        double ReturnOrderedParts(QuotationPart quotationPart);
        double UndoReturnOrderedParts(QuotationPart quotationPart);
        TechnicalNotesData GetTechnicalNotesData(string AccidentNo, int ObjectTypeID);
        string PrintCITechnicalReportPdf(PdfData pdfData);
        string SaveTechnicalReport(TechnicalNotesData technical);
        string SaveBodyDamageReport(TechnicalNotes technical);
        string OnApproveTechnicalReport(string AccidentNo,  int ObjectTypeID, int TRApprovalID, int? ModifiedBy, bool? IsApproved, string Note,bool? IsReturn, string ReturnNote);
        List<QuotationPart> RejectedPartByWorkshop(QuotationPart RejectedPart);
        RequestData GetRepairOrder(int RequestID);
        List<Request> GetAccidentCost(string AccidentNo, string StartDate, string EndDate);
        string PrintROPdf(PdfData pdfData);
        List<Company> GetAllCompanies(int? CountryID);
        List<Supplier> GetICSuppliers(int CompanyID,string StartDate,string EndDate);
        string UpdateSupplierBlockStatus(int CompanyID, int SupplierID, int UserID, bool IsBlocked);
        SupplierWorkDetail GetSupplierWorkDetail(int CompanyID, int SupplierID);
        DemandQuotations GetICLivetQuotationData(QuotationFilterModel model);
        string SaveWorkshopProfile(Workshop workshop);
        Object GetJCMainSeries(string MakeName, string ModelName);
        List<Gallery> DownloadCarseerImages();
        List<JCSeriesCase> GetImageCase(int AccidentID);
        List<JCSeriesCase> GetJCMainSeriesAndJCSeriesCase(VehicleSeriesMeta joClaimsSeriesData, int? ProductionYear);
        List<JCSeriesCase> GetJCSeriesCase(string JCSeriesID,int? YearCode);
        Object GetTechnicalNotesLog(string AccidentNo,string columnName, int? AccidentID,int?RequestID);
        RequestResponse SaveRequestDraft(DraftData draftData);
        Object GetWorkshopDraftData( int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery);
        Object GetSingleRequestDraft(int DraftID);
        string DeleteDraftPart(int DraftPartID,int ModifiedBy,int DamagePintID);
        RequestResponse UpdateRequestDraft(DraftData draftData);
        List<AccidentMarker> GetAccidentMarkers();
        List<Accident> SearchAccident(string searchQuery,int CompanyID);
        RequestResponse UpdateDraftData(int DraftID,int AccidentID,int ModifiedBy);
        Object GetPendingDraft(int StatusID, string VIN, int? WorkshopID);
        string UpdateDraftStatus(int StatusID,int DraftID, int ModifiedBy, string RejectDraftReason, string RestoreDraftReason);
        List<Accident> GetAccident(string VIN,int? CompanyID);
        Object GetAccidentForCloseRequest(int RequestID);
        string PublishLabourOnlyRequest(PublishRequest publishRequest);
        Object GetPriceReductionReport(int CompanyID, string StartDate, string EndDate,int? PageNo, int? CountryID);
        Object GetAccidentCarPartReport(int CompanyID, int? PageNo, int? MakeID, int? ModelID,int? YearID, string StartDate, string EndDate,string PartName,int? CountryID);
        string ReferTRApproval(int UserID, string AccidentNo, int ObjectTypeID,double Total);
        Object SaveAccountNumber(Account Account);
        List<TRApproval> TRApprovalNoteLog(int UserID, int ObjectTypeID, string @AccidentNo);
        Object GetChequeData(int AccidentID, int CompanyID);
        string UpdateChequeDate(ChequeData cheque);
        string PrintChequeDatePdf(PdfData pdfData);
        Object GetAccidentDocuments(string StartDate, string EndDate, int CompanyID, int? TabID,int? StatusID,int? PageNo, int? MakeID, int? ModelID, string searchQuery, int? LossAdjusterID);

        Object GetSurveyorReport(string StartDate, string EndDate, int CompanyID, int? MakeID, int? ModelID, int? YearID, int? UserID);
        Object GetSurveyorDetailRequestReport(string StartDate, string EndDate, int CompanyID);

        AccidentMetaData getaccidentdraft(int CompanyID, int? PageNo, string SearchQuery, DateTime? StartDate, DateTime? EndDate, int? MakeID,int? ModelID, int? YearID);
        Object getSingleAccidentDraft(int CompanyID,int ClaimentID);


        bool saveRequestTaskImage(List<Image> image, double? TotalPrice, int? RequestID,int? IsEnterLabourPartPriceChecked);


        bool getDuplicateVinCheck(int CompanyID, string VIN);

        bool getInstantPriceBitUpdate(int RequestID);
        bool savePhoneNumber(string VehicalOwnerPhoneNumber, int AccidentID);
        bool updateAprrovalStatusInClearance(bool ISApproved, int ClearanceSummaryApprovalID , int UserID);
        bool publishRequest(int RequestID, int UserID);

        InspektObj getAIdraftData(int DraftID);

        bool changePermissionStatus(ICWorkshop workshop);


        Object GetWorkshopAccidentData(int? StatusID, int? PageNo, int? WorkshopID, int? CompanyID, int? MakeID, int? ModelID, int? YearID, DateTime? StartDate, DateTime? EndDate, string SearchQuery);

        string mapAccidentOnDraft(int DraftID, int AccidentID, int ModifiedBy);

        Object downloadDraftDocs(int DraftID);

        string saveDepriciationValue(int RequestID, double depriciationValue , int? requestedPartID,int? UserID);

        bool updateSurveyorAppointment(int UserID,DateTime SurveyorAppointmentDate,int accidentID);

        bool SaveClearanceSummaryFreeText(int AccidentID, string Text);



    }
}
