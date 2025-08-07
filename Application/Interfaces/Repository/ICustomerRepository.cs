using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface ICustomerRepository
    {
        Task<VehicleOwner> CustomerRegistration(VehicleOwner owner);
        Task<List<CustomerRequest>> GetAllCustomers();
    }
}
