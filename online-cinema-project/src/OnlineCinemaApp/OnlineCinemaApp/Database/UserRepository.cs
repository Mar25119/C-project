using System;
using System.Data.SQLite;

class UserRepository
{
    private DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }

    public void Register(string name, string email, string passwordHash)
    {
        using (var connection = _context.CreateConnection())
        {
            if (connection == null) return;

            string query = "INSERT INTO Users (Name, Email, PasswordHash) VALUES (@name, @email, @password)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", passwordHash);

                try
                {
                    command.ExecuteNonQuery();
                    Console.WriteLine("Регистрация прошла успешно!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка регистрации: {ex.Message}");
                }
            }
        }
    }

    public bool Login(string email, string password)
    {
        using (var connection = _context.CreateConnection())
        {
            if (connection == null) return false;

            string query = "SELECT COUNT(1) FROM Users WHERE Email = @email AND PasswordHash = @password";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                try
                {
                    long count = (long)command.ExecuteScalar();
                    return count == 1;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка входа: {ex.Message}");
                    return false;
                }
            }
        }
    }
}