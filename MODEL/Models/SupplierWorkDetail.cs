using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
   public class SupplierWorkDetail
    {
        public Supplier supplier { get; set; }
        public List<Make> make { get; set; }
        public List<Model> model { get; set; }
    }
}
