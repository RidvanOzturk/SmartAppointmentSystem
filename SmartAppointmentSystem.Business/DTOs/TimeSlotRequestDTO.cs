namespace SmartAppointmentSystem.Business.DTOs;

public class TimeSlotRequestDTO
{
    public Guid DoctorId { get; set; }
    public Guid ProcessId { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }
}
