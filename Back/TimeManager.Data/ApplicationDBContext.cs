using Microsoft.EntityFrameworkCore;
using TimeManager.Data.Entities;

namespace TimeManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //User
            builder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            builder.Entity<User>().Property(p => p.FirstName).IsRequired();
            builder.Entity<User>().Property(p => p.LastName).IsRequired();

            builder.Entity<User>().Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.Entity<User>().HasMany(x => x.Reports).WithOne(x => x.User);

            //Report
            builder.Entity<Report>().Property(p => p.Description).IsRequired();
            builder.Entity<Report>().Property(p => p.Hours).IsRequired();
            builder.Entity<Report>().Property(p => p.StartDate).IsRequired();
            builder.Entity<Report>().Property(p => p.UserId).IsRequired();

            base.OnModelCreating(builder);
        }
    }
}