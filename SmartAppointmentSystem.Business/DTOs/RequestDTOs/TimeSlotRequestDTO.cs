namespace SmartAppointmentSystem.Business.DTOs.RequestDTOs;

public record TimeSlotRequestDTO
(
     Guid DoctorId,
     int AvailableDay,
     int AppointmentFrequency,
     TimeSpan AvailableFrom,
     TimeSpan AvailableTo
);
