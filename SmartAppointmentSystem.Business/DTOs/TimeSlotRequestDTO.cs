namespace SmartAppointmentSystem.Business.DTOs;

public class TimeSlotRequestDTO
{
    public Guid ProfessionalId { get; set; }
    public Guid ProcessId { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }
}
