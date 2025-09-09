using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces.JobCard
{
    public class JobCardService : IJobCardService
    {
        private readonly IJobCardRepository _repository;
        private readonly IPartRepository _partRepository;

        public JobCardService(IJobCardRepository repository, IPartRepository partRepository)
        {
            _repository = repository;
            _partRepository = partRepository;
        }

        public async Task<Domain.Entities.JobCard> CreateAsync(JobCardDto dto)
        {
            // Check if vehicle already has an active (not completed) job card
            var activeJobCard = await _repository.GetActiveJobCardByVehicleIdAsync(dto.VehicleID);
            if (activeJobCard != null)
            {
                throw new InvalidOperationException("This vehicle already has an active job card and cannot create another one until it is completed.");
            }

            decimal totalPartsCost = 0;

            foreach (var part in dto.Parts)
            {
                var partEntity = await _partRepository.GetPartByIdAsync(part.PartId);
                if (partEntity != null)
                {
                    totalPartsCost += partEntity.UnitPrice * part.Quantity;
                }
            }

            decimal totalCost = (dto.LabourCost ?? 0) + totalPartsCost;

            var jobcardEntity = new Domain.Entities.JobCard
            {
                VehicleID = dto.VehicleID,
                MechanicID = dto.MechanicID,
                DateIn = dto.DateIn,
                DateOut = dto.DateOut,
                Mileage = dto.Mileage,
                AdvisoryReport = dto.AdvisoryReport,
                LabourHours = dto.LabourHours,
                LabourCost = dto.LabourCost,
                PartsCost = totalPartsCost,
                TotalCost = totalCost,
                Notes = dto.Notes,
                IsCompleted = false
            };

            var createdJobCard = await _repository.AddAsync(jobcardEntity);

            foreach (var serviceId in dto.ServiceIds)
            {
                var jobcardService = new Domain.Entities.JobCardService
                {
                    JobCardId = createdJobCard.JobCardID,
                    ServiceId = serviceId,
                    Requested = true,
                    Completed = false
                };

                await _repository.AddJobCardServiceAsync(jobcardService);
            }

            foreach (var part in dto.Parts)
            {
                var jobcardPart = new Domain.Entities.JobCardPart
                {
                    JobCardId = createdJobCard.JobCardID,
                    PartId = part.PartId,
                    Quantity = part.Quantity,
                    IsAdded = true
                };

                await _repository.AddJobCardPartAsync(jobcardPart);
            }

            return createdJobCard;
        }

        public async Task<Domain.Entities.JobCard> UpdateAsync(int id, JobCardUpdateDto dto)
        {
            var jobCard = await _repository.GetByIdAsync(id);
            if (jobCard == null) throw new KeyNotFoundException("JobCard not found");

            jobCard.DateOut = dto.DateOut ?? jobCard.DateOut;
            jobCard.LabourCost = dto.LabourCost ?? jobCard.LabourCost;
            jobCard.LabourHours = dto.LabourHours ?? jobCard.LabourHours;
            jobCard.Notes = dto.Notes ?? jobCard.Notes;
            jobCard.AdvisoryReport = dto.AdvisoryReport ?? jobCard.AdvisoryReport;
            jobCard.IsCompleted = true;

            if (dto.Parts != null && dto.Parts.Any())
            {
                decimal partsCost = 0;
                foreach (var partDto in dto.Parts)
                {
                    var partEntity = await _partRepository.GetPartByIdAsync(partDto.PartId);
                    if (partEntity != null)
                    {
                        partsCost += partEntity.UnitPrice * partDto.Quantity;

                        var existingPart = jobCard.JobCardParts.FirstOrDefault(p => p.PartId == partDto.PartId);
                        if (existingPart != null)
                            existingPart.Quantity = partDto.Quantity;
                        else
                        {
                            jobCard.JobCardParts.Add(new JobCardPart
                            {
                                PartId = partDto.PartId,
                                Quantity = partDto.Quantity,
                                JobCardId = jobCard.JobCardID,
                                IsAdded = true
                            });
                        }
                    }
                }
                jobCard.PartsCost = partsCost;
            }

            if (dto.Services != null && dto.Services.Any())
            {
                foreach (var serviceDto in dto.Services)
                {
                    var existingService = jobCard.JobCardServices.FirstOrDefault(s => s.ServiceId == serviceDto.ServiceId);
                    if (existingService != null)
                    {
                        existingService.Completed = serviceDto.Completed;
                        existingService.CompletionDate = serviceDto.CompletionDate;
                        existingService.Remarks = serviceDto.Remarks;
                    }
                }
            }

            jobCard.TotalCost = (jobCard.LabourCost ?? 0) + (jobCard.PartsCost ?? 0);

            return await _repository.UpdateAsync(jobCard);
        }
        public async Task<List<JobCardDetailDto>> GetIncompleteJobCardsAsync()
        {
            var jobCards = await _repository.GetIncompleteJobCardsAsync();

            return jobCards.Select(j => new JobCardDetailDto
            {
                JobCardID = j.JobCardID,
                VehicleID = j.VehicleID,
                MechanicID = j.MechanicID,
                DateIn = j.DateIn,
                DateOut = j.DateOut,
                IsCompleted = j.IsCompleted,
                Parts = j.JobCardParts.Select(p => new JobCardPartsDto
                {
                    PartId = p.PartId,
                    PartName = p.Part.PartName,
                    UnitPrice = p.Part.UnitPrice,
                    Quantity = p.Quantity
                }).ToList(),
                Services = j.JobCardServices.Select(s => new JobCardServiceDto
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.Service.ServiceName,
                    Requested = s.Requested,
                    Completed = s.Completed,
                    CompletionDate = s.CompletionDate,
                    Remarks = s.Remarks
                }).ToList()
            }).ToList();
        }
        public async Task<JobCardDetailDto> GetDetailsById(int jobId)
        {
            // Load JobCard with related parts and services including the actual Part and Service entities
            var jobCard = await _repository.GetByIdAsync(jobId);

            if (jobCard == null)
                throw new KeyNotFoundException("JobCard not found");

            // Map to DTO
            var result = new JobCardDetailDto
            {
                JobCardID = jobCard.JobCardID,
                VehicleID = jobCard.VehicleID,
                MechanicID = jobCard.MechanicID,
                DateIn = jobCard.DateIn,
                DateOut = jobCard.DateOut,
                Mileage = jobCard.Mileage,
                Notes = jobCard.Notes,
                IsCompleted = jobCard.IsCompleted,
                Parts = jobCard.JobCardParts.Select(p => new JobCardPartsDto
                {
                    PartId = p.PartId,
                    PartName = p.Part?.PartName ?? $"Part #{p.PartId}",
                    UnitPrice = p.Part?.UnitPrice ?? 0,
                    Quantity = p.Quantity
                }).ToList(),
                Services = jobCard.JobCardServices.Select(s => new JobCardServiceDto
                {
                    ServiceId = s.ServiceId,
                    ServiceName = s.Service?.ServiceName ?? $"Service #{s.ServiceId}",
                    Requested = s.Requested,
                    Completed = s.Completed,
                    CompletionDate = s.CompletionDate,
                    Remarks = s.Remarks
                }).ToList()
            };

            return result;
        }
    }
}
