using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class InpektlabResponse
    {
        public string CaseID { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string token { get; set; }
        public string session { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int CreatedBy { get; set; }
        public int AppID { get; set; }
        public int? PersonaID { get; set; }
        public bool IsDeleted { get; set; }

    }
}
