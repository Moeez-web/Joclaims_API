using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ObjectType
    {
        public Int16 ObjectTypeID { get; set; }
        public string ObjectName { get; set; }
        public string TypeName { get; set; }

        public string ArabicTypeName { get; set; }
        public string Icon { get; set; }
        public bool? IsEditAllowed { get; set; }
    }
}
