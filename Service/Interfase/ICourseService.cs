using WebApplication.Dto.Course;

namespace WebApplication.Service.Interfase
{
    public interface ICourseService
    {
        public Task<bool> Add(CourseAddDto courseAddDto, Guid profile_Id);
    }
}
