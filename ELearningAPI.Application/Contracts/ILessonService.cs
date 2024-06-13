using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Contracts
{
    public interface ILessonService
    {
        Task<Guid> CreateLesson(CreateLessonDto lessonDto);

        Task<LessonViewModel> GetLessonById(Guid id);

        List<LessonViewModel> GetLessonList();

        Task UpdateLesson(Guid id, UpdateLessonDto updateDto);

        Task DeleteLesson(Guid id);

    }
}
