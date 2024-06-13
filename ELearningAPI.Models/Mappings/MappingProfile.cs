using AutoMapper;
using ELearningAPI.Models.Domain.Entities;
using ELearningAPI.Models.DTOs;
using ELearningAPI.Models.ViewModels;

namespace ELearningAPI.Models.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<CreateCourseDto, Course>().ReverseMap();

            //CreateMap<Course, CourseViewModel>()
            //   .ForMember(dest => dest.CorrelatedCourseIds, opt => opt.MapFrom(src => src.CorrelatedCourses.Select(cc => cc.CorrelatedCourseId).ToList()))
            //   .ForMember(dest => dest.CorrelatedCourses, opt => opt.MapFrom(src => src.CorrelatedCourses.Select(cc => cc.RelatedCourse).ToList()));

            //// Mapeo de CorrelatedCourse a CorrelatedCourseViewModel
            //CreateMap<CorrelatedCourse, CorrelatedCourseViewModel>()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CorrelatedCourseId))
            //    .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.RelatedCourse.Title));

        }

    }
}
