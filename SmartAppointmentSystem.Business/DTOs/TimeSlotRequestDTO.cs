namespace SmartAppointmentSystem.Business.DTOs;

public record TimeSlotRequestDTO
(
     Guid DoctorId,
     int AvailableDay,
     int AppointmentFrequency,
     TimeSpan AvailableFrom,
     TimeSpan AvailableTo
);
