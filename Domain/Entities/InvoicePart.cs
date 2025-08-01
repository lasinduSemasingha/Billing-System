using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace Domain.Entities
{
    public class InvoicePart
    {
        [Key]
        public int InvoicePartId { get; set; }
        [ForeignKey("Invoice")]
        public int InvoiceId { get; set; }
        [ForeignKey("Part")]
        public int PartId { get; set; }
        public int quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [JsonIgnore]
        public Invoice Invoice { get; set; }
        public Part Part { get; set; }
    }
}
