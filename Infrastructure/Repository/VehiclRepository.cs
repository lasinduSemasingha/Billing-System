using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class VehiclRepository : IVehicleRepository
    {
        private readonly AppDbContext _appDbContext;
        public VehiclRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<VehicleRequest>> GetAllVehicles()
        {
            return await _appDbContext.Vehicles
                .Select(v => new VehicleRequest
                {
                    VehicleId = v.VehicleId,
                    OwnerId = v.OwnerId,
                    LicensePlate = v.LicensePlate,
                    Make = v.Make,
                    Model = v.Model,
                    Year = v.Year,
                    VIN = v.VIN,
                    Mileage = v.Mileage
                })
                .ToListAsync();
        }
        public async Task<Domain.Entities.Vehicle> AddAsync(Domain.Entities.Vehicle vehicle)
        {
            await _appDbContext.Vehicles.AddAsync(vehicle);
            _appDbContext.SaveChanges();
            return vehicle;
        }
        public async Task<Domain.Entities.Vehicle> GetByIdAsync(int id)
        {
            var result = await _appDbContext.Vehicles.FirstOrDefaultAsync(v => v.VehicleId == id);
            return result;
        }

        public async Task<VehicleOwner?> GetCustomerByVehicleIdAsync(int vehicleId)
        {
            return await _appDbContext.Vehicles
                .Where(v => v.VehicleId == vehicleId)
                .Select(v => v.VehicleOwner)  // assuming Vehicle → Customer navigation
                .FirstOrDefaultAsync();
        }
    }
}
