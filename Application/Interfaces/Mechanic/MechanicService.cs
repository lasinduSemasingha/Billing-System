using Application.DTOs;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Mechanic
{
    public class MechanicService : IMechanicService
    {
        private readonly IMechanicRepository _mechanicRepository;
        public MechanicService(IMechanicRepository mechanicRepository)
        {
            _mechanicRepository = mechanicRepository;
        }
        public Task<List<MechanicDetailsRequest>> GetMechanicDetails()
        {
            var mechanics = _mechanicRepository.GetMechanicDetails();
            return mechanics;
        }
    }
}
