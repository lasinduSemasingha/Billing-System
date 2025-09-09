using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Mechanic
{
    public interface IMechanicService
    {
        Task<List<MechanicDetailsRequest>> GetMechanicDetails();
    }
}
