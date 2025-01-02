namespace SmartAppointmentSystem.Data.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Mail { get; set; }
    public string PasswordHash { get; set; }
    public string Role { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
}
