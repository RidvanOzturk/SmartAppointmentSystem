namespace SmartAppointmentSystem.Business.DTOs;

public record GenerateTokenRequestDTO
(
     Guid UserId,
     string Name,
     string Mail 

);
