using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class TimeSlotService(AppointmentContext context) : ITimeSlotService
{
    public async Task CreateTimeSlotAsync(TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken)
    {
        var timeSlot = timeSlotRequestDTO.Map();
        await context.TimeSlots
            .AddAsync(timeSlot, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<TimeSlot>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Where(x=>x.DoctorId == id)
            .ToListAsync(cancellationToken);
    }
    public async Task<List<TimeSlot>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.DoctorId == id && ts.AvailableFrom < ts.AvailableTo)
            .ToListAsync(cancellationToken);
    }
    public async Task<TimeSlot> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task<List<TimeSlot>> GetAllTimeSlotsAsync(CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        timeSlotRequestDTO.Map(timeSlot);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.TimeSlots.Remove(timeSlot);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> IsTimeSlotExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context
            .TimeSlots
            .AnyAsync(x => x.Id == id, cancellationToken);
    }
}
