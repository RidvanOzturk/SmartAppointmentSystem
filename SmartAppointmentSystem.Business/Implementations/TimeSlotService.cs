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
        await context.TimeSlots.AddAsync(timeSlot, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<TimeSlot>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots
            .AsNoTracking()
            .Where(x=>x.DoctorId == id)
            .ToListAsync(cancellationToken);
        return timeSlot;
    }
    public async Task<List<TimeSlot>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots
            .AsNoTracking()
            .Where(ts => ts.DoctorId == id && ts.AvailableFrom < ts.AvailableTo)
            .ToListAsync(cancellationToken);
        return timeSlot;
    }
    public async Task<TimeSlot> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return timeSlot;
    }
    public async Task<List<TimeSlot>> GetAllTimeSlotsAsync(CancellationToken cancellationToken)
    {
        var timeSlots = await context.TimeSlots
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return timeSlots;
    }
    public async Task UpdateTimeSlotByIdAsync(Guid id, TimeSlotRequestDTO timeSlotRequestDTO, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        timeSlotRequestDTO.Map(timeSlot);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task DeleteTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var timeSlot = await context.TimeSlots.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.TimeSlots.Remove(timeSlot);
        await context.SaveChangesAsync(cancellationToken);
    }
}
