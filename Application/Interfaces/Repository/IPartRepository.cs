using Application.DTOs;
using DomainPart = Domain.Entities.Part;

namespace Application.Interfaces.Repository
{
    public interface IPartRepository
    {
        Task<DomainPart> GetPartByIdAsync(int partId);
        Task<List<PartRequest>> GetAllPartsAsync();
        Task AddingPartsAsync(DomainPart part);
        Task DeletePartsById(int partId);
        Task UpdatePartsAsync(IEnumerable<DomainPart> parts);
    }
}
