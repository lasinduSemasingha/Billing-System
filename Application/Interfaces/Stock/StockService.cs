using Application.DTOs;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }

        public async Task<StockSummaryDto> GetStockSummaryAsync() => await _repository.GetStockSummaryAsync();
    }
}
