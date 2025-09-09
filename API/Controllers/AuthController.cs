using Application.Commands.Auth;
using Application.DTOs;
using Application.Interfaces.Auth;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest request)
        {
            var result = await _mediator.Send(new LoginCommand
            {
                UserName = request.UserName,
                Password = request.Password
            });

            return Ok(result);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse>> Register([FromBody] RegisterUserRequest request)
        {
            var command = new RegisterUserCommand
            {
                Name = request.Name,
                UserName = request.Username,
                Email = request.Email,
                Password = request.Password,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _mediator.Send(command);
            return Ok(new ServiceResponse(true, "User Registration Successful", result));
        }
    }
}
