using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AircraftModel> AircraftModel { get; set; }
        public DbSet<AircraftSell> AircraftSell { get; set; }
        public DbSet<AircraftImage> AircraftImage { get; set; }
        public DbSet<AircraftDocument> AircraftDocument { get; set; }
        public DbSet<Observation> Observation { get; set; }
    }
}
