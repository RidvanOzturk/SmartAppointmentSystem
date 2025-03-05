using SmartAppointmentSystem.Business.DTOs;
using SmartAppointmentSystem.Data.Entities;

namespace SmartAppointmentSystem.Business.Contracts;

public interface IAppointmentService
{
    Task<bool> CreateAppointmentAsync(AppointmentRequestDTO requestDTO);
    Task<Appointment> GetAppointmentsByIdAsync(Guid id);
    Task<List<Appointment>> GetUserAppointmentsAsync(Guid id);
    Task<bool> DeleteAppointmentByIdAsync(Guid id);
    Task<bool> UpdateAppointmentByIdAsync(Guid id, AppointmentRequestDTO appointmentRequestDTO);
    Task<List<Appointment>> GetAllAppointmentsAsync();
}
