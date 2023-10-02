using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjektDaniel.DTOs;
using ProjektDaniel.services;
using System.Net.Mail;
using System.Net;

namespace ProjektDaniel.Controllers
{
    [Route("api/wnioski")]
    [ApiController]
    public class WniosekController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly WniosekService _wniosekService;

        public WniosekController( WniosekService wniosekService, IConfiguration configuration)
        {
            _wniosekService = wniosekService;
            _configuration = configuration;
        }
        //1

        [HttpGet]
        public ActionResult<IEnumerable<WniosekDTO>> PobierzWszystkichWniosek()
        { 
            return Ok(_wniosekService.PobierzWszystkichWniosek());
        }


        //2
        [HttpGet("{id}")]
        public ActionResult<WniosekDTO> PobierzUzytkownika(int id)
        {
            return Ok(_wniosekService.PobierzWniosekById(id));
        }

        //3
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> DodajPlik([FromForm] DodajWniosekDTO dodajWniosekDTO)
        {
            await _wniosekService.DodajWniosekZPlikiem(dodajWniosekDTO);
            return Ok("Plik został dodany do bazy danych.");
        }

        //4
        [HttpDelete("{id}")]
        public IActionResult UsunUzytkownika(int id)
        {
            var result = _wniosekService.UsunWniosek(id);
            return Ok(); // Zwracamy 204 No Content, jeśli usunięcie zakończyło się sukcesem.
        }
         

       
    }
}
