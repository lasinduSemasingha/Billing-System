using Application.DTOs;
using Domain.Entities;
using DomainPart = Domain.Entities.Part;

namespace Application.Interfaces.Repository
{
    public interface IPartRepository
    {
        Task<DomainPart> GetPartByIdAsync(int partId);
        Task<List<PartRequest>> GetAllPartsAsync();
        Task<DomainPart> AddAsync(DomainPart part);
        Task DeletePartsById(int partId);
        Task UpdatePartsAsync(IEnumerable<DomainPart> parts);
        Task AddAsync(ManualPart manualPart);
    }
}
