public abstract class Content
{
    public int Id { get; set; }
    private string title;
    public int Year { get; set; }
    public string Description { get; set; }

    public string Title
    {
        get => title;
        set => title = value ?? throw new ArgumentNullException(nameof(value));
    }

    public abstract void DisplayInfo();
}