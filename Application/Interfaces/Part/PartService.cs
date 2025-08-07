using Application.DTOs;
using DomainPart = Domain.Entities.Part;
using Application.Interfaces.Repository;
using Domain.Entities;

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
                StockQty = p.StockQty,
                ReOrderLevel = p.ReOrderLevel,
                PartDescription = p.PartDescription
            }).ToList();

            return partDtos;
        }
        public async Task<DomainPart> GetPartByIdAsync(int partId)
        {
            var part = await _partRepository.GetPartByIdAsync(partId);

            if (part == null)
            {
                throw new KeyNotFoundException($"Part with ID {partId} was not found.");
            }

            var partDto = new DomainPart
            {
                PartId = part.PartId,
                PartName = part.PartName,
                UnitPrice = part.UnitPrice,
                Description = part.Description,
                StockQty = part.StockQty,
                ReorderLevel = part.ReorderLevel
            };

            return partDto;
        }
        public async Task<DomainPart> CreatePartAsync(CreatePartRequest request)
        {
            var partDto = new DomainPart
            {
                PartName = request.PartName,
                Description = request.PartDescription,
                UnitPrice = request.UnitPrice,
                StockQty = request.StockQty,
                ReorderLevel = request.ReOrderLevel

            };
            var created = await _partRepository.AddAsync(partDto);
            return created;
        }
    }
}
