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
            IEnumerable<Course> coursesList = _context.Course.ToList();
            return coursesList;
        }

        public async Task<bool> Add(Course course)
        {
            if(course != null)
            {
                await _context.Course.AddAsync(course);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
