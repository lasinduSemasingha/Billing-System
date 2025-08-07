using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreatePartRequest
    {
        public string PartName { get; set; }
        public string PartDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQty { get; set; }
        public int ReOrderLevel { get; set; }
    }
}
