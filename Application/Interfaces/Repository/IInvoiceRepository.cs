using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IInvoiceRepository
    {
        Task AddInvoiceAsync(Domain.Entities.Invoice invoice);

        Task<Domain.Entities.Invoice> GetInvoiceWithItemsAsync(int invoiceId);

        Task SaveChangesAsync();

        Task<string?> GetLastInvoiceNumberAsync();
        Task<List<InvoiceRequest>> GetAllInvoices();
        Task<int> GetPartsCount();
        Task<int> GetServiceCount();
        Task<List<InvoiceReportDto>> GetAllInvoiceReportsAsync();
        Task<InvoiceReportDto> GetInvoiceReportByIdAsync(int invoiceId);
    }
}
