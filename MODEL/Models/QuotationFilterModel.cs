using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class QuotationFilterModel
    {
        public int DemandID { get; set; }
        public int RequestID { get; set; }
        public bool? IsReferred { get; set; }
        public Int16? ConditionTypeID { get; set; }
        public Int16? NewPartConditionTypeID { get; set; }
        public Int16? Availability { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
        public string SortByPrice { get; set; }
        public string SortByFillingRate { get; set; }
        public string SortByRating { get; set; }
        public int? IsPaid { get; set; }
        public string AreaName { get; set; }
        public int? Price { get; set; }
        public List<City> Cities { get; set; }
        //public List<ManufacturerRegion> ManufacturerRegions { get; set; }
        //public List<PartManufacturer> PartManufacturers { get; set; }
        public List<RequestedPart> RequestedParts { get; set; }
        public int MinFillingRate { get; set; }
        public int MaxFillingRate { get; set; }
        public int Rating { get; set; }
        public Int16? NewConditionTypeID { get; set; }
    }
}
