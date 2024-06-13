using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IAnswerRepository : IGenericRepository<StudentAnswer>
    {
        Task<StudentAnswer> GetAnswerByUserAndQuestion(Guid userId, Guid questionId);

        Task<StudentAnswer> GetStudentAnswerById(Guid id);
    }
}
