using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AutomotivePartLog
    {
        public int AutomotivePartID { get; set; }
        public string PartName { get; set; }
        public string CreatedByName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public short? StatusID { get; set; }
        public string ApprovedByName { get; set; }
        public DateTime? ApprovedDateTime { get; set; }
        public string RejectedByName { get; set; }
        public DateTime? RejectedDateTime { get; set; }
        public string EditedBy { get; set; }
        public DateTime? EditedDateTime { get; set; }
    }
}
