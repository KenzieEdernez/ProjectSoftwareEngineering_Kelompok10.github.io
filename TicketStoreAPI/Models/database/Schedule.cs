using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class Schedule
{
    [Key]
    [Column("schedules_id")]
    public int SchedulesId { get; set; }
    [Column("movie_id")]
    public int MovieId { get; set; }
    [Column("theater_id")]
    public int TheaterId { get; set; }
    [Column("show_time")]
    public DateTime ShowTime { get; set; }
    public decimal Price { get; set; }

    [JsonIgnore]
    public Movie Movie { get; set; }

    [JsonIgnore]
    public Theater Theater { get; set; }

    [JsonIgnore]
    public ICollection<Booking> Bookings { get; set; }
}
