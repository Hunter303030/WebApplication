namespace WebApplication.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public int Price { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime DateUpdata { get; set; }
        public string PreviewUrl { get; set; }
        public Guid Profile_Id { get; set; }

        public Profile Profile { get; set; }
    }
}
