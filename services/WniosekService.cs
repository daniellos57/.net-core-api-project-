using ProjektDaniel.Data;
using ProjektDaniel.DTOs;
using ProjektDaniel.Models;
using System.Globalization;
using System.Text.RegularExpressions;


using Aspose.Words;

namespace ProjektDaniel.services
{
    public class WniosekService
    {
        private readonly WniosekDbContext _context;

        public WniosekService(WniosekDbContext dbContext)
        {
            _context = dbContext;
        }
        public List<WniosekDTO> PobierzWszystkichWniosek()
        {
            var wnioski = _context.Wniosek
                .Select(u => new WniosekDTO
                {
                    Id = u.Id,

                    Nazwa = u.Nazwa,
                    IdOsobyZgłaszającej = u.IdOsobyZgłaszającej,
                    IdOsobyZaakceptował = u.IdOsobyZaakceptował
                })
                .ToList();

            return wnioski;
        }
        public WniosekDTO PobierzWniosekById(int id)
        {
            var wniosek = _context.Wniosek
                .Where(u => u.Id == id)
                .Select(u => new WniosekDTO
                {
                    Id = u.Id,

                    Nazwa = u.Nazwa,
                    IdOsobyZgłaszającej = u.IdOsobyZgłaszającej,
                    IdOsobyZaakceptował = u.IdOsobyZaakceptował
                })
                .FirstOrDefault();

            return wniosek;
        }
        public async Task DodajWniosekZPlikiem(DodajWniosekDTO dodajWniosekDTO)
        {
            if (dodajWniosekDTO.Plik == null || dodajWniosekDTO.Plik.Length == 0)
            {
                throw new ArgumentException("Plik jest pusty.");
            }

            try
            {
                var rozszerzenie = Path.GetExtension(dodajWniosekDTO.Plik.FileName);

             
                using (var stream = dodajWniosekDTO.Plik.OpenReadStream())
                {
                    var doc = new Document(stream);

                    var text = doc.GetText();

                   

                    var ImieNazwisko = SzukajTekstuPomiedzy(text, "URLOPOWA", "Nr");
                    var NrEwidencyjny = SzukajTekstuPomiedzy(text, "ewid.", "(nazwisko");
                    var iloscdni = SzukajTekstuPomiedzy(text,"ilości","dni");

  
                    //data
                    var datarozpoczeciaText = SzukajTekstuPomiedzy(text, "Od dnia", "Do");
                    string datarozpoczecia = null;

                    if (!string.IsNullOrEmpty(datarozpoczeciaText))
                    {
                        
                        var datePattern = @"\d{2}\.\d{2}\.\d{4}";
                        var match = Regex.Match(datarozpoczeciaText, datePattern);

                        if (match.Success)
                        {
                            if (DateTime.TryParseExact(match.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                            {
                                
                                datarozpoczecia = parsedDate.ToString("yyyy-MM-dd");
                                Console.WriteLine("Dzięki działa.");
                            }
                            else
                            {
                                
                                Console.WriteLine("Nie można sparsować daty.");
                            }
                        }
                    }

                    var datakoncaTeskt = SzukajTekstuPomiedzy(text, "Do dnia", "Zgoda");
                    string datakonca = null;

                    if (!string.IsNullOrEmpty(datakoncaTeskt))
                    {
                        
                        var datePattern = @"\d{2}\.\d{2}\.\d{4}";
                        var match = Regex.Match(datakoncaTeskt, datePattern);

                        if (match.Success)
                        {
                            if (DateTime.TryParseExact(match.Value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
                            {
                             
                                datakonca = parsedDate.ToString("yyyy-MM-dd");
                                Console.WriteLine("Dzięki działa.");
                            }
                            else
                            {
                              
                                Console.WriteLine("Nie można sparsować daty.");
                            }
                        }
                    }

                    //data
                    Console.WriteLine($"datakoniec: {datakoncaTeskt}");
                   
                    int liczbadni = int.Parse(iloscdni);

                    if (!string.IsNullOrEmpty(ImieNazwisko) && !string.IsNullOrEmpty(NrEwidencyjny))
                    {
                        var wniosek = new Wniosek
                        {
                            Plik = await ConvertFormFileToByteArrayAsync(dodajWniosekDTO.Plik),
                            Nazwa = dodajWniosekDTO.Nazwa,
                            IdOsobyZgłaszającej = dodajWniosekDTO.IdOsobyZgłaszającej,
                            IdOsobyZaakceptował = dodajWniosekDTO.IdOsobyZaakceptował,
                            Rozszerzenie = rozszerzenie,
                            ImieNazwisko = ImieNazwisko,
                            NrEwidencyjny = NrEwidencyjny,
                            iloscdni = liczbadni,
                            poczatek = datarozpoczecia,
                            koniec = datakonca


                        };

                        _context.Wniosek.Add(wniosek);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Wystąpił błąd podczas dodawania pliku: {ex.Message}");
            }
        }

        private string SzukajTekstuPomiedzy(string text, string startKeyword, string endKeyword)
        {
            var startIndex = text.IndexOf(startKeyword);
            var endIndex = text.IndexOf(endKeyword, startIndex);

            if (startIndex != -1 && endIndex != -1)
            {
                var extractedData = text.Substring(startIndex + startKeyword.Length, endIndex - startIndex - startKeyword.Length).Trim();
                return extractedData.Trim();
            }

            return null;
        }


        private async Task<byte[]> ConvertFormFileToByteArrayAsync(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
        public bool UsunWniosek(int id)
        {
            var wniosek = _context.Wniosek.FirstOrDefault(u => u.Id == id);

            if (wniosek == null)
            {
                return false; // Zwracamy false, jeśli użytkownik o podanym ID nie istnieje.
            }

            try
            {
                _context.Wniosek.Remove(wniosek);
                _context.SaveChanges();
                return true; // Zwracamy true, jeśli usunięcie zakończyło się sukcesem.
            }
            catch (Exception)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas usuwania.
                throw new ArgumentException("Błąd zapisu."); // Zwracamy false w przypadku błędu.
            }
        }
        public string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case "pdf":
                    return "application/pdf";
                case "jpeg":
                    return "image/jpeg";
                case "png":
                    return "image/png";
                case "txt":
                    return "text/plain";
                default:
                    return "application/octet-stream"; // Domyślny Content-Type dla innych typów plików
            }
        }


    }
}
