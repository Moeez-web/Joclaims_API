using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Country
    {
        public Int16 LanguageCountryID { get; set; }
        public string CountryName { get; set; }

        public string CountryNameArabic { get; set; }

        public byte LanguageID { get; set; }
        public Int16 CountryID { get; set; }
    }
}
