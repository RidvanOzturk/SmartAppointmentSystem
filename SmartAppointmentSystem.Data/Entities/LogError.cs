namespace SmartAppointmentSystem.Data.Entities;

public class LogError
{
    public Guid Id { get; set; }
    public string ExceptionMessage { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
