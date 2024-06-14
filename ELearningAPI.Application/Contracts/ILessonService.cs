using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface ILessonService
    {
        Task<int> CreateLesson(CreateLessonDto lessonDto);

        Task<LessonViewModel> GetLessonById(int id);

        List<LessonViewModel> GetLessonList();

        Task UpdateLesson(int id, UpdateLessonDto updateDto);

        Task DeleteLesson(int id);

    }
}
