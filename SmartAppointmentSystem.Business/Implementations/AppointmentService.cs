using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(AppointmentContext appointmentContext) : IAppointmentService
{
    public async Task<bool> CreateAppointment(AppointmentRequestDTO appointmentRequestDTO)
    {
        var filled = appointmentRequestDTO.Map();
        await appointmentContext.Appointments.AddAsync(filled);
        var changes = await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }

    public async Task<Appointment> GetAppointmentsById(Guid id)
    {
       var app = appointmentContext.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        return await app;
    }
    public async Task<bool> DeleteAppointmentById(Guid id)
    {
        var app = await appointmentContext.Appointments.FirstOrDefaultAsync(x => x.Id == id);
        if (app == null)
        {
            throw new Exception("There is no Appointment");
        }
        appointmentContext.Appointments.Remove(app);
        var changes =await appointmentContext.SaveChangesAsync();
        return changes > 0;
    }
   
}
