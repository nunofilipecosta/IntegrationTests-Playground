namespace SimpleWebApi.Model;

public class Movie
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Director { get; set; } = string.Empty;
    
    public DateTime ReleaseDate { get; set; }

    public string Genre { get; set; } = string.Empty;

    public decimal Price { get; set; } = 0.0m;
}
