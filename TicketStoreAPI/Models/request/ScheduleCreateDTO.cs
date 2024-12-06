namespace TicketStoreAPI.Models.request;

public class ScheduleCreateDTO
{
    public int MovieId { get; set; }
    public int TheaterId { get; set; }
    public DateTime ShowTime { get; set; }
    public decimal Price { get; set; }
}
