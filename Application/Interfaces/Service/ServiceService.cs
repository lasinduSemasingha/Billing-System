using Application.DTOs;
using Application.Interfaces.Repository;
using DomainService = Domain.Entities.Service;

namespace Application.Interfaces.Service
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<List<ServiceRequest>> GetAllServicesAsync()
        {
            var services = await _serviceRepository.GetAllServicesAsync();

            var serviceDtos = services.Select(s => new ServiceRequest
            {
                ServiceId = s.ServiceId,
                ServiceName = s.ServiceName,
                ServiceDescription = s.ServiceDescription
            }).ToList();

            return serviceDtos;
        }

        public async Task<CreateServiceRequest> CreateService(CreateServiceRequest request)
        {
            var service = new DomainService
            {
                ServiceName = request.ServiceName,
                ServiceDescription = request.ServiceDescription,
                BasePrice = request.BasePrice
            };

            var requestService = await _serviceRepository.CreateService(service);
            return request;
        }
    }
}
