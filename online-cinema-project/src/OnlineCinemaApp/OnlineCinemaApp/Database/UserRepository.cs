using System.Data.SQLite;

public class UserRepository
{
    private readonly DbContext _context;

    public UserRepository(DbContext context)
    {
        _context = context;
    }

    public void Register(string name, string email, string passwordHash)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "INSERT INTO Users (Name, Email, PasswordHash) VALUES (@name, @email, @password)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", passwordHash);

                command.ExecuteNonQuery();
            }
        }
    }

    public bool Login(string email, string password)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Email = @email AND PasswordHash = @password";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                long count = (long)command.ExecuteScalar();
                return count == 1;
            }
        }
    }

    public User GetUserByEmail(string email)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT Id, Name, Email FROM Users WHERE Email = @email";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@email", email);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString()
                        };
                    }
                }
            }
        }
        return null;
    }
}