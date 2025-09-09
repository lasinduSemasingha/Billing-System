using Application.DTOs;
using Application.Interfaces.Repository;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class JobCardRepository : IJobCardRepository
    {
        private readonly AppDbContext _context;

        public JobCardRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<JobCard> AddAsync(JobCard jobCard)
        {
            _context.JobCards.Add(jobCard);
            await _context.SaveChangesAsync();
            return jobCard;
        }
        public async Task<JobCardPart> AddJobCardPartAsync(JobCardPart jobCardPart)
        {
            _context.JobCardParts.Add(jobCardPart);
            await _context.SaveChangesAsync();
            return jobCardPart;
        }
        public async Task<JobCardService> AddJobCardServiceAsync(JobCardService jobCardService)
        {
            _context.JobCardServices.Add(jobCardService);
            await _context.SaveChangesAsync();
            return jobCardService;
        }
        public async Task<JobCard?> GetByIdAsync(int id)
        {
            return await _context.JobCards
                .Include(j => j.JobCardParts)
                    .ThenInclude(p => p.Part) // Include Part details
                .Include(j => j.JobCardServices)
                    .ThenInclude(s => s.Service) // Include Service details
                .FirstOrDefaultAsync(j => j.JobCardID == id);
        }
        public async Task<JobCard> UpdateAsync(JobCard jobCard)
        {
            _context.JobCards.Update(jobCard);
            await _context.SaveChangesAsync();
            return jobCard;
        }
        public async Task<List<JobCard>> GetIncompleteJobCardsAsync()
        {
            return await _context.JobCards
                .Where(j => j.IsCompleted == false)
                .Include(j => j.JobCardParts)
                    .ThenInclude(p => p.Part)
                .Include(j => j.JobCardServices)
                    .ThenInclude(s => s.Service)
                .ToListAsync();
        }
        public async Task<JobCard?> GetActiveJobCardByVehicleIdAsync(int vehicleId)
        {
            return await _context.JobCards
                .FirstOrDefaultAsync(jc => jc.VehicleID == vehicleId && (jc.IsCompleted == false));
        }
    }
}
