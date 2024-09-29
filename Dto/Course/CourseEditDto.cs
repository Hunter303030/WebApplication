namespace WebApplication.Dto.Course
{
    public class CourseEditDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string PreviewUrl { get; set; }

        public IFormFile Preview { get; set; }
    }
}
