namespace SmartAppointmentSystem.Business.DTOs;

public record UserResponseModel
(
         bool AuthenticateResult,
         string AuthToken,
         DateTime? AccessTokenExpireDate
);
