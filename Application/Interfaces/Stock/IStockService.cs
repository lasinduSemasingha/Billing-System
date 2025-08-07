using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using InvoiceEntity = Domain.Entities.Invoice;

namespace Application.Interfaces.Stock
{
    public interface IStockService
    {
        Task<StockSummaryDto> GetStockSummaryAsync();
        Task<List<InvoiceDashboardDto>> GetFilteredInvoicesAsync(DateTime? date, int? month, int? year);
    }
}
