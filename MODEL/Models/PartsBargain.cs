using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public  class PartsBargain
    {

        public int? PartsBargainID { get; set; }

        public int? RequestID { get; set; }

        public int? QuotationID { get; set; }

        public double? SuggestedPrice { get; set; }

        public double? CounterOffer { get; set; }
        

        public bool? ISSuggestionAccepted { get; set; }

        public bool? ISCounterAccepted   { get; set; }

        public int SupplierUserID  { get; set; }

        public int IcUserID { get; set; }

         public DateTime? CreatedOn { get; set; }

        public string SupplierName     { get; set; }

        public string IcUserName    { get; set; }
    }
}
