namespace SmartAppointmentSystem.Business.DTOs;

public class RatingRequestDTO
{
    public Guid ProfessionalId { get; set; }
    public Guid CustomerId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}
