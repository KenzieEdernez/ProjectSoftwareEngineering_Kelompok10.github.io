using System.ComponentModel.DataAnnotations;

public class Movie
{
    [Key]
    public int MovieId { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Genre { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    [Range(0, 9.99, ErrorMessage = "Rating must be between 0 and 9.99")]
    public decimal Rating { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string PosterUrl { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public ICollection<Schedule> Schedules { get; set; }
}
