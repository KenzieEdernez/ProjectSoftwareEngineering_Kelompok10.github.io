// Models/Movie.cs
using System;
using System.Collections.Generic;
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
    public decimal Rating { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string PosterUrl { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public ICollection<Schedule> Schedules { get; set; }
}
