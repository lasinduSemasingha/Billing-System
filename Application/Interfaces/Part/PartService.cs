using Application.DTOs;
using Application.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Part
{
    public class PartService : IPartService
    {
        private readonly IPartRepository _partRepository;

        public PartService(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }

        public async Task<List<PartRequest>> GetAllPartsAsync()
        {
            var parts = await _partRepository.GetAllPartsAsync();

            var partDtos = parts.Select(p => new PartRequest
            {
                PartId = p.PartId,
                PartName = p.PartName,
                UnitPrice = p.UnitPrice,
                PartDescription = p.PartDescription
            }).ToList();

            return partDtos;
        }
    }
}
