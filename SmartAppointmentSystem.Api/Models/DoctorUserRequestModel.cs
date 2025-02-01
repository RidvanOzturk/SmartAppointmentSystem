namespace SmartAppointmentSystem.Api.Models;

public class DoctorUserRequestModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public string Polyclinic { get; set; }
}
