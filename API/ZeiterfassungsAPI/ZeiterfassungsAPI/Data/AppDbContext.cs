using Microsoft.EntityFrameworkCore;
using ZeiterfassungsAPI.Models;


namespace ZeiterfassungsAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<User> Users { get; set; }


    }
}
