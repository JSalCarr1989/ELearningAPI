using AutoMapper;
using ELearningAPI.Application.Contracts;
using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        private readonly IUserContextService userContextService;
        public CourseService(ICourseRepository courseRepository,IMapper mapper, IUserContextService userContextService)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
            this.userContextService = userContextService;
        }

        public async Task<Guid> CreateCourse(CreateCourseDto courseDto)
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

        public async Task DeleteCourse(Guid id)
        {
            var course = await courseRepository.GetById(id);

            if (course == null)
            {
                throw new KeyNotFoundException("Course not found");
            }

            courseRepository.Delete(course.Id);



        }

        public async Task<CourseViewModel> GetCourseById(Guid id)
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

        public async Task UpdateCourse(Guid id, UpdateCourseDto updateDto)
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
