using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface ILessonRepository : IGenericRepository<Lesson>
    {
        Task<Lesson> GetLessonById(int id);

        Task<Lesson> GetLastLessonOnCourse(int courseId);


        Task<List<Lesson>> GetLessonsByCourseId(int courseId);

    }
}
