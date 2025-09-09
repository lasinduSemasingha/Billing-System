using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class JobCardPartsDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; }
        public decimal UnitPrice { get; set; }
        public int? Quantity { get; set; }
    }
}
