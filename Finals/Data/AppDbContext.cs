using Finals.Models;
using Microsoft.EntityFrameworkCore;


namespace Finals.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        public DbSet<User> Users => Set<User>();

        public DbSet<Feedback> Feedbacks { get; set; } = null!;

        public DbSet<Order> Orders { get; set; } = null!;
    }
}
