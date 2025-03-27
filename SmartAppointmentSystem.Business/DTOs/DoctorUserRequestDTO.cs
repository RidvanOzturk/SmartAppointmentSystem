namespace SmartAppointmentSystem.Business.DTOs;

public record DoctorUserRequestDTO
(
     string? Name,
     string Email,
     string? Password,
     string? Description,
     string? Image,
     int BranchId
);
