using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
namespace Application.Interfaces.Invoice
{
    public interface IInvoiceService
    {
        Task<Domain.Entities.Invoice> CreateInvoiceAsync(int vehicleId, DateTime dateIssued, bool paidStatus, decimal totalAmount, string notes);

        Task AddServiceToInvoiceAsync(int invoiceId, int serviceId, int quantity, decimal price);

        Task AddPartToInvoiceAsync(int invoiceId, int partId, int quantity, decimal unitPrice);

        Task SubmitInvoiceAsync(int invoiceId);

        Task<Domain.Entities.Invoice> GetInvoiceAsync(int invoiceId);

    }
}
