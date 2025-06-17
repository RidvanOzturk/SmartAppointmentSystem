namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

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
