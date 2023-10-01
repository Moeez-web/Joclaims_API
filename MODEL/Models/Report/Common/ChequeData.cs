using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
    public class ChequeData 
    { 


        public int AccidentID { get; set; }
        public string FreeText { get; set; }
        public string SignatureUrl { get; set; }
        public DateTime ChequeCreatedOn { get; set; }
        public DateTime ChequeDate { get; set; }
        public int SignaturedBy { get; set; }

    }
}
