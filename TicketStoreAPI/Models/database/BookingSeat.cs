using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class BookingSeat
{
    [Key]
    [Column("booking_seats_id")]
    public int BookingSeatId { get; set; }
    [Column("booking_id")]
    public int BookingId { get; set; }
    [Column("seat_id")]
    public int SeatId { get; set; }

    [JsonIgnore]
    public Booking Booking { get; set; }

    [JsonIgnore]
    public Seat Seat { get; set; }
}
