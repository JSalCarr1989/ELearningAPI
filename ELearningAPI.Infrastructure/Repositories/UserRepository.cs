using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ELearningContext context;

        public UserRepository(ELearningContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByNameAndPassword(string username , string password)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.Email == username && u.Password == password);
        }
    }
}
