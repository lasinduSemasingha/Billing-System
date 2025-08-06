using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class StockSummaryDto
    {
        public int TotalParts { get; set; }
        public int InStock {  get; set; } 
        public int LowStock { get; set; }
        public int OutOfStock { get; set; }
    }
}
