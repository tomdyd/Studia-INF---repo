using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace mechanizmLogowania
{
    public class User
    {
        private string _firstname;
        private string _lastname;
        private byte _age;
        private string _email;
        private SecureString _password;

        #region Konstruktory
        public User()
        {
            _firstname = SetFirstName();
            _lastname = SetLastName();
            _age = SetAge();
            _email = SetEmail();
            _password = SetPassword();
        }
        public User(string admin)
        {
            _firstname = AdminSetFirstName();
            _lastname = AdminSetLastName();
            _age = AdminSetAge();
            _email = AdminSetEmail();
            _password = AdminSetPassword();
        }

        public User(string adminLogin, SecureString adminPassword)
        {
            _email = adminLogin;
            _password = adminPassword;
        }
        #endregion

        #region metody
        private string SetFirstName()
        {
            Regex regex = new(@"^[A-Z][1]*[a-z]*$");
            bool isMatch = false;
            string firstName = "";

            while (!isMatch)
            {
                Console.Write("Podaj imie: ");
                firstName = Console.ReadLine();
                isMatch = regex.IsMatch(firstName);
                if (!isMatch)
                    Console.WriteLine("Imie może składać się tylko z liter i musi zaczynać się od wielkiej litery!" +
                        @" Spróbuj jeszcze raz.");
            }
            //AddCredentials(firstName);
            return firstName;
        }
        private string SetLastName()
        {
            Regex regex = new(@"^[A-Z][1]*[a-z]*$");
            bool isMatch = false;
            string lastName = "";

            while (!isMatch)
            {
                Console.Write("Podaj nazwisko: ");
                lastName = Console.ReadLine();
                isMatch = regex.IsMatch(lastName);
                if (!isMatch)
                    Console.WriteLine("Nazwisko może składać się tylko z liter i musi zaczynać się od wielkiej litery!" +
                        @" Spróbuj jeszcze raz.");
            }
            return lastName;
                
        }
        private byte SetAge()
        {
            bool isByte = false;
            byte age = 0;
            while (!isByte)
            {
                Console.Write("Podaj wiek: ");
                isByte = byte.TryParse(Console.ReadLine(), out age);
                if(!isByte)
                    Console.WriteLine("Wiek musi być liczbą!");
            }
                return age;
        }
        private string SetEmail()
        {
            Regex regex = new(@"[A-Za-z0-9]+\.*[A-Za-z0-9]*@[A-Za-zA-Z]+\.[A-Za-z]{2,3}$");
            string email = "";
            bool isEmail = false;
            while (!isEmail)
            {
                Console.Write("Podaj email: ");
                email = Console.ReadLine();
                isEmail = regex.IsMatch(email);
                if(!isEmail)
                    Console.Write("Niepoprawny adres email! Spróbuj jeszcze raz.");
            }
            email = email.ToLower();
            return email;
        }
        private SecureString SetPassword()
        {


            SecureString password = new();
            SecureString password1;

            
            bool isMatch = false;            

            while (!isMatch)
            {
                

                Console.Write("Ustaw hasło: ");
                password = GetConsoleSecurePassword();

                IsSecureStringValid(password);
                while (!IsSecureStringValid(password))
                {
                    Console.Write("\nHasło musi składać się z co najmniej 5znaków, jednej dużej litery, jednej cyfry," +
                        " oraz jednego ze znaków specjalnych (!; @; #; $; %; ^; &; *; (; (; ); ): ");
                    Console.Write("\nUstaw hasło: ");
                    password = GetConsoleSecurePassword();
                }

                Console.Write("\nPodaj hasło jeszcze raz: ");
                password1 = GetConsoleSecurePassword();

                isMatch = IsEqualTo(password, password1);

                if (isMatch)
                    break;
                else
                {
                    Console.WriteLine("\nHasła różnią się między sobą! Spróbuj jeszcze raz.");
                    continue;
                }
            }
            return password;
        }
        internal void GetFirstName()
        {
            Console.WriteLine($"Imie: {_firstname}");
        }
        internal void GetLastName()
        {
            Console.WriteLine($"Nazwisko: {_lastname}");
        }
        internal void GetAge()
        {
            Console.WriteLine($"Wiek: {_age}");
        }
        internal string GetEmail()
        {
            string email = _email;
            return email;
        }
        internal SecureString GetPassword()
        {
            SecureString password = _password;
            return password;
        }
        internal static SecureString GetConsoleSecurePassword()
        {
            SecureString password = new();
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (password.Length > 0)
                    {
                        password.RemoveAt(password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    password.AppendChar(i.KeyChar);
                    Console.Write("*");
                }
            }
            return password;
        }
        internal bool IsEqualTo(SecureString ss1, SecureString ss2)
        {
            IntPtr bstr1 = IntPtr.Zero;
            IntPtr bstr2 = IntPtr.Zero;
            try
            {
                bstr1 = Marshal.SecureStringToBSTR(ss1);
                bstr2 = Marshal.SecureStringToBSTR(ss2);
                int length1 = Marshal.ReadInt32(bstr1, -4);
                int length2 = Marshal.ReadInt32(bstr2, -4);
                if (length1 == length2)
                {
                    for (int x = 0; x < length1; ++x)
                    {
                        byte b1 = Marshal.ReadByte(bstr1, x);
                        byte b2 = Marshal.ReadByte(bstr2, x);
                        if (b1 != b2) return false;
                    }
                }
                else return false;
                return true;
            }
            finally
            {
                if (bstr2 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr2);
                if (bstr1 != IntPtr.Zero) Marshal.ZeroFreeBSTR(bstr1);
            }
        }
        internal bool IsSecureStringValid(SecureString secureString)
        {
            IntPtr ptr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
            string plainString = Marshal.PtrToStringUni(ptr);
            Marshal.ZeroFreeGlobalAllocUnicode(ptr);

            string regexPattern = "^(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()])[A-Za-z\\d!@#$%^&*()]{5,}$";
            return Regex.IsMatch(plainString, regexPattern);
        }

        #region adminMethods
        internal string AdminSetFirstName()
        {
            Console.Write("Podaj imie: ");
            string firstName = Console.ReadLine();
            return firstName;
        } //przy metodach Admin... wychodze z założenia że administrator
        internal string AdminSetLastName()
        {
            Console.Write("Podaj nazwisko: ");
            string lastName = Console.ReadLine();
            return lastName;
        }  //wie co robi, dlatego w tych metodach nie ma kontroli błędów
        internal byte AdminSetAge()
        {
            Console.Write("Podaj wiek: ");
            bool isByte = byte.TryParse(Console.ReadLine(), out byte age);
            while (!isByte)
            {
                Console.Write("Wiek musi być liczbą! Podaj wiek: ");
                isByte = byte.TryParse(Console.ReadLine(), out age);
            }
            return age;
        }         //(poza metodą AdminSetAge(); gdzie przy wprowadzeniu błędnej
        internal string AdminSetEmail()
        {
            Console.Write("Podaj email: ");
            string email = Console.ReadLine();
            return email;
        }     //wartości program zawiesiłby się)
        internal SecureString AdminSetPassword()
        {
            Console.Write("Ustaw hasło: ");
            SecureString password = GetConsoleSecurePassword();
            return password;
        }
        #endregion      
        
        #endregion
    }
}
