using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JobCardServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public bool Requested { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string? Remarks { get; set; }
    }
}
