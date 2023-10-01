using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Inspekt
{
    public class InspektlabObj
    {
        public string session { get; set; }
        public string type { get; set; }
        public List<string> urls { get; set; }
        public string case_id { get; set; }

        public string refresh_token { get; set; }
    }
}
