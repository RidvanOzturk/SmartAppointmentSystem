namespace SmartAppointmentSystem.Business.DTOs.ResponseDTOs;

public record TimeSlotResponseDTO
(    
     Guid Id,
     Guid DoctorId,
     int AvailableDay,
     int AppointmentFrequency,
     TimeSpan AvailableFrom,
     TimeSpan AvailableTo
);
