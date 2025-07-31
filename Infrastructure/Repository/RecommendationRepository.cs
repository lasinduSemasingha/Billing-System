using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RecommendationRepository : IRecommendationRepository
    {
        private readonly AppDbContext _context;

        public RecommendationRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task CreateRecommendationAsync(Recommendation recommendation)
        {
            await _context.Recommendations.AddAsync(recommendation);
        }

        public async Task<Recommendation?> GetRecommendationAsync(int recommendationId, int vehicleId)
        {
            return await _context.Recommendations
                .FirstOrDefaultAsync(r => r.RecommendationId == recommendationId && r.VehicleId == vehicleId);
        }

        //public async Task UpdateRecommendationAsync(int recommendationId)
        //{
        //    await _context.Recommendations.ExecuteUpdateAsync(recommendationId);
        //}
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Recommendation> GetByIdAsync(int id)
        {
            return await _context.Recommendations.FindAsync(id);
        }

        public async Task UpdateAsync(Recommendation recommendation)
        {
            _context.Recommendations.Update(recommendation);
            await _context.SaveChangesAsync();
        }
    }
}
