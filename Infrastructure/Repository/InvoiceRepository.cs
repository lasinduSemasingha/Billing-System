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
using Application.DTOs;

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

        public async Task<string?> GetLastInvoiceNumberAsync()
        {
            return await _context.Invoices
                .OrderByDescending(i => i.InvoiceId)
                .Select(i => i.InvoiceNumber)
                .FirstOrDefaultAsync();
        }

        public async Task<List<InvoiceRequest>> GetAllInvoices()
        {
            return await _context.Invoices
                .Select(i => new InvoiceRequest
                {
                    Customer = i.Vehicle.VehicleOwner.FullName,
                    InvoiceNumber = i.InvoiceNumber,
                    DateIssued = i.DateIssued,
                    PaidStatus = i.PaidStatus,
                    Notes = i.Notes,
                    TotalAmount = i.TotalAmount + i.VatAmount
                })
                .ToListAsync();
        }

        public async Task<int> GetServiceCount()
        {
            return await _context.InvoiceServices.CountAsync();
        }

        public async Task<int> GetPartsCount()
        {
            return await _context.InvoiceParts.CountAsync();
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
        public async Task<List<InvoiceReportDto>> GetAllInvoiceReportsAsync()
        {
            return await _context.Invoices
                .Include(i => i.Vehicle)
                .Include(i => i.InvoiceParts).ThenInclude(ip => ip.Part)
                .Include(i => i.InvoiceServices).ThenInclude(isv => isv.Service)
                .Select(i => new InvoiceReportDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    DateIssued = i.DateIssued,
                    TotalAmount = i.TotalAmount,
                    VatAmount = i.VatAmount,
                    PaidStatus = i.PaidStatus,
                    VehicleInfo = i.Vehicle.Model,  // adjust as per your Vehicle entity
                    Parts = i.InvoiceParts.Select(ip => new InvoicesPartDto
                    {
                        PartId = ip.PartId,
                        PartName = ip.Part.PartName,
                        Quantity = ip.quantity,
                        UnitPrice = ip.UnitPrice
                    }).ToList(),
                    Services = i.InvoiceServices.Select(isv => new InvoicesServiceDto
                    {
                        ServiceId = isv.ServiceId,
                        ServiceName = isv.Service.ServiceName,
                        ServicePrice = isv.Price // or however you store price
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<InvoiceReportDto> GetInvoiceReportByIdAsync(int invoiceId)
        {
            return await _context.Invoices
                .Include(i => i.Vehicle)
                .Include(i => i.InvoiceParts).ThenInclude(ip => ip.Part)
                .Include(i => i.InvoiceServices).ThenInclude(isv => isv.Service)
                .Where(i => i.InvoiceId == invoiceId)
                .Select(i => new InvoiceReportDto
                {
                    InvoiceId = i.InvoiceId,
                    InvoiceNumber = i.InvoiceNumber,
                    DateIssued = i.DateIssued,
                    TotalAmount = i.TotalAmount,
                    VatAmount = i.VatAmount,
                    PaidStatus = i.PaidStatus,
                    VehicleInfo = i.Vehicle.Model,
                    Parts = i.InvoiceParts.Select(ip => new InvoicesPartDto
                    {
                        PartId = ip.PartId,
                        PartName = ip.Part.PartName,
                        Quantity = ip.quantity,
                        UnitPrice = ip.UnitPrice
                    }).ToList(),
                    Services = i.InvoiceServices.Select(isv => new InvoicesServiceDto
                    {
                        ServiceId = isv.ServiceId,
                        ServiceName = isv.Service.ServiceName,
                        ServicePrice = isv.Price
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
    }
}
