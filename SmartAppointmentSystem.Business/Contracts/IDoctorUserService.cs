using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<Doctor> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken = default);
    Task<List<Doctor>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken = default);
    Task CreateDoctorAsync(DoctorUserSignUpRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken = default);
    Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken = default);
    Task UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken = default);
    Task DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> IsDoctorExistAsync(Guid id, CancellationToken cancellationToken);
}
