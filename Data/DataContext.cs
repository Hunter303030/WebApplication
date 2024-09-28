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
        public DbSet<Lession> Lession { get; set; }
        public DbSet<CommentLession> CommentLession { get; set; }
        public DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Profile>()
                .HasOne(p => p.Role)
                .WithMany(r => r.Profiles)
                .HasForeignKey(p => p.Role_Id);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.Profile)
                .WithMany(y => y.Courses)
                .HasForeignKey(x => x.Profile_Id);

            modelBuilder.Entity<Course>()
                .HasOne(x => x.StatusModeration)
                .WithMany(y => y.Courses)
                .HasForeignKey(x => x.Status_Id);
        }

    }
}
