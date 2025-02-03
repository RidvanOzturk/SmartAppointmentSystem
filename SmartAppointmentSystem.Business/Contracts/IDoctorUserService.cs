using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<Doctor> GetDoctorById(Guid id);
    Task<List<Doctor>> GetAllDoctors();
    Task<bool> CreateDoctor(DoctorUserRequestDTO requestDTO);
    Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request);
    Task<bool> UpdateDoctorById(Guid id, DoctorUserRequestDTO requestDTO);
    Task<bool> DeleteDoctorById(Guid id);
}
