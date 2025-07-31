using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceResponse
    {
        public int InvoiceId { get; set; }
        public int VehicleId { get; set; }
        public DateTime DateIssued { get; set; }
        public decimal TotalAmount { get; set; }
        public bool PaidStatus { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
