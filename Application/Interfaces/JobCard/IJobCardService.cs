using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.JobCard
{
    public interface IJobCardService
    {
        Task<Domain.Entities.JobCard> CreateAsync(JobCardDto dto);
        Task<Domain.Entities.JobCard> UpdateAsync(int id, JobCardUpdateDto dto);
        Task<List<JobCardDetailDto>> GetIncompleteJobCardsAsync();
        Task<JobCardDetailDto> GetDetailsById(int jobId);
    }
}
