using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class Theater
{
    [Key]
    [Column("theaters_id")]
    public int TheatersId { get; set; }
    [Column("theaters_name")]
    public string TheatersName { get; set; }
    public int Capacity { get; set; }

    [JsonIgnore]
    public ICollection<Schedule> Schedules { get; set; }

    [JsonIgnore]
    public ICollection<Seat> Seats { get; set; }
}

