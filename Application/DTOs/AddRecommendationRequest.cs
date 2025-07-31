using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AddRecommendationRequest
    {
        public int VehicleId { get; set; }
        public string PartName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime RecommendedDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
