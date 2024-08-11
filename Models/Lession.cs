namespace WebApplication.Models
{
    public class Lession
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ContentUrl { get; set; }
        public Guid Course_Id { get; set; }

        public Course Course { get; set; }
    }
}
