using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
  public class SeriesCase
  {
    public joClaimsSeriesData JoClaimsSeriesData { get; set; }
    public List<JCSeriesCase> MapExistingSeries { get; set; }
    public List<JCSeriesCase> MapCurrentSeries { get; set; }
    public List<joClaimsSeriesData> Conflict { get; set; }
    public List<joClaimsSeriesData> seriesremoved { get; set; }
    public List<JCSeriesCase> SelfSeriesCases { get; set; }
    }
}
