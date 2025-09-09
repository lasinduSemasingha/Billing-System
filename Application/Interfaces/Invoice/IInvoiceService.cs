using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces.Invoice
{
    public interface IInvoiceService
    {
        Task<Domain.Entities.Invoice> CreateInvoiceAsync(int vehicleId, DateTime dateIssued, bool paidStatus, decimal totalAmount, decimal vatAmount, string notes);

        Task AddServiceToInvoiceAsync(int invoiceId, int serviceId, int quantity, decimal price);

        Task AddPartToInvoiceAsync(int invoiceId, int partId, int quantity, decimal unitPrice);
        Task AddManualPartToInvoiceAsync(int invoiceId, string manualPartName, int quantity, decimal unitPrice);
        Task AddManualServiceToInvoiceAsync(int invoiceId, string manualServiceName, int quantity, decimal unitPrice);

        Task SubmitInvoiceAsync(int invoiceId);

        Task<Domain.Entities.Invoice> GetInvoiceAsync(int invoiceId);
        Task<List<InvoiceRequest>> GetAllInvoices();
        Task<int> GetPartsCount();
        Task<int> GetServiceCount();
        Task<List<InvoiceReportDto>> GetInvoiceReportsAsync();
        Task<InvoiceReportDto> GetInvoiceReportAsync(int invoiceId);

    }
}
