using Application.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Recommendation
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IRecommendationRepository _recommendationRepository;
        public RecommendationService(IRecommendationRepository recommendationRepository)
        {
            _recommendationRepository = recommendationRepository;
        }

        public async Task<Domain.Entities.Recommendation> CreateRecommendationAsync(int vehicleId, string partName, string description, DateTime recommendedDate, string status)
        {
            var recommendation = new Domain.Entities.Recommendation
            {
                VehicleId = vehicleId,
                PartName = partName,
                Description = description,
                RecommendedDate = recommendedDate,
                Status = status
            };
            await _recommendationRepository.CreateRecommendationAsync(recommendation);
            await _recommendationRepository.SaveChangesAsync();
            return recommendation;
        }

        public async Task<Domain.Entities.Recommendation?> GetRecommendationAsync(int recommendationId, int vehicleId)
        {
            return await _recommendationRepository.GetRecommendationAsync(recommendationId, vehicleId);
        }

        public async Task UpdateRecommendationStatusAsync(int recommendationId, string newStatus)
        {
            var recommendation = await _recommendationRepository.GetByIdAsync(recommendationId);
            if (recommendation == null)
                throw new KeyNotFoundException("Recommendation not found");

            if (string.IsNullOrWhiteSpace(newStatus))
                throw new InvalidOperationException("Status cannot be empty");

            recommendation.Status = newStatus;
            await _recommendationRepository.UpdateAsync(recommendation);
        }
    }
}
