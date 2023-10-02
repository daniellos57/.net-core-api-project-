using Microsoft.AspNetCore.Mvc;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;

namespace ProjektDaniel.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RolaController : ControllerBase
    {
        private readonly RolaDbContext _context;

        public RolaController(RolaDbContext context)
        {
            _context = context;
        }

    }
}
