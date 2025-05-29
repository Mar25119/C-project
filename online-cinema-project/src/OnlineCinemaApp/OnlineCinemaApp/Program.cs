using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string connectionString = "Data Source=OnlineCinema.db;Version=3;";
        var dbContext = new DbContext(connectionString);

        // Создание репозиториев
        var userRepository = new UserRepository(dbContext);
        var contentRepository = new ContentRepository(dbContext);
        var reviewRepository = new ReviewRepository(dbContext);
        var subscriptionRepository = new SubscriptionRepository(dbContext);

        // Создание сервисов
        var userService = new UserService(userRepository);
        var contentService = new ContentService(contentRepository);
        var reviewService = new ReviewService(reviewRepository);
        var subscriptionService = new SubscriptionService(subscriptionRepository);

        bool exit = false;
        int currentUserId = -1;

        while (!exit)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Онлайн-кинотеатр");
                Console.WriteLine("1. Вход");
                Console.WriteLine("2. Регистрация");
                Console.WriteLine("3. Просмотреть фильмы");
                Console.WriteLine("4. Просмотреть сериалы");
                Console.WriteLine("5. Просмотреть жанры");
                Console.WriteLine("6. Поиск контента");
                Console.WriteLine("7. Оставить отзыв");
                Console.WriteLine("8. Оформить подписку");
                Console.WriteLine("9. Выйти");

                if (currentUserId != -1)
                {
                    Console.WriteLine("\n(Вы вошли как пользователь ID: " + currentUserId + ")");
                }

                Console.Write("\nВыберите действие: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine("=== Вход ===");
                        Console.Write("Email: ");
                        string email = Console.ReadLine();
                        Console.Write("Пароль: ");
                        string password = ReadPassword();
                        if (userService.Login(email, password))
                        {
                            var user = userRepository.GetUserByEmail(email);
                            currentUserId = user.Id;
                            Console.WriteLine($"Вход выполнен успешно! ID пользователя: {currentUserId}");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка входа. Проверьте данные.");
                        }
                        break;

                    case "2":
                        Console.Clear();
                        Console.WriteLine("=== Регистрация ===");
                        Console.Write("Имя: ");
                        string name = Console.ReadLine();
                        Console.Write("Email: ");
                        email = Console.ReadLine();
                        Console.Write("Пароль: ");
                        password = ReadPassword();
                        userService.Register(name, email, password);
                        Console.WriteLine("Регистрация прошла успешно!");
                        break;

                    case "3":
                        Console.Clear();
                        Console.WriteLine("=== Фильмы ===");
                        var movies = contentService.GetAllMovies();
                        foreach (var movie in movies)
                        {
                            movie.DisplayInfo();
                        }
                        break;

                    case "4":
                        Console.Clear();
                        Console.WriteLine("=== Сериалы ===");
                        var seriesList = contentService.GetAllSeries();
                        foreach (var series in seriesList)
                        {
                            series.DisplayInfo();
                        }
                        break;

                    case "5":
                        Console.Clear();
                        Console.WriteLine("=== Жанры ===");
                        var genres = contentService.GetAllGenres();
                        foreach (var genre in genres)
                        {
                            Console.WriteLine(genre.Name);
                        }
                        break;

                    case "6":
                        Console.Clear();
                        Console.WriteLine("=== Поиск ===");
                        Console.Write("Введите запрос: ");
                        string query = Console.ReadLine();

                        var results = contentService.Search(query);

                        Console.WriteLine("Результаты:");
                        foreach (var item in results)
                        {
                            item.DisplayInfo(); 
                        }
                        break;

                    case "7":
                        if (currentUserId == -1)
                        {
                            Console.WriteLine("Сначала войдите в систему.");
                            break;
                        }

                        Console.Clear();
                        Console.WriteLine("=== Оставить отзыв ===");
                        Console.Write("ID контента: ");
                        int contentId = int.Parse(Console.ReadLine());
                        Console.Write("Текст отзыва: ");
                        string text = Console.ReadLine();
                        Console.Write("Оценка (1–10): ");
                        int rating = int.Parse(Console.ReadLine());

                        reviewService.AddReview(currentUserId, contentId, text, rating);
                        Console.WriteLine("Отзыв успешно добавлен!");
                        break;

                    case "8":
                        if (currentUserId == -1)
                        {
                            Console.WriteLine("Сначала войдите в систему.");
                            break;
                        }

                        Console.Clear();
                        Console.WriteLine("=== Подписка ===");
                        subscriptionService.Subscribe(currentUserId, "Premium");
                        Console.WriteLine("Подписка оформлена на месяц.");
                        break;

                    case "9":
                        exit = true;
                        Console.WriteLine("Выход из приложения...");
                        break;

                    default:
                        Console.WriteLine("Некорректный выбор.");
                        break;
                }

                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
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