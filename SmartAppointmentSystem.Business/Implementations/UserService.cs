using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Business.Extensions;
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
    public async Task<bool> CommitAsync()
    {
        var changed = await appointmentContext.SaveChangesAsync();
        return changed > 0;
    }
}
