namespace WebApplication.Dto.Lesson
{
    public class LessonAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Content { get; set; }

        public Guid CourseId { get; set; }
    }
}
