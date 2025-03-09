namespace SmartAppointmentSystem.Business.DTOs;

public record AppointmentRequestDTO
(
     Guid DoctorId,
     Guid PatientId,
     DateTime Time,
     string Status,
     string Notes
);