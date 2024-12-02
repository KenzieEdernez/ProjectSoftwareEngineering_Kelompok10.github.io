namespace TicketStoreAPI.Models;

public class ScheduleResponse
{
    public int SchedulesId { get; set; }
    public int MovieId { get; set; }
    public int TheaterId { get; set; }
    public DateTime ShowTime { get; set; }
    public decimal Price { get; set; }
}
