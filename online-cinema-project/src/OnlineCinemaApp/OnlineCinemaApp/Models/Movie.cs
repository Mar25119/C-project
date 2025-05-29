public class Movie : Content
{
    public int Duration { get; set; }
    public string Director { get; set; }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Фильм: {Title} ({Year}), реж. {Director}, {Duration} мин.");
    }
}