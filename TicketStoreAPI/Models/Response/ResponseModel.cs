using System;

namespace TicketStoreAPI.Models.Response;

public class ResponseModel<T>
{
    public int StatusCode { get; set; }
    public string RequestMethod { get; set; }
    public T Data { get; set; }
}
