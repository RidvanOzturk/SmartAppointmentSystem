using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITimeSlotService
{
    Task CreateTimeSlotAsync(TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken = default);
    Task<TimeSlotResponseDTO?> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlotResponseDTO?>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlotResponseDTO?>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TimeSlotResponseDTO?>> GetAllTimeSlotsAsync(CancellationToken cancellationToken = default);
    Task UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken = default);
    Task DeleteTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsTimeSlotExistAsync(Guid id, CancellationToken cancellationToken = default);
}
