using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson> GetLessonById(Guid id);

        Task<Lesson> GetLastLessonOnCourse(Guid courseId);
    }
}
