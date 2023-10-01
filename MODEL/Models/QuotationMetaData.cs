using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationMetaData
    {
        public List<Make> Makes { get; set; }
        public List<Model> Models { get; set; }
        public List<Year> Years { get; set; }
        public List<ObjectStatus> ObjectStatuses { get; set; }
    }
}
