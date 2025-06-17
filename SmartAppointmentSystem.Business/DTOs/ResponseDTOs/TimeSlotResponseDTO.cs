namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record TimeSlotResponseDTO
(    Guid DoctorId,
     int AvailableDay,
     int AppointmentFrequency,
     TimeSpan AvailableFrom,
     TimeSpan AvailableTo
);
