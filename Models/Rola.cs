using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjektDaniel.Models;

[Table("rola")]
public partial class Rola
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Nazwa { get; set; } = null!;

    [InverseProperty("IdRolaNavigation")]
    public virtual ICollection<Użytkownik> Użytkownik { get; } = new List<Użytkownik>();
}
