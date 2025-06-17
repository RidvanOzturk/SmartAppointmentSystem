using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Business.DTOs.RequestDTOs;
using SmartAppointmentSystem.Business.DTOs.ResponseDTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> GetNewAddedDoctors(CancellationToken cancellationToken = default);
    Task<bool> CreateDoctorAsync(DoctorUserSignUpRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken = default);
    Task<List<DoctorResponseDTO>> SearchDoctorsBranchAsync(int query, CancellationToken cancellationToken = default);
    Task<TokenReponse?> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken = default);
    Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsDoctorExistAsync(Guid id, CancellationToken cancellationToken = default);
}
