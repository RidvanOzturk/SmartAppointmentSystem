namespace SmartAppointmentSystem.Business.DTOs.RequestDTOs;

public record PatientUserRequestDTO
(
     string? Name,
     string? Email,
     string? Password
);
