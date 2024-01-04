using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WitajSwiecie
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathLogin = "login.txt";
            string pathPassword = "password.txt";

            if (!File.Exists(pathLogin))
            {
                File.CreateText(pathLogin);
            }
            else if (!File.Exists(pathPassword))
            {
                File.CreateText(pathPassword);
            }

            StreamWriter sw;

            Console.Write("Podaj login: ");
            string login = Console.ReadLine();
            Console.Write("Podaj hasło: ");
            string password = Console.ReadLine();

            StreamReader sr = File.OpenText(pathLogin);
            string login1 = "";
            string password1 = "";

            while (login != login1)
            {
                login1 = sr.ReadLine();
                if (login1 == login)
                    break;

                else
                {
                    Console.Write("Podano błędne hasło! Spróbuj jeszcze raz: ");
                    login = Console.ReadLine();
                }
            }
            sr.Close();

            sr = File.OpenText(pathPassword);

            while (password != password1)
            {
                password1 = sr.ReadLine();
                if (password1 == password)
                    break;

                else
                {
                    Console.Write("Podano błędne hasło! Spróbuj jeszcze raz: ");
                    password = Console.ReadLine();
                }
            }
            sr.Close ();

            Console.WriteLine("Udało Ci się zalogować!");


            Console.ReadKey();


        }
    }
}