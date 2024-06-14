using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;
using System.Text.Json;

namespace ELearningAPI.Application.Services
{
    public class LessonService : ILessonService
    {
        private readonly ILessonRepository lessonRepository;
        private readonly IAnswerRepository answerRepository;
        private readonly IUserContextService userContextService;

        public LessonService(ILessonRepository lessonRepository, IAnswerRepository answerRepository, IUserContextService userContextService)
        {
            this.lessonRepository = lessonRepository;
            this.answerRepository = answerRepository;
            this.userContextService = userContextService;
        }
        public async Task<int> CreateLesson(CreateLessonDto lessonDto)
        {

            var newLesson = new Lesson
            {
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

        public async Task DeleteLesson(int id)
        {
            var course = await lessonRepository.GetById(id);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            lessonRepository.Delete(course.Id);
        }

        public async Task<List<LessonViewModel>> GetAvailableLessonsForACourse(int courseId)
        {
            List<LessonViewModel> availableLessons = new List<LessonViewModel>();

            var userId = userContextService.GetUserId();

            // traer todas las lecciones de un curso que no tengan una correlacion
            var lessons = await lessonRepository.GetLessonsByCourseId(courseId);

            var studentAnswers = await answerRepository.GetAnswersByStudentId(userId);

            foreach (var lesson in lessons) {

                if (!lesson.CorrelatedLessons.Any())
                {
                    var vwLesson = MapLessonToViewModel(lesson);
                    availableLessons.Add(vwLesson);

                }
                else
                {



                    //foreach(var relatedLesson in lesson.CorrelatedLessons)
                    //{

                        var approvalCriteria = lesson.ApprovalThreshold;
                        int scoreSummary = 0;
                        int incorrectQuestions = 0;

                        foreach (var question in lesson.CorrelatedLessons.FirstOrDefault().RelatedLesson.Questions)
                        {
                            // correct id options for the question
                            var qCorrectAnswerIds = question.Options.Where(o => o.IsCorrect).Select(oid => oid.Id).ToList();

                            // we get the question that the student had answered
                            var saQuestion = studentAnswers.Where(sa => sa.QuestionId == question.Id).FirstOrDefault();

                            if (saQuestion == null)
                            {
                                continue;
                            }

                            var sRightAnswers = JsonSerializer.Deserialize<List<int>>(saQuestion.SelectedOptions);

                            // if user answers are equal to question correct answers we sum the score to the score summary the question is right
                            if (qCorrectAnswerIds.Count == sRightAnswers?.Count)
                            {
                                scoreSummary = scoreSummary + question.Score;
                            }
                            else
                            {
                                incorrectQuestions = incorrectQuestions++;
                            }
                        }

                        if (scoreSummary >= approvalCriteria)
                        {
                            var vwLesson = MapLessonToViewModel(lesson);
                            availableLessons.Add(vwLesson);
                        }

                    //}

                  

                }


            }

            return availableLessons;

        }

        public async Task<LessonViewModel> GetLessonById(int id)
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

        public async Task UpdateLesson(int id, UpdateLessonDto updateDto)
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
