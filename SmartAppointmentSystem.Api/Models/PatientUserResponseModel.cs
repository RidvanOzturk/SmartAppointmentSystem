namespace SmartAppointmentSystem.Api.Models;

public class PatientUserResponseModel
{
    public bool AuthenticateResult { get; set; }
    public string AuthToken { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
}
