using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data;

namespace SmartAppointmentSystem.Business.Implementations;

public class UserService(AppointmentContext appointmentContext) : IUserService
{
    public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
    {
        // hashleme burada yapıcan

        // db'ye ekleme


     
        return true;
    }
}
