using Microsoft.AspNetCore.Mvc;
using ProjektDaniel.DTOs;
using ProjektDaniel.services;
using static ProjektDaniel.services.AccountService;

namespace ProjektDaniel.Controllers
{
    [Route("api/accoutns")]
    [ApiController]
    public class AccountController : ControllerBase 
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDTO dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody]LoginDTO dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
