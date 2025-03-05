using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITimeSlotService
{
    Task CreateTimeSlotAsync(TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken = default);
    Task<TimeSlot> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlot>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlot>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlot>> GetAllTimeSlotsAsync(CancellationToken cancellationToken = default);
    Task UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken = default);
    Task DeleteTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsTimeSlotExistAsync(Guid id, CancellationToken cancellationToken = default);
}
