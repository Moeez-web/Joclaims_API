using MODEL.Models;
using MODEL.Models.Report.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface ICommonManager
    {
        CommonMeta GetCommonMeta(int? userID,int? RoleID);
        List<City> GetAllCities(int? CountryID);
        List<AutomotivePart> SearchPart(string PartName,int? DamagePointID,int? CountryID);
        Object UpdatePartInfo(UpdateAutomotivePart updateAutomotivePart);
        RequestLog checkVINHistory(string VIN);
        Object GetAccidentCostReport(RequestPartObj RPObj);
        Object GetPartPriceEstimate(string SeriesID, int RequestID, int UserID,int? DemandID, int? monthfilter);
        AutomotivePart GetNewAutomotivePartDetail(int DamagePointID, string Name1,int? AutomotivePartID);
        Object SearchName(string PartName, int DamagePointID, int? CountryID);
        string CreateAutomotivePart(AutomotivePart automotivePart);

        string RejectAutomotivePart(int automotivePartID,int UserID);
        Object GetICStaffKPI(int companyID, int UserID, string StartDate, string EndDate, int? ReceptionID, int? LossAdjusterID, int? PublishBy, int? POOrderBy, int? POApproveBy,int? PageNo);

        Object GetMonthlyClaimsReport(int CompanyID, int UserID, string StartDate, string EndDate,int? PageNo);
        SignInResponse GetWorkshopCompany(int UserID, int RoleID);
        Object GetMobileAPIUrl(string MobileAppVersion);
        Object SearchNameInMultipleDamagePoint(AutomotivePart automotivePart);
        Object generateMultiAutoMotivePartCode(AutomotivePart automotivePart);

        Object PoRoDetailReport(int CompanyID, string StartDate, string EndDate, bool IsExcel, int PageNo);
        Object GetlatestQuotationSuppliers(string SeriesID, int AutomotivePartID, int CoditionTypeID, int NewPartConditionTypeID,int monthfilter,int userRoleID, int? CountryID);

        bool printrequest(int requestID, string filename);


        bool saveEstimationPriceAndCondition(EstimationPrice estimationPrice);

        bool updateTokenAndCaseID(string CaseId, string Token, int draftID , int UserID);
        Object getDraftRequestreport(int? WorkShopID, int? PageNo, string StartDate, string EndDate, int? CompanyID, int? CountryID);

        


    }
}
