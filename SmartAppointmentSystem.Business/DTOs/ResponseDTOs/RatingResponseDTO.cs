namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record RatingResponseDTO
(
     Guid Id,
     Guid DoctorId,
     Guid PatientId,
     int Score,
     string Comment
);