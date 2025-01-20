namespace SmartAppointmentSystem.Api.Models;

public class UserResponseModel
{
    public bool AuthenticateResult { get; set; }
    public string AuthToken { get; set; }
    public DateTime AccessTokenExpireDate { get; set; }
}
