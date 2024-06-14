using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class LessonRepository : GenericRepository<Lesson>, ILessonRepository
    {

        private readonly ELearningContext context;
        public LessonRepository(ELearningContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<Lesson> GetLastLessonOnCourse(int courseId)
        {
            var lastLesson = await context.Lessons
    .Where(l => l.CourseId == courseId)
    .OrderByDescending(l => l.CreatedAt)
    .FirstOrDefaultAsync();

            return lastLesson;
        }

        public async  Task<Lesson> GetLessonById(int id)
        {

            var lesson = await context.Lessons
                .Include(l => l.CorrelatedLessons)
                .SingleOrDefaultAsync(l => l.Id == id);

            if (lesson == null) return null;

            return lesson;

        }

        public async Task<List<Lesson>> GetLessonsByCourseId(int courseId)
        {

            var lessons = await context.Lessons
                                 .Include(cl => cl.CorrelatedLessons)
                                 .Include(q => q.Questions)
                                    .ThenInclude(o => o.Options)
                                    .Where(l => l.CourseId == courseId)
                                    .ToListAsync();

            return lessons;

        }
    }
}
