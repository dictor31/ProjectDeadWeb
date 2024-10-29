using Microsoft.EntityFrameworkCore;
using WebDead.Model;

namespace WebDead.BD
{
    public class BD : DbContext
    {
        DbSet<User> Users { get; set; }

        public BD() 
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
            => optionsBuilder.UseMySql("server=192.168.200.35;userid=user07;password=24367;database=Dictor", ServerVersion.Parse("10.3.39mariadb"));
        
    }
}
