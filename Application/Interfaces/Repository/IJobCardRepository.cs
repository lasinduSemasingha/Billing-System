using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IJobCardRepository
    {
        Task<Domain.Entities.JobCard> AddAsync(Domain.Entities.JobCard jobCard);
        Task<Domain.Entities.JobCardPart> AddJobCardPartAsync(Domain.Entities.JobCardPart jobCardPart);
        Task<Domain.Entities.JobCardService> AddJobCardServiceAsync(Domain.Entities.JobCardService jobCardService);
        Task<Domain.Entities.JobCard> UpdateAsync(Domain.Entities.JobCard jobCard);
        Task<Domain.Entities.JobCard?> GetByIdAsync(int id);
        Task<List<Domain.Entities.JobCard>> GetIncompleteJobCardsAsync();
        Task<Domain.Entities.JobCard?> GetActiveJobCardByVehicleIdAsync(int vehicleId);
    }
}
