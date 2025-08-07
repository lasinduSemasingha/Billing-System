using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class PartRepository : IPartRepository
    {
        private readonly AppDbContext _context;

        public PartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Part> GetPartByIdAsync(int partId)
        {
            return await _context.Parts.FirstOrDefaultAsync(p => p.PartId == partId);
        }

        public async Task<List<PartRequest>> GetAllPartsAsync()
        {
            return await _context.Parts
                .Select(p => new PartRequest
                {
                    PartId = p.PartId,
                    PartName = p.PartName,
                    UnitPrice = p.UnitPrice,
                    StockQty = p.StockQty,
                    ReOrderLevel = p.ReorderLevel,
                    PartDescription = p.Description
                })
                .ToListAsync();
        }
        public async Task AddingPartsAsync(Part part)
        {
            //var part = new Part
            //{
            //    PartName = request.PartName,
            //    UnitPrice = request.UnitPrice,
            //    Description = request.PartDescription,
            //    StockQty = request.StockQty,
            //    ReorderLevel = request.ReOrderLevel
            //};
            await _context.AddAsync(part);
        }
        public async Task DeletePartsById(int partId)
        {
            var part = await _context.Parts.FindAsync(partId);

            if (part != null)
            {
                _context.Parts.Remove(part);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdatePartsAsync(IEnumerable<Part> parts)
        {
            foreach (var updatedPart in parts)
            {
                var existingPart = await _context.Parts.FindAsync(updatedPart.PartId);
                if (existingPart != null)
                {
                    existingPart.PartName = updatedPart.PartName;
                    existingPart.Description = updatedPart.Description;
                    existingPart.UnitPrice = updatedPart.UnitPrice;
                    existingPart.StockQty = updatedPart.StockQty;
                    existingPart.ReorderLevel = updatedPart.ReorderLevel;
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePartAsync(Part part)
        {
            _context.Parts.Update(part);
            await _context.SaveChangesAsync();
        }
        public async Task<Part> AddAsync(Part part)
        {
            _context.Parts.Add(part);
            await _context.SaveChangesAsync();
            return part;
        }
    }
}
