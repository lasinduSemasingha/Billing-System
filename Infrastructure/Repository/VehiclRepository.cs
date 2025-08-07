using Application.DTOs;
using Application.Interfaces.Repository;
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

    }
}
