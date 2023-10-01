using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class SurveyorsSignature
    {
        public string CreatedByName { get; set; }
        public string ESignatureURL { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
