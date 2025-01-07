using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;
using SmartAppointmentSystem.Data.Repositories.Contracts;

namespace SmartAppointmentSystem.Business.Implementations;

public class UserService(AppointmentContext appointmentContext) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
    {
        // hashleme burada yapıcan

        // db'ye ekleme


        var user = await userRepository.GetByMailandNameAsync(requestDTO.name, requestDTO.mail);
        if (user == null) 
        { 
            return false;
        }

        return true;
    }
}
