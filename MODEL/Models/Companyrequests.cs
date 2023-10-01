using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Companyrequests : RequestMetaData
    {
        public List<Request> Requests { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public List<Image> RequestedPartsImages { get; set; }
        public List<Company> Companies { get; set; }
        public List<POApproval> POApprovalEmployees { get; set; }
        public List<PurchaseOrder> PurchaseOrders { get; set; }
        public List<ICWorkshop> WorkShops { get; set; }
        public int TotalPages { get; set; }
        public TabInfo TabInfoData { get; set; }

        public List<ObjectType> ObjectType { get; set; }
    }
}
