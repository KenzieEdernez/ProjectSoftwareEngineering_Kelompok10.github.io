using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
namespace TicketStoreAPI.Models;

public class SeatResponse
{
    public int SeatsId { get; set; }
    public int TheaterId { get; set; }
    public string SeatNumber { get; set; }
}