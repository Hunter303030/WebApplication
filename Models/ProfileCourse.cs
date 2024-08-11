namespace WebApplication.Models
{
    public class ProfileCourse
    {
        public Guid Id { get; set; }
        public Guid Profile_Id {  get; set; }
        public Guid Course_Id { get; set; }

        public Profile Profile { get; set; }
        public IQueryable<Course> Courses { get;set; }
    }
}
