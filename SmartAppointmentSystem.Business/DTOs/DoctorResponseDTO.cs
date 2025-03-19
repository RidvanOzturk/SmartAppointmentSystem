namespace SmartAppointmentSystem.Business.DTOs;

public record DoctorResponseDTO(
Guid Id,
string Name,
string Email,
string? Description,
string? Image,
int? BranchId,
DateTime CreatedAt,
BranchResponseDTO? Branch,
double AverageRating 
);
