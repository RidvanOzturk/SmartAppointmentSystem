namespace SmartAppointmentSystem.Business.DTOs;

public record RatingRequestDTO
(
     Guid DoctorId,
     Guid PatientId,
     int Score,
     string Comment,
     DateTime CreatedAt 
);
