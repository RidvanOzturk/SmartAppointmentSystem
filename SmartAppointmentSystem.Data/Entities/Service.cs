namespace SmartAppointmentSystem.Data.Entities;

public  class Service
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public Guid ProfessionalId { get; set; }

    public User Professional { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; set; }
}
