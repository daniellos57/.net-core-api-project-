namespace ProjektDaniel.DTOs
{
    public class WniosekDTO
    {
        public int Id { get; set; } = default!;
        public string Nazwa { get; set; } = default!;
        public int? IdOsobyZgłaszającej { get; set; } = default!;
        public int? IdOsobyZaakceptował { get; set; } = default!;

        public string? ImieNazwisko { get; set; }
        public string? NrEwidencyjny { get; set; }
    }
}