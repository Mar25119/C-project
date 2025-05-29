public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public virtual void ShowProfile()
    {
        Console.WriteLine($"Имя: {Name}, Email: {Email}");
    }
}