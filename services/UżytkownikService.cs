using System.Collections.Generic;
using System.Linq;
using ProjektDaniel.Data;
using ProjektDaniel.DTOs;
using ProjektDaniel.Models;

namespace ProjektDaniel.services
{
    public class UżytkownikService
    {
        private readonly UżytkownikDbContext _context;
        public UżytkownikService(UżytkownikDbContext context)
        {
            _context = context;
        }

        public List<UżytkownikDTO> PobierzWszystkichUzytkownikow()
        {
            var uzytkownicy = _context.Użytkownicy
                .Select(u => new UżytkownikDTO
                {
                    Id = u.Id,
                    Imię = u.Imię,
                    Nazwisko = u.Nazwisko,
                    Email = u.Email
                })
                .ToList();
            return uzytkownicy;
        }

        public UżytkownikDTO PobierzUzytkownika(int id)
        {
            var uzytkownik = _context.Użytkownicy
                .Where(u => u.Id == id)
                .Select(u => new UżytkownikDTO
                {
                    Id = u.Id,
                    Imię = u.Imię,
                    Nazwisko = u.Nazwisko,
                    Email = u.Email
                })
                .FirstOrDefault();
            if (uzytkownik == null)
            {
                throw new Exception("Złe id"); // Użytkownik o podanym ID nie istnieje.
            }

            return uzytkownik;
        }

        public bool AktualizujUzytkownika(int id, UżytkownikDTO uzytkownikDTO)
        {
            var uzytkownik = _context.Użytkownicy.FirstOrDefault(u => u.Id == id);

            if (uzytkownik == null)
            {
                throw new Exception("Złe id"); // Użytkownik o podanym ID nie istnieje.
            }

            // Aktualizujemy dane użytkownika na podstawie przekazanego DTO.
            uzytkownik.Imię = uzytkownikDTO.Imię;
            uzytkownik.Nazwisko = uzytkownikDTO.Nazwisko;
            uzytkownik.Email = uzytkownikDTO.Email;

            try
            {
                _context.SaveChanges(); // Zapisujemy zmiany w bazie danych.
                return true; // Aktualizacja zakończyła się sukcesem.
            }
            catch (Exception e)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas zapisu do bazy danych.
                throw new Exception("Błąd aktualizacji", e); // Błąd podczas aktualizacji.
            }
        }

        public UżytkownikDTO DodajUzytkownika(UżytkownikDTO uzytkownikDTO)
        {
            var nowyUzytkownik = new Użytkownik
            {
                Imię = uzytkownikDTO.Imię,
                Nazwisko = uzytkownikDTO.Nazwisko,
                Email = uzytkownikDTO.Email
            };

            try
            {
                _context.Użytkownicy.Add(nowyUzytkownik); // Dodaj nowego użytkownika do kontekstu bazy danych.
                _context.SaveChanges(); // Zapisz zmiany w bazie danych.
                uzytkownikDTO.Id = nowyUzytkownik.Id; // Ustaw ID nowego użytkownika w DTO.
                return uzytkownikDTO;
            }
            catch (Exception e)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas dodawania użytkownika.
                throw new Exception("Wystąpił błąd podczas dodawania użytkownika.",e);
            }
        }

        public bool UsunUzytkownika(int id)
        {
            var uzytkownik = _context.Użytkownicy.FirstOrDefault(u => u.Id == id);

            if (uzytkownik == null)
            {
                throw new Exception("Złe id "); // Użytkownik o podanym ID nie istnieje.
            }

            try
            {
                _context.Użytkownicy.Remove(uzytkownik); // Usuwamy użytkownika z kontekstu bazy danych.
                _context.SaveChanges(); // Zapisujemy zmiany w bazie danych.
                return true; // Usunięcie zakończyło się sukcesem.
            }
            catch (Exception e)
            {
                // Obsłuż wyjątek, jeśli coś poszło nie tak podczas usuwania.
                throw new Exception("Błąd podczas usuwania", e); // Błąd podczas usuwania.
            }
        }

    }
}