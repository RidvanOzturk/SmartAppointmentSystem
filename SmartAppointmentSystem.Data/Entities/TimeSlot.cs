namespace SmartAppointmentSystem.Data.Entities;

public class TimeSlot
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public TimeSpan AvailableFrom { get; set; }
    public TimeSpan AvailableTo { get; set; }
    public int AvailableDay { get; set; }
    public int AppointmentFrequency { get; set; }
    public Doctor Doctor { get; set; }
}
