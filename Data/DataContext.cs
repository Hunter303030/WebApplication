using Microsoft.EntityFrameworkCore;
using WebApplication.Models;

namespace WebApplication.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Profile> Profile { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CommentCourse> CommentCourse { get; set; }
        public DbSet<Lesson> Lesson { get; set; }
        public DbSet<CommentLesson> CommentLesson { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Profiles)
                .HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.Profile)
                .WithMany(y => y.Courses)
                .HasForeignKey(x => x.ProfileId);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.StatusModeration)
                .WithMany(y => y.Courses)
                .HasForeignKey(x => x.StatusId);

            modelBuilder.Entity<Lesson>()
                .HasOne(x => x.Course)
                .WithMany(y => y.Lessons)
                .HasForeignKey(x => x.CourseId);
        }

    }
}
