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
    public class MechanicRepository : IMechanicRepository
    {
        private readonly AppDbContext _appDbContext;
        public MechanicRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<MechanicDetailsRequest>> GetMechanicDetails()
        {
            return await _appDbContext.Users.Select(u => new MechanicDetailsRequest
            {
                MechanicId = u.UserId,
                MechanicName = u.Name,
            }).ToListAsync();
        }
    }
}
