namespace TicketStoreAPI.Models;

public class BookingResponse
{
    public int BookingId { get; set; }
    public int ScheduleId { get; set; }
    public string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public byte BookingStatus { get; set; }
    public DateTime CreatedAt { get; set; }
}
