using Application.DTOs;
using Application.Interfaces.Customer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> CreateCustomer(CreateCustomerRequest request)
        {
            try
            {
                await _customerService.CustomerRegistration(request);
                return Ok(new ServiceResponse(true, "Customer Registration Successful", request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse>> GetAllCustomers()
        {
            try
            {
                var request = await _customerService.GetAllCustomers();
                return Ok(new ServiceResponse(true, "Success", request));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
