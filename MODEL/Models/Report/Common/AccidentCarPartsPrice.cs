using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class AccidentCarPartsPrice
    {
        public string EnglishMakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public string EnglishModelName { get; set; }
        public string ArabicModelName { get; set; }
        public int YearCode { get; set; }
        public string PartName { get; set; }
        public double? PartPrice { get; set; }
        public int? AccidentCount { get; set; }
    }
}
