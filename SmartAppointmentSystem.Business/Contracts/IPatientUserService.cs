using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IPatientUserService
{
    Task RegisterAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    public Task<UserResponseModel> LoginUserAsync(PatientUserLoginRequestDTO request ,CancellationToken cancellationToken = default);
    Task<List<Patient>> GetUsersAsync(CancellationToken cancellationToken = default);
    Task<Patient> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsPatientExistAsync(Guid id, CancellationToken cancellationToken = default);
}
