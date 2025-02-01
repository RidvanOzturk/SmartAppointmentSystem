namespace SmartAppointmentSystem.Data.Entities;

public class Process
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public Guid DoctorId { get; set; }

    public Doctor Doctor { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; set; }
}
