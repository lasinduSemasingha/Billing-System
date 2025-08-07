using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Customer
{
    public interface ICustomerService
    {
        Task<CreateCustomerRequest> CustomerRegistration(CreateCustomerRequest request);
        Task<List<CustomerRequest>> GetAllCustomers();
    }
}
