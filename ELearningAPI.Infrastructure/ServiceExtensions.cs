using ELearningAPI.Infrastructure.Contracts;
using ELearningAPI.Infrastructure.Data;
using ELearningAPI.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELearningAPI.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddELearningInfraestructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ELearningContext>(opt => opt.UseInMemoryDatabase("ELearning"));

            // Ensure database is created and seed data is applied
            using (var scope = services.BuildServiceProvider().CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var context = serviceProvider.GetRequiredService<ELearningContext>();
                DbInitializer.Initialize(context);
            }

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
        }
    }
}
