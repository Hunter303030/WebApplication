using WebApplication.Dto.Course;
using WebApplication.Models;

namespace WebApplication.Service.Interfase
{
    public interface ICourseService
    {
        public Task<IEnumerable<Course>> ListAll();        
        public Task<IEnumerable<Course>> ListControl(Guid profileId);        
        public Task<bool> Add(CourseAddDto courseAddDto, Guid profileId);            
    }
}
