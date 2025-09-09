using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class JobCardService
    {
        public int JobCardServiceID {  get; set; }
        public int JobCardId { get; set; }
        public int ServiceId { get; set; }
        public bool Requested { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Remarks { get; set; }
        public Service Service { get; set; }
        public JobCard JobCard { get; set; }
    }
}
