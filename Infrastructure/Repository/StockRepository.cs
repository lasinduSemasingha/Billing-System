using Application.DTOs;
using Application.Interfaces.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<StockSummaryDto> GetStockSummaryAsync()
        {
            var total = await _context.Parts.CountAsync();
            var inStock = await _context.Parts.CountAsync(p => p.StockQty > p.ReorderLevel);
            var lowStock = await _context.Parts.CountAsync(p => p.StockQty > 0 && p.StockQty <= p.ReorderLevel);
            var outOfStock = await _context.Parts.CountAsync(p => p.StockQty == 0);

            return new StockSummaryDto
            {
                TotalParts = total,
                InStock = inStock,
                LowStock = lowStock,
                OutOfStock = outOfStock
            };
        }
    }
}