namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record RatingResponseDTO
(
    Guid DoctorId,
     Guid PatientId,
     int Score,
     string Comment
);