namespace SmartAppointmentSystem.Business.DTOs.RequestDTOs;

public record AppointmentRequestDTO
(
     Guid DoctorId,
     Guid PatientId,
     DateTime Time,
     string Status,
     string Notes
);