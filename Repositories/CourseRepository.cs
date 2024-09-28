using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Course>> ListAll()
        {
            var list = await _context.Course.Include(x=>x.StatusModeration).Where(x=>x.StatusModeration.Id == 1).ToListAsync();

            if (list == null) return null;

            return list;
        }

        public async Task<IEnumerable<Course>> ListControl(Guid profileId)
        {
            if (profileId == Guid.Empty) return null;

            var list = await _context.Course.Include(x=>x.StatusModeration).Where(x=>x.Profile_Id == profileId).ToListAsync();

            if (list == null) return null;

            return list;
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
