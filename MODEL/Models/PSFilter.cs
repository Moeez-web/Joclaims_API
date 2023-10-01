using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PSFilter
    {
        public string ShopName { get; set; }
        public string CPName { get; set; }
        public string CPEmail { get; set; }
        public int? MakeID { get; set; }
        public int? YearID { get; set; }
        public int? ModelID { get; set; }
        public string SearchQuery { get; set; }
        public string SortBy { get; set; }
        public DateTime? WorkingFrom { get; set; }
        public DateTime? WorkingTo { get; set; }

        public int? CountryID { get; set; }
    }
}
