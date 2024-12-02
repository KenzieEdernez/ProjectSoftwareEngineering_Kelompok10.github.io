using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class BookingResponse
{
    public int BookingId { get; set; }
    public int ScheduleId { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public byte BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; }
    public Schedule Schedule { get; set; }
}
