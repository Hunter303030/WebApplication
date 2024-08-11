using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options)
        {
            
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CommentCourse> CommentCourses { get; set; }
        public DbSet<Lession> Lessions { get; set; }
        public DbSet<CommentLession> CommentLessions { get; set; }

        //protected override void OnConfiguring(ModelBuilder modelBuilder)
        //{

        //}

    }
}
