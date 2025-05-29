using System.Data.SQLite;

public class SubscriptionRepository
{
    private DbContext _context;

    public SubscriptionRepository(DbContext context)
    {
        _context = context;
    }

    public void Subscribe(int userId, string type)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "INSERT OR REPLACE INTO Subscriptions (UserId, Type, StartDate, EndDate) VALUES (@userId, @type, datetime('now'), datetime('now', '+1 month'))";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@type", type);

                command.ExecuteNonQuery();
            }
        }
    }

    public bool IsPremium(int userId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(1) FROM Subscriptions WHERE UserId = @userId AND EndDate > datetime('now')";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                long count = (long)command.ExecuteScalar();
                return count == 1;
            }
        }
    }
}