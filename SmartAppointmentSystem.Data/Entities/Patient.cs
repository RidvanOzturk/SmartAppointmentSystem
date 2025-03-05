namespace SmartAppointmentSystem.Data.Entities;

public class Patient
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}
