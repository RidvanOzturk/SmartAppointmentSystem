using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(AppointmentContext context) : IAppointmentService
{
    public async Task CreateAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO, CancellationToken cancellationToken)
    {
        var appointmentEntity = appointmentRequestDTO.Map();
        await context.Appointments
            .AddAsync(appointmentEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken)
    {
        return await context.Appointments
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
    public async Task<List<Appointment>> GetUserAppointmentsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Appointments
            .AsNoTracking()
            .Where(x => x.PatientId == id)
            .ToListAsync(cancellationToken);
    }
    public async Task<Appointment?> GetAppointmentsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    public async Task DeleteAppointmentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var deletedAppointment = await context.Appointments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        context.Appointments
            .Remove(deletedAppointment);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<string>> GetAvailableTimeSlotsForDoctorAsync(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        int dayOfWeek = (int)date.DayOfWeek;
        var timeSlot = await context.TimeSlots
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DoctorId == id && x.AvailableDay == dayOfWeek, cancellationToken);
        if (timeSlot == null)
        {
            return new List<string>();
        }
        var availableSlots = new List<string>();
        var currentSlot = timeSlot.AvailableFrom; 
        TimeSpan frequency = TimeSpan.FromMinutes(timeSlot.AppointmentFrequency);
        while (currentSlot + frequency <= timeSlot.AvailableTo)
        {
            availableSlots.Add(currentSlot.ToString(@"hh\:mm"));
            currentSlot = currentSlot.Add(frequency);
        }
        return availableSlots;
    }

    public async Task UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO, CancellationToken cancellationToken)
    {
        var updatedAppointment = await context.Appointments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        appointmentRequestDTO.Map(updatedAppointment);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> IsAppointmentExistAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Appointments
            .AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<List<AvailableAppointmentResponseDTO>> GetAvailableAppointmentsAsync(
    string branch,
    DateOnly date,
    TimeOnly? afterTime,
    CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(branch);

        int dayOfWeek = (int)date.DayOfWeek;

        var timeSlots = await context.TimeSlots
            .AsNoTracking()
            .Where(x =>
                x.AvailableDay == dayOfWeek &&
                x.Doctor.Branch != null &&
                EF.Functions.ILike(x.Doctor.Branch.Title, $"%{branch}%"))
            .Select(x => new
            {
                x.DoctorId,
                DoctorName = x.Doctor.Name,
                Branch = x.Doctor.Branch.Title,
                x.AvailableFrom,
                x.AvailableTo,
                x.AppointmentFrequency
            })
            .ToListAsync(cancellationToken);

        if (timeSlots.Count == 0)
            return [];

        var doctorIds = timeSlots
            .Select(x => x.DoctorId)
            .Distinct()
            .ToList();

        DateTime startOfDay = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        DateTime endOfDay = startOfDay.AddDays(1);

        var occupiedAppointments = await context.Appointments
            .AsNoTracking()
            .Where(x =>
                doctorIds.Contains(x.DoctorId) &&
                x.Time >= startOfDay &&
                x.Time < endOfDay)
            .Select(x => new
            {
                x.DoctorId,
                x.Time
            })
            .ToListAsync(cancellationToken);

        var result = new List<AvailableAppointmentResponseDTO>();

        foreach (var timeSlot in timeSlots)
        {
            TimeSpan currentTime = timeSlot.AvailableFrom;
            TimeSpan frequency = TimeSpan.FromMinutes(timeSlot.AppointmentFrequency);

            while (currentTime + frequency <= timeSlot.AvailableTo)
            {
                TimeOnly appointmentTime = TimeOnly.FromTimeSpan(currentTime);

                if (afterTime.HasValue && appointmentTime <= afterTime.Value)
                {
                    currentTime = currentTime.Add(frequency);
                    continue;
                }

                bool isOccupied = occupiedAppointments.Any(x =>
                    x.DoctorId == timeSlot.DoctorId &&
                    x.Time.TimeOfDay == currentTime);

                if (!isOccupied)
                {
                    result.Add(new AvailableAppointmentResponseDTO(
                        timeSlot.DoctorId,
                        timeSlot.DoctorName,
                        timeSlot.Branch,
                        date,
                        appointmentTime));
                }

                currentTime = currentTime.Add(frequency);
            }
        }

        return result
            .OrderBy(x => x.Time)
            .ThenBy(x => x.DoctorName)
            .ToList();
    }

}
