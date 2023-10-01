using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekDamageImageList
    {
        public int DamageImageListID { get; set; }
        public string TchekImageID { get; set; }
        public string TchekDamageID { get; set; }
        public double? TchekID { get; set; }
        public string EncryptedName { get; set; }
        public string PartName { get; set; }
        public double? CenterX { get; set; }
        public double? CenterY { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string ImageUrl { get; set; }
        public bool? IsAuto { get; set; }
        public double? Score { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
