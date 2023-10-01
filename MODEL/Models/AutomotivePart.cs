using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class AutomotivePart
    {
        public int? AutomotivePartID { get; set; }
        public string PartName { get; set; }
        public string PartNameEnglish { get; set; }
        //public string PartNameArabic { get; set; }
        //public int MakeID { get; set; }
        //public int ModelID { get; set; }
        //public Int16 ProductionYear { get; set; }

        //public string ImageURL { get; set; }
        //public string OriginalName { get; set; }
        //public string EncryptedName { get; set; }

        public string DamagePointID { get; set; } 
        public string DamageName { get; set; } 
        public string ItemNumber { get; set; } 
        public string Name1 { get; set; } 
        public string OptionName { get; set; } 
        public string OptionNumber { get; set; } 
        public int OptionCount { get; set; }
        public string FinalCode { get; set; } 
        public string Name2 { get; set; } 
        public string Name3 { get; set; } 
        public string Name4 { get; set; } 
        public string Name5 { get; set; } 
        public string Name6 { get; set; } 
        public string Name7 { get; set; } 
        public string Name8 { get; set; } 
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? IsVerified { get; set; }
        public int? UniversalPartID { get; set; }
        public int? SupplierID { get; set; }
        public bool? IsConditionNew { get; set; }
        public bool? IsConditionAfterMarket { get; set; }
        public bool? IsConditionOriginal { get; set; }
        public bool? IsConditionUsed { get; set; }
        public bool? IsDeleted { get; set; }
        public Int16? IsApproved { get; set; }
        public Int16? StatusID { get; set; }
        public bool? IsSelected { get; set; }
        public bool? IsModified { get; set; }
        public int? OldAutomotivePartID { get; set; }
        public int? OffersCount { get; set; }
        public string NoteInfo { get; set; }
        public int? RequestCount { get; set; }
        public string EnglishMakeName { get; set; }
        public string ArabicMakeName { get; set; }
        public string EnglishModelName { get; set; }
        public string ArabicModelName { get; set; }
        public List<DamagePoint> DamagePoint { get; set; }
        public int? PageNo { get; set; }
        public int? Status { get; set; }
        public int? TotalParts { get; set; }
        public string OldName1 { get; set; }
        public string OldFinalCode { get; set; }
        public int? CountryID { get; set; }
    }
}
