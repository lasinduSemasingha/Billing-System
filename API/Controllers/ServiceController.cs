using Application.DTOs;
using Application.Interfaces.Part;
using Application.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse>> GetAllParts()
        {
            var services = await _serviceService.GetAllServicesAsync();

            if (services == null || services.Count == 0)
            {
                return NotFound("No parts found.");
            }

            return Ok(new ServiceResponse(true, "Parts Got Successfully", services));
        }
        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> CreateService([FromBody] CreateServiceRequest request)
        {
            try
            {
                var service = await _serviceService.CreateService(request);
                return Ok(new ServiceResponse(true, "Service created successfully", service));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
