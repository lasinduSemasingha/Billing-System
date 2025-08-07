using Application.Interfaces.Invoice;
using Application.Interfaces.Repository;
using Application.Interfaces.Stock;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IInvoiceService _service;
        private readonly IStockService _stockService;
        public ReportController(IInvoiceService service, IStockService stockService)
        {
            _service = service;
            _stockService = stockService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _service.GetInvoiceReportsAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _service.GetInvoiceReportAsync(id);
            if (report == null)
                return NotFound();

            return Ok(report);
        }
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredInvoices([FromQuery] DateTime? date, [FromQuery] int? month, [FromQuery] int? year)
        {
            try
            {
                var data = await _stockService.GetFilteredInvoicesAsync(date, month, year);
                return Ok(new { values = data });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Failed to get report", error = ex.Message });
            }
        }
    }
}
