using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Auth
{
    public interface IAuthService
    {
        string GenerateJwtToken(Domain.Entities.User? user);
    }
}
