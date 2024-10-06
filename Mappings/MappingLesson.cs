using WebApplication.Dto.Lesson;
using WebApplication.Models;

namespace WebApplication.Mappings
{
    public class MappingLesson: AutoMapper.Profile
    {
        public MappingLesson()
        {
            CreateMap<Lesson, LessonListDto>();
            CreateMap<Lesson, LessonAddDto>();

            CreateMap<LessonListDto, Lesson>();
            CreateMap<LessonAddDto, Lesson>();
        }
    }
}
