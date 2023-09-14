using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Models;

namespace ProjektDaniel.Data
{
    public class WniosekDbContext : DbContext
    {
        public WniosekDbContext(DbContextOptions<WniosekDbContext> options) : base(options)
        {
        }

        public DbSet<Wniosek> Wniosek { get; set; }
    


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}