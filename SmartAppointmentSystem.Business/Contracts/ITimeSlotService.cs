using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface ITimeSlotService
{
    Task<bool> CreateTimeSlot(TimeSlotRequestDTO timeSlotRequestDTO);
    Task<TimeSlot> GetTimeSlotById(Guid id);
    Task<List<TimeSlot>> GetAllTimeSlots();
    Task<bool> UpdateTimeSlotById(Guid id, TimeSlotRequestDTO timeSlotRequestDTO);
    Task<bool> DeleteTimeSlotById(Guid id);
}
