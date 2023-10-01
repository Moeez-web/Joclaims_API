using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Tchek
{
    public class TchekImages
    {
        public int? ImageID { get; set; }
        public string TcheckImgaeID { get; set; }
        public string TchekID { get; set; }
        public string Path { get; set; }
        public string Name { get; set; }
        public int? Angle { get; set; }
        public int?   Height { get; set; }
        public int? Width { get; set; }
        public DateTime? TchekCreatedOn { get; set; }
        public string ThumbnailUrl { get; set; }
        public int? TypeImage { get; set; }
        public string Url { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
