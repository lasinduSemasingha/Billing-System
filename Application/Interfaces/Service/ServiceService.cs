using Application.DTOs;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
