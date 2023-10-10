using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class PDFDetail
    {
        public int PDFDetailID { get; set; }
        public int CompanyID { get; set; }
        public int ObjectTypeID { get; set; }
        public string SectionOne { get; set; }
        public string SectionTwo { get; set; }
        public string SectionThree { get; set; }
        public string SectionFour { get; set; }
    }
}
