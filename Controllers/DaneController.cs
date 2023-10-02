using Microsoft.AspNetCore.Mvc;
using ProjektDaniel.DTOs;
using ProjektDaniel.Services;

namespace ProjektDaniel.Controllers
{
    [Route("api/dane")]
    [ApiController]
    public class DaneController : ControllerBase
    {
        private readonly DaneService _daneService;

        public DaneController(DaneService daneService)
        {
            _daneService = daneService;
        }

        [HttpGet("WczytajPlik/{wniosekId}")]
        public IActionResult WczytajPlik(int wniosekId)
        {
            try
            {
                var fileContent = _daneService.WczytajPlik(wniosekId);
                return Ok(fileContent);
            }
            catch (Exception ex)
            {
                return BadRequest($"Wystąpił błąd: {ex.Message}");
            }
        }
       


    }
}