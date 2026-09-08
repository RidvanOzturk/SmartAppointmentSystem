namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public sealed record AppointmentSearchResponseDTO(
    string Branch,
    DateOnly Date,
    List<AppointmentSearchItemResponseDTO> Appointments
);