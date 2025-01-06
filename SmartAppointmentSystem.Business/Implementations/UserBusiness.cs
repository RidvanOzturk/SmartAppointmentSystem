using SmartAppointmentSystem.Business.Contracts;
using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Repositories.Contracts;
using SmartAppointmentSystem.Data.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Implementations;

public class UserBusiness(IUserRepository userRepository) : IUserBusiness
{
    public async Task<bool> RegisterAsync(RegisterRequestDTO requestDTO)
    {
        var user = await userRepository.GetByMailandNameAsync(requestDTO.name, requestDTO.mail);
        if (user == null) 
        { 
            return false;
        }

        return true;
    }
}
