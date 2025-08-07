using Application.DTOs;
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

        public async Task<Service> CreateService(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _context.Services.FirstOrDefaultAsync(p => p.ServiceId == serviceId);
        }

        public async Task<List<ServiceRequest>> GetAllServicesAsync()
        {
            return await _context.Services
                .Select(p => new ServiceRequest
                {
                    ServiceId = p.ServiceId,
                    ServiceName = p.ServiceName,
                    ServiceDescription = p.ServiceDescription
                })
                .ToListAsync();
        }
    }
}
