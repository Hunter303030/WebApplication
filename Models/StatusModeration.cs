namespace WebApplication.Models
{
    public class StatusModeration
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public IEnumerable<Course> Courses { get; set; }
    }
}
