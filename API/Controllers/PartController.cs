using Application.DTOs;
using Application.Interfaces.Part;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartService _partService;

        public PartController(IPartService partService)
        {
            _partService = partService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse>> GetAllParts()
        {
            var parts = await _partService.GetAllPartsAsync();

            if (parts == null || parts.Count == 0)
            {
                return NotFound("No parts found.");
            }

            return Ok(new ServiceResponse(true,"Parts Got Successfully", parts));
        }
    }
}
