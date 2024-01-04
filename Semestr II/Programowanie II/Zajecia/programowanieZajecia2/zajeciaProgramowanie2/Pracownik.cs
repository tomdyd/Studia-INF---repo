using ProgramowanieObiektowe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zajeciaProgramowanie2
{
    internal struct Pracownik
    {
        #region Pola
        public string _firstName;
        public string _lastName;
        public byte _age;
        public string _email;
        public Stanowisko _stanowisko;
        
        #endregion

        #region Funkcje

        public string GetInfo()
        {
            return $"Pracownik: Imie: {_firstName}, Nazwisko: {_lastName}, Wiek: {_age}," +
                $" Email {_email}, Stanowisko: {_stanowisko}";
        }

        public void IncreaseAge()
        {
            _age++;
        }

        #endregion

        #region Konstruktory
        public Pracownik(string firstName, string lastName, byte age, string email, Stanowisko stanowisko)
        {
            _firstName = firstName;
            _lastName = lastName;
            _age = age;
            _email = email;
            _stanowisko = stanowisko;
        }

        //public Pracownik(string firstName)
        //{
        //    _firstName = firstName;
        //    _lastName = "Default";
        //    _age = 25;
        //    _email = "default@mail.pl";
        //    _stanowisko = Stanowisko.Majster;
        //}

        public Pracownik(string firstName) : this (firstName, "Default", 25, "default@mail.pl", Stanowisko.Majster)
        {

        }
        #endregion
    }
}
