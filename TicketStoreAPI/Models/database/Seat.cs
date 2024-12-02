using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class Seat
{
    [Key]
    [Column("seats_id")]
    public int SeatId { get; set; }
    [Column("theater_id")]
    public int TheaterId { get; set; }
    [Column("seat_number")]
    public string SeatNumber { get; set; }

    [JsonIgnore]
    public Theater Theater { get; set; }
    public ICollection<BookingSeat> BookingSeats { get; set; }
}