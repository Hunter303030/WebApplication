namespace WebApplication.Models
{
    public class CommentLesson
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Ration { get; set; }
        public Guid? Profile_Id { get; set; }
        public Guid Lesson_Id { get; set; }

        public Profile Profile { get; set; }
        public Lesson Lesson { get; set; }
    }
}
