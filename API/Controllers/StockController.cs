using Application.DTOs;
using Application.Interfaces.Stock;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockService _service;
        public StockController(IStockService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<ActionResult<ServiceResponse>> GetSummary()
        {
            try
            {
                var summary = await _service.GetStockSummaryAsync();
                return Ok(new ServiceResponse(true, "Details fetched successfully", summary));
            }
            catch(Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
