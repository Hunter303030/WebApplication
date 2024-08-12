using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        public IEnumerable<Course> List();
    }
}
