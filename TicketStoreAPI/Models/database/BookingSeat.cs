using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class BookingSeat
{
    [Key]
    public int BookingSeatId { get; set; }

    [Required]
    public int BookingId { get; set; }

    [Required]
    public int SeatId { get; set; }

    [ForeignKey("BookingId")]
    public Booking Booking { get; set; }

    [ForeignKey("SeatId")]
    public Seat Seat { get; set; }
}
