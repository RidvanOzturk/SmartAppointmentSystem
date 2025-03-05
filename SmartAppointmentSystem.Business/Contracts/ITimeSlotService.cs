using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITimeSlotService
{
    Task<bool> CreateTimeSlotAsync(TimeSlotRequestDTO timeSlotRequestDTO);
    Task<TimeSlot> GetTimeSlotByIdAsync(Guid id);
    Task<List<TimeSlot>> AvailableTimeSlotDoctorAsync(Guid id);
    Task<List<TimeSlot>> GetDoctorTimeSlotsAsync(Guid id);
    Task<List<TimeSlot>> GetAllTimeSlotsAsync();
    Task<bool> UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO);
    Task<bool> DeleteTimeSlotByIdAsync(Guid id);
}
