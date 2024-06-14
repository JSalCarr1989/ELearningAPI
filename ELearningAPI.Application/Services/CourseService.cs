using AutoMapper;
using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace ELearningAPI.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        private readonly IUserContextService userContextService;
        private readonly IAnswerRepository answerRepository;
        public CourseService(ICourseRepository courseRepository,IMapper mapper, IUserContextService userContextService, IAnswerRepository answerRepository)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
            this.userContextService = userContextService;
            this.answerRepository = answerRepository;
        }

        public async Task<int> CreateCourse(CreateCourseDto courseDto)
        {
            // aqui entra el automapper}

            var userId = userContextService.GetUserId();

            var newCourse = mapper.Map<Course>(courseDto);

            newCourse.CreatedById = userId;

            newCourse.CreatedAt = DateTime.Now;

            var lastCourse = await courseRepository.GetLastCourse(userId);

            if (lastCourse != null)
            {
                newCourse.CorrelatedCourses = new List<CorrelatedCourse>
                {
                    new CorrelatedCourse
                    {
                        CourseId = newCourse.Id,
                        CorrelatedCourseId = lastCourse.Id
                    }
                };
            }

            var courseId = courseRepository.Add(newCourse);

            return courseId;
        }

        public async Task DeleteCourse(int id)
        {
            var course = await courseRepository.GetById(id);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            courseRepository.Delete(course.Id);



        }

        public async Task<List<AvailableCourseViewModel>> GetAvailableCourses(int studentId)
        {
            List<AvailableCourseViewModel> availableCourses = new List<AvailableCourseViewModel>();

            //List<CourseViewModel> approvedCourse = new List<CourseViewModel>();


            //1. obtener todas las respuestas que ha dado un usuario 

            var studentAnswers = await answerRepository.GetAnswersByStudentId(studentId);

            var studentAnswersIds = studentAnswers.Select(answer => answer.Id).ToList();

            var courses = await courseRepository.GetCoursesByQuestionsIdsAsync(studentAnswersIds);

            // if the student not have any progress for now.

            if (courses.IsNullOrEmpty())
            {
                // get all courses without correlations

                var coursesWithoutCorrelations = await courseRepository.GetCoursesWithoutCorrelations();

                foreach(var course in coursesWithoutCorrelations)
                {
                    var courseVm = MapCourseToAvailableCourseViewModel(course);

                    courseVm.IsApproved = false;

                    availableCourses.Add(courseVm);

                }

                return availableCourses;

            }

            foreach(var course in courses)
            {

                var approvedLessons = 0;
                var totalLessons = course.Lessons.Count;

                foreach(var lesson in course.Lessons)
                {

                    var approvalCriteria = lesson.ApprovalThreshold;
                    int scoreSummary = 0;
                    int incorrectQuestions = 0;

                    foreach(var question in lesson.Questions)
                    {
                        // correct id options for the question
                        var qCorrectAnswerIds = question.Options.Where(o => o.IsCorrect).Select(oid => oid.Id).ToList();

                        // we get the question that the student had answered
                        var saQuestion = studentAnswers.Where(sa => sa.QuestionId == question.Id).FirstOrDefault();

                        var sRightAnswers = JsonSerializer.Deserialize<List<int>>(saQuestion.SelectedOptions);

                        // if user answers are equal to question correct answers we sum the score to the score summary the question is right
                        if(qCorrectAnswerIds.Count == sRightAnswers?.Count)
                        {
                            scoreSummary = scoreSummary + question.Score;
                        } else
                        {
                            incorrectQuestions = incorrectQuestions++;
                        }
                    }

                    if (scoreSummary >= approvalCriteria)
                    {
                        // we increment approvedLessons
                        approvedLessons = approvedLessons + 1;
                    }

                }

                if(approvedLessons == totalLessons)
                {
                    // completed course
                    var vmCourse = MapCourseToAvailableCourseViewModel(course);
                    vmCourse.IsApproved = true;
                    availableCourses.Add(vmCourse);

                    // get the course that is correlated with this course (next course or courses to do)
                    var nextCourses = await courseRepository.GetCoursesByCorrelatedCourse(course.Id);

                    foreach(var nextCourse in nextCourses)
                    {
                        var vwNextCourse = MapCourseToAvailableCourseViewModel(nextCourse);
                        vwNextCourse.IsApproved = false;
                        availableCourses.Add(vwNextCourse);
                    }


                } else
                {
                    var vmCourse = MapCourseToAvailableCourseViewModel(course);
                    vmCourse.IsApproved = false;
                    availableCourses.Add(vmCourse);
                }

            }

            return availableCourses;
        }

        public async Task<CourseViewModel> GetCourseById(int id)
        {
            var courseFromDb = await courseRepository.GetCourseById(id);

            var courseToView = MapCourseToViewModel(courseFromDb);

            return courseToView;

        }

        public  List<CourseViewModel> GetCourseList()
        {

            List<CourseViewModel> courseViewModels = new List<CourseViewModel>();

            var coursesFromDb = courseRepository.GetAll();

            foreach(var course in coursesFromDb)
            {
                var viewCourse = MapCourseToViewModel(course);

                courseViewModels.Add(viewCourse);
            }

            return courseViewModels;
        }

        public async Task UpdateCourse(int id, UpdateCourseDto updateDto)
        {
            var course = await courseRepository.GetById(id);

            if(course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            course.Title = updateDto.Title;
            course.Description = updateDto.Description;
            course.UpdatedAt = DateTime.Now;

            courseRepository.Update(course);

        }


        private AvailableCourseViewModel MapCourseToAvailableCourseViewModel(Course course)
        {
            return new AvailableCourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,

                CorrelatedCourseIds = course.CorrelatedCourses?.Select(cc => cc.CorrelatedCourseId).ToList(),
                CorrelatedCourses = course.CorrelatedCourses?.Select(cc => new CorrelatedCourseViewModel
                {
                    Id = cc.CorrelatedCourseId,
                    Title = cc.RelatedCourse.Title
                }).ToList(),
                Lessons = course.Lessons?.Select(l => new LessonViewModel()
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Source = l.Source,
                    ApprovalThreshold = l.ApprovalThreshold,
                    CourseId = l.CourseId,
                    CorrelatedLessonIds = l.CorrelatedLessons.Select(cl => cl.LessonId).ToList(),
                    Questions = l.Questions.Select(q => new QuestionViewModel()
                    {
                        Id = q.Id,
                        Description = q.Description,
                        Score = q.Score,
                        Type = q.Type,
                        LessonId = q.LessonId,
                        Options = q.Options.Select(o => new QuestionOptionViewModel()
                        {
                            Id = o.Id,
                            Option = o.Option,
                            IsCorrect = o.IsCorrect
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }

        private CourseViewModel MapCourseToViewModel(Course course)
        {
            return new CourseViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,

                CorrelatedCourseIds = course.CorrelatedCourses?.Select(cc => cc.CorrelatedCourseId).ToList(),
                CorrelatedCourses = course.CorrelatedCourses?.Select(cc => new CorrelatedCourseViewModel
                {
                    Id = cc.CorrelatedCourseId,
                    Title = cc.RelatedCourse.Title
                }).ToList(),
                Lessons = course.Lessons?.Select(l => new LessonViewModel()
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    Source = l.Source,
                    ApprovalThreshold = l.ApprovalThreshold,
                    CourseId = l.CourseId,
                    CorrelatedLessonIds = l.CorrelatedLessons.Select(cl => cl.LessonId).ToList(),
                    Questions = l.Questions.Select(q => new QuestionViewModel()
                    {
                        Id = q.Id,
                        Description = q.Description,
                        Score = q.Score,
                        Type = q.Type,
                        LessonId = q.LessonId,
                        Options = q.Options.Select(o => new QuestionOptionViewModel()
                        {
                            Id = o.Id,
                            Option = o.Option,
                            IsCorrect = o.IsCorrect
                        }).ToList()
                    }).ToList()
                }).ToList()
            };
        }

    }
}
