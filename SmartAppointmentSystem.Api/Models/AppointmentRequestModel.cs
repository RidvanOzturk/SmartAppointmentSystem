namespace SmartAppointmentSystem.Api.Models;

public class AppointmentRequestModel
{
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public Guid TimeSlotId { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }
}
