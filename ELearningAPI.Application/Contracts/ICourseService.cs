using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public  interface ICourseService
    {
        Task<Guid> CreateCourse(CreateCourseDto course);
        Task<CourseViewModel> GetCourseById(Guid id);

        List<CourseViewModel> GetCourseList();

        Task UpdateCourse(Guid id, UpdateCourseDto updateDto);

        Task DeleteCourse(Guid id);


    }
}
