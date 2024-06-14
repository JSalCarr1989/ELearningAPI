using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface IAnswerRepository : IGenericRepository<StudentAnswer>
    {
        Task<StudentAnswer> GetAnswerByUserAndQuestion(int userId, int questionId);

        Task<StudentAnswer> GetStudentAnswerById(int id);

        Task<List<StudentAnswer>> GetAnswersByStudentId(int userId);
 
    }
}
