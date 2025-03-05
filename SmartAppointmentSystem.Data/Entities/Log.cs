namespace SmartAppointmentSystem.Data.Entities;

public class Log
{
    public Guid Id { get; set; }
    public string Request { get; set; }
    public string Headers { get; set; }
    public string Endpoint { get; set; }
    public string HttpMethod { get; set; }
    public string Response { get; set; }
    public string Ip { get; set; }
    public DateTime CreatedAt { get; set; }
}
