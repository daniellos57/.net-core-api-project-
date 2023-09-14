using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Models;

namespace ProjektDaniel.Data
{
    public class RolaDbContext : DbContext
    {
        public RolaDbContext(DbContextOptions<RolaDbContext> options) : base(options)
        {
        }

        public DbSet<Rola> Rola { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}