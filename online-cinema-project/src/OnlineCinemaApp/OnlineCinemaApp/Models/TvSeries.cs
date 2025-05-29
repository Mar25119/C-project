public class TvSeries : Content
{
    public int Seasons { get; set; }
    public int Episodes { get; set; }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Сериал: {Title} ({Year}), {Seasons} сезонов, {Episodes} эпизодов");
    }
}