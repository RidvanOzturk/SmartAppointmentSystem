namespace SmartAppointmentSystem.Api.Models;

public class ProcessRequestModel
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public Guid DoctorId { get; set; }
}
