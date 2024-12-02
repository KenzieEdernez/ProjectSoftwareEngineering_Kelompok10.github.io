using System;

namespace TicketStoreAPI.Models.request;

public class SeatCreateDTO
{
    public int TheaterId { get; set; }
    public string SeatNumber { get; set; }
}
