using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IMechanicRepository
    {
        Task<List<MechanicDetailsRequest>> GetMechanicDetails();
    }
}
