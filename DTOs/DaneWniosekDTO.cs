namespace ProjektDaniel.DTOs
{
    public class DaneWniosekDTO
    {
        public int Id { get; set; } = default!;
        public string? ImieNazwisko { get; set; }
        public int? IloscDni { get; set; }
        public DateTime? DataUrlopu { get; set; }
        public string? NrEwidencyjny { get; set; }
        public DateTime? DataWypelnienia { get; set; }
        public int? WniosekId { get; set; }
    }
}