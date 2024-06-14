using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IQuestionRepository : IGenericRepository<Question>
    {
        Task<Question> GetQuestionById(int id);
    }
}
