using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<DoctorResponseDTO> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<AllDoctorResponseDTO>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetNewAddedDoctors(CancellationToken cancellationToken = default);
    Task<bool> CreateDoctorAsync(DoctorUserSignUpRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken = default);
    Task<List<Doctor>> SearchDoctorsBranchAsync(int query, CancellationToken cancellationToken = default);
    Task<string?> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken = default);
    Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsDoctorExistAsync(Guid id, CancellationToken cancellationToken);
    Task<UserResponseModel> RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);
}
