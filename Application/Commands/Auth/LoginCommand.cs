using Application.DTOs;
using MediatR;

namespace Application.Commands.Auth
{
    public class LoginCommand : IRequest<UserLoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
