using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(AppointmentContext context) : IAppointmentService
{
    public async Task<bool> CreateAppointmentAsync(AppointmentRequestDTO appointmentRequestDTO)
    {
        var appointment = appointmentRequestDTO.Map();
        await context.Appointments.AddAsync(appointment);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<List<Appointment>> GetAllAppointmentsAsync()
    {
        var allApp = await context.Appointments
            .AsNoTracking()
            .ToListAsync();
        return allApp;
    }
    public async Task<List<Appointment>> GetUserAppointmentsAsync(Guid id)
    {
        var gelUserAppointments = await context.Appointments
            .AsNoTracking()
            .Where(x=> x.PatientId == id)
            .ToListAsync();
        return gelUserAppointments;
    }
    public async Task<Appointment> GetAppointmentsByIdAsync(Guid id)
    {
        var app = context.Appointments
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
        return await app;
    }
    public async Task<bool> DeleteAppointmentByIdAsync(Guid id)
    {
        var app = await context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        if (app == null)
        {
            throw new Exception("There is no Appointment");
        }
        context.Appointments.Remove(app);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
    public async Task<bool> UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO)
    {
        var appId = await context.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        appointmentRequestDTO.Map(appId);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }

}
