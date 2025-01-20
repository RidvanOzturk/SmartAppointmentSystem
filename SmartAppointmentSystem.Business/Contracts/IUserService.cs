using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IUserService
{
    Task<bool> RegisterAsync(UserRequestDTO requestDTO);
    public Task<UserResponseDTO> LoginUserAsync(UserRequestDTO request);
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid id);
    Task<bool> DeleteUserById(Guid id);
    Task<bool> CommitAsync();
}
