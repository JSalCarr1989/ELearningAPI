using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Repositories
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        private readonly ELearningContext context;
        public CourseRepository(ELearningContext context) : base(context) {
          this.context = context;
        }

        public async Task<Course> GetCourseById(int id)
        {
            Course course = null;

            try
            {

                 course = await context.Courses
                                       .Include(c => c.CorrelatedCourses)
                                         .ThenInclude(cc => cc.RelatedCourse)
                                       .Include(l => l.Lessons)
                                         .ThenInclude(cl => cl.CorrelatedLessons)
                                        .Include(lt => lt.Lessons)
                                          .ThenInclude(lq => lq.Questions)
                                          .ThenInclude(o => o.Options)
                                       .SingleOrDefaultAsync(c => c.Id == id);

                if (course == null) return null;

            }
            catch (Exception ex) {

                Console.WriteLine(ex.Message);
            
            } 

            return course;
        }

        public async Task<Course> GetLastCourse(int userId)
        {

            var lastcourse = await context.Courses
   .Where(l => l.CreatedById == userId)
   .OrderByDescending(l => l.CreatedAt)
   .FirstOrDefaultAsync();

            return lastcourse;
        }
    }
}
