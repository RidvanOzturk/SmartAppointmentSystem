using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class TimeSlotService(AppointmentContext appointmentContext) : ITimeSlotService
{
    public async Task<bool> CreateTimeSlot(TimeSlotRequestDTO timeSlotRequestDTO)
    {
        if (timeSlotRequestDTO == null)
        {
            return false;
        }
        var mappingTarget = timeSlotRequestDTO.Map();
        var addTimeSlot = await appointmentContext.TimeSlots.AddAsync(mappingTarget);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<TimeSlot> GetTimeSlotById(Guid id)
    {
        var getTimeSlot = await appointmentContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        return getTimeSlot;
    }
    public async Task<List<TimeSlot>> GetAllTimeSlots()
    {
        var getAll = await appointmentContext.TimeSlots.ToListAsync();
        return getAll;
    }
    public async Task<bool> UpdateTimeSlotById(Guid id, TimeSlotRequestDTO timeSlotRequestDTO)
    {
        var TimeSlotId = await appointmentContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        timeSlotRequestDTO.Map(TimeSlotId);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteTimeSlotById(Guid id)
    {
        var deletedId = await appointmentContext.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        appointmentContext.TimeSlots.Remove(deletedId);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
}
