using WebApplication.Models;

namespace WebApplication.Repositories.Interfaces
{
    public interface ILessonRepository
    {
        public Task<IEnumerable<Lesson>> List(Guid courseId);
        public Task<bool> Add(Lesson lesson);
    }
}
