namespace SmartAppointmentSystem.Api.Models;

public class DoctorUserRequestModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Description { get; set; }
    public string? Image { get; set; }
    public int BranchId { get; set; }
}
