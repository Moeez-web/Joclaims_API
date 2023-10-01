using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class joClaimsSeriesData
    {
        public string JCSeriesID { get; set; }
        public string EnglishMakeName { get; set; }
        public string EnglishModelName { get; set; }
        public string BodyCode { get; set; }
        public string BodyType { get; set; }
        public int? EndYear { get; set; }
        public int? StartYear { get; set; }
        public string ModelVariant { get; set; }
        public string YearRange { get; set; }
        public string FuelType { get; set; }
        public bool? face_lift { get; set; }
        public string ImageURL { get; set; }
        public string GroupName { get; set; }
        public string FuelTypeName { get; set; }
        public string BodyTypeName { get; set; }
        public int? ProductionYear { get; set; }
        public string CorrectSeriesID { get; set; }
        public int? OverLappingYear { get; set; }
        public string EncryptedName { get; set; }
        public int UserID { get; set; }
        public Int16? StatusID { get; set; }
        public string CreatedFrom { get; set; }
        public string CreatedTo { get; set; }
        public int PageNO { get; set; }
        public bool? IsNew { get; set; }
        public int? ModelID { get; set; }
        public int? AccidentCount { get; set; }
        public string OldJoclaimSeriesID { get; set; }
        public string OldJoclaimSeriesImage { get; set; }
        public int ID { get; set; }
        public string OriginalCaseImage { get; set; }

  }
}
