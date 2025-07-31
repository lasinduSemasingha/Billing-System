using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Auth;
using Application.Interfaces.User;

namespace Application.Commands.Auth
{
    public class LoginHandler : IRequestHandler<LoginCommand, UserLoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<UserLoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsernameAsync(request.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials.");

            var token = _authService.GenerateJwtToken(user);

            return new UserLoginResponse
            {
                Token = token,
                UserName = user.UserName,
                Role = "User" // or user.Role if available
            };
        }
    }
}
