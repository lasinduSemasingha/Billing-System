using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class InvoiceRequest
    {
        public string Customer { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime DateIssued { get; set; }
        public decimal TotalAmount { get; set; }
        public Boolean PaidStatus { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
