using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
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
            .Where(x=> x.PatientId == id)
            .ToListAsync(cancellationToken);
    }
    public async Task<Appointment> GetAppointmentsByIdAsync(Guid id, CancellationToken cancellationToken)
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

}
