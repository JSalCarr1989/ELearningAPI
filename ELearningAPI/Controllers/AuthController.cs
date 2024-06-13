using ELearningAPI.Application.Contracts;
using ELearningAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ELearningAPI.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        public AuthController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto login)
        {
            var response  = await authenticationService.Authenticate(login);

            if (response == null)
                return Unauthorized();

            return Ok(response);    
        }

    }
}
