namespace SmartAppointmentSystem.Data.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public Guid CustomerId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public User Professional { get; set; }
    public User Customer { get; set; }
}
