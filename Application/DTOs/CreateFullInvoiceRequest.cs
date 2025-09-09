using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateFullInvoiceRequest
    {
        public int VehicleId { get; set; }
        public DateTime DateIssued { get; set; }
        public bool PaidStatus { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal VatAmount { get; set; }
        public string Notes { get; set; }

        public List<InvoicePartDto> Parts { get; set; }
        public List<InvoiceServiceDto> Services { get; set; }
    }

    public class InvoicePartDto
    {
        public int PartId { get; set; }
        public string? ManualPartName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceServiceDto
    {
        public int ServiceId { get; set; }
        public string? ManualServiceName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
