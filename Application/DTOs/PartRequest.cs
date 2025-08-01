using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class PartRequest
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public string PartDescription { get; set; } = string.Empty;
    }
}
