public class Subscription
{
    public int UserId { get; set; }
    public string Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}