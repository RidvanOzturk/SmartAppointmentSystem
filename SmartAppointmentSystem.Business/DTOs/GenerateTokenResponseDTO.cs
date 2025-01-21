namespace SmartAppointmentSystem.Business.DTOs;

public class GenerateTokenResponseDTO
{
    public string Token { get; set; }
    public DateTime TokenExpireDate { get; set; }
}
