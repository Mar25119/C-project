using System.Data.SQLite;

class DbContext
{
    private string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SQLiteConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        try
        {
            connection.Open();
            return connection;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения к БД: {ex.Message}");
            return null;
        }
    }
}