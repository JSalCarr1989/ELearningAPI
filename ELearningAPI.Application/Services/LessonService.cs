using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository lessonRepository;

        public LessonService(ILessonRepository lessonRepository)
        {
            this.lessonRepository = lessonRepository;
        }
        public async Task<Guid> CreateLesson(CreateLessonDto lessonDto)
        {

            var newLesson = new Lesson
            {
                Id = Guid.NewGuid(),
                Title = lessonDto.Title,
                Description = lessonDto.Description,
                Source = lessonDto.Source,
                ApprovalThreshold = lessonDto.ApprovalThreshold,
                CourseId = lessonDto.CourseId,
                CreatedAt = DateTime.UtcNow
            };

            var lastLessonOnCourse = await lessonRepository.GetLastLessonOnCourse(lessonDto.CourseId);

            if (lastLessonOnCourse != null)
            {
                newLesson.CorrelatedLessons = new List<CorrelatedLesson>
                {
                    new CorrelatedLesson
                    {
                        LessonId = newLesson.Id,
                        CorrelatedLessonId = lastLessonOnCourse.Id
                    }
                };
            }

            var newLessonId = lessonRepository.Add(newLesson);


            return newLessonId;

        }

        public async Task DeleteLesson(Guid id)
        {
            var course = await lessonRepository.GetById(id);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            lessonRepository.Delete(course.Id);
        }

        public async Task<LessonViewModel> GetLessonById(Guid id)
        {

            var lessonFromDb = await lessonRepository.GetLessonById(id);

            var viewLesson = MapLessonToViewModel(lessonFromDb);

            return viewLesson;
        }

        public List<LessonViewModel> GetLessonList()
        {
            List<LessonViewModel> lessonViewModels = new List<LessonViewModel>();

            var lessonsFromDb = lessonRepository.GetAll();

            foreach (var lesson in lessonsFromDb)
            {
                var viewLesson = MapLessonToViewModel(lesson);

                lessonViewModels.Add(viewLesson);
            }

            return lessonViewModels;
        }

        public async Task UpdateLesson(Guid id, UpdateLessonDto updateDto)
        {
            var lesson = await lessonRepository.GetById(id);

            if (lesson == null)
            {
                throw new KeyNotFoundException("Lesson not found");
            }

            lesson.Title = updateDto.Title;
            lesson.Description = updateDto.Description;
            lesson.Source = updateDto.Source;
            lesson.ApprovalThreshold = updateDto.ApprovalThreshold;
            lesson.UpdatedAt = DateTime.UtcNow;

            lessonRepository.Update(lesson);
        }

        private LessonViewModel MapLessonToViewModel(Lesson lesson)
        {
            return new LessonViewModel
            {
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Source = lesson.Source,
                ApprovalThreshold = lesson.ApprovalThreshold,
                CourseId = lesson.CourseId,
                CorrelatedLessonIds = lesson.CorrelatedLessons?.Select(cl => cl.CorrelatedLessonId).ToList()
            };
        }
    }
}
