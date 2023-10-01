using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Vehicle
    {
        public string VIN { get; set; }
        public string PlateNo { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public Int16? ProductionYear { get; set; }
        public Int16? EngineTypeID { get; set; }
        public Int16? BodyTypeID { get; set; }
        public string ArabicMakeName { get; set; }
        public string MakeName { get; set; }
        public string ArabicModelName { get; set; }
        public string ModelCode { get; set; }
        public int YearCode { get; set; }
        public string EngineTypeArabicName { get; set; }
        public string EngineTypeName { get; set; }
        public string BodyTypeArabicName { get; set; }
        public string BodyTypeName { get; set; }
        public string BodyTypeIcon { get; set; }
                       
    }
}
