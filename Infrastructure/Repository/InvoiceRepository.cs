using Application.Interfaces.Invoice;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repository;

namespace Infrastructure.Repository
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context;

        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddInvoiceAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
        }

        public async Task<int> GetLatestInvoiceId()
        {
            var latestId = await _context.Invoices
                .OrderByDescending(i => i.InvoiceId)
                .Select(i => i.InvoiceId)
                .FirstOrDefaultAsync();

            return latestId;
        }


        public async Task<Invoice> GetInvoiceWithItemsAsync(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.InvoiceParts)
                    .ThenInclude(ip => ip.Part)
                .Include(i => i.InvoiceServices)
                    .ThenInclude(ie => ie.Service)
                .FirstOrDefaultAsync(i => i.InvoiceId == invoiceId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
