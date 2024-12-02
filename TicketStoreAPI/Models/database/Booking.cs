using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class Booking
{
    [Key]
    [Column("bookings_id")]
    public int BookingId { get; set; }

    [Column("schedule_id")]
    public int ScheduleId { get; set; }

    [Column("users_id")]
    public int UserId { get; set; }

    [Column("total_price")]
    public decimal TotalPrice { get; set; }

    [Column("bookings_status")]
    public byte BookingStatus { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public Schedule Schedule { get; set; }

    [JsonIgnore]
    public User User { get; set; }

    [JsonIgnore]
    public ICollection<BookingSeat> BookingSeats { get; set; }
}
