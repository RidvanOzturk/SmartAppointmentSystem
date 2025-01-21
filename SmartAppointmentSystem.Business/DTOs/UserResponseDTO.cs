namespace SmartAppointmentSystem.Business.DTOs;

public class UserResponseDTO
{
        public bool AuthenticateResult { get; set; }
        public string AuthToken { get; set; }
        public DateTime? AccessTokenExpireDate { get; set; }
}
