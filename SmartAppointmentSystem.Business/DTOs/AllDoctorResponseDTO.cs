namespace SmartAppointmentSystem.Business.DTOs;

public record AllDoctorResponseDTO(
    Guid Id,
    string Name,
    string Email,
    int? BranchId,
    BranchResponseDTO Branch,
    double AverageRating  
);
