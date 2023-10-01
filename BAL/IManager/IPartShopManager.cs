using MODEL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface IPartShopManager
    {
        SupplierProfile GetPartShopProfile(int SupplierID);
        SavePart SaveParts(SavePart partInfoData);
        string UpdatePartInfo(UpdatePart partInfoData);
        string DeletePart(int PartInfoID, int SupplierID);
        Supplier UpdatePartShopProfile(SupplierProfile profileData);
        //List<PartInfo> SavePartShopProfile(SupplierProfile profileData);
        Companyrequests GetPartsDemands(int SupplierID);
        string SavePartsQuotation(RequestData request);
        string UpdatePartsQuotation(QuotationData QuotationData);
        ShopQuotations GetShopQuotations(int SupplierID, int StatusID, int PageNo, int? MakeID, int? ModelID, int? YearID, DateTime? POStartDate, DateTime? POEndDate, string AccidentNo, int? CompanyID, string SearchQuery);
        QuotationData GetSingleQuotation(int QuotationID);
        QuotationData GetQuotationRequestData(int RequestID, int? QuotationID, int SupplierID);
        ShopQuotations GetShopInvoice(int QuotationID, int DemandID);
        QuotationData GetPSInvoice(int QuotationID, int DemandID);
        QuotationData GetDemandRequestData(int RequestID, int? QuotationID, int SupplierID);
        PSDashboard GetPSDashboard(int SupplierID);
        string SaveBranch(PartBranch PartBranch);
        string UpdateBranch(PartBranch PartBranch);
        string DeleteBranch(int BranchID, int ModifiedBy);
        List<PartBranch> GetAllBranches(int SupplierID, int LoginUserID);
        PartBranch GetSingleBranch(int BranchID, int LoginUserID);
        string SaveInvoiceImage(Quotation quotation);
        string SaveUvPart(AutomotivePart uvPart);
        string DeleteUvPart(int UniversalPartID, int ModifiedBy);
        string QuotationNotAvailable(int RequestID, int SupplierID, int DemandID, string notAvailableNote);
        AppSetting GetAppVersion(string MobileAppVersion, int? SupplierID);
        string updatePartiallySellingStatus(int QuotationID, bool IsPartialSellings, int ModifiedBy);
        AppSetting GetMobileAppVersion(string MobileAppVersion, int? UserID);
        string saveTotallabourPartsPrice(int demandID,int TotallabourPartsPrice,int UserID, int CompanyTypeID, int RoleID);
    }
}
