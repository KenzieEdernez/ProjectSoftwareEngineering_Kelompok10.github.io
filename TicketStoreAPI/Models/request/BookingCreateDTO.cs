using System;

namespace TicketStoreAPI.Models.request;

public class BookingCreateDTO
{
    public int ScheduleId { get; set; }
    public int UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public byte BookingStatus { get; set; }
}
