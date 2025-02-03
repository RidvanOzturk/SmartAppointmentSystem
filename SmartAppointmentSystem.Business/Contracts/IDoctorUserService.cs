using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IDoctorUserService
{
    Task<Doctor> GetDoctorById(Guid id);
    Task<bool> CreateDoctor(DoctorUserRequestDTO requestDTO);
    Task<UserResponseModel> LoginUserAsync(DoctorUserLoginRequestDTO request);
    Task<bool> UpdateDoctorById(Guid id, DoctorUserRequestDTO requestDTO);
}
