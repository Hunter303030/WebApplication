namespace WebApplication.Models
{
    public class CommentLession
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Ration { get; set; }
        public Guid? Profile_Id { get; set; }
        public Guid Lession_Id { get; set; }

        public Profile Profile { get; set; }
        public Lession Lession { get; set; }
    }
}
