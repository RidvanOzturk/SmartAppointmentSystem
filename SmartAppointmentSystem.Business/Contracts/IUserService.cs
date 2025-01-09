using SmartAppointmentSystem.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IUserService
{
    Task<bool> RegisterAsync(RegisterRequestDTO requestDTO);
    Task<bool> CommitAsync();
}
