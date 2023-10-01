using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class SavePart
    {
        public int SupplierID { get; set; }
        public int CreatedBy { get; set; }
        public List<PartInfo> PartsInfo { get; set; }
        public List<PartInfo> ExistsPartsInfo { get; set; }
    }
}
