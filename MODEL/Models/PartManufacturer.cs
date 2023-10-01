using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PartManufacturer
    {
        public Int16 PartManufacturerID { get; set; }
        public string ManufacturerName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsVerified { get; set; }
        public int TotalQuotations { get; set; }
        public bool? IsSelected { get; set; }
    }
}
