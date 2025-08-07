using Application.DTOs;
using Application.Interfaces.Repository;
using DomainCustomer = Domain.Entities.VehicleOwner;

namespace Application.Interfaces.Customer
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<CreateCustomerRequest> CustomerRegistration(CreateCustomerRequest request)
        {
            var customer = new DomainCustomer
            {
                FullName = request.FullName,
                Phone = request.Phone,
                Email = request.Email,
                Address = request.Address
            };

            var creatingCustomer = await _customerRepository.CustomerRegistration(customer);
            return request;
        }
        public async Task<List<CustomerRequest>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }
    }
}
