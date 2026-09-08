namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public sealed record AppointmentSearchItemResponseDTO(
    Guid DoctorId,
    string DoctorName,
    TimeOnly Time
);