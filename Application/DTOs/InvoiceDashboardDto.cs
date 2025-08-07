using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceDashboardDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DateIssued { get; set; }
        public string VehicleRegistration { get; set; } = "";
        public string VehicleModel { get; set; } = "";
        public decimal TotalAmount { get; set; }
        public decimal VatAmount { get; set; }
        public bool PaidStatus { get; set; }
        public List<InvoiceDashboardPartDto> Parts { get; set; } = new();
        public List<InvoiceDashboardServiceDto> Services { get; set; } = new();
    }

    public class InvoiceDashboardPartDto
    {
        public int PartId { get; set; }
        public string PartName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceDashboardServiceDto
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
