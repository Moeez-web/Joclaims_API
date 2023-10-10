using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class RequestData
    {
        public Request Request { get; set; }
        public bool WillDeliver { get; set; }
        public bool? IsMobileApp { get; set; }
        public decimal? DeliveryCost { get; set; }
        public List<Request> Requests { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<RequestedPart> RequestedPartsPrice { get; set; }
        public List<QuotationPart> QuotationParts { get; set; }
        public List<QuotationPart> OrderedParts { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<Image> QuotationPartsImages { get; set; }
        public List<RequestTask> RequestTasks { get; set; }
        public List<AccidentMarker> AccidentMarkers { get; set; }
        public List<Note> Notes { get; set; }
        public List<Image> AccidentImages { get; set; }
        public List<POApproval> POApprovalEmployees { get; set; }
        public List<SurveyorsSignature> SurveyorsSignatures { get; set; }
        public List<SurveyorsSignature> PartsApprovedBySignatures { get; set; }
        public List<SurveyorsSignature> TasksApprovedBySignatures { get; set; }
        public List<SurveyorsSignature> POApprovedSignatures { get; set; }
        public List<PurchaseOrder> Suppliers { get; set; }

        public List<Image> CarReadyImages { get; set; }
        public List<PDFDetail> PDFDetail { get; set; }


    }
}
