using ELearningAPI.Models.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ELearningAPI.Infrastructure.Data
{
    public class ELearningContext : DbContext
    {
        public ELearningContext(DbContextOptions<ELearningContext> options)
             : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CorrelatedCourse> CorrelatedCourses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<CorrelatedLesson> CorrelatedLessons { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionOption> Options { get; set; }
        public DbSet<StudentAnswer> StudentAnswers { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CorrelatedCourse>()
                .HasKey(cc => new { cc.CourseId, cc.CorrelatedCourseId });

            modelBuilder.Entity<CorrelatedCourse>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.CorrelatedCourses)
                .HasForeignKey(cc => cc.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CorrelatedCourse>()
                .HasOne(cc => cc.RelatedCourse)
                .WithMany()
                .HasForeignKey(cc => cc.CorrelatedCourseId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CorrelatedLesson>()
            .HasKey(cl => new { cl.LessonId, cl.CorrelatedLessonId });

            modelBuilder.Entity<CorrelatedLesson>()
                .HasOne(cl => cl.Lesson)
                .WithMany(l => l.CorrelatedLessons)
                .HasForeignKey(cl => cl.LessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CorrelatedLesson>()
                .HasOne(cl => cl.RelatedLesson)
                .WithMany()
                .HasForeignKey(cl => cl.CorrelatedLessonId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Course>()
               .HasMany(c => c.Lessons)
               .WithOne(l => l.Course)
               .HasForeignKey(l => l.CourseId);

            modelBuilder.Entity<Question>()
               .HasMany(q => q.Options)
               .WithOne(o => o.Question)
               .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<Lesson>()
                .HasMany(l => l.Questions)
                .WithOne(q => q.Lesson)
                .HasForeignKey(q => q.LessonId);


            base.OnModelCreating(modelBuilder);
        }

    }
}
