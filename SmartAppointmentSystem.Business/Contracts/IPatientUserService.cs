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
    Task RegisterAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken);
    public Task<UserResponseModel> LoginUserAsync(PatientUserRequestDTO request ,CancellationToken cancellationToken);
    Task<List<Patient>> GetUsersAsync(CancellationToken cancellationToken);
    Task<Patient> GetUserByIdAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken);
}
