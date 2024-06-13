using ELearningAPI.Models.Domain.Entities;

namespace ELearningAPI.Infrastructure.Contracts
{
    public interface ICourseRepository : IGenericRepository<Course>
    {
        Task<Course> GetCourseById(Guid id);
        Task<Course> GetLastCourse(Guid userId);
    }
}
