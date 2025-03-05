using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<Doctor> GetDoctorByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken);
    Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync(CancellationToken cancellationToken);
    Task<List<Doctor>> GetDoctorsWithMostAppointmentsAsync(CancellationToken cancellationToken);
    Task CreateDoctorAsync(DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<List<Doctor>> SearchDoctorsNameAsync(string query, CancellationToken cancellationToken);
    Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request, CancellationToken cancellationToken);
    Task<bool> UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO, CancellationToken cancellationToken);
    Task<bool> DeleteDoctorByIdAsync(Guid id, CancellationToken cancellationToken);
}
