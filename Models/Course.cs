namespace WebApplication.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdata { get; set; }
        public string PreviewUrl { get; set; }
        public Guid Profile_Id { get; set; }
        public int Status_Id { get; set; }

        public Profile Profile { get; set; }
        public StatusModeration StatusModeration { get; set; }
    }
}
