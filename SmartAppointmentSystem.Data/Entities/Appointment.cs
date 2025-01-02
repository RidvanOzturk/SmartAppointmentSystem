namespace SmartAppointmentSystem.Data.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }

    public User Professional { get; set; }
    public User Customer { get; set; }
}
