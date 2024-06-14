using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetCourseById(int id);
        Task<Course> GetLastCourse(int userId);
        Task<List<Course>> GetCoursesByQuestionsIdsAsync(IEnumerable<int> questionIds);
        Task<List<Course>> GetCoursesWithoutCorrelations();
        Task<List<Course>> GetCoursesByCorrelatedCourse(int relatedCourseId);

    }
}
