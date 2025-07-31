using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Invoice;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceService _invoiceService;
        private static int _serviceItemCounter = 1;
        private static int _partItemCounter = 1;

        public InvoiceController(IInvoiceService service)
        {
            _invoiceService = service;
        }
        // In-memory storage
        private static readonly Dictionary<int, InvoiceResponse> Invoices = new();

        [HttpPost]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            if (request == null || request.VehicleId <= 0)
            {
                return BadRequest("Invalid request data.");
            }

            var invoice = await _invoiceService.CreateInvoiceAsync(request.VehicleId, request.DateIssued, request.PaidStatus, request.TotalAmount, request.Notes);

            var response = new InvoiceResponse
            {
                InvoiceId = invoice.InvoiceId,
                VehicleId = invoice.VehicleId,
                DateIssued = invoice.DateIssued,
                TotalAmount = invoice.TotalAmount,
                PaidStatus = invoice.PaidStatus,
                Notes = invoice.Notes
            };

            return CreatedAtAction(nameof(GetInvoice), new { invoiceId = response.InvoiceId }, response);
        }

        //[HttpPost("{invoiceId}/services")]
        //public IActionResult AddService(int invoiceId, [FromBody] AddServiceRequest request)
        //{
        //    if (!Invoices.TryGetValue(invoiceId, out var invoice))
        //        return NotFound("Invoice not found.");

        //    var serviceItem = new ServiceItemResponse
        //    {
        //        Id = _serviceItemCounter++,
        //        ServiceId = request.ServiceId,
        //        Description = request.Description,
        //        Quantity = request.Quantity,
        //        UnitPrice = request.UnitPrice
        //    };

        //    invoice.Services.Add(serviceItem);
        //    return CreatedAtAction(nameof(GetInvoice), new { invoiceId = invoice.InvoiceId }, serviceItem);
        //}

        //[HttpPost("{invoiceId}/parts")]
        //public IActionResult AddPart(int invoiceId, [FromBody] AddPartRequest request)
        //{
        //    if (!Invoices.TryGetValue(invoiceId, out var invoice))
        //        return NotFound("Invoice not found.");

        //    var partItem = new PartItemResponse
        //    {
        //        Id = _partItemCounter++,
        //        PartId = request.PartId,
        //        Description = request.Description,
        //        Quantity = request.Quantity,
        //        UnitPrice = request.UnitPrice
        //    };

        //    invoice.Parts.Add(partItem);
        //    return CreatedAtAction(nameof(GetInvoice), new { invoiceId = invoice.InvoiceId }, partItem);
        //}

        //[HttpPost("{invoiceId}/submit")]
        //public IActionResult SubmitInvoice(int invoiceId)
        //{
        //    if (!Invoices.TryGetValue(invoiceId, out var invoice))
        //        return NotFound("Invoice not found.");

        //    if (!invoice.Services.Any() && !invoice.Parts.Any())
        //        return BadRequest("Invoice must contain at least one service or part before submitting.");

        //    invoice.Status = "Submitted";
        //    return Ok(new
        //    {
        //        invoice.InvoiceId,
        //        invoice.Status,
        //        SubmittedAt = DateTime.UtcNow
        //    });
        //}

        [HttpGet("{invoiceId}")]
        public IActionResult GetInvoice(int invoiceId)
        {
            if (!Invoices.TryGetValue(invoiceId, out var invoice))
                return NotFound("Invoice not found.");

            return Ok(invoice);
        }
    }
}
