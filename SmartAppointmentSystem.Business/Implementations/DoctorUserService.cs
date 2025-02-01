using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.Extensions;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Implementations;

public class DoctorUserService(AppointmentContext context) : IDoctorUserService
{
    public async Task<Doctor> GetDoctorById(Guid id)
    {
        var getDoc = await context.Doctors.FirstOrDefaultAsync(x=>x.Id== id);
        return getDoc;
    }
    public async Task<bool> CreateDoctor(DoctorUserRequestDTO requestDTO)
    {
        var filled = requestDTO.Map();
        var createDoc = await context.Doctors.AddAsync(filled);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
