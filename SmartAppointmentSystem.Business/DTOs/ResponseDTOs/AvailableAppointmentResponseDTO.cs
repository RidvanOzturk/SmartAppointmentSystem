namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public sealed record AvailableAppointmentResponseDTO(
    Guid DoctorId,
    string DoctorName,
    string Branch,
    DateOnly Date,
    TimeOnly Time
);