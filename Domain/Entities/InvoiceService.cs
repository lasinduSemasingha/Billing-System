using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class InvoiceService
    {
        [Key]
        public int InvoiceServiceId { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        [ForeignKey("Service")]
        public int ServiceId {  get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public Invoice Invoice { get; set; }
        public Service Service { get; set; }
    }
}
