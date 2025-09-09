using Application.DTOs;
using Application.Interfaces.JobCard;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobCardController : ControllerBase
    {
        private readonly IJobCardService _jobCardService;

        public JobCardController(IJobCardService jobCardService)
        {
            _jobCardService = jobCardService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] JobCardDto dto)
        {
            if (dto == null)
                return BadRequest(new ServiceResponse(false, "Invalid JobCard data", null));

            try
            {
                var createdJobCard = await _jobCardService.CreateAsync(dto);

                var response = new ServiceResponse(
                    true,
                    "Job Card Created Successfully",
                    createdJobCard
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ServiceResponse(
                    false,
                    $"Error creating Job Card: {ex.Message}",
                    null
                );

                return BadRequest(response);
            }
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobCardUpdateDto dto)
        {
            try
            {
                var updatedJobCard = await _jobCardService.UpdateAsync(id, dto);
                return Ok(new ServiceResponse(true, "JobCard updated successfully", updatedJobCard));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
        [HttpGet("incomplete")]
        public async Task<IActionResult> GetIncompleteJobCards()
        {
            try
            {
                var jobCards = await _jobCardService.GetIncompleteJobCardsAsync();
                return Ok(new ServiceResponse(true, "JobCards fetched successfully", jobCards));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetJobCard(int id)
        {
            try
            {
                var jobCard = await _jobCardService.GetDetailsById(id);
                return Ok(new ServiceResponse(true, "JobCards fetched successfully", jobCard));
            }
            catch (Exception ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
        }
    }
}
