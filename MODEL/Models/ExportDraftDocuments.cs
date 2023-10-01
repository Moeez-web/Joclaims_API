using MODEL.Models.Request_Draft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class ExportDraftDocuments
    {
        public RequestDraft draftdata { get; set; }
        public List<RequestDraftImage> carImages { get; set; }
        public List<RequestDraftImage> CarVideos { get; set; }
        public List<RequestDraftImage> carDocuments { get; set; }
        public List<RequestDraftImage> CarVINMilage { get; set; }
    }
}
