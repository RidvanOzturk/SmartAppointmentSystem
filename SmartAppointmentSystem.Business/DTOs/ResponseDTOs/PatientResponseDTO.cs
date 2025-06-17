using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record PatientResponseDTO(
    string Name,
    string Email,
    ICollection<Appointment> Appointments,
    ICollection<Rating> Ratings
    );
