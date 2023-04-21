using Forum.Models;
using Microsoft.EntityFrameworkCore;
namespace Forum.Data
{
    public class MySqlContext : DbContext
    {
        public MySqlContext(DbContextOptions options) : base(options)
        {

        }
       
        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Status> Statuss { get; set; }
        public DbSet<Respon> Respons { get; set; }


    }
}
