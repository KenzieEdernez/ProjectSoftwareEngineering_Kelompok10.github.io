using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Seat
{
    [Key]
    public int SeatId { get; set; }

    [Required]
    public int TheaterId { get; set; }

    [Required]
    public string SeatNumber { get; set; }

    [ForeignKey("TheaterId")]
    public Theater Theater { get; set; }

    public ICollection<BookingSeat> BookingSeats { get; set; }
}
