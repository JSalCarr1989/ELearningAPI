using ELearningAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELearningAPI.Application.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto> Authenticate(LoginDto logInDto);
    }
}
