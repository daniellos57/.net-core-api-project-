using Microsoft.AspNetCore.Mvc;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;

namespace ProjektDaniel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolaController : ControllerBase
    {
        private readonly RolaDbContext _context;

        public RolaController(RolaDbContext context)
        {
            _context = context;
        }

        /*[HttpGet]
        public ActionResult<IEnumerable<RolaDTO>> PobierzRole()
        {
            var role = _context.Rola
                .Select(r => new RolaDTO
                {
                    Id = r.Id,
                    Nazwa = r.Nazwa
                    // Mapuj inne właściwości encji "Rola" na właściwości DTO, jeśli są dostępne
                })
                .ToList();

            return Ok(role);
        }
        [HttpGet("{id}")]
        public ActionResult<RolaDTO> PobierzRola(int id)
        {
            var rola = _context.Rola
                .Where(u => u.Id == id)
                .Select(u => new RolaDTO
                {
                    Id = u.Id,
                    Nazwa = u.Nazwa,
                    
                })
                .FirstOrDefault();

            if (rola == null)
            {
                return NotFound();
            }

            return Ok(rola);
        }

        [HttpPut("{id}")]
        public IActionResult AktualizujRola(int id, RolaDTO RolaDTO)
        {
            var rola = _context.Rola.FirstOrDefault(u => u.Id == id);

            if (rola == null)
            {
                return NotFound(); // Jeśli użytkownik o podanym ID nie istnieje, zwracamy 404 Not Found.
            }

            // Aktualizujemy dane użytkownika na podstawie przekazanego DTO.
            rola.Nazwa = RolaDTO.Nazwa;
           

            try
            {
                _context.SaveChanges(); // Zapisujemy zmiany w bazie danych.
                return NoContent(); // Jeśli aktualizacja zakończyła się sukcesem, zwracamy 204 No Content.
            }
            catch (Exception)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas zapisu do bazy danych.
                return StatusCode(500, "Wystąpił błąd podczas zapisu do bazy danych.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult UsunRola(int id)
        {
            var rola = _context.Rola.FirstOrDefault(u => u.Id == id);

            if (rola == null)
            {
                return NotFound(); // Jeśli użytkownik o podanym ID nie istnieje, zwracamy 404 Not Found.
            }

            try
            {
                _context.Rola.Remove(rola); // Usuwamy role z kontekstu bazy danych.
                _context.SaveChanges(); // Zapisujemy zmiany w bazie danych.
                return NoContent(); // Jeśli usunięcie zakończyło się sukcesem, zwracamy 204 No Content.
            }
            catch (Exception)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas usuwania.
                return StatusCode(500, "Wystąpił błąd podczas usuwania użytkownika.");
            }
        }

        /*public IActionResult Index()
        {
            return View();
        }*/
    }
}
