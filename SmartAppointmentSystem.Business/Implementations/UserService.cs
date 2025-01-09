using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Business.Extensions;
namespace SmartAppointmentSystem.Business.Implementations;

public class UserService(AppointmentContext appointmentContext) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
    {
        var addUser = await appointmentContext.AddAsync(requestDTO);
        if (addUser == null) 
        {
            return false;
        }
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(requestDTO.Password); //  Passwordu hashledimm

        var user = requestDTO.Map();
        var result = await CommitAsync();
        return result != null;
        // db'ye ekleme

    }

    public async Task<bool> CommitAsync()
    {
        var changed = await appointmentContext.SaveChangesAsync();
        return changed > 0;
    }
}
