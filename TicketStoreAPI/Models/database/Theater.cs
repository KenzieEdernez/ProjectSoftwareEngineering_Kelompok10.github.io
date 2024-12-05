// Models/Theater.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Theater
{
    [Key]
    public int TheaterId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Capacity { get; set; }

    public ICollection<Seat> Seats { get; set; }
    public ICollection<Schedule> Schedules { get; set; }
}
