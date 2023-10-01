using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class CompanyStats
    {
      public int  CompanyID { get; set; }
        public string CompanyName { get; set; }
        public int? AccidentWithrequestCount { get; set; }
        public int? RequestedPartsCount { get;set; }
        public int? CompanyRate { get; set; }
        public bool? IsEdit { get; set; }
        public int? tempCompanyRate { get; set; }
        public double? Invoice { get; set; }

        public int? TotalAccidentCount { get; set; }
        public int? DuplicateAccidentCount { get; set; }
        public int? AccidentWithZeroQuotation { get; set; }
        public int? AccidentWithoutRequest { get; set; }
        public int? AccidentWithUnpublishRequest { get; set; }




    }
}
