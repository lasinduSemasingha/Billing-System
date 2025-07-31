using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Recommendation
{
    public interface IRecommendationService
    {
        Task<Domain.Entities.Recommendation> CreateRecommendationAsync(int vehicleId, string partName, string description, DateTime recommendedDate, string status);
        Task<Domain.Entities.Recommendation?> GetRecommendationAsync(int recommendationId, int vehicleId);
    }
}
