public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ContentId { get; set; }
    public string Text { get; set; }
    public int Rating { get; set; } // от 1 до 10
}