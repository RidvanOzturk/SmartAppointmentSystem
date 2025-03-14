namespace SmartAppointmentSystem.Data.Entities;

public class Doctor
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public int? BranchId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Branch Branch { get; set; }         

    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<Rating> Ratings { get; set; }
    public ICollection<TimeSlot> TimeSlots { get; set; }
}
