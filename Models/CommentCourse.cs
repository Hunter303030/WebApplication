namespace WebApplication.Models
{
    public class CommentCourse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Ration { get; set; }
        public DateTime Created { get; set; }
        public Guid? Profile_Id { get; set; }
        public Guid Course_Id { get; set; }

        public Profile Profile { get; set; }
        public Course Course { get; set; }
    }
}
