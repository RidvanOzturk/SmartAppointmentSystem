namespace SmartAppointmentSystem.Business.DTOs;

public class AppointmentRequestDTO
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public DateTime Time { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}
