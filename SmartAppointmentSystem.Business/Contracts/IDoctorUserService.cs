using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<Doctor> GetDoctorByIdAsync(Guid id);
    Task<List<Doctor>> GetAllDoctorsAsync();
    Task<List<DoctorsRatingDTO>> GetTopRatedDoctorsAsync();
    Task<bool> CreateDoctorAsync(DoctorUserRequestDTO requestDTO);
    Task<List<Doctor>> SearchDoctorsNameAsync(string query);
    Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request);
    Task<bool> UpdateDoctorByIdAsync(Guid id, DoctorUserRequestDTO requestDTO);
    Task<bool> DeleteDoctorByIdAsync(Guid id);
}
