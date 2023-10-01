using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
  public class Account
  {
    public int? AccountID { get; set; }
    public int? CompanyID { get; set; }
    public string AccountNumber { get; set; }
    public int? ObjectTypeID { get; set; }
    public int? ClientID { get; set; }

  }
}
