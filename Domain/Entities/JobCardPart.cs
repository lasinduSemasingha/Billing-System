using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobCardPart
    {
        public int JobCardPartID { get; set; }
        public int JobCardId { get; set; }
        public int PartId { get; set; }
        public int? Quantity { get; set; }
        public bool? IsAdded { get; set; }
        public JobCard JobCard { get; set; }
        public Part Part { get; set; }
    }
}
