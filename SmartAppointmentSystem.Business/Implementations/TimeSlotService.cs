using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
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
    public async Task<List<TimeSlotResponseDTO>> GetDoctorTimeSlotsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Where(x => x.DoctorId == id)
            .Select(x => new TimeSlotResponseDTO
            (
                x.Id,
                x.DoctorId, 
                x.AvailableDay, 
                x.AppointmentFrequency, 
                x.AvailableFrom, 
                x.AvailableTo))
            .ToListAsync(cancellationToken);
    }
    public async Task<List<TimeSlotResponseDTO>> AvailableTimeSlotDoctorAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Where(x => x.DoctorId == id && x.AvailableFrom < x.AvailableTo)
            .Select(x => new TimeSlotResponseDTO(
                x.Id,
                x.DoctorId,
                x.AvailableDay,
                x.AppointmentFrequency,
                x.AvailableFrom,
                x.AvailableTo))
            .ToListAsync(cancellationToken);
    }
    public async Task<TimeSlotResponseDTO?> GetTimeSlotByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new TimeSlotResponseDTO(
                x.Id,
                x.DoctorId,
                x.AvailableDay,
                x.AppointmentFrequency,
                x.AvailableFrom,
                x.AvailableTo
       ))
       .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<List<TimeSlotResponseDTO>> GetAllTimeSlotsAsync(CancellationToken cancellationToken)
    {
        return await context.TimeSlots
            .AsNoTracking()
            .Select(x => new TimeSlotResponseDTO(
                x.Id,
                x.DoctorId,
                x.AvailableDay,
                x.AppointmentFrequency,
                x.AvailableFrom,
                x.AvailableTo
       ))
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
