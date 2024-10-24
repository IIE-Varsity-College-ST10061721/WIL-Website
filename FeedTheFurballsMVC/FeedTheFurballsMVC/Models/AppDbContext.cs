using Microsoft.EntityFrameworkCore;

namespace FeedTheFurballsMVC.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<DonationGoal> DonationGoals { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    }
}
