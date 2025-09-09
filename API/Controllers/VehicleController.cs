using Application.DTOs;
using Application.Interfaces.Vehicle;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse>> GetAllVehicles()
        {
            try
            {
                var vehicles = await _vehicleService.GetAllVehicles();
                return Ok(new ServiceResponse(true, "Success", vehicles));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddVehicle([FromBody] VehicleRegistrationDto dto)
        {
            try
            {
                var vehicle = await _vehicleService.AddVehicleAsync(dto);
                return Ok(new ServiceResponse(true, "Success", vehicle));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<ServiceResponse>> GetVehicleDetailsById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
                return Ok(new ServiceResponse(true, "Success", vehicle));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
        [HttpGet("GetByOwner")]
        public async Task<ActionResult<ServiceResponse>> GetByVehicleOwner(int id)
        {
            try
            {
                var vehicleOwner = await _vehicleService.GetCustomerByVehicleAsync(id);
                return Ok(new ServiceResponse(true, "Success", vehicleOwner));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
