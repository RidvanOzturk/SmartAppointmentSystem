namespace SmartAppointmentSystem.Business.DTOs;

public class ProcessRequestDTO
{
    public string Name { get; set; }
    public int Duration { get; set; }
    public Guid ProfessionalId { get; set; }
}
