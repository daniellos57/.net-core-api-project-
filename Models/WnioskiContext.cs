using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjektDaniel.Models;

public partial class WnioskiContext : DbContext
{
    public WnioskiContext()
    {
    }

    public WnioskiContext(DbContextOptions<WnioskiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rola> Rola { get; set; }

    public virtual DbSet<Użytkownik> Użytkownik { get; set; }

    public virtual DbSet<Wniosek> Wniosek { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=projektlokalnie");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rola>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__rola__3214EC07BE0903E5");
        });

        modelBuilder.Entity<Użytkownik>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__użytkown__3214EC072392E7F2");

            entity.HasOne(d => d.IdPrzełożonegoNavigation).WithMany(p => p.InverseIdPrzełożonegoNavigation).HasConstraintName("FK__użytkowni__Id_pr__5165187F");

            entity.HasOne(d => d.IdRolaNavigation).WithMany(p => p.Użytkownik).HasConstraintName("FK__użytkowni__Id_ro__5070F446");
        });

        modelBuilder.Entity<Wniosek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__wniosek__3214EC07B877C965");

            entity.HasOne(d => d.IdOsobyZaakceptowałNavigation).WithMany(p => p.WniosekIdOsobyZaakceptowałNavigation).HasConstraintName("FK__wniosek__Id_osob__5535A963");

            entity.HasOne(d => d.IdOsobyZgłaszającejNavigation).WithMany(p => p.WniosekIdOsobyZgłaszającejNavigation).HasConstraintName("FK__wniosek__Id_osob__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
