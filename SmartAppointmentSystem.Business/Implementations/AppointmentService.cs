﻿using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;

namespace SmartAppointmentSystem.Business.Implementations;

public class AppointmentService(AppointmentContext appointmentContext) : IAppointmentService
{
    public async Task<bool> CreateAppointment(AppointmentRequestDTO appointmentRequestDTO)
    {
        var filled = appointmentRequestDTO.Map();
        await appointmentContext.Appointments.AddAsync(filled);
        return await CommitAsync();
    }
    public async Task<bool> CommitAsync()
    {
        var changes = appointmentContext.SaveChangesAsync();
        return await changes > 0;
    }
}
