namespace SmartAppointmentSystem.Data.Entities;

public class TimeSlot
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }

    public Service Service { get; set; }
}
