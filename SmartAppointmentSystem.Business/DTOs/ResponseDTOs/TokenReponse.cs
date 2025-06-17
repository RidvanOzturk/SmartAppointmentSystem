namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public class TokenReponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpireDate { get; set; }
}
