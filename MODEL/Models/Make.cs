using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Make
    {
        public Int16 LanguageMakeID { get; set; }
        public string MakeCode { get; set; }
        public string ImgURL { get; set; }
        public Byte LanguageID { get; set; }
        public int MakeID { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string MakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public string EnglishMakeName { get; set; }
        public int UserID { get; set; }
    }
}
