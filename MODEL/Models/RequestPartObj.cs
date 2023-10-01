using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
  public class RequestPartObj
  {
    public int CompanyId { get; set; }
    public int PageNo { get; set; }
    public List<Make> MakeID { get; set; }
    public List<Model> ModelID { get; set; }
    public int YearID { get; set; }
    public bool IsExcel { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int CountryID { get; set; }

  }
}
