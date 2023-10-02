using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;
using ProjektDaniel.ErrorHandler;
using ProjektDaniel.Models;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProjektDaniel.services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDTO dto);
        void RegisterUser(RegisterUserDTO dto);
    }
    public class AccountService : IAccountService
    {
       
        private readonly UżytkownikDbContext _context;
        private readonly IPasswordHasher<Użytkownik> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        public AccountService(UżytkownikDbContext context, IPasswordHasher<Użytkownik> passwordHasher,AuthenticationSettings authenticationSettings) { 
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }
        public void RegisterUser(RegisterUserDTO dto)
        {
            
            var newUżytkownik = new Użytkownik()
            {
                Email = dto.Email,
                Imię = dto.Imię,
                Nazwisko = dto.Nazwisko,
                IdRola = dto.IdRola,
            };
           var hashedPassword = _passwordHasher.HashPassword(newUżytkownik, dto.Password);
            newUżytkownik.PasswordHash = hashedPassword;
            _context.Użytkownicy.Add(newUżytkownik); // Dodaj nowego użytkownika do kontekstu bazy danych.
            _context.SaveChanges();
        }

        public string GenerateJwt(LoginDTO dto)
        { 
            var user = _context.Użytkownicy.Include(u => u.IdRola).FirstOrDefault(u => u.Email == dto.Email);
            if (user == null)
            {
                throw new BadRequestException("Invalid username or expection");
            }

           var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if(result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Failed)
            {
                    throw new BadRequestException("Invalid username or expection");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Imię}{user.Nazwisko}"),
                new Claim(ClaimTypes.Role, $"{user.IdRola}"),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
             var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }


    }
}
