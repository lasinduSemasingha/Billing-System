using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateInvoiceRequest
    {
        public int VehicleId { get; set; }
        public DateTime DateIssued { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public bool PaidStatus { get; set; }
        public string Notes {  get; set; } = string.Empty;
    }

}
