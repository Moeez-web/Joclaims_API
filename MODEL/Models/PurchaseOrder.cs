using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
     public class PurchaseOrder
    {
        public int  PurchaseOrderID { get; set; }
        public Int16? StatusID { get; set; }
        public int? RequestID { get; set; }
        public int? QuotationID { get; set; }
        public int? SupplierID { get; set; }
        public string PONote { get; set; }
        public string POPdfURL { get; set; }
        public double? POAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBY { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string SupplierName { get; set; }
        public string SupplierPhone { get; set; }
        public int? RequestNumber { get; set; }
    }
}
