using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjektDaniel.Models;

[Table("wniosek")]
public partial class Wniosek
{
    [Key]
    public int Id { get; set; }

    public byte[]? Plik { get; set; }

    public string? Nazwa { get; set; }

    [Column("Id_osoby_zgłaszającej")]
    public int? IdOsobyZgłaszającej { get; set; }

    [Column("Id_osoby_zaakceptował")]
    public int? IdOsobyZaakceptował { get; set; }
    public string? Rozszerzenie { get; set; }
    public string? ImieNazwisko { get; set; }
    public string? NrEwidencyjny { get; set; }
    public int? iloscdni { get; set; }
    public string? poczatek { get; set; }
    public string? koniec {  get; set; }


    [ForeignKey("IdOsobyZaakceptował")]
    [InverseProperty("WniosekIdOsobyZaakceptowałNavigation")]
    public virtual Użytkownik? IdOsobyZaakceptowałNavigation { get; set; }

    [ForeignKey("IdOsobyZgłaszającej")]
    [InverseProperty("WniosekIdOsobyZgłaszającejNavigation")]
    public virtual Użytkownik? IdOsobyZgłaszającejNavigation { get; set; }
}
