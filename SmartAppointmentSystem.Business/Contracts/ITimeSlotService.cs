using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITimeSlotService
{
    Task CreateTimeSlotAsync(TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken);
    Task<TimeSlot> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TimeSlot>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TimeSlot>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken);
    Task<List<TimeSlot>> GetAllTimeSlotsAsync(CancellationToken cancellationToken);
    Task UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken);
    Task DeleteTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken);
}
