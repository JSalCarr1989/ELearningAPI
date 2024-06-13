using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using ELearningAPI.Models.Mappings;

namespace ELearningAPI.Models
{
    public static class ServiceExtensions
    {
        public static void AddELearningModels(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}
