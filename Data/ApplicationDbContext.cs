using Microsoft.EntityFrameworkCore;
using ZurumPark.Entities;

namespace ZurumPark.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<NationalPark> NationalParks {get; set;}
    }
}