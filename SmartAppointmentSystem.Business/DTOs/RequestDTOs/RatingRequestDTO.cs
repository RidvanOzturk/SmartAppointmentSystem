namespace SmartAppointmentSystem.Business.DTOs.RequestDTOs;

public record RatingRequestDTO
(
     Guid DoctorId,
     Guid PatientId,
     int Score,
     string Comment,
     DateTime CreatedAt 
);
