// Models/Booking.cs
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Booking
{
    [Key]
    public int BookingId { get; set; }

    [Required]
    public int ScheduleId { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public decimal TotalPrice { get; set; }

    [Required]
    public byte BookingStatus { get; set; } 

    [Required]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("ScheduleId")]
    public Schedule Schedule { get; set; }

    [ForeignKey("UserId")]
    public ApplicationUser User { get; set; }

    public ICollection<BookingSeat> BookingSeats { get; set; }
}
