namespace WebApplication.Dto.Course
{
    public class CourseAddDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public IFormFile Preview { get; set; }
    }
}
