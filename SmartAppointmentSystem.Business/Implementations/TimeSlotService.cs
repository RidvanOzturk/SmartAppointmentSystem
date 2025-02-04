using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class TimeSlotService(AppointmentContext context) : ITimeSlotService
{
    public async Task<bool> CreateTimeSlot(TimeSlotRequestDTO timeSlotRequestDTO)
    {
        if (timeSlotRequestDTO == null)
        {
            return false;
        }
        var tsEntity = timeSlotRequestDTO.Map();
        var addTimeSlot = await context.TimeSlots.AddAsync(tsEntity);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<List<TimeSlot>> GetDoctorTimeSlots(Guid id)
    {
        var getDoctorTS = await context.TimeSlots
            .Where(x=>x.DoctorId == id)
            .ToListAsync();
        return getDoctorTS;
    }
    public async Task<List<TimeSlot>> AvailableTimeSlotDoctor(Guid id)
    {
        var availableTimeSlots = await context.TimeSlots
            .Where(ts => ts.DoctorId == id && !context.Appointments
            .Any(a => a.TimeSlotId == ts.Id))
            .ToListAsync();
        return availableTimeSlots;
    }
    public async Task<TimeSlot> GetTimeSlotById(Guid id)
    {
        var getTimeSlot = await context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return getTimeSlot;
    }
    public async Task<List<TimeSlot>> GetAllTimeSlots()
    {
        var getAll = await context.TimeSlots
            .AsNoTracking()
            .ToListAsync();
        return getAll;
    }
    public async Task<bool> UpdateTimeSlotById(Guid id, TimeSlotRequestDTO timeSlotRequestDTO)
    {
        var TimeSlotId = await context.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        timeSlotRequestDTO.Map(TimeSlotId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> DeleteTimeSlotById(Guid id)
    {
        var deletedId = await context.TimeSlots.FirstOrDefaultAsync(x => x.Id == id);
        context.TimeSlots.Remove(deletedId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
