using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekDamages
    {
        public int? DamageID { get; set; }
        public string TchekDamageID { get; set; }
        public string TchekID { get; set; }
        public int  TcheckDamageUID { get; set; }
        public string PointName { get; set; }
        public string PointNameArabic { get; set; }
        public string SeverityMapEnglishName { get; set; }
        public double? PercentBestRoi { get; set; }
        public bool? IsAuto { get; set; }
        public string DamageAreaImage { get; set; }
        public string Severity { get; set; }
        public double? Size { get; set; }
        public string Type { get; set; }
        public string SvgLocation { get; set; }
        public double? RadiusWorld { get; set; }
        public DateTime? TChekCreatedOn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
