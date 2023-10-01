using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Tchek
{
    public class TchekToken
    {
        public int? TokenID { get; set; }
        public string TchekTokenID { get; set; }
        public string TchekID { get; set; }
        public string Token { get; set; }
        public int? SendingType { get; set; }
        public bool? TradeInVehicle { get; set; }
        public int? TradeInStatus { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public DateTime? CreatedOnTchek { get; set; }
        public string TchekAPITokenID { get; set; }
        public string TchekCustomerID { get; set; }
        public string TchekVehicleID { get; set; }
        public int? AppID { get; set; }
        public string Response { get; set; }
        public string Message { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public DateTime? ModifiedBy { get; set; }

    }
}
