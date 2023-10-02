using Aspose.Words;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;
using ProjektDaniel.Models;
using ProjektDaniel.services;
using System.Globalization;

namespace ProjektDaniel.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly WniosekDbContext _context;
        private readonly DaneWniosekDbContext _DaneWniosekDbContext; 
        private readonly WniosekService _wniosekService;


  

        public FilesController(WniosekDbContext context, WniosekService wniosekService, DaneWniosekDbContext DaneWniosekDbContext) 
        {
            _wniosekService = wniosekService;
            _context = context;
            _DaneWniosekDbContext = DaneWniosekDbContext;

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var wniosek = await _context.Wniosek
                .FirstOrDefaultAsync(w => w.Id == id);

            if (wniosek == null)
            {
                return NotFound(); // Wniosek o podanym identyfikatorze nie istnieje
            }

            if (wniosek.Plik == null || wniosek.Plik.Length == 0)
            {
                return NotFound("Plik nie istnieje w bazie danych."); // Plik nie jest dostępny w bazie danych
            }

            // Odczytaj rozszerzenie z wniosku
            string fileExtension = wniosek.Rozszerzenie;

            // Określ odpowiedni Content-Type na podstawie rozszerzenia
            string contentType = _wniosekService.GetContentType(fileExtension);

            // Zwróć plik jako odpowiedź do pobrania
            string fileName = wniosek.Nazwa + "." + fileExtension;

            // Zwróć plik jako odpowiedź do pobrania
            return File(wniosek.Plik, contentType, fileName);
        }



    }
}