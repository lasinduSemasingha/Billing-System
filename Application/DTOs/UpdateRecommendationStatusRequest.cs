using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateRecommendationStatusRequest
    {
        public int RecommendationId { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
