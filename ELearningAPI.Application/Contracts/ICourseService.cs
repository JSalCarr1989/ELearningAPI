using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public  interface ICourseService
    {
        Task<int> CreateCourse(CreateCourseDto course);
        Task<CourseViewModel> GetCourseById(int id);

        List<CourseViewModel> GetCourseList();

        Task UpdateCourse(int id, UpdateCourseDto updateDto);

        Task DeleteCourse(int id);

        Task<List<AvailableCourseViewModel>> GetAvailableCourses(int studentId);




    }
}
