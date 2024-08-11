namespace WebApplication.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public Profile Profile { get; set; }
    }
}
