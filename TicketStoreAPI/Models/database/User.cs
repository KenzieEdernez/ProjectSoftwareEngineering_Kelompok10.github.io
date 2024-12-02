using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TicketStoreAPI.Models;

public class User
{
    [Key]
    [Column("users_id")]
    public int UsersId { get; set; }
    [Column("first_name")]
    public string FirstName { get; set; }
    [Column("last_name")]
    public string LastName { get; set; }
    public string Email { get; set; }
    [Column("passwords")]
    public string Password { get; set; }
    public string Phone { get; set; }
    public ICollection<Booking> Bookings { get; set; }
}