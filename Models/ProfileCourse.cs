namespace WebApplication.Models
{
    public class ProfileCourse
    {
        public Guid Id { get; set; }
        public Guid ProfileId {  get; set; }
        public Guid CourseId { get; set; }

        public Profile Profile { get; set; }
        public IQueryable<Course> Courses { get;set; }
    }
}
