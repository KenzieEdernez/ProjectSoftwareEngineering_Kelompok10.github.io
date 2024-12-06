namespace TicketStoreAPI.Models;

public class MovieResponse
{
    public int MoviesId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public decimal Rating { get; set; }
    public string Description { get; set; }
    public string PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
}