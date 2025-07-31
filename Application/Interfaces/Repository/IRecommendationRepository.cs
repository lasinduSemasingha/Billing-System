using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IRecommendationRepository
    {
        Task CreateRecommendationAsync(Domain.Entities.Recommendation recommendation);
        Task<Domain.Entities.Recommendation?> GetRecommendationAsync(int recommendationId, int vehicleId);
        Task SaveChangesAsync();
    }
}
