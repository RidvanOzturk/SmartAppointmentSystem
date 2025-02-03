namespace SmartAppointmentSystem.Data.Entities;

public class Appointment
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }  
    public Guid PatientId { get; set; }
    public Guid TimeSlotId { get; set; }
    public string Status { get; set; }
    public string Notes { get; set; }

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
    public TimeSlot TimeSlot { get; set; }
}