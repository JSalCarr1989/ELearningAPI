using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetUserByNameAndPassword(string username, string password);
    }
}
