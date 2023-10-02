using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.ComponentModel.DataAnnotations;

namespace ProjektDaniel.DTOs
{
    public class RegisterUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword {  get; set; }
        public string Imię { get; set; } = default!;
        public string Nazwisko { get; set; } = default!;
        public int? IdRola { get; set; } = 1;
    }
}
