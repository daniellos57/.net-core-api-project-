using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Models;

namespace ProjektDaniel.Data
{
    public class DaneWniosekDbContext : DbContext
    {
        public DbSet<Danewniosek> DaneWniosek { get; set; }

        public DaneWniosekDbContext(DbContextOptions<DaneWniosekDbContext> options) : base(options)
        {
        }
    }
}