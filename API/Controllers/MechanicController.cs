using Application.DTOs;
using Application.Interfaces.Auth;
using Application.Interfaces.Mechanic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MechanicController : ControllerBase
    {
        private readonly IMechanicService _mechanicService;
        public MechanicController(IMechanicService mechanicService)
        {
            _mechanicService = mechanicService;
        }
        [HttpGet("All-Mechanics")]
        public async Task<ActionResult<ServiceResponse>> GetAllMechanics()
        {
            try
            {
                var mechanics = await _mechanicService.GetMechanicDetails();
                return Ok(new ServiceResponse(true, "Mechanics fetched successfully", mechanics));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
