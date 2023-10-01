using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class CompanyProfile
    {
        public Company Company { get; set; }
        public List<City> Cities { get; set; }
        public List<Country> Countries { get; set; }
        public List<ObjectType> Positions { get; set; }
    }
}
