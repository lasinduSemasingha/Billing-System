using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string ServiceDescription { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal BasePrice { get; set; }
        public ICollection<InvoiceService> InvoiceServices { get; set; }
    }
}
