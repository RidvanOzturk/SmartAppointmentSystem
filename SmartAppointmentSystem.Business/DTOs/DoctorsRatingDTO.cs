namespace SmartAppointmentSystem.Business.DTOs;

public record DoctorsRatingDTO
(
     Guid Id,
     string Name,
     string Email,
     string Description,
     int BranchId,
     double AverageRating
);
