﻿using ELearningAPI.Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace ELearningAPI.Application.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = httpContextAccessor.HttpContext?.User?.FindFirst("UserId");
            return userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId)
                ? userId
                : 0;
        }
    }
}
