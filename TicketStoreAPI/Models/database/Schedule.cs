using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Schedule
{
    [Key]
    public int ScheduleId { get; set; }

    [Required]
    public int MovieId { get; set; }

    [Required]
    public int TheaterId { get; set; }

    [Required]
    public DateTime ShowTime { get; set; }

    [Required]
    public decimal Price { get; set; }

    [ForeignKey("MovieId")]
    public Movie Movie { get; set; }

    [ForeignKey("TheaterId")]
    public Theater Theater { get; set; }

    public ICollection<Booking> Bookings { get; set; }
}
