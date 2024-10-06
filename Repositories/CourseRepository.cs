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

            var list = await _context.Course.Include(x=>x.StatusModeration).Where(x=>x.ProfileId == profileId).ToListAsync();

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

        public async Task<Course> GetCourse(Guid courseId)
        {
            if (courseId == Guid.Empty) return null;

            var course = await _context.Course.Where(x => x.Id == courseId).FirstOrDefaultAsync();

            if (course == null) return null;

            return course;
        }

        public async Task<bool> Edit(Course course)
        {
            if(course == null) return false;

            _context.Course.Update(course);
            await _context.SaveChangesAsync();
            return true;
        }        

        public async Task<bool> Delete(Course course)
        {
            if (course == null) return false;

            _context.Course.Remove(course);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
