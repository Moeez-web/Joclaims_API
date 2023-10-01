using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Request_Draft
{
    public class RequestDraftImage:Image
    {
        public int DraftImageID { get; set; }
        public bool IsVideo { get; set; }
    }
}
