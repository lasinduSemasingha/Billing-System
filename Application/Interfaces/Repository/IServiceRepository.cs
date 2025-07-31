using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task<Service> GetServiceByIdAsync(int serviceId);
    }
}
