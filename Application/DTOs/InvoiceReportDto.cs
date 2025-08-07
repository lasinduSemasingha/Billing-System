using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceReportDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateIssued { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public bool PaidStatus { get; set; }
        public string VehicleInfo { get; set; } // e.g., vehicle registration or model
        public List<InvoicesPartDto> Parts { get; set; }
        public List<InvoicesServiceDto> Services { get; set; }
    }

    public class InvoicesPartDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoicesServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal ServicePrice { get; set; }
    }

}
