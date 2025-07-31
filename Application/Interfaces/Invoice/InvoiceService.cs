using Application.Interfaces.Repository;

namespace Application.Interfaces.Invoice
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IPartRepository _partRepository;
        private readonly IServiceRepository _serviceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository,
                              IPartRepository partRepository,
                              IServiceRepository serviceRepository)
        {
            _invoiceRepository = invoiceRepository;
            _partRepository = partRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<Domain.Entities.Invoice> CreateInvoiceAsync(int vehicleId, DateTime dateIssued, bool paidStatus, decimal totalAmount, string notes)
        {
            var invoice = new Domain.Entities.Invoice
            {
                VehicleId = vehicleId,
                DateIssued = dateIssued,
                PaidStatus = paidStatus,
                TotalAmount = totalAmount,
                Notes = notes,
                InvoiceParts = new List<Domain.Entities.InvoicePart>(),
                InvoiceServices = new List<Domain.Entities.InvoiceService>()
            };

            await _invoiceRepository.AddInvoiceAsync(invoice);
            await _invoiceRepository.SaveChangesAsync();
            return invoice;
        }

        public async Task AddServiceToInvoiceAsync(int invoiceId, int serviceId, int quantity, decimal price)
        {
            var invoice = await _invoiceRepository.GetInvoiceWithItemsAsync(invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            var service = await _serviceRepository.GetServiceByIdAsync(serviceId);
            if (service == null) throw new Exception("Service not found");

            var invoiceService = new Domain.Entities.InvoiceService
            {
                InvoiceId = invoiceId,
                ServiceId = serviceId,
                Quantity = quantity,
                Price = price,
                Service = service
            };

            invoice.InvoiceServices.Add(invoiceService);
            UpdateTotalAmount(invoice);

            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task AddPartToInvoiceAsync(int invoiceId, int partId, int quantity, decimal unitPrice)
        {
            var invoice = await _invoiceRepository.GetInvoiceWithItemsAsync(invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            var part = await _partRepository.GetPartByIdAsync(partId);
            if (part == null) throw new Exception("Part not found");

            if (part.StockQty < quantity)
                throw new InvalidOperationException("Not enough stock available");

            part.StockQty -= quantity;

            var invoicePart = new Domain.Entities.InvoicePart
            {
                InvoiceId = invoiceId,
                PartId = partId,
                quantity = quantity,
                UnitPrice = unitPrice,
                Part = part
            };

            invoice.InvoiceParts.Add(invoicePart);
            UpdateTotalAmount(invoice);

            await _invoiceRepository.SaveChangesAsync();
        }

        public async Task SubmitInvoiceAsync(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceWithItemsAsync(invoiceId);
            if (invoice == null) throw new Exception("Invoice not found");

            if ((invoice.InvoiceParts == null || !invoice.InvoiceParts.Any()) &&
                (invoice.InvoiceServices == null || !invoice.InvoiceServices.Any()))
            {
                throw new Exception("Invoice must have at least one service or part before submitting.");
            }

            invoice.PaidStatus = true;

            await _invoiceRepository.SaveChangesAsync();
        }

        private void UpdateTotalAmount(Domain.Entities.Invoice invoice)
        {
            decimal servicesTotal = invoice.InvoiceServices?.Sum(s => s.Price * s.Quantity) ?? 0m;
            decimal partsTotal = invoice.InvoiceParts?.Sum(p => p.UnitPrice * p.quantity) ?? 0m;
            invoice.TotalAmount = servicesTotal + partsTotal;
        }

        public async Task<Domain.Entities.Invoice> GetInvoiceAsync(int invoiceId)
        {
            return await _invoiceRepository.GetInvoiceWithItemsAsync(invoiceId);
        }

        public async Task<int> GetLatestInvoiceId()
        {
            return await _invoiceRepository.GetLatestInvoiceId();
        }

    }
}
