using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
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
    Task<TokenReponse?> LoginPatientUserAsync(PatientUserLoginRequestDTO request, CancellationToken cancellationToken = default);
    Task<List<PatientResponseDTO>> GetPatientUsersAsync(CancellationToken cancellationToken = default);
    Task<PatientResponseDTO?> GetPatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpdatePatientById(Guid id, PatientUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task DeletePatientUserByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsPatientExistAsync(Guid id, CancellationToken cancellationToken = default);
}
