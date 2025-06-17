namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record DoctorResponseDTO
(
    Guid Id,
    string Name,
    string Email,
    string? Description,
    string? Image,
    DateTime CreatedAt,
    BranchResponseDTO? Branch,
    double AverageRating
);
