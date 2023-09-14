using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjektDaniel.Models;

[Table("użytkownik")]
[Microsoft.EntityFrameworkCore.Index("Email", Name = "UQ__użytkown__A9D10534C7C7CBD8", IsUnique = true)]
public partial class Użytkownik
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Imię { get; set; } = null!;

    [StringLength(50)]
    public string Nazwisko { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [Column("Id_przełożonego")]
    public int? IdPrzełożonego { get; set; }

    [Column("Id_rola")]
    public int? IdRola { get; set; }

    [ForeignKey("IdPrzełożonego")]
    [InverseProperty("InverseIdPrzełożonegoNavigation")]
    public virtual Użytkownik? IdPrzełożonegoNavigation { get; set; }

    [ForeignKey("IdRola")]
    [InverseProperty("Użytkownik")]
    public virtual Rola? IdRolaNavigation { get; set; }

    [InverseProperty("IdPrzełożonegoNavigation")]
    public virtual ICollection<Użytkownik> InverseIdPrzełożonegoNavigation { get; } = new List<Użytkownik>();

    [InverseProperty("IdOsobyZaakceptowałNavigation")]
    public virtual ICollection<Wniosek> WniosekIdOsobyZaakceptowałNavigation { get; } = new List<Wniosek>();

    [InverseProperty("IdOsobyZgłaszającejNavigation")]
    public virtual ICollection<Wniosek> WniosekIdOsobyZgłaszającejNavigation { get; } = new List<Wniosek>();
}
