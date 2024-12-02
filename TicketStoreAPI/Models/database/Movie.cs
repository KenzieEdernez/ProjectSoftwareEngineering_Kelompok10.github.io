using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketStoreAPI.Models;

public class Movie
{
    [Key]
    [Column("movies_id")]
    public int MoviesId { get; set; }
    public string Title { get; set; }
    public string Genre { get; set; }
    public int Duration { get; set; }
    public decimal Rating { get; set; }
    public string Description { get; set; }
    [Column("poster_url")]
    public string PosterUrl { get; set; }
    [Column("release_date")]
    public DateTime? ReleaseDate { get; set; }
}