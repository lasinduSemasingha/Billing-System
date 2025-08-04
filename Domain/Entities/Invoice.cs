using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Invoice
    {
        [Key]
        public int InvoiceId {  get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime DateIssued { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal VatAmount { get; set; }
        public Boolean PaidStatus { get; set; }
        public string Notes { get; set; } = string.Empty;
        public Vehicle Vehicle { get; set; }
        public ICollection<InvoiceService> InvoiceServices { get; set; }
        public ICollection<InvoicePart> InvoiceParts { get; set; }
    }
}
