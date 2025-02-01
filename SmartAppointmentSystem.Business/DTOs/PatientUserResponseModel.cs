namespace SmartAppointmentSystem.Business.DTOs;

public class PatientUserResponseModel
{
        public bool AuthenticateResult { get; set; }
        public string AuthToken { get; set; }
        public DateTime? AccessTokenExpireDate { get; set; }
}
