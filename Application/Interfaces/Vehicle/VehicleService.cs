using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Vehicle
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }
        public async Task<List<VehicleRequest>> GetAllVehicles()
        {
            return await _vehicleRepository.GetAllVehicles();
        }
        public async Task<Domain.Entities.Vehicle> AddVehicleAsync(VehicleRegistrationDto vehicleRegistrationDto)
        {
            var vehicle = new Domain.Entities.Vehicle
            {
                OwnerId = vehicleRegistrationDto.OwnerId,
                LicensePlate = vehicleRegistrationDto.LicensePlate,
                Make = vehicleRegistrationDto.Make,
                Model = vehicleRegistrationDto.Model,
                Year = vehicleRegistrationDto.Year,
                VIN = vehicleRegistrationDto.VIN,
                Mileage = vehicleRegistrationDto.Mileage
            };
            var addedVehicle = await _vehicleRepository.AddAsync(vehicle);
            return addedVehicle;
        }
        public async Task<Domain.Entities.Vehicle> GetVehicleByIdAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            return vehicle;
        }
        public async Task<Domain.Entities.VehicleOwner?> GetCustomerByVehicleAsync(int vehicleId)
        {
            var vehicleOwner = await _vehicleRepository.GetCustomerByVehicleIdAsync(vehicleId);
            return vehicleOwner;
        }
    }
}
