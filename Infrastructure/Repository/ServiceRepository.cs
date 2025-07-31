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
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;

        public ServiceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        // Add more methods if needed, e.g. Add, Update, Delete...
    }
}
