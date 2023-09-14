    namespace ProjektDaniel.DTOs
    {
        public class DodajWniosekDTO
        { 
            public IFormFile Plik { get; set; } = default!;
            public string Nazwa { get; set; } = default!;
            public int? IdOsobyZgłaszającej { get; set; } = default!;
            public int? IdOsobyZaakceptował { get; set; } = default!;


    }
    }