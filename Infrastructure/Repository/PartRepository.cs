using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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

        // Add more methods if needed, e.g. Add, Update, Delete...
    }
}
