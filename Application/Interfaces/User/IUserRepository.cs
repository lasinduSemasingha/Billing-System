using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.User
{
    public interface IUserRepository
    {
        Task<Domain.Entities.User?> GetByUsernameAsync(string username);
        Task CreateAsync(Domain.Entities.User user);
    }
}
