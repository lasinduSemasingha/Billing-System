using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Service
{
    public interface IServiceService
    {
        //Task<Domain.Entities.Service> GetServiceByIdAsync(int serviceId);
        Task<List<ServiceRequest>> GetAllServicesAsync();
    }
}
