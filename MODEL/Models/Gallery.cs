using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Gallery
    {
        public int GalleryID { get; set; }
        public string OriginalName { get; set; }
        public string EncryptedName { get; set; }
        public string EncryptedName2 { get; set; }
        public string ImageDataUrl { get; set; }
        public string Heading { get; set; }
        public string VideoDescription { get; set; }
        public bool? IsVideo { get; set; }
        public string ImageURL { get; set; }
        public string ImageURL2 { get; set; }
        public string NewEncryptedName { get; set; }
        public string NewEncryptedName2 { get; set; }
        public string SeriesID { get; set; }
       

    }
}
