using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data.Entities;
using System.Collections.Generic;
namespace SmartAppointmentSystem.Business.Implementations;

public class UserService(AppointmentContext appointmentContext) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
    {
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password); 
        var user = requestDTO.Map();
        var addUser = await appointmentContext.Users.AddAsync(user);
        if (addUser == null) 
        {
            return false;
        }
        return await CommitAsync();
    }
    public async Task<List<User>> GetUsersAsync()
    {
        try
        {
            return await appointmentContext.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Veritabanından kullanıcıları alırken hata oluştu", ex);
        }
    }
    public async Task<User> GetUserByIdAsync(Guid id)
    {
        var user = await appointmentContext.Users.FirstOrDefaultAsync(x =>x.Id == id);
        return user;
    }
    public async Task<bool> CommitAsync()
    {
        var changed = await appointmentContext.SaveChangesAsync();
        return changed > 0;
    }
}
