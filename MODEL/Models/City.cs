using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class City
    {
        public int LanguageCityID { get; set; }
        public string CityCode { get; set; }
        public Int16 CountryID { get; set; }
        public byte LanguageID { get; set; }
        public int CityID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CityName { get; set; }

        public string CityNameArabic { get; set; }
        public int TotalQuotations { get; set; }
        public bool? IsSelected { get; set; }
    }
}
