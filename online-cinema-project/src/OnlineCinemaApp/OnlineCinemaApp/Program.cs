using System;

class Program
{
    static void Main()
    {
        bool exit = false;
        while (!exit)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в OnlineCinema!");
                Console.WriteLine("1. Вход");
                Console.WriteLine("2. Регистрация");
                Console.WriteLine("3. Просмотреть каталог фильмов");
                Console.WriteLine("4. Поиск фильма/сериала");
                Console.WriteLine("5. Выйти");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        Register();
                        break;
                    case "3":
                        ViewMovies();
                        break;
                    case "4":
                        SearchContent();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Выход из приложения.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                Console.WriteLine("Попробуйте снова.");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }
    }

    static void Login()
    {
        Console.Clear();
        Console.WriteLine("=== Вход ===");
        Console.Write("Введите email: ");
        string email = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = ReadPassword();

        // Здесь будет проверка в БД
        Console.WriteLine($"\nВход выполнен для пользователя: {email}");
    }

    static void Register()
    {
        Console.Clear();
        Console.WriteLine("=== Регистрация ===");

        Console.Write("Введите имя: ");
        string name = Console.ReadLine();

        Console.Write("Введите email: ");
        string email = Console.ReadLine();

        Console.Write("Введите пароль: ");
        string password = ReadPassword();

        // Здесь будет сохранение в БД
        Console.WriteLine("\nРегистрация прошла успешно!");
    }

    static void ViewMovies()
    {
        Console.Clear();
        Console.WriteLine("=== Каталог фильмов ===");

        // Заглушка — имитация данных из БД
        Console.WriteLine("1. Терминатор (1984) — Боевик");
        Console.WriteLine("2. Назад в будущее (1985) — Фантастика");
        Console.WriteLine("3. Интерстеллар (2014) — Научная фантастика");
    }

    static void SearchContent()
    {
        Console.Clear();
        Console.WriteLine("=== Поиск контента ===");

        Console.Write("Введите запрос для поиска: ");
        string query = Console.ReadLine();

        // Имитация результата
        Console.WriteLine($"\nРезультаты по запросу \"{query}\":");
        Console.WriteLine("1. Терминатор");
        Console.WriteLine("2. Терминатор 2");
    }

    static string ReadPassword()
    {
        string password = "";
        ConsoleKeyInfo key;

        do
        {
            key = Console.ReadKey(true);

            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
            {
                password += key.KeyChar;
                Console.Write("*");
            }
            else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password = password.Substring(0, password.Length - 1);
                Console.Write("\b \b");
            }
        } while (key.Key != ConsoleKey.Enter);

        Console.WriteLine();
        return password;
    }
}