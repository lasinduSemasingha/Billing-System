using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Interfaces.Invoice;
using Domain.Entities;

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
            // 1️⃣ Create the invoice
            var invoice = await _invoiceService.CreateInvoiceAsync(
                request.VehicleId,
                request.DateIssued,
                request.PaidStatus,
                request.TotalPrice,
                request.VatAmount,
                request.Notes
            );

            // 2️⃣ Add parts (stock or manual)
            foreach (var part in request.Parts)
            {
                if (part.PartId > 0)
                {
                    // Stock part
                    await _invoiceService.AddPartToInvoiceAsync(invoice.InvoiceId, part.PartId, part.Quantity, part.UnitPrice);
                }
                else if (!string.IsNullOrWhiteSpace(part.ManualPartName))
                {
                    // Manual part
                    await _invoiceService.AddManualPartToInvoiceAsync(invoice.InvoiceId, part.ManualPartName, part.Quantity, part.UnitPrice);
                }
            }

            // 3️⃣ Add services
            foreach (var service in request.Services)
            {
                if (service.ServiceId > 0)
                {
                    await _invoiceService.AddServiceToInvoiceAsync(invoice.InvoiceId, service.ServiceId, service.Quantity, service.Price);
                }
                else if (!string.IsNullOrWhiteSpace(service.ManualServiceName))
                {
                    await _invoiceService.AddManualServiceToInvoiceAsync(invoice.InvoiceId, service.ManualServiceName, service.Quantity, service.Price);
                }
            }

            // 4️⃣ Return the created invoice
            return Ok(invoice);
        }


        [HttpGet("GetAllInvoices")]
        public async Task<ActionResult<ServiceResponse>> GetAllInvoices()
        {
            try
            {
                var invoices = await _invoiceService.GetAllInvoices();
                return Ok(new
                ServiceResponse
                (
                    true,
                    "Invoices retrieved successfully.",
                    invoices
                ));
            }
            catch (Exception ex)
            {
                // You can log the exception here if needed
                return StatusCode(500, new
                ServiceResponse
                (
                    false,
                    ex.Message,
                    null
                ));
            }
        }
        [HttpGet("SoldPartsCount")]
        public async Task<IActionResult> GetAllSoldParts()
        {
            try
            {
                int partsCount = await _invoiceService.GetPartsCount();
                return Ok(new { count = partsCount });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("SoldServiceCount")]
        public async Task<IActionResult> GetAllSoldServices()
        {
            try
            {
                int servicesCount = await _invoiceService.GetServiceCount();
                return Ok(new { count = servicesCount });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
