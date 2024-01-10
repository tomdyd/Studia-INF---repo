using mechanizmLogowania;
using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;

int number = 0;
List<User> users = new List<User>();

string admin = "admin";
SecureString adminpass = new();

foreach (char c in "admin")
{
    adminpass.AppendChar(c);
}

users.Add(new User(admin, adminpass));

DefaultMenu();

#region metody
void DefaultMenu()
{
    while (number != 3)
    {
        Console.Clear();
        Console.WriteLine("      MENU");
        Console.WriteLine("******************");
        Console.WriteLine("1. Zaloguj się");
        Console.WriteLine("2. Zarejestruj się");
        Console.WriteLine("3. Wyjdź");
        Console.Write("\nWybierz opcje: ");

        bool isNumber = int.TryParse(Console.ReadLine(), out number);
        switch (number)
        {
            case 1:
                Console.Clear();
                Console.Write("Podaj login: ");
                string login = Console.ReadLine();
                login = login.ToLower();
                bool loginExists = users.Exists(x => x.GetEmail() == login);

                if (loginExists)
                {
                    int index = users.FindIndex(a => a.GetEmail() == login);

                    Console.Write("Podaj hasło: ");
                    SecureString password = User.GetConsoleSecurePassword();
                    bool passwordExists = users.Exists(x => x.IsEqualTo(password, x.GetPassword()));

                    if (passwordExists)
                    {
                        int index1 = users.FindIndex(a => a.IsEqualTo(password, a.GetPassword()));

                        if (index == 0 && index1 == 0)
                            AdminMenu();

                        else if (index1 == index)
                        {
                            UserMenu();
                        }

                        else
                        {
                            Console.WriteLine("Nieprawidłowe hasło! Kliknij dowolny klawisz aby kontynować.");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nNieprawidłowe hasło! Kliknij dowolny klawisz aby kontynować.");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Uzytkownik nie istnieje! Kliknij dowolny klawisz aby kontynować.");
                    Console.ReadKey();
                }
                break;

            case 2:
                Console.Clear();

                users.Add(new User());

                Console.WriteLine("\nRejestracja przebiegła pomyślnie. Wciśnij dowolny klawisz " +
                    "aby wrócić do menu głównego.\n");
                Console.ReadKey();
                break;

            case 3:
                Environment.Exit(0);
                break;
        }
    }
}
void AdminMenu()
{
    int AdminMenuNumber = 0;

    while (AdminMenuNumber != 3)
    {
        Console.Clear();
        Console.WriteLine("      MENU");
        Console.WriteLine("******************");
        Console.WriteLine("1. Dodaj użytkownika");
        Console.WriteLine("2. Usuń użytkownika");
        Console.WriteLine("3. Wyloguj się");
        Console.Write("\nWybierz opcje: ");

        bool isNumber = int.TryParse(Console.ReadLine(), out AdminMenuNumber);
        Console.WriteLine();
        switch (AdminMenuNumber)
        {
            case 1:
                users.Add(new User(admin));
                Console.WriteLine("\nUżytkownik został utworzony! Kliknij dowolny klawisz aby kontunować.");
                Console.ReadKey();
                AdminMenu();
                break;
            case 2:
                DisplayUsersList();              
                
                break;
            case 3:
                DefaultMenu();
                break;
            default:
                continue;
        }
    }
}
void UserMenu()
{
    Console.Clear();
    Console.WriteLine("Udało Ci się zalogować. Kliknij dowolny przycisk aby kontynuować.");
    Console.ReadKey();    

    int UserMenuNumber = 0;

    while (UserMenuNumber != 3)
    {
        Console.Clear();
        Console.WriteLine("      MENU");
        Console.WriteLine("******************");
        Console.WriteLine("1. BRAK FUNKCJONALNOŚCI"); //todo
        Console.WriteLine("2. BRAK FUNKCJONALNOŚCI"); //todo
        Console.WriteLine("3. Wyloguj się");
        Console.Write("\nWybierz opcje: ");

        bool isNumber = int.TryParse(Console.ReadLine(), out UserMenuNumber);
        Console.WriteLine();

        switch (UserMenuNumber)
        {
            case 1:
                continue;
            case 2:
                continue;
            case 3:
                DefaultMenu();
                break;
            default:
                continue;
        }
    }
}
void UsersList()
{
    Console.Clear();
    foreach (User user in users)
    {
        if (users.IndexOf(user) == 0)
        {
            continue;
        }
        user.GetFirstName();
        user.GetLastName();
        user.GetAge();
        string email = user.GetEmail();
        Console.WriteLine($"Email: {email}");
        Console.WriteLine("Numer użytkownika: " + users.FindIndex(a => a.GetEmail() == user.GetEmail()));
        Console.WriteLine();
    }
}
void DisplayUsersList()
{
    int DisplayUsersListNumber = 0;
    while (DisplayUsersListNumber != 1 || DisplayUsersListNumber != 2 || DisplayUsersListNumber != 3)
    {
        Console.Clear();
        Console.WriteLine("1. Wyświetl listę użytkowników.");
        Console.WriteLine("2. Kontynuuj bez wyświetlania listy użytkowników.");
        Console.WriteLine("3. Wróć");
        Console.Write("\nWybierz opcje: ");
        bool isNumber = int.TryParse(Console.ReadLine(), out DisplayUsersListNumber);

        switch (DisplayUsersListNumber)
        {
            case 1:
                UsersList();
                RemovingMenu();
                break;

            case 2:
                Console.Clear();
                RemovingMenu();
                break;

            case 3:
                AdminMenu();
                break;


            default:
                continue;
        }
        break;
    }
}
void RemovingMenu()
{
    int RemovingMenuNumber = 0;
    while (RemovingMenuNumber != 1 || RemovingMenuNumber != 2)
    {
        Console.WriteLine("1. Usuń użytkownika");
        Console.WriteLine("2. Wróć");
        Console.Write("\nWybierz opcje: ");

        bool isNumber = int.TryParse(Console.ReadLine(), out RemovingMenuNumber);

        switch (RemovingMenuNumber)
        {
            case 1:
                Console.Write("Wybierz użytkownika którego chcesz usunąć: ");

                isNumber = int.TryParse(Console.ReadLine(), out int userNumber);

                while (!isNumber || userNumber >= users.Count)
                {
                    Console.Write("Nie ma użytkownika o takim numerze, podaj numer ponownie: ");
                    isNumber = int.TryParse(Console.ReadLine(), out userNumber);
                }

                if (userNumber == 0)
                {
                    Console.Write("Nie można usunąć konta administratora!" +
                        " Kliknij dowolny przycisk aby wrócić do panelu administratora.");
                    Console.ReadKey();
                    AdminMenu();
                }

                else
                {
                    users.RemoveAt(userNumber);
                    Console.WriteLine("Użytkownik usunięty! Kliknij dowolny klawisz aby kontynuować.");
                    Console.ReadKey();
                    AdminMenu();
                }
                break;
            case 2:
                DisplayUsersList();
                break;

            default:
                continue;
        }
    }
}
#endregion




