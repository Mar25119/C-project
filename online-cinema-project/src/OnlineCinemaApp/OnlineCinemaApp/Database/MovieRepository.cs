using System;
using System.Data.SQLite;

class MovieRepository
{
    private DbContext _context;

    public MovieRepository(DbContext context)
    {
        _context = context;
    }

    public void GetAllMovies()
    {
        using (var connection = _context.CreateConnection())
        {
            if (connection == null) return;

            string query = "SELECT Title, Year FROM Content WHERE Id IN (SELECT ContentId FROM Movies)";
            using (var command = new SQLiteCommand(query, connection))
            {
                try
                {
                    using (var reader = command.ExecuteReader())
                    {
                        Console.WriteLine("Фильмы в каталоге:");
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["Title"]} ({reader["Year"]})");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка получения фильмов: {ex.Message}");
                }
            }
        }
    }
}