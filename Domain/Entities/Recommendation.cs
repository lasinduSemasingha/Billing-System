using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Recommendation
    {
        [Key]
        public int RecommendationId { get; set; }
        [ForeignKey("Vehicle")]
        public int VehicleId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime RecommendedDate { get; set; }
        public string status { get; set; } = string.Empty;
        public Vehicle Vehicle { get; set; }
    }
}
