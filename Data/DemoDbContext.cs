using DemoHangFire.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoHangFire.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }
        

        public DbSet<UserModel> Users { get; set; }

    }
}
