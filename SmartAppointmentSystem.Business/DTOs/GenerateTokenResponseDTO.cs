namespace SmartAppointmentSystem.Business.DTOs;

public record GenerateTokenResponseDTO
(
     string Token,
     DateTime TokenExpireDate
);
