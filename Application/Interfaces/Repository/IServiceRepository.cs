using Application.DTOs;
using ServiceDomain = Domain.Entities.Service;

namespace Application.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task<Domain.Entities.Service> GetServiceByIdAsync(int serviceId);
        Task<List<ServiceRequest>> GetAllServicesAsync();
        Task<ServiceDomain> CreateService(ServiceDomain service);
    }
}
