namespace SmartAppointmentSystem.Api.Models;

public class AppointmentRequestModel
{
    public Guid ProfessionalId { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime DateTime { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}
