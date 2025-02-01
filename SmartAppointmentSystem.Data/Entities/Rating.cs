namespace SmartAppointmentSystem.Data.Entities;

public class Rating
{
    public Guid Id { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public int Score { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }

    public Doctor Doctor { get; set; }
    public Patient Patient { get; set; }
}