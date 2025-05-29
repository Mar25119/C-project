using System.Data.SQLite;

public class ContentRepository
{
    private readonly DbContext _context;

    public ContentRepository(DbContext context)
    {
        _context = context;
    }

    public List<Movie> GetAllMovies()
    {
        var movies = new List<Movie>();
        using (var connection = _context.CreateConnection())
        {
            string query = @"
            SELECT 
                m.ContentId AS Id,
                c.Title,
                c.Year,
                m.Duration,
                m.Director
            FROM Movies m
            JOIN Content c ON m.ContentId = c.Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        movies.Add(new Movie
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Year = Convert.ToInt32(reader["Year"]),
                            Duration = Convert.ToInt32(reader["Duration"]),
                            Director = reader["Director"].ToString()
                        });
                    }
                }
            }
        }
        return movies;
    }

    public List<TvSeries> GetAllSeries()
    {
        var seriesList = new List<TvSeries>();
        using (var connection = _context.CreateConnection())
        {
            string query = @"
            SELECT 
                s.ContentId AS Id,
                c.Title,
                c.Year,
                s.Seasons,
                s.Episodes
            FROM TvSeries s
            JOIN Content c ON s.ContentId = c.Id";

            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seriesList.Add(new TvSeries
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Title = reader["Title"].ToString(),
                            Year = Convert.ToInt32(reader["Year"]),
                            Seasons = Convert.ToInt32(reader["Seasons"]),
                            Episodes = Convert.ToInt32(reader["Episodes"])
                        });
                    }
                }
            }
        }
        return seriesList;
    }

    public List<Genre> GetAllGenres()
    {
        var genres = new List<Genre>();
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT Id, Name FROM Genres";
            using (var command = new SQLiteCommand(query, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genres.Add(new Genre
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
        }
        return genres;
    }

    public List<Content> Search(string query)
    {
        var results = new List<Content>();
        using (var connection = _context.CreateConnection())
        {
            string sql = $@"
            SELECT 
                c.Id,
                c.Title,
                c.Year
            FROM Content c
            WHERE c.Title LIKE '%{query}%'
        ";

            using (var command = new SQLiteCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int contentId = Convert.ToInt32(reader["Id"]);
                        string title = reader["Title"].ToString();
                        int year = Convert.ToInt32(reader["Year"]);

                        if (IsMovie(contentId))
                        {
                            // Получаем данные о фильме
                            Movie movie = GetMovieById(connection, contentId, title, year);
                            results.Add(movie);
                        }
                        else
                        {
                            // Получаем данные о сериале
                            TvSeries series = GetSeriesById(connection, contentId, title, year);
                            results.Add(series);
                        }
                    }
                }
            }
        }

        return results;
    }

    private bool IsMovie(int contentId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(1) FROM Movies WHERE ContentId = @id";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", contentId);
                long count = (long)command.ExecuteScalar();
                return count == 1;
            }
        }
    }
    private Movie GetMovieById(SQLiteConnection connection, int contentId, string title, int year)
    {
        string query = "SELECT Duration, Director FROM Movies WHERE ContentId = @id";
        using (var cmd = new SQLiteCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", contentId);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Movie
                    {
                        Id = contentId,
                        Title = title,
                        Year = year,
                        Duration = Convert.ToInt32(reader["Duration"]),
                        Director = reader["Director"].ToString()
                    };
                }
            }
        }
        return null;
    }
    private TvSeries GetSeriesById(SQLiteConnection connection, int contentId, string title, int year)
    {
        string query = "SELECT Seasons, Episodes FROM TvSeries WHERE ContentId = @id";
        using (var cmd = new SQLiteCommand(query, connection))
        {
            cmd.Parameters.AddWithValue("@id", contentId);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new TvSeries
                    {
                        Id = contentId,
                        Title = title,
                        Year = year,
                        Seasons = Convert.ToInt32(reader["Seasons"]),
                        Episodes = Convert.ToInt32(reader["Episodes"])
                    };
                }
            }
        }
        return null;
    }
}