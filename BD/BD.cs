using Microsoft.EntityFrameworkCore;
using WebDead.Model;

namespace WebDead.BD
{
    public class BD : DbContext
    {
        public DbSet<User> Users { get; set; }

        public BD() 
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseMySql("server=192.168.200.35;userid=user04;password=93499;database=user04", ServerVersion.Parse("10.3.39mariadb"));
    }
}
