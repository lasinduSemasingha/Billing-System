using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using InvoiceEntity = Domain.Entities.Invoice;

namespace Application.Interfaces.Repository
{
    public interface IStockRepository
    {
        Task<StockSummaryDto> GetStockSummaryAsync();
        Task<List<InvoiceDashboardDto>> GetInvoiceDtosByDateAsync(DateTime? date, int? month, int? year);
    }
}
