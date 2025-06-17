namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record AllDoctorResponseDTO(
    Guid Id,
    string Name,
    string Email,
    int? BranchId,
    BranchResponseDTO Branch,
    double AverageRating  
);
