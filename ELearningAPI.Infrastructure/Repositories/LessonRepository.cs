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

        public async Task<Lesson> GetLastLessonOnCourse(Guid courseId)
        {
            var lastLesson = await context.Lessons
    .Where(l => l.CourseId == courseId)
    .OrderByDescending(l => l.CreatedAt)
    .FirstOrDefaultAsync();

            return lastLesson;
        }

        public async  Task<Lesson> GetLessonById(Guid id)
        {

            var lesson = await context.Lessons
                .Include(l => l.CorrelatedLessons)
                .SingleOrDefaultAsync(l => l.Id == id);

            if (lesson == null) return null;

            return lesson;

        }
    }
}
