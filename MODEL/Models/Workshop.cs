using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Workshop : Company
    {
        public int? WorkshopID { get; set; }
        public int UserID { get; set; }
        public string WorkshopName { get; set; }
        public string WorkshopPhone { get; set; }
        public string WorkshopCityName { get; set; }
        public string WorkshopGoogleMapLink { get; set; }
        public int? WorkshopCityID { get; set; }
        public string WorkshopAreaName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int16? StatusID { get; set; }
        public string ProfileImageURL { get; set; }
        public string Email { get; set; }
        public bool? IsCompanyApproved { get; set; }
        public int? CompanyID { get; set; }
        public int? RequestLimit { get; set; }
        public int? SupplierID { get; set; }

        public string CurrencyEnglish { get; set; }

        public string CurrencyArabic { get; set; }

    }
}
