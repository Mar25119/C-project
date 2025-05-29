public class PremiumUser : User
{
    public DateTime ExpirationDate { get; set; }

    public override void ShowProfile()
    {
        base.ShowProfile();
        Console.WriteLine($"Подписка активна до: {ExpirationDate.ToShortDateString()}");
    }
}