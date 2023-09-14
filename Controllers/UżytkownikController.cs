using Microsoft.AspNetCore.Mvc;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;
using ProjektDaniel.services;
namespace ProjektDaniel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UżytkownikController : ControllerBase
    {
       
        private readonly UżytkownikService _użytkownikService;

        public UżytkownikController(UżytkownikService użytkownikService)
        {
          
            _użytkownikService = użytkownikService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UżytkownikDTO>> PobierzWszystkichUzytkownikow()
        {
            return Ok(_użytkownikService.PobierzWszystkichUzytkownikow());
        }



        [HttpGet("{id}")]
        public ActionResult<UżytkownikDTO> PobierzUzytkownika(int id)
        {    
            return Ok(_użytkownikService.PobierzUzytkownika(id));
        }

        [HttpPost]
        public IActionResult DodajUzytkownika(UżytkownikDTO uzytkownikDTO)
        {
            
                var createdUzytkownik = _użytkownikService.DodajUzytkownika(uzytkownikDTO);

                return CreatedAtAction(nameof(DodajUzytkownika), new { id = createdUzytkownik.Id }, createdUzytkownik);
            
  
        }   

        [HttpPut("{id}")]
        public IActionResult AktualizujUzytkownika(int id, UżytkownikDTO uzytkownikDTO)
        {
            var isUpdated = _użytkownikService.AktualizujUzytkownika(id, uzytkownikDTO);

            return Ok(); // Jeśli aktualizacja zakończyła się sukcesem, zwracamy 204 No Content.
        }

        [HttpDelete("{id}")]
        public IActionResult UsunUzytkownika(int id)
        {
            var isDeleted = _użytkownikService.UsunUzytkownika(id);

            return Ok(); // Jeśli usunięcie zakończyło się sukcesem, zwracamy 204 No Content.
        }

        /*[HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Użytkownik użytkownik)
        {
            await _context.Użytkowniks.AddAsync(użytkownik);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetById", new { id = użytkownik.Id }, użytkownik);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult>  Update(int id, Użytkownik użytkownik)
        {
            if(id != użytkownik.Id) return BadRequest();
            _context.Entry(użytkownik).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete (int id)
        {
            var uzytkownikToDelete = await _context.Użytkowniks.FindAsync(id);
            if (uzytkownikToDelete == null) return NotFound();

            _context.Użytkowniks.Remove(uzytkownikToDelete);
            await _context.SaveChangesAsync() ;
            return NoContent();
        }*/
    }
}
