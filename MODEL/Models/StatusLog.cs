using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class StatusLog
    {
        public int StatusLogID { get; set; }
        public int RequestID { get; set; }
        public Int16 StatusID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}
