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
    Task<bool> CreatePatientAsync(PatientUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    public Task<UserResponseModel> LoginPatientUserAsync(PatientUserLoginRequestDTO request ,CancellationToken cancellationToken = default);
    Task<List<Patient>> GetPatientUsersAsync(CancellationToken cancellationToken = default);
    Task<Patient> GetPatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task DeletePatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsPatientExistAsync(Guid id, CancellationToken cancellationToken = default);
}
