using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly DataContext _context;

        public CourseRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> List()
        {
            IEnumerable<Course> coursesList = _context.Courses.ToList();
            return coursesList;
        }
    }
}
