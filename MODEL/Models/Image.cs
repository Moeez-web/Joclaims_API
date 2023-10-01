using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Image
    {
        public int? ImageID { get; set; }
        public string ImageURL { get; set; }
        public string OriginalName { get; set; }
        public string EncryptedName { get; set; }
        //public Int16 ImageTypeID { get; set; }
        public int? ObjectID { get; set; }
        public Int16? ObjectTypeID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public string ImageDataUrl { get; set; }
        public int RequestID { get; set; }
        public int? QuotationID { get; set; }
        public bool? IsDocument { get; set; }
        public string Description { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string ModifiedByName { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string PartNameEnglish { get; set; }
        public string PartName { get; set; }
        public string AccidentNo { get; set; }
        public int AccidentID { get; set; }
        public bool? IsImage { get; set; }
        public int? DocType { get; set; }

        public int? RequestTaskId { get; set; }

        List<Image> RequestedTaskimages { get; set; }
    }
}
