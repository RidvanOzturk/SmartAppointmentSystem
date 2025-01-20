using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Business.Extensions;
using Microsoft.EntityFrameworkCore;
using SmartAppointmentSystem.Data.Entities;
using System.Linq;
using System.Collections.Generic;
using static Azure.Core.HttpHeader;
namespace SmartAppointmentSystem.Business.Implementations;

public class UserService(AppointmentContext appointmentContext) : IUserService
{
    public async Task<bool> RegisterAsync(UserRequestDTO requestDTO)
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
    public Task<UserResponseDTO> LoginUserAsync(UserRequestDTO request)
    {
        UserResponseDTO response = new();
        var user = request.Map();
        if (string.IsNullOrEmpty(request.Name) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }

        if (appointmentContext.Users.Any(x=>x.Name == request.Name) && appointmentContext.Users.Any(x=>x.PasswordHash == request.Password))
        {
            response.AccessTokenExpireDate = DateTime.UtcNow;
            response.AuthenticateResult = true;
            response.AuthToken = string.Empty;
        }

        return Task.FromResult(response);
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
    public async Task<bool> DeleteUserById(Guid id)
    {
        var user = await appointmentContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        var deletedById = appointmentContext.Users.Remove(user);
       
        return await CommitAsync();
    }
    public async Task<bool> CommitAsync()
    {
        var changed = await appointmentContext.SaveChangesAsync();
        return changed > 0;
    }
}
