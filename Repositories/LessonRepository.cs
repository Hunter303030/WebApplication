using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;
using WebApplication.Repositories.Interfaces;

namespace WebApplication.Repositories
{
    public class LessonRepository: ILessonRepository
    {
        private readonly DataContext _context;

        public LessonRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lesson>> List(Guid courseId)
        {
            if (courseId == Guid.Empty) return null;

            return await _context.Lesson.Where(x => x.CourseId == courseId).OrderBy(x=>x.Number).ToListAsync();
        }

        public async Task<bool> Add(Lesson lesson)
        {
            if (lesson == null) return false;

            await _context.Lesson.AddAsync(lesson);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
