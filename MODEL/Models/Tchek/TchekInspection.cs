using MODEL.Tchek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekInspection : TchekToken
    {
        public int? TchekInspectionID { get; set; }
        public string TchekID { get; set; }
        public string ShortID { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsFlagged { get; set; }
        public bool? IsNew { get; set; }
        public int? Type { get; set; }
        public bool? ScanToUpload { get; set; }
        public bool? UploadHDPictures { get; set; }
        public DateTime? TchekCreatedOn { get; set; }
        public int? PercentDamage { get; set; }
        public int? PercentParts { get; set; }
        public string ReportUrl { get; set; }
        public string ReportUrlWithoutCosts { get; set; }
        public int DamageNumber { get; set; }
        public string ThumbnailUrl { get; set; }
        public int? Status { get; set; }
        public string Response { get; set; }
        public string Token { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
