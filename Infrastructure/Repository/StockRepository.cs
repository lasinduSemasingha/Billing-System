using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
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
        public async Task<List<InvoiceDashboardDto>> GetInvoiceDtosByDateAsync(DateTime? date, int? month, int? year)
        {
            var query = _context.Invoices
                .Include(i => i.Vehicle)
                .Include(i => i.InvoiceParts).ThenInclude(ip => ip.Part)
                .Include(i => i.InvoiceServices).ThenInclude(isv => isv.Service)
                .AsQueryable();

            if (date.HasValue)
            {
                query = query.Where(i => i.DateIssued.Date == date.Value.Date);
            }
            else
            {
                // If filtering by month and year, year is mandatory if month is provided
                if (month.HasValue)
                {
                    if (!year.HasValue)
                        throw new ArgumentException("Year must be specified when filtering by month.");

                    query = query.Where(i => i.DateIssued.Month == month.Value && i.DateIssued.Year == year.Value);
                }
                else if (year.HasValue)
                {
                    query = query.Where(i => i.DateIssued.Year == year.Value);
                }
            }

            var invoices = await query.ToListAsync();

            var invoiceDtos = invoices.Select(i => new InvoiceDashboardDto
            {
                InvoiceId = i.InvoiceId,
                InvoiceNumber = i.InvoiceNumber,
                DateIssued = i.DateIssued,
                VehicleRegistration = i.Vehicle?.LicensePlate ?? "",
                VehicleModel = i.Vehicle?.Model ?? "",
                TotalAmount = i.TotalAmount,
                VatAmount = i.VatAmount,
                PaidStatus = i.PaidStatus,

                Parts = i.InvoiceParts.Select(ip => new InvoiceDashboardPartDto
                {
                    PartId = ip.PartId,
                    PartName = ip.Part?.PartName ?? "",
                    Quantity = ip.quantity,
                    UnitPrice = ip.UnitPrice
                }).ToList(),

                Services = i.InvoiceServices.Select(isv => new InvoiceDashboardServiceDto
                {
                    ServiceId = isv.ServiceId,
                    ServiceName = isv.Service?.ServiceName ?? "",
                    Quantity = isv.Quantity,
                    Price = isv.Price
                }).ToList()
            }).ToList();

            return invoiceDtos;
        }


    }
}