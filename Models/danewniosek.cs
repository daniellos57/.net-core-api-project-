using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NPOI.SS.Formula.Functions;

namespace ProjektDaniel.Models;

[Table("danewniosek")]
public partial class Danewniosek
{
    [Key]
    public int Id { get; set; }

    [Column("ImieNazwisko")]
    public string ImieNazwisko { get; set; }

    [Column("IloscDni")]
    public int IloscDni { get; set; }

    [Column("DataUrlopu")]
    public DateTime DataUrlopu { get; set; }

    [Column("NrEwidencyjny")]
    public string NrEwidencyjny { get; set; }

    [Column("DataWypelnienia")]
    public DateTime DataWypelnienia { get; set; }

    [ForeignKey("WniosekId")]
    public int WniosekId { get; set; }

    public virtual Wniosek Wniosek { get; set; }
}   