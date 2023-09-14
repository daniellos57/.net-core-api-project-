using ProjektDaniel.Data;
using Aspose.Words;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ProjektDaniel.Models;
using ProjektDaniel.DTOs;

namespace ProjektDaniel.Services
{
    public class DaneService
    {
        private readonly WniosekDbContext _wniosekDbContext;
        private readonly DaneWniosekDbContext _daneWniosekDbContext;

        public DaneService(WniosekDbContext wniosekDbContext, DaneWniosekDbContext daneWniosekDbContext)
        {
            _wniosekDbContext = wniosekDbContext;
            _daneWniosekDbContext = daneWniosekDbContext;
        }

        public async Task<string> WczytajPlik(int wniosekId)
        {
            var wniosek = _wniosekDbContext.Wniosek.Find(wniosekId);

            if (wniosek == null)
                throw new ArgumentException("Wniosek o podanym ID nie istnieje.");

            if (wniosek.Plik == null || wniosek.Plik.Length == 0)
                throw new ArgumentException("Plik jest pusty.");

            using (var memoryStream = new MemoryStream(wniosek.Plik))
            {
                var fileContent = ExtractTextFromDoc(memoryStream);
                return fileContent;
            }
        }
      

        private string ExtractTextFromDoc(Stream docStream)
        {
            try
            {
                var doc = new Document(docStream);
                return doc.GetText();
            }
            catch (Exception ex)
            {
                throw new Exception($"Wystąpił błąd podczas ekstrakcji tekstu z pliku .doc: {ex.Message}");
            }
        }
    }
}