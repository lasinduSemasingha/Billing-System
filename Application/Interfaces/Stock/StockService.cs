using Application.DTOs;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InvoiceEntity = Domain.Entities.Invoice;

namespace Application.Interfaces.Stock
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _repository;

        public StockService(IStockRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<InvoiceDashboardDto>> GetFilteredInvoicesAsync(DateTime? date, int? month, int? year)
        {
            return await _repository.GetInvoiceDtosByDateAsync(date, month, year);
        }

        public async Task<StockSummaryDto> GetStockSummaryAsync() => await _repository.GetStockSummaryAsync();
    }
}
