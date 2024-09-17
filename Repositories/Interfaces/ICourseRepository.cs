using WebApplication.Dto.Course;
using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> List();
        public Task<bool> Add(Course course);
    }
}
