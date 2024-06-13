using ELearningAPI.Application.Contracts;
using ELearningAPI.Application.Services;
using ELearningAPI.Infrastructure;
using ELearningAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ELearningAPI.Application
{
    public static class ServiceExtensions
    {
        public static void AddELearningApplication(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHttpContextAccessor();

            services.AddScoped<IUserContextService, UserContextService>();

            services.AddELearningModels(configuration);

            services.AddELearningInfraestructure(configuration);

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ICourseService, CourseService>();

            services.AddScoped<ILessonService, LessonService>();

            services.AddScoped<IQuestionService, QuestionService>();

            services.AddScoped<IAnswerService, AnswerService>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "elearning.com",
                        ValidAudience = "elearning.com",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"))
                    };

                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireProfessorRole", policy => policy.RequireRole("Professor"));
                options.AddPolicy("RequiredStudentRole", policy => policy.RequireRole("Student"));
            });
        }
    }
}
