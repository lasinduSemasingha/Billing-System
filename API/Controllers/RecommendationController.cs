using Application.DTOs;
using Application.Interfaces.Recommendation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;
        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpGet("{vehicleId}/recommendations/{recommendationId}")]
        public async Task<ActionResult<ServiceResponse>> GetRecommendation(int vehicleId, int recommendationId)
        {
            try
            {
                var recommendation = await _recommendationService.GetRecommendationAsync(recommendationId, vehicleId);

                if (recommendation == null)
                {
                    return NotFound(new ServiceResponse(false, "Values Not Found", null));
                }

                return Ok(new ServiceResponse(true, "Values Found", recommendation));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ServiceResponse(false, ex.Message, null));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ServiceResponse(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ServiceResponse(false, ex.Message, null));
            }
        }

        [HttpPost("recommendations")]
        public async Task<ActionResult<ServiceResponse>> CreateRecommendation([FromBody] AddRecommendationRequest request)
        {
            try
            {
                var createdRecommendation = await _recommendationService
                .CreateRecommendationAsync(
                    request.VehicleId, 
                    request.PartName, 
                    request.Description, 
                    request.RecommendedDate, 
                    request.Status
                );

                return Ok(new ServiceResponse(true, "Recommendation Added Successfully", createdRecommendation));
            }
            catch (InvalidOperationException ex)
            {
                return UnprocessableEntity(new ServiceResponse(false, ex.Message, null));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ServiceResponse(false, ex.Message, null));
            }
        }

    }
}
