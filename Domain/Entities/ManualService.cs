using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ManualService
    {
        [Key]
        public int ManualServiceId { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public string? ServiceDescription { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,2)")]
        public decimal? BasePrice { get; set; }
        public Invoice Invoice { get; set; }
    }
}
