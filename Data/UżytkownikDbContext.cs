using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Models; 

namespace ProjektDaniel.Data
{
    public class UżytkownikDbContext : DbContext
    {
        public UżytkownikDbContext(DbContextOptions<UżytkownikDbContext> options) : base(options)
        {
        }

        public DbSet<Użytkownik> Użytkownicy { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
        }
    }
}