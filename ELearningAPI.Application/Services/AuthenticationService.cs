using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELearningAPI.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;
        public AuthenticationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async  Task<AuthResponseDto> Authenticate(LoginDto logInDto)
        {
            var user = await userRepository.GetUserByNameAndPassword(logInDto.UserName, logInDto.Password);

            if (user == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("this is my custom Secret key for authentication");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Type.ToString()),
                    new Claim("UserId",user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "elearning.com",
                Audience = "elearning.com"
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new AuthResponseDto
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = token.ValidTo
            };

        }
    }
}
