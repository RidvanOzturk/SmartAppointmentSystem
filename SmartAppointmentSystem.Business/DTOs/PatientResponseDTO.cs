namespace SmartAppointmentSystem.Business.DTOs;

public record PatientResponseDTO(
     Guid Id,
string Name,
string Email,
string? Appointment,
int? Ratings,
DateTime CreatedAt
    );
