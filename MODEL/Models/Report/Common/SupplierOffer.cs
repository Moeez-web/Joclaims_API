using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models.Report.Common
{
  public class SupplierOffer
  {
    public int RequestID { get; set; }
    public int Requestnumber { get; set; }
    public Int16 StatusID { get; set; }
    public string RequestStatusName { get; set; }
    public string RequestArabicStatusName { get; set; }
    public string EnglishMakeName { get; set; }
    public string ArabicMakeName { get; set; }
    public string CompanyName { get; set; }
    public int QuotationID { get; set; }
    public bool? IsPurchasing { get; set; }
    public bool? IsNotAvailable { get; set; }
    public decimal? FillingRate { get; set; }
    public double? BestOfferPrice { get; set; }
    public decimal? MatchingFillingRate { get; set; }
    public double? LowestOfferMatchingPrice { get; set; }
    public double? POTotalAmount { get; set; }
    public decimal? Differnece { get; set; }
    public string POWinStatus { get; set; }


                                                                                                                                                                                                                                                                                                                                                                                                                                                                   
  }
}
