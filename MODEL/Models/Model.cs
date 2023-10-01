using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Model
    {
        public Int32 LanguageModelID { get; set; }
        public string ModelCode { get; set; }
        public string ArabicModelName { get; set; }
        public int MakeID { get; set; }
        public Int16 YearCode { get; set; }
        public byte LanguageID { get; set; }
        public int ModelID { get; set; }
        public string GroupName { get; set; }
        public string ModelGroupName { get; set; }
        public string EnglishModelName { get; set; }
        public string ImgURL { get; set; }
        public string ArabicMakeName { get; set; }
        public string MakeName { get; set; }
        public int UserID { get; set; }
        public int AccidentCount { get; set; }
    }
}
