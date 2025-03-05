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
        await context.Appointments.AddAsync(appointmentEntity, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<Appointment>> GetAllAppointmentsAsync(CancellationToken cancellationToken)
    {
        var allAppointments = await context.Appointments
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return allAppointments;
    }
    public async Task<List<Appointment>> GetUserAppointmentsAsync(Guid id, CancellationToken cancellationToken)
    {
        var gelUserAppointments = await context.Appointments
            .AsNoTracking()
            .Where(x=> x.PatientId == id)
            .ToListAsync(cancellationToken);
        return gelUserAppointments;
    }
    public async Task<Appointment> GetAppointmentsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointment = context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return await appointment;
    }
    public async Task<bool> DeleteAppointmentByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var deletedAppointment = await context.Appointments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (deletedAppointment == null)
        {
            throw new Exception("There is no Appointment");
        }
        context.Appointments.Remove(deletedAppointment);
        var changes = await context.SaveChangesAsync(cancellationToken);
        return changes > 0;
    }
    public async Task<bool> UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO, CancellationToken cancellationToken)
    {
        var updatedAppointment = await context.Appointments
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        appointmentRequestDTO.Map(updatedAppointment);
        var changes = await context.SaveChangesAsync(cancellationToken);
        return changes > 0;
    }

}
