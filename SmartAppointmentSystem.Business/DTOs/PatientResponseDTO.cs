using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.DTOs;

public record PatientResponseDTO(
    Guid Id,
    string Name,
    string Email,
    ICollection<Appointment> Appointments,
    ICollection<Rating> Ratings
    );
