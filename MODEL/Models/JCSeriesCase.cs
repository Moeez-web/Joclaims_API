using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class JCSeriesCase
    {
        public string JCSeriesID { get; set; }
        public string FuelType { get; set; }
        public string CarImageURL { get; set; }
        public string CarImageEncryptedName { get; set; }
        public int? OverLappingYear { get; set; }
        public string CorrectSeriesID { get; set; }
        public int? AccidentID { get; set; }
        public string BodyType { get; set; }
        public string EnglishModelName { get; set; }
        public int? DraftID { get; set; }
        public int ID { get; set; }
        public bool? IsDeleted { get; set; }
        public string EnglishMakeName { get; set; }
        public string ModelVariant { get; set; }


    }
}
