namespace SmartAppointmentSystem.Data.Entities;

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public Guid? PatientId { get; set; } 
    public Guid? DoctorId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }

    public virtual Patient Patient { get; set; }
    public virtual Doctor Doctor { get; set; }

}
