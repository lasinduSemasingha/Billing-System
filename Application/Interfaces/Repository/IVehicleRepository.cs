using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IVehicleRepository
    {
        Task<List<VehicleRequest>> GetAllVehicles();
        Task<Domain.Entities.Vehicle> AddAsync(Domain.Entities.Vehicle vehicle);
        Task<Domain.Entities.Vehicle> GetByIdAsync(int id);
        Task<Domain.Entities.VehicleOwner?> GetCustomerByVehicleIdAsync(int vehicleId);
    }
}
