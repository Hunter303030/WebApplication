namespace WebApplication.Models
{
    public class CommentLesson
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Ration { get; set; }
        public Guid? ProfileId { get; set; }
        public Guid LessonId { get; set; }

        public Profile Profile { get; set; }
        public Lesson Lesson { get; set; }
    }
}
