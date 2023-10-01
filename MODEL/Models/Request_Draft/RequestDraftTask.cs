using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Request_Draft
{
    public class RequestDraftTask:RequestTask
    {
        public int DraftTaskID { get; set; }
        public int DraftID { get; set; } 
    }
}
