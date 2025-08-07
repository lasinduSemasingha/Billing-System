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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _appDbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<VehicleOwner> CustomerRegistration(VehicleOwner owner)
        {
            _appDbContext.VehicleOwners.Add(owner);
            await _appDbContext.SaveChangesAsync();
            return owner;
        }
        public async Task<List<CustomerRequest>> GetAllCustomers()
        {
            return await _appDbContext.VehicleOwners
                .Select(p => new CustomerRequest
                {
                    Id = p.OwnerId,
                    FullName = p.FullName,
                    Phone = p.Phone,
                    Email = p.Email,
                    Address = p.Address
                })
                .ToListAsync();
        }
    }
}
