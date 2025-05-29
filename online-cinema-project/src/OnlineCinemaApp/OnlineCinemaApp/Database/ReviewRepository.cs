using System.Data.SQLite;

public class ReviewRepository
{
    private DbContext _context;

    public ReviewRepository(DbContext context)
    {
        _context = context;
    }

    public void AddReview(int userId, int contentId, string text, int rating)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "INSERT INTO Reviews (UserId, ContentId, Text, Rating) VALUES (@userId, @contentId, @text, @rating)";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@contentId", contentId);
                command.Parameters.AddWithValue("@text", text);
                command.Parameters.AddWithValue("@rating", rating);

                command.ExecuteNonQuery();
            }
        }
    }

    public List<Review> GetReviewsByContent(int contentId)
    {
        var reviews = new List<Review>();
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT Id, UserId, ContentId, Text, Rating FROM Reviews WHERE ContentId = @contentId";
            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@contentId", contentId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new Review
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            UserId = Convert.ToInt32(reader["UserId"]),
                            ContentId = Convert.ToInt32(reader["ContentId"]),
                            Text = reader["Text"].ToString(),
                            Rating = Convert.ToInt32(reader["Rating"])
                        });
                    }
                }
            }
        }
        return reviews;
    }
}