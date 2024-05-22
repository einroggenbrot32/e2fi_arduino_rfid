using Microsoft.EntityFrameworkCore;
using ZeiterfassungsAPI.Models;


namespace ZeiterfassungsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
             
        }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RfidChip> RfidChip { get; set; }


    }
}
