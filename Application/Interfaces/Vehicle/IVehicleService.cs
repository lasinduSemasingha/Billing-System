using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Vehicle
{
    public interface IVehicleService
    {
        Task<List<VehicleRequest>> GetAllVehicles();
        Task<Domain.Entities.Vehicle> AddVehicleAsync(VehicleRegistrationDto vehicleRegistrationDto);
        Task<Domain.Entities.Vehicle> GetVehicleByIdAsync(int id);
        Task<Domain.Entities.VehicleOwner?> GetCustomerByVehicleAsync(int vehicleId);
    }
}
