using WebApplication.Models;
using WebApplication.Dto.Course;
using AutoMapper;

namespace WebApplication.Mappings
{
    public class MappingCourse: AutoMapper.Profile
    {
        public MappingCourse()
        {
            CreateMap<Course, CourseAddDto>();
            CreateMap<Course, CourseEditDto>();

            CreateMap<CourseAddDto, Course>();
            CreateMap<CourseEditDto, Course>();
        }
    }
}
