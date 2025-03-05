namespace SmartAppointmentSystem.Api.Models;

public class GenerateTokenResponseModel
{
    public string Token { get; set; }
    public DateTime TokenExpireDate { get; set; }
}
