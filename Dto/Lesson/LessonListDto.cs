
namespace WebApplication.Dto.Lesson
{
    public class LessonListDto
    {
        public Guid CourseId { get; set; }
        public IEnumerable<Models.Lesson> Lessons { get; set; }
    }
}
