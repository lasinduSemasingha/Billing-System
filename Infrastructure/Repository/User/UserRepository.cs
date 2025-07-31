using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Application.Interfaces.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task CreateAsync(Domain.Entities.User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
