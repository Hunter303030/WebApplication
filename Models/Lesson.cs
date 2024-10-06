namespace WebApplication.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public Guid CourseId { get; set; }

        public Course Course { get; set; }
    }
}
