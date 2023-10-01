using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Models
{
    public class Note
    {
        public int NoteID { get; set; }
        public string TextValue { get; set; }
        public int AccidentID { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedByName { get; set; }
        public string ModifiedByName { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool? IsPublic { get; set; }
        public bool? IsModified { get; set; }
        public DateTime? EventDateTime { get; set; }
        public string CreatedByEmail { get; set; }
        public string ModifiedByEmail { get; set; }
        public string AccidentNo { get; set; }
    }
}
