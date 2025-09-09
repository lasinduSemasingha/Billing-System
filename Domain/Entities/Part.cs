using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Part
    {
        [Key]
        public int PartId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Description {  get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        public int StockQty { get; set; }
        public int ReorderLevel { get; set; }
        public ICollection<InvoicePart> InvoiceParts { get; set; }
        public ICollection<JobCardPart> JobCardParts { get; set; } = new List<JobCardPart>();
    }
}