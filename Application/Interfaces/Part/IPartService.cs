using Application.DTOs;
using DomainPart = Domain.Entities.Part;

namespace Application.Interfaces.Part
{
    public interface IPartService
    {
        Task<List<PartRequest>> GetAllPartsAsync();
        Task<DomainPart> GetPartByIdAsync(int partId);
    }
}
