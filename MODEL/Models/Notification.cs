using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public int? RecipientID { get; set; }
        public bool? IsSent { get; set; }
        public string TextData { get; set; }
        public string RedirectURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool? IsRead { get; set; }
        public Int16? NotificationTypeId { get; set; }
        public string Icon { get; set; }
        public string NotificationTypeName { get; set; }
        public string CreatedSince { get; set; }
        public string CreatedSinceArabic { get; set; }
        public byte? RoleID { get; set; }
        public int CreatedBy { get; set; }
        public int? RequestID { get; set; }
        public int? QuotationID { get; set; }
        public int? DemandID { get; set; }
        public string MobileScreenName { get; set; }
        public string MobileNotificationToken { get; set; }
        public string TextArabicData { get; set; }
        public int? SupplierID { get; set; }
        public Int16? MobileTypeID { get; set; }
        public bool? IsBiddingTimeExpired { get; set; }
        public int? AppliedQuotationID { get; set; }
        public string AccidentNo { get; set; }
        public Int16? ObjectTypeID { get; set; }
        public int? DraftID { get; set; }
        public string VIN { get; set; }

        public int? ExpiredConditionNumber { get; set; }
    }
}
