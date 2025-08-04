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

        public InvoiceController(IInvoiceService service)
        {
            _invoiceService = service;
        }

        private static readonly Dictionary<int, InvoiceResponse> Invoices = new();

        [HttpPost]
        public async Task<ServiceResponse> CreateInvoice([FromBody] CreateInvoiceRequest request)
        {
            if (request == null || request.VehicleId <= 0)
            {
                return new ServiceResponse(true, "Invalid request data.", null);
            }

            var invoice = await _invoiceService.CreateInvoiceAsync(request.VehicleId, request.DateIssued, request.PaidStatus, request.TotalAmount, request.VatAmount, request.Notes);

            var response = new InvoiceResponse
            {
                InvoiceId = invoice.InvoiceId,
                VehicleId = invoice.VehicleId,
                DateIssued = invoice.DateIssued,
                TotalAmount = invoice.TotalAmount,
                VatAmount = invoice.VatAmount,
                PaidStatus = invoice.PaidStatus,
                Notes = invoice.Notes
            };

            return new ServiceResponse(true, "Invoice Created Successfully", response);
        }

        [HttpPost("services")]
        public async Task<ActionResult<ServiceResponse>> AddService([FromBody] AddServiceRequest request)
        {
            try
            {
                await _invoiceService.AddServiceToInvoiceAsync(
                    request.InvoiceId,
                    request.ServiceId,
                    request.Quantity,
                    request.Price
                );

                var serviceItem = new ServiceItemResponse
                {
                    ServiceId = request.ServiceId,
                    InvoiceId = request.InvoiceId,
                    Quantity = request.Quantity,
                    Price = request.Price
                };

                return Ok(new ServiceResponse(true, "Service added successfully", serviceItem));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ServiceResponse(false, ex.Message, null));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse(false, ex.Message, null));
            }
        }


        [HttpPost("parts")]
        public async Task<ActionResult<ServiceResponse>> AddPart([FromBody] AddPartRequest request)
        {
            try
            {
                await _invoiceService.AddPartToInvoiceAsync(
                    request.InvoiceId,
                    request.PartId,
                    request.Quantity,
                    request.UnitPrice
                );
                var partItem = new PartItemResponse
                {
                    PartId = request.PartId,
                    InvoicePartId = request.InvoiceId,
                    Quantity = request.Quantity,
                    UnitPrice = request.UnitPrice
                };

                return Ok(new ServiceResponse(true, "Part added successfully", partItem));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ServiceResponse(false, ex.Message, null));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse(false, ex.Message, null));
            }
        }

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

        [HttpPost("create-full")]
        public async Task<IActionResult> CreateFullInvoice([FromBody] CreateFullInvoiceRequest request)
        {
            var invoice = await _invoiceService.CreateInvoiceAsync(request.VehicleId, request.DateIssued, request.PaidStatus, request.TotalPrice, request.VatAmount, request.Notes);

            foreach (var part in request.Parts)
            {
                await _invoiceService.AddPartToInvoiceAsync(invoice.InvoiceId, part.PartId, part.Quantity, part.UnitPrice);
            }

            foreach (var service in request.Services)
            {
                await _invoiceService.AddServiceToInvoiceAsync(invoice.InvoiceId, service.ServiceId, service.Quantity, service.Price);
            }

            return Ok(invoice);
        }

    }
}
