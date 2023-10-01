using MODEL.InspectAI;
using MODEL.Models;
using MODEL.Models.Inspekt;
using MODEL.Models.Tchek;
using MODEL.Tchek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IManager
{
    public interface IAIManager
    {

        string AI(InspektObj inspektObj);
        bool saveCustomerData(AIInspectionRequest customerData);

        Object getAICustomerRequest(int CompanyID, int? PageNo);
        AIInspectionRequest getSingleCustomerRequest(int CustomerRequestID);

        bool updateCustomerAIRequest(AIInspectionRequest customerData);

        InspektObj GetAICustomerRequestReport(int CustomerRequestID);

        Object getFilteredAICustomerRequest(int CompanyID, string SearchQuery, DateTime? StartDate, DateTime? Enddate, int? PageNo);
        bool reSendSMS(AIInspectionRequest customerData);
        string RejectionReportResponse(InpektlabResponse inspektresponse);

        bool changeAIRequestCustomerStatus(int CustomerRequestID, int UserID);
        string getDraftID(string caseID);
        TchekToken SaveTchekToken(TchekToken tchekobj);
        string SaveTchekResponse(TchekResponse TchekResponse);
        string SaveHookTchekResponse(TchekResponse tchek);
        TchekResponse getCustomerRequestTchekReport(int CustomerRequestID);
        TchekResponse getImagesFromTchek(int? PointID, string TchekID);
    }
}
