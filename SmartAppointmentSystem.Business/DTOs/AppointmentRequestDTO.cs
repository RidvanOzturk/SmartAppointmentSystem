namespace SmartAppointmentSystem.Business.DTOs;

public class AppointmentRequestDTO
{
    public Guid Id { get; set; }
    public Guid ProfessionalId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}
